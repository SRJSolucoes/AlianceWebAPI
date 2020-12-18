using Domain.Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapping
{
    public class ITRequisicaoMap : ClassMap<ITRequisicao>
    {
        public ITRequisicaoMap()
        {
            Table("E_ITREQAPROVAPI_VIEW");

            Map(x => x.ReqNumero).Column("REQNUMERO").Not.Nullable();
            Map(x => x.NumItem).Column("NUMITEM").Not.Nullable();
            Map(x => x.Estoque).Column("ESTOQUE");
            Map(x => x.Item).Column("ITEM");
            Map(x => x.Descricao).Column("DESCRICAO");
            Map(x => x.GrupoCTA).Column("GRUPOCTA");
            Map(x => x.QtdPedida).Column("QTDPEDIDA");
            Map(x => x.Unidade).Column("UNIDADE");
            Map(x => x.Entrega).Column("ENTREGA");
            Map(x => x.Valor).Column("VALOR");
            Map(x => x.VBUrgente).Column("VBURGENTE");
            Map(x => x.LocalEntrega).Column("LOCALENTREGA");
            Map(x => x.Mapa).Column("MAPA");
            Map(x => x.QTDAtendida).Column("QTDATENDIDA");
            Map(x => x.DTAtendida).Column("DTATENDIDA");

        }
    }
}
