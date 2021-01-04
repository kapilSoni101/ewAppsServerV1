using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace ewApps.Core.Money {
  public static class CurrencyFormatter {
    #region formatting
    /// <summary>
    /// Gets formatted string value from amount
    /// </summary>
    /// <param name="amount">Amount tobe formatted</param>
    /// <param name="info">Culture info used to formate the value</param>
    /// <param name="blankIfZero">Returns empty string if value is 0</param>
    /// <param name="blankDecimalIfZero">Returns only number part if decimal value is 0</param>
    /// <returns>string</returns>
    public static string CurrencyToString(decimal amount, CultureInfo info, bool blankIfZero = default(bool), bool blankDecimalIfZero = default(bool)) {
      if(amount == 0 && blankIfZero)
        return "";
        string s = amount.ToString("C", info);
      if(amount % 1 == 0 && blankDecimalIfZero) {
        s = s.Remove(s.IndexOf(info.NumberFormat.CurrencyDecimalSeparator));
        if(info.NumberFormat.CurrencyPositivePattern == 1)
          s += info.NumberFormat.CurrencySymbol;
        else if(info.NumberFormat.CurrencyPositivePattern == 3)
          s += " " + info.NumberFormat.CurrencySymbol;
      }
      return s;
    }
/// <summary>
/// Gets culture info based on the Application required Format
/// </summary>
/// <param name="format">application required format parameter</param>
/// <param name="cultureInfo">base culture info, Default to current culture info</param>
/// <returns>Culture Info</returns>
    public static CultureInfo GetCultureInfo(AppCurrencyFormat format, CultureInfo cultureInfo) {
      CultureInfo info;
      if(cultureInfo == null)
        info = CultureInfo.CurrentCulture.Clone() as CultureInfo;
      else
        info = cultureInfo.Clone() as CultureInfo;
      info.NumberFormat.CurrencySymbol = format.CurrencySymbol;
      //Group Seperator
      if(format.Seperator == CurrencyFormatSeparators.CommaGSDotDS) {
        info.NumberFormat.CurrencyGroupSeparator = ",";
        info.NumberFormat.CurrencyDecimalSeparator = ".";
      }
      else {
        info.NumberFormat.CurrencyGroupSeparator = ".";
        info.NumberFormat.CurrencyDecimalSeparator = ",";
      }

      //Decimal Places seperation digits
      if(format.GroupDigits == CurrencyFormatGroupDigits.ThreeFour)
        info.NumberFormat.CurrencyGroupSizes = new int[] { 3, 4, 4 };
      else if(format.GroupDigits == CurrencyFormatGroupDigits.TwoThree)
        info.NumberFormat.CurrencyGroupSizes = new int[] { 2, 3, 3 };
      else if(format.GroupDigits == CurrencyFormatGroupDigits.ThreeThree)
        info.NumberFormat.CurrencyGroupSizes = new int[] { 3, 3, 3 };
      else if(format.GroupDigits == CurrencyFormatGroupDigits.FourFour)
        info.NumberFormat.CurrencyGroupSizes = new int[] { 4, 4, 4 };
      info.NumberFormat.CurrencyPositivePattern = (int)format.CurrencySymbolPosition ;
      info.NumberFormat.CurrencyNegativePattern = (int)format.CurrencySymbolPosition;

      info.NumberFormat.CurrencyDecimalDigits = format.DecimalPlaces;
      return info;

    }
   
    public static AppCurrencyFormat GetCurrencyFormat(string currencySymbol,CurrencySymbolPosition cSymbolPosition,int decimalPlaces,CurrencyFormatGroupDigits groupDigits,CurrencyFormatSeparators currencySeparator) {
      AppCurrencyFormat format = new AppCurrencyFormat();
      format.CurrencySymbol = currencySymbol;
      format.CurrencySymbolPosition = cSymbolPosition;
      format.DecimalPlaces = decimalPlaces;
      format.GroupDigits = groupDigits;
      format.Seperator = currencySeparator;
      return format;
    }
    #endregion

    

  }
}
