using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class AnexoRequisicaoModel
    {
        public String ReqNumero { get; set; }
        public String Codigo { get; set; }
        public byte Anexo { get; set; }
        public String FileName { get; set; }
        public String UsuInclusao { get; set; }
        public String Nome { get; set; }
        public DateTime DTInclusao { get; set; }
    }
}
