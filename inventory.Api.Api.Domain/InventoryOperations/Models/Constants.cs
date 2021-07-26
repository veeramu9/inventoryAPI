using System;
using System.Collections.Generic;
using System.Text;

namespace inventory.Api.Api.Domain.Models
{
    public class Constants
    {
        private static readonly Dictionary<int, String> info = new Dictionary<int, string>()
        {
            {2,"Item Id is Mandatory" },
            {3,"Name or Description field is empty" },
            {4,"Price is empty or zero" }
        };

        public static string GetMessage(int key)
        {
            return info[key];
        }
    }
}
