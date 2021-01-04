using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.AppDeeplinkService {

  /// <summary>
  /// Contains the method to  convert the string.
  /// </summary>
  public class AppDeeplinkShortUrlKeyGenerator {

    #region Public Methods

    /// <summary>
    /// To generate a short url from intger. 
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns> 
    /// <remarks>Referense: https://www.geeksforgeeks.org/how-to-design-a-tiny-url-or-url-shortener/</remarks>
    public static string NumberToShortURL(long n) {
      // Map to store 62 possible characters 
      char[] map = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

      //string shorturl;
      ArrayList shorturl = new ArrayList();

      // Convert given integer id to a base 62 number 
      while (n > 0) {
        // use above map to store actual character 
        // in short url 
        shorturl.Add(map[n % 62]);
        n = n / 62;
      }

      // Reverse shortURL to complete base conversion 
      shorturl.Reverse();
      string result = "";
      //Asha - Why this Foreach loop???
      foreach (var str in shorturl)
        result += str.ToString();
      return result;
    }

    /// <summary>
    ///  Get integer value back from a short url.
    /// </summary>
    /// <param name="shortURL"></param>
    /// <returns></returns>
    public static long ShortURLtoID(string shortURL) {
      long id = 0; // initialize result 

      // A simple base conversion logic 
      for (int i = 0; i < shortURL.Length; i++) {
        if ('a' <= shortURL[i] && shortURL[i] <= 'z')
          id = id * 62 + shortURL[i] - 'a';
        if ('A' <= shortURL[i] && shortURL[i] <= 'Z')
          id = id * 62 + shortURL[i] - 'A' + 26;
        if ('0' <= shortURL[i] && shortURL[i] <= '9')
          id = id * 62 + shortURL[i] - '0' + 52;
      }
      return id;
    }


    #endregion Public Methods

  }
}

/*
 
//Asha - where this method will be used?
    /// <summary>
    /// Method generate the key from actual generated value.
    /// Convert value to base62 and return.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GenerateUrlKey(string value) {
      byte[] byteVal = System.Text.ASCIIEncoding.ASCII.GetBytes(value);
      string strValue = byteVal.ToBase62();

      return strValue;
    }

//Asha - where this method be used?
    /// <summary>
    /// return actual value from base62 url key.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetActualRendomNumberFromUrlKey(string encodedUrlKeyBASE62) {
      byte[] byteVal = encodedUrlKeyBASE62.FromBase62();
      string strValue = System.Text.ASCIIEncoding.ASCII.GetString(byteVal);

      return strValue;
    }
 */
