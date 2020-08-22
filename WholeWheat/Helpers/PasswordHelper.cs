using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WholeWheat.Helpers
{
    public static class PasswordHelper
    {
        private static object _lockObj = new object();
        public static String DecryptPassword(string TextFrom)
        {
            lock (_lockObj)
            {
                // Warning!!! Optional parameters not supported
                string encrypted = String.Empty;
                string decrypted = String.Empty;
                string password;

                TripleDESCryptoServiceProvider des;
                MD5CryptoServiceProvider hashmd5;
                byte[] pwdhash;
                byte[] buff;
                password = "ItSeCZaIGhAm610654025810097284009!";
                //password = strPassword;
                try
                {
                    hashmd5 = new MD5CryptoServiceProvider();
                    pwdhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
                    hashmd5 = null;
                    des = new TripleDESCryptoServiceProvider();
                    des.Key = pwdhash;
                    des.Mode = CipherMode.ECB;
                    buff = Convert.FromBase64String(TextFrom);
                    decrypted = ASCIIEncoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buff, 0, buff.Length));
                    des = null;
                    //Decrypt = decrypted;
                }
                catch (Exception ex)
                {
                    string strMessage;
                    strMessage = ex.Message;
                }

                return decrypted;
            }

        }

        public static String EncryptPassword(string TextFrom)
        {
            lock (_lockObj)
            {
                string original;
                string encrypted = String.Empty;
                string decrypted = String.Empty;
                string password;
                TripleDESCryptoServiceProvider des;
                MD5CryptoServiceProvider hashmd5;
                byte[] pwdhash;
                byte[] buff;
                password = "ItSeCZaIGhAm610654025810097284009!";
                //password = strPassword;
                original = TextFrom;
                try
                {
                    hashmd5 = new MD5CryptoServiceProvider();
                    pwdhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
                    hashmd5 = null;
                    des = new TripleDESCryptoServiceProvider();
                    des.Key = pwdhash;
                    des.Mode = CipherMode.ECB;
                    buff = ASCIIEncoding.ASCII.GetBytes(original);
                    encrypted = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buff, 0, buff.Length));
                    des = null;
                    //Encrypt = encrypted;
                }
                catch (Exception ex)
                {
                    string strMessage;
                    strMessage = ex.Message;
                }
                return encrypted;
            }
        }
    }
}