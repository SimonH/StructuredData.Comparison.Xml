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
            var path = array.FirstOrDefault(s => s.EndsWith(file));
            if (path != null)
            {
                var stream = Assembly.GetAssembly(typeof(XmlComparisonFixture)).GetManifestResourceStream(path);
                return stream == null ? null : new StreamReader(stream).ReadToEnd();
            }
            return null;
        }

        [Test]
        public void ComparingSameXmlObject()
        {
            var data1 = LoadEmbeddedResource(@"SimpleValues.xml");
            var data2 = LoadEmbeddedResource(@"SimpleValues.xml");
            var candidate = data1.ContentComparison(data2, "application/xml");
            Assert.True(string.IsNullOrWhiteSpace(candidate));
        }

        [Test]
        public void ComparingXmlObjectsWithDifferentValues()
        {
            var data1 = LoadEmbeddedResource(@"SimpleValues.xml");
            var data2 = LoadEmbeddedResource(@"DifferentSimpleValues.xml");
            var candidate = data1.ContentComparison(data2, "application/xml");
            Assert.False(string.IsNullOrWhiteSpace(candidate));
        }

        [Test]
        public void ComparingXmlObjectsWithIgnoredValues()
        {
            var data1 = LoadEmbeddedResource(@"MultipleValues.xml");
            var data2 = LoadEmbeddedResource(@"IgnoredMultipleValues.xml");
            var candidate = data1.ContentComparison(data2, "application/xml");
            Assert.True(string.IsNullOrWhiteSpace(candidate));
        }
    }
}