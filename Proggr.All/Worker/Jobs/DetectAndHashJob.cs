using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using Proggr.LangDetect.Detectors;
using Worker.Controllers;
using Worker.Models;
using Worker.Repositories;

namespace Worker.Jobs
{
    public class DetectAndHashJobArgs : JobArgs
    {
        public IReadOnlyList<string> Shas { get; set; }

        public Guid RepositoryId { get; set; }
    }

    public class DetectAndHashJob : Job, IJob
    {
        private readonly ExtensionDetector _extDectector = new ExtensionDetector();

        private readonly ICodeLocationRepository _codeLocationRepository;
        private readonly IFileRepository _fileRepository;

        public DetectAndHashJob(JobDescriptor jobDescription, Guid workerId, ILocator locator) : 
            base(jobDescription, workerId, locator)
        {
            _codeLocationRepository = _serviceLocator.Locate<ICodeLocationRepository>();
            _fileRepository = _serviceLocator.Locate<IFileRepository>();
        }

        public JobDescriptor JobDescriptor { get; }

        public override async Task<JobResult> Run()
        {
            var args = JobDescriptor.GetArgumentsJson<DetectAndHashJobArgs>();
            var codeLocation = _codeLocationRepository.GetCodeLocation(args.RepositoryId);
            return await Task.Run(() =>
            {
                try
                {
                    var repoRoot = _codeLocationRepository.GetCodeLocationLocalPath(codeLocation.FullName);
                    var repo = new Repository(repoRoot, new RepositoryOptions() { });

                    Parallel.ForEach(args.Shas, async (sha) =>
                    {
                        var commit = await RepositoryController.GetCommit(repo, sha);
                        var patch = await CommitController.GetChangeSet(repo, commit);

                        Parallel.ForEach(patch, (change) =>
                        {
                            if (change.Mode == Mode.NonExecutableFile && change.Status != ChangeKind.Deleted &&
                                change.Status != ChangeKind.Ignored && change.Status != ChangeKind.Untracked)
                            {
                                var filePath = Path.Combine(repoRoot, change.Path);
                                string contents = null;
                                if (!change.IsBinaryComparison)
                                {
                                    var treeEntry = commit.Tree[change.Path];
                                    if (treeEntry.TargetType == TreeEntryTargetType.Blob)
                                    {
                                        // this is a file we can open and read
                                        // get the file contents
                                        var blob = (Blob) treeEntry.Target;
                                        var contentStream = blob.GetContentStream();
                                        using (var reader = new StreamReader(contentStream, Encoding.UTF8))
                                        {
                                            contents = reader.ReadToEnd();
                                        }
                                    }
                                }

                                var detection = _extDectector.Detect(filePath, contents);

                                // compute our hashes
                                byte[] raw, nowhitespace, nonewlines;
                                using (MD5 md5 = MD5.Create())
                                {
                                    raw = md5.ComputeHash(Encoding.UTF8.GetBytes(contents));
                                    nowhitespace = md5.ComputeHash(Encoding.UTF8.GetBytes(contents.Replace(" ", "")));
                                    nonewlines = md5.ComputeHash(Encoding.UTF8.GetBytes(contents.Replace("\n", "")));
                                }

                                // save the results
                                _fileRepository.AddFile(new FileData
                                {
                                    CommitId = sha,
                                    Ext = detection.Extension,
                                    FileName = filePath,
                                    RelativePath = change.Path,
                                    HashRaw = raw,
                                    HashNoWhiteSpace = nowhitespace,
                                    HashNoNewLines = nonewlines
                                });
                            }
                        });
                        // get the files that changed in this commit
                    });


                    return Task.FromResult<JobResult>(new JobSuccessResult(this));
                }
                catch (Exception e)
                {
                    return Task.FromResult<JobResult>(new JobFailureResult(e, this));
                }
            });
        }
    }
}
