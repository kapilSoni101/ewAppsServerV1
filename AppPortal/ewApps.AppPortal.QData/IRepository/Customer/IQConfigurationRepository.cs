using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.QData {

    /// <summary>
    /// Configuration details
    /// </summary>
    public interface IQConfigurationRepository {

        /// <summary>
        /// Customer configuration
        /// </summary>
        /// <param name="buspartnertenantid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<CustConfigurationViewDTO> GetConfigurationDetailAsync(Guid buspartnertenantid, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer address list
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<CustCustomerAddressDTO>> GetCustomerAddressListByIdAsync(Guid CustomerID, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer contact list
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<CustCustomerContactDTO>> GetCustomerContactListByIdAsync(Guid CustomerID, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer account detail
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<CustomerAccountDTO>> GetCustomerAccListByCustomerIdAsync(Guid CustomerID, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Customer configuration
        /// </summary>
        /// <param name="buspartnertenantid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<VendorConfigurationDTO> GetVendorConfigurationDetailAsync(Guid buspartnertenantid, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer address list
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<VendorAddressDTO>> GetVendorAddressListByIdAsync(Guid CustomerID, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer contact list
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<VendorContactDTO>> GetVendorContactListByIdAsync(Guid vendorID, CancellationToken token = default(CancellationToken));


    }
}
