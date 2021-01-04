/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Rajesh Thakur <rthakur@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

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

    /// <summary>
    /// Provides CRUD operations for customer entity.
    /// </summary>
    public class BACustomerRepository:BaseRepository<BACustomer, BusinessEntityDbContext>, IBACustomerRepository {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BACustomerRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sessionManager">The session manager.</param>
        public BACustomerRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {

        }

        #endregion

        #region Get

        ///<inheritdoc/>
        public async Task<List<BACustomerDTO>> GetCustomerListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {

            string query = string.Format(@"SELECT cust.ID, cust.CreatedBy, cust.CreatedOn, cust.UpdatedOn,
                                    cust.UpdatedBy,cust.Deleted,cust.TenantID,cust.CustomerName,cust.ERPCustomerKey, cust.[Group],cust.FederalTaxID, 
                                    cust.BusinessPartnerTenantId,cust.Tel1,cust.Website,cust.Tel2, cust.MobilePhone,cust.Email,cust.Currency,cust.Status
                                    FROM be.BACustomer as cust where cust.TenantId  = @tenatId And cust.Deleted = @deleted   
                                    ORDER BY cust.CustomerName");

            SqlParameter parameterS = new SqlParameter("@tenatId", tenantId);
            SqlParameter parameterDeleted = new SqlParameter("@deleted", false);

            List<BACustomerDTO> customerDTOs = await GetQueryEntityListAsync<BACustomerDTO>(query, new object[] { parameterS, parameterDeleted });

            return customerDTOs;
        }

        ///<inheritdoc/>
        public async Task<List<BACustomerDTO>> GetCustomerListByStatusAndTenantIdAsync(Guid tenantId, int status, CancellationToken token = default(CancellationToken)) {

            string query = string.Format(@"SELECT cust.ID, cust.CreatedBy, cust.CreatedOn, cust.UpdatedOn,
                                    cust.UpdatedBy,cust.Deleted,cust.TenantID,cust.CustomerName,cust.ERPCustomerKey, cust.[Group],cust.FederalTaxID, 
                                    cust.BusinessPartnerTenantId,cust.Tel1,cust.Website,cust.Tel2, cust.MobilePhone,cust.Email,cust.Currency,cust.Status
                                    FROM be.BACustomer as cust where cust.TenantId  = @tenatId And cust.Deleted = @deleted   
                                    AND cust.Status  = @status 
                                    ORDER BY cust.CustomerName");

            SqlParameter parameterS = new SqlParameter("@tenatId", tenantId);
            SqlParameter parameterDeleted = new SqlParameter("@deleted", false);
            SqlParameter statusDeleted = new SqlParameter("@status", status);

            List<BACustomerDTO> customerDTOs = await GetQueryEntityListAsync<BACustomerDTO>(query, new object[] { parameterS, parameterDeleted, statusDeleted });

            return customerDTOs;
        }

        ///<inheritdoc/>
        public async Task<BACustomerDTO> GetCustomerDetailByIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {

            string query = string.Format(@"SELECT cust.ID, cust.CreatedBy, cust.CreatedOn, cust.UpdatedOn,
                                    cust.UpdatedBy,cust.Deleted,cust.TenantID,cust.CustomerName,cust.ERPCustomerKey, cust.[Group],cust.FederalTaxID, 
                                    cust.BusinessPartnerTenantId,cust.Tel1,cust.Website,cust.Tel2, cust.MobilePhone,cust.Email,cust.Currency,cust.Status
                                    FROM be.BACustomer as cust where cust.Id  = @customerId And cust.Deleted = @deleted  ");

            SqlParameter parameterS = new SqlParameter("@customerId", customerId);
            SqlParameter parameterDeleted = new SqlParameter("@deleted", false);

            BACustomerDTO customerDTO = await GetQueryEntityAsync<BACustomerDTO>(query, new object[] { parameterS, parameterDeleted });
            return customerDTO;

        }

        /// <inheritdoc/>
        public async Task<List<BusCustomerSetUpAppDTO>> GetCustomerListForBizSetupApp(Guid tenantId, bool isDeleted, CancellationToken token = default(CancellationToken)) {
            string sql = @"SELECT cu.ID, cu.CreatedOn, cu.ERPCustomerKey, cu.CustomerName ,cu.BusinessPartnerTenantId,Convert(int,0) as ApplicationCount
                      FROM be.BACustomer cu
                      WHERE cu.TenantId = @TenantId And cu.Deleted = @isDeleted order by (cu.CustomerName)";

            SqlParameter parameters = new SqlParameter("@TenantId", tenantId);
            SqlParameter isdeletedParam = new SqlParameter("@isDeleted", isDeleted);
            object[] param = { parameters, isdeletedParam };
            return await GetQueryEntityListAsync<BusCustomerSetUpAppDTO>(sql, param);
        }

        ///<inheritdoc/>
        public async Task<BusCustomerSetUpAppViewDTO> GetCustomerDetailForBizSetupApp(Guid customerId, CancellationToken token = default(CancellationToken)) {
            
            string query = string.Format(@"SELECT cust.ID, cust.CreatedBy, cust.CreatedOn,'customer' as PartnerType,
                                    cust.Status,cust.CustomerName,cust.ERPCustomerKey,cust.FederalTaxID, 
                                    cust.BusinessPartnerTenantId,cust.Tel1,cust.Website,cust.Tel2, cust.MobilePhone,cust.Email,cust.Currency
                                    FROM be.BACustomer as cust where cust.Id  = @customerId And cust.Deleted = @deleted  ");

            SqlParameter parameterS = new SqlParameter("@customerId", customerId);
            SqlParameter parameterDeleted = new SqlParameter("@deleted", false);

            BusCustomerSetUpAppViewDTO customerDTO = await GetQueryEntityAsync<BusCustomerSetUpAppViewDTO>(query, new object[] { parameterS, parameterDeleted });
            return customerDTO;

        }
        #endregion Get
    }
}

