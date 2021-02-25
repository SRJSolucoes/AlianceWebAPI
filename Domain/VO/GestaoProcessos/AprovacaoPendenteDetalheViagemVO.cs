using System;

namespace Domain.VO.GestaoProcessos
{
    public class AprovacaoPendenteDetalheViagemVO : AprovacaoPendenteVO
    {
        public Int64 IdentificadorViagem { get; set; }
        public String CodigoViajante { get; set; }
        public String NomeViajante { get; set; }
        public String DescricaoViajante { get; set; }
        public String CodigoSetor { get; set; }
        public String DescricaoSetor { get; set; }
        public String CodigoCentroCusto { get; set; }
        public String DescricaoCentroCusto { get; set; }
        public String CodigoProjeto { get; set; }
        public String DescricaoProjeto { get; set; }
        public String CodigoMotivoViagem { get; set; }
        public String DescricaoMotivoViagem { get; set; }
        public String DescricaoDetalhamento { get; set; }
    }
}
