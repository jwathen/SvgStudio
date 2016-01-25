using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SvgStudio.Shared.Drawing
{
    public class Color
    {
        private enum Type
        {
            Named,
            Hex
        }

        private readonly Type _type;

        private Color(Type type)
        {
            _type = type;
        }

        public string Name { get; private set; }
        public string Hex { get; private set; }

        public static Color FromName(string name)
        {
            return new Color(Type.Named)
            {
                Name = name
            };
        }

        public static Color FromHex(string hex)
        {
            if (!hex.StartsWith("#"))
            {
                hex = "#" + hex;
            }
            hex = hex.ToLower();

            return new Color(Type.Hex)
            {
                Hex = hex
            };
        }

        public static Color Transparent
        {
            get
            {
                return Color.FromName("none");
            }
        }

        public override string ToString()
        {
            switch (_type)
            {
                case Type.Named:
                    return Name;
                case Type.Hex:
                    return Hex;
                default:
                    throw new NotImplementedException();
            }
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Color))
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
