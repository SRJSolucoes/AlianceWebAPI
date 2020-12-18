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
            Table("E_ANEXOREQAPROVAPI_VIEW");

            Map(x => x.ReqNumero).Column("REQNUMERO").Not.Nullable();
            Map(x => x.Codigo).Column("CODIGO");
            Map(x => x.Anexo).Column("ANEXO");
            Map(x => x.FileName).Column("FILENAME");
            Map(x => x.UsuInclusao).Column("USUINCLUSAO");
            Map(x => x.Nome).Column("NOME");
            Map(x => x.DTInclusao).Column("DTINCLUSAO");
        }
    }
}

