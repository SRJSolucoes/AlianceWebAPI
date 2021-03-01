using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class AnexoRequisicaoDTO
    {
        public int Id { get; set; }
        public String ReqNumero { get; set; }
        public String Codigo { get; set; }
        public byte Anexo { get; set; }
        public String FileName { get; set; }

        //public String UsuInclusao { get; set; }
        //public String Nome { get; set; }
        //public String DTInclusao { get; set; }
    }
}
