using inventory.Api.Api.Domain.Interfaces;
using inventory.Api.Api.Domain.Models;
using inventory.Api.Api.Web.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory.Api.Api.Web.Controllers.V1.InventoryOperations
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        readonly IConfiguration _configuration;
        readonly IInventoryService _inventoryService;
        public InventoryController(IConfiguration configuration, IInventoryService inventoryService)
        {
            _configuration = configuration;
            _inventoryService = inventoryService;
        }
        [HttpGet]
        //[Ac]
        public IActionResult GetItemDetails()
        {
            try
            {
                var ListOfItem = _inventoryService.GetItemDetails();
                if (ListOfItem.Any())
                    return Ok(ListOfItem);
            }
            catch (Exception ex)
            {
                Log.Logger.Information(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "");
            }

            return NotFound("Item List is empty, Please contact Support");
        }

        [HttpPost]
        [ActionFilter]
        public IActionResult AddItemToInventory(Item item)
        {
            try
            {
                int result = _inventoryService.AddItemToInventory(item);
                if (result == 5)
                    return BadRequest("ItemId is not allowed in Create request");
                if (result == 1)
                    return BadRequest("Item already exists in inventory");
                if (result == 0)
                    return Ok("Successfully added to Inventory");
                return BadRequest(Constants.GetMessage(result));
            }

            catch (Exception ex)
            {
                Log.Logger.Information(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,"");
            }

        }

        [HttpPut]
        [ActionFilter]
        public IActionResult UpdateItemToInventory(Item item)
        {
            try
            {
                int result = _inventoryService.UpdateItemToInventory(item);
                if (result == 0)
                    return Ok("Successfully updated the item to Inventory");
                if (result == 1)
                    return NotFound("Item not in the Inventory");
                return BadRequest(Constants.GetMessage(result));
            }
            catch (Exception ex)
            {
                Log.Logger.Information(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "");
            }
        }

        [HttpPut]
        [ActionFilter]
        [Route("delete")]
        public IActionResult DeleteItemToInventory(Item item)
        {
            try
            {
                int result = _inventoryService.DeleteItemFromInventory(item);
                if (result == 0)
                    return Ok("Successfully delete from Inventory");
                if (result == 1)
                    return NotFound("Item not in the Inventory");
                return BadRequest(Constants.GetMessage(result));
            }
            catch (Exception ex)
            {
                Log.Logger.Information(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "");
            }
        }
    }
}
