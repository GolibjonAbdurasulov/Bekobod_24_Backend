using System.Security.Cryptography;
using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.DTOs.Admin;
using Services.DTOs.Order;
using Services.Interfaces;

namespace Services.Implementations;

public class AdminService : IAdminService
{
    private readonly AppDbContext _db;

    public AdminService(AppDbContext db)
    {
        _db = db;
    }

    // ── Users ──────────────────────────────────────────────────
    public async Task<List<UserAdminResponse>> GetUsersAsync()
    {
        return await _db.Users
            .OrderBy(u => u.CreatedAt)
            .Select(u => new UserAdminResponse
            {
                Id = u.Id,
                FullName = u.FullName,
                Phone = u.Phone,
                Email = u.Email,
                Role = u.Role.ToString(),
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<UserAdminResponse?> GetUserByIdAsync(Guid id)
    {
        var u = await _db.Users.FindAsync(id);
        if (u == null) return null;
        return new UserAdminResponse
        {
            Id = u.Id,
            FullName = u.FullName,
            Phone = u.Phone,
            Email = u.Email,
            Role = u.Role.ToString(),
            IsActive = u.IsActive,
            CreatedAt = u.CreatedAt
        };
    }

    public async Task<UserAdminResponse> UpdateUserAsync(Guid id, UpdateUserRequest request)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null) throw new NotFoundException("Foydalanuvchi topilmadi");

        user.FullName = request.FullName;
        user.Phone = request.Phone;
        user.Email = request.Email;
        user.IsActive = request.IsActive;

        if (!string.IsNullOrEmpty(request.Role))
            user.Role = Enum.Parse<UserRole>(request.Role);

        await _db.SaveChangesAsync();

        return new UserAdminResponse
        {
            Id = user.Id,
            FullName = user.FullName,
            Phone = user.Phone,
            Email = user.Email,
            Role = user.Role.ToString(),
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }

    // ── StoreTypes ──────────────────────────────────────────────
    public async Task<StoreTypeDetailResponse> CreateStoreTypeAsync(CreateStoreTypeRequest request)
    {
        var entity = new StoreType
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description
        };
        _db.StoreTypes.Add(entity);
        await _db.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<List<StoreTypeDetailResponse>> GetAllStoreTypesAsync()
    {
        return await _db.StoreTypes
            .OrderBy(st => st.Name)
            .Select(st => new StoreTypeDetailResponse
            {
                Id = st.Id,
                Name = st.Name,
                Description = st.Description
            })
            .ToListAsync();
    }

    public async Task<StoreTypeDetailResponse?> GetStoreTypeByIdAsync(Guid id)
    {
        var st = await _db.StoreTypes.FindAsync(id);
        return st == null ? null : Map(st);
    }

    public async Task<StoreTypeDetailResponse> UpdateStoreTypeAsync(Guid id, UpdateStoreTypeRequest request)
    {
        var st = await _db.StoreTypes.FindAsync(id);
        if (st == null) throw new NotFoundException("Do'kon turi topilmadi");

        st.Name = request.Name;
        st.Description = request.Description;
        await _db.SaveChangesAsync();
        return Map(st);
    }

    public async Task DeleteStoreTypeAsync(Guid id)
    {
        var st = await _db.StoreTypes.FindAsync(id);
        if (st == null) throw new NotFoundException("Do'kon turi topilmadi");
        _db.StoreTypes.Remove(st);
        await _db.SaveChangesAsync();
    }

    // ── Stores ──────────────────────────────────────────────────
    public async Task<StoreAdminResponse> CreateStoreAsync(CreateStoreRequest request)
    {
        var entity = new Store
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Address = request.Address,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Phone = request.Phone,
            OwnerId = request.OwnerId,
            StoreTypeId = request.StoreTypeId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        _db.Stores.Add(entity);
        await _db.SaveChangesAsync();
        return await MapStore(entity.Id);
    }

    public async Task<List<StoreAdminResponse>> GetAllStoresAsync()
    {
        return await _db.Stores
            .OrderBy(s => s.Name)
            .Select(s => new StoreAdminResponse
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Address = s.Address,
                Latitude = s.Latitude ?? 0,
                Longitude = s.Longitude ?? 0,
                Phone = s.Phone,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt,
                OwnerName = s.Owner.FullName,
                StoreTypeName = s.StoreType.Name
            })
            .ToListAsync();
    }

    public async Task<StoreAdminResponse?> GetStoreByIdAsync(Guid id)
    {
        return await _db.Stores
            .Where(s => s.Id == id)
            .Select(s => new StoreAdminResponse
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Address = s.Address,
                Latitude = s.Latitude ?? 0,
                Longitude = s.Longitude ?? 0,
                Phone = s.Phone,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt,
                OwnerName = s.Owner.FullName,
                StoreTypeName = s.StoreType.Name
            })
            .FirstOrDefaultAsync();
    }

    public async Task<StoreAdminResponse> UpdateStoreAsync(Guid id, UpdateStoreRequest request)
    {
        var store = await _db.Stores.FindAsync(id);
        if (store == null) throw new NotFoundException("Do'kon topilmadi");

        store.Name = request.Name;
        store.Description = request.Description;
        store.Address = request.Address;
        store.Latitude = request.Latitude;
        store.Longitude = request.Longitude;
        store.Phone = request.Phone;
        store.IsActive = request.IsActive;
        await _db.SaveChangesAsync();

        return (await GetStoreByIdAsync(id))!;
    }

    public async Task DeleteStoreAsync(Guid id)
    {
        var store = await _db.Stores.FindAsync(id);
        if (store == null) throw new NotFoundException("Do'kon topilmadi");
        _db.Stores.Remove(store);
        await _db.SaveChangesAsync();
    }

    // ── Categories ──────────────────────────────────────────────
    public async Task<CategoryDetailResponse> CreateCategoryAsync(CreateCategoryRequest request)
    {
        var entity = new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            StoreTypeId = request.StoreTypeId,
            ImageId = request.ImageId
        };
        _db.Categories.Add(entity);
        await _db.SaveChangesAsync();
        return await MapCategory(entity.Id);
    }

    public async Task<List<CategoryDetailResponse>> GetAllCategoriesAsync()
    {
        var baseUrl = "http://localhost:5000";
        return await _db.Categories
            .OrderBy(c => c.Name)
            .Select(c => new CategoryDetailResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                StoreTypeId = c.StoreTypeId ?? Guid.Empty,
                StoreTypeName = c.StoreType != null ? c.StoreType.Name : "",
                ImageId = c.ImageId,
                ImageUrl = c.Image != null
                    ? baseUrl + "/api/files/download/" + c.ImageId
                    : "https://picsum.photos/seed/" + c.Id + "/400/400"
            })
            .ToListAsync();
    }

    public async Task<CategoryDetailResponse?> GetCategoryByIdAsync(Guid id)
    {
        var baseUrl = "http://localhost:5000";
        return await _db.Categories
            .Where(c => c.Id == id)
            .Select(c => new CategoryDetailResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                StoreTypeId = c.StoreTypeId ?? Guid.Empty,
                StoreTypeName = c.StoreType != null ? c.StoreType.Name : "",
                ImageId = c.ImageId,
                ImageUrl = c.Image != null
                    ? baseUrl + "/api/files/download/" + c.ImageId
                    : "https://picsum.photos/seed/" + c.Id + "/400/400"
            })
            .FirstOrDefaultAsync();
    }

    public async Task<CategoryDetailResponse> UpdateCategoryAsync(Guid id, UpdateCategoryRequest request)
    {
        var cat = await _db.Categories.FindAsync(id);
        if (cat == null) throw new NotFoundException("Kategoriya topilmadi");

        cat.Name = request.Name;
        cat.Description = request.Description;
        cat.StoreTypeId = request.StoreTypeId;
        cat.ImageId = request.ImageId;
        await _db.SaveChangesAsync();

        return (await GetCategoryByIdAsync(id))!;
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        var cat = await _db.Categories.FindAsync(id);
        if (cat == null) throw new NotFoundException("Kategoriya topilmadi");
        _db.Categories.Remove(cat);
        await _db.SaveChangesAsync();
    }

    // ── Products ────────────────────────────────────────────────
    public async Task<ProductAdminResponse> CreateProductAsync(CreateProductRequest request)
    {
        var entity = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Unit = request.Unit,
            Attributes = request.Attributes,
            StoreId = request.StoreId,
            CategoryId = request.CategoryId,
            ImageId = request.ImageId,
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow
        };
        _db.Products.Add(entity);
        await _db.SaveChangesAsync();
        return await MapProduct(entity.Id);
    }

    public async Task<List<ProductAdminResponse>> GetAllProductsAsync()
    {
        var baseUrl = "http://localhost:5000";
        return await _db.Products
            .OrderBy(p => p.Name)
            .Select(p => new ProductAdminResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Unit = p.Unit,
                Attributes = p.Attributes,
                IsAvailable = p.IsAvailable,
                CreatedAt = p.CreatedAt,
                StoreName = p.Store.Name,
                CategoryName = p.Category.Name,
                ImageId = p.ImageId,
                ImageUrl = p.Image != null
                    ? baseUrl + "/api/files/download/" + p.ImageId
                    : "https://picsum.photos/seed/" + p.Id + "/400/400"
            })
            .ToListAsync();
    }

    public async Task<ProductAdminResponse?> GetProductByIdAsync(Guid id)
    {
        var baseUrl = "http://localhost:5000";
        return await _db.Products
            .Where(p => p.Id == id)
            .Select(p => new ProductAdminResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Unit = p.Unit,
                Attributes = p.Attributes,
                IsAvailable = p.IsAvailable,
                CreatedAt = p.CreatedAt,
                StoreName = p.Store.Name,
                CategoryName = p.Category.Name,
                ImageId = p.ImageId,
                ImageUrl = p.Image != null
                    ? baseUrl + "/api/files/download/" + p.ImageId
                    : "https://picsum.photos/seed/" + p.Id + "/400/400"
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ProductAdminResponse> UpdateProductAsync(Guid id, UpdateProductRequest request)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null) throw new NotFoundException("Mahsulot topilmadi");

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Unit = request.Unit;
        product.Attributes = request.Attributes;
        product.IsAvailable = request.IsAvailable;
        product.CategoryId = request.CategoryId;
        product.ImageId = request.ImageId;
        await _db.SaveChangesAsync();

        return (await GetProductByIdAsync(id))!;
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null) throw new NotFoundException("Mahsulot topilmadi");
        _db.Products.Remove(product);
        await _db.SaveChangesAsync();
    }

    // ── Couriers ────────────────────────────────────────────────
    public async Task<CourierAdminResponse> CreateCourierAsync(CreateCourierRequest request)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Phone = request.Phone,
            PasswordHash = HashPassword(request.Password),
            Role = UserRole.Courier,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        _db.Users.Add(user);

        var courier = new Courier
        {
            Id = Guid.NewGuid(),
            IsAvailable = true,
            Rating = 0,
            TotalDeliveries = 0,
            VehicleType = request.VehicleType,
            UserId = user.Id
        };
        _db.Couriers.Add(courier);
        await _db.SaveChangesAsync();

        return new CourierAdminResponse
        {
            Id = courier.Id,
            FullName = user.FullName,
            Phone = user.Phone,
            VehicleType = courier.VehicleType,
            Rating = courier.Rating,
            TotalDeliveries = courier.TotalDeliveries,
            IsAvailable = courier.IsAvailable,
            UserId = user.Id
        };
    }

    public async Task<List<CourierAdminResponse>> GetAllCouriersAsync()
    {
        return await _db.Couriers
            .OrderBy(c => c.User.FullName)
            .Select(c => new CourierAdminResponse
            {
                Id = c.Id,
                FullName = c.User.FullName,
                Phone = c.User.Phone,
                VehicleType = c.VehicleType,
                Rating = c.Rating,
                TotalDeliveries = c.TotalDeliveries,
                IsAvailable = c.IsAvailable,
                UserId = c.UserId
            })
            .ToListAsync();
    }

    public async Task<CourierAdminResponse?> GetCourierByIdAsync(Guid id)
    {
        return await _db.Couriers
            .Where(c => c.Id == id)
            .Select(c => new CourierAdminResponse
            {
                Id = c.Id,
                FullName = c.User.FullName,
                Phone = c.User.Phone,
                VehicleType = c.VehicleType,
                Rating = c.Rating,
                TotalDeliveries = c.TotalDeliveries,
                IsAvailable = c.IsAvailable,
                UserId = c.UserId
            })
            .FirstOrDefaultAsync();
    }

    public async Task<CourierAdminResponse> UpdateCourierAsync(Guid id, UpdateCourierRequest request)
    {
        var courier = await _db.Couriers.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id);
        if (courier == null) throw new NotFoundException("Kuryer topilmadi");

        courier.User.FullName = request.FullName;
        courier.VehicleType = request.VehicleType;
        courier.IsAvailable = request.IsAvailable;
        await _db.SaveChangesAsync();

        return new CourierAdminResponse
        {
            Id = courier.Id,
            FullName = courier.User.FullName,
            Phone = courier.User.Phone,
            VehicleType = courier.VehicleType,
            Rating = courier.Rating,
            TotalDeliveries = courier.TotalDeliveries,
            IsAvailable = courier.IsAvailable,
            UserId = courier.UserId
        };
    }

    public async Task DeleteCourierAsync(Guid id)
    {
        var courier = await _db.Couriers.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id);
        if (courier == null) throw new NotFoundException("Kuryer topilmadi");
        _db.Users.Remove(courier.User);
        _db.Couriers.Remove(courier);
        await _db.SaveChangesAsync();
    }

    // ── Orders ──────────────────────────────────────────────────
    public async Task<List<OrderAdminResponse>> GetAllOrdersAsync()
    {
        return await _db.Orders
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => new OrderAdminResponse
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                Status = o.Status.ToString(),
                TotalAmount = o.TotalAmount,
                DeliveryAddress = o.DeliveryAddress,
                Note = o.Note,
                CreatedAt = o.CreatedAt,
                DeliveredAt = o.DeliveredAt,
                ClientName = o.Client.FullName,
                ClientPhone = o.Client.Phone,
                CourierName = o.Courier != null ? o.Courier.FullName : null,
                StoreName = o.Store.Name,
                Items = o.Items.Select(i => new OrderItemResponse
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    TotalPrice = i.TotalPrice
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<OrderAdminResponse?> GetOrderByIdAsync(Guid id)
    {
        return await _db.Orders
            .Where(o => o.Id == id)
            .Select(o => new OrderAdminResponse
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                Status = o.Status.ToString(),
                TotalAmount = o.TotalAmount,
                DeliveryAddress = o.DeliveryAddress,
                Note = o.Note,
                CreatedAt = o.CreatedAt,
                DeliveredAt = o.DeliveredAt,
                ClientName = o.Client.FullName,
                ClientPhone = o.Client.Phone,
                CourierName = o.Courier != null ? o.Courier.FullName : null,
                StoreName = o.Store.Name,
                Items = o.Items.Select(i => new OrderItemResponse
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    TotalPrice = i.TotalPrice
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<OrderAdminResponse> UpdateOrderStatusAsync(Guid id, UpdateOrderStatusRequest request)
    {
        var order = await _db.Orders.FindAsync(id);
        if (order == null) throw new NotFoundException("Buyurtma topilmadi");

        order.Status = request.Status;

        if (request.Status == OrderStatus.Delivered)
            order.DeliveredAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return (await GetOrderByIdAsync(id))!;
    }

    // ── Reviews ─────────────────────────────────────────────────
    public async Task<List<ReviewAdminResponse>> GetAllReviewsAsync()
    {
        return await _db.Reviews
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new ReviewAdminResponse
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                UserName = r.User.FullName,
                StoreName = r.Store != null ? r.Store.Name : null,
                CourierName = r.Courier != null ? r.Courier.User.FullName : null
            })
            .ToListAsync();
    }

    public async Task DeleteReviewAsync(Guid id)
    {
        var review = await _db.Reviews.FindAsync(id);
        if (review == null) throw new NotFoundException("Izoh topilmadi");
        _db.Reviews.Remove(review);
        await _db.SaveChangesAsync();
    }

    // ── Helpers ─────────────────────────────────────────────────
    private static StoreTypeDetailResponse Map(StoreType st) => new()
    {
        Id = st.Id,
        Name = st.Name,
        Description = st.Description
    };

    private async Task<StoreAdminResponse> MapStore(Guid id)
    {
        return (await _db.Stores
            .Where(s => s.Id == id)
            .Select(s => new StoreAdminResponse
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Address = s.Address,
                Latitude = s.Latitude ?? 0,
                Longitude = s.Longitude ?? 0,
                Phone = s.Phone,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt,
                OwnerName = s.Owner.FullName,
                StoreTypeName = s.StoreType.Name
            })
            .FirstOrDefaultAsync())!;
    }

    private async Task<CategoryDetailResponse> MapCategory(Guid id)
    {
        var baseUrl = "http://localhost:5000";
        return (await _db.Categories
            .Where(c => c.Id == id)
            .Select(c => new CategoryDetailResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                StoreTypeId = c.StoreTypeId ?? Guid.Empty,
                StoreTypeName = c.StoreType != null ? c.StoreType.Name : "",
                ImageId = c.ImageId,
                ImageUrl = c.Image != null
                    ? baseUrl + "/api/files/download/" + c.ImageId
                    : "https://picsum.photos/seed/" + c.Id + "/400/400"
            })
            .FirstOrDefaultAsync())!;
    }

    private async Task<ProductAdminResponse> MapProduct(Guid id)
    {
        var baseUrl = "http://localhost:5000";
        return (await _db.Products
            .Where(p => p.Id == id)
            .Select(p => new ProductAdminResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Unit = p.Unit,
                Attributes = p.Attributes,
                IsAvailable = p.IsAvailable,
                CreatedAt = p.CreatedAt,
                StoreName = p.Store.Name,
                CategoryName = p.Category.Name,
                ImageId = p.ImageId,
                ImageUrl = p.Image != null
                    ? baseUrl + "/api/files/download/" + p.ImageId
                    : "https://picsum.photos/seed/" + p.Id + "/400/400"
            })
            .FirstOrDefaultAsync())!;
    }

    private static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100000, HashAlgorithmName.SHA256, 32);
        return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
    }
}
