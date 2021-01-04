using System;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.QData {
    public class QPublisherUserSessionRepository: IQPublisherUserSessionRepository {

        QAppPortalDbContext _qAppPortalDbContext;

        public QPublisherUserSessionRepository(QAppPortalDbContext qAppPortalDbContext) {
            _qAppPortalDbContext = qAppPortalDbContext;
        }


        ///<inheritdoc/>
        public async Task<PublisherUserSessionDTO> GetPublisherPortalUserSessionInfoAsync(Guid identityUserId, Guid tenantId, string portalKey) {
            //ToDo:Review:Nitin: use sqlparameters.
            string query = string.Format("exec [prcQGetUserSessionPublisherPortal] '{0}' , '{1}' , '{2}' ", identityUserId, tenantId, portalKey);
            return _qAppPortalDbContext.Query<PublisherUserSessionDTO>().FromSql(query).ToList().FirstOrDefault();
        }

        ///<inheritdoc/>
        public async Task<UserSessionAppDTO> GetSessionAppDetailsAsync(Guid identityUserId, Guid tenantId, string appKey) {
            //ToDo:Review:Nitin: use sqlparameters.
            string query = string.Format("exec [prcQGetUserSessionPublisherApp] '{0}' , '{1}' , '{2}' ", identityUserId, tenantId, appKey);
            return _qAppPortalDbContext.Query<UserSessionAppDTO>().FromSql(query).ToList().FirstOrDefault();
        }

        //
    }
}
