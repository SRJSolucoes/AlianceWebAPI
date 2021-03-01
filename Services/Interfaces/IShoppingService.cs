using Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IShoppingService
    {
        Task<IEnumerable<RequisicaoDTO>> GetRequisicaoporCodigo(string Requisicao);
        Task<IEnumerable<RequisicaoDTO>> GetAllRequisicao(string UsuarioName);
        Task<IEnumerable<ItemRequisicaoDTO>> GetItemdaRequisicao(string Requisicao);
        Task<IEnumerable<AnexoRequisicaoDTO>> GetAnexodaRequisicao(string Requisicao);
    }
}
