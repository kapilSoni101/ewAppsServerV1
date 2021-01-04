using System;
using System.Collections.Generic;
using System.Text;
using ewApps.AppPortal.DTO;
using ewApps.Core.ExceptionService;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// This class contains publisher sign up request validation result and also other required information.
    /// </summary>
    public class PublisherPreSignUpRespDTO {

        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherPreSignUpRespDTO"/> class.
        /// </summary>
        public PublisherPreSignUpRespDTO() {
            ErrorDataList = new List<EwpErrorData>();
            ApplicationList = new List<AppInfoDTO>();
        }

        /// <summary>
        /// It contains validation result in form of <see cref="EwpErrorData"/> list.
        /// </summary>
        public List<EwpErrorData> ErrorDataList {
            get; set;
        }

        /// <summary>
        /// The application list.
        /// </summary>
        public List<AppInfoDTO> ApplicationList {
            get; set;
        }

        /// <summary>
        /// The requested tenant user information.
        /// </summary>
        public TenantUserDTO TenantUser {
            get; set;
        }

    }
}
