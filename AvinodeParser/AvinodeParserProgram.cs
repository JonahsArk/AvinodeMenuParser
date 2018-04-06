using AvinodeParser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace AvinodeParser
{
    public static class AvinodeParserProgram
    {
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                if (args.Length > 2)
                    Console.WriteLine("Please input only two arguments.");
                else if (args.Length == 1)
                    Console.WriteLine("Please input two arguments.");
                else
                {
                    var builder = new StringBuilder();
                    var info = LoadFile(args[0]);
                    var menuItems = ProcessFileString(info);

                    CheckActiveItems(menuItems, args[1]);
                    BuildPrintableMenu(menuItems, builder);
                    Console.WriteLine(builder);
                }
            }
            else
            {
                Console.WriteLine("Please input the correct arguments. ");
            }
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
                Console.WriteLine("Error while loading file: " + ex);
            }

            return contents;
        }

        private static List<AvinodeMenuItem> ProcessFileString(string fileString)
        {
            var menuItems = new List<AvinodeMenuItem>();

            try
            {
                if (!String.IsNullOrWhiteSpace(fileString))
                {
                    var xdoc = XDocument.Parse(fileString);
                    var menuItemNodes = xdoc.Root.Elements("item");

                    menuItems = ParseMenuItems(menuItemNodes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while process file: " + ex);
            }

            return menuItems;
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

        private static void CheckActiveItems(List<AvinodeMenuItem> menuItems, string toMatch)
        {
            foreach (var item in menuItems)
            {
                item.GetIsActive(toMatch);
            }
        }

        private static void BuildPrintableMenu(List<AvinodeMenuItem> menuItems, StringBuilder builder)
        {
            var childBuilder = new StringBuilder();
            foreach (var item in menuItems)
            {
                builder.Append(item.Print());
            }
        }
    }
}