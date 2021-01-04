 using System; 
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.AppPortal.Entity;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.Data {

public class VendorRepository:BaseRepository<Vendor, AppPortalDbContext>, IVendorRepository {

	#region Constructor

    /// <summary>
    /// Constructor initializing the base variables
    /// </summary>
    /// <param name="context"></param>
    public VendorRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    }

    #endregion

 }

}