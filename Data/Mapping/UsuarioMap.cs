using Domain.Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapping
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("USUARIO");

            Id(x => x.Idusuario).Column("IDUSUARIO").GeneratedBy.Assigned();
            Map(x => x.Nome).Column("NOME").Not.Nullable();
            Map(x => x.Email).Column("EMAIL").Not.Nullable();
            Map(x => x.Telefone).Column("TELEFONE").Nullable();
            Map(x => x.Role).Column("ROLE").Not.Nullable();
            Map(x => x.Hash).Column("HASH");
            Map(x => x.Salt).Column("SALT");

            Map(x => x.Parceiro).Column("FKPARCEIRO").Not.Nullable();
            Map(x => x.Ativo).Column("ATIVO").Not.Nullable();
            Map(x => x.Datadealteracao).Column("DATADEALTERACAO");
            Map(x => x.Datadeinclusao).Column("DATADEINCLUSAO").Not.Nullable();
            Map(x => x.Datadeinativacao).Column("DATADEINATIVACAO");
            Map(x => x.Usuariodealteracao).Column("USUARIODEALTERACAO");
            Map(x => x.Usuariodeinclusao).Column("USUARIODEINCLUSAO").Not.Nullable();
            Map(x => x.Usuariodeinativacao).Column("USUARIODEINATIVACAO");
        }
    }
}
