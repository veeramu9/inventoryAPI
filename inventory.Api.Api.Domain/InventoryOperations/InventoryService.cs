using inventory.Api.Api.Domain.Interfaces;
using inventory.Api.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace inventory.Api.Api.Domain
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        public int AddItemToInventory(Item item)
        {
            if (item.ItemId != 0)
                return 5;
            int result = ValidatItemRequest(item);
            if (result == 0)
                return _inventoryRepository.AddItemToInventory(item).Result;
            return result;
        }

        public int DeleteItemFromInventory(Item item)
        {
            if (item.ItemId == 0)
                return 2;
            return _inventoryRepository.DeleteItemFromInventory(item).Result;
        }

        public int UpdateItemToInventory(Item item)
        {
            if (item.ItemId == 0)
                return 2;
            int result = ValidatItemRequest(item);
            if (result == 0)
                return _inventoryRepository.UpdateItemToInventory(item).Result;
            return result;
        }
        public IList<Item> GetItemDetails()
        {
            return _inventoryRepository.GetItemDetails().Result;
        }
        public int ValidatItemRequest(Item item)
        {

            if (item.Name == null || String.IsNullOrWhiteSpace(item.Name.Trim())
                || item.Description == null || String.IsNullOrWhiteSpace(item.Description.Trim()))
                return 3;
            return item.Price > 0.00m ? 0 : 4;
        }
    }
}
