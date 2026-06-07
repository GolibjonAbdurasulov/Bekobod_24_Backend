namespace Core.Entities;

public class Courier
{
    public Guid Id { get; set; }
    public bool IsAvailable { get; set; } = true;
    public decimal Rating { get; set; }
    public int TotalDeliveries { get; set; }
    public string? VehicleType { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
