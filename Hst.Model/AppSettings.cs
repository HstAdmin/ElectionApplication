using Hst.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Model
{
    public class AppSettings
    {
        public string Cuture { get; set; }
        public string BaseUrl { get; set; }
        public static string Secret { get; set; }
        public int Expire { get; set; }
        public string ConnectionString { get; set; }
        //public string GetSecret()
        //{
        //    return Cryptography.Cryptography.Decrypt(Secret);
        //}

        //public string GetConnectionString()
        //{
        //    return Cryptography.Cryptography.Decrypt(ConnectionString);
        //}
    }
}
