using Domain.Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapping
{
    // TODO Ajustar com os atributos da VIEW, não esquecer de incluir um ID
    public class RequisicaoMap : ClassMap<Requisicao>
    {
        public RequisicaoMap()
        {
            Table("CLIENTE_CLI");
            //Table("PARCEIRO");

            //Id(x => x.Id).Column("IDPARCEIRO").GeneratedBy.Assigned();
            //Map(x => x.Nome).Column("NOME").Not.Nullable();
            Id(x => x.Codigo).Column("CLI_CODIGO").GeneratedBy.Assigned();
            Map(x => x.Nome).Column("CLI_NOME").Not.Nullable();
        }
    }
}