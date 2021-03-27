using PadraoWebApi.VOs;
using System;
using System.Collections.Generic;

namespace PadraoWebApi.Interfaces
{
    public interface IWebServiceReturn
    {
        void ProcessWebServiceReturn(String xmlReturn);
        List<WebServiceRegisterVO> Registers { get; set; }
    }
}
