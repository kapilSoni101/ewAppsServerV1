using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {

    /// <summary>
    /// 
    /// </summary>
    public class BrandingDS :IBrandingDS {

        #region Local member
        ITenantSubscriptionDS _tenantSubscriptionDS;
        ITenantDS _tenantDS;
        ITenantLinkingDS _tenantLinkingDS;
        IUnitOfWork _unitOfWork;
        #endregion


        #region Constructor 

        public BrandingDS(ITenantSubscriptionDS tenantSubscription, ITenantDS tenantDS, ITenantLinkingDS tenantLinkingDS, IUnitOfWork unitOfWork) {
            _tenantSubscriptionDS = tenantSubscription;
            _tenantDS = tenantDS;
            _tenantLinkingDS = tenantLinkingDS;
            _unitOfWork = unitOfWork;

        }

        #endregion

        #region Platform Update

        public List<Guid?> UpdatePlatformBrandingDetail(BrandingDTO platformBrandingDTO) {
            //Get TenantSubscription Entity Information
            TenantSubscription tenantSubscription = _tenantSubscriptionDS.FindAsync(i => i.TenantId == platformBrandingDTO.TenantId).Result;
            tenantSubscription.ThemeId = platformBrandingDTO.ThemeId;
            //Update TenantSubscription System Fields Entity Information 
            _tenantSubscriptionDS.UpdateSystemFieldsByOpType(tenantSubscription, OperationType.Update);
            //Update TenantSubscription Entity Information
            _tenantSubscriptionDS.Update(tenantSubscription, tenantSubscription.ID);

            //Get Tenant Entity Information 
            Tenant tenant = _tenantDS.Get(platformBrandingDTO.TenantId);
            tenant.Name = platformBrandingDTO.Name;
            //Update Tenant System Fields Entity Information 
            _tenantDS.UpdateSystemFieldsByOpType(tenant, OperationType.Update);
            //Update Tenant Entity Information 
            _tenantDS.Update(tenant, tenant.ID);

            _unitOfWork.SaveAll();

            //Get List of Tenantlinking Entity For To Get All Publisher  
            List<Guid?> tenantLinking = _tenantLinkingDS.FindAll(i => i.PlatformTenantId == platformBrandingDTO.TenantId && i.PublisherTenantId != null && i.BusinessTenantId == null).ToList().Select(m => m.PublisherTenantId).ToList();
            
            return tenantLinking;
            
        }

        #endregion

        #region Publisher Update

        public async Task UpdatePublisherBrandingDetail(BrandingDTO publisherBrandingDTO) {
            //Get TenantSubscription Entity Information
            TenantSubscription tenantSubscription = _tenantSubscriptionDS.FindAsync(i => i.TenantId == publisherBrandingDTO.TenantId).Result;
            tenantSubscription.ThemeId = publisherBrandingDTO.ThemeId;
            //Update TenantSubscription System Fields Entity Information 
            _tenantSubscriptionDS.UpdateSystemFieldsByOpType(tenantSubscription, OperationType.Update);
            //Update TenantSubscription Entity Information
            _tenantSubscriptionDS.Update(tenantSubscription, tenantSubscription.ID);

            //Get Tenant Entity Information 
            Tenant tenant = _tenantDS.Get(publisherBrandingDTO.TenantId);
            tenant.Name = publisherBrandingDTO.Name;
            //Update Tenant System Fields Entity Information 
            _tenantDS.UpdateSystemFieldsByOpType(tenant, OperationType.Update);
            //Update Tenant Entity Information 
            _tenantDS.Update(tenant, tenant.ID);

            _unitOfWork.SaveAll();

            
        }

        #endregion

        #region Business Update

        /// <summary>
        ///  Update business branding
        /// </summary>
        /// <param name="businessBrandingDTO"></param>
        /// <returns></returns>
        public async Task UpdatebusinessBrandingDetail(BusinessBrandingDTO businessBrandingDTO) {
            //Get TenantSubscription Entity Information
            TenantSubscription tenantSubscription = _tenantSubscriptionDS.FindAsync(i => i.TenantId == businessBrandingDTO.TenantId && i.AppId == businessBrandingDTO.AppId).Result;
            tenantSubscription.ThemeId = businessBrandingDTO.ThemeId;
            //Update TenantSubscription System Fields Entity Information 
            _tenantSubscriptionDS.UpdateSystemFieldsByOpType(tenantSubscription, OperationType.Update);
            //Update TenantSubscription Entity Information
            _tenantSubscriptionDS.Update(tenantSubscription, tenantSubscription.ID);

            //Get Tenant Entity Information 
            Tenant tenant = _tenantDS.Get(businessBrandingDTO.TenantId);
            tenant.Name = businessBrandingDTO.Name;
            //Update Tenant System Fields Entity Information 
            _tenantDS.UpdateSystemFieldsByOpType(tenant, OperationType.Update);
            //Update Tenant Entity Information 
            _tenantDS.Update(tenant, tenant.ID);
            _unitOfWork.SaveAll();
        }

        #endregion


        #region Ship-App Update

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipAppBrandingDTO"></param>
        /// <returns></returns>
        public async Task UpdateShipAppBrandingAsync(AppPortalBrandingDTO shipAppBrandingDTO) {
            //Get TenantSubscription Entity Information
            TenantSubscription tenantSubscription = _tenantSubscriptionDS.FindAsync(i => i.TenantId == shipAppBrandingDTO.TenantId && i.AppId == shipAppBrandingDTO.AppId).Result;
            tenantSubscription.ThemeId = shipAppBrandingDTO.ThemeId;
            //Update TenantSubscription System Fields Entity Information 
            _tenantSubscriptionDS.UpdateSystemFieldsByOpType(tenantSubscription, OperationType.Update);
            //Update TenantSubscription Entity Information
            _tenantSubscriptionDS.Update(tenantSubscription, tenantSubscription.ID);
            
            _unitOfWork.SaveAll();
        }

        #endregion

        #region Pay-App Update

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipAppBrandingDTO"></param>
        /// <returns></returns>
        public async Task UpdatePayAppBrandingAsync(AppPortalBrandingDTO payAppBrandingDTO) {
            //Get TenantSubscription Entity Information
            TenantSubscription tenantSubscription = _tenantSubscriptionDS.FindAsync(i => i.TenantId == payAppBrandingDTO.TenantId && i.AppId == payAppBrandingDTO.AppId).Result;
            tenantSubscription.ThemeId = payAppBrandingDTO.ThemeId;
            //Update TenantSubscription System Fields Entity Information 
            _tenantSubscriptionDS.UpdateSystemFieldsByOpType(tenantSubscription, OperationType.Update);
            //Update TenantSubscription Entity Information
            _tenantSubscriptionDS.Update(tenantSubscription, tenantSubscription.ID);

            _unitOfWork.SaveAll();
        }

        #endregion

        #region Cust-App Update

        /// <summary>
        /// 
        /// </summary>
        /// <param name="custAppBrandingDTO"></param>
        /// <returns></returns>
        public async Task UpdateCustAppBrandingAsync(AppPortalBrandingDTO custAppBrandingDTO) {
            //Get TenantSubscription Entity Information
            TenantSubscription tenantSubscription = _tenantSubscriptionDS.FindAsync(i => i.TenantId == custAppBrandingDTO.TenantId && i.AppId == custAppBrandingDTO.AppId).Result;
            tenantSubscription.ThemeId = custAppBrandingDTO.ThemeId;
            //Update TenantSubscription System Fields Entity Information 
            _tenantSubscriptionDS.UpdateSystemFieldsByOpType(tenantSubscription, OperationType.Update);
            //Update TenantSubscription Entity Information
            _tenantSubscriptionDS.Update(tenantSubscription, tenantSubscription.ID);

            _unitOfWork.SaveAll();
        }

        #endregion

        #region Vend-App Update

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendAppBrandingDTO"></param>
        /// <returns></returns>
        public async Task UpdateVendAppBrandingAsync(AppPortalBrandingDTO vendAppBrandingDTO) {
            //Get TenantSubscription Entity Information
            TenantSubscription tenantSubscription = _tenantSubscriptionDS.FindAsync(i => i.TenantId == vendAppBrandingDTO.TenantId && i.AppId == vendAppBrandingDTO.AppId).Result;
            tenantSubscription.ThemeId = vendAppBrandingDTO.ThemeId;
            //Update TenantSubscription System Fields Entity Information 
            _tenantSubscriptionDS.UpdateSystemFieldsByOpType(tenantSubscription, OperationType.Update);
            //Update TenantSubscription Entity Information
            _tenantSubscriptionDS.Update(tenantSubscription, tenantSubscription.ID);

            _unitOfWork.SaveAll();
        }

        #endregion
    }
}
