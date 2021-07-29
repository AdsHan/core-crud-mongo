using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMO.Product.Domain.DomainObjects
{
    public interface IRepository<T> : IAggregateRoot
    {
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T obj);
        Task<ReplaceOneResult> UpdateAsync(T obj);
        Task<DeleteResult> DeleteAsync(Guid id);
    }
}