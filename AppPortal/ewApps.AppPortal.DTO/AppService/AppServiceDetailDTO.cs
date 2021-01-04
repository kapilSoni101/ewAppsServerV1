using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class AppServiceDetailDTO {

        public Guid SerivceId {
            get; set;
        }

        public string ServiceName {
            get; set;
        }

        public bool Active {
            get; set;
        }

        public Guid AppId {
            get; set;
        }
        
        [NotMapped]
        public List<AppServiceAttributeDetailDTO> AppServiceAttributeList {
            get; set;
        }

    }
}
