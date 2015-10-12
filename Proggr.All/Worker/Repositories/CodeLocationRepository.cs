using System;
using System.Configuration;
using System.IO;
using Simple.Data;
using Worker.Models;

namespace Worker.Repositories
{
    public class CodeLocationRepository : SimpleDataRepository, ICodeLocationRepository
    {
        private const string CloneRootSettingKey = "CloneJob:CloneRootPath";

        public CodeLocation GetCodeLocation(Guid codelocationId)
        {
            return Database.CodeLocations.Get(codelocationId);
        }

        public CodeLocation GetCodeLocation(string fullname)
        {
            return Database.CodeLocations.FindAllBy(FullName: fullname).FirstOrDefault();
        }

        public CodeLocation CreateCodeLocation(string fullname, string name, string createdBy, bool isPublic = false)
        {
            var codelocation = GetCodeLocation(fullname);
            if (codelocation == null)
            {
                codelocation = (CodeLocation)Database.CodeLocations.Insert(FullName: fullname, Name: name, CreatedBy: createdBy, IsPublic: isPublic);
                if (codelocation == null)
                {
                    return GetCodeLocation(fullname);
                }
            }
            return codelocation;
        }

        public bool HasCodeLocationLocally(string fullname)
        {
            return Directory.Exists(GetCodeLocationLocalPath(fullname));
        }

        public string GetCodeLocationLocalPath(string fullname)
        {
            var root = ConfigurationManager.AppSettings[CloneRootSettingKey];
            return Path.Combine(Directory.GetCurrentDirectory(), $"{root}/{fullname.Replace("/", "-")}");
        }

        public CommitData AddCommit(CommitData newCommit, CodeLocation codeLocation)
        {
            newCommit.RepositoryId = codeLocation.Id;
            return Database.Commits.Insert(newCommit);
        }
    }
}
