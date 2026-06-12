using Core.Entities;
using Infrastructure.Repositories.CartItemRepositories;
using Infrastructure.Repositories.CartRepositories;
using Services.DTOs;
using Services.Mapping;

namespace Services.Implementations;

public class CartService
{
    private readonly ICartRepository _cartRepo;
    private readonly ICartItemRepository _itemRepo;

    public CartService(ICartRepository cartRepo, ICartItemRepository itemRepo)
    {
        _cartRepo = cartRepo;
        _itemRepo = itemRepo;
    }

    public async Task<CartDto> GetOrCreateCart(long userId)
    {
        var cart = await _cartRepo.GetByUserId(userId);
        if (cart is null)
        {
            cart = new Cart { UserId = userId };
            cart = await _cartRepo.AddAsync(cart);
        }
        return CartMapper.ToDto(cart);
    }

    public async Task AddItem(long userId, AddToCartDto dto)
    {
        var cart = await _cartRepo.GetByUserId(userId);
        if (cart is null)
        {
            cart = new Cart { UserId = userId };
            cart = await _cartRepo.AddAsync(cart);
        }

        var item = new CartItem
        {
            CartId = cart.Id,
            StoreId = dto.StoreId,
            ProductId = dto.ProductId,
            ServiceId = dto.ServiceId,
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity,
            BookingTime = dto.BookingTime
        };

        await _itemRepo.AddAsync(item);
    }

    public async Task RemoveItem(long itemId)
    {
        await _itemRepo.RemoveByIdAsync(itemId);
    }

    public async Task ClearCart(long userId)
    {
        var cart = await _cartRepo.GetByUserId(userId);
        if (cart is null) return;

        foreach (var item in cart.Items.ToList())
        {
            await _itemRepo.RemoveByIdAsync(item.Id);
        }
    }
}
