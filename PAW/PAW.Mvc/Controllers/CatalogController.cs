using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PAW.Models;
using APW.Architecture;
using PAW.Services;

namespace PAW.Mvc.Controllers
{
    public class CatalogController(ICatalogService catalogService) : Controller
    {

        // GET: Catalog
        public async Task<IActionResult> Index()
        {
            try
            {
                //throw new Exception("");
                var catalogs = await catalogService.GetCatalogsAsync();
                return View(catalogs);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $@"An unexpected error has occured. Double check with your IT Admnin. Detail: {ex.Message}";
                return View(Enumerable.Empty<Catalog>());
            }
        }

        // POST: Catalog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Catalog catalog)
        {
            try
            {
                var result = await catalogService.SaveCatalogsAsync([catalog]);
                if (result)
                    TempData["ErrorMessage"] = $@"Item has been saved successfully";
            }
            catch
            {
                throw;
            }
            return await Index();
        }

        // POST: Catalog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var found = await catalogService.GetCatalogAsync((int)id);
            if (found != null)
            {
                var result = await catalogService.DeleteCatalogAsync((int)id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
