using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using Microsoft.Extensions.Logging;

namespace ewApps.AppMgmt.DS {

    public class TenantSignUpForCustomerDS:ITenantSignUpForCustomerDS {

        #region Local Member

        ILogger<TenantSignUpForCustomerDS> _loggerService;
        IUnitOfWork _unitOfWork;
        ITenantDS _tenantDS;
        ITenantLinkingDS _tenantLinkingDS;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantSignUpForPublisherDS" /> class and it's dependencies.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="tenantDS">The tenant ds.</param>
        /// <param name="tenantLinkingDS">The tenant linking ds.</param>
        /// <param name="tenantSubscriptionDS">The tenant subscription ds.</param>
        public TenantSignUpForCustomerDS(ILogger<TenantSignUpForCustomerDS> loggerService,
             IUnitOfWork unitOfWork,
             ITenantDS tenantDS, ITenantLinkingDS tenantLinkingDS,
             ITenantSubscriptionDS tenantSubscriptionDS) {
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
            _tenantDS = tenantDS;
            _tenantLinkingDS = tenantLinkingDS;
        }

        #endregion Constructor      

        #region Public Methods

        public async Task<TenantSignUpForCustomerResDTO> TenantSignUpForCustomerAsync(List<TenantSignUpForCustomerReqDTO> customerSignUpDTOs, CancellationToken token = default(CancellationToken)) {
            for(int i = 0; i < customerSignUpDTOs.Count; i++) {
                // Add Tenant
                await AddTenant(customerSignUpDTOs[i]);

                // Add Tenant Linking
                await AddTenantLinking(customerSignUpDTOs[i]);
            }
            //save data
            _unitOfWork.SaveAll();

            return new TenantSignUpForCustomerResDTO();
        }

        #endregion Public Methods

        #region Private Methods

        private async Task AddTenant(TenantSignUpForCustomerReqDTO customerSignUpDTO, CancellationToken token = default(CancellationToken)) {

            // Add Tenant
            Tenant tenant = new Tenant();
            tenant.TenantType = (int)TenantType.BuisnessPartner;
            tenant.ID = customerSignUpDTO.BusinesPartnerTenantId;
            tenant.CreatedBy = customerSignUpDTO.BusinesPrimaryUserId;
            tenant.UpdatedBy = customerSignUpDTO.BusinesPrimaryUserId;
            tenant.CreatedOn = DateTime.UtcNow;
            tenant.UpdatedOn = DateTime.UtcNow;
            tenant.Active = true;
            tenant.Deleted = false;
            tenant.Currency = customerSignUpDTO.Currency;
            tenant.Language = "";
            tenant.TimeZone = "";
            tenant.Name = customerSignUpDTO.CutomerName;
            tenant.IdentityNumber = "TNTSo10108";
            tenant.SubDomainName = customerSignUpDTO.BusinesPartnerTenantId.ToString();
            await _tenantDS.AddAsync(tenant);

        }

        private async Task AddTenantLinking(TenantSignUpForCustomerReqDTO customerSignUpDTO, CancellationToken token = default(CancellationToken)) {

            // Add TenantLinking
            TenantLinking busTenantLinking = _tenantLinkingDS.Find(tl => tl.BusinessTenantId == customerSignUpDTO.TenantId && tl.BusinessPartnerTenantId == null);

            TenantLinking tenantLinking = new TenantLinking();
            tenantLinking.ID = Guid.NewGuid();
            tenantLinking.CreatedBy = customerSignUpDTO.BusinesPrimaryUserId;
            tenantLinking.UpdatedBy = customerSignUpDTO.BusinesPrimaryUserId;
            tenantLinking.CreatedOn = DateTime.UtcNow;
            tenantLinking.UpdatedOn = DateTime.UtcNow;
            tenantLinking.BusinessTenantId = customerSignUpDTO.TenantId;
            tenantLinking.BusinessPartnerTenantId = customerSignUpDTO.BusinesPartnerTenantId;
            tenantLinking.PlatformTenantId = busTenantLinking.PlatformTenantId;
            tenantLinking.PublisherTenantId = busTenantLinking.PublisherTenantId;

            await _tenantLinkingDS.AddAsync(tenantLinking);
        }

        #endregion Private Methods
    }
}
