using System.Security.Cryptography;
using System.Web;
using System;

namespace FalzoniNetFCSharp.Utils.Helpers
{
    public static class RandomOAuthStateGeneratorHelper
    {
        public static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

        public static string Generate(int strengthInBits)
        {
            const int bitsPerByte = 8;

            if (strengthInBits % bitsPerByte != 0)
            {
                throw new ArgumentException("strengthInBits deve ser divisível por 8.", "strengthInBits");
            }

            int strengthInBytes = strengthInBits / bitsPerByte;

            byte[] data = new byte[strengthInBytes];
            _random.GetBytes(data);
            return HttpServerUtility.UrlTokenEncode(data);
        }
    }
}
