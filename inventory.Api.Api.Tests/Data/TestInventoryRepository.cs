using inventory.Api.Api.Domain.Interfaces;
using inventory.Api.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace inventory.Api.Api.Tests.Data
{
    public class TestInventoryRepository : IInventoryRepository
    {
        public async Task<int> AddItemToInventory(Item item)
        {
            return 0;
        }

        public Task<int> DeleteItemFromInventory(Item item)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Item>> GetItemDetails()
        {
            return new List<Item>(){
                new Item()
                {
                         Name = "ShampooWhite",
                Description = "Pure Natural 100% organic",
                InStock = true,
                Price = 5.00M
                }
            };
        }

        public Task<int> UpdateItemToInventory(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
