using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// Contains chhild attribute for AppService.
    /// </summary>
    public class BusAppServiceAttributeDTO:BaseDTO {

        public BusAppServiceAttributeDTO() {
            BusPayAcctDetail = new BusPayAcctDetailDTO();
            CustPayAcctDetail = new CustPayAcctDetailDTO();
        }

        public new Guid ID {
            get; set;
        }

        public string Name {
            get; set;
        }

        public bool Active {
            get; set;
        }

        public string AttributeKey {
            get; set;
        }

        /// <summary>
        /// service id of attrubute.
        /// </summary>
        public Guid AppServiceId {
            get; set;
        }

        [NotMapped]
        public bool Checked {
            get; set;
        }

        [NotMapped]
        public BusPayAcctDetailDTO BusPayAcctDetail {
            get; set;
        }

        [NotMapped]
        public CustPayAcctDetailDTO CustPayAcctDetail {
            get; set;
        }

    }
}
