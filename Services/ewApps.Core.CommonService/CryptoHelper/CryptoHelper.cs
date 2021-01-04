using System;
using System.IO;
using System.Security.Cryptography;

namespace ewApps.Core.CommonService {
  public class CryptoHelper
  {
    /// <summary>
    /// This class encrypts a string or decrypts an encrypted string.
    /// It implements two encryption algorithms - 3DES and AES.
    /// It uses predefined Key and IV for each algorithm.
    /// 
    /// Some Web references:
    /// Used code from 
    ///     https://www.c-sharpcorner.com/article/tripledes-encryption-in-c-sharp/
    ///     https://www.c-sharpcorner.com/article/aes-encryption-in-c-sharp/
    /// Other methods of encrytion/decryption:
    ///     https://dotnetcodr.com/2015/10/23/encrypt-and-decrypt-plain-string-with-triple-des-in-c/
    ///     https://www.selamigungor.com/post/7/encrypt-decrypt-a-string-in-csharp
    /// Tutorial:
    ///      https://www.tutorialspoint.com/cryptography/index.htm
    ///  DataProtectionService:  
    ///      https://stackoverflow.com/questions/38795103/encrypt-string-in-net-core    /// </summary>

    public enum EncryptionAlgorithm { TripleDES, AES }

    private static readonly byte[] _key_3DES = { 92, 58, 225, 249, 163, 14, 194, 1, 3, 99, 224, 66, 244, 97, 99, 128, 213, 99, 52, 185, 94, 126, 174, 48 };
    private static readonly byte[] _iv_3DES = { 32, 8, 225, 190, 224, 231, 199, 60 };

    private static readonly byte[] _key_AES = { 214, 51, 100, 230, 115, 182, 146, 124, 152, 170, 154, 6, 236, 156, 94, 171, 6, 186, 27, 167, 114, 158, 216, 42, 170, 58, 61, 58, 214, 98, 138, 107 };
    private static readonly byte[] _iv_AES = { 44, 216, 85, 174, 4, 209, 159, 63, 139, 145, 71, 12, 29, 126, 58, 126 };

    /// <summary>
    /// Public constructor. Does nothing.
    /// </summary>
    public CryptoHelper() { }

    /// <summary>
    /// Encrypt a given string (plaintext)
    /// </summary>
    /// <param name="plaintext">string to be encryped</param>
    /// <param name="algorithm">Either TripleDES or AES</param>
    /// <returns>Encrypted result as a Base64 string</returns>
    public string Encrypt(string plaintext, EncryptionAlgorithm algorithm)
    {
      // Resulting encrypted byte array
      byte[] encrypted = null;

      // Go to appropriate algorithm implementation
      if (algorithm == EncryptionAlgorithm.TripleDES)
      {
        encrypted = Encrypt_3DES(plaintext, _key_3DES, _iv_3DES);
      }
      else if (algorithm == EncryptionAlgorithm.AES)
      {
        encrypted = Encrypt_AES(plaintext, _key_AES, _iv_AES);
      }

      // Convert encrypted byte array to Base64 string
      string ciphertext = null;
      if (encrypted != null)
        ciphertext = Convert.ToBase64String(encrypted);

      return ciphertext;
    }

    /// <summary>
    /// Descrypt an encrypted string (As a Base64 string)
    /// </summary>
    /// <param name="ciphertext">Encrypted byte array as a Base64 string</param>
    /// <param name="algorithm">TripleDES or AES</param>
    /// <returns>Original unencrypted string</returns>
    public string Decrypt(string ciphertext, EncryptionAlgorithm algorithm)
    {
      if (ciphertext == null)
        return null;

      // Result string
      string plaintext = null;
      // Convert ciphertext to byte[]
      byte[] encoded = Convert.FromBase64String(ciphertext);

      // Go to appropriate algorithm implementation
      if (algorithm == EncryptionAlgorithm.TripleDES)
      {
        plaintext = Decrypt_3DES(encoded, _key_3DES, _iv_3DES);
      }
      else if (algorithm == EncryptionAlgorithm.AES)
      {
        plaintext = Decrypt_AES(encoded, _key_AES, _iv_AES);
      }

      return plaintext;
    }          
    private byte[] Encrypt_3DES(string plaintext, byte[] key, byte[] iv)
    {
      byte[] encrypted;

      // Create a new TripleDESCryptoServiceProvider.  
      using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
      {
        // Create encryptor  
        ICryptoTransform encryptor = tdes.CreateEncryptor(key, iv);

        // Create MemoryStream  
        using (MemoryStream ms = new MemoryStream())
        {
          // Create crypto stream using the CryptoStream class. This class is the key to encryption  
          // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream  
          // to encrypt  
          using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
          {
            // Create StreamWriter and write data to a stream  
            using (StreamWriter sw = new StreamWriter(cs))
              sw.Write(plaintext);
            encrypted = ms.ToArray();
          }
        }
      }

      // Return encrypted data  
      return encrypted;
    }

    private string Decrypt_3DES(byte[] ciphertext, byte[] key, byte[] iv)
    {
      string plaintext = null;

      // Create TripleDESCryptoServiceProvider  
      using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
      {
        // Create a decryptor  
        ICryptoTransform decryptor = tdes.CreateDecryptor(key, iv);

        // Create the streams used for decryption.  
        using (MemoryStream ms = new MemoryStream(ciphertext))
        {
          // Create crypto stream  
          using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
          {
            // Read crypto stream  
            using (StreamReader reader = new StreamReader(cs))
              plaintext = reader.ReadToEnd();
          }
        }
      }
      return plaintext;
    }

    private byte[] Encrypt_AES(string plaintext, byte[] key, byte[] iv)
    {
      byte[] encrypted;

      // Create a new AesManaged.    
      using (AesManaged aes = new AesManaged())
      {
        // Create encryptor    
        ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

        // Create MemoryStream    
        using (MemoryStream ms = new MemoryStream())
        {
          // Create crypto stream using the CryptoStream class. This class is the key to encryption    
          // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
          // to encrypt    
          using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
          {
            // Create StreamWriter and write data to a stream    
            using (StreamWriter sw = new StreamWriter(cs))
              sw.Write(plaintext);
            encrypted = ms.ToArray();
          }
        }
      }

      // Return encrypted data    
      return encrypted;
    }
    private string Decrypt_AES(byte[] ciphertext, byte[] key, byte[] iv)
    {
      string plaintext = null;

      // Create AesManaged    
      using (AesManaged aes = new AesManaged())
      {
        // Create a decryptor    
        ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);

        // Create the streams used for decryption.    
        using (MemoryStream ms = new MemoryStream(ciphertext))
        {
          // Create crypto stream    
          using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
          {
            // Read crypto stream    
            using (StreamReader reader = new StreamReader(cs))
              plaintext = reader.ReadToEnd();
          }
        }
      }

      return plaintext;
    }
  }
}
