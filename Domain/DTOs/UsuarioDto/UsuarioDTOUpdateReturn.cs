using System;

namespace Domain.DTOs.UsuarioDto
{
    public class UsuarioDTOUpdateReturn
    {
        public Guid Idusuario { get; set; }

        public DateTime DatadeAlteracao;

        public String Nome { get; set; }

        public String Email { get; set; }
    }
}
