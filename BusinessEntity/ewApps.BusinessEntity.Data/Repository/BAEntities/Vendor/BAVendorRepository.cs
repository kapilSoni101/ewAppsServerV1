using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.BusinessEntity.Data {

public class BAVendorRepository:BaseRepository<BAVendor, BusinessEntityDbContext>, IBAVendorRepository {

	#region Constructor

    /// <summary>
    /// Constructor initializing the base variables
    /// </summary>
    /// <param name="context"></param>
    public BAVendorRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    }

    #endregion
    //<inheritdoc/>
    public async Task<List<BusVendorDTO>> GetVendorListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      string query = string.Format(@"SELECT vend.ID, vend.CreatedBy, vend.CreatedOn, vend.UpdatedOn,
                                    vend.UpdatedBy,vend.Deleted,vend.TenantID,vend.VendorName,vend.ERPVendorKey, vend.[Group],vend.FederalTaxID, 
                                    vend.BusinessPartnerTenantId,vend.Tel1,vend.Website,vend.Tel2, vend.MobilePhone,vend.Email,vend.Currency,vend.Status
                                    FROM be.BAVendor as vend where vend.TenantId  = @tenatId And vend.Deleted = @deleted   
                                    ORDER BY vend.VendorName");

      SqlParameter parameterS = new SqlParameter("@tenatId", tenantId);
      SqlParameter parameterDeleted = new SqlParameter("@deleted", false);

      List<BusVendorDTO> vendorDTOs = await GetQueryEntityListAsync<BusVendorDTO>(query, new object[] { parameterS, parameterDeleted });

      return vendorDTOs;
    }

    ///<inheritdoc/>
    public async Task<List<BusVendorDTO>> GetVendorListByStatusAndTenantIdAsync(Guid tenantId, int status, CancellationToken token = default(CancellationToken))
    {

      string query = string.Format(@"SELECT vend.ID, vend.CreatedBy, vend.CreatedOn, vend.UpdatedOn,
                                    vend.UpdatedBy,vend.Deleted,vend.TenantID,vend.VendorName,cust.ERPVendorKey, vend.[Group],vend.FederalTaxID, 
                                    vend.BusinessPartnerTenantId,vend.Tel1,vend.Website,vend.Tel2, vend.MobilePhone,vend.Email,vend.Currency,vend.Status
                                    FROM be.BAVendor as vend where vend.TenantId  = @tenatId And vend.Deleted = @deleted   
                                    AND vend.Status  = @status 
                                    ORDER BY vend.VendorName");

      SqlParameter parameterS = new SqlParameter("@tenatId", tenantId);
      SqlParameter parameterDeleted = new SqlParameter("@deleted", false);
      SqlParameter statusDeleted = new SqlParameter("@status", status);

      List<BusVendorDTO> vendorDTOs = await GetQueryEntityListAsync<BusVendorDTO>(query, new object[] { parameterS, parameterDeleted, statusDeleted });

      return vendorDTOs;
    }

    ///<inheritdoc/>
    public async Task<BusVendorDTO> GetVendorDetailByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken))
    {

      string query = string.Format(@"SELECT vend.ID, vend.CreatedBy, vend.CreatedOn, vend.UpdatedOn,
                                    vend.UpdatedBy,vend.Deleted,vend.TenantID,vend.VendorName,vend.ERPVendorKey, vend.[Group],vend.FederalTaxID, 
                                    vend.BusinessPartnerTenantId,vend.Tel1,vend.Website,vend.Tel2, vend.MobilePhone,vend.Email,vend.Currency,vend.Status
                                    FROM be.BAVendor as vend where vend.Id  = @vendorId And vend.Deleted = @deleted  ");

      SqlParameter parameterS = new SqlParameter("@vendorId", vendorId);
      SqlParameter parameterDeleted = new SqlParameter("@deleted", false);

      BusVendorDTO vendorDTO = await GetQueryEntityAsync<BusVendorDTO>(query, new object[] { parameterS, parameterDeleted });
      return vendorDTO;

    }
    /// <inheritdoc/>
    public async Task<List<BusVendorSetUpAppDTO>> GetVendorListForBizSetupApp(Guid tenantId, bool isDeleted, CancellationToken token = default(CancellationToken))
    {
      string sql = @"SELECT vend.ID, vend.CreatedOn, vend.ERPVendorKey, vend.VendorName ,vend.BusinessPartnerTenantId,Convert(int,0) as ApplicationCount
                      FROM be.BAVendor vend
                      WHERE vend.TenantId = @TenantId And vend.Deleted = @isDeleted order by (vend.VendorName)";

      SqlParameter parameters = new SqlParameter("@TenantId", tenantId);
      SqlParameter isdeletedParam = new SqlParameter("@isDeleted", isDeleted);
      object[] param = { parameters, isdeletedParam };
      return await GetQueryEntityListAsync<BusVendorSetUpAppDTO>(sql, param);
    }

    ///<inheritdoc/>
    public async Task<BusVendorSetUpAppViewDTO> GetVendorDetailForBizSetupApp(Guid vendorId, CancellationToken token = default(CancellationToken))
    {

      string query = string.Format(@"SELECT vend.ID, vend.CreatedBy, vend.CreatedOn,'vendor' as PartnerType,
                                    vend.Status,vend.VendorName,vend.ERPVendorKey,vend.FederalTaxID, 
                                    vend.BusinessPartnerTenantId,vend.Tel1,vend.Website,vend.Tel2, vend.MobilePhone,vend.Email,vend.Currency
                                    FROM be.BAVendor as vend where vend.Id  = @vendorId And vend.Deleted = @deleted  ");

      SqlParameter parameterS = new SqlParameter("@vendorId", vendorId);
      SqlParameter parameterDeleted = new SqlParameter("@deleted", false);

      BusVendorSetUpAppViewDTO customerDTO = await GetQueryEntityAsync<BusVendorSetUpAppViewDTO>(query, new object[] { parameterS, parameterDeleted });
      return customerDTO;

    }
  }

}