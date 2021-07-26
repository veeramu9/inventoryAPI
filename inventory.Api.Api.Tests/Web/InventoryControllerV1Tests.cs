using inventory.Api.Api.Domain.Interfaces;
using inventory.Api.Api.Domain.Models;
using inventory.Api.Api.Tests.Data;
using inventory.Api.Api.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace inventory.Api.Api.Tests.Web
{
    public class InventoryControllerV1Tests : IDisposable
    {
        protected TestServer Server;
        protected HttpClient Client;
        private const string RequestUri = "/v1/api/Inventory";
        public InventoryControllerV1Tests()
        {
            var builder = new WebHostBuilder().ConfigureAppConfiguration((config) =>
                {
                    config.AddJsonFile("appsetting.json", optional: true, reloadOnChange: false);
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IInventoryRepository, TestInventoryRepository>();
                }).UseStartup<Startup>();
            Server = new TestServer(builder);
            Client = Server.CreateClient();          
        }

        public void Dispose()
        {
            Server?.Dispose();
            Client?.Dispose();
        }

        [Fact]
        public async Task GetItems()
        {
            var response = await Client.GetAsync(RequestUri);
            Assert.Equal( HttpStatusCode.OK, response.StatusCode);
            Dispose();
        }


        [Fact]
        public async Task AddItem()
        {
            var ItemVal = new Item
            {
                Name = "ShampooWhite1",
                Description = "Pure Natural 100% organic",
                InStock = true,
                Price = 5.00M
            };

            var newValString = JsonConvert.SerializeObject(ItemVal);
            var postedContent = new StringContent(newValString, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(RequestUri, postedContent);
            var value = response.Content.ReadAsStringAsync().Result;
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Dispose();
        }

        [Fact]
        public async Task Update()
        {
            var ItemVal = new Item
            {
                Name = "ShampooWhite",
                Description = "Pure Natural 100% organic",
                InStock = true,
                Price = 5.00M
            };

            var newValString = JsonConvert.SerializeObject(ItemVal);
            var postedContent = new StringContent(newValString, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(RequestUri, postedContent);
            var value = response.Content.ReadAsStringAsync().Result;
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Dispose();
        }

        [Fact]
        public async Task Delete()
        {
            var ItemVal = new Item
            {
                Name = "ShampooWhite",
                Description = "Pure Natural 100% organic",
                InStock = true,
                Price = 5.00M
            };

            var newValString = JsonConvert.SerializeObject(ItemVal);
            var postedContent = new StringContent(newValString, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(RequestUri + "/delete", postedContent);
            var value = response.Content.ReadAsStringAsync().Result;
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Dispose();
        }

    }
}
