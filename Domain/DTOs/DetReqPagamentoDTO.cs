using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class DetReqPagamentoDTO
    {
        public String CDGrupo { get; set; }
        public String DescGrupo { get; set; }
        public String CCusto { get; set; }
        public String DescCCusto { get; set; }
        public String CTAFluxo { get; set; }
        public String DescCTAFluxo { get; set; }
        public Double ValorItem { get; set; }
    }
}
