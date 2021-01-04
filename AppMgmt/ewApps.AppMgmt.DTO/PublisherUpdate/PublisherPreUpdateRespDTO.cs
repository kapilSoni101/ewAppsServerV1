using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.ExceptionService;

namespace ewApps.AppMgmt.DTO {

    /// <summary>
    /// This class contains publisher sign up request validation result and also other required information.
    /// </summary>
    public class PublisherPreUpdateRespDTO {

        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherPreSignUpRespDTO"/> class.
        /// </summary>
        public PublisherPreUpdateRespDTO() {
            ErrorDataList = new List<EwpErrorData>();
            ApplicationList = new List<AppInfoDTO>();
            SubsriptionPlanInfoList = new List<SubscriptionPlanInfoDTO>();
            SuscriptionPlanServiceInfoList = new List<SubsPlanServiceInfoDTO>();
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

        public List<SubscriptionPlanInfoDTO> SubsriptionPlanInfoList {
            get; set;
        }

        /// <summary>
        /// The requested plan and service list.
        /// </summary>
        public List<SubsPlanServiceInfoDTO> SuscriptionPlanServiceInfoList {
            get; set;
        }

    }
}
