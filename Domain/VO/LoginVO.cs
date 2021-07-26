using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.VO
{
    public class LoginVO
    {
        [Required(ErrorMessage = "Usuario é um campo obrigatório para o login")]
        [StringLength(100, ErrorMessage = "Usuario deve ter no máximo {1} caracteres e no mínimo {2}.", MinimumLength = 4)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Senha é um campo obrigatório para o login")]
        [StringLength(100, ErrorMessage = "Senha deve ter no máximo {1} caracteres e no mínimo {2}.", MinimumLength = 2)]
        public string Senha { get; set; }
        public string? ServiceName { get; set; }
        public string? SID { get; set; }
        public string? AmbWs { get; set; }
        public string? Host { get; set; }
        public string? Port { get; set; }

    }
}
