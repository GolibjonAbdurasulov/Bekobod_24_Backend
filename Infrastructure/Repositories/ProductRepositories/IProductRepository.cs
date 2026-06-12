using Core.Entities;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories.ProductRepositories;

public interface IProductRepository : IRepositoryBase<Product,long>
{
}
