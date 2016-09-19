using System.ComponentModel.Composition;
using StructuredData.Comparison.Interfaces;

namespace StructuredData.Comparison.Xml.Exports
{
    [Export(typeof(IFileMimeType))]
    [ExportMetadata("Extension", "xml")]
    public class XmlMimeType : IFileMimeType
    {
        public string MimeType => "application/xml";
    }
}