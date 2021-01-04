using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ewApps.Core.Money {
  /// <summary>
  /// Entity class to keep cultural information of currencyCode
  /// </summary>
  public class CurrencyCultureInfo {

    public CurrencyCultureInfo(CurrencyISOCode code, string symbol, string shortName, string fullName) {
      Code = code;
      Symbol = symbol;
      ShortName = shortName;
      FullName = fullName;
    }
    /// <summary>
    /// Currency ISO Code
    /// </summary>
    public CurrencyISOCode Code {
      get; private set;
    }

    /// <summary>
    /// Currency Symbol
    /// </summary>
    public string Symbol {
      get; private set;
    }
    /// <summary>
    /// Short name of currency
    /// </summary>
    public string ShortName {
      get; private set;
    }
    /// <summary>
    /// Long name of currency
    /// </summary>
    public string FullName {
      get; private set;
    }
  }
}
