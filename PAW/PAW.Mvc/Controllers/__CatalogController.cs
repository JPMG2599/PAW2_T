using APW.Architecture;
using Microsoft.AspNetCore.Mvc;
using PAW.Mvc.Helper.Converters;
using PAW.Architecture.Providers;
using PAW.Models;
using PAW.Services;

namespace PAW.Mvc.Controllers;

public class __CatalogController(ICatalogService catalogService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var catalog = await catalogService.GetCatalogAsync(1);        
        return View("~/Views/Catalog/Index.cshtml", Converter.ToCatalogViewModel(catalog));
    }

    public async Task<IActionResult> All()
    {
        var catalogs = await catalogService.GetCatalogsAsync();
        return View("~/Views/Catalog/All.cshtml", catalogs.Select(Converter.ToCatalogViewModel));
    }
}
