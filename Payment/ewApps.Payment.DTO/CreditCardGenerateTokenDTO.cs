using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {

    /// <summary>
    ///To generate TSys key, required following properties.
    /// </summary>
    public class CreditCardGenerateTokenDTO {

        public string mid {
            get; set;
        }

        public string userID {
            get; set;
        }

        public string password {
            get; set;
        }


    }
}
