using Microsoft.AspNetCore.Mvc;
using Services.Implementations;
using WebAPI.DTOs;
using WebAPI.Mapping;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/services")]
public class ServiceController : ControllerBase
{
    private readonly ServiceService _service;

    public ServiceController(ServiceService service)
    {
        _service = service;
    }

    [HttpGet("store/{storeId}")]
    public async Task<List<ServiceDto>> GetByStore(long storeId)
    {
        var services = await _service.GetByStore(storeId);
        return services.Select(ServiceMapper.ToDto).ToList();
    }
}