using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public interface IQConfigurationDS {

        /// <summary>
        /// get configuration details
        /// </summary>
        /// <param name="buspartnertenantid"></param>        
        /// <param name="token"></param>
        /// <returns></returns>
        Task<CustConfigurationViewDTO> GetConfigurationDetailAsync(Guid buspartnertenantid, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Vendor Configuration Detail
        /// </summary>
        /// <param name="buspartnertenantid"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<VendorConfigurationDTO> GetVendorConfigurationDetailAsync(Guid buspartnertenantid, CancellationToken cancellationToken = default(CancellationToken));

    }
}
