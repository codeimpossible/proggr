using System;
using System.ComponentModel;
using System.Threading.Tasks;
using LibGit2Sharp;
using Newtonsoft.Json;
using Worker.Controllers;
using Worker.Models;
using Worker.Repositories;

namespace Worker.Jobs
{
    public class CloneJobArgs : JobArgs
    {
        public string Url { get; set; }

        public string Username { get; set; }

        public bool Bare { get; set; }

        public string RepoName { get; set; }

        public string RepoFullName { get; set; }

        [DefaultValue("master")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string Branch { get; set; }
    }

    public class CloneJob : Job
    {
        private readonly ICodeLocationRepository _codeLocationRepository;
        private readonly IRepositoryController _repositoryController;

        public CloneJob(JobDescriptor jobDescription, Guid workerId, ILocator locator) : 
            base(jobDescription, workerId, locator)
        {
            _codeLocationRepository = _serviceLocator.Locate<ICodeLocationRepository>();
            _repositoryController = _serviceLocator.Locate<IRepositoryController>();
        }

        public override async Task<JobResult> Run()
        {
            var args = JobDescriptor.GetArgumentsJson<CloneJobArgs>();
            var fullname = args.Url.Replace("https://github.com/", "")
                                    .Replace("http://github.com/", "")
                                    .Replace(".git", "");
            var clonePathAbs = _codeLocationRepository.GetCodeLocationLocalPath(fullname);
            return await Task.Run(() =>
            {
                var options = new CloneOptions()
                {
                    BranchName = args.Branch,
                    IsBare = args.Bare,
                    Checkout = true,
                    CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials
                    {
                        Username = args.Username,
                        Password = _apiRepository.GetUserToken(args.Username)
                    }
                };
                try
                {
                    var result = _repositoryController.Clone(args.Url, clonePathAbs, options);
                    
                    // create an entry for the repository in our db, so we can link commits to it
                    var codelocation = _codeLocationRepository.CreateCodeLocation(fullname, args.RepoName, args.Username);

                    // let other workers know that this worker has this code location
                    _codeLocationRepository.RecordCodeLocationOnWorker(WorkerId, codelocation.Id);

                    // schedule an import
                    _jobRepository.ScheduleJob<ImportHistoryJob>(codelocation);

                    _jobRepository.CompleteJob(Id, WorkerId, new
                    {
                        localPath = result
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
