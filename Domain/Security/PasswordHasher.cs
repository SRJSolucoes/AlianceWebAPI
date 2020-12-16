using Domain.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private const String PreConfig = "#{0}@O2S1T3cnologia#";
        private static String key = String.Format(PreConfig, "O2Si");
        private readonly HMACSHA512 chave = new HMACSHA512(Encoding.UTF8.GetBytes(key));

        public byte[] Hash(string password, byte[] salt)
        {
            var bytes = Encoding.UTF8.GetBytes(password);

            var allBytes = new byte[bytes.Length + salt.Length];
            Buffer.BlockCopy(bytes, 0, allBytes, 0, bytes.Length);
            Buffer.BlockCopy(salt, 0, allBytes, bytes.Length, salt.Length);

            return chave.ComputeHash(allBytes);
        }
    }
}
