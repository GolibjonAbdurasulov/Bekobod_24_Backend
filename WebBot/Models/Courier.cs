namespace WebBot.Models;

public class CourierProfile
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = "";
    public string Phone { get; set; } = "";
    public decimal Rating { get; set; }
    public string? VehicleType { get; set; }
    public bool IsAvailable { get; set; }
    public int TotalDeliveries { get; set; }
}
