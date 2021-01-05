using Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IShoopingService
    {
        Task<IEnumerable<RequisicaoDTO>> GetRequisicaoporCodigo(string Requisicao);
        Task<IEnumerable<RequisicaoDTO>> GetAllRequisicao();
        Task<IEnumerable<ITRequisicaoDTO>> GetItemdaRequisicao(string Requisicao);
        Task<IEnumerable<AnexoRequisicaoDTO>> GetAnexodaRequisicao(string Requisicao);
    }
}
