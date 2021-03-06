﻿using Domain.Entidades;
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
            Id(x => x.ReqNumero).Column("REQNUMERO").Not.Nullable();
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
            Map(x => x.SqAprovacao).Column("SQAPROVACAO");
            Map(x => x.SqItemAprovacao).Column("SQITEMAPROVACAO");
        }
    }
}
