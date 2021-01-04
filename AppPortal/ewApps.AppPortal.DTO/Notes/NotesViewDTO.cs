using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class NotesViewDTO {

        public Guid NotesId {
            get; set;
        }

        public Guid EntityId {
            get; set;
        }

        public int EntityType {
            get; set;
        }

        public string CreatedByName {
            get; set;
        }

        public DateTime CreatedOn {
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
        public bool Deleted {
            get;
            set;
        }
        public Guid TenantId {
            get;
            set;
        }
    }
}
