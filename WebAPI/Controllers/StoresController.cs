using Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Services.Implementations;
using WebAPI.DTOs;
using WebAPI.Mapping;

namespace WebAPI.Controllers;
[ApiController]
[Route("api/stores")]
public class StoreController : ControllerBase
{
    private readonly StoreService _service;

    public StoreController(StoreService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<List<StoreDto>> GetAll([FromQuery] StoreType? type)
    {
        var stores = await _service.GetAllAsync();

        if (type != null)
        {
            stores = await _service.GetByType(type.Value);
        }

        return stores.Select(StoreMapper.ToDto).ToList();
    }

    [HttpGet("type/{type}")]
    public async Task<List<StoreDto>> GetByType(StoreType type)
    {
        var stores = await _service.GetByType(type);
        return stores.Select(StoreMapper.ToDto).ToList();
    }
}