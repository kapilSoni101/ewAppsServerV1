using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.AppDeeplinkService {

  /// <summary>
  /// AppDeeplinkNumberIdGenerator is a static method.
  /// Class contains static method to generate the rendom number.
  /// </summary>
  public class AppDeeplinkNumberIdGenerator {

    #region Variables

    /// <summary>
    /// Static variable to avoid getting the same value lots of times. 
    /// So keep a single Random instance and keep using Next on the same instance..
    /// </summary>
    private static readonly Random random = new Random();

    #endregion Variables

    #region Public Methods

    /// <summary>
    /// Method generate the 8 charecter long number and return as string.
    /// </summary>
    /// <returns></returns>
    public static long GenerateRendomNumber() {
      string strValue = "";
      /// Generating 8 charecter long number.
      for (int i = 0; i < 8; i++) {
        // Get random number.
        strValue += AppDeeplinkNumberIdGenerator.random.Next(0, 9).ToString();
      }

      return Convert.ToInt64(strValue);
    }

    /// <summary>
    /// Method generate the rendom string using datetime.
    /// </summary>
    /// <returns></returns>
    public static string GenerateRendomNumberUsingDatetime() {
      string strValue = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

      return strValue;
    }

    #endregion Public Methods

  }
}
