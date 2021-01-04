using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {
   public class AppShortInfoDTO {

        public Guid AppId {
            get; set;
        }

        public string AppName {
            get; set;
        }

        public string AppKey {
            get; set;
        }

        [NotMapped]
        public bool Deleted {
            get; set;
        }
    }
}
