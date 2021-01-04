using System;
using System.Collections.Generic;
using System.Text;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {
   public class TenantAppLinkingDS:BaseDS<TenantAppLinking>, ITenantAppLinkingDS {

        ITenantAppLinkingRepository _tenantAppLinkingRepository;
        IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="PortalDS"/> class with its dependencies.
        /// </summary>
        public TenantAppLinkingDS(ITenantAppLinkingRepository tenantAppLinkingRepository, IUnitOfWork unitOfWork) : base(tenantAppLinkingRepository) {
            _tenantAppLinkingRepository = tenantAppLinkingRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
