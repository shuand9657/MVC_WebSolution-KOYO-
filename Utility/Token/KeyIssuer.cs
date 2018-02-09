using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class KeyIssuer
    {
        public static string GenerateSharedSymmetriKey()
        {
            //generate base key
            using (var provider = new RNGCryptoServiceProvider())
            {
                byte[] byteKey = new byte[32];
                provider.GetBytes(byteKey);
                return Convert.ToBase64String(byteKey);
            }
        }
    }
}
