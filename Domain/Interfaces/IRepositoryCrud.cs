using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entidades;

namespace Domain.Interfaces
{
    public interface IRepositoryCrud<T> : IDisposable where T : EntidadeBase
    {
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item, Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteAdminAsync(Guid id);
        Task<T> SelectAsync(Guid id);
        Task<IEnumerable<T>> GetInatives();
        Task<IEnumerable<T>> SelectAsync();
        Task<bool> ExistsAsync(Guid id);
    }
}
