using System;

namespace Domain.Entidades
{
    public class UsuarioO2Si
    {
        public virtual Guid Idusuario { get; set; }
        public virtual Guid Idparceiro { get; set; }
        public virtual String Email { get; set; }
        public virtual String Role { get; set; }
        public virtual String Senha { get; set; }
        
    }
}
