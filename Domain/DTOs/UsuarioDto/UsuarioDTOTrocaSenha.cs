using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.UsuarioDto
{
    public class UsuarioDTOTrocaSenha
    {
        public Guid Idusuario { get; set; }

        public String SenhaNova { get; set; }

        public String SenhaAtual { get; set; }
    }
}
