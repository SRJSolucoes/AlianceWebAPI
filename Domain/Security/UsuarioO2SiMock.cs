using Domain.Entidades;
using System;
using System.Collections.Generic;

namespace Domain.Security
{
    public class UsuarioO2SiMock
    {
        public List<UsuarioO2Si> usuarios;

        public UsuarioO2SiMock()
        {
            usuarios = new List<UsuarioO2Si>(){
                new UsuarioO2Si {
                    Idusuario = new Guid("440cdeee-5b8a-462a-96fd-20b24bd82f55"),
                    Idparceiro = new Guid("440cdeee-5b8a-462a-96fd-20b24bd82f55"),
                    Email = "admin@srjsolucoes.com.br",
                    Role = "Admin",
                    Senha = "440çdeee-5b8a-462a-96fd-20b24b"
                }
            };
        }
    }
}
