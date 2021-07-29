using CMO.Product.Domain.Entities;
using CMO.Product.Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMO.Product.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<ProductModel> _collection;

        public ProductRepository(IMongoDatabase mongoDatabase)
        {
            _collection = mongoDatabase.GetCollection<ProductModel>("products");
        }

        public async Task<List<ProductModel>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync(); ;
        }

        public async Task<ProductModel> GetByIdAsync(Guid id)
        {
            return await _collection.Find(c => c.Id == id).SingleOrDefaultAsync();
        }

        public async Task<ReplaceOneResult> UpdateAsync(ProductModel product)
        {
            return await _collection.ReplaceOneAsync(c => c.Id.Equals(product.Id), product);
        }

        public async Task<ProductModel> AddAsync(ProductModel product)
        {
            await _collection.InsertOneAsync(product);
            return product;
        }

        public async Task<DeleteResult> DeleteAsync(Guid id)
        {
            return await _collection.DeleteOneAsync(c => c.Id == id);
        }
    }
}