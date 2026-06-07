namespace Core.Entities;

public class Review
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid? StoreId { get; set; }
    public Store? Store { get; set; }

    public Guid? CourierId { get; set; }
    public Courier? Courier { get; set; }

    public Guid? OrderId { get; set; }
    public Order? Order { get; set; }
}
