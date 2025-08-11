using APW.Architecture;
using PAW.Architecture.Providers;
using PAW.Models;
using System.Text.Json;

namespace PAW.Services
{
    public interface ICatalogService
    {
        Task<Catalog> GetCatalogAsync(int id);
        Task<IEnumerable<Catalog>> GetCatalogsAsync();
        Task<bool> DeleteCatalogAsync(int id);
        Task<bool> SaveCatalogsAsync(IEnumerable<Catalog> catalogs);
    }

    public class CatalogService(IRestProvider restProvider) : ICatalogService
    {
        public async Task<Catalog> GetCatalogAsync(int id)
        {
            var result = await restProvider.GetAsync("https://localhost:7252/Catalog/", "1");
            var catalog = await JsonProvider.DeserializeAsync<Catalog>(result);
            return catalog;
        }

        public async Task<IEnumerable<Catalog>> GetCatalogsAsync()
        {
            var result = await restProvider.GetAsync("https://localhost:7252/Catalog/", null);
            var catalogs = await JsonProvider.DeserializeAsync<IEnumerable<Catalog>>(result);
            return catalogs;
        }

        public async Task<bool> DeleteCatalogAsync(int id)
        {
            var result = await restProvider.DeleteAsync("https://localhost:7252/Catalog/", $"{id}");

            //var isSaved = JsonProvider.DeserializeSimple<bool>(result);
            return true;
        }

        public async Task<bool> SaveCatalogsAsync(IEnumerable<Catalog> catalogs)
        {
            var content = JsonProvider.Serialize(catalogs);
            var result = await restProvider.PostAsync("https://localhost:7252/Catalog/", content);
            return true;
        }
    }
}
