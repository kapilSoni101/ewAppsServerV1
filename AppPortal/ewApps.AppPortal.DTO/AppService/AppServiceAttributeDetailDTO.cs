using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

    public class AppServiceAttributeDetailDTO {

        public Guid AttributeId {
            get; set;
        }

        public string AttributeName {
            get; set;
        }

        public bool Active {
            get; set;
        }

        public Guid AppServiceId {
            get; set;
        }

    }
}
