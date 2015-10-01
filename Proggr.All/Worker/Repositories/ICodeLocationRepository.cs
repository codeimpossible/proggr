using System;
using Worker.Models;

namespace Worker.Repositories
{
    public interface ICodeLocationRepository
    {
        CodeLocation GetCodeLocation(Guid codelocationId);
        CodeLocation GetCodeLocation(string fullname);
        CodeLocation CreateCodeLocation(string fullname, string name);
        bool HasCodeLocationLocally(string fullname);
        string GetCodeLocationLocalPath(string fullname);

        CommitData AddCommit(CommitData newCommit, CodeLocation codeLocation);
    }
}