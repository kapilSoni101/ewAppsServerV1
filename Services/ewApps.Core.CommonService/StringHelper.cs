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
using System.Globalization;

namespace ewApps.Core.CommonService
{

  /// <summary>
  /// Provides  StringHelper
  /// </summary>
  public static class StringHelper
  {

    #region public methods 
    /// <summary>
    /// Splits the full name of the name into first name and last.
    /// </summary>
    /// <param name="fullName">The full name.</param>
    /// <returns></returns>
    public static Tuple<string, string> GetFirstNameAndLastName(string fullName) {
      Tuple<string, string> name = new Tuple<string, string>("", "");
      // Spilit FullName into First and Last Name.
      if (fullName.IndexOf(" ") > 0) {
        int firstSpaceIndex = fullName.IndexOf(" ");
        string firstName = fullName.Substring(0, firstSpaceIndex).Trim();
        string lastName = fullName.Substring(firstSpaceIndex + 1, fullName.Length - firstSpaceIndex - 1).Trim();
        name = new Tuple<string, string>(firstName, lastName);
      }
      return name;
    }

    /// <summary>
    /// Spilits the string  and concatenate.
    /// </summary>
    /// <param name="inputString">The input string.</param>
    /// <param name="seperator">The seperator character array.</param>
    /// <param name="option">The string split option.</param>
    /// <param name="jointSeperator">The joint seperator to join seperated string array.</param>
    /// <returns>
    /// Returns a string seperated with provided seperator and joint with provided join character.
    /// </returns>
    public static string SpilitAndConcatenate(this string inputString, char[] seperator, StringSplitOptions option, string jointSeperator) {
      string[] addressArray = inputString.Split(seperator, option);
      return string.Join(jointSeperator, addressArray);
    }

    public static string GetInitials(string firstName, string lastName) {
      string initials = string.Empty;

      if (string.IsNullOrEmpty(firstName) == false) {
        //Int32[] firstNameElemIndex = StringInfo.ParseCombiningCharacters(FirstName);SS

        TextElementEnumerator charEnumForFirstName = StringInfo.GetTextElementEnumerator(firstName.Trim());
        while (charEnumForFirstName.MoveNext()) {
          initials = charEnumForFirstName.GetTextElement();
          break;
        }
      }

      if (string.IsNullOrEmpty(lastName) == false) {
        //Int32[] lastNameElemIndex = StringInfo.ParseCombiningCharacters(LastName);

        TextElementEnumerator charEnumForLastName = StringInfo.GetTextElementEnumerator(lastName.Trim());
        while (charEnumForLastName.MoveNext()) {
          initials += charEnumForLastName.GetTextElement();
          break;
        }
      }

      return initials;

    } 
    #endregion




  }

  

}

