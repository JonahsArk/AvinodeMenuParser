using AvinodeParser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace AvinodeParser
{
    public static class AvinodeParserProgram
    {
        public const string file1 = @"";
        public const string file2 = @"";
        public const string searchText = @"";

        static void Main(string[] args)
        {
            var info1 = LoadFile(file1);
            var info2 = LoadFile(file2);

            ProcessFileString(info1, searchText);
        }

        private static string LoadFile(string location)
        {
            var contents = String.Empty;

            try
            {
                if (!String.IsNullOrWhiteSpace(location))
                {
                    using (StreamReader streamReader = new StreamReader(location))
                    {
                        contents = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return contents;
        }

        private static void ProcessFileString(string fileString, string toMatch)
        {
            try
            {
                var xdoc = XDocument.Parse(fileString);

                var menuItemNodes = xdoc.Root.Elements("item");

                var menuItems = ParseMenuItems(menuItemNodes);

                foreach (var itm in menuItems)
                {
                    itm.GetIsActive(toMatch);
                }
                //var items = Parser.GetXMLValue(xdoc, "item", String.Empty);
                //var test = Parser.GetXMLElement(xdoc, "item", String.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static List<AvinodeMenuItem> ParseMenuItems(IEnumerable<XElement> menuItems)
        {
            if (menuItems != null)
            {
                var avinodeMenuItems = new List<AvinodeMenuItem>();

                foreach (var item in menuItems)
                {
                    var avinodeItem = new AvinodeMenuItem();
                    avinodeItem.DisplayName = Parser.GetXMLElement(item, "displayName", String.Empty);
                    avinodeItem.Path = Parser.GetXMLElementAttribute(item, "path", "value", String.Empty);

                    //check for subItems in item.
                    var subMenu = item.Elements("subMenu");
                    if (subMenu != null)
                    {
                        var subMenuItems = ParseMenuItems(subMenu.Elements("item"));

                        if (subMenuItems != null)
                        {
                            avinodeItem.SubMenuItems = subMenuItems;
                        }
                    }

                    avinodeMenuItems.Add(avinodeItem);
                }

                return avinodeMenuItems;
            }

            return null;
        }
    }
}