using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace SvgStudio.Web.Helpers
{
    public static class XmlHelper
    {
        public const int INDENT_AMOUNT = 2;

        //Implemented based on interface, not part of algorithm
        public static string RemoveAllNamespaces(string xmlDocument)
        {
            try
            {
                XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(xmlDocument));

                return xmlDocumentWithoutNs.ToString();
            }
            catch
            {
                return xmlDocument;
            }
        }

        public static string AddRootNamespace(XElement element, string ns)
        {
            var result = element.ToString();
            int nameLength = element.Name.LocalName.Length;
            result = result.Substring(nameLength + 1, result.Length - (nameLength + 1));
            result = "<" + element.Name.LocalName + " xmlns=\"" + ns + "\" " + result;
            return result;
        }

        public static string EmitStrokeAndFillAttributesFirst(XElement element, int indent = 0)
        {
            StringBuilder builder = new StringBuilder();

            bool isUnnecessaryGroupTag = element.Name == "g" && !element.Attributes().Any(x => !x.IsNamespaceDeclaration) && indent != 0;
            bool isSvgTag = element.Name == "svg";
            
            if (isUnnecessaryGroupTag)
            {
                indent -= INDENT_AMOUNT;
            }

            string indentString = new string(' ', indent);
            if (!isUnnecessaryGroupTag && !isSvgTag)
            {
                builder.Append(indentString);
                builder.Append("<" + element.Name);
                foreach (var attr in element.Attributes().OrderBy(x => PrioritizeAttribute(x)))
                {
                    if (!attr.IsNamespaceDeclaration)
                    {
                        builder.Append(" " + attr);
                    }
                }
                builder.Append(">");
                if (element.HasElements)
                {
                    builder.Append(Environment.NewLine);
                }
            }

            foreach(var child in element.Elements())
            {
                builder.AppendLine(EmitStrokeAndFillAttributesFirst(child, indent + INDENT_AMOUNT));
            }
            if (!isUnnecessaryGroupTag && !isSvgTag)
            {
                if (element.HasElements)
                {
                    builder.Append(indentString);
                }
                builder.Append("</" + element.Name + ">");
            }

            string result = string.Join(Environment.NewLine, builder.ToString().Split(new[] { Environment.NewLine },StringSplitOptions.RemoveEmptyEntries));
            return result;
        }

        private static int PrioritizeAttribute(XAttribute attribute)
        {
            if (attribute.Name.ToString().StartsWith("data-stroke-index"))
            {
                return 1;
            }
            else if (attribute.Name.ToString().StartsWith("data-fill-index"))
            {
                return 2;
            }
            else if (attribute.Name.ToString().StartsWith("stroke"))
            {
                return 3;
            }
            else  if (attribute.Name.ToString().StartsWith("fill"))
            {
                return 4;
            }
            else if (attribute.Name.ToString() != "d")
            {
                return 5;
            }
            else
            {
                return 6;
            }
        }

        private static XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    xElement.Add(attribute);

                return xElement;
            }
            return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
        }

        
    }
}