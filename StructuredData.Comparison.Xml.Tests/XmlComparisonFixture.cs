﻿using System.IO;
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
        [TestCase("ListOfValues.xml", "ListOfValuesWithSettings.xml", true, TestName = "ListOfValuesAsNodes")]
        [TestCase("ListWithNamespaces.xml", "ListWithNamespaceWithSettings.xml", true, TestName = "ListWithNamespaces")]
        [TestCase("ListOfValues.xml", "ReducedListOfValuesAsList.xml", true, TestName = "ReducedListOfValuesAsList")]
        [TestCase("ListOfValues.xml", "CaseSensitiveListOfValues.xml", false, TestName = "CaseSensitiveListOfValuesAsList")]
        [TestCase("SimpleValues.xml", "CaseSensitiveSimpleValues.xml", false, TestName = "CaseSensitiveXmlObject")]
        public void ComparingXml(string source, string result, bool expectNullOrWhitespace)
        {
            var data1 = LoadEmbeddedResource(source);
            var data2 = LoadEmbeddedResource(result);
            if(data1 == null || data2 == null)
            {
                Assert.Fail("Could not load data");
            }
            var candidate = data1.ContentComparison(data2, "text/xml");
            Assert.That(string.IsNullOrWhiteSpace(candidate), Is.EqualTo(expectNullOrWhitespace));
        }
    }
}