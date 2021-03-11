using Domain.Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapping
{
    public class DetReqPagamentoMap : ClassMap<DetReqPagamento>
    {
        public DetReqPagamentoMap()
        {
            Table("E_REQAPROVAPI_VIEW");

            Id(x => x.GRPAG).Column("GRPAG").Not.Nullable();
            Map(x => x.DSGRPAG).Column("DSGRPAG");
            Map(x => x.CCUSTO).Column("CCUSTO");
            Map(x => x.DSCCUSTO).Column("DSCCUSTO");
            Map(x => x.CTAFLUXO).Column("CTAFLUXO");
            Map(x => x.DSCTAFLUXO).Column("DSCTAFLUXO");
            Map(x => x.VLRITEM).Column("VLRITEM");
    }
    }
}