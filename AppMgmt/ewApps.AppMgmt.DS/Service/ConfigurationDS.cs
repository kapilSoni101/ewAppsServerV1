using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {
    public class ConfigurationDS:IConfigurationDS {

        #region Local member
        ITenantDS _tenantDS;
        ITenantUserDS _tenantUserDS;
        IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor 
        public ConfigurationDS( ITenantDS tenantDS, ITenantUserDS tenantUserDS,IUnitOfWork unitOfWork) {
            _tenantDS = tenantDS;
            _tenantUserDS = tenantUserDS;
            _unitOfWork = unitOfWork;
        }
        #endregion


        #region Publisher configuration Update

        /// <summary>
        /// Publisher Configuration update
        /// </summary>
        /// <param name="publisherConfigurationDTO"></param>
        /// <returns></returns>
        public async Task UpdatePublisherConfigurationDetail(ConfigurationDTO publisherConfigurationDTO) {
            
            //Get Tenant Entity Information 
            Entity.Tenant tenant = _tenantDS.Get(publisherConfigurationDTO.TenantId);
            tenant.Name = publisherConfigurationDTO.Name;
            tenant.SubDomainName = publisherConfigurationDTO.SubDomainName;
            
            //Update Tenant System Fields Entity Information 
            _tenantDS.UpdateSystemFieldsByOpType(tenant, OperationType.Update);
            
            //Update Tenant Entity Information 
            _tenantDS.Update(tenant, publisherConfigurationDTO.TenantId);

            //Get Tenant User Entity Information  
            Entity.TenantUser tenantUser = _tenantUserDS.Get(publisherConfigurationDTO.AdminUserId);
            tenantUser.FirstName = publisherConfigurationDTO.AdminUserFirstName;
            tenantUser.LastName = publisherConfigurationDTO.AdminUserLastName;
            tenantUser.FullName = publisherConfigurationDTO.AdminUserFirstName + " " + publisherConfigurationDTO.AdminUserLastName;
            
            //Update Tenant User System Fields Entity Information 
            _tenantUserDS.UpdateSystemFieldsByOpType(tenantUser, OperationType.Update);

            //Update Tenant User Entity Information 
            _tenantUserDS.Update(tenantUser, publisherConfigurationDTO.AdminUserId);

            _unitOfWork.SaveAll();
        }

        #endregion

    }
}
