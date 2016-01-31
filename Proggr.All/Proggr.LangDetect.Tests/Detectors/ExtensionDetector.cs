using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Proggr.LangDetect.Detectors;
using Xunit;

namespace Proggr.LangDetect.Tests.Detectors
{
    public class ExtensionDetectorTests
    {
        public class DetectMethod
        {
            public class WhenGivenASourceFile
            {
                [Theory]
                [InlineData("./test/test/file.cs", "test.cs", ProgrammingLanguage.CSharp, 12)]
                [InlineData("./test/test/file.jsx", "test.jsx", ProgrammingLanguage.JavaScript, 20)]
                public void ShouldReturnTheLanguageAndNumberOfLines(string filepath, string resourceName,
                    ProgrammingLanguage expectedLanguage, int expectedlines)
                {
                    var detector = new ExtensionDetector();
                    var assembly = Assembly.GetExecutingAssembly();

                    var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();

                    using (var stream = assembly.GetManifestResourceStream($"Proggr.LangDetect.Tests.Fixtures.{resourceName}"))
                    using (var reader = new StreamReader(stream))
                    {
                        var contents = reader.ReadToEnd();
                        var result = detector.Detect(filepath, contents);

                        Assert.Equal(expectedLanguage, result.Language);
                        Assert.Equal(expectedlines, result.Lines);
                    }
                }
            }

            public class WhenGivenADotFile
            {
                [Fact]
                public void ShouldClassifyTheFileAsUnknown()
                {
                    var detector = new ExtensionDetector();
                    var result = detector.Detect("./some/file/path/.gitignore");

                    Assert.Equal(null, result.Language);
                    Assert.Equal(DetectionResult.Unknown, result);
                }

                [Fact]
                public void ShouldSetTheExtensionAsEmpty()
                {
                    var detector = new ExtensionDetector();
                    var result = detector.Detect("./some/file/path/.gitignore");

                    Assert.Equal(null, result.Extension);
                }
            }
        }
    }
}
