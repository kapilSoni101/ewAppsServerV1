/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 30 January 2019
 * 
 * Contributor/s: Ishwar Rathore/Amit Mundra
 * Last Updated On: 30 January 2019
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
    /// Contains supportive method for Address entity.
    /// </summary>
    public class BACustomerAddressRepository:BaseRepository<BACustomerAddress, BusinessEntityDbContext>, IBACustomerAddressRepository {

        #region Constructor 

        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        public BACustomerAddressRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor 
        #region Get
        ///<inheritdoc/>
        public async Task<List<CustomerAddressDTO>> GetCustomerAddressListByIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {

            string query = string.Format(@"SELECT custAdd.ID, custAdd.Label,custAdd.StreetNo,custAdd.Street, custAdd.City,custAdd.State, custAdd.ZipCode, custAdd.CustomerId, 
                                    custAdd.Country,custAdd.ObjectType,custAdd.ObjectTypeText ,custAdd.AddressName ,custAdd.Line1 ,custAdd.Line2 ,custAdd.Line3  ,
custAdd.Line1 AS AddressStreet1, custAdd.Line2 AS AddressStreet2, custAdd.Line3 AS AddressStreet3 
                                    FROM be.BACustomerAddress as custAdd where custAdd.CustomerId  = @customerId And custAdd.Deleted = @deleted   
                                    ORDER BY custAdd.CreatedOn DESC ");

            SqlParameter parameterS = new SqlParameter("@customerId", customerId);
            SqlParameter parameterDeleted = new SqlParameter("@deleted", false);
            List<CustomerAddressDTO> customerAddressDTOs = await GetQueryEntityListAsync<CustomerAddressDTO>(query, new object[] { parameterS, parameterDeleted });
            return customerAddressDTOs;


        }

        /// <summary>
        /// Get customer address entity list.
        /// </summary>
        /// <param name="customerId">Unqique customerid.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BACustomerAddress>> GetCustomerAddressEntityListByIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _context.BACustomerAddress.Where(add => add.Deleted == false && add.CustomerId == customerId).ToListAsync(token);
        }

        #endregion Get

    }
}
