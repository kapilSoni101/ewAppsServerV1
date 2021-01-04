using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.Common {
    public static class PaymentHelper {

        public static string GetCardNameFromType(string cardType) {
            switch(cardType) {
                case "V":
                    return "Visa";
            }

            return "";
        }

    }
}
