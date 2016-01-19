using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public class DefinitionCollection : ICollection<XElement>
    {
        private ICollection<XElement> _inner = new List<XElement>();
        private HashSet<string> _ids = new HashSet<string>();

        public int Count
        {
            get
            {
                return _inner.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _inner.IsReadOnly;
            }
        }

        public void Add(XElement item)
        {
            if (item == null)
            {
                return;
            }

            var idAttr = item.Attribute("id");
            if (idAttr == null || string.IsNullOrWhiteSpace(idAttr.Value) || _ids.Contains(idAttr.Value))
            {
                return;
            }

            _inner.Add(item);
            _ids.Add(idAttr.Value);
        }

        public void Add(DefinitionCollection definitions)
        {
            if (definitions != null)
            {
                foreach (var def in definitions)
                {
                    Add(def);
                }
            }
        }

        public void Clear()
        {
            _inner.Clear();
        }

        public bool Contains(XElement item)
        {
            return _inner.Contains(item);
        }

        public void CopyTo(XElement[] array, int arrayIndex)
        {
            _inner.CopyTo(array, arrayIndex);
        }

        public IEnumerator<XElement> GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        public bool Remove(XElement item)
        {
            return _inner.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _inner.GetEnumerator();
        }
    }
}
