using Microsoft.AspNetCore.Mvc;
using Services.Implementations;
using WebAPI.DTOs;
using WebAPI.Mapping;

namespace WebAPI.Controllers;
[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly ProductService _service;

    public ProductController(ProductService service)
    {
        _service = service;
    }

    [HttpGet("store/{storeId}")]
    public async Task<List<ProductDto>> GetByStore(long storeId)
    {
        var products = await _service.GetByStore(storeId);
        return products.Select(ProductMapper.ToDto).ToList();
    }
}