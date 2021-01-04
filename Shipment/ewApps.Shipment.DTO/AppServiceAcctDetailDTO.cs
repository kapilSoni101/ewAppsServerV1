using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.Shipment.DTO {
    public class AppServiceAcctDetailDTO : BaseDTO {

        public new Guid ID {
            get; set;
        }

        public string AccountJson {
            get; set;
        }
    }
}
