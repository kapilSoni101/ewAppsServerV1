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

namespace ewApps.Core.CommonService
{/// <summary>
 /// This class provides the helper method to work on mask value.
 /// </summary>
  public static class FlagEnumHelper
  {

    #region public methods 
    /// <summary>
    /// Determines whether the specified flag item is set in masked flag value.
    /// </summary>
    /// <typeparam name="T">The type of Flag datatype.</typeparam>
    /// <param name="flags">The masked flag value.</param>
    /// <param name="flag">The item to be check in masked value.</param>
    /// <returns>Returns true if specified value is set in masked flag value.</returns>
    public static bool IsSet<T>(T flags, T flag) where T : struct {
      int flagsValue = (int)(object)flags;
      int flagValue = (int)(object)flag;

      return ((flagsValue & flagValue) == flagValue);
    }

    /// <summary>
    /// Sets the specified item in masked flag value.
    /// </summary>
    /// <typeparam name="T">The type of Flag datatype.</typeparam>
    /// <param name="flags">The masked flag value.</param>
    /// <param name="flag">The item to be set in masked input value.</param>
    public static void Set<T>(ref T flags, T flag) where T : struct {
      int flagsValue = (int)(object)flags;
      int flagValue = (int)(object)flag;

      flags = (T)(object)(flagsValue | flagValue);
    }

    /// <summary>
    /// Unsets the specified item in masked flag value
    /// </summary>
    /// <typeparam name="T">The type of Flag datatype.</typeparam>
    /// <param name="flags">The masked flag value.</param>
    /// <param name="flag">The item to be unset in masked input value.</param>
    public static void Unset<T>(ref T flags, T flag) where T : struct {
      int flagsValue = (int)(object)flags;
      int flagValue = (int)(object)flag;

      flags = (T)(object)(flagsValue & (~flagValue));
    } 
    #endregion
  }
}
