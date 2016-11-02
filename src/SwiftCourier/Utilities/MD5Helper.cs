using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCourier.Utilities
{
    public class MD5Helper
    {
        public static string Encode(string value)
        {
            var hasher = System.Security.Cryptography.MD5.Create();
            var bytes = new UTF8Encoding().GetBytes(value);
            var hashedPassword = hasher.ComputeHash(bytes);
            string encodedPassword = BitConverter.ToString(hashedPassword);

            return encodedPassword;
        }
    }
}
