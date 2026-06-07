namespace Services.DTOs.Courier;

public class CourierResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public string? VehicleType { get; set; }
    public bool IsAvailable { get; set; }
}
