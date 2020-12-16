using Domain.Models;
using Domain.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.UsuarioDto
{
    public class UsuarioChaveDTOCreate
    {
        [Required(ErrorMessageResourceName = "Campo_obrigatorio", ErrorMessageResourceType = typeof(EntitiesResources))]
        [StringLength(255, ErrorMessageResourceName = "Campo_com_tamanho_excedido", ErrorMessageResourceType = typeof(EntitiesResources))]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é um campo obrigatório para o login")]
        [EmailAddress(ErrorMessage = "Email em formato inválido")]
        [StringLength(255, ErrorMessage = "Email deve ter no máximo {1} caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Campo_obrigatorio", ErrorMessageResourceType = typeof(EntitiesResources))]
        public Guid Parceiro { get; set; }

        [Required(ErrorMessageResourceName = "Campo_obrigatorio", ErrorMessageResourceType = typeof(EntitiesResources))]
        public String Role { get; set; }
    }
}
