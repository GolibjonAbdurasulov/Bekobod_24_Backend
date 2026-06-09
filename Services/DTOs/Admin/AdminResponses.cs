namespace Services.DTOs.Admin;

public class UserAdminResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string Role { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class StoreTypeDetailResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class StoreAdminResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Address { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public string StoreTypeName { get; set; } = string.Empty;
}

public class CategoryDetailResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid StoreTypeId { get; set; }
    public string StoreTypeName { get; set; } = string.Empty;
    public Guid? ImageId { get; set; }
    public string? ImageUrl { get; set; }
}

public class ProductAdminResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Unit { get; set; }
    public string? Attributes { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public Guid? ImageId { get; set; }
    public string? ImageUrl { get; set; }
}

public class CourierAdminResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? VehicleType { get; set; }
    public decimal Rating { get; set; }
    public int TotalDeliveries { get; set; }
    public bool IsAvailable { get; set; }
    public Guid UserId { get; set; }
}

public class ReviewAdminResponse
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? StoreName { get; set; }
    public string? CourierName { get; set; }
}

public class OrderAdminResponse
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string ClientPhone { get; set; } = string.Empty;
    public string? CourierName { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public List<Services.DTOs.Order.OrderItemResponse> Items { get; set; } = new();
}
