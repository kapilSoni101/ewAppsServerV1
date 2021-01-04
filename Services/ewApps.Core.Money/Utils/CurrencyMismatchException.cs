using System;

namespace ewApps.Core.Money
{
    public class CurrencyCodeMismatchException: Exception   
    {
        internal CurrencyCodeMismatchException(CurrencyISOCode c1, CurrencyISOCode c2)
            : base($"This operation cannot be performed between {c1} and {c2}.")
        { }
    }
}