using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class AppDetailInfoDTO {

        /// <summary>
        /// The Unique Application Id.
        /// </summary>
        public Guid Id {
            get; set;
        }

        /// <summary>
        /// Identity number.
        /// </summary>
        public string IdentityNumber {
            get; set;
        }

        /// <summary>
        /// The name  of application.
        /// </summary>
        public string Name {
            get; set;
        }

        /// <summary>
        /// App unique identification key.
        /// </summary>
        public string AppKey {
            get; set;
        }

        /// <summary>
        /// App unique identification key.
        /// </summary>
        public bool Active {
            get; set;
        }

        [NotMapped]
        public List<AppServiceDetailDTO> AppServiceList {
            get; set;
        }

    }
}
