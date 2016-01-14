using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SvgStudio.Shared.Drawing
{
    public abstract class DefObject
    {
        public abstract string CssClass { get; }
        public abstract IEnumerable<XElement> ToDefXml();

        public override int GetHashCode()
        {
            return CssClass.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DefObject))
            {
                return false;
            }
            else
            {
                return obj.GetHashCode() == this.GetHashCode();
            }
        }
    }
}
