using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Entity {


    [Table("Notes", Schema = "ap")]
    public class Notes:BaseEntity {


        public const string EntityName = "Notes";

        public int EntityType {
            get; set;
        }
        public Guid EntityId {
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
    }
}

