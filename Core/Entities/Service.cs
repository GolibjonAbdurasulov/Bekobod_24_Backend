namespace Core.Entities;

public class Service
{
    public long Id { get; set; }

    public long StoreId { get; set; }
    public Store Store { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public bool RequiresBooking { get; set; } = true;
}