using System;

namespace Domain.VO.GestaoProcessos
{
    public class AprovacaoPendenteDetalheRelacaoPagamentoVO : AprovacaoPendenteVO
    {
        public Int64? IdRelacaoPagamento { get; set; }
        public String CdFornecedor { get; set; }
        public String DsFornecedor { get; set; }
        public String CdTpRequisicao { get; set; }
        public String DsTpRequisicao { get; set; }
        public String CdRequisicao { get; set; }
        public String Titulo { get; set; }
        public String DocFiscal { get; set; }
        public Decimal? ValorTitulo { get; set; }
        public DateTime? DtEntradaTitulo { get; set; }
        public DateTime? DtVencimentoTitulo { get; set; }
        public String CdMoeda { get; set; }
    }
}