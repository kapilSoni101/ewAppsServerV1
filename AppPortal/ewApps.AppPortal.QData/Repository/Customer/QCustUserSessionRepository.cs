using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.QData {
    public class QCustUserSessionRepository:IQCustUserSessionRepository {

        QAppPortalDbContext _qAppPortalDbContext;

        public QCustUserSessionRepository(QAppPortalDbContext qAppPortalDbContext) {
            _qAppPortalDbContext = qAppPortalDbContext;
        }


        ///<inheritdoc/>
        public async Task<CustomerUserSessionDTO> GetCustomerPortalUserSessionInfoAsync(Guid identityUserId, Guid tenantId, string portalKey, int userType) {
            //ToDo:Review:Nitin: use sqlparameters.
            string query = string.Format("exec [prcQGetUserSessionCustomerPortal] '{0}' , '{1}' , '{2}' , {3} ", identityUserId, tenantId, portalKey, userType);
            return _qAppPortalDbContext.Query<CustomerUserSessionDTO>().FromSql(query).ToList().FirstOrDefault();
        }

        ///<inheritdoc/>
        public async Task<List<UserSessionAppDTO>> GetSessionAppDetailsAsync(Guid identityUserId, Guid tenantId, int userType) {
            //ToDo:Review:Nitin: use sqlparameters.
            string query = string.Format("exec [prcQGetUserSessionCustomerApp] '{0}' , '{1}' , '{2}' ", identityUserId, tenantId, userType);
            return _qAppPortalDbContext.Query<UserSessionAppDTO>().FromSql(query).ToList();
        }
    }
}
