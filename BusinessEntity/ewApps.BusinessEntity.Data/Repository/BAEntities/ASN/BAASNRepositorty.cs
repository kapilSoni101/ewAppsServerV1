using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.Data {
    public class BAASNRepositorty:BaseRepository<BAASN, BusinessEntityDbContext>, IBAASNRepositorty {

        #region Constructor

        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context">ship Db context reference</param>
        /// <param name="sessionManager">Session manager reference</param>
        public BAASNRepositorty(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        #region Business Get Methods

        /// <inhritdoc/>
        public IQueryable<BusBAASNDTO> GetASNListByBusinessTenantId(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, string asnType) {
            string sql = @"SELECT ID, ERPConnectorKey, ERPCustomerKey, CustomerId, CustomerName, ERPASNKey, DeliveryNo, 
                            ShipDate, ExpectedDate, TrackingNo, ShipmentType, ShipmentTypeText, ShipmentPlan, PackagingSlipNo, 
                            TotalAmount, Discount, Freight, Tax, ERPDocNum, LocalCurrency
                            FROM be.BAASN AS asn 
                            WHERE asn.TenantId=@BusinessTenantId AND asn.Deleted=@Deleted AND asn.ExpectedDate BETWEEN @FromDate AND @ToDate ";
            //ToDo: Nitin: Add ASNType filter as available and Excepted Date Filter.
            // Angular has three types of view encapsulation. None, Native, Emulated
            // None: This disables the view encapsulation.
            // Native: This enables the view encapsulation using shadow DOM and will work if browser is supported.
            // Emulated: This is default value of view encapsulation. This enabled the view encapsulation but will work on all browser because angualr will not use
            // Shadow DOM instead it uses the random id generation logic to fix the scope of a style.
            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter fromDateParam = new SqlParameter("FromDate", listDateFilterDTO.FromDate);
            SqlParameter toDateParam = new SqlParameter("ToDate", listDateFilterDTO.ToDate);
            SqlParameter deletedParam = new SqlParameter("Deleted", listDateFilterDTO.Deleted);
            SqlParameter asnTypeParam = new SqlParameter("ASNType", asnType);

            return _context.BusBAASNDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, fromDateParam, toDateParam, deletedParam, asnTypeParam });
        }

        /// <inhritdoc/>
        public async Task<BusBAASNViewDTO> GetASNDetailByASNIdAsync(Guid asnId, string asnType, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ID, ERPConnectorKey, ERPASNKey, ERPCustomerKey, CustomerId, CustomerName, DeliveryNo, 
                            ShipDate, ExpectedDate, TrackingNo, ShipmentType, ShipmentTypeText, ShipmentPlan, PackagingSlipNo, 
                            TotalAmount, Discount, Freight, Tax, ERPDocNum, LocalCurrency
                            FROM be.BAASN AS asn 
                            WHERE asn.ID=@ASNID AND asn.Deleted=0";
            //ToDo: Nitin: Add ASNType filter as available.

            SqlParameter asnIdParam = new SqlParameter("ASNID", asnId);
            SqlParameter asnTypeParam = new SqlParameter("ASNType", asnType);
            return await _context.BusBAASNViewDTOQuery.FromSql(sql, new object[] { asnIdParam, asnTypeParam }).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inhritdoc/>
        public IQueryable<CustBAASNDTO> GetASNListByBusinessTenantIdForCust(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO) {
            string sql = @"SELECT asn.ID, asn.ERPConnectorKey, asn.ERPCustomerKey, CustomerId, asn.CustomerName, ERPASNKey, DeliveryNo, 
                            ShipDate, ExpectedDate, TrackingNo, ShipmentType, ShipmentTypeText, ShipmentPlan, PackagingSlipNo, 
                            TotalAmount, Discount, Freight, Tax, ERPDocNum, LocalCurrency
                            FROM be.BAASN AS asn 
                            INNER JOIN be.BACustomer as c ON asn.CustomerId=c.Id
                            WHERE c.BusinessPartnerTenantId=@BusinessPartnerTenantId AND asn.Deleted=@Deleted AND asn.ExpectedDate BETWEEN @FromDate AND @ToDate";
            // AND asn.ExpectedDate BETWEEN @FromDate AND @ToDate 

            SqlParameter businessTenantIdParam = new SqlParameter("BusinessPartnerTenantId", businessPartnerTenantId);
            SqlParameter fromDateParam = new SqlParameter("FromDate", listDateFilterDTO.FromDate);
            SqlParameter toDateParam = new SqlParameter("ToDate", listDateFilterDTO.ToDate);
            SqlParameter deletedParam = new SqlParameter("Deleted", listDateFilterDTO.Deleted);

            return _context.CustBAASNDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, fromDateParam, toDateParam, deletedParam });
        }

        /// <inhritdoc/>
        public async Task<CustBAASNViewDTO> GetASNDetailByASNIdAsyncForCust(Guid asnId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ID, ERPConnectorKey, ERPASNKey, ERPCustomerKey, CustomerId, CustomerName, DeliveryNo, 
                            ShipDate, ExpectedDate, TrackingNo, ShipmentType, ShipmentTypeText, ShipmentPlan, PackagingSlipNo, 
                            TotalAmount, Discount, Freight, Tax, ERPDocNum, LocalCurrency
                            FROM be.BAASN AS asn 
                            WHERE asn.ID=@ASNID AND asn.Deleted=0";

            SqlParameter asnIdParam = new SqlParameter("ASNID", asnId);
            return await _context.CustBAASNViewDTOQuery.FromSql(sql, new object[] { asnIdParam }).FirstOrDefaultAsync(cancellationToken);
        }

        #endregion
    }
}
