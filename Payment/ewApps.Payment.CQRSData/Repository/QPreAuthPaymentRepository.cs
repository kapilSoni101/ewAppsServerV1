using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Payment.DTO;

namespace ewApps.Payment.QData {
    public class QPreAuthPaymentRepository:QBaseRepository<QPaymentDBContext>, IQPreAuthPaymentRepository {

        #region Local         

        #endregion Local

        #region Constructor         

        /// <summary>
        /// Initializes a new instance of the <see cref="QPreAuthPaymentRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of CQRSAppPortalDbContext to executes database operations.</param>        
        public QPreAuthPaymentRepository(QPaymentDBContext context) : base(context) {
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get preauthorization detail by account id.
        /// </summary>
        /// <param name="customerAccountId">Configured account id</param>
        /// <param name="expirationDate">Date to compare with expiration date or pre-auth.</param>
        /// <returns></returns>
        public async Task<PreAuthPaymentDTO> GetPreAuthDetailByCardIdAsync(Guid customerAccountId, DateTime expirationDate, CancellationToken token = default(CancellationToken)) {
            string sql = "SELECT DISTINCT pay.*, custActDtl.AccountJson " +
                      "FROM PAY.PreAuthPayment pay " +
                      "INNER JOIN AP.CustomerAccountDetail custActDtl ON " +
                      "custActDtl.ID = @customerAccountId AND " +
                      "pay.CustomerAccountDetailId = custActDtl.ID AND " +
                      "pay.ExpirationDate >= @expirationDate AND pay.Deleted = 0 ";

            SqlParameter paramActId = new SqlParameter("@customerAccountId", customerAccountId);
            SqlParameter paramExpDate = new SqlParameter("@expirationDate", expirationDate);


            return await GetQueryEntityAsync<PreAuthPaymentDTO>(sql, new object[] { paramActId, paramExpDate }, token);
        }

        public async Task<IList<PreAuthPaymentDetailDQ>> GetFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            string sql = "SELECT DISTINCT pay.ID,pay.Amount  AS AmountPaid,pay.AmountFC  AS AmountPaidFC, pay.RemainingAmount, pay.RemainingAmountFC, pay.CreatedOn AS PaymentDate, " +
                      "pay.Status, pay.Description AS Note, " +
                      "pay.IdentityNumber AS PaymentIdentityNo, cust.CustomerName, pay.NameOnCard, cust.ERPCustomerKey, " +
                      "cust.Currency as CustomerCurrency,AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.PaymentTransectionCurrency, pay.PayeeName , pay.CardNumber AS CustomerAccountNumber, cust.ID AS CustomerId, " +
                      "pay.MaxTotalPaymentCount, pay.CurrentPaymentSequenceNumber, pay.Captured " +
                      "FROM PAY.PreAuthPayment pay " +
                      "INNER JOIN BE.BACustomer cust on cust.TenantId = @tenantId And cust.ID = pay.BACustomerId " +
                      " AND pay.TenantId = @tenantId AND (pay.CreatedOn BETWEEN @FromDate AND @ToDate) " +
                      "INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  " +
                      "INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  ";                    
            sql += " ORDER BY pay.CreatedOn DESC";

            SqlParameter paramPaymentId = new SqlParameter("@tenantId", filter.ID);
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);

            return await GetQueryEntityListAsync<PreAuthPaymentDetailDQ>(sql, new object[] { paramPaymentId, fromDate, toDate });
        }

        #endregion Get

    }
}
