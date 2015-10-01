using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Models
{
    public class CommitData
    {
        public string Sha { get; set; }

        public string Message { get; set; }
        public string MessageShort { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorUserName { get; set; }
        public string CommitterEmail { get; set; }
        public string CommitterUserName { get; set; }
        public DateTime DateAuthored { get; set; }
        public DateTime DateCommitted { get; set; }
        
        public Guid RepositoryId { get; set; }
    }
}
