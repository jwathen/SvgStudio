using SvgStudio.Shared.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SvgStudio.Shared.Helpers
{
    public static class XmlHelper
    {
        private static readonly int INDENT_AMOUNT = 2;

        public static string RenderDocument(XDocument doc, bool includeDocTypeDelcaration)
        {
            StringBuilder builder = new StringBuilder();
            if (includeDocTypeDelcaration)
            {
                builder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            }
            builder.Append(WriteXElement(doc.Root));
            return builder.ToString();
        }

        public static string RemoveRootSvgElement(string input)
        {
            try
            {
                XElement svg = XElement.Parse(input);
                if (svg.Name.LocalName == "svg")
                {
                    return string.Join(Environment.NewLine, svg.Elements());
                }
            }
            catch { }

            return input;
        }

        public static string WriteXElement(XElement element, int indent = 0)
        {
            StringBuilder builder = new StringBuilder();
            WriteXElement(element, builder);
            return builder.ToString();
        }

        public static void WriteXElement(XElement element, StringBuilder builder, int indent = 0)
        {
            bool isRoot = indent == 0;
            bool isEmpty = element.HasElements == false && string.IsNullOrWhiteSpace(element.Value);
            bool isUnncessaryTag = !isRoot
                && element.Attributes().All(x => x.Name.LocalName == "xmlns" || x.IsNamespaceDeclaration)
                && element.Name.LocalName == "g";

            string indentString = new string(' ', indent);

            if (isUnncessaryTag)
            {
                indent -= INDENT_AMOUNT;
            }
            else
            {
                builder.Append(indentString);
                builder.Append("<");
                WriteXName(element.Name, builder);
                if (isRoot)
                {
                    builder.Append(" xmlns=\"");
                    builder.Append(xmlns.svg);
                    builder.Append("\" xmlns:xlink=\"");
                    builder.Append(xmlns.xlink);
                    builder.Append("\"");
                }
                foreach (var attr in element.Attributes().OrderBy(PrioritizeAttribute))
                {
                    WriteXAttribute(attr, builder);
                }
                if (isEmpty)
                {
                    builder.Append(" />");
                }
                else
                {
                    builder.Append(">");
                }
            }
            if (element.HasElements)
            {
                builder.Append(Environment.NewLine);
                foreach (var child in element.Elements())
                {
                    WriteXElement(child, builder, indent + INDENT_AMOUNT);
                    builder.Append(Environment.NewLine);
                }
                builder.Append(indentString);
            }
            else
            {
                WriteXValue(element.Value, builder);
            }

            if (!isEmpty && !isUnncessaryTag)
            {
                builder.Append("</" + element.Name.LocalName + ">");
            }
        }

        private static void WriteXName(XName name, StringBuilder builder)
        {
            string prefix = string.Empty;
            if (name.Namespace == xmlns.xlink)
            {
                prefix = "xlink:";
            }
            builder.Append(prefix + name.LocalName);
        }

        private static void WriteXAttribute(XAttribute attr, StringBuilder builder)
        {
            if (!attr.IsNamespaceDeclaration)
            {
                builder.Append(" ");
                WriteXName(attr.Name, builder);
                builder.Append("=\"");
                WriteXValue(attr.Value, builder);
                builder.Append("\"");
            }
        }

        private static void WriteXValue(string value, StringBuilder builder)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            builder.Append(value);
        }

        private static int PrioritizeAttribute(XAttribute attribute)
        {
            if (attribute.Name.LocalName == "id")
            {
                return 1;
            }
            else if (attribute.Name.LocalName == "data-stroke-index")
            {
                return 2;
            }
            else if (attribute.Name.LocalName == "data-fill-index")
            {
                return 3;
            }
            else if (attribute.Name.LocalName.StartsWith("stroke"))
            {
                return 4;
            }
            else if (attribute.Name.LocalName.StartsWith("fill"))
            {
                return 5;
            }
            else if (attribute.Name.LocalName != "d")
            {
                return 6;
            }
            else
            {
                return 7;
            }
        }
    }
}