using System;
using System.Collections.Generic;
using System.Text;

namespace AvinodeParser.Models
{
    public class AvinodeMenuItem
    {
        public string DisplayName { get; set; } = String.Empty;
        public string Path { get; set; } = String.Empty;
        public bool IsActive { get; set; } = false;
        public List<AvinodeMenuItem> SubMenuItems { get; set; } = new List<AvinodeMenuItem>();

        public bool GetIsActive(string toMatch)
        {
            var toReturn = false;

            if (Path == toMatch)
                toReturn = true;

            //always check subitems
            if (SubMenuItems.Count > 0)
            {
                foreach (var item in SubMenuItems)
                {
                    if (item.GetIsActive(toMatch))
                    {
                        toReturn = true;
                    }
                }
            }

            IsActive = toReturn;
            return toReturn;
        }

        public string Print(string indent = "\t")
        {
            var builder = new StringBuilder();

            builder.Append(DisplayName + ", " + Path);
            builder.AppendLine(IsActive ? " ACTIVE" : String.Empty);

            if (SubMenuItems.Count > 0)
            {
                var childBuilder = new StringBuilder();
                foreach (var item in SubMenuItems)
                {
                    childBuilder.Append(indent + item.Print(indent + "\t"));
                }

                builder.Append(childBuilder.ToString());
            }

            return builder.ToString();
        }
    }
}