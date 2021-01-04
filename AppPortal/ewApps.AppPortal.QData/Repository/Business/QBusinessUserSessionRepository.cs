using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;
using ewApps.Core.BaseService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.QData {
    public class QBusinessUserSessionRepository:IQBusinessUserSessionRepository {

        QAppPortalDbContext _qAppPortalDbContext;

        public QBusinessUserSessionRepository(QAppPortalDbContext qAppPortalDbContext) {
            _qAppPortalDbContext = qAppPortalDbContext;
        }

        //prcQGetUserSessionBusinessPortal
        //prcQGetUserSessionBusinessApp
       
        ///<inheritdoc/>
        public async Task<BusinessUserSessionDTO> GetBusinessPortalUserSessionInfoAsync(Guid identityUserId, Guid tenantId, string portalKey , int userType) {
            //ToDo:Review:Nitin: use sqlparameters.
            string query = string.Format("exec [prcQGetUserSessionBusinessPortal] '{0}' , '{1}' , '{2}' , {3} ", identityUserId, tenantId, portalKey, userType);
            return _qAppPortalDbContext.Query<BusinessUserSessionDTO>().FromSql(query).ToList().FirstOrDefault();
        }

        ///<inheritdoc/>
        public async Task<List<UserSessionAppDTO>> GetSessionAppDetailsAsync(Guid identityUserId, Guid tenantId, int userType) {
            //ToDo:Review:Nitin: use sqlparameters.
            string query = string.Format("exec [prcQGetUserSessionBusinessApp] '{0}' , '{1}' , '{2}' ", identityUserId, tenantId, userType);
            return _qAppPortalDbContext.Query<UserSessionAppDTO>().FromSql(query).ToList();
        }
    }
}
