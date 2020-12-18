using Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IShoopingService
    {
        Task<IEnumerable<RequisicaoDTO>> GetRequisicaoporCodigo(int Requisicao);
        Task<IEnumerable<RequisicaoDTO>> GetAllRequisicao();
        Task<IEnumerable<ITRequisicaoDTO>> GetItemdaRequisicao(int Requisicao);
        Task<IEnumerable<AnexoRequisicaoDTO>> GetAnexodaRequisicao(int Requisicao);
    }
}
