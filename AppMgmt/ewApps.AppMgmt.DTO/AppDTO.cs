using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppMgmt.DTO {

    
    public class AppDTO {

           
        public Guid ID {
            get; set;
        }

        
        public string Name {
            get;
            set;
        }

        public bool Active {
            get; set;
        }

        [NotMapped]
        public List<AppServiceDTO> AppServiceList {
            get;
            set;
        }

    }
}
