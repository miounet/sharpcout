using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Core.Comm
{
    /// <summary>
    /// 平衡加解密区域
    /// </summary>
    public class Security
    {
        /// <summary>
        /// Decrypt key
        /// </summary>
        const string Decrypt_string = "jmsrb";
 
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="dataValue">被加密源字符串</param>
        /// <returns>加密后字符串</returns>
        public static String EncryptCommon(String dataValue)
        {
            return EncryptCommon(dataValue, Decrypt_string);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="keyPwd">密钥</param>
        /// <param name="dataValue">被加密源字符串</param>
        /// <returns>加密后字符串</returns>
        public static String EncryptCommon(String dataValue, string kEY)
        {

            String keyPwd = kEY;
            try
            {
                using (DESCryptoServiceProvider desc = new DESCryptoServiceProvider()) //des进行加密
                {
                    PasswordDeriveBytes db = new PasswordDeriveBytes(keyPwd, null);//产生Provider key
                    byte[] key = db.GetBytes(8);
                    using (MemoryStream ms = new MemoryStream())//存储加密后的数据
                    {
                        using (CryptoStream cs
                            = new CryptoStream(ms, desc.CreateEncryptor(key, key), CryptoStreamMode.Write))
                        {
                            byte[] data = Encoding.Unicode.GetBytes(dataValue);//取到密码的字节流
                            cs.Write(data, 0, data.Length);//进行加密
                            cs.FlushFinalBlock();
                            byte[] res = ms.ToArray();//取加密后的数据
                            return Convert.ToBase64String(res);//转换到字符串返回
                        }
                    }
                }
            }
            catch
            {
                return dataValue;
                //throw new Exception("EncryptCommon Failed) " + e.Message);
                //return null;
            }
        }


        /// <summary>
        /// 数据表中数据的解密
        /// </summary>
        /// <param name="dataValue">值</param>
        /// <returns>解密后的结果</returns>
        public static String DecryptCommon(String dataValue)
        {
            return DecryptCommon(dataValue, Decrypt_string);
        }

        /// <summary>
        ///  数据表中数据的解密
        /// </summary>
        /// <param name="dataValue">值</param>
        /// <param name="kEY">key</param>
        /// <returns>解密后的密码</returns>
        public static String DecryptCommon(String dataValue, string kEY)
        {
            try
            {
                using (DESCryptoServiceProvider desc = new DESCryptoServiceProvider())
                {
                    PasswordDeriveBytes db = new PasswordDeriveBytes(kEY, null);//产生key
                    byte[] key = db.GetBytes(8);
                    using (MemoryStream ms = new MemoryStream())  //存储解密后的数据
                    {
                        using (CryptoStream cs = new CryptoStream(ms, desc.CreateDecryptor(key, key), CryptoStreamMode.Write))
                        {
                            byte[] databytes = Convert.FromBase64String(dataValue);//Encoding.Unicode.GetBytes(dataValue);//取到加密后的数据的字节流

                            cs.Write(databytes, 0, databytes.Length);//解密数据
                            cs.FlushFinalBlock();
                            byte[] res = ms.ToArray();
                            return Encoding.Unicode.GetString(res);//返回解密后的数据
                        }
                    }
                }
            }
            catch
            {
                return dataValue;
                //throw e;//为null的话，证明输入错误。
            }
        }

        /// <summary>
        /// 得到md5加密后的字符串
        /// </summary>
        /// <param name="sDataIn">要加密的数据</param>
        /// <returns>加密后的前缀</returns>
        public static string GetServerU_MD5(string sDataIn)
        {
            Random rd = new Random();
            char[] c = new char[2];

            // a - z
            c[0] = (char)rd.Next(97, 122);
            c[1] = (char)rd.Next(97, 122);
            string move = new string(c);


            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytValue, bytHash;
            bytValue = System.Text.Encoding.UTF8.GetBytes(move + sDataIn);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            StringBuilder sTemp = new StringBuilder();
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp.Append(bytHash[i].ToString("X").PadLeft(2, '0'));
            }
            return move + sTemp.ToString();
        }
        /// <summary>
        /// 得到md5加密后的字符串
        /// </summary>
        /// <param name="sDataIn">要加密的数据</param>
        /// <returns>加密后的前缀</returns>
        public static string GetMD5(string sDataIn)
        {
           

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytValue, bytHash;
            bytValue = System.Text.Encoding.UTF8.GetBytes(sDataIn);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            StringBuilder sTemp = new StringBuilder();
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp.Append(bytHash[i].ToString("X").PadLeft(2, '0'));
            }
            return   sTemp.ToString();
        }

    }
}
