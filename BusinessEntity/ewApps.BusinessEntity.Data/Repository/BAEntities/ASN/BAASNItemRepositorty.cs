using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.Data {
  public  class BAASNItemRepositorty:BaseRepository<BAASNItem, BusinessEntityDbContext>, IBAASNItemRepositorty {

        #region Constructor

        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context">ship Db context reference</param>
        /// <param name="sessionManager">Session manager reference</param>
        public BAASNItemRepositorty(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }
        #endregion Constructor

        public IQueryable<BusBAASNItemDTO> GetASNItemListByASNId(Guid asnId) {
            string sql = @"SELECT i.ID, asn.ID AS 'ASNId', asn.ERPASNKey, i.ERPConnectorKey, i.ERPItemKey, i.ItemID, i.ItemName, i.Quantity, i. QuantityUnit, i.UnitPrice, i.UnitPriceFC, i.Unit,
                            TaxCode, TaxPercent, Tax, TotalLC, TotalLCFC, SerialOrBatchNo
                            FROM be.BAASNItem as i
                            INNER JOIN be.BAASN AS asn ON i.ASNId = asn.ID
                            Where asn.ID=@AsnId AND i.Deleted=0";

            SqlParameter asnIdParam = new SqlParameter("AsnId", asnId);
            return _context.BusBAASNItemDTOQuery.FromSql(sql, new object[] { asnIdParam });
        }

        public IQueryable<CustBAASNItemDTO> GetASNItemListByASNIdForCust(Guid asnId)
        {
          string sql = @"SELECT i.ID, asn.ID AS 'ASNId', asn.ERPASNKey, i.ERPConnectorKey, i.ERPItemKey, i.ItemID, i.ItemName, i.Quantity, i. QuantityUnit, i.UnitPrice, i.UnitPriceFC, i.Unit,
                                TaxCode, TaxPercent, Tax, TotalLC, TotalLCFC, SerialOrBatchNo
                                FROM be.BAASNItem as i
                                INNER JOIN be.BAASN AS asn ON i.ASNId = asn.ID
                                Where asn.ID=@AsnId AND i.Deleted=0";

          SqlParameter asnIdParam = new SqlParameter("AsnId", asnId);
          return _context.CustBAASNItemDTOQuery.FromSql(sql, new object[] { asnIdParam });
        }

  }
}
