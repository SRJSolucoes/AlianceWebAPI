using Domain.Enum;
using Domain.Enum.Core;
using Domain.Models;
using Domain.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades
{
    public class DetReqPagamento : EntidadeBase
    {
        public virtual String GRPAG { get; set; }
        public virtual String DSGRPAG { get; set; }
        public virtual String CCUSTO { get; set; }
        public virtual String DSCCUSTO { get; set; }
        public virtual String CTAFLUXO { get; set; }
        public virtual String DSCTAFLUXO { get; set; }
        public virtual Double VLRITEM { get; set; }

        //PGRR.PGRR_CDGRUPO GRPAG,
        //GPA.GPA_DSGRUPO DSGRPAG,
        //PGRR.PGRR_NOCCUSTO CCUSTO,
        //CC.CC_DESCRICAO DSCCUSTO,
        //PGRR.PGRR_CTAFCAIXA CTAFLUXO,
        //FCX.FCX_DESCRICAO DSCTAFLUXO,
        //PGRR.PGRR_VLRGRUPO VLRITEM

    }
}
