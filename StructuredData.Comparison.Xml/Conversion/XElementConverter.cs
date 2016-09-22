using System.Linq;
using System.Xml.Linq;
using StructuredData.Comparison.Interfaces;
using StructuredData.Comparison.Model;
using StructuredData.Comparison.Xml.Extensions;

namespace StructuredData.Comparison.Xml.Conversion
{
    public class XElementConverter : IConvertToStructuredDataNodes
    {
        public IStructuredDataNode Convert(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            var ret = new StructuredDataNode
            {
                Name = element.Name.NamespaceName + ":" + element.Name.LocalName, // output as namespace:localName rather than {namespace}localName as this is more standard for a qualified name
                IsValue = !element.HasElements,
                Value = element.HasElements ? null : element.Value,
                Children = element.Elements().Select(el => new XElementConverter().Convert(el)),
                Path = element.GetAbsoluteXPath()
            };
            return ret;
        }
    }
}