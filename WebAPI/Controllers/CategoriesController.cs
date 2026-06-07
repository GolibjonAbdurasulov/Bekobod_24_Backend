using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetByStoreType([FromQuery] Guid? storeTypeId)
    {
        var result = await _categoryService.GetByStoreTypeAsync(storeTypeId);
        return Ok(result);
    }
}
