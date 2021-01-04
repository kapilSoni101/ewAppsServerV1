using System;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.UserSessionService;

namespace ewApps.BusinessEntity.Data {

    public class BAAPInvoiceAttachmentRepository:BaseRepository<BAAPInvoiceAttachment, BusinessEntityDbContext>, IBAAPInvoiceAttachmentRepository {

        #region Constructor

        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        public BAAPInvoiceAttachmentRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

    }

}