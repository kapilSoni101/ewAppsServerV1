using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Shipment.DTO {
   public class RoleUpdateResponseDTO {

        public Guid RoleId {
            get; set;
        }

        public string RoleKey {
            get; set;
        }

        public long PermisssionBitMask {
            get; set;
        }

        public bool RoleUpdated {
            get; set;
        }
    }
}
