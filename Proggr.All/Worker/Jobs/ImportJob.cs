using System;
using System.Threading.Tasks;
using log4net;
using LibGit2Sharp;
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