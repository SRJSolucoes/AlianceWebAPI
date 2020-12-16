using Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IShoopingService
    {
        Task<IEnumerable<RequisicaoDTO>> GetAll();
    }
}
