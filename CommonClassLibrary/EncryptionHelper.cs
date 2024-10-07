using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace CommonClassLibrary
{
    public static class EncryptionHelper
    {

        // Encyption key
        private static readonly string EncryptionKey = "@@@522ef3e7-7036-4473%-b#_d4e-c31c8@$e4e0-cob7@#ba$-dcozasaa01@";

        public static string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x43, 0x87, 0x23, 0x72, 0x34, 0x11, 0x55, 0x90 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Encrypt_UrlSafe(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x43, 0x87, 0x23, 0x72, 0x34, 0x11, 0x55, 0x90 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());

                    // Make Base64 URL-safe by replacing characters
                    clearText = clearText.Replace("+", "-").Replace("/", "_").Replace("=", "");
                }
            }
            return clearText;
        }




        public static string Decrypt(Page page, string cipherText)
        {
            try
            {
                cipherText = cipherText.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x43, 0x87, 0x23, 0x72, 0x34, 0x11, 0x55, 0x90 });
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
            catch (FormatException ex)
            {
                SweetAlert.GetSweet(page, "warning", "Invalid ID Foarmat", $"{ex.Message}");
            }
            catch (CryptographicException ex)
            {
                SweetAlert.GetSweet(page, "warning", "Decryption Failed", $"{ex.Message}");
            }
            catch (Exception ex)
            {
                SweetAlert.GetSweet(page, "warning", "Decryption Exception", $"{ex.Message}");
            }
            finally
            {

            }

            return null;
        }

        public static string Decrypt_UrlSafe(Page page, string cipherText)
        {
            try
            {
                // Revert the URL-safe characters back to their original Base64 form
                cipherText = cipherText.Replace("-", "+").Replace("_", "/");

                // Add padding if necessary
                switch (cipherText.Length % 4)
                {
                    case 2: cipherText += "=="; break;
                    case 3: cipherText += "="; break;
                }

                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x43, 0x87, 0x23, 0x72, 0x34, 0x11, 0x55, 0x90 });
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
            catch (FormatException ex)
            {
                SweetAlert.GetSweet(page, "warning", "Invalid ID Format", $"{ex.Message}");
            }
            catch (CryptographicException ex)
            {
                SweetAlert.GetSweet(page, "warning", "Decryption Failed", $"{ex.Message}");
            }
            catch (Exception ex)
            {
                SweetAlert.GetSweet(page, "warning", "Decryption Exception", $"{ex.Message}");
            }

            return null;
        }





        // How To Use ?

        //string projectId = "12345";
        //string encryptedId = EncryptionHelper.Encrypt(projectId);
        //string url = $"CreateProject.aspx?ID={HttpUtility.UrlEncode(encryptedId)}";
        //Response.Redirect(url);

        //string encryptedId = Request.QueryString["ID"];
        //if (!string.IsNullOrEmpty(encryptedId)) // update page mode
        //{
        //   // decrypting encrypted request refid from URL
        //   string RefID = EncryptionHelper.Decrypt(this.Page, HttpUtility.UrlDecode(encryptedId));
        //   if (!string.IsNullOrEmpty(RefID))
        //   {

        //   }
        //}
        //else // fresh page mode
        //{

        //}


    }
}
