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
using System.Net;
using System.Security.Cryptography;

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
                mxmLogin = baseDTO.Login;
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

        public LoginVO GetMXMLoginFromToken()
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

                var baseDTO = JsonConvert.DeserializeObject<WithTokenVO<Object>>(bodyString);

                mxmLogin = getCredentialsFromToken(baseDTO.Token);
            }
            req.Body.Position = 0;

            return mxmLogin;
        }

        private LoginVO getCredentialsFromToken(string Token)
        {
            const string consumerKey = "";
            const string consumerSecret = "";
            const string tokenSecret = "";
            const string tokenValue = "";
            const string host = "https://192.168.100.85";
            string UserID = "664";

            string url = $@"{host}/iso/coe/senha/{UserID}";

            string Escape(string s)
            {
                // https://stackoverflow.com/questions/846487/how-to-get-uri-escapedatastring-to-comply-with-rfc-3986
                var charsToEscape = new[] { "!", "*", "'", "(", ")" };
                var escaped = new StringBuilder(Uri.EscapeDataString(s));
                foreach (var t in charsToEscape)
                {
                    escaped.Replace(t, Uri.HexEscape(t[0]));
                }
                return escaped.ToString();
            }
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "GET";

            var timeStamp = ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
            var nonce = Convert.ToBase64String(Encoding.UTF8.GetBytes(timeStamp));

            var signatureBaseString = Escape(httpWebRequest.Method.ToUpper()) + "&";
            signatureBaseString += Uri.EscapeDataString(url.ToLower()) + "&";
            signatureBaseString += Uri.EscapeDataString(
                "oauth_consumer_key=" + Uri.EscapeDataString(consumerKey) + "&" +
                "oauth_nonce=" + Uri.EscapeDataString(nonce) + "&" +
                "oauth_signature_method=" + Uri.EscapeDataString("HMAC-SHA1") + "&" +
                "oauth_timestamp=" + Uri.EscapeDataString(timeStamp) + "&" +
                "oauth_token=" + Uri.EscapeDataString(tokenValue) + "&" +
                "oauth_version=" + Uri.EscapeDataString("1.0"));
            Console.WriteLine(@"signatureBaseString: " + signatureBaseString);

            var key = Uri.EscapeDataString(consumerSecret) + "&" + Uri.EscapeDataString(tokenSecret);
            Console.WriteLine(@"key: " + key);
            var signatureEncoding = new ASCIIEncoding();
            var keyBytes = signatureEncoding.GetBytes(key);
            var signatureBaseBytes = signatureEncoding.GetBytes(signatureBaseString);
            string signatureString;
            using (var hmacsha1 = new HMACSHA1(keyBytes))
            {
                var hashBytes = hmacsha1.ComputeHash(signatureBaseBytes);
                signatureString = Convert.ToBase64String(hashBytes);
            }
            signatureString = Uri.EscapeDataString(signatureString);
            Console.WriteLine(@"signatureString: " + signatureString);

            string SimpleQuote(string s) => '"' + s + '"';
            var header =
                "OAuth realm=" + SimpleQuote("") + "," +
                "oauth_consumer_key=" + SimpleQuote(consumerKey) + "," +
                "oauth_nonce=" + SimpleQuote(nonce) + "," +
                "oauth_signature_method=" + SimpleQuote("HMAC-SHA1") + "," +
                "oauth_timestamp=" + SimpleQuote(timeStamp) + "," +
                "oauth_token=" + SimpleQuote(tokenValue) + "," +
                "oauth_version=" + SimpleQuote("1.0") + "," +
                "oauth_signature= " + SimpleQuote(signatureString);
            Console.WriteLine(@"header: " + header);
            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, header);

            var response = httpWebRequest.GetResponse();
            var characterSet = ((HttpWebResponse)response).CharacterSet;
            var responseEncoding = characterSet == ""
                ? Encoding.UTF8
                : Encoding.GetEncoding(characterSet ?? "utf-8");
            var responsestream = response.GetResponseStream();
            if (responsestream == null)
            {
                throw new ArgumentNullException(nameof(characterSet));
            }
            using (responsestream)
            {
                var reader = new StreamReader(responsestream, responseEncoding);
                var result = reader.ReadToEnd();
                Console.WriteLine(@"result: " + result);
            }

            return new LoginVO();
        }
    }
}
