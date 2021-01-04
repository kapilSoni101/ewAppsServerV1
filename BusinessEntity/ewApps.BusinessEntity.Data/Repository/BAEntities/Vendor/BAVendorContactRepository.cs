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
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.Data
{

  public class BAVendorContactRepository : BaseRepository<BAVendorContact, BusinessEntityDbContext>, IBAVendorContactRepository
  {

    #region Constructor

    /// <summary>
    /// Constructor initializing the base variables
    /// </summary>
    /// <param name="context"></param>
    public BAVendorContactRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager)
    {
    }

    #endregion

    ///<inheritdoc/>
    public async Task<List<VendorContactDTO>> GetVendorContactListByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken))
    {

      string query = string.Format(@"SELECT vendcontact.ID, vendcontact.TenantID,vendcontact.FirstName,vendcontact.VendorId,
                                    vendcontact.LastName, vendcontact.Title,vendcontact.Position,vendcontact.ERPContactKey, 
                                    vendcontact.Address,vendcontact.Telephone,vendcontact.Email
                                    FROM be.BAVendorContact as vendcontact where vendcontact.VendorId  = @vendorId And vendcontact.Deleted = @deleted   
                                    ORDER BY vendcontact.CreatedOn DESC ");

      SqlParameter parameterS = new SqlParameter("@vendorId", vendorId);
      SqlParameter parameterDeleted = new SqlParameter("@deleted", false);

      List<VendorContactDTO> vendorContactDTOs = await GetQueryEntityListAsync<VendorContactDTO>(query, new object[] { parameterS, parameterDeleted });

      return vendorContactDTOs;
    }

    ///<inheritdoc/>
    public async Task<List<BAVendorContact>> GetVendorContactListByVendorIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken))
    {
      return await _context.BAVendorContact.Where(ba => ba.VendorId == vendorId).ToListAsync(token);


    }

  }
}