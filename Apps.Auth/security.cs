using System;
using System.Security.Cryptography;
using System.Text;

namespace Apps.Auth
{
    public static class security
    {
        public static string Encrypt(string input)
        {
            try
            {
                return security.GetEncrypt(input);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool VerifyEncrypt(string input, string hash)
        {
            try
            {
                // Hash the input. 
                string hashOfInput = security.GetEncrypt(input);

                // Create a StringComparer an compare the hashes.
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                if (0 == comparer.Compare(hashOfInput, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string GetEncrypt(string input)
        {
            // Create a new Stringbuilder to collect the bytes and create a string.
            StringBuilder sBuilder = new StringBuilder();
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash. 
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Loop through each byte of the hashed data and format each one as a hexadecimal string. 
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }
        public static string GetEncrypt2(string input)
        {
            EncryptDecrypt.cTripleDES enc = new EncryptDecrypt.cTripleDES();

            return enc.Encrypt(input);
        }
    }
}

