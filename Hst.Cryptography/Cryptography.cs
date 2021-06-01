using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Hst.Cryptography
{
    public class Cryptography
    {
        public static string Encrypt(string plainText, string securityKey) 
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;

            Byte[] iVector = new Byte[] { 27, 9, 45, 27, 0, 72, 171, 54 };
            Byte[] buffer = Encoding.ASCII.GetBytes(plainText);

            TripleDESCryptoServiceProvider 
                tripleDES = new TripleDESCryptoServiceProvider();

            MD5CryptoServiceProvider 
                mD5 = new MD5CryptoServiceProvider();

            tripleDES.Key = mD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(securityKey));
            tripleDES.IV = iVector;

            ICryptoTransform cryptoTransform = tripleDES.CreateEncryptor();

            return Convert.ToBase64String(cryptoTransform.TransformFinalBlock(buffer, 0, buffer.Length));


        }

        public static string Decrypt(string ciperText, string securityKey)
        {
            if (string.IsNullOrEmpty(ciperText))
                return string.Empty;

            Byte[] iVector = new Byte[] { 27, 9, 45, 27, 0, 72, 171, 54 };
            Byte[] buffer = Encoding.ASCII.GetBytes(ciperText);

            TripleDESCryptoServiceProvider
                tripleDES = new TripleDESCryptoServiceProvider();

            MD5CryptoServiceProvider
                mD5 = new MD5CryptoServiceProvider();

            tripleDES.Key = mD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(securityKey));
            tripleDES.IV = iVector;

            ICryptoTransform cryptoTransform = tripleDES.CreateEncryptor();
            return Encoding.ASCII.GetString(cryptoTransform.TransformFinalBlock(buffer, 0, buffer.Length));
        }




    }
}
