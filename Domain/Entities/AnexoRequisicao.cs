using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entidades
{
    public class AnexoRequisicao : EntidadeBase
    {
        public virtual String ReqNumero { get; set; }
        public virtual String Codigo { get; set; }
        public virtual byte? Anexo { get; set; }
        public virtual String FileName { get; set; }
        public virtual String UsuInclusao { get; set; }
        public virtual String Nome { get; set; }
        public virtual DateTime DTInclusao { get; set; }
    }
}
