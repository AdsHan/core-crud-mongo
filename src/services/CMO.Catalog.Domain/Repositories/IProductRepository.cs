using CMO.Product.Domain.DomainObjects;
using CMO.Product.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMO.Product.Domain.Repositories
{
    public interface IProductRepository : IRepository<ProductModel>
    {
        Task<List<ProductModel>> GetAllAsync();
        Task<ProductModel> GetByIdAsync(Guid id);
        Task<ProductModel> AddAsync(ProductModel product);
        Task<ReplaceOneResult> UpdateAsync(ProductModel product);
        Task<DeleteResult> DeleteAsync(Guid id);
    }
}