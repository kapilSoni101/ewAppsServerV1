using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO {
    public class AuthenticationRequestDTO {      
            public GenerateTokenDTO GenerateKey {
                get; set;
            }

        }
        public class GenerateTokenDTO {
            public string mid {
                get; set;
            } //= "887000003172";
            public string userID {
                get; set;
            } // = "TA5612124";
            public string password {
                get; set;
            } //= "Batch@1234";

        }
    }

