using System;
using System.Collections.Generic;

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
    }
}