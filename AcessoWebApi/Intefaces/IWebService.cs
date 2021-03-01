using PadraoWebApi.XML;
using System;

namespace PadraoWebApi.Interfaces
{
    public interface IWebService
    {
        TUserProcessToken TUserProcessTokenValue { get; set; }
        String Url { get; set; }
    }
}
