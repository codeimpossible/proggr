using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proggr.LangDetect
{
    public enum FileType
    {
        SourceFile,         // a file that contains source code that belongs to the project
        ConfigFile,         // a file that is used to configure the project (project.json, packages.config, web.config, app.config, database.yml)
        EditorFile,         // a file that is typically only used by an editor (.editorconfig, .csproj, .suo, .idea)
        EnvironmentFile,    // a file that is used by the environment (.DS_Store, Thumbs.db)
        ServiceFile,        // a file that is used to configure SAAS deployment (.procfile, publish.xml)
        SourceControlFile,  // a file that is used to configure how source control handles this project (.gitignore, .gitattributes)
    }

    public enum ProgrammingLanguage
    {
        JavaScript,
        CSharp,
        VisualBasic,
        Python,
        C,
        CPlusPlus,
        Ruby,
        Java,
        CSS,
        HTML,
        XML,
        JSON,
        YAML,
    }
}
