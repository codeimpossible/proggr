using System;
using Proggr.Data.Models;

namespace Worker.Repositories
{
    public interface ICodeLocationRepository
    {
        CodeLocation GetCodeLocation(Guid codelocationId);
        CodeLocation GetCodeLocation(string fullname);
        CodeLocation CreateCodeLocation(string fullname, string name, string createdBy, bool isPublic = false);
        bool HasCodeLocationLocally(string fullname);
        string GetCodeLocationLocalPath(string fullname);

        CommitData AddCommit(CommitData newCommit, CodeLocation codeLocation);


        // TODO: probably find a better repository for these methods to live in
        bool RecordCodeLocationOnWorker(Guid workerId, Guid codelocationId);
    }
}