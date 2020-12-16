using Domain.DTOs;
using System;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ILoginService
    {
        Task<object> FindByLogin(LoginDto usuario);
    }
}
