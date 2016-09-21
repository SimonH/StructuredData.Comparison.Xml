using System.Collections.Generic;
using System.ComponentModel.Composition;
using StructuredData.Comparison.Interfaces;

namespace StructuredData.Comparison.Xml.Exports
{
    [Export(typeof(ICreateStructuredDataWalkers))]
    [ExportMetadata("MimeType", "text/xml")]
    public class WalkerCreator : ICreateStructuredDataWalkers
    {
        public IEnumerable<IStructuredDataNode> CreateWalker(string data)
        {
            return new XmlDataWalker(data);
        }
    }
}