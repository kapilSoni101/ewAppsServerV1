using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
   public  class BusCustomerAppAndUserDetailModel {
       
            /// <summary>
        /// 
        /// </summary>
        public BusCustomerApplicationDTO ApplicationDetail {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<BusCustomerUserDTO> UserList {
            get; set;
        }
    }
}
