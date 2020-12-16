using Domain.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades
{
    public class Usuario: EntidadeBase
    {
        [Required(ErrorMessageResourceName = "Campo_obrigatorio", ErrorMessageResourceType = typeof(EntitiesResources))]
        public virtual Guid Idusuario { get; set; }

        [Required(ErrorMessageResourceName = "Campo_obrigatorio", ErrorMessageResourceType = typeof(EntitiesResources))]
        [StringLength(100, ErrorMessageResourceName = "Campo_com_tamanho_excedido", ErrorMessageResourceType = typeof(EntitiesResources))]
        public virtual String Nome { get; set; }

        [Required(ErrorMessageResourceName = "Campo_obrigatorio", ErrorMessageResourceType = typeof(EntitiesResources))]
        [StringLength(50, ErrorMessageResourceName = "Campo_com_tamanho_excedido", ErrorMessageResourceType = typeof(EntitiesResources))]
        public virtual String Email { get; set; }

        [StringLength(20, ErrorMessageResourceName = "Campo_com_tamanho_excedido", ErrorMessageResourceType = typeof(EntitiesResources))]
        public virtual String Telefone { get; set; }

        [StringLength(50, ErrorMessageResourceName = "Campo_com_tamanho_excedido", ErrorMessageResourceType = typeof(EntitiesResources))]
        [Required(ErrorMessageResourceName = "Campo_obrigatorio", ErrorMessageResourceType = typeof(EntitiesResources))]
        public virtual String Role { get; set; }

        //[Required(ErrorMessageResourceName = "Campo_obrigatorio", ErrorMessageResourceType = typeof(EntitiesResources))]
        public virtual byte[] Hash { get; set; }

        //[Required(ErrorMessageResourceName = "Campo_obrigatorio", ErrorMessageResourceType = typeof(EntitiesResources))]
        public virtual byte[] Salt { get; set; }
    }
}
