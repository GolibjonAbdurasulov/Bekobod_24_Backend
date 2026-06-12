using Core.Attributes;
using Core.Entities;
using Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ProductRepositories;

[Injectable]
public class ProductRepository : RepositoryBase<Product,long>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
