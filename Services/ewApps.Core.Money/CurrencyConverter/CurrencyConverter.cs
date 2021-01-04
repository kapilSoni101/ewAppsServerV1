using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.Money {
  /// <summary>
  /// Converts the currency value from one currencyto another
  /// </summary>
  public class CurrencyConverter {
    private ICurrencyConversion _currencyConversion;
    private CurrencyCultureInfoTable _table;

    /// <summary>
    /// Constructor that expect the ICurrencyImplentor
    /// </summary>
    /// <param name="currencyConversion">DI for currency conversion Implementation</param>
    private CurrencyConverter(ICurrencyConversion currencyConversion, CurrencyCultureInfoTable table) {
      _currencyConversion = currencyConversion;
      _table = table;
    }

    /// <summary>
    /// Converts the Amount from One currency to another currency
    /// </summary>
    /// <param name="fromAmount">Amount to be converted</param>
    /// <param name="fromCurrencyCode">Currency of the amount to be converted</param>
    /// <param name="toCurrencyCode">Currency in which Amount to be converted</param>
    /// <param name="decimalPrecision">Decimal Precision used for rounding, If null no precision limit is applied</param>
    /// <returns>Converted Amount </returns>
    public decimal Convert(decimal fromAmount, CurrencyISOCode fromCurrencyCode, CurrencyISOCode toCurrencyCode, int? decimalPrecision) {
      decimal conversionRate = _currencyConversion.GetConversionRate(fromCurrencyCode, toCurrencyCode);
      decimal convertedAmount = (decimal)(conversionRate) * fromAmount;
      if(decimalPrecision != null)
        convertedAmount = Round(convertedAmount, decimalPrecision.Value);  // Bankers prefer this rounding: ToEven
      return convertedAmount;
    }

    /// Converts the Amount from One currency to amother currency
    /// </summary>
    /// <param name="fromAmount">Amount to be converted</param>
    /// <param name="fromCurrency">Currency of the amount to be converted</param>
    /// <param name="toCurrency">Currency in which Amount to be converted</param>
    /// <param name="conversionRate">Currency conversion rate</param>
    /// <param name="decimalPrecision"></param>
    /// <returns>Converted Amount </returns>
    public decimal Convert(decimal fromAmount, CurrencyISOCode fromCurrencyCode, CurrencyISOCode toCurrencyCode, double conversionRate, int? decimalPrecision) {
      decimal convertedAmount = (decimal)(conversionRate) * fromAmount;
      if(decimalPrecision != null)
        convertedAmount = Round(convertedAmount, decimalPrecision.Value);
      return convertedAmount;
    }

    /// <summary>
    /// Converts the Amount from One currency to another currency
    /// </summary>
    /// <param name="fromMoney">Money to be converted</param>
    /// <param name="toCurrencyCode">Currency in which Amount to be converted</param>
    /// <param name="decimalPrecision">Decimal Precision used for rounding, If null no precision limit is applied</param>
    /// <returns>Converted Amount </returns>

    public Money Convert(Money fromMoney, CurrencyISOCode toCurrencyCode, int? decimalPrecision) {
      decimal fromAmount = fromMoney.Amount;
      CurrencyISOCode fromCurrencyCode = fromMoney.CurrencyISOCode;
      decimal toAmount = Convert(fromAmount, fromCurrencyCode, toCurrencyCode, decimalPrecision);
      Money toMoney = new Money(toAmount, toCurrencyCode);
      return toMoney;
    }
    /// Converts the Money from One currency to amother currency
    /// </summary>
    /// <param name="fromMoney">Money to be converted</param>
    /// <param name="toCurrencyCode">Currency in which Amount to be converted</param>
    /// <param name="conversionRate">Currency conversion rate</param>
    /// <param name="decimalPrecision"></param>
    /// <returns>Converted Money </returns>
    // "ASHA:"
    public Money Convert(Money fromMoney, CurrencyISOCode toCurrencyCode, double conversionRate, int? decimalPrecision) {
      decimal fromAmount = fromMoney.Amount;
      CurrencyISOCode fromCurrencyCode = fromMoney.CurrencyISOCode;
      decimal toAmount = Convert(fromAmount, fromCurrencyCode, toCurrencyCode, conversionRate, decimalPrecision);
      Money toMoney = new Money(toAmount, toCurrencyCode);
      return toMoney;
    }

    /// <summary>
    /// Rounds a number value to a specified precision. A parameter specifies how to round the value if it is midway between two other numbers.
    /// </summary>
    /// <param name="amount">The amount to round.</param>
    /// <param name="decimals">The number of significant decimal places (precision) in the return value.</param>
    /// <returns>Amount Nearest to the decimal parameter with a precision equal to the decimals parameter. </returns>
    /// <seealso cref="decimal.Round(Decimal, Int32, MidpointRounding)"/>
    public static decimal Round(decimal amount, int decimals) {
      return decimal.Round(amount, decimals);
    }
   
  }
}
