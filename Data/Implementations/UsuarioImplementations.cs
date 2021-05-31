using AcessoWebApi.Infrastructure.Security;
using Data.Repository;
using Domain.Config;
using Domain.Entidades;
using Domain.Interfaces;
using Domain.Repository;
using Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class UsuarioImplementations : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly Func<string, ISession> _session;

        private readonly IPasswordHasher _passwordHasher;
        private readonly ICurrentUserAccessor _currentUserAccessor;
        private AlianceApiSettings _appSettings;

        public UsuarioImplementations(
            Func<string, ISession> _session, 
            IPasswordHasher _passwordHasher, 
            ICurrentUserAccessor _currentUserAccessor,
            IOptionsSnapshot<AlianceApiSettings> appSettings,
            IConfiguration configuration
            ) : base(
                _session, 
                _currentUserAccessor,
                appSettings,
                configuration
           )
        {
            this._session = _session;
            this._passwordHasher = _passwordHasher;
            this._currentUserAccessor = _currentUserAccessor;
            
            _appSettings = appSettings.Value;
        }

        public async Task<Usuario> FindByLogin(Guid Idparceiro, string email, string senha)
        {
            var _usuario = await _session("Acesso").Query<Usuario>().FirstOrDefaultAsync(u => u.Parceiro.Equals(Idparceiro) && u.Email.Equals(email) && u.Ativo.Equals('A'));
            if (_usuario is null)
            {
                var _usuarioMock = new UsuarioO2SiMock().usuarios.FirstOrDefault(u => u.Email.Equals(email) && u.Senha.Equals(senha));

                if (_usuarioMock is null) { return null; }
                else
                {
                    return new Usuario()
                    {
                        Idusuario = _usuarioMock.Idusuario,
                        Parceiro = Guid.Empty,
                        Email = _usuarioMock.Email,
                        Role = _usuarioMock.Role,
                    };
                }
            }
            else
            {
                if (_usuario.Hash.SequenceEqual(_passwordHasher.Hash(senha, _usuario.Salt)))
                { return _usuario; }
                else { return null; }
            }
        }

        public async Task<Usuario> FindtoChangePassword(Guid Idusuario)
        {
            return await _session("Acesso").Query<Usuario>().FirstOrDefaultAsync(u => u.Idusuario.Equals(Idusuario));

        }
    }
}
