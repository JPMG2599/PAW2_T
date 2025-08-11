using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAW.Business;
using PAW.Models.ViewModels;
using PAW.Models;

namespace PAW.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IBusinessProduct business) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Product>> Get() => await business.GetAllAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var item = await business.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] Product product)
    {
        if (product is null) return BadRequest();
        var ok = await business.SaveAsync(product);
        return ok ? Ok(true) : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await business.DeleteAsync(id);
        return ok ? NotFound() : Ok(true);
    }

    [HttpGet("filter")]
    public async Task<IEnumerable<ProductViewModel>> Filter([FromQuery] string filter)
        => await business.FilterAsync(filter);
}
