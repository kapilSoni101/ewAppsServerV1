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

namespace ewApps.BusinessEntity.Data
{

  /// <summary>
  /// This class implements standard database logic and operations for BAVendorContractRepositorty entity.
  /// </summary>
  public class BAVendorContractRepository : BaseRepository<BAVendorContract, BusinessEntityDbContext>, IBAVendorContractRepository
  {

    #region Constructor

    /// <summary>
    /// Constructor initializing the base variables
    /// </summary>
    /// <param name="context">ship Db context reference</param>
    /// <param name="sessionManager">Session manager reference</param>
    public BAVendorContractRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager)
    {
    }

    #endregion

    #region Business Methods

    #region Get Methods

    /// <inheritdoc/>
    public IQueryable<BusBAVendorContractDTO> GetContractListByTenantId(Guid tenantId, ListDateFilterDTO listDateFilterDTO)
    {
      //ToDo: ERPContractKey field to be add in query onces added in database.
      string query = @"Select ID, ERPConnectorKey, ERPContractKey, ERPVendorKey, VendorId, VendorName, ContactPerson, VendorRefNo, 
                            BPCurrency, TelephoneNo, Email, DocumentNo, AgreementMethod, 
                            StartDate, EndDate, BPProject, TerminationDate, SigningDate, [Description], AgreementType, 
                            PaymentTerms, PaymentMethod, ShippingType, ShippingTypeText, Status, StatusText, 
                            Remarks, [Owner], ERPDocNum FROM be.BAVendorContract AS c
                            WHERE c.TenantId=@TenantId AND c.Deleted=@Deleted AND c.StartDate BETWEEN @StartDate AND @EndDate
                            ORDER BY c.ERPVendorKey DESC ";

      SqlParameter tenantIdParam = new SqlParameter("TenantId", tenantId);
      SqlParameter deletedParam = new SqlParameter("Deleted", listDateFilterDTO.Deleted);
      SqlParameter startDateParam = new SqlParameter("StartDate", listDateFilterDTO.FromDate);
      SqlParameter endDateParam = new SqlParameter("EndDate", listDateFilterDTO.ToDate);

      return _context.BusBAVendorContractDTOQuery.FromSql(query, new object[] { tenantIdParam, deletedParam, startDateParam, endDateParam });
    }

    /// <inheritdoc/>
    public async Task<BusBAVendorContractViewDTO> GetContractDetailByContractIdAsync(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken))
    {
      //ToDo: ERPContractKey field to be add in query onces added in database.
      string query = @"Select ERPContractKey, ERPVendorKey, VendorId, VendorName, ContactPerson, VendorRefNo, 
                            BPCurrency, TelephoneNo, Email, DocumentNo, AgreementMethod, 
                            StartDate, EndDate, BPProject, TerminationDate, SigningDate, [Description], AgreementType, 
                            PaymentTerms, PaymentMethod, ShippingType, ShippingTypeText, Status, StatusText, 
                            Remarks, [Owner], ERPDocNum FROM be.BAVendorContract AS c
                            WHERE c.TenantId=@TenantId AND c.Deleted=@Deleted AND c.ID=@ContractId";

      SqlParameter tenantIdParam = new SqlParameter("TenantId", businessTenantId);
      SqlParameter deletedParam = new SqlParameter("Deleted", false);
      SqlParameter contractIdParam = new SqlParameter("ContractId", contractId);

      return await _context.BusBAVendorContractViewDTOQuery.FromSql(query, new object[] { tenantIdParam, deletedParam, contractIdParam }).FirstOrDefaultAsync(cancellationToken);
    }

    #endregion

    #region Vendor Methods

    /// <inheritdoc/>
    public IQueryable<VendorBAContractDTO> GetContractListByTenantIdForVendor(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO)
    {
      string query = @"SELECT c.ID, c.ERPConnectorKey, ERPContractKey, c.ERPVendorKey, VendorId, c.VendorName, ContactPerson, VendorRefNo, 
                                BPCurrency, TelephoneNo, c.Email, DocumentNo, AgreementMethod, 
                                StartDate, EndDate, BPProject, TerminationDate, SigningDate, [Description], AgreementType, 
                                PaymentTerms, PaymentMethod, c.ShippingType, c.ShippingTypeText, c.Status, c.StatusText, 
                                c.Remarks, [Owner], ERPDocNum FROM be.BAVendorContract AS c
                                INNER JOIN be.BAVendor AS vend ON c.VendorId=vend.id 
                                WHERE vend.BusinessPartnerTenantId=@BusinessPartnerTenantId AND c.Deleted=@Deleted AND c.StartDate BETWEEN @StartDate AND @EndDate
                                ORDER BY c.ERPVendorKey DESC ";

      SqlParameter tenantIdParam = new SqlParameter("BusinessPartnerTenantId", businessPartnerTenantId);
      SqlParameter deletedParam = new SqlParameter("Deleted", listDateFilterDTO.Deleted);
      SqlParameter startDateParam = new SqlParameter("StartDate", listDateFilterDTO.FromDate);
      SqlParameter endDateParam = new SqlParameter("EndDate", listDateFilterDTO.ToDate);

      return _context.VendorBAContractDTOQuery.FromSql(query, new object[] { tenantIdParam, deletedParam, startDateParam, endDateParam });
    }

    /// <inheritdoc/>
    public VendorBAContractViewDTO GetContractDetailByContractIdForVendor(Guid businessTenantId, Guid contractId)
    {
      string query = @"Select ERPContractKey, ERPVendorKey, VendorId, VendorName, ContactPerson, VendorRefNo, 
                            BPCurrency, TelephoneNo, Email, DocumentNo, AgreementMethod, 
                            StartDate, EndDate, BPProject, TerminationDate, SigningDate, [Description], AgreementType, 
                            PaymentTerms, PaymentMethod, ShippingType, ShippingTypeText, Status, StatusText, 
                            Remarks, [Owner], ERPDocNum FROM be.BAVendorContract AS c
                            WHERE c.Deleted=@Deleted AND c.ID=@contractId";

      //SqlParameter tenantIdParam = new SqlParameter("TenantId", businessTenantId);
      SqlParameter deletedParam = new SqlParameter("Deleted", false);
      SqlParameter contractIdParam = new SqlParameter("contractId", contractId);

      return _context.VendorBAContractViewDTOQuery.FromSql(query, new object[] { deletedParam, contractIdParam }).FirstOrDefault();
    }


    #endregion

    #endregion
  }
}
