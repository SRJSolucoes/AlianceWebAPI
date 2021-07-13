﻿using Domain.Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapping
{
    public class RequisicaoMap : ClassMap<Requisicao>
    {
        public RequisicaoMap()
        {
            Table("E_REQAPROVAPI_VIEW");

            Id(x => x.ReqNumero).Column("REQNUMERO").Not.Nullable();
            //Map(x => x.ReqNumero).Column("REQNUMERO").Not.Nullable();
            Map(x => x.ReqData).Column("REQDATA");
            Map(x => x.Empresa).Column("EMPRESA");

            Map(x => x.Empresa_Nome).Column("EMPRESA_NOME");
            Map(x => x.Moeda).Column("MOEDA");
            Map(x => x.DS_Moeda).Column("DS_MOEDA");
            Map(x => x.TipoReq).Column("TIPOREQ");
            Map(x => x.DS_TipoReq).Column("DS_TIPOREQ");
            Map(x => x.Setor).Column("SETOR");
            Map(x => x.Requisitante).Column("REQUISITANTE");
            //Map(x => x.Posicao_Funcional).Column("POSICAO_FUNCIONAL");
            //Map(x => x.DSPosicao_Funcional).Column("DSPOSICAO_FUNCIONAL");
            Map(x => x.Observacao).Column("OBSERVACAO");
            Map(x => x.DataMov).Column("DATAMOV");
            Map(x => x.Etape).Column("ETAPE");
            Map(x => x.Tpoper).Column("TPOPER");
            Map(x => x.Reqcompra).Column("REQCOMPRA");
            Map(x => x.Usuario).Column("USUARIO");
            Map(x => x.Ordem).Column("ORDEM");
            Map(x => x.Justificativa).Column("JUSTIFICATIVA");
            Map(x => x.Devolucao).Column("DEVOLUCAO");
            Map(x => x.Protocolo).Column("PROTOCOLO");
            Map(x => x.ForaPrazo).Column("FORAPRAZO");
            Map(x => x.StatusAprv).Column("STATUSAPRV");
            Map(x => x.Impresso).Column("IMPRESSO");
            Map(x => x.Encerram).Column("ENCERRAM");
            Map(x => x.Destinacao).Column("DESTINACAO");
            Map(x => x.CCusto).Column("CCUSTO");
            Map(x => x.Fornecedor).Column("FORNECEDOR");
            Map(x => x.Fornecedor_Nome).Column("FORNECEDOR_NOME");
            Map(x => x.Tipo).Column("TIPO");
            Map(x => x.Valor_Total).Column("VALOR_TOTAL");
            Map(x => x.SqAprovacao).Column("SQAPROVACAO");
            Map(x => x.SqItemLinhaAprov).Column("SQITEMLINHAAPROV");
            //Map(x => x.Cat_Funcional).Column("CAT_FUNCIONAL");
            Map(x => x.SqItemAprovacao).Column("SQITEMAPROVACAO");
            Map(x => x.Usuario_Aprovador).Column("USUARIO_APROVADOR");
        }
    }
}