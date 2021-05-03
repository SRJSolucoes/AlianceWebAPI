using AcessoWebApi.Infrastructure.Security;
using Data.FluentySession;
using Data.Handlers;
using Domain.Entidades;
using Domain.Enum;
using Domain.Enum.Core;
using Domain.Interfaces;
using Domain.Models;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : EntidadeBase
    {
        //private readonly Func<string, ISession> _session;
        private readonly ISession _session;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public RepositoryBase(
            Func<string, ISession> session,
            ICurrentUserAccessor currentUserAccessor
        )
        {
            var sessaoDefault = session("Acesso");
            ISession sessaoCustomizada = null;

            var userFromToken = currentUserAccessor.GetMXMLoginFromToken();
            if (userFromToken != null)
            {
                var sessionFact = SessionFact.GetSessionFact(userFromToken.Usuario, userFromToken.Senha, userFromToken.ServiceName);
                if (sessionFact != null)
                {
                    sessaoCustomizada = sessionFact.OpenSession();
                }
            }

            var userFromBody = currentUserAccessor.GetMXMLoginFromRequestBody();
            if (userFromBody != null && sessaoCustomizada == null)
            {
                var sessionFact = SessionFact.GetSessionFact(userFromBody.Usuario, userFromBody.Senha, userFromBody.ServiceName);
                if (sessionFact != null)
                {
                    sessaoCustomizada = sessionFact.OpenSession();
                }
            }

            var userFromHeader = currentUserAccessor.GetMXMLoginFromRequestHeaderBasic();
            if (userFromHeader != null && sessaoCustomizada == null)
            {
                var sessionFact = SessionFact.GetSessionFact(userFromHeader.Usuario, userFromHeader.Senha, userFromHeader.ServiceName);
                if (sessionFact != null)
                {
                    sessaoCustomizada = sessionFact.OpenSession();
                }
            }

            var sessaoAtual = sessaoDefault;
            if (!sessaoDefault.IsOpen)
            {
                sessaoAtual = sessaoCustomizada.SessionFactory.OpenSession();
            }

            if (sessaoCustomizada != null )
            {
                if (!sessaoCustomizada.IsOpen)
                {
                    sessaoAtual = sessaoCustomizada.SessionFactory.OpenSession();
                }
                else
                {
                    sessaoAtual = sessaoCustomizada;
                }
                // sessaoDefault.Close();
            }

            this._session = sessaoAtual;
            this._currentUserAccessor = currentUserAccessor;
        }

        public async Task<T> InsertAsync(T item)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    item.Datadealteracao = null;
                    item.Datadeinativacao = null;
                    item.Usuariodealteracao = null;
                    item.Usuariodeinativacao = null;
                    if (item.Ativo == '\0')
                    {
                        item.Ativo = 'A';
                    }

                    item.Datadeinclusao = DateTime.UtcNow;
                    item.Usuariodeinclusao = _currentUserAccessor.GetCurrentUser().Idusuario;
                    if (item.Parceiro == Guid.Empty)
                    {
                        item.Parceiro = _currentUserAccessor.GetCurrentUser().Idparceiro;
                    }

                    await _session.SaveAsync(item);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    if (!transaction.WasCommitted) transaction.Rollback();
                    throw ex;
                }
                return item;
            }
        }

        public async Task<T> UpdateAsync(T item, Guid id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var _itemCadastrado = await _session.GetAsync<T>(id);
                    if (_itemCadastrado == null)
                        return null;
                    else
                    {

                        item.Datadeinclusao = _itemCadastrado.Datadeinclusao;
                        item.Usuariodeinclusao = _itemCadastrado.Usuariodeinclusao;
                        item.Parceiro = _itemCadastrado.Parceiro;
                        item.Ativo = _itemCadastrado.Ativo;

                        item.Datadealteracao = DateTime.UtcNow;
                        if (item.Usuariodealteracao == null)
                            item.Usuariodealteracao = _currentUserAccessor.GetCurrentUser().Idusuario;

                        await _session.MergeAsync(item);
                        await _session.FlushAsync();
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    if (!transaction.WasCommitted) transaction.Rollback();
                    throw ex;
                }
                return item;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var result = await _session.GetAsync<T>(id);
                    if (result == null)
                        return false;
                    else
                    {

                        result.Ativo = 'I';
                        result.Datadeinativacao = DateTime.UtcNow;
                        result.Usuariodeinativacao = _currentUserAccessor.GetCurrentUser().Idusuario;

                        await _session.UpdateAsync(result);

                        transaction.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    if (!transaction.WasCommitted) transaction.Rollback();

                    throw ex;
                }
            }
        }

        public async Task<bool> DeleteAdminAsync(Guid id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var result = await _session.GetAsync<T>(id);
                    if (result == null)
                        return false;
                    else
                    {

                        await _session.DeleteAsync(result);

                        transaction.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    if (!transaction.WasCommitted) transaction.Rollback();

                    throw ex;
                }
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {

            var result = await _session.GetAsync<T>(id);
            if (result == null)
                return false;
            else
                return result.Ativo != 'A';
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                var result = await _session.GetAsync<T>(id);
                if (result != null)
                    return result.Ativo.Equals('A') ? result : null;
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                // TODO Comentei por se tratar de uma regra da nossa framework 
                //var _idparceiro = _currentUserAccessor.GetCurrentUser().Idparceiro;
                //return await (from e in _session.Query<T>() select e).Where(e => e.Parceiro.Equals(_idparceiro) && e.Ativo.Equals('A')).ToListAsync();
                return await (from e in _session.Query<T>() select e).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<T> QuerySelect()
        {
            try
            {
                return _session.Query<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<T> ExecuteQuerySelect(string queryStatement)
        {
            try
            {
                //var query = _session.CreateSQLQuery(queryStatement)
                //                    .AddEntity(typeof(T));
                var query = _session.CreateSQLQuery(queryStatement)
                            .SetResultTransformer(NHibernate.Transform.Transformers.AliasToBean<T>());

                var list = query.List<T>();

                return list;
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    throw new HttpStatusException(
                        HttpStatusCode.InternalServerError, 
                        "Falha ao realizar a conversão da lista para o lista do tipo informado." 
                        +" Verifique se a tipagem dos campos classe correspondem aos campos da query. " 
                        + ex.Message
                    );
                }
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> GetInatives()
        {
            var Idparceiro = _currentUserAccessor.GetCurrentParcID();
            return await _session.Query<T>().Where(u => u.Parceiro.Equals(Idparceiro) && u.Ativo.Equals('I')).ToListAsync();
        }

        public void Dispose()
        {
            if (_session.Transaction.WasCommitted) _session.Transaction.Dispose();
        }
    }
}
