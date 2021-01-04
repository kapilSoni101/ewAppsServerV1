using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
   public  class CustomerAccountDTO {
        public new Guid ID {
            get; set;
        }
        public int AccountType {
            get; set;
        }
        public string AccountJson {
            get; set;
        }
    }
}
