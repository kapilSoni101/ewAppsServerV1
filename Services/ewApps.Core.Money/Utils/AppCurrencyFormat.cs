using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ewApps.Core.Money {
  public class AppCurrencyFormat {
    public CurrencyFormatSeparators Seperator {
      get; set; }

    public CurrencyFormatGroupDigits GroupDigits {
      get;set;
    }
    public int DecimalPlaces {
      get;set;
    }
    
    public CurrencySymbolPosition CurrencySymbolPosition {
      get;set;
    }

    public string CurrencySymbol {
      get;set;
    }
   

  }
}
