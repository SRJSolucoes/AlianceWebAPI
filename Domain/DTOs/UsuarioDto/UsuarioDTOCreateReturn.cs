using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.UsuarioDto
{
    public class UsuarioDTOCreateReturn
    {
        public Guid Idusuario { get; set; }

        public String Nome { get; set; }

        public String Email { get; set; }

        public String Senha { get; set; }

        public DateTime DatadeInclusao;
    }
}
