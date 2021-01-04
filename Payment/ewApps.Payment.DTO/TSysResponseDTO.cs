using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {
    public class TSysResponseDTO {

        /// <summary>
        /// Status will be "Success", If payment made successfully.
        /// </summary>
        public string Status {
            get; set;
        }

        /// <summary>
        /// Error message if any error occured.
        /// </summary>
        public string ErrorMessage {
            get; set;
        }

        /// <summary>
        /// TSys payment response detail.
        /// </summary>
        public TSysCardResponse Response {
            get; set;
        }

        public TSysTransectionDTO Transaction {
            get; set;
        }

    }
}
