using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Report.DTO {

  public class ReportFilterDTO {

    public DateTime FromDate {
      get; set;
    }

    public DateTime ToDate {
      get; set;
    }

    public Guid CustomerId {
      get; set;
    }
        public bool Deleted {
            get;
            set;
        }
    }
}
