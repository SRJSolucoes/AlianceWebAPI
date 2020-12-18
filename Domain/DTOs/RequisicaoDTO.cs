    using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class RequisicaoDTO
    {
        public string? ReqNumero { get; set; }
        public DateTime? ReqData { get; set; }
        public string? Empresa { get; set; }
        public string? Empresa_Nome { get; set; }
        public string? Moeda { get; set; }
        public string? DS_Moeda { get; set; }
        public string? TipoReq { get; set; }
        public string? DS_TipoReq { get; set; }
        public string? Setor { get; set; }
        public string? Requisitante { get; set; }
        public string? Posicao_Funcional { get; set; }
        public string? DS_Posicao_Funcional { get; set; }
        public string? Observacao { get; set; }
        public DateTime? DataMov { get; set; }
        public string? Etape { get; set; }
        public string? Tpoper { get; set; }
        public string? VBReqcompra { get; set; }
        public string? Usuario { get; set; }
        public string? Ordem { get; set; }
        public string? Justificativa { get; set; }
        public string? Devolucao { get; set; }
        public string? Protocolo { get; set; }
        public string? ForaPrazo { get; set; }
        public string? StatusAprv { get; set; }
        public string? Impresso { get; set; }
        public string? Encerram { get; set; }
        public string? Destinacao { get; set; }
        public string? CCusto { get; set; }

    }
}
