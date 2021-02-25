using System;

namespace Domain.VO.GestaoProcessos
{
    public class AprovacaoPendenteDetalheRequisicaoVO : AprovacaoPendenteVO
    {
        public String Descricao { get; set; }
        public String Setor { get; set; }
        public String Observacao { get; set; }
    }
}