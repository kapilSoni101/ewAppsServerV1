/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Sourabh Agrawal<sagrawal @eworkplaceapps.com>
 * Date:05 september 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.Data {

    /// <summary>
    /// This class implements standard database logic and operations for BAContractItem entity.
    /// </summary>
    public class BAContractItemRepository:BaseRepository<BAContractItem, BusinessEntityDbContext>, IBAContractItemRepository {
        
        #region Constructor

        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context">ship Db context reference</param>
        /// <param name="sessionManager">Session manager reference</param>
        public BAContractItemRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }


        #endregion

        #region Get

        /// <inheritdoc/>
        public IQueryable<BusBAContractItemDTO> GetContractItemListByContractId(Guid businessTenantId, Guid contractId) {
            string sql = @"SELECT ERPItemKey, ERPConnectorKey, ContractId, ItemNo, ItemDescription, ItemGroup, PlannedQuantity, UnitPrice, 
                            CumulativeCommittedQuantity, CumulativeCommittedAmount, CumulativeAmountLC, 
                            CumulativeQuantity, OpenQuantity, OpenAmountLC, Project, [FreeText], EndOfWarranty, 
                            UoMCode, UoMName, UoMGroup, ItemsPerUnit, PortionofReturnsPerc, ShippingType, ShippingTypeText, 
                            ItemRowStatus, ItemRowStatusText FROM be.BAContractItem WHERE ContractId=@ContractId";
            SqlParameter contractIdParam = new SqlParameter("ContractId",contractId);
          return  _context.BusBAContractItemDTOQuery.FromSql(sql, new object[] { contractIdParam });
        }

        /// <inheritdoc/>
        public IQueryable<CustBAContractItemDTO> GetContractItemListByContractIdForCust(Guid businessTenantId, Guid contractId)
        {
          string sql = @"SELECT ERPConnectorKey, ContractId, ItemNo, ItemDescription, ItemGroup, PlannedQuantity, UnitPrice, 
                            CumulativeCommittedQuantity, CumulativeCommittedAmount, CumulativeAmountLC, 
                            CumulativeQuantity, OpenQuantity, OpenAmountLC, Project, [FreeText], EndOfWarranty, 
                            UoMCode, UoMName, UoMGroup, ItemsPerUnit, PortionofReturnsPerc, ShippingType, ShippingTypeText, 
                            ItemRowStatus, ItemRowStatusText FROM be.BAContractItem WHERE ContractId=@ContractId";
          SqlParameter contractIdParam = new SqlParameter("ContractId", contractId);
          return _context.CustBAContractItemDTOQuery.FromSql(sql, new object[] { contractIdParam });
        }


    #endregion
  }
}
