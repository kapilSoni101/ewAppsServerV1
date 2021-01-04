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
    /// This class implements standard database logic and operations for BAContractAttachment entity.
    /// </summary>
    public class BAContractAttachmentRepository:BaseRepository<BAContractAttachment, BusinessEntityDbContext>, IBAContractAttachmentRepository {

        #region Constructor

        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context">ship Db context reference</param>
        /// <param name="sessionManager">Session manager reference</param>
        public BAContractAttachmentRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBAContractAttachmentDTO>> GetContractAttachmentListByContractIdAsync(Guid contractId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ID, ERPConnectorKey, ERPContractAttachmentKey, ContractId, ERPContractKey, Name, [FreeText],
                            AttachmentDate FROM be.BAContractAttachment as da
                            WHERE da.ContractId=@ContractId";

            SqlParameter contractIdParam = new SqlParameter("ContractId", contractId);
            return await _context.BusBAContractAttachmentDTOQuery.FromSql(sql, new object[] { contractIdParam }).ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBAContractAttachmentDTO>> GetContractAttachmentListByContractIdForCustAsync(Guid contractId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ID, ERPConnectorKey, ERPContractAttachmentKey, ContractId, ERPContractKey, Name, [FreeText],
                            AttachmentDate FROM be.BAContractAttachment as da
                            WHERE da.ContractId=@ContractId";

            SqlParameter contractIdParam = new SqlParameter("ContractId", contractId);
            return await _context.CustBAContractAttachmentDTOQuery.FromSql(sql, new object[] { contractIdParam }).ToListAsync(cancellationToken);
        }
    }
}
