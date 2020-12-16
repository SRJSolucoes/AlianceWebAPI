using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.UsuarioDto
{
    public class UsuarioDTO
    {
        public Guid Idusuario { get; set; }
        public String Nome { get; set; }
        public String Email { get; set; }
    }
}
