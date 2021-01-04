using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ewApps.Core.Money {
  /// <summary>
  /// Represents an amount of money in a specific currency.
  /// </summary>
  [Serializable]
  public struct Money {
    /// <summary>
    /// Initializes a new instance of Money with the specified amount and currencyCode.
    /// </summary>
    /// <param name="amount">The amount of money.</param>
    /// <param name="currencyISOCode">The currencyISOCode to represent with the instance of Money.</param>
    public Money(decimal amount, CurrencyISOCode currencyISOCode) {
      Amount = amount;
      CurrencyISOCode = currencyISOCode;
    }

    /// <summary>
    /// The amount of money.
    /// </summary>
    public decimal Amount {
      get; private set;
    }

    /// <summary>
    /// The currency of the money.
    /// </summary>
    public CurrencyISOCode CurrencyISOCode {
      get; private set;
    }
    #region instance Comparison
    /// <summary>
    /// COmpare Money
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Money other) {
      return Amount == other.Amount && CurrencyISOCode == other.CurrencyISOCode;
    }

    /// <summary>
    /// Compares this instance to another <see cref="Money">Money</see> value of the same currency and returns an indication of their relative values. 
    /// </summary>
    /// <param name="value">The Money value to compare to this instance.</param>
    /// <returns>A signed number indicating the relative values of this instance and value.</returns>
    public int CompareTo(Money value) {
      RequireSameCurrency(this, value);
      return Amount.CompareTo(value.Amount);
    }


    #endregion

    #region Math

    /// <summary>
    /// Adds two Money values of the same currency.
    /// </summary>
    /// <param name="m1">The first Money value to add.</param>
    /// <param name="m2">The second Money value to add.</param>
    /// <returns>A Money value equal to the sum of both Money values.</returns>
    /// <exception cref="CurrencyMismatchException">m1 and m2 represent different currencies.</exception>
    public static Money operator +(Money m1, Money m2) {
      RequireSameCurrency(m1, m2);
      return new Money(m1.Amount + m2.Amount, m1.CurrencyISOCode);
    }

    /// <summary>
    /// Subtracts one Money value from another.
    /// </summary>
    /// <param name="m1">The Money value from which to subtract.</param>
    /// <param name="m2">The Money value to subtract.</param>
    /// <returns>A Money value with an amount equal to the amount of m1 minus the amount of m2.</returns>
    public static Money operator -(Money m1, Money m2) {
      RequireSameCurrency(m1, m2);
      return new Money(m1.Amount - m2.Amount, m1.CurrencyISOCode);
     
    }

    public static Money operator *(Money m, decimal d) {
      return new Money(m.Amount * d, m.CurrencyISOCode);
    }

    public static Money operator /(Money m1, decimal d) {
      return new Money(m1.Amount / d, m1.CurrencyISOCode);
    }

    /// <summary>
    /// Rounds a Money value to the nearest integer.
    /// </summary>
    /// <param name="m">The Money value to round.</param>
    /// <returns>The integer value of Money that is nearest to the d parameter. If d is halfway between two integers, one of which is even and the other odd, the even value is returned.</returns>
    /// <seealso cref="decimal.Round(Decimal)"/>
    public static Money Round(Money m) {
      return new Money(decimal.Round(m.Amount), m.CurrencyISOCode);
    }

    /// <summary>
    /// Rounds a Money value to a specified number of decimal places.
    /// </summary>
    /// <param name="m">The Money value to round.</param>
    /// <param name="decimals">The number of significant decimal places (precision) in the return value.</param>
    /// <returns>The Money value equivalent to m rounded to decimals number of decimal places.</returns>
    /// <seealso cref="decimal.Round(Decimal, Int32)"/>
    public static Money Round(Money m, int decimals) {
      return new Money(decimal.Round(m.Amount, decimals), m.CurrencyISOCode);
    }

    /// <summary>
    /// Rounds a Money value to the nearest integer. A parameter specifies how to round the value if it is midway between two other numbers.
    /// </summary>
    /// <param name="m">The Money value to round.</param>
    /// <param name="mode">A value that specifies how to round the amount of m if it is midway between two other numbers.</param>
    /// <returns>The integer value of Money that is nearest to the d parameter. If the amount of m is halfway between two numbers, one of which is even and the other odd, the mode parameter determines which of the two values is returned.</returns>
    /// <seealso cref="decimal.Round(Decimal, MidpointRounding)"/>
    public static Money Round(Money m, MidpointRounding mode) {
      return new Money(decimal.Round(m.Amount, mode), m.CurrencyISOCode);
    }

    /// <summary>
    /// Rounds a Money value to a specified precision. A parameter specifies how to round the value if it is midway between two other numbers.
    /// </summary>
    /// <param name="m">The Money value to round.</param>
    /// <param name="decimals">The number of significant decimal places (precision) in the return value.</param>
    /// <param name="mode">A value that specifies how to round the amount of m if it is midway between two other numbers.</param>
    /// <returns>The Money value that is nearest to the d parameter with a precision equal to the decimals parameter. If the amount of m is halfway between two numbers, one of which is even and the other odd, the mode parameter determines which of the two values is returned. If the precision of the amount of m is less than decimals, m is returned unchanged.</returns>
    /// <seealso cref="decimal.Round(Decimal, Int32, MidpointRounding)"/>
    public static Money Round(Money m, int decimals, MidpointRounding mode) {
      return new Money(decimal.Round(m.Amount, decimals, mode), m.CurrencyISOCode);
    }

    #endregion

    #region Comparison
   
    public static bool operator ==(Money m1, Money m2) {
      RequireSameCurrency(m1, m2);
      return m1.Amount.Equals(m2.Amount);
    }

    public static bool operator !=(Money m1, Money m2) {
      RequireSameCurrency(m1, m2);
      return !m1.Amount.Equals(m2.Amount);
    }

    public static bool operator >(Money m1, Money m2) {
      RequireSameCurrency(m1, m2);
      return m1.Amount > m2.Amount;
    }

    public static bool operator <(Money m1, Money m2) {
      RequireSameCurrency(m1, m2);
      return m1.Amount < m2.Amount;
    }

    public static bool operator >=(Money m1, Money m2) {
      RequireSameCurrency(m1, m2);
      return m1.Amount >= m2.Amount;
    }

    public static bool operator <=(Money m1, Money m2) {
      RequireSameCurrency(m1, m2);
      return m1.Amount <= m2.Amount;
    }

  

    #endregion

    private static void RequireSameCurrency(Money m1, Money m2) {
      if(!m1.CurrencyISOCode.Equals(m2.CurrencyISOCode))
        throw new CurrencyCodeMismatchException(m1.CurrencyISOCode, m2.CurrencyISOCode);
    }

  }
}
