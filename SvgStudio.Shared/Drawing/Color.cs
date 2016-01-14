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
            Rgb,
            Rgba,
            Hex
        }

        private readonly Type _type;

        private Color(Type type)
        {
            _type = type;
        }

        public string Name { get; private set; }
        public byte A { get; private set; }
        public byte R { get; private set; }
        public byte G { get; private set; }
        public byte B { get; private set; }
        public string Hex { get; private set; }

        public static Color FromName(string name)
        {
            return new Color(Type.Named)
            {
                Name = name
            };
        }

        public static Color FromRgba(byte r, byte g, byte b, byte a)
        {
            return new Color(Type.Rgba)
            {
                R = r,
                G = g,
                B = b,
                A = a
            };
        }

        public static Color FromRgb(byte r, byte g, byte b)
        {
            return new Color(Type.Rgb)
            {
                R = r,
                G = g,
                B = b
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
                return Color.FromRgba(0, 0, 0, 0);
            }
        }

        public override string ToString()
        {
            switch (_type)
            {
                case Type.Named:
                    return Name;
                case Type.Rgb:
                    return string.Format("rgb({0},{1},{2})", R, G, B);
                case Type.Rgba:
                    return string.Format("rgba({0},{1},{2},{3})", R, G, B, A);
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
