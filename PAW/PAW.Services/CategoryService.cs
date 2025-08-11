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
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<bool> SaveAsync(Category category);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<CategoryViewModel>> FilterAsync(string filter);
    }

    public class CategoryService : ICategoryService
    {
        private readonly IRestProvider _rest;
        private readonly string _base;

        public CategoryService(IRestProvider rest, IConfiguration cfg)
        {
            _rest = rest;

            var baseUrl = cfg["Api:BaseUrl"];
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ApplicationException("Falta Api:BaseUrl en appsettings.json (PAW.Mvc).");

            _base = $"{baseUrl.TrimEnd('/')}/category/";
        }


        public async Task<IEnumerable<Category>> GetAsync()
        {
            var json = await _rest.GetAsync(_base, null);
            return await JsonProvider.DeserializeAsync<IEnumerable<Category>>(json) ?? [];
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            var json = await _rest.GetAsync(_base + id, null);
            return await JsonProvider.DeserializeAsync<Category>(json);
        }

        public async Task<bool> SaveAsync(Category category)
        {
            var body = JsonProvider.Serialize(category);
            await _rest.PostAsync(_base, body);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _rest.DeleteAsync(_base + id, null);
            return true;
        }

        public async Task<IEnumerable<CategoryViewModel>> FilterAsync(string filter)
        {
            var json = await _rest.GetAsync($"{_base}filter?filter={filter}", null);
            return await JsonProvider.DeserializeAsync<IEnumerable<CategoryViewModel>>(json) ?? [];
        }
    }
}
