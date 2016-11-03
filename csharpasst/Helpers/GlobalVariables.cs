using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using csharpasst.Models;
using System.Security.Cryptography;
using System.Text;

namespace csharpasst.Helpers
{
    public class GlobalVariables
    {
        public static Owner loggedInUser = null;
        public static string starting = "login.aspx";
        public static string GetIPAddress()
        {
            return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        static public string md5(string data)
        {
            //create new instance of md5
            MD5 md5 = MD5.Create();

            //convert the input text to array of bytes
            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(data));

            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();

            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            // return hexadecimal string
            return returnValue.ToString();

        }
    }

}