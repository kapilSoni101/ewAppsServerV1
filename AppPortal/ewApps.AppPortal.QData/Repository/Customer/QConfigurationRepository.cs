using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.QData {
    public class QConfigurationRepository:QBaseRepository<QAppPortalDbContext>, IQConfigurationRepository {


        #region Local member
        #endregion Local member

        #region cunstructor
        public QConfigurationRepository(QAppPortalDbContext context) : base(context) {

        }
        #endregion cunstructor

        #region GET Customer Configuration

        /// <summary>
        /// Get configuration details
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<CustConfigurationViewDTO> GetConfigurationDetailAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            //string query = @"SELECT be.BACustomer.ID AS CustomerID, be.BACustomer.CustomerName,be.BACustomer.[Group],be.BACustomer.FederalTaxID,be.BACustomer.Tel1,be.BACustomer.Tel2, be.BACustomer.MobilePhone,be.BaCustomer.Email,be.BACustomer.Website
            //                        FROM be.BACustomer 
            //                 WHERE be.BACustomer.BusinessPartnerTenantId=@customerId";

            string query = @"SELECT be.BACustomer.ID AS CustomerID, be.BACustomer.BusinessPartnerTenantId AS businessPartnerTenantId, be.BACustomer.CustomerName,be.BACustomer.ERPCustomerKey,be.BACustomer.FederalTaxID,be.BACustomer.Tel1,be.BACustomer.Tel2, be.BACustomer.MobilePhone,be.BaCustomer.Email,be.BACustomer.Website,
                                    ap.Customer.Currency AS CurrencyCode,ap.Customer.DecimalPrecision,ap.Customer.DecimalSeperator,ap.Customer.GroupSeperator,ap.Customer.GroupValue,ap.Customer.Language,ap.Customer.TimeZone,ap.Customer.DateTimeFormat,ap.Customer.CanUpdateCurrency
                             FROM be.BACustomer
                             INNER JOIN ap.Customer ON be.BACustomer.BusinessPartnerTenantId = ap.Customer.BusinessPartnerTenantId
                             WHERE be.BACustomer.BusinessPartnerTenantId = @customerId";
            SqlParameter paramTenantId = new SqlParameter("@customerId", customerId);
            return await GetQueryEntityAsync<CustConfigurationViewDTO>(query, new SqlParameter[] { paramTenantId }, token);
        }


        ///<inheritdoc/>
        public async Task<List<CustCustomerAddressDTO>> GetCustomerAddressListByIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            string query = string.Format(@"SELECT custAdd.ID, custAdd.Label,custAdd.StreetNo,custAdd.Street, custAdd.City,custAdd.State, custAdd.ZipCode, custAdd.CustomerId, 
                                    custAdd.Country,custAdd.ObjectType,custAdd.ObjectTypeText ,custAdd.AddressName ,custAdd.Line1 ,custAdd.Line2 ,custAdd.Line3 ,
                                   custAdd.Line1 AS AddressStreet1 ,custAdd.Line2 AS AddressStreet2 ,custAdd.Line3  AS AddressStreet3 
                                    FROM be.BACustomerAddress as custAdd where custAdd.CustomerId  = @customerId And custAdd.Deleted = @deleted   
                                    ORDER BY custAdd.CreatedOn DESC ");
            SqlParameter parameterS = new SqlParameter("@customerId", customerId);
            SqlParameter parameterDeleted = new SqlParameter("@deleted", false);
            List<CustCustomerAddressDTO> customerAddressDTOs = await GetQueryEntityListAsync<CustCustomerAddressDTO>(query, new object[] { parameterS, parameterDeleted });
            return customerAddressDTOs;
        }

        ///<inheritdoc/>
        public async Task<List<CustCustomerContactDTO>> GetCustomerContactListByIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {

            string query = string.Format(@"SELECT custcontact.ID, custcontact.TenantID,custcontact.FirstName,custcontact.CustomerId,
                                    custcontact.LastName, custcontact.Title,custcontact.Position,custcontact.ERPContactKey, 
                                    custcontact.Address,custcontact.Telephone,custcontact.Email
                                    FROM be.BACustomerContact as custcontact where custcontact.CustomerId  = @customerId And custcontact.Deleted = @deleted   
                                    ORDER BY custcontact.CreatedOn DESC ");

            SqlParameter parameterS = new SqlParameter("@customerId", customerId);
            SqlParameter parameterDeleted = new SqlParameter("@deleted", false);
            List<CustCustomerContactDTO> customerContactDTOs = await GetQueryEntityListAsync<CustCustomerContactDTO>(query, new object[] { parameterS, parameterDeleted });

            return customerContactDTOs;
        }

        ///<inheritdoc/>
        public async Task<List<CustomerAccountDTO>> GetCustomerAccListByCustomerIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {

            string query = string.Format(@"SELECT custacc.ID,custacc.AccountType,custacc.AccountJson
                                    FROM ap.CustomerAccountDetail as custacc where custacc.CustomerId  = @customerId And custacc.Deleted = @deleted  ");

            SqlParameter parameterS = new SqlParameter("@customerId", customerId);
            SqlParameter parameterDeleted = new SqlParameter("@deleted", false);

            List<CustomerAccountDTO> customerAccountDTOs = await GetQueryEntityListAsync<CustomerAccountDTO>(query, new object[] { parameterS, parameterDeleted });
            return customerAccountDTOs;
        }

        #endregion GET configuration


        #region Vendor Configuration
        /// <summary>
        /// Get Vendor Configuration details
        /// </summary>
        /// <param name="vendorId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<VendorConfigurationDTO> GetVendorConfigurationDetailAsync(Guid vendorId, CancellationToken token = default(CancellationToken)) {
            //string query = @"SELECT be.BACustomer.ID AS CustomerID, be.BACustomer.CustomerName,be.BACustomer.[Group],be.BACustomer.FederalTaxID,be.BACustomer.Tel1,be.BACustomer.Tel2, be.BACustomer.MobilePhone,be.BaCustomer.Email,be.BACustomer.Website
            //                        FROM be.BACustomer 
            //                 WHERE be.BACustomer.BusinessPartnerTenantId=@customerId";

            string query = @"SELECT be.BAVendor.ID AS VendorId, be.BAVendor.BusinessPartnerTenantId AS businessPartnerTenantId, be.BAVendor.VendorName,be.BAVendor.ERPVendorKey,be.BAVendor.FederalTaxID,be.BAVendor.Tel1,be.BAVendor.Tel2, be.BAVendor.MobilePhone,be.BAVendor.Email,be.BAVendor.Website,
                                    ap.Vendor.Currency AS CurrencyCode,ap.Vendor.DecimalPrecision,ap.Vendor.DecimalSeperator,ap.Vendor.GroupSeperator,ap.Vendor.GroupValue,ap.Vendor.Language,ap.Vendor.TimeZone,ap.Vendor.DateTimeFormat,ap.Vendor.CanUpdateCurrency
                             FROM be.BAVendor
                             INNER JOIN ap.Vendor ON be.BAVendor.BusinessPartnerTenantId = ap.Vendor.BusinessPartnerTenantId
                             WHERE be.BAVendor.BusinessPartnerTenantId = @VendorId";
            SqlParameter paramTenantId = new SqlParameter("@VendorId", vendorId);
            return await GetQueryEntityAsync<VendorConfigurationDTO>(query, new SqlParameter[] { paramTenantId }, token);
        }


        ///<inheritdoc/>
        public async Task<List<VendorAddressDTO>> GetVendorAddressListByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken)) {
            string query = string.Format(@"SELECT vendAdd.ID, vendAdd.Label,vendAdd.StreetNo,vendAdd.Street, vendAdd.City,vendAdd.State, vendAdd.ZipCode, vendAdd.VendorId, 
                                    vendAdd.Country,vendAdd.ObjectType,vendAdd.ObjectTypeText ,vendAdd.AddressName ,vendAdd.Line1 ,vendAdd.Line2 ,vendAdd.Line3 ,
                                   vendAdd.Line1 AS AddressStreet1 ,vendAdd.Line2 AS AddressStreet2 ,vendAdd.Line3  AS AddressStreet3 
                                    FROM be.BAVendorAddress as vendAdd where vendAdd.VendorId  = @vendorId And vendAdd.Deleted = @deleted   
                                    ORDER BY vendAdd.CreatedOn DESC ");
            SqlParameter parameterS = new SqlParameter("@vendorId", vendorId);
            SqlParameter parameterDeleted = new SqlParameter("@deleted", false);
            List<VendorAddressDTO> vendorAddressDTOs = await GetQueryEntityListAsync<VendorAddressDTO>(query, new object[] { parameterS, parameterDeleted });
            return vendorAddressDTOs;
        }

        ///<inheritdoc/>
        public async Task<List<VendorContactDTO>> GetVendorContactListByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken)) {

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
        #endregion

    }
}
