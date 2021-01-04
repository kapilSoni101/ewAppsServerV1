using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class BusCustomerContactDTO {


        /// <summary>
        /// Unique id of customer.
        /// </summary>
        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ERPContactKey {
            get; set;
        }


        /// <summary>
        /// 
        /// </summary>
        public Guid CustomerId {
            get; set;
        }



        /// <summary>
        /// 
        /// </summary>
        public string FirstName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string LastName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Title {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Position {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Address {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Telephone {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Email {
            get; set;
        }
    }
}
