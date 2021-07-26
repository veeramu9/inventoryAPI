using inventory.Api.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace inventory.Api.Api.Domain.Interfaces
{
    public interface IInventoryService
    {
        public int DeleteItemFromInventory(Item item);
        public int AddItemToInventory(Item item);
        public int UpdateItemToInventory(Item item);
        public IList<Item> GetItemDetails();
    }
}
