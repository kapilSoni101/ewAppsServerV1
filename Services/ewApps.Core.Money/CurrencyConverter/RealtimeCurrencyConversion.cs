using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.CommonService;
using ewApps.Core.ServiceProcessor;

namespace ewApps.Core.Money {
  /// <summary>
  /// Provides the conversion rate between the currencies
  /// It is a static conversion implementation class based on tenantId
  /// </summary>
  public class RealtimeCurrencyConversion:ICurrencyConversion {
    private CurrencyCultureInfoTable _table;

    /// <summary>
    /// Implements IcurrencyConverter to get the curency rate from DB
    /// </summary>
    public RealtimeCurrencyConversion(CurrencyCultureInfoTable table) {
      _table = table;
    }

    /// <summary>
    /// Provides the currency conversion rate between the given currencies
    /// </summary>
    /// <param name="fromCurrencyCode"></param>
    /// <param name="toCurrencyCode"></param>
    /// <param name="conversionRateDate"></param>
    /// <returns>Conversion Rate</returns>
    public decimal GetConversionRate(CurrencyISOCode fromCurrencyCode, CurrencyISOCode toCurrencyCode) {
      //Make HTTP call to get data
      //  https://api.exchangeratesapi.io/latest?base=USD&symbols=GBP
      // https://api.exchangeratesapi.io/2010-01-12?base=USD&symbols=GBP
      return GetExchangeRateAsync("https://api.exchangeratesapi.io", fromCurrencyCode, toCurrencyCode);
    }

        public void SetFixedRateList(List<CurrencyConversionRate> list) {
            throw new NotImplementedException();
        }

        private decimal GetExchangeRateAsync(string endPoint, CurrencyISOCode fromCurrency, CurrencyISOCode toCurrency) {
      //Create HttpClient
      //Send request to the calbackendpoint 
      //Send List of Event with payload object 
      HttpClient client = new HttpClient();
      HttpRequestProcessor processor = new HttpRequestProcessor(client);
      CurrencyCultureInfo fromCurrencyInfo = _table.GetCultureInfo(fromCurrency);
      ;
      CurrencyCultureInfo toCurrencyInfo = _table.GetCultureInfo(toCurrency);
      endPoint += "/latest?base=" + fromCurrencyInfo.ShortName + "&symbols=" + toCurrencyInfo.ShortName;
      try {
        ExchangeRateDTO dto = processor.ExecuteGetRequest<ExchangeRateDTO>(endPoint, "", AcceptMediaTypeEnum.JSON, null, null, null);
        return dto.rates[toCurrencyInfo.ShortName];
      }
      catch(Exception ex) {
        //Log error and return
        throw new Exception("Error In getting Online Exchange rate", ex);
      }
    }
  }
/// <summary>
/// DTO specific to the HTTP call to get data in required format
/// </summary>
  public class ExchangeRateDTO {
    //"base":"USD","rates":{"GBP":0.7557346559},"date":"2019-03-26"
    public string @base {
      get; set;
    }
    public Dictionary<string, decimal> rates {
      get; set;
    }
    public string date {
      get; set;
    }

  }
}
