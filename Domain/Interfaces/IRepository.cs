using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entidades;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : EntidadeBase
    {
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item, Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteAdminAsync(Guid id);
        Task<T> SelectAsync(Guid id);
        Task<IEnumerable<T>> GetInatives();
        Task<IEnumerable<T>> SelectAsync();
        IQueryable<T> QuerySelect();
        Task<bool> ExistsAsync(Guid id);
    }
}
