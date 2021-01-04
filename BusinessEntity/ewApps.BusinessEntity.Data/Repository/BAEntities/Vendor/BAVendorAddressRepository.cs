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

public class BAVendorAddressRepository:BaseRepository<BAVendorAddress, BusinessEntityDbContext>, IBAVendorAddressRepository {

	#region Constructor

    /// <summary>
    /// Constructor initializing the base variables
    /// </summary>
    /// <param name="context"></param>
    public BAVendorAddressRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    }

    #endregion

    #region Get
    ///<inheritdoc/>
    public async Task<List<VendorAddressDTO>> GetVendorAddressListByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken))
    {

      string query = string.Format(@"SELECT vendAdd.ID, vendAdd.Label,vendAdd.StreetNo,vendAdd.Street, vendAdd.City,vendAdd.State, vendAdd.ZipCode, vendAdd.VendorId, 
                                    vendAdd.Country,vendAdd.ObjectType,vendAdd.ObjectTypeText ,vendAdd.AddressName ,vendAdd.Line1 ,vendAdd.Line2 ,vendAdd.Line3  ,
vendAdd.Line1 AS AddressStreet1, vendAdd.Line2 AS AddressStreet2, vendAdd.Line3 AS AddressStreet3 
                                    FROM be.BAVendorAddress as vendAdd where vendAdd.VendorId  = @vendorId And vendAdd.Deleted = @deleted   
                                    ORDER BY vendAdd.CreatedOn DESC ");

      SqlParameter parameterS = new SqlParameter("@vendorId", vendorId);
      SqlParameter parameterDeleted = new SqlParameter("@deleted", false);
      List<VendorAddressDTO> vendorAddressDTOs = await GetQueryEntityListAsync<VendorAddressDTO>(query, new object[] { parameterS, parameterDeleted });
      return vendorAddressDTOs;


    }

    /// <summary>
    /// Get customer address entity list.
    /// </summary>
    /// <param name="vendorId">Unqique customerid.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<List<BAVendorAddress>> GetVendorAddressEntityListByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken))
    {
      return await _context.BAVendorAddress.Where(add => add.Deleted == false && add.VendorId == vendorId).ToListAsync(token);
    }

    #endregion Get

  }

}