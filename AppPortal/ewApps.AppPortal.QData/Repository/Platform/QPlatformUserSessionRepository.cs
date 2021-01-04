using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.QData {
    public class QPlatformUserSessionRepository:IQPlatformUserSessionRepository {

        QAppPortalDbContext _qAppPortalDbContext;

        public QPlatformUserSessionRepository(QAppPortalDbContext qAppPortalDbContext) {
            _qAppPortalDbContext = qAppPortalDbContext;
        }

        ///<inheritdoc/>
        public async Task<PlatfromUserSessionDTO> GetPlaformPortalUserSessionInfoAsync(Guid identityUserId, Guid tenantId, string portalKey) {
            //ToDo:Review:Nitin: use sqlparameters.
            string query = string.Format("exec [prcQGetUserSessionPlatformPortal] '{0}' , '{1}' , '{2}' ", identityUserId, tenantId, portalKey);
            return _qAppPortalDbContext.Query<PlatfromUserSessionDTO>().FromSql(query).ToList().FirstOrDefault();
        }

        ///<inheritdoc/>
        public async Task<UserSessionAppDTO> GetSessionAppDetailsAsync(Guid identityUserId, Guid tenantId, string appKey) {
            //ToDo:Review:Nitin: use sqlparameters.
            string query = string.Format("exec [prcQGetUserSessionPlatformApp] '{0}' , '{1}' , '{2}' ", identityUserId, tenantId, appKey);
            return _qAppPortalDbContext.Query<UserSessionAppDTO>().FromSql(query).ToList().FirstOrDefault();
        }

        //
    }
}
