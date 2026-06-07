using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/store-types")]
public class StoreTypesController : ControllerBase
{
    private readonly IStoreTypeService _storeTypeService;

    public StoreTypesController(IStoreTypeService storeTypeService)
    {
        _storeTypeService = storeTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _storeTypeService.GetAllAsync();
        return Ok(result);
    }
}
