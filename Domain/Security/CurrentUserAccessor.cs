using Domain.Entidades;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;

namespace AcessoWebApi.Infrastructure.Security
{
    public class CurrentUserAccessor : ICurrentUserAccessor
    {
        private readonly IHttpContextAccessor _accessor;

        public CurrentUserAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public UsuarioO2Si GetCurrentUser()
        {
            Guid _idusuario = GetCurrentUserID();
            Guid _idparceiro = GetCurrentParcID();
            string _emailusuario = GetCurrentUserEmail();
            string _role = GetCurrentUserRole();

            UsuarioO2Si usuario = new UsuarioO2Si()
            {
                Idusuario = _idusuario,
                Idparceiro = _idparceiro,
                Email = _emailusuario,
                Role = _role
            };

            return usuario;
        }

        public string GetCurrentUserEmail()
        {
            return GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.Email)?.Value;
        }

        public string GetCurrentUserRole()
        {
            return GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.Role)?.Value;
        }

        public Guid GetCurrentUserID()
        {
            return new Guid(GetClaimsIdentity().FirstOrDefault(a => a.Type == "IdUsuario")?.Value);
        }

        public Guid GetCurrentParcID()
        {
            return new Guid(GetClaimsIdentity().FirstOrDefault(a => a.Type == "IdParceiro")?.Value);
        }
    }
}
