using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace ewApps.Core.Money {
    /// <summary>
    /// Contains Culture info of the currency  
    /// </summary>
    public class CurrencyCultureInfoTable {
        private Dictionary<CurrencyISOCode, CurrencyCultureInfo> _currencyDictionary;
        private IServiceProvider _provider;

        public CurrencyCultureInfoTable(IServiceProvider provider) {
            _currencyDictionary = new Dictionary<CurrencyISOCode, CurrencyCultureInfo>();
            _currencyDictionary = InitializeCurrencyDictionary();
            _provider = provider;
        }
        /// <summary>
        /// GetCulture info for the given CurrencyCode
        /// </summary>
        /// <param name="currencyISOCode">Currency Code</param>
        /// <returns>Culture Info</returns>
        public CurrencyCultureInfo GetCultureInfo(CurrencyISOCode currencyISOCode) {
            if(_currencyDictionary.ContainsKey(currencyISOCode))
                return _currencyDictionary[currencyISOCode];
            else
                return null;
        }

        /// <summary>
        /// Checks the Currency code exists or not
        /// </summary>
        /// <param name="currencyISOCode"></param>
        /// <returns></returns>
        public bool Exists(CurrencyISOCode currencyISOCode) {
            return _currencyDictionary.ContainsKey(currencyISOCode);
        }
        /// <summary>
        /// Gets Currency culture info for supported currencies
        /// </summary>
        /// <returns></returns>
        public Dictionary<CurrencyISOCode, CurrencyCultureInfo> GetCurrencyCultureInfo() {
            return _currencyDictionary;
        }
        /// <summary>
        /// Intialize dictionary for 4 currencies, It mimics the database
        /// </summary>
        /// <returns></returns>

        private Dictionary<CurrencyISOCode, CurrencyCultureInfo> InitializeCurrencyDictionary() {
            var result = new Dictionary<CurrencyISOCode, CurrencyCultureInfo>();
            CurrencyCultureInfo info = new CurrencyCultureInfo(CurrencyISOCode.None, "", "None", "Select the currency");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.USD, "$", "USD", "United States dollar");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.GBP, "£", "GBP", "Great Britain pound");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.EUR, "€", "EUR", "Euro");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.CAD, "$", CurrencyISOCode.CAD.ToString(), "Canadian dollar");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.INR, "₹", "INR", "Indian rupee");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.ARS, "$", "ARS", "Argentina Peso");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.AUD, "$", "AUD", "Australia Dollar");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.BSD, "$", "BSD", "Bahamas Dollar");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.BOB, "$b", "BOB", "Bolivia Bolíviano");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.BRL, "R$", "BRL", "Brazil Real");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.CLP, "$", "CLP", "Chile Peso");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.CNY, "¥", "CNY", "China Yuan Renminbi");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.COP, "$b", "COP", "Colombia Peso");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.CUP, "₱", "CUP", "Cuba Peso");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.DKK, "kr", "DKK", "Denmark Krone");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.EGP, "£", "EGP", "Egypt Pound");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.SVC, "$", "SVC", "El Salvador Colon");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.HKD, "$", "HKD", "Hong Kong Dollar");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.JPY, "¥", "JPY", "Japan Yen");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.LBP, "£", "LBP", "Lebanon Pound");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.MUR, "₨", "MUR", "Mauritius Rupee");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.MXN, "$", "MXN", "Mexico Peso");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.QAR, "﷼", "QAR", "Qatar Riyal");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.RUB, "₽", "RUB", "Russia Ruble");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.SAR, "﷼", "SAR", "Saudi Arabia Riyal");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.ZAR, "R", "ZAR", "South Africa Rand");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.CHF, "₣", "CHF", "Switzerland Franc");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.TRY, "₺", "TRY", "Turkey Lira");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.VEF, "Bs", "VEF", "Venezuela Bolívar");
            result.Add(info.Code, info);
            info = new CurrencyCultureInfo(CurrencyISOCode.YER, "﷼", "YER", "Yemen Rial");
            result.Add(info.Code, info);
            return result;
        }
    }
}