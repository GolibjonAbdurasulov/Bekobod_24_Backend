namespace Services.DTOs;

public class ServiceDto
{
    public long Id { get; set; }
    public long StoreId { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public bool RequiresBooking { get; set; }
}

public class CreateServiceDto
{
    public long StoreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public bool RequiresBooking { get; set; } = true;
}

public class UpdateServiceDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public bool RequiresBooking { get; set; }
}
