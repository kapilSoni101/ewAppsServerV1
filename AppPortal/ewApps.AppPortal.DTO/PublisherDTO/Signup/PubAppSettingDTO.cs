using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {


    public class PubAppSettingDTO {
        /// <summary>
        /// Initializes a new instance of the <see cref="PubAppSettingDTO"/> class.
        /// </summary>
        public PubAppSettingDTO() {
            AppSubscriptionList = new List<SubscriptionPlanInfoDTO>();
            //   AppServiceList = new List<SubsPlanServiceInfoDTO>();
        }

        public Guid AppID {
            get; set;
        }

        public string AppName {
            get;
            set;
        }

        [NotMapped]
        public OperationType OpType {
            get; set;
        }


        [NotMapped]
        public List<SubscriptionPlanInfoDTO> AppSubscriptionList {
            get; set;
        }

        //public List<SubsPlanServiceInfoDTO> AppServiceList {
        //    get;
        //    set;
        //}

    }
}
