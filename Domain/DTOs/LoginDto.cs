using Domain.Models;
using Domain.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessageResourceName = "Campo_obrigatorio", ErrorMessageResourceType = typeof(EntitiesResources))]
        public Guid Idparceiro { get; set; }

        [Required (ErrorMessage = "Email é um campo obrigatório para o login")]
        [EmailAddress (ErrorMessage = "Email em formato inválido")]
        [StringLength(100, ErrorMessage = "Email deve ter no máximo {1} caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email é um campo obrigatório para o login")]
        [StringLength(30, ErrorMessage = "Email deve ter no máximo {1} caracteres e no mínimo {2}.", MinimumLength = 5)]            
        public string Senha { get; set; }
    }
}
