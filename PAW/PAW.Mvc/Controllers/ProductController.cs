using Microsoft.AspNetCore.Mvc;
using PAW.Models;
using PAW.Models.ViewModels;
using PAW.Services;

namespace PAW.Mvc.Controllers
{
    public class ProductController(IProductService service) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var entities = await service.GetAsync();
            var vm = entities.Select(x => new ProductViewModel
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                InventoryId = x.InventoryId,
                SupplierId = x.SupplierId,
                Description = x.Description,
                Rating = x.Rating,
                CategoryId = x.CategoryId,
                LastModified = x.LastModified,
                ModifiedBy = x.ModifiedBy
            });
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Filter(string filter)
        {
            var data = await service.FilterAsync(filter);
            return Json(data);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0) return NotFound();
            var model = await service.GetByIdAsync(id);
            if (model is null) return NotFound();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product model)
        {
            if (!ModelState.IsValid) return View(model);
            await service.SaveAsync(model);
            TempData["Ok"] = "Saved";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return NotFound();
            await service.DeleteAsync(id);
            TempData["Ok"] = "Deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}
