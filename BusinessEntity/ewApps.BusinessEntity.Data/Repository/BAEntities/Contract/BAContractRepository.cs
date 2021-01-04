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
    /// This class implements standard database logic and operations for BAContractRepositorty entity.
    /// </summary>
    public class BAContractRepository:BaseRepository<BAContract, BusinessEntityDbContext>, IBAContractRepository {

        #region Constructor

        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context">ship Db context reference</param>
        /// <param name="sessionManager">Session manager reference</param>
        public BAContractRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        #region Business Methods

        #region Get Methods

        /// <inheritdoc/>
        public IQueryable<BusBAContractDTO> GetContractListByTenantId(Guid tenantId, ListDateFilterDTO listDateFilterDTO) {
            //ToDo: ERPContractKey field to be add in query onces added in database.
            string query = @"Select ID, ERPConnectorKey, ERPContractKey, ERPCustomerKey, CustomerId, CustomerName, ContactPerson, CustomerRefNo, 
                            BPCurrency, TelephoneNo, Email, DocumentNo, AgreementMethod, 
                            StartDate, EndDate, BPProject, TerminationDate, SigningDate, [Description], AgreementType, 
                            PaymentTerms, PaymentMethod, ShippingType, ShippingTypeText, Status, StatusText, 
                            Remarks, [Owner], ERPDocNum FROM be.BAContract AS c
                            WHERE c.TenantId=@TenantId AND c.Deleted=@Deleted AND c.StartDate BETWEEN @StartDate AND @EndDate
                            ORDER BY c.ERPCustomerKey DESC ";

            SqlParameter tenantIdParam = new SqlParameter("TenantId", tenantId);
            SqlParameter deletedParam = new SqlParameter("Deleted", listDateFilterDTO.Deleted);
            SqlParameter startDateParam = new SqlParameter("StartDate", listDateFilterDTO.FromDate);
            SqlParameter endDateParam = new SqlParameter("EndDate", listDateFilterDTO.ToDate);

            return _context.BusBAContractDTOQuery.FromSql(query, new object[] { tenantIdParam, deletedParam, startDateParam, endDateParam });
        }

        /// <inheritdoc/>
        public async Task<BusBAContractViewDTO> GetContractDetailByContractIdAsync(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken)) {
            //ToDo: ERPContractKey field to be add in query onces added in database.
            string query = @"Select ERPContractKey, ERPCustomerKey, CustomerId, CustomerName, ContactPerson, CustomerRefNo, 
                            BPCurrency, TelephoneNo, Email, DocumentNo, AgreementMethod, 
                            StartDate, EndDate, BPProject, TerminationDate, SigningDate, [Description], AgreementType, 
                            PaymentTerms, PaymentMethod, ShippingType, ShippingTypeText, Status, StatusText, 
                            Remarks, [Owner], ERPDocNum FROM be.BAContract AS c
                            WHERE c.TenantId=@TenantId AND c.Deleted=@Deleted AND c.ID=@ContractId";

            SqlParameter tenantIdParam = new SqlParameter("TenantId", businessTenantId);
            SqlParameter deletedParam = new SqlParameter("Deleted", false);
            SqlParameter contractIdParam = new SqlParameter("ContractId", contractId);

            return await _context.BusBAContractViewDTOQuery.FromSql(query, new object[] { tenantIdParam, deletedParam, contractIdParam }).FirstOrDefaultAsync(cancellationToken);
        }

        #endregion

        #region Customer Methods

        /// <inheritdoc/>
        public IQueryable<CustBAContractDTO> GetContractListByTenantIdForCust(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO) {
            string query = @"Select c.ID, c.ERPConnectorKey, ERPContractKey, c.ERPCustomerKey, CustomerId, c.CustomerName, ContactPerson, CustomerRefNo, 
                            BPCurrency, TelephoneNo, c.Email, DocumentNo, AgreementMethod, 
                            StartDate, EndDate, BPProject, TerminationDate, SigningDate, [Description], AgreementType, 
                            PaymentTerms, PaymentMethod, c.ShippingType, c.ShippingTypeText, c.Status, c.StatusText, 
                            c.Remarks, [Owner], ERPDocNum FROM be.BAContract AS c
							INNER JOIN be.BACustomer as cust ON c.CustomerId=cust.id 
                            WHERE cust.BusinessPartnerTenantId=@BusinessPartnerTenantId AND c.Deleted=@Deleted AND c.StartDate BETWEEN @StartDate AND @EndDate
                            ORDER BY c.ERPCustomerKey DESC ";

            SqlParameter tenantIdParam = new SqlParameter("BusinessPartnerTenantId", businessPartnerTenantId);
            SqlParameter deletedParam = new SqlParameter("Deleted", listDateFilterDTO.Deleted);
            SqlParameter startDateParam = new SqlParameter("StartDate", listDateFilterDTO.FromDate);
            SqlParameter endDateParam = new SqlParameter("EndDate", listDateFilterDTO.ToDate);

            return _context.CustBAContractDTOQuery.FromSql(query, new object[] { tenantIdParam, deletedParam, startDateParam, endDateParam });
        }

        /// <inheritdoc/>
        public async Task<CustBAContractViewDTO> GetContractDetailByContractIdAsyncForCust(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken)) {
            string query = @"Select ERPContractKey, ERPCustomerKey, CustomerId, CustomerName, ContactPerson, CustomerRefNo, 
                            BPCurrency, TelephoneNo, Email, DocumentNo, AgreementMethod, 
                            StartDate, EndDate, BPProject, TerminationDate, SigningDate, [Description], AgreementType, 
                            PaymentTerms, PaymentMethod, ShippingType, ShippingTypeText, Status, StatusText, 
                            Remarks, [Owner], ERPDocNum FROM be.BAContract AS c
                            WHERE c.Deleted=@Deleted AND c.ID=@contractId";

            //SqlParameter tenantIdParam = new SqlParameter("TenantId", businessTenantId);
            SqlParameter deletedParam = new SqlParameter("Deleted", false);
            SqlParameter contractIdParam = new SqlParameter("contractId", contractId);

            return await _context.CustBAContractViewDTOQuery.FromSql(query, new object[] { deletedParam, contractIdParam }).FirstOrDefaultAsync(cancellationToken);
        }

        #endregion

        #endregion
    }
}
