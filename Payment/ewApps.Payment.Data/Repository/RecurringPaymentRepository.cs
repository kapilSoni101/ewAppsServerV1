/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 25 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 25 February 2019
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.CommonService;
using ewApps.Core.UserSessionService;
using ewApps.Payment.Entity;
using ewApps.Core.DbConProvider;
using ewApps.Core.BaseService;
using ewApps.Payment.DTO;

namespace ewApps.Payment.Data {

    /// <summary>
    /// Dataclass for recurring payment.
    /// </summary>
    public class RecurringPaymentRepository:BaseRepository<RecurringPayment, PaymentDbContext>, IRecurringPaymentRepository {

        public RecurringPaymentRepository(PaymentDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #region Get

        /// <summary>
        /// Get recurring payment schedule by tenantid.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<RecurringPaymentViewDTO>> GetRecurrtingPaymentAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string sql = "SELECT rp.*, cust.Name AS CustomerName FROM RecurringPayment rp INNER JOIN Customer cust ON cust.ID = rp.CustomerId And cust.TenantId = @tenantId And rp.Deleted = 0";
            SqlParameter param = new SqlParameter("@tenantId", tenantId);
            return await GetQueryEntityListAsync<RecurringPaymentViewDTO>(sql, parameters: new SqlParameter[] { param });
        }

        /// <summary>
        /// Get recurring payment schedule by customerid.
        /// </summary>
        /// <param name="customerid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<RecurringPaymentViewDTO>> GetRecurrtingPaymentByCustomerAsync(Guid customerid, CancellationToken token = default(CancellationToken)) {
            string sql = "SELECT rp.*, cust.Name AS CustomerName FROM RecurringPayment rp INNER JOIN Customer cust ON cust.ID = rp.CustomerId And cust.ID = @customerid AND rp.Deleted = 0";
            SqlParameter param = new SqlParameter("@customerid", customerid);
            return await GetQueryEntityListAsync<RecurringPaymentViewDTO>(sql, parameters: new SqlParameter[] { param });
        }

        /// <summary>
        /// Get recurring payment schedule by customerid and order.
        /// </summary>
        /// <param name="customerid"></param>
        /// <param name="orderId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<RecurringPaymentViewDTO>> GetRecurrtingPaymentByCustomerOrderAsync(Guid customerid, string orderId, CancellationToken token = default(CancellationToken)) {
            string sql = "SELECT rp.*, cust.Name AS CustomerName FROM RecurringPayment rp INNER JOIN Customer cust ON cust.ID = rp.CustomerId And cust.ID = @customerid And rp.OrderId = @orderId";
            SqlParameter param = new SqlParameter("@customerid", customerid);
            SqlParameter orderParam = new SqlParameter("@orderId", orderId);
            return await GetQueryEntityListAsync<RecurringPaymentViewDTO>(sql, parameters: new SqlParameter[] { param });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recurringId"></param>
        /// <returns></returns>
        public async Task<RecurringDTO> GetRecurringModelById(Guid recurringId) {
            /*
             await _context.RoleLinking.Join(_context.Role, rl => rl.RoleId, r => r.ID, (rl, r) => 
                  new { r.AppId, r.ID, rl.AppUserId }).Where(a => a.AppId == appId && a.AppUserId == appUserId)
                  .Select(a => a.ID)
             */

            return _context.RecurringPayment.Where(rec => rec.ID == recurringId).Select(
                rec => new RecurringDTO() {
                    ID = rec.ID,
                    RecurringPeriod = rec.RecurringPeriod,
                    RecurringTerms = rec.RecurringTerms,
                    TermAmount = rec.TermAmount,
                    StartDate = rec.StartDate,
                    EndDate = rec.EndDate,
                    TotalAmount = rec.TotalAmount,
                    Status = rec.Status,
                    CustomerAccountId = rec.CustomerAccountId,
                    CreatedOn = rec.CreatedOn,
                    CreatedBy = rec.CreatedBy,
                    RemainingTermCount = rec.RemainingTermCount,
                    RecurringPaymentDetail = (JsonSerializer.DeSerialize<RecurringPaymentDTO>(rec.Payload))
                }
                ).FirstOrDefault();
        }

        /// <summary>
        /// Checking whether orderid exist.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="tenantId"></param>
        /// <param name="recId"></param>
        /// <returns></returns>
        public bool IsOrderIdExist(string orderId, Guid tenantId, Guid recId) {
            if(tenantId != Guid.Empty) {
                if(_context.RecurringPayment.Any(o => o.ID != recId && o.TenantId == tenantId && o.OrderId == orderId))
                    return true;
            }
            else {
                if(_context.RecurringPayment.Any(o => o.ID != recId && o.OrderId == orderId))
                    return true;
            }
            return false;
        }

        #endregion Get

    }
}
