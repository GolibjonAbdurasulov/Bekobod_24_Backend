using Core.Enums;

namespace Services.DTOs.Admin;

public class CreateStoreTypeRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class UpdateStoreTypeRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class CreateStoreRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Address { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Phone { get; set; }
    public Guid OwnerId { get; set; }
    public Guid StoreTypeId { get; set; }
}

public class UpdateStoreRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Address { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
}

public class CreateCategoryRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid StoreTypeId { get; set; }
    public Guid? ImageId { get; set; }
}

public class UpdateCategoryRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid StoreTypeId { get; set; }
    public Guid? ImageId { get; set; }
}

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Unit { get; set; }
    public string? Attributes { get; set; }
    public Guid StoreId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid? ImageId { get; set; }
}

public class UpdateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Unit { get; set; }
    public string? Attributes { get; set; }
    public bool IsAvailable { get; set; }
    public Guid CategoryId { get; set; }
    public Guid? ImageId { get; set; }
}

public class CreateCourierRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? VehicleType { get; set; }
}

public class UpdateCourierRequest
{
    public string FullName { get; set; } = string.Empty;
    public string? VehicleType { get; set; }
    public bool IsAvailable { get; set; }
}

public class UpdateOrderStatusRequest
{
    public OrderStatus Status { get; set; }
}

public class UpdateUserRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Role { get; set; }
    public bool IsActive { get; set; }
}
