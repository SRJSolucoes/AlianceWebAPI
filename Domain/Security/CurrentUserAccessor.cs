using Domain.Entidades;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using Domain.DTOs;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Domain.VO;

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

        public HttpRequest GetRequest => _accessor.HttpContext.Request;

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

        public LoginVO GetMXMLoginFromRequestBody()
        {
            var req = GetRequest;
            //req.EnableRewind();
            req.EnableBuffering();
            LoginVO mxmLogin = null;
            using (var reader = new StreamReader(
                   req.Body,
                   encoding: Encoding.UTF8,
                   true, 1024, true
            ))
            {
                var bodyString = reader.ReadToEndAsync().Result;

                // TODO Mechi aqui para ajustar o Login
                var baseDTO = JsonConvert.DeserializeObject<WithLoginVO<Object>>(bodyString);
                mxmLogin.Usuario = "HOM_SHP";
                mxmLogin.Senha = "HOM_SHP";
                mxmLogin.Host = "10.0.100.23";
                mxmLogin.ServiceName = "HOM";
                mxmLogin.Port = "1521";
            }
            req.Body.Position = 0;

            return mxmLogin;
        }

        public LoginVO GetMXMLoginFromRequestHeaderBasic()
        {
            var req = GetRequest;
            string authHeader = req.Headers["Authorization"];
            string ambHeader = req.Headers["ServiceName"];
            LoginVO mxmLogin = null;

            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                string headerEncoded = authHeader.Substring("Basic ".Length).Trim();
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(headerEncoded));

                int seperatorIndex = usernamePassword.IndexOf(':');

                mxmLogin.Usuario = usernamePassword.Substring(0, seperatorIndex);
                mxmLogin.Senha = usernamePassword.Substring(seperatorIndex + 1);
                mxmLogin.ServiceName = ambHeader;
            }

            return mxmLogin;
        }
    }
}
