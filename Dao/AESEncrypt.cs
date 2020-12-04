using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

///===============================================================
/// <summary>
/// FileName       : AESEncrypt.cs
/// Description    : 암/복호화 클래스 (AES-256)
/// Copyright      : 2020 by PayLetter Inc. All rights reserved.
/// Author         : mh_kim@payletter.com, 2020-11-23
/// Modify History : Just Created.
/// </summary>
///================================================================
namespace PGproject.Dao
{
    public sealed class AESEncrypt
    {
        private string EncryptionKey = string.Empty;

        /// -------------------------------------------------------
        /// <summary>
        /// 비밀번호 암호화
        /// </summary>
        /// <returns> plainText : plain text password</returns>
        /// -------------------------------------------------------
        public string Encrypt(string plainText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(plainText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    plainText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return plainText;
        }

        /// -------------------------------------------------------
        /// <summary>
        /// 비밀번호 복호화
        /// </summary>
        /// <params>cipherText : 암호화된 password</params>
        /// -------------------------------------------------------
        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}