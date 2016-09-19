using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using StructuredData.Comparison.Interfaces;
using StructuredData.Comparison.Xml.Conversion;

namespace StructuredData.Comparison.Xml
{
    public class XmlDataWalker : IEnumerable<IStructuredDataNode>
    {
        private readonly XElement _element;
        public XmlDataWalker(string xmlData)
        {
            try
            {
                _element = XElement.Parse(xmlData);
            }
            catch
            {
                _element = null;
            }
        }

        public IEnumerator<IStructuredDataNode> GetEnumerator()
        {
            if (_element == null)
            {
                yield break;
            }
            yield return new XElementConverter().Convert(_element);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}