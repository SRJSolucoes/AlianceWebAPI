using Domain.DTOs;
using Domain.Entidades;
using System;

namespace AcessoWebApi.Infrastructure.Security
{
    public interface ICurrentUserAccessor
    {
        UsuarioO2Si GetCurrentUser();
        string GetCurrentUserEmail();
        Guid GetCurrentUserID();
        Guid GetCurrentParcID();
        string GetCurrentUserRole();
        MXMLoginDTO GetMXMLoginFromRequestBody();
        MXMLoginDTO GetMXMLoginFromRequestHeaderBasic();
    }
}
