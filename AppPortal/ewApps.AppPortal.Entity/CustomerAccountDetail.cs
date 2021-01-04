using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Entity {
    [Table("CustomerAccountDetail", Schema = "ap")]
    public class CustomerAccountDetail:BaseEntity {
        
        
            public Guid CustomerId {
                get; set;
            }
        

            public string AccountJson {
                get; set;
            }

            public int AccountType {
                get; set;
            }

        }
    }
