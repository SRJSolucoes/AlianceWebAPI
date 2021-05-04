using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.VO
{
    public class WithLoginVO<T>
    {
        // TODO Mechi aqui para ajustar o Login
        //public LoginVO Login { get; set; }
        public string Token { get; set; }

        public T Dados { get; set; }
    }
}
