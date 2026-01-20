using DevExpress.Web.Office.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace eSTS.Common
{
    public enum FAction
    {
        Submit = 0,
        Pending = 1,
        Reject = 2,
        Approve = 3

    }
    public enum ECCLlvl
    {
        L = 0,
        M = 1,
        Q = 2,
        H = 3
    }
    public class LPJSecurity
    {
        public static string GetEncrypt(string input)
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
    }
}