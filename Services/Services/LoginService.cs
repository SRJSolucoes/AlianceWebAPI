using Domain.DTOs;
using Domain.Entidades;
using Domain.Repository;
using Domain.Security;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Service.Services
{
    public class LoginService : ILoginService
    {
        IUsuarioRepository _repository;

        private SigningConfigurations _signingConfigurations;

        private TokenConfiguration _tokenConfiguration;

        public LoginService(IUsuarioRepository repository,
                            SigningConfigurations signingConfigurations,
                            TokenConfiguration tokenConfiguration)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfiguration = tokenConfiguration;

        }

        public async Task<object> FindByLogin(LoginDto usuario)
        {
            try
            {
                var usuarioBase = new Usuario();

                if (usuario != null && !string.IsNullOrWhiteSpace(usuario.Email))
                {
                    usuarioBase = await _repository.FindByLogin(usuario.Idparceiro, usuario.Email, usuario.Senha);

                    if (usuarioBase == null)
                        return new { Autenticado = false, Mensagem = "Falha na autenticação" };
                    else
                    {
                        ClaimsIdentity identidade = new ClaimsIdentity(
                            new GenericIdentity(usuarioBase.Idusuario.ToString()),
                            new[]
                            {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, usuarioBase.Idusuario.ToString()),
                            new Claim(JwtRegisteredClaimNames.Email, usuarioBase.Email),
                            new Claim(ClaimTypes.Role, usuarioBase.Role),
                            new Claim("IdUsuario", usuarioBase.Idusuario.ToString()),
                            new Claim("IdParceiro", usuarioBase.Parceiro.ToString()),
                            });
                        

                        DateTime createDate = DateTime.Now;
                        DateTime expiration = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);
                        DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                        var handle = new JwtSecurityTokenHandler();
                        string Token = CriaToken(identidade, createDate, expirationDate, handle);
                        return ObjetodeSucesso(createDate, expirationDate, Token, usuario);
                    }
                }
                else return new { Autenticado = false, Mensagem = "Falha na autenticação" };
            }
            catch (Exception e)
            {
                return new { Autenticado = false, Mensagem = e.Message };
            }
        }

        private string CriaToken(ClaimsIdentity identidade,
                                 DateTime horadeCriacao,
                                 DateTime horadeExpirar,
                                 JwtSecurityTokenHandler handle)
        {

            var TokendeSeguranca = handle.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identidade,
                NotBefore = horadeCriacao,
                Expires = horadeExpirar,
            });

            var token = handle.WriteToken(TokendeSeguranca);
            return token;

        }

        private object ObjetodeSucesso(DateTime createDate, DateTime ExpirationDate, string token, LoginDto usuario)
        {
            return new
            {
                Autenticado = true,
                Criadoem = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiraem = ExpirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Token = token,
                Usuario = usuario.Email,
                Mensagem = "Usuário logado com sucesso"
            };
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
