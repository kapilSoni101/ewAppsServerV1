using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.Money{
/// <summary>
/// Interface to get conversion rate
/// </summary>
  public interface ICurrencyConversion {
    /// <summary>
    /// Gets conversion rate between 2 currency.
    /// </summary>
    /// <param name="fromCurrencyCode">Source currency code</param>
    /// <param name="toCurrencyCode">Destination currency Code</param>
    /// <returns>Conversion Rate</returns>
    decimal GetConversionRate(CurrencyISOCode fromCurrencyCode, CurrencyISOCode toCurrencyCode);

        void SetFixedRateList(List<CurrencyConversionRate> list);


  }
}
