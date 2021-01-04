using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class UserSessionCurrencyDTO {

        public string Currency {
            get;
            set;
        }
        public int CurrencyCode {
            get;
            set;
        }
        //public string CurrencyShortName {
        //    get;
        //    set;
        //}
        //public string CurrencyFullName {
        //    get;
        //    set;
        //}
        public string GroupValue {
            get;
            set;
        }
        public string GroupSeperator {
            get;
            set;
        }
        public string DecimalSeperator {
            get;
            set;
        }
        public int DecimalPrecision {
            get;
            set;
        }
        //public decimal ExchangeRate {
        //    get;
        //    set;
        //}
    }
}
