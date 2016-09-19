using System.Xml.Linq;
using StructuredData.Comparison.Interfaces;

namespace StructuredData.Comparison.Xml.Conversion
{
    public interface IConvertToStructuredDataNodes
    {
        IStructuredDataNode Convert(XElement element);
    }
}