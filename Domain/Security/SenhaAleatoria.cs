using System;

namespace Domain.Security
{
    public static class SenhaAleatoria
    {
        public static String GetSenhaAleatoria()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
        }
    }
}

