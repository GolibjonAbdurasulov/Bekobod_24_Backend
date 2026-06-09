using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.Admin;
using Services.Interfaces;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _admin;

    public AdminController(IAdminService admin)
    {
        _admin = admin;
    }

    // ── Users ───────────────────────────────────────────────────
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers() => Ok(await _admin.GetUsersAsync());

    [HttpGet("users/{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _admin.GetUserByIdAsync(id);
        if (user == null) return NotFound(new { error = "Foydalanuvchi topilmadi" });
        return Ok(user);
    }

    [HttpPut("users/{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, UpdateUserRequest request) =>
        Ok(await _admin.UpdateUserAsync(id, request));

    // ── StoreTypes ──────────────────────────────────────────────
    [HttpGet("store-types")]
    public async Task<IActionResult> GetStoreTypes() => Ok(await _admin.GetAllStoreTypesAsync());

    [HttpGet("store-types/{id}")]
    public async Task<IActionResult> GetStoreType(Guid id)
    {
        var st = await _admin.GetStoreTypeByIdAsync(id);
        if (st == null) return NotFound(new { error = "Do'kon turi topilmadi" });
        return Ok(st);
    }

    [HttpPost("store-types")]
    public async Task<IActionResult> CreateStoreType(CreateStoreTypeRequest request) =>
        Ok(await _admin.CreateStoreTypeAsync(request));

    [HttpPut("store-types/{id}")]
    public async Task<IActionResult> UpdateStoreType(Guid id, UpdateStoreTypeRequest request) =>
        Ok(await _admin.UpdateStoreTypeAsync(id, request));

    [HttpDelete("store-types/{id}")]
    public async Task<IActionResult> DeleteStoreType(Guid id)
    {
        await _admin.DeleteStoreTypeAsync(id);
        return NoContent();
    }

    // ── Stores ──────────────────────────────────────────────────
    [HttpGet("stores")]
    public async Task<IActionResult> GetStores() => Ok(await _admin.GetAllStoresAsync());

    [HttpGet("stores/{id}")]
    public async Task<IActionResult> GetStore(Guid id)
    {
        var store = await _admin.GetStoreByIdAsync(id);
        if (store == null) return NotFound(new { error = "Do'kon topilmadi" });
        return Ok(store);
    }

    [HttpPost("stores")]
    public async Task<IActionResult> CreateStore(CreateStoreRequest request) =>
        Ok(await _admin.CreateStoreAsync(request));

    [HttpPut("stores/{id}")]
    public async Task<IActionResult> UpdateStore(Guid id, UpdateStoreRequest request) =>
        Ok(await _admin.UpdateStoreAsync(id, request));

    [HttpDelete("stores/{id}")]
    public async Task<IActionResult> DeleteStore(Guid id)
    {
        await _admin.DeleteStoreAsync(id);
        return NoContent();
    }

    // ── Categories ──────────────────────────────────────────────
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories() => Ok(await _admin.GetAllCategoriesAsync());

    [HttpGet("categories/{id}")]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        var cat = await _admin.GetCategoryByIdAsync(id);
        if (cat == null) return NotFound(new { error = "Kategoriya topilmadi" });
        return Ok(cat);
    }

    [HttpPost("categories")]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequest request) =>
        Ok(await _admin.CreateCategoryAsync(request));

    [HttpPut("categories/{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryRequest request) =>
        Ok(await _admin.UpdateCategoryAsync(id, request));

    [HttpDelete("categories/{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        await _admin.DeleteCategoryAsync(id);
        return NoContent();
    }

    // ── Products ────────────────────────────────────────────────
    [HttpGet("products")]
    public async Task<IActionResult> GetProducts() => Ok(await _admin.GetAllProductsAsync());

    [HttpGet("products/{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var prod = await _admin.GetProductByIdAsync(id);
        if (prod == null) return NotFound(new { error = "Mahsulot topilmadi" });
        return Ok(prod);
    }

    [HttpPost("products")]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request) =>
        Ok(await _admin.CreateProductAsync(request));

    [HttpPut("products/{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductRequest request) =>
        Ok(await _admin.UpdateProductAsync(id, request));

    [HttpDelete("products/{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _admin.DeleteProductAsync(id);
        return NoContent();
    }

    // ── Couriers ────────────────────────────────────────────────
    [HttpGet("couriers")]
    public async Task<IActionResult> GetCouriers() => Ok(await _admin.GetAllCouriersAsync());

    [HttpGet("couriers/{id}")]
    public async Task<IActionResult> GetCourier(Guid id)
    {
        var courier = await _admin.GetCourierByIdAsync(id);
        if (courier == null) return NotFound(new { error = "Kuryer topilmadi" });
        return Ok(courier);
    }

    [HttpPost("couriers")]
    public async Task<IActionResult> CreateCourier(CreateCourierRequest request) =>
        Ok(await _admin.CreateCourierAsync(request));

    [HttpPut("couriers/{id}")]
    public async Task<IActionResult> UpdateCourier(Guid id, UpdateCourierRequest request) =>
        Ok(await _admin.UpdateCourierAsync(id, request));

    [HttpDelete("couriers/{id}")]
    public async Task<IActionResult> DeleteCourier(Guid id)
    {
        await _admin.DeleteCourierAsync(id);
        return NoContent();
    }

    // ── Orders ──────────────────────────────────────────────────
    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders() => Ok(await _admin.GetAllOrdersAsync());

    [HttpGet("orders/{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var order = await _admin.GetOrderByIdAsync(id);
        if (order == null) return NotFound(new { error = "Buyurtma topilmadi" });
        return Ok(order);
    }

    [HttpPatch("orders/{id}/status")]
    public async Task<IActionResult> UpdateOrderStatus(Guid id, UpdateOrderStatusRequest request) =>
        Ok(await _admin.UpdateOrderStatusAsync(id, request));

    // ── Reviews ─────────────────────────────────────────────────
    [HttpGet("reviews")]
    public async Task<IActionResult> GetReviews() => Ok(await _admin.GetAllReviewsAsync());

    [HttpDelete("reviews/{id}")]
    public async Task<IActionResult> DeleteReview(Guid id)
    {
        await _admin.DeleteReviewAsync(id);
        return NoContent();
    }
}
