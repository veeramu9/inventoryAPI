using System;
using System.Collections.Generic;
using System.Text;

namespace inventory.Api.Api.Domain.Models
{
        public class Item
        {
            public int ItemId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool InStock { get; set; }
            public decimal Price { get; set; }
        }
 
}
