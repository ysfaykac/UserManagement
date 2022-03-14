using System.Security.Cryptography;
using System.Text;

namespace UserService.Application.Encryption;

public static class EncryptionHelper
{
    public static string Encrypt(string plainText, string password)
    {
        if (string.IsNullOrEmpty(plainText))
        {
            return string.Empty;
        }

        if (string.IsNullOrEmpty(password))
        {
            password = string.Empty;
        }

        var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

        var bytesEncrypted = Encrypt(bytesToBeEncrypted, passwordBytes);

        return Convert.ToBase64String(bytesEncrypted);
    }
    public static string Decrypt(string encryptedText, string password)
    {
        try
        {
            if (string.IsNullOrEmpty(encryptedText))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(password))
            {
                password = string.Empty;
            }

            var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesDecrypted = Decrypt(bytesToBeDecrypted, passwordBytes);

            return Encoding.UTF8.GetString(bytesDecrypted);
        }
        catch
        {
            return null;
        }
    }
    private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
    {
        byte[] encryptedBytes = null;

      
        var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        using (MemoryStream ms = new MemoryStream())
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                AES.KeySize = 256;
                AES.BlockSize = 128;
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);

                AES.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                    cs.Close();
                }

                encryptedBytes = ms.ToArray();
            }
        }

        return encryptedBytes;
    }
    private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
    {
        byte[] decryptedBytes = null;

     
        var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        using (MemoryStream ms = new MemoryStream())
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                AES.KeySize = 256;
                AES.BlockSize = 128;
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);
                AES.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                    cs.Close();
                }

                decryptedBytes = ms.ToArray();
            }
        }

        return decryptedBytes;
    }
    public static string Md5SifrelemeString(string sText)
    {
        byte[] md5Sifre = Md5Sifreleme(sText);
        string md5String = "";
        foreach (byte b in md5Sifre)
        {
            md5String += b.ToString();
        }
        return md5String;
    }
    public static byte[] Md5Sifreleme(string sText)
    {
        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] md5Sifre = md5.ComputeHash(encoder.GetBytes(sText));
        return md5Sifre;
    }

}