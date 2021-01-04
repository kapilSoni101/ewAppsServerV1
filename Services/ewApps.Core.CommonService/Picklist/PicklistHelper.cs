using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ewApps.Core.CommonService {
  public static class PicklistHelper {

        public static int GetCurrencyIdByName(string shortName) {
            CurrencyPicklist currencyPicklist = new CurrencyPicklist();
            List<CurrencyPicklist> list = currencyPicklist.GetCurrencyPL();
            foreach(CurrencyPicklist obj in list) {
                if(shortName == obj.ShortName)
                    return obj.CurrencyId;                
            }
            return 0;
        }

        public static string GetCurrencySymbolById(int currencyId) {
            CurrencyPicklist currencyPicklist = new CurrencyPicklist();
            List<CurrencyPicklist> list = currencyPicklist.GetCurrencyPL();
            foreach(CurrencyPicklist obj in list) {
                if(currencyId == obj.CurrencyId)
                    return obj.Symbol;
            }
            return null;
        }

        public static int GetCurrencyIdBySymbol(string symbol) {
            CurrencyPicklist currencyPicklist = new CurrencyPicklist();
            List<CurrencyPicklist> list = currencyPicklist.GetCurrencyPL();
            foreach(CurrencyPicklist obj in list) {
                if(symbol == obj.Symbol)
                    return obj.CurrencyId;
            }
            return 0;
        }

        public static string GetCurrencyGroupingByCurrencyGroupingId(string currencyGroupingId) {
            CurrencyGroupingPicklist currencyGroupingPicklist = new CurrencyGroupingPicklist();
            List<CurrencyGroupingPicklist> list = currencyGroupingPicklist.GetCurrencyGroupingPL();
            foreach(CurrencyGroupingPicklist obj in list) {
                if(currencyGroupingId == obj.CurrencyGroupingId)
                    return obj.CurrencyGrouping;
            }
            return null;
        }

        public static string GetCurrencySeperatorIdByCurrencySeperator(string currencySeperator) {
            CurrencySeperatorPicklist currencySeperatorPicklist = new CurrencySeperatorPicklist();
            List<CurrencySeperatorPicklist> list = currencySeperatorPicklist.GetCurrencySepratorPL();
            foreach(CurrencySeperatorPicklist obj in list) {
                if(currencySeperator == obj.CurrencySeperator)
                    return obj.CurrencySeperatorId ;
            }
            return null;
        }

        public static string GetDateTimeFormatByDateTimeFormatId(string dateTimeFormatId) {
            DateTimeFormatPicklist dateTimeFormatPicklist = new DateTimeFormatPicklist();
            List<DateTimeFormatPicklist> list = dateTimeFormatPicklist.GetDateTimeFormatPL();
            foreach(DateTimeFormatPicklist obj in list) {
                if(dateTimeFormatId == obj.DateTimeFormatId)
                    return obj.DateTimeFormat;
            }
            return null;
        }

        public static string GetDecimalFractionByDecimalFractionId(string decimalFractionId) {
            DecimalFractionPicklist decimalFractionPicklist = new DecimalFractionPicklist();
            List<DecimalFractionPicklist> list = decimalFractionPicklist.GetDecimalFractionPL();
            foreach(DecimalFractionPicklist obj in list) {
                if(decimalFractionId == obj.DecimalFractionId)
                    return obj.DecimalFraction;
            }
            return null;
        }

        public static string GetLanguageIdByLanguageName(string languageName) {
            LanguagePicklist languagePicklist = new LanguagePicklist();
            List<LanguagePicklist> list = languagePicklist.GetLanguagePL();
            foreach(LanguagePicklist obj in list) {
                if(languageName == obj.LanguageName )
                    return obj.LanguageId ;
            }
            return null;
        }

    }



}
