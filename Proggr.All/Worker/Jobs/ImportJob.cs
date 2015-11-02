using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using LibGit2Sharp;
using Proggr.Data.Models;
using Worker.Models;
using Worker.Repositories;

namespace Worker.Jobs
{
    public class ImportHistoryJob : Job
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof (ImportHistoryJob));

        private readonly ICodeLocationRepository _codeLocationRepository;

        public ImportHistoryJob(JobDescriptor jobDescription, Guid workerId, ILocator locator) 
            : base(jobDescription, workerId, locator)
        {
            _codeLocationRepository = _serviceLocator.Locate<ICodeLocationRepository>();
        }

        private List<T>[] ToChunks<T>(List<T> list, int maxChunkSize)
        {
            return list
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / maxChunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToArray();
        }


        public override async Task<JobResult> Run()
        {
            var args = JobDescriptor.GetArgumentsJson<CodeLocation>();
            return await Task.Run(() =>
            {
                try
                {
                    if(!_codeLocationRepository.HasCodeLocationLocally(args.FullName))
                    {
                        // TODO: get the codelocation from the worker that cloned it? or run a quick clone job here...
                    }

                    var repo = new Repository(_codeLocationRepository.GetCodeLocationLocalPath(args.FullName), new RepositoryOptions() {});

                    repo.Fetch("origin");

                    int count = 0;
                    var start = DateTime.Now.Ticks;
                    Parallel.ForEach(repo.Commits, (commit) =>
                    {
                        count++;
                        var newCommit = new CommitData()
                        {
                            Sha = commit.Sha,
                            Message = commit.Message,
                            MessageShort = commit.MessageShort,
                            AuthorEmail = commit.Author.Email,
                            AuthorUserName = commit.Author.Name,
                            CommitterEmail = commit.Committer.Email,
                            CommitterUserName = commit.Committer.Name,
                            DateAuthored = commit.Author.When.DateTime.ToUniversalTime(),
                            DateCommitted = commit.Committer.When.DateTime.ToUniversalTime(),
                            RepositoryId = args.Id
                        };
                        _codeLocationRepository.AddCommit(newCommit, args);
                    });

                    var duration = new TimeSpan(DateTime.Now.Ticks - start).TotalMilliseconds;

                    _logger.Info($"{count} commits imported in {duration}ms");

                    _jobRepository.CompleteJob(Id, WorkerId, new { NumCommits = count, Duration = duration });

                    // break the list of commits into chunks, no bigger than 512 commits in length
                    // then create a DetectAndHashJob for each chunk.
                    var chunks = ToChunks(repo.Commits.Select(c => c.Sha).ToList(), DetectAndHashJob.MAX_NUMBER_OF_SHAS_TO_PROCESS);
                    Parallel.ForEach(chunks, (chunk) =>
                    {
                        // create a DetectAndHashJob for this chunk
                        _jobRepository.ScheduleJob<DetectAndHashJob>(new DetectAndHashJobArgs()
                        {
                            RepositoryId = args.Id,
                            Shas = chunk
                        });
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