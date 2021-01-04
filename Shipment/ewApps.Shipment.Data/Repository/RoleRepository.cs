/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Linq;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Shipment.Data;
using ewApps.Shipment.Entity;
using Microsoft.EntityFrameworkCore;

namespace ewApps.Shipment.Data {

    /// <summary>
    /// This class implements standard database logic and operations for Role entity.
    /// </summary>
    public class RoleRepository:BaseRepository<Role, ShipmentDbContext>, IRoleRepository {

        #region Constructor

        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        ///  <param name="connectionManager"></param>
        public RoleRepository(ShipmentDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        #region Get

        /////<inheritdoc/>
        //public async Task<Role> GetAdminRoleIdByAppIdAndUserTypeAsync(Guid appId, int userType, CancellationToken token = default(CancellationToken)) {
        //    return await _context.Role.Where(role => role.RoleKey == Constants.AdminRoleKey && role.AppId == appId && role.UserType == userType).FirstOrDefaultAsync(token);
        //}

        ///<inheritdoc/>
        public async Task<Guid?> GetRoleAsync(long maskingBit, Guid? appId, int userType) {
            Role role = await _context.Role.Where(r => r.PermissionBitMask == maskingBit && r.AppId == appId && r.UserType == userType).FirstOrDefaultAsync<Role>();
            if(role != null) {
                return role.ID;
            }
            else {
                return null;
            }
        }

        /////<inheritdoc/>
        //public Role GetEntityByAppUserAndAppId(Guid appId, Guid tenantUserId) {
        //    //string sql =
        //    //" select r.Id,r.CreatedBy,r.CreatedOn,r.UpdatedBy,r.UpdatedOn,r.Deleted,r.RoleName, " +
        //    //" r.RoleKey,r.AppId,r.Active,r.PermissionBitMask,r.UserType " +
        //    //" from Role r inner join RoleLinking rl " +
        //    //" on r.id = rl.RoleId " +
        //    //" where rl.AppId = '{0}' and rl.TenantUserId = '{1}'";

        //    //sql = string.Format(sql, appId.ToString(), tenantUserId.ToString());

        //    return _context.Role.Join(
        //    _context.RoleLinking, r => r.ID, rl => rl.RoleId, (r, rl) => new { r, rl }
        //    ).Where(k => k.rl.AppId == appId && k.rl.TenantUserId == tenantUserId).Select(p => p.r).FirstOrDefault();
        //}

        #endregion Get

    }
}
