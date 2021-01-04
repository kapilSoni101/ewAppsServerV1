using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;

namespace ewApps.AppMgmt.DS {
    public interface IConfigurationDS {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationDTO"></param>
        /// <returns></returns>
        Task UpdatePublisherConfigurationDetail(ConfigurationDTO publisherConfigurationDTO);
    }
}
