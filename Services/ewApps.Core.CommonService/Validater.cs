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
using System.Text;
using System.Text.RegularExpressions;

namespace ewApps.Core.CommonService
{
  // <summary>
  /// This class implements a method to check email.
  /// </summary>            
  public class Validater
  {

    #region public methods 
    /// <summary>
    /// Method to validate the input email.
    /// </summary>
    /// <param name="inputEmail">The email to be validated.</param>
    /// <returns>A boolean if the email is valid or not</returns>
    public static bool ValidateEmail(string inputEmail) {

      //check login email contain these character or not if contain then return false else true.
      if (inputEmail.Contains("*") || inputEmail.Contains("#") || inputEmail.Contains("/") || inputEmail.Contains("&") || inputEmail.Contains("%")) {
        return false;
      }
      //The regular expression of email.           
      string regEx = @"(?:[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*|" + '"' + @"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*" + '"' + @")@(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-zA-Z0-9-]*[a-zA-Z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";

      //Checking if the email is valid or not.
      bool isEmail = Regex.IsMatch(inputEmail, regEx, RegexOptions.Singleline);

      //Return true if email is valid else return false.
      return isEmail;
    }


    public static bool ValidateEmailDomain(string email) {
      //try
      //{
      string domainPart = email.Substring(email.IndexOf("@") + 1);
      System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(domainPart);

      return true;
      //}
      //catch (Exception ex)
      //{
      //  if (ex.Message == "The requested name is valid, but no data of the requested type was found")
      //  {
      //    return true;
      //  }
      //  return false;
      //}
    } 
    #endregion
  }

}
