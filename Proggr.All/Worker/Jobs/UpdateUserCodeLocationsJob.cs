using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Models;
using Worker.Repositories;

namespace Worker.Jobs
{

    public class UpdateUserCodeLocationsJobArgs
    {
        public string UserName { get; set; }
    }

    public class UpdateUserCodeLocationsJob : Job, IJob
    {
        private IApiDataRepository _apiDataRepository;
        private ICodeLocationRepository _codeLocationRepository;

        public UpdateUserCodeLocationsJob(JobDescriptor jobDescription, Guid workerId, ILocator locator) : base(jobDescription, workerId, locator)
        {
            _apiDataRepository = locator.Locate<IApiDataRepository>();
            _codeLocationRepository = locator.Locate<ICodeLocationRepository>();
        }

        public override async Task<JobResult> Run()
        {
            var args = JobDescriptor.GetArgumentsJson<UpdateUserCodeLocationsJobArgs>();
            return await Task.Run(() =>
            {
                try
                {
                    var repos = _apiDataRepository.GetRepositories(args.UserName);
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
