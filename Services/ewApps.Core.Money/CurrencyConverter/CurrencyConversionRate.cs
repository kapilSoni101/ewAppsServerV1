using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.Money{
/// <summary>
/// Property class which keepsConversionRates
/// </summary>
  public class CurrencyConversionRate { 
    public CurrencyISOCode FromCurrencyCode {   
      get; set;
    }
    public CurrencyISOCode ToCurrencyCode {
      get; set;
    }
    /// <summary>
    /// Converion Rate
    /// </summary>
    /// <remarks>Decimal DattaType is choosen with reference to the given link</remarks>
    /// <see cref="http://www.yacoset.com/how-to-handle-currency-conversions"/>
    public decimal Rate {    
      get; set;
    }

  }
}
