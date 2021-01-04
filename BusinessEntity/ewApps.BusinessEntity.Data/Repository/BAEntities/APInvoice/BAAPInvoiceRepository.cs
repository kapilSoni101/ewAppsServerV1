 using System; 
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.UserSessionService;

namespace ewApps.BusinessEntity.Data {

public class BAAPInvoiceRepository:BaseRepository<BAAPInvoice, BusinessEntityDbContext>, IBAAPInvoiceRepository {

	#region Constructor

    /// <summary>
    /// Constructor initializing the base variables
    /// </summary>
    /// <param name="context"></param>
    public BAAPInvoiceRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    }

    #endregion

 }

}