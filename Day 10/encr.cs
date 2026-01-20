using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

// Simple AES encryption/decryption example.
// Output format (Base64): [16-byte IV][ciphertext...]

public static class Program
{
    public static void Main()
    {
        string message = "Hello, this is secret!";
        string password = "StrongPassword123!!";

        string encrypted = Encrypt(message, password);
        Console.WriteLine("Encrypted (Base64):");
        Console.WriteLine(encrypted);

        string decrypted = Decrypt(encrypted, password);
        Console.WriteLine();
        Console.WriteLine("Decrypted:");
        Console.WriteLine(decrypted);
    }

    public static string Encrypt(string plaintext, string password)
    {
        // For simplicity: key = SHA256(password). (Not as strong as PBKDF2/Argon2.)
        byte[] key = SHA256.HashData(Encoding.UTF8.GetBytes(password)); // 32 bytes
        byte[] iv = RandomNumberGenerator.GetBytes(16);                // AES block size

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var ms = new MemoryStream();
        ms.Write(iv, 0, iv.Length); // prefix IV

        using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
        using (var sw = new StreamWriter(cs, Encoding.UTF8))
        {
            sw.Write(plaintext);
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    public static string Decrypt(string base64, string password)
    {
        byte[] data = Convert.FromBase64String(base64);
        if (data.Length < 16) throw new ArgumentException("Invalid encrypted data.", nameof(base64));

        byte[] key = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        byte[] iv = new byte[16];
        Buffer.BlockCopy(data, 0, iv, 0, 16);

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var ms = new MemoryStream(data, 16, data.Length - 16);
        using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using var sr = new StreamReader(cs, Encoding.UTF8);
        return sr.ReadToEnd();
    }
}

