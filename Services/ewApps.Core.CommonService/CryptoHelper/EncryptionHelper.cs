/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul badgujar <abadgujar@batchmaster.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ewApps.Core.CommonService
{
  /// <summary>
  /// This class provides encryption/decription methods based on one fixed key.
  /// </summary>
  public static class EncryptionHelper
  {

    #region member variable 
    private const string key = "ewApps-DataEncript-Key";
    #endregion

    #region public methods 
    /// <summary>
    /// Encrypts the specified clear text.
    /// </summary>
    /// <param name="clearText">The clear text.</param>
    /// <returns>Returns encrypted string.</returns>
    public static string Encrypt(string clearText) {
      string EncryptionKey = key;
      byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
      using (Aes encryptor = Aes.Create()) {
        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);
        using (MemoryStream ms = new MemoryStream()) {
          using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)) {
            cs.Write(clearBytes, 0, clearBytes.Length);
            cs.Close();
          }
          clearText = Convert.ToBase64String(ms.ToArray());
        }
      }
      return clearText;
    }

    /// <summary>
    /// Decrypts the specified cipher text.
    /// </summary>
    /// <param name="cipherText">The cipher text.</param>
    /// <returns>Returns decrypted plain text.</returns>
    public static string Decrypt(string cipherText) {
      string EncryptionKey = key;
      cipherText = cipherText.Replace(" ", "+");
      byte[] cipherBytes = Convert.FromBase64String(cipherText);
      using (Aes encryptor = Aes.Create()) {
        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);
        using (MemoryStream ms = new MemoryStream()) {
          using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)) {
            cs.Write(cipherBytes, 0, cipherBytes.Length);
            cs.Close();
          }
          cipherText = Encoding.Unicode.GetString(ms.ToArray());
        }
      }
      return cipherText;
    } 
    #endregion

  }
}
