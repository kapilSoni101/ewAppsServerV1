using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {

    public class CustResponseDTO {

        /// <summary>
        /// 
        /// </summary>
         [NotMapped]
        public CustGetOnusTokenResponseDTO GetOnusTokenResponse {
            get; set;
        }
    }
}
