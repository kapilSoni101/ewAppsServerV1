/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Newtonsoft.Json;

namespace ewApps.BusinessEntity.DS {

    public class ERPConfigDS:BaseDS<ERPConnectorConfig>, IERPConfigDS {

        IERPConnectorConfigRepository _systemConfigRepository;
        IUserSessionManager _userSessionManager;
        //ISyncServiceDS _syncServiceDS;
        IUnitOfWork _unitOfWork;

        #region Constructor

        /// <summary>
        /// default constructor.
        /// </summary>
        /// <param name="systemConfigRep"></param>
        /// <param name="userSessionManager"></param>
        /// <param name="unitOfWork"></param>
        public ERPConfigDS(IERPConnectorConfigRepository systemConfigRep, IUserSessionManager userSessionManager, IUnitOfWork unitOfWork) : base(systemConfigRep) {
            _systemConfigRepository = systemConfigRep;
            _userSessionManager = userSessionManager;
            //syncServiceDS = _syncServiceDS;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        #region public methods 

        /// <summary>
        /// Get all the Application list subscribed by a business tenant.
        /// </summary>
        /// <param name="businessId">Id of Business Tenant</param>
        /// <param name="token"></param>
        /// <returns>return list of application.</returns>
        public async Task<List<ERPConnectorConfigDQ>> GetBusinessAppConnectorConfigByBusinessIdAsync(Guid businessId, CancellationToken token = default(CancellationToken)) {
            return await _systemConfigRepository.GetBusinessAppConnectorConfigByBusinessIdAsync(businessId, token);
        }

        /// <summary>
        /// Get business detail by tenantId And AppId.
        /// </summary>
        /// <param name="tenantId">Id of Business Tenant</param>
        /// <param name="appId">Id of Business Application</param>
        /// <param name="token"></param>
        public async Task<ERPConnectorConfig> GetConnectorConfigByTenantIdAndAppIdAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken)) {
            return await _systemConfigRepository.GetConnectorConfigByTenantIdAndAppIdAsync(tenantId, appId, token);
        }

        #endregion public methods 

        #region Add/Update/Delete

        /// <summary>
        /// Adding configuration for tenant application.
        /// </summary>
        /// <param name="connectorConfigDTO">Client connector configuration setting for a tenant.</param>
        /// <param name="token"></param>
        public async Task AddBusinessConnectorConfigsAsync(List<ERPConnectorConfigDTO> connectorConfigDTO, CancellationToken token = default(CancellationToken)) {
            if(connectorConfigDTO == null) {
                return;
            }
            ERPConnectorConfig connectorConfig;
            UserSession session = _userSessionManager.GetSession();
            for(int j = 0; j < connectorConfigDTO.Count; j++) {
                connectorConfig = new ERPConnectorConfig();
                connectorConfig.SettingJson = connectorConfigDTO[j].Json; //JsonConvert.SerializeObject(connectorConfigDTO[j]);
                connectorConfig.ConnectorKey = connectorConfigDTO[j].ConnectorKey;
                connectorConfig.Status = connectorConfigDTO[j].Status;
                connectorConfig.Message = connectorConfigDTO[j].Message;
                connectorConfig.Active = true;
                if(session == null) {
                    connectorConfig.CreatedBy = connectorConfigDTO[j].CreatedBy;
                    connectorConfig.UpdatedBy = connectorConfigDTO[j].UpdatedBy;
                    connectorConfig.CreatedOn = DateTime.UtcNow;
                    connectorConfig.UpdatedOn = DateTime.UtcNow;
                }
                else {
                    base.UpdateSystemFieldsByOpType(connectorConfig, OperationType.Add);
                }

                connectorConfig.TenantId = connectorConfigDTO[j].TenantId;
                await _systemConfigRepository.AddAsync(connectorConfig, token);
            }
        }

        /// <summary>
        /// Adding/updating/deleting configuration for business.
        /// </summary>                
        /// <param name="connectorConfigDTO">Incoming connetor request is coming for add/update/delete.</param>
        /// <param name="token"></param>
        public async Task UpdateBusinessConnectorConfigsAsync(List<ERPConnectorConfigDTO> connectorConfigDTO, Guid tenantId, CancellationToken token) {

            //Getting exiting connector config for tenant.
            List<ERPConnectorConfigDQ> existingAppConnectorConfig = await GetBusinessAppConnectorConfigByBusinessIdAsync(tenantId, token);

            // Add/Update config
            if(connectorConfigDTO != null) {
                ERPConnectorConfig connectorConfig;
                for(int j = 0; j < connectorConfigDTO.Count; j++) {
                    ERPConnectorConfigDQ exist = null;

                    if(existingAppConnectorConfig != null)
                        exist = existingAppConnectorConfig.Find(ex => ex.ID == connectorConfigDTO[j].ID);

                    bool isAdd = exist == null;
                    connectorConfig = new ERPConnectorConfig();
                    connectorConfig.SettingJson = connectorConfigDTO[j].Json;
                    connectorConfig.ID = connectorConfigDTO[j].ID;
                    connectorConfig.ConnectorKey = connectorConfigDTO[j].ConnectorKey;
                    connectorConfig.Active = true;
                    connectorConfig.Status = connectorConfigDTO[j].Status;
                    connectorConfig.Message = connectorConfigDTO[j].Message;

                    // Adding the config.
                    if(isAdd) {
                        base.UpdateSystemFieldsByOpType(connectorConfig, OperationType.Add);
                        connectorConfig.TenantId = tenantId;//connectorConfigDTO[j].TenantId;
                        await _systemConfigRepository.AddAsync(connectorConfig, token);
                    }
                    else {
                        // updating config.
                        base.UpdateSystemFieldsByOpType(connectorConfig, OperationType.Update);
                        await _systemConfigRepository.UpdateAsync(connectorConfig, connectorConfig.ID, token);
                    }

                    // Add Connection In Connector
                    BAAddConnectionReqDTO baAddConnectionReqDTO = new BAAddConnectionReqDTO();
                    if(connectorConfigDTO[j].ConnectorKey == "SAP") {
                        baAddConnectionReqDTO = JsonConvert.DeserializeObject<BAAddConnectionReqDTO>(connectorConfigDTO[j].Json);
                        //await _syncServiceDS.AddConnectionAsync(baAddConnectionReqDTO);
                    }
                }
            }

            // Deleting the config.
            for(int j = 0; j < existingAppConnectorConfig.Count; j++) {
                ERPConnectorConfigDTO delete = null;
                if(connectorConfigDTO != null) {
                    delete = connectorConfigDTO.Find(data => data.ID == existingAppConnectorConfig[j].ID);
                }

                if(delete == null) {
                    ERPConnectorConfig config = new ERPConnectorConfig();
                    config.ID = existingAppConnectorConfig[j].ID;
                    await _systemConfigRepository.DeleteAsync(config.ID, token);

                    // Delete Connector
                    //await _syncServiceDS.DeleteConnectionAsync(tenantId, connectorConfigDTO[j].ConnectorKey);
                }
            }
            _unitOfWork.SaveAll();
        }

        #endregion Add/Update/Delete

    }
}

