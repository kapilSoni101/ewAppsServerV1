using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.QData {
    public class QVendUserSessionRepository:IQVendUserSessionRepository {

        QAppPortalDbContext _qAppPortalDbContext;

        public QVendUserSessionRepository(QAppPortalDbContext qAppPortalDbContext) {
            _qAppPortalDbContext = qAppPortalDbContext;
        }


        ///<inheritdoc/>
        public async Task<VendorUserSessionDTO> GetVendorPortalUserSessionInfoAsync(Guid identityUserId, Guid tenantId, string portalKey, int userType) {
           
            string query = string.Format("exec [prcQGetUserSessionVendorPortal] '{0}' , '{1}' , '{2}' , {3} ", identityUserId, tenantId, portalKey, userType);
            return _qAppPortalDbContext.Query<VendorUserSessionDTO>().FromSql(query).ToList().FirstOrDefault();
        }

        ///<inheritdoc/>
        public async Task<List<UserSessionAppDTO>> GetSessionAppDetailsAsync(Guid identityUserId, Guid tenantId, int userType) {
           
            string query = string.Format("exec [prcQGetUserSessionVendorApp] '{0}' , '{1}' , '{2}' ", identityUserId, tenantId, userType);
            return _qAppPortalDbContext.Query<UserSessionAppDTO>().FromSql(query).ToList();
        }
    }
}
