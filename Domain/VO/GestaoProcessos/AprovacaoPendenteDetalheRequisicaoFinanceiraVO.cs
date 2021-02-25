using System;
using System.Collections.Generic;

namespace Domain.VO.GestaoProcessos
{
    public class AprovacaoPendenteDetalheRequisicaoFinanceiraVO : AprovacaoPendenteDetalheRequisicaoVO
    {
        public String Nome { get; set; }
        public String Documento { get; set; }
        public String Filial { get; set; }
        public String Conta { get; set; }
        public String TipoRequisicao { get; set; }

        /// <summary>
        /// Atributos para Projeto específico 
        /// Não foi criado outro VO devido a complexidade do processo Aprovações Pendentes
        /// Caso fosse criado outro VO para ser usado somente em Projeto específico
        /// teria que alterar toda a rotina de Aprovações Pendentes do Manager para classe genérica
        /// </summary>
        public DateTime? DataEntrada { get; set; }
        public String CentroCusto { get; set; }
        public String Projeto { get; set; }
        public String JustificativaRequisicaoFinanceira { get; set; }
        public String Sistema { get; set; }

        public List<AprovacaoPendenteDetalheApropriacaoVO> ListaAprovacaoPendenteDetalheApropriacaoVO { get; set; }
    }
}
