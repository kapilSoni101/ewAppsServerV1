using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Report.DTO {
    public class ListFilterDTO {
        public DateTime FromDate {
            get; set;
        }
        public DateTime ToDate {
            get; set;
        }
        public Guid ID {
            get; set;
        }
        public bool Deleted {
            get; set;
        }
    }
}

