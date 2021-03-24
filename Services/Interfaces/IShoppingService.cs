using Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IShoppingService
    {
        Task<IEnumerable<RequisicaoDTO>> GetRequisicaoporCodigo(string Requisicao);
        Task<IEnumerable<RequisicaoDTO>> GetAllRequisicao(string usuarioEmail);
        Task<IEnumerable<ItemRequisicaoDTO>> GetItemdaRequisicao(string Requisicao, string usuarioNome);
        Task<IEnumerable<DetReqPagamentoDTO>> GetDetReqPagamento(string Requisicao, string codigoFornecedor);
        Task<IEnumerable<AnexoRequisicaoDTO>> GetAnexodaRequisicao(string Requisicao);
    }
}
