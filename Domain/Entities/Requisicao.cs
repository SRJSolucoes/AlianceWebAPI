using Domain.Enum;
using Domain.Enum.Core;
using Domain.Models;
using Domain.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades
{
    // TODO Ajustar com os atributos da VIEW, não esquecer de incluir um ID
    public class Requisicao : EntidadeBase
    {   
        [Required(ErrorMessageResourceName = "Campo_obrigatorio", ErrorMessageResourceType = typeof(EntitiesResources))]
        //public virtual Guid Id { get; set; }
        public virtual string Codigo { get; set; }

        [Required(ErrorMessageResourceName = "Campo_obrigatorio", ErrorMessageResourceType = typeof(EntitiesResources))]
        [StringLength(100, ErrorMessageResourceName = "Campo_com_tamanho_excedido", ErrorMessageResourceType = typeof(EntitiesResources))]
        public virtual String Nome { get; set; }

    }
}
