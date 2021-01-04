using System;
using ewApps.Core.BaseService;

namespace AppManagement.DTO {
  public class PubBusinessSubsPlanAppServiceDTO : BaseDTO {

        public Guid AppId {
            get; set;
        }
     
        public Guid AppServiceId {
            get; set;
        }

        public Guid AppServiceAttributeId {
            get; set;
        }
    }

}
