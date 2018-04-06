using System;
using System.Xml.Linq;

namespace AvinodeParser.Models
{
    public static class Parser
    {
        public static string GetXMLElement(XElement xel, string toFind, string defaultVal)
        {
            var xmlElement = defaultVal;

            try
            {
                if (xel != null && xel.Element(toFind) != null)
                    xmlElement = xel.Element(toFind).Value.Trim();
                else
                    xmlElement = defaultVal;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return xmlElement;
        }

        public static string GetXMLElementAttribute(XElement xel, string elementName, string toFind, string defaultVal)
        {
            var xmlElementAttribute = defaultVal;
            try
            {
                if (xel != null && xel.Element(elementName) != null)
                {
                    var element = xel.Element(elementName);

                    if (element.Attribute(toFind) != null)
                        xmlElementAttribute = element.Attribute(toFind).Value.Trim();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return xmlElementAttribute;
        }
    }
}