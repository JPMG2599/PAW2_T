using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAW.Business;
using PAW.Models.ViewModels;
using PAW.Models;

namespace PAW.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(IBusinessCategory business) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Category>> Get() => await business.GetAllAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Category>> GetById(int id)
    {
        var item = await business.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] Category category)
    {
        if (category is null) return BadRequest();
        var ok = await business.SaveAsync(category);
        return ok ? Ok(true) : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await business.DeleteAsync(id);
        return ok ? NotFound() : Ok(true);
    }

    [HttpGet("filter")]
    public async Task<IEnumerable<CategoryViewModel>> Filter([FromQuery] string filter)
        => await business.FilterAsync(filter);
}
