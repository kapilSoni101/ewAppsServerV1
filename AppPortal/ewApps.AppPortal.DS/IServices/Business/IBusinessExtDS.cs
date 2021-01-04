/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit mundra <amundra@eworkplaceapps.com>
 * Date: 04 September 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    /// <summary>
    /// A wrapper class contains method to get business and related entities.
    /// </summary>
    public interface IBusinessExtDS {

        #region Get

        /// <summary>
        /// Get business update model with all child entities models.
        /// </summary>
        /// <param name="tenantId">Unique tenant id of business.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<UpdateBusinessTenantModelDQ> GetBusinessUpdateModelAsync(Guid tenantId, CancellationToken token = default(CancellationToken));



        #endregion Get 

        #region ERP Mgmt

        /// <summary>
        /// Method is used to validate ERP connector connection.
        /// </summary>
        /// <param name="request">Request model</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> TestConnectionAsync(object request, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Sync data from V1 connector to application
        /// </summary>
        /// <param name="pullERPDataAsync"></param>
        /// <returns></returns>

        Task<bool> PullERPDataAsync(PullERPDataReqDTO pullERPDataAsync, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Sync data time log from connector to application
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BusBASyncTimeLogDTO>> SyncTimeLogAsync(Guid tenantId, CancellationToken token = default(CancellationToken));


        Task UpdateBusinessConnectorConfigsAsync(Guid tenantId, List<ConnectorConfigDTO> connectorConfigDTO, CancellationToken token);

        Task<bool> PushSalesOrderDataInERPAsync(Guid tenantid, BusBASalesOrderDTO request, CancellationToken token = default(CancellationToken));

        Task<AttachmentResDTO> GetAttachmentFromERP(AttachmentReqDTO request, CancellationToken token = default(CancellationToken));

        #endregion ERP Mgmt

        #region Configuration
        /// <summary>
        /// Get Configuration Detail For Business 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<BusConfigurationDTO> GetBusinessConfigurationDetailAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Update Business Configuration 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task UpdateBusinessConfigurationDetailAsync(BusConfigurationDTO dto, CancellationToken token = default(CancellationToken));


        #endregion

        #region Delete

        /// <summary>
        /// Delete business and its subscription.
        /// </summary>
        /// <param name="tenantId">Business tenant id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task DeleteBusinessAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        #endregion Delete

    }
}
