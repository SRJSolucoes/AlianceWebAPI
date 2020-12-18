using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class AnexoRequisicaoDTO
    {
        public string ReqNumero { get; set; }
        public string Codigo { get; set; }
        public byte? Anexo { get; set; }
        public string? FileName { get; set; }
        public string UsuInclusao { get; set; }
        public string? Nome { get; set; }
        public string DTInclusao { get; set; }
    }
}
