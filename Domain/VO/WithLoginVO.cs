using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.VO
{
    public class WithLoginVO<T>
    {
        public LoginVO Login { get; set; }

        public T Dados { get; set; }
    }
}
