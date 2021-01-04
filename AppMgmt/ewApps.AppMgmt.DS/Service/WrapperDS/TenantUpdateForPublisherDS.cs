using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;

namespace ewApps.AppMgmt.DS {
    /// <summary>
    /// This class implements publisher tenant update operations.
    /// </summary>
    /// <seealso cref="ewApps.AppMgmt.DS.ITenantUpdateForPublisherDS" />
    public class TenantUpdateForPublisherDS:ITenantUpdateForPublisherDS {
        ITenantDS _tenantDS;
        ITenantUserDS _tenantUserDS;
        IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantUpdateForPublisherDS"/> class.
        /// </summary>
        /// <param name="tenantDS">The tenant ds.</param>
        /// <param name="tenantUserDS">The tenant user ds.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public TenantUpdateForPublisherDS(ITenantDS tenantDS, ITenantUserDS tenantUserDS, IUnitOfWork unitOfWork) {
            _tenantDS = tenantDS;
            _tenantUserDS = tenantUserDS;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public async Task UpdatePublisherTenantAsync(TenantUpdateForPublisherDTO tenantUpdateForPublisherDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            // Get existing tenant user.
            TenantUser tenantUser = await _tenantUserDS.GetAsync(tenantUpdateForPublisherDTO.UserId, cancellationToken);

            // Update user's properties.
            tenantUser.FirstName = tenantUpdateForPublisherDTO.FirstName;
            tenantUser.LastName = tenantUpdateForPublisherDTO.LastName;
            tenantUser.FullName = tenantUpdateForPublisherDTO.FullName;
            _tenantUserDS.UpdateSystemFieldsByOpType(tenantUser, Core.BaseService.OperationType.Update);
            // Update User.
            await _tenantUserDS.UpdateAsync(tenantUser, tenantUser.ID, cancellationToken);

            // Get existing tenant record.
            Tenant tenant = await _tenantDS.GetAsync(tenantUpdateForPublisherDTO.TenantId, cancellationToken);

            // Update tenant's modified properties.
            tenant.Name = tenantUpdateForPublisherDTO.TenantName;
            tenant.Active = tenantUpdateForPublisherDTO.TenantActiveState;
            _tenantDS.UpdateSystemFieldsByOpType(tenant, Core.BaseService.OperationType.Update);

            // Update tenant.
            await _tenantDS.UpdateAsync(tenant, tenant.ID, cancellationToken);

            _unitOfWork.SaveAll();
        }
    }
}
