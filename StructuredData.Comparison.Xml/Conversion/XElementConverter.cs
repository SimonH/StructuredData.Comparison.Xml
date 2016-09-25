using System.Linq;
using System.Xml.Linq;
using StructuredData.Comparison.Interfaces;
using StructuredData.Comparison.Model;
using StructuredData.Comparison.Xml.Extensions;

namespace StructuredData.Comparison.Xml.Conversion
{
    public class XElementConverter
    {
        public static IStructuredDataNode Convert(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            // output as namespace:localName rather than {namespace}localName as this is more standard for a qualified name
            var name = element.Name.LocalName;
            if(!string.IsNullOrWhiteSpace(element.Name.NamespaceName))
            {
                name = element.Name.NamespaceName + ":" + element.Name.LocalName;
            }
            var ret = new StructuredDataNode
            {
                Name = name, 
                IsValue = !element.HasElements,
                Value = element.HasElements ? null : element.Value,
                Children = element.Elements().Select(Convert),
                Path = element.GetAbsoluteXPath()
            };
            return ret;
        }
    }
}