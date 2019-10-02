using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace T1807EHello.Entity
{
    class HarshMD5
    {
        public string Harsh(string password, string salt)
        {
            var provider = MD5.Create();
            var bytes = provider.ComputeHash(Encoding.ASCII.GetBytes(salt + password));
            var computedHash = BitConverter.ToString(bytes);
            return computedHash; 
        }
    }
}
