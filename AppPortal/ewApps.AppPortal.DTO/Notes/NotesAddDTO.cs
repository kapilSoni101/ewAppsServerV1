using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class NotesAddDTO {

        public Guid ID {
            get; set;
        }

        public Guid EntityId {
            get; set;
        }

        public int EntityType {
            get; set;
        }

        public string Content {
            get; set;
        }

        public bool System {
            get; set;
        }
        public bool Private {
            get; set;
        }
        public Guid TenantId {
            get;
            set;
        }
    }
}
