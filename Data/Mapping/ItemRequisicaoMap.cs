using Domain.Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapping
{
    public class ItemRequisicaoMap : ClassMap<ItemRequisicao>
    {
        public ItemRequisicaoMap()
        {
            Table("IREQCOMPRA_IRC");
            //Table("E_ITREQAPROVAPI_VIEW");

            //Id(x => x.Id).Column("ID").Not.Nullable();
            Id(x => x.ReqNumero).Column("IRC_NUMERO").Not.Nullable();
            Map(x => x.NumItem).Column("IRC_NUMITEM").Not.Nullable();
            Map(x => x.Estoque).Column("IRC_TPESTOQ");
            Map(x => x.Item).Column("IRC_ITEM");
            Map(x => x.Descricao).Column("IRC_DESCRICAO");
            Map(x => x.GrupoCTA).Column("IRC_GRUPOCOTA");
            Map(x => x.QtdPedida).Column("IRC_QTDPEDIDA");
            Map(x => x.Unidade).Column("IRC_UNIDADE");
            Map(x => x.Entrega).Column("IRC_ENTREGA");
            Map(x => x.Valor).Column("IRC_VALOR");
            Map(x => x.VBUrgente).Column("IRC_URGENTE");
            Map(x => x.LocalEntrega).Column("IRC_LOCENTREGA");
            Map(x => x.Mapa).Column("IRC_MAPA");
            Map(x => x.QTDAtendida).Column("IRC_QTDATENDIDA");
            Map(x => x.DTAtendida).Column("IRC_DTATENDIDA");

        }
    }
}
