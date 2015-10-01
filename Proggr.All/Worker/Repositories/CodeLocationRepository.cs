using System;
using System.Configuration;
using System.IO;
using Simple.Data;
using Worker.Models;

namespace Worker.Repositories
{
    public class CodeLocationRepository : ICodeLocationRepository
    {
        private const string CloneRootSettingKey = "CloneJob:CloneRootPath";
        private readonly dynamic _database;

        public CodeLocationRepository()
        {
            _database = Database.OpenNamedConnection("DefaultConnection");
        }

        public CodeLocation GetCodeLocation(Guid codelocationId)
        {
            return _database.CodeLocations.Get(codelocationId);
        }

        public CodeLocation GetCodeLocation(string fullname)
        {
            return _database.CodeLocations.FindAllBy(FullName: fullname).FirstOrDefault();
        }

        public CodeLocation CreateCodeLocation(string fullname, string name)
        {
            var codelocation = GetCodeLocation(fullname);
            if (codelocation == null)
            {
                codelocation = (CodeLocation)_database.CodeLocations.Insert(FullName: fullname, Name: name);
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
            return _database.Commits.Insert(newCommit);
        }
    }
}
