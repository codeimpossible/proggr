using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Models
{
    public class FileData
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Ext { get; set; }
        public string RelativePath { get; set; }
        public string CommitId { get; set; }
        public byte[] HashRaw { get; set; }
        public byte[] HashNoWhiteSpace { get; set; }
        public byte[] HashNoNewLines { get; set; }
        public string ContentType { get; set; }
    }
}
