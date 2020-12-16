using Domain.Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapping
{
    public class O2sicontroleMap : ClassMap<O2sicontrole>
    {
        public O2sicontroleMap()
        {
            Table("O2SICONTROLE");

            Id(x => x.Ido2sicontrole).Column("IDO2SICONTROLE").GeneratedBy.Assigned();

            Map(x => x.Parceiro).Column("FKPARCEIRO").Not.Nullable();
            Map(x => x.Ativo).Column("ATIVO").Not.Nullable();
            Map(x => x.Datadealteracao).Column("DATAALTERACAO");
            Map(x => x.Datadeinclusao).Column("DATAINCLUSAO").Not.Nullable();
            Map(x => x.Datadeinativacao).Column("DATAINATIVACAO");
            Map(x => x.Usuariodealteracao).Column("USUARIOALTERACAO");
            Map(x => x.Usuariodeinclusao).Column("USUARIOINCLUSAO").Not.Nullable();
            Map(x => x.Usuariodeinativacao).Column("USUARIOINATIVACAO");
        }
    }
}
