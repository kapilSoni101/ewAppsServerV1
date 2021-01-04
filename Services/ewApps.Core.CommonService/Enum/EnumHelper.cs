﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
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

namespace ewApps.Core.CommonService
{
  /// <summary>
  /// This class provides utility methods for enum functionality.
  /// </summary>
  public class EnumHelper
  {

    #region public methods 
    /// <summary> 
    /// This method converts enum string value to given enum type.
    /// If value does not falls in enum type it returns default value.
    /// </summary>
    /// <typeparam name="TEnum">Enum type.</typeparam>
    /// <param name="strEnumValue">String enum value.</param>
    /// <param name="defaultValue">Default value of enum if string not found in enum.</param>
    /// <returns>Enum value.</returns>
    public static TEnum ToEnum<TEnum>(string strEnumValue, TEnum defaultValue) {
      if (string.IsNullOrEmpty(strEnumValue) || !Enum.IsDefined(typeof(TEnum), strEnumValue))
        return defaultValue;
      return (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
    }

    /// <summary> 
    /// This method converts enum string value to given enum type.
    /// If value does not falls in enum type it returns default value.
    /// </summary>
    /// <typeparam name="TEnum">Enum type.</typeparam>
    /// <param name="strEnumValue">Int enum value.</param>
    /// <param name="defaultValue">Default value of enum if string not found in enum.</param>
    /// <returns>Enum value.</returns>
    public static TEnum ToEnum<TEnum>(int strEnumValue, TEnum defaultValue) {
      if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
        return defaultValue;
      return (TEnum)Enum.Parse(typeof(TEnum), Convert.ToString(strEnumValue));
    }

    /// <summary> 
    /// This method converts enum string value to given enum type.
    /// </summary>
    /// <typeparam name="TEnum">Enum type.</typeparam>
    /// <param name="strEnumValue">String enum value.</param>
    /// <returns>Enum value.</returns>
    public static TEnum ToEnum<TEnum>(string strEnumValue) {
      return (TEnum)Enum.Parse(typeof(TEnum), (string)strEnumValue);
    } 
    #endregion


  }
}
