using Domain.Entidades;
using Domain.VO;
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
        LoginVO GetMXMLoginFromRequestBody();
        LoginVO GetMXMLoginFromRequestHeaderBasic();
    }
}
