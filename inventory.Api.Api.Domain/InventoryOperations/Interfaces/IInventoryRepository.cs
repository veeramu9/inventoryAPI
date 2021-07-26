using inventory.Api.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace inventory.Api.Api.Domain.Interfaces
{
    public interface IInventoryRepository
    {
        public Task<int> AddItemToInventory(Item item);
        public Task<int> UpdateItemToInventory(Item item);
        public Task<IList<Item>> GetItemDetails();
        public Task<int> DeleteItemFromInventory(Item item);

    }
}
