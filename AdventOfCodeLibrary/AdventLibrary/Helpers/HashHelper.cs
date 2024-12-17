using System;
using System.Security.Cryptography;
using System.Text;

namespace AdventLibrary.Helpers
{
    public static class HashHelper
    {
        public static string GetMd5HashAsHexString(string input)
        {
            using (MD5 _md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = _md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);
            }
        }
    }
}
