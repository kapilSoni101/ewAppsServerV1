
using System;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
    public class BankAcctDetailDTO {

        public new Guid ID {
            get; set;
        }

        public string AccountNo {
            get; set;
        }
        public string BankName {
            get; set;
        }
        public string NameInBank {
            get; set;
        }
        public string ABARounting {
            get; set;
        }
        public int OperationType {
            get; set;
        }
        public string AccountType {
            get; set;
        }
    }
}
