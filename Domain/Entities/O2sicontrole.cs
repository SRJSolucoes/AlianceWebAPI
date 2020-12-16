using Domain.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades
{
    public class O2sicontrole : EntidadeBase
    {
        [Required(ErrorMessageResourceName = "Campo_obrigatorio", ErrorMessageResourceType = typeof(EntitiesResources))]
        public virtual Guid Ido2sicontrole { get; set; }
    } 
}