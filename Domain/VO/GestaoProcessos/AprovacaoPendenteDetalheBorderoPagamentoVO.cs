using System;

namespace Domain.VO.GestaoProcessos
{
    public class AprovacaoPendenteDetalheBorderoPagamentoVO : AprovacaoPendenteVO
    {
        public String NomeFornecedor { get; set; }
        public String Titulo { get; set; }
        public DateTime? DataVencimento { get; set; }

        public String CodigoConta { get; set; }
        public String Observacao { get; set; }
    }
}
