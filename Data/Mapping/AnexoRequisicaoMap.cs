using Domain.Entidades;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping
{
    public class AnexoRequisicaoMap : ClassMap<AnexoRequisicao>
    {
        public AnexoRequisicaoMap()
        {
            Table("ANEXOREQCOMPRA_ARC");

            //Id(x => x.Id).Column("ID").Not.Nullable();
            Id(x => x.ReqNumero).Column("ARC_NUMERO").Not.Nullable();
            Map(x => x.Codigo).Column("ARC_CODIGO");
            Map(x => x.Anexo).Column("ARC_ANEXO");
            Map(x => x.FileName).Column("ARC_FILENAME");
            //Map(x => x.UsuInclusao).Column("USUINCLUSAO");
            //Map(x => x.Nome).Column("NOME");
            //Map(x => x.DTInclusao).Column("DTINCLUSAO");
        }
    }
}

