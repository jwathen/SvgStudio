using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace SvgStudio.Web.Helpers
{
    public static class XmlHelper
    {
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

        public static string EmitStrokeAndFillAttributesFirst(XElement element, int indent = 0)
        {
            StringBuilder result = new StringBuilder();
            string indentString = new string(' ', indent);
            result.Append(indentString);
            result.Append("<" + element.Name);            
            foreach(var attr in element.Attributes().OrderBy(x => PrioritizeAttribute(x)))
            {
                result.Append(" " + attr);
            }
            result.Append(">");
            foreach(var child in element.Elements())
            {
                result.AppendLine(EmitStrokeAndFillAttributesFirst(child, indent + 2));
            }
            result.Append(indentString + "</" + element.Name + ">");
            return result.ToString();
        }

        private static int PrioritizeAttribute(XAttribute attribute)
        {
            if (attribute.Name.ToString().StartsWith("stroke"))
            {
                return 1;
            }
            else  if (attribute.Name.ToString().StartsWith("fill"))
            {
                return 2;
            }
            else if (attribute.Name.ToString() != "d")
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }

        //Core recursion function
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