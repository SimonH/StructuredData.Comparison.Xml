using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace StructuredData.Comparison.Xml.Tests
{
    [TestFixture]
    public class XmlComparisonFixture
    {
        private string LoadEmbeddedResource(string file)
        {
            var array = Assembly.GetAssembly(typeof(XmlComparisonFixture)).GetManifestResourceNames();
            var path = array.FirstOrDefault(s => s.EndsWith("." + file));
            if (path != null)
            {
                var stream = Assembly.GetAssembly(typeof(XmlComparisonFixture)).GetManifestResourceStream(path);
                return stream == null ? null : new StreamReader(stream).ReadToEnd();
            }
            return null;
        }

        [Test]
        [TestCase("SimpleValues.xml", "SimpleValues.xml", true, TestName = "SameXmlObject")]
        [TestCase("SimpleValues.xml", "DifferentSimpleValues.xml", false, TestName = "SameXmlObjectDifferentValues")]
        [TestCase("MultipleValues.xml", "IgnoredMultipleValues.xml", true, TestName = "IgnoringValues")]
        [TestCase("SourceList.xml", "SameListDefaultSettings.xml", true, TestName = "SameListDefaultSettings")]
        [TestCase("SourceList.xml", "ReducedListDefaultSettings.xml", true, TestName = "ReducedListDefaultSettings")]
        [TestCase("SourceList.xml", "ExtendedListDefaultSettings.xml", false, TestName = "ExtendedListDefaultSettings")]
        [TestCase("SourceList.xml", "SameListStrictOrderedSettings.xml", true, TestName = "SameListStrictOrderedSettings")]
        [TestCase("SourceList.xml", "ReducedListStrictOrderedSettings.xml", false, TestName = "ReducedListStrictOrderedSettings")]
        [TestCase("BatchRequestSource.xml", "BatchRequestExpected.xml", true, TestName = "BatchRequestWithIgnoredValues")]
        [TestCase("ListOfValues.xml", "ListOfValuesWithSettings.xml", true, TestName = "ListOfValues")]
        [TestCase("ListWithNamespaces.xml", "ListWithNamespaceWithSettings.xml", true, TestName = "ListWithNamespaces")]
        public void ComparingXml(string source, string result, bool expectNullOrWhitespace)
        {
            var data1 = LoadEmbeddedResource(source);
            var data2 = LoadEmbeddedResource(result);
            var candidate = data1.ContentComparison(data2, "text/xml");
            Assert.That(string.IsNullOrWhiteSpace(candidate), Is.EqualTo(expectNullOrWhitespace));
        }
    }
}