using System;
using System.Security.Cryptography;
using System.Text;

namespace CamcoTasks.Infrastructure.Defaults;

public static class CryptoEngine
{
    const string DefaultEncryptionValue = "sblw-3hn8-sqoy19";

    public static string Encrypt(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;
        byte[] inputArray = Encoding.UTF8.GetBytes(input);
        TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider();
        tripleDes.Key = Encoding.UTF8.GetBytes(DefaultEncryptionValue);
        tripleDes.Mode = CipherMode.ECB;
        tripleDes.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = tripleDes.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        tripleDes.Clear();
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    public static string Decrypt(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        byte[] inputArray = Convert.FromBase64String(input);
        TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider();
        tripleDes.Key = Encoding.UTF8.GetBytes(DefaultEncryptionValue);
        tripleDes.Mode = CipherMode.ECB;
        tripleDes.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = tripleDes.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        tripleDes.Clear();
        return Encoding.UTF8.GetString(resultArray);
    }

    public static string ToConvertString(this decimal num)
    {
        return $"{num:#,0.##}";
    }
}