using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class DetReqPagamentoModel
    {
        public String GRPAG { get; set; }
        public String DSGRPAG { get; set; }
        public String CCusto { get; set; }
        public String DSCCUSTO { get; set; }
        public String CTAFLUXO { get; set; }
        public String DSCTAFLUXO { get; set; }
        public Double VLRITEM { get; set; }
    }
}
