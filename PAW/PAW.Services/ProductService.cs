using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APW.Architecture;
using Microsoft.Extensions.Configuration;
using PAW.Architecture.Providers;
using PAW.Models.ViewModels;
using PAW.Models;

namespace PAW.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<bool> SaveAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ProductViewModel>> FilterAsync(string filter);
    }

    public class ProductService : IProductService
    {
        private readonly IRestProvider _rest;
        private readonly string _base;

        public ProductService(IRestProvider rest, IConfiguration cfg)
        {
            _rest = rest;

            var baseUrl = cfg["Api:BaseUrl"];   
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ApplicationException("Falta Api:BaseUrl en appsettings.json (PAW.Mvc).");

            _base = $"{baseUrl.TrimEnd('/')}/product/";
        }


        public async Task<IEnumerable<Product>> GetAsync()
        {
            var json = await _rest.GetAsync(_base, null);
            return await JsonProvider.DeserializeAsync<IEnumerable<Product>>(json) ?? [];
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var json = await _rest.GetAsync(_base + id, null);
            return await JsonProvider.DeserializeAsync<Product>(json);
        }

        public async Task<bool> SaveAsync(Product product)
        {
            var body = JsonProvider.Serialize(product);
            await _rest.PostAsync(_base, body);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _rest.DeleteAsync(_base + id, null);
            return true;
        }

        public async Task<IEnumerable<ProductViewModel>> FilterAsync(string filter)
        {
            var json = await _rest.GetAsync($"{_base}filter?filter={filter}", null);
            return await JsonProvider.DeserializeAsync<IEnumerable<ProductViewModel>>(json) ?? [];
        }
    }
}
