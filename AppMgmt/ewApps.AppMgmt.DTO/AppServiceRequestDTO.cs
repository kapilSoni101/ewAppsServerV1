using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO {
    /// <summary>
    /// Contains list of subscribed services.
    /// </summary>
    public class AppServiceRequestDTO {

        public Guid ID;

        public string Name;

        public List<AppServiceAttributeRequestDTO> AppServiceAttributeDTO;
    }
}
