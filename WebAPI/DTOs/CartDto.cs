namespace WebAPI.DTOs;

public class CartDto
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public List<CartItemDto> Items { get; set; } = new();

    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}