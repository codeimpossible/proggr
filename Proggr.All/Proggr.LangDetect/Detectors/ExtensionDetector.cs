using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proggr.LangDetect.Detectors
{
    public class DetectionResult
    {
        public static readonly DetectionResult Unknown = new DetectionResult() {Language = null, Lines = 0};

        public ProgrammingLanguage? Language { get; set; }
        public int Lines { get; set; }

        public string Extension { get; set; }
    }

    public class ExtensionDetector
    {
        private readonly IDictionary<string, ProgrammingLanguage> _map = new Dictionary<string, ProgrammingLanguage>();

        public ExtensionDetector()
        {
            AddMapping(ProgrammingLanguage.JavaScript, "js", "jsx", "es6", "babel");
            AddMapping(ProgrammingLanguage.C, "c", "h");
            AddMapping(ProgrammingLanguage.CPlusPlus, "cpp");
            AddMapping(ProgrammingLanguage.CSS, "css");
            AddMapping(ProgrammingLanguage.CSharp, "cs");
            AddMapping(ProgrammingLanguage.HTML, "htm", "html");
            AddMapping(ProgrammingLanguage.JSON, "json");
            AddMapping(ProgrammingLanguage.Java, "java");
            AddMapping(ProgrammingLanguage.Python, "py");
            AddMapping(ProgrammingLanguage.Ruby, "rb");
            AddMapping(ProgrammingLanguage.VisualBasic, "vb");
            AddMapping(ProgrammingLanguage.XML, "xml");
            AddMapping(ProgrammingLanguage.YAML, "yaml", "yml");
        }

        public DetectionResult Detect(string filepath, string filecontents = null)
        {
            // Path.GetExtension() will return the whole filename for "dot" files (e.g. ".gitignore")
            var ext = Path.GetExtension(filepath);
            var lines = String.IsNullOrWhiteSpace(filecontents) ? 0 : filecontents.Split('\n').Length;

            if (_map.ContainsKey(ext))
            {
                return new DetectionResult() {Language = _map[ext], Lines = lines, Extension = ext };
            }

            return DetectionResult.Unknown;
        }

        private void AddMapping(ProgrammingLanguage lang, params string[] extensions)
        {
            extensions.ToList().ForEach(ext => _map.Add($".{ext}", lang));
        }
    }
}
