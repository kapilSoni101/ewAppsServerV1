using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.Money{
  /// <summary>
  /// Provides the conversion rate between the currencies
  /// It is a static conversion implementation class based on tenantId
  /// </summary>
  public class FixedRateCurrencyConversion:ICurrencyConversion { 
   private List<CurrencyConversionRate> _conversionRates;  
   
    /// <summary>
    /// Implements IcurrencyConverter to get the curency rate from DB
    /// </summary>
    /// <param name="id">Unique Identifier to get the Currency Conversion for like tenantId,Customer Id etc.</param>
    /// <param name="conversionRateDate">Date for which conversion rate should be picked</param>
    public FixedRateCurrencyConversion() {
      _conversionRates = new List<CurrencyConversionRate>();
     
    }

    public void SetFixedRateList(List<CurrencyConversionRate> list) {
      _conversionRates = list;
    }

    public List<CurrencyConversionRate> GetFixedRateList() {
      return _conversionRates;
    }

    /// <summary>
    /// Provides the currency conversion rate between the given currencies
    /// </summary>
    /// <param name="fromCurrencyCode"></param>
    /// <param name="toCurrencyCode"></param>
    /// <returns>Conversion Rate</returns>
    public decimal GetConversionRate(CurrencyISOCode fromCurrencyCode, CurrencyISOCode toCurrencyCode) {
      if(_conversionRates == null)
        throw new Exception("Conversion List not found");
      CurrencyConversionRate conversionRate = _conversionRates.Find(x => x.FromCurrencyCode == fromCurrencyCode && x.ToCurrencyCode == toCurrencyCode);
      if(conversionRate == null)
        throw new Exception(string.Format("Conversion from {0} to {1} not found", fromCurrencyCode.ToString(),toCurrencyCode.ToString()));
      return conversionRate.Rate;
    }
    #region Not used
    /*  public virtual void InitializeConversionRates(string id, DateTime conversionRateDate) {
        //Get the rates from table based on TenantId 
        //This is temprpry test for getting data by tenantID from DB
        //Instead of DB we are using fix rows here.
        ConversionRateDate = conversionRateDate;
        CurrencyConversionRate conversionRate = new CurrencyConversionRate();
        conversionRate.FromCurrencyCode = CurrencyISOCode.USD;
        conversionRate.ToCurrencyCode = CurrencyISOCode.INR;
        conversionRate.Rate = 68.99M;
        _conversionRates.Add(conversionRate);
        conversionRate = new CurrencyConversionRate();
        conversionRate.FromCurrencyCode = CurrencyISOCode.INR;
        conversionRate.ToCurrencyCode = CurrencyISOCode.USD;
        conversionRate.Rate = 0.014M;
        _conversionRates.Add(conversionRate);
      }*/
    #endregion
  }
}
