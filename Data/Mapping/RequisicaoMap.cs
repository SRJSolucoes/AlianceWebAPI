using Domain.Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapping
{
    // TODO Ajustar com os atributos da VIEW, não esquecer de incluir um ID
    public class RequisicaoMap : ClassMap<Requisicao>
    {
        public RequisicaoMap()
        {
            Table("E_REQAPROVAPI_VIEW");

            Map(x => x.ReqNumero).Column("REQNUMERO").Not.Nullable();
            Map(x => x.ReqData).Column("REQDATA");
            Map(x => x.Empresa).Column("EMPRESA");

            Map(x => x.Empresa_Nome).Column("EMPRESA_NOME");
            Map(x => x.Moeda).Column("MOEDA");
            Map(x => x.DS_Moeda).Column("DS_MOEDA");
            Map(x => x.TipoReq).Column("TIPOREQ");
            Map(x => x.DS_TipoReq).Column("DS_TIPOREQ");
            Map(x => x.Setor).Column("SETOR");
            Map(x => x.Requisitante).Column("REQUISITANTE");
            Map(x => x.Posicao_Funcional).Column("POSICAO_FUNCIONAL");
            Map(x => x.DS_Posicao_Funcional).Column("DSPOSICAO_FUNCIONAL");
            Map(x => x.Observacao).Column("OBSERVACAO");
            Map(x => x.DataMov).Column("DATAMOV");
            Map(x => x.Etape).Column("ETAPE");
            Map(x => x.Tpoper).Column("TPOPER");
            Map(x => x.VBReqcompra).Column("REQCOMPRA");   
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


        }
    }
}