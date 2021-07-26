using inventory.Api.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using inventory.Api.Api.Domain.Interfaces;
using Serilog;

namespace inventory.Api.Api.Data
{
    public class InMemoryInventoryRepository : IInventoryRepository
    {
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _appDbContext;
        public InMemoryInventoryRepository(IConfiguration configuration, AppDBContext appDbContext)
        {
            _configuration = configuration;
            _appDbContext = appDbContext;
        }

        public async Task<int> AddItemToInventory(Item item)
        {
            try
            {
                var list = await _appDbContext.Items.AsNoTracking().SingleOrDefaultAsync(s => s.Name.ToLower().Trim().Equals(item.Name.ToLower().Trim()));
                if (list == null)
                {
                    _appDbContext.Items.Add(item);
                    await _appDbContext.SaveChangesAsync();
                    return 0;
                }
            }
            catch(Exception ex)
            {
                Log.Logger.Information(ex.Message);
                throw ex;
            }
            return 1;
        }

        public async Task<int> UpdateItemToInventory(Item item)
        {
            try
            {
                var list = await _appDbContext.Items.AsNoTracking().SingleOrDefaultAsync(s => s.ItemId == item.ItemId);
                if (list != null)
                {
                    _appDbContext.Items.Update(item);
                    await _appDbContext.SaveChangesAsync();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Information(ex.Message);
                throw ex;
            }
            return 1;
        }

        public async Task<int> DeleteItemFromInventory(Item item)
        {
            try
            {
                var list = await _appDbContext.Items.AsNoTracking().SingleOrDefaultAsync(s => s.ItemId == item.ItemId);
                if (list != null)
                {
                    _appDbContext.Items.Remove(item);
                    await _appDbContext.SaveChangesAsync();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Information(ex.Message);
                thr ow ex;
            }
            return 1;
        }
        public async Task<IList<Item>> GetItemDetails()
        {
            try
            {
                return await _appDbContext.Items.ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Logger.Information(ex.Message);
                throw ex;
            }
        }
    }
}
