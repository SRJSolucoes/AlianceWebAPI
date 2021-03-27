using System;
using System.Collections.Generic;

namespace PadraoWebApi.VOs
{
    public class WebServiceRegisterVO
    {
        public Int64 Sequence { get; set; }
        public String Type { get; set; }
        public List<WebServiceFieldVO> Fields { get; set; }

        public WebServiceRegisterVO()
        {
            Fields = new List<WebServiceFieldVO>();
        }
    }
}
