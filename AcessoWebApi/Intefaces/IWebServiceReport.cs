using System;

namespace PadraoWebApi.Interfaces
{
    public interface IWebServiceReport: IWebService
    {
        String ExecutaProcesso(String xml);
    }
}
