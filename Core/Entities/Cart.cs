namespace Core.Entities;

public class Cart
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public List<CartItem> Items { get; set; } = new();
}
