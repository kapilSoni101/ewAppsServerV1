/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal <sagrawal@eworkplaceapps.com>
 * Date: 08 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 22 August 2019
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.BusinessEntity.QData;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.Money;
using ewApps.Core.UniqueIdentityGeneratorService;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// This class implements standard business logic and operations for BACustomer entity.
    /// </summary>
    public class BACustomerDS:BaseDS<BACustomer>, IBACustomerDS {

        #region Local Member

        IBACustomerRepository _customerRepo;
        IBACustomerAddressDS _customerAddressDS;
        IBACustomerContactDS _customerContactDS;
        IBACustomerPaymentDetailDS _customerPaymentDetailDS;
        IUnitOfWork _unitOfWork;
        IQNotificationDS _qNotificationDS;
        IBusinessEntityNotificationHandler _businessEntityNotificationHandler;
        IUniqueIdentityGeneratorDS _identityDataService;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initialinzing local variables .
        /// </summary>
        /// <param name="customerRepo"></param>
        /// <param name="customerAddressDS"></param>
        /// <param name="customerContactDS"></param>
        /// <param name="customerPaymentDetailDS"></param>
        /// <param name="unitOfWork"></param>
        public BACustomerDS(IBACustomerRepository customerRepo, IBACustomerAddressDS customerAddressDS, IBACustomerContactDS customerContactDS,
            IBACustomerPaymentDetailDS customerPaymentDetailDS, IUniqueIdentityGeneratorDS identityDataService, IUnitOfWork unitOfWork, IQNotificationDS qNotificationDS, IBusinessEntityNotificationHandler businessEntityNotificationHandler) : base(customerRepo) {
            _customerRepo = customerRepo;
            _customerAddressDS = customerAddressDS;
            _customerContactDS = customerContactDS;
            _customerPaymentDetailDS = customerPaymentDetailDS;
            _unitOfWork = unitOfWork;
            _qNotificationDS = qNotificationDS;
            _identityDataService = identityDataService;
            _businessEntityNotificationHandler = businessEntityNotificationHandler;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get customer list.
        /// </summary>
        /// <param name="tenantId">Tenant id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BACustomerDTO>> GetCustomerListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _customerRepo.GetCustomerListByTenantIdAsync(tenantId, token);
        }

        /// <summary>
        /// Get customer list.
        /// </summary>
        /// <param name="tenantId">TenantId</param>
        /// <param name="status">Status of customer</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BACustomerDTO>> GetCustomerListByStatusAndTenantIdAsync(Guid tenantId, int status, CancellationToken token = default(CancellationToken)) {
            return await _customerRepo.GetCustomerListByStatusAndTenantIdAsync(tenantId, status, token);
        }

        /// <summary>
        /// Get customer list.
        /// </summary>
        /// <param name="CustomerId">Tenant id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BusBACustomerDetailDTO> GetCustomerDetailByIdAsync(Guid CustomerId, CancellationToken token = default(CancellationToken)) {
            BusBACustomerDetailDTO customerDetailDTO = new BusBACustomerDetailDTO();

            BACustomerDTO customer = await _customerRepo.GetCustomerDetailByIdAsync(CustomerId, token);

            List<CustomerAddressDTO> customerAddressDTOs = await _customerAddressDS.GetCustomerAddressListByIdAsync(CustomerId, token);

            List<CustomerContactDTO> customerContactDTOs = await _customerContactDS.GetCustomerContactListByIdAsync(CustomerId, token);
            if(customerAddressDTOs != null) {
                foreach(CustomerAddressDTO custAddress in customerAddressDTOs) {
                    if(custAddress.ObjectTypeText.Equals("B")) {
                        customerDetailDTO.BillToAddressList.Add(custAddress);
                    }
                    else {
                        customerDetailDTO.ShipToAddressList.Add(custAddress);
                    }

                }
            }
            customerDetailDTO.Customer = customer;
            customerDetailDTO.CustomerContactList = customerContactDTOs;

            return customerDetailDTO;
        }

        /// <summary>
        /// Get customer list.
        /// </summary>
        /// <param name="tenantId">Tenant id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BusCustomerSetUpAppDTO>> GetCustomerListForBizSetupApp(Guid tenantId, bool isDeleted, CancellationToken token = default(CancellationToken)) {
            return await _customerRepo.GetCustomerListForBizSetupApp(tenantId, isDeleted, token);
        }

        /// <summary>
        /// Get customer list.
        /// </summary>
        /// <param name="CustomerId">Tenant id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BusCustomerSetUpAppViewDTO> GetCustomerDetailForBizSetupApp(Guid CustomerId, CancellationToken token = default(CancellationToken)) {
            BusCustomerSetUpAppViewDTO customerDetailDTO = new BusCustomerSetUpAppViewDTO();

            customerDetailDTO = await _customerRepo.GetCustomerDetailForBizSetupApp(CustomerId, token);

            List<CustomerAddressDTO> customerAddressDTOs = await _customerAddressDS.GetCustomerAddressListByIdAsync(CustomerId, token);

            List<CustomerContactDTO> customerContactDTOs = await _customerContactDS.GetCustomerContactListByIdAsync(CustomerId, token);
            if(customerAddressDTOs != null) {
                foreach(CustomerAddressDTO custAddress in customerAddressDTOs) {
                    if(custAddress.ObjectTypeText.Equals("B")) {
                        customerDetailDTO.BillToAddressList.Add(custAddress);
                    }
                    else {
                        customerDetailDTO.ShipToAddressList.Add(custAddress);
                    }

                }
            }
            customerDetailDTO.CustomerContactList = customerContactDTOs;

            return customerDetailDTO;
        }


        #region Get Methods for cust

        /// <inheritdoc/>
        public async Task<CustBACustomerDetailDTO> GetCustomerDetailByIdAsyncForCust(Guid CustomerId, CancellationToken token = default(CancellationToken)) {
            CustBACustomerDetailDTO customerDetailDTO = new CustBACustomerDetailDTO();

            BACustomerDTO customer = await _customerRepo.GetCustomerDetailByIdAsync(CustomerId, token);

            List<CustomerAddressDTO> customerAddressDTOs = await _customerAddressDS.GetCustomerAddressListByIdAsync(CustomerId, token);

            List<CustomerContactDTO> customerContactDTOs = await _customerContactDS.GetCustomerContactListByIdAsync(CustomerId, token);

            if(customerAddressDTOs != null) {

                foreach(CustomerAddressDTO custAddress in customerAddressDTOs) {

                    if(custAddress.ObjectTypeText.Equals("B")) {
                        customerDetailDTO.BillToAddressList.Add(custAddress);
                    }
                    else {
                        customerDetailDTO.ShipToAddressList.Add(custAddress);
                    }

                }
            }
            customerDetailDTO.Customer = customer;
            customerDetailDTO.CustomerContactList = customerContactDTOs;

            return customerDetailDTO;
        }

        #endregion Get Methods for Cust

        #endregion Get

        public async Task<bool> UpdateCustomerDetail(BACustomerDTO custDetailDTO, CancellationToken token = default(CancellationToken)) {

            Guid customerId = custDetailDTO.ID;
            BACustomer customer = await FindAsync(cust => cust.ID == customerId);
            customer.CustomerName = custDetailDTO.CustomerName;
            customer.FederalTaxID = custDetailDTO.FederalTaxID;
            customer.Email = custDetailDTO.Email;
            customer.MobilePhone = custDetailDTO.MobilePhone;
            customer.Tel1 = custDetailDTO.Tel1;
            customer.Tel2 = custDetailDTO.Tel2;
            customer.Website = custDetailDTO.Website;

            UpdateSystemFieldsByOpType(customer, OperationType.Update);
            await UpdateAsync(customer, customerId, token);
            // Save Data
            _unitOfWork.SaveAll();
            try {
                List<string> customerAddERPKeyList = new List<string>();
                customerAddERPKeyList.Add(customer.ERPCustomerKey);
                OnUpdateCustomerInIntegratedModeAsync(customerAddERPKeyList, customer.TenantId, (long)BusinessEntityNotificationEventEnum.BizUpdateCutsomer);
            }
            catch(Exception ex) {
                // throw ex;
            }

            return true;

        }

        /// <summary>
        /// Update customer and customercontact list.
        /// </summary>
        /// <param name="custContactDetailDTO">Detail object of customer and contact list.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCustomerAndContactDetailAsync(BusBACustomerDetailDTO custContactDetailDTO, CancellationToken token = default(CancellationToken)) {
            BACustomerDTO custDetailDTO = custContactDetailDTO.Customer;
            Guid customerId = custDetailDTO.ID;
            BACustomer customer = await FindAsync(cust => cust.ID == customerId, token);
            customer.CustomerName = custDetailDTO.CustomerName;
            customer.FederalTaxID = custDetailDTO.FederalTaxID;
            customer.Email = custDetailDTO.Email;
            customer.MobilePhone = custDetailDTO.MobilePhone;
            customer.Tel1 = custDetailDTO.Tel1;
            customer.Tel2 = custDetailDTO.Tel2;
            customer.Website = custDetailDTO.Website;

            UpdateSystemFieldsByOpType(customer, OperationType.Update);
            await UpdateAsync(customer, customerId, token);
            // Updateing customer contact.
            await UpdateCustomerContactAsync(custContactDetailDTO.CustomerContactList, customer, customer.TenantId, token);
            // Add/update customer address.
            await AddUpdateCustomerAddressAsync(custContactDetailDTO, customer, customer.TenantId, token);
            // Save Data
            _unitOfWork.SaveAll();
            try {
                List<string> customerAddERPKeyList = new List<string>();
                customerAddERPKeyList.Add(customer.ERPCustomerKey);
                OnUpdateCustomerInIntegratedModeAsync(customerAddERPKeyList, customer.TenantId, (long)BusinessEntityNotificationEventEnum.BizUpdateCutsomer);
            }
            catch(Exception ex) {
                // throw ex;
            }

            return true;
        }

        /// <summary>
        /// add/Update customer address list.
        /// </summary>
        /// <param name="custDetailDTO"></param>
        /// <param name="customer"></param>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task AddUpdateCustomerAddressAsync(BusBACustomerDetailDTO custDetailDTO, BACustomer customer, Guid tenantId, CancellationToken token) {
            List<BACustomerAddress> existingCustomerAddressList = await _customerAddressDS.GetCustomerAddressEntityListByIdAsync(customer.ID, token);

            List<CustomerAddressDTO> listAddressDTO = new List<CustomerAddressDTO>();
            if(custDetailDTO.BillToAddressList != null) {
                listAddressDTO = custDetailDTO.BillToAddressList;
            }
            if(custDetailDTO.ShipToAddressList != null) {
                listAddressDTO.AddRange(custDetailDTO.ShipToAddressList);
            }

            foreach(var item in listAddressDTO) {
                item.CustomerId = customer.ID;
                BACustomerAddress customerAddress = existingCustomerAddressList.Find(i => item.ID == i.ID);
                if(customerAddress != null) {
                    customerAddress = CustomerAddressDTO.CopyToEntity(item, customerAddress);
                    _customerAddressDS.UpdateSystemFieldsByOpType(customerAddress, OperationType.Update);
                    await _customerAddressDS.UpdateAsync(customerAddress, customerAddress.ID, token);
                }
                else {
                    int contactKeyidentity = _identityDataService.GetIdentityNo(tenantId, (int)EntityTypeEnum.BACustomerAddress, BusinessEntityConstants.CustomerPrefix, 1000);
                    customerAddress = new BACustomerAddress();
                    customerAddress = CustomerAddressDTO.CopyToEntity(item, customerAddress);
                    customerAddress.ID = Guid.NewGuid();
                    customerAddress.ERPCustomerKey = customer.ERPCustomerKey;
                    customerAddress.ERPConnectorKey = customer.ERPConnectorKey;
                    _customerAddressDS.UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
                    await _customerAddressDS.AddAsync(customerAddress);
                }
            }
            foreach(var item in existingCustomerAddressList) {
                CustomerAddressDTO customerContact = listAddressDTO.Find(i => item.ID == i.ID);
                if(customerContact == null) {
                    _customerAddressDS.UpdateSystemFieldsByOpType(item, OperationType.Update);
                    item.Deleted = true;
                    await _customerAddressDS.UpdateAsync(item, item.ID, token);
                }
            }

        }

        /// <summary>
        /// Update customer contact list.
        /// </summary>
        /// <param name="CustomerContactList"></param>
        /// <param name="customer"></param>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdateCustomerContactAsync(List<CustomerContactDTO> CustomerContactList, BACustomer customer, Guid tenantId, CancellationToken token) {
            List<BACustomerContact> existingCustomerContactList = await _customerContactDS.GetCustomerContactListByCustomerIdAsync(customer.ID, token);
            if(CustomerContactList != null) {
                foreach(var item in CustomerContactList) {
                    item.CustomerId = customer.ID;
                    BACustomerContact customerContact = existingCustomerContactList.Find(i => item.ID == i.ID);
                    if(customerContact != null) {

                        customerContact = CustomerContactDTO.CopyToEntity(item, customerContact, customer.ERPCustomerKey, customer.ERPConnectorKey);
                        _customerContactDS.UpdateSystemFieldsByOpType(customerContact, OperationType.Update);
                        await _customerContactDS.UpdateAsync(customerContact, customerContact.ID, token);
                    }
                    else {
                        int contactKeyidentity = _identityDataService.GetIdentityNo(tenantId, (int)EntityTypeEnum.BACustomerContact, BusinessEntityConstants.CustomerPrefix, 1000);
                        customerContact = CustomerContactDTO.MapToEntity(item, customer.ERPCustomerKey, customer.ERPConnectorKey);
                        customerContact.ID = Guid.NewGuid();
                        customerContact.ERPContactKey = contactKeyidentity.ToString();
                        customerContact.ERPConnectorKey = customer.ERPConnectorKey;
                        customerContact.Deleted = false;
                        _customerContactDS.UpdateSystemFieldsByOpType(customerContact, OperationType.Add);
                        await _customerContactDS.AddAsync(customerContact, token);
                    }
                }
                foreach(var item in existingCustomerContactList) {
                    CustomerContactDTO customerContact = CustomerContactList.Find(i => item.ID == i.ID);
                    if(customerContact == null) {
                        _customerContactDS.UpdateSystemFieldsByOpType(item, OperationType.Update);
                        item.Deleted = true;
                        await _customerContactDS.UpdateAsync(item, item.ID, token);
                    }
                }
            }
        }

        public async Task<bool> UpdateCustomerDetailForBizSetupApp(BusCustomerUpdateDTO custDetailDTO, CancellationToken token = default(CancellationToken)) {

            Guid customerId = custDetailDTO.ID;
            BACustomer customer = await FindAsync(cust => cust.ID == customerId);
            customer.CustomerName = custDetailDTO.CustomerName;
            customer.FederalTaxID = custDetailDTO.FederalTaxID;
            customer.Email = custDetailDTO.Email;
            customer.MobilePhone = custDetailDTO.MobilePhone;
            customer.Tel1 = custDetailDTO.Tel1;
            customer.Tel2 = custDetailDTO.Tel2;
            customer.Website = custDetailDTO.Website;
            customer.Status = custDetailDTO.Status;

            UpdateSystemFieldsByOpType(customer, OperationType.Update);
            await UpdateAsync(customer, customerId, token);
            await UpdateCustomerContactAsync(custDetailDTO.CustomerContactList, customer, customer.TenantId, token);
            await this.AddUpdateCustomerSetupAddressAsync(custDetailDTO, customer, customer.TenantId, token);

            // Save Data
            _unitOfWork.SaveAll();
            try {
                List<string> customerAddERPKeyList = new List<string>();
                customerAddERPKeyList.Add(customer.ERPCustomerKey);
                OnUpdateCustomerInIntegratedModeAsync(customerAddERPKeyList, customer.TenantId, (long)BusinessEntityNotificationEventEnum.BizUpdateCutsomer);
            }
            catch(Exception ex) {
                // throw ex;
            }

            return true;

        }

        /// <summary>
        /// add/Update customer address list.
        /// </summary>
        /// <param name="custDetailDTO"></param>
        /// <param name="customer"></param>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task AddUpdateCustomerSetupAddressAsync(BusCustomerUpdateDTO custDetailDTO, BACustomer customer, Guid tenantId, CancellationToken token) {
            List<BACustomerAddress> existingCustomerAddressList = await _customerAddressDS.GetCustomerAddressEntityListByIdAsync(customer.ID, token);

            List<CustomerAddressDTO> listAddressDTO = new List<CustomerAddressDTO>();
            if(custDetailDTO.BillToAddressList != null) {
                listAddressDTO = custDetailDTO.BillToAddressList;
            }
            if(custDetailDTO.ShipToAddressList != null) {
                listAddressDTO.AddRange(custDetailDTO.ShipToAddressList);
            }

            foreach(var item in listAddressDTO) {
                item.CustomerId = customer.ID;
                BACustomerAddress customerAddress = existingCustomerAddressList.Find(i => item.ID == i.ID);
                if(customerAddress != null) {
                    customerAddress = CustomerAddressDTO.CopyToEntity(item, customerAddress);
                    _customerAddressDS.UpdateSystemFieldsByOpType(customerAddress, OperationType.Update);
                    await _customerAddressDS.UpdateAsync(customerAddress, customerAddress.ID, token);
                }
                else {
                    int contactKeyidentity = _identityDataService.GetIdentityNo(tenantId, (int)EntityTypeEnum.BACustomerAddress, BusinessEntityConstants.CustomerPrefix, 1000);
                    customerAddress = new BACustomerAddress();
                    customerAddress = CustomerAddressDTO.CopyToEntity(item, customerAddress);
                    customerAddress.ID = Guid.NewGuid();
                    customerAddress.ERPCustomerKey = customer.ERPCustomerKey;
                    customerAddress.ERPConnectorKey = customer.ERPConnectorKey;
                    _customerAddressDS.UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
                    await _customerAddressDS.AddAsync(customerAddress);
                }
            }
            foreach(var item in existingCustomerAddressList) {
                CustomerAddressDTO customerContact = listAddressDTO.Find(i => item.ID == i.ID);
                if(customerContact == null) {
                    _customerAddressDS.UpdateSystemFieldsByOpType(item, OperationType.Update);
                    item.Deleted = true;
                    await _customerAddressDS.UpdateAsync(item, item.ID, token);
                }
            }

        }

        #region Notification


        private void OnUpdateCustomerInIntegratedModeAsync(List<string> customerErpKeyList, Guid businessTenantId, long bizNotificationEnum) {
            for(int i = 0; i < customerErpKeyList.Count; i++) {
                CustomerNotificationDTO customerNotificationDTO = _qNotificationDS.GetCustomerDetailByCustomerERPKeyAsync(customerErpKeyList[i], businessTenantId).Result;
                _businessEntityNotificationHandler.SendUpdateCustomerInIntegratedMode(customerNotificationDTO, bizNotificationEnum);
            }
        }

        #endregion


        #region PUT


        /// <summary>
        /// Update Customer Details
        /// </summary>
        /// <param name="custConfigurationUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseModelDTO> BACustomerUpdateAsync(CustConfigurationUpdateDTO custConfigurationUpdateDTO, CancellationToken token = default(CancellationToken)) {

            BACustomer bACustomer = new BACustomer();
            ResponseModelDTO responseModelDTO = new ResponseModelDTO();

            bACustomer = _customerRepo.Get(custConfigurationUpdateDTO.CustomerID);

            bACustomer.CustomerName = custConfigurationUpdateDTO.CustomerName;
            // bACustomer.BusinessPartnerTenantId = custConfigurationUpdateDTO.CustomerID;
            bACustomer.FederalTaxID = custConfigurationUpdateDTO.FederalTaxID;
            bACustomer.Tel1 = custConfigurationUpdateDTO.Tel1;
            bACustomer.Tel2 = custConfigurationUpdateDTO.Tel2;
            bACustomer.MobilePhone = custConfigurationUpdateDTO.MobilePhone;
            bACustomer.Email = custConfigurationUpdateDTO.Email;
            bACustomer.Website = custConfigurationUpdateDTO.Website;

      if (!string.IsNullOrEmpty(custConfigurationUpdateDTO.CurrencyCode))
      {
        int currency = Convert.ToInt32(custConfigurationUpdateDTO.CurrencyCode);
        //var invcc = new CurrencyCultureInfoTable(null).GetCultureInfo((CurrencyISOCode)currency);
        bACustomer.Currency = PicklistHelper.GetCurrencySymbolById(currency);
      }
      UpdateSystemFieldsByOpType(bACustomer, OperationType.Update);

      await ManageCustomerSetupAddressAsync(custConfigurationUpdateDTO, bACustomer, bACustomer.TenantId, token);

      await UpdateCustomerContactAsync(custConfigurationUpdateDTO.CustomerContactList, bACustomer, bACustomer.TenantId, token);

      // Update BA Customer detail
      await UpdateAsync(bACustomer, bACustomer.ID, token);
      _unitOfWork.Save();

            try {
                List<string> customerAddERPKeyList = new List<string>();
                customerAddERPKeyList.Add(bACustomer.ERPCustomerKey);
                OnUpdateCustomerInIntegratedModeAsync(customerAddERPKeyList, bACustomer.TenantId, (long)BusinessEntityNotificationEventEnum.BizUpdateCutsomer);
            }
            catch(Exception ex) {
                // throw ex;
            }

            return responseModelDTO;

    }
    /// <summary>
    /// add/Update customer address list.
    /// </summary>
    /// <param name="custDetailDTO"></param>
    /// <param name="customer"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task ManageCustomerSetupAddressAsync(CustConfigurationUpdateDTO custDetailDTO, BACustomer customer, Guid tenantId, CancellationToken token) {
      List<BACustomerAddress> existingCustomerAddressList = await _customerAddressDS.GetCustomerAddressEntityListByIdAsync(customer.ID, token);

      //List<CustomerAddressDTO> listAddressDTO = new List<CustomerAddressDTO>();
      //if (custDetailDTO.BillToAddressList != null)
      //{
      //  listAddressDTO = custDetailDTO.BillToAddressList;
      //}
      //if (custDetailDTO.ShipToAddressList != null)
      //{
      //  listAddressDTO.AddRange(custDetailDTO.ShipToAddressList);
      //}
      foreach (var item in custDetailDTO.CustomerAddressList) {

        item.CustomerId = customer.ID;
        BACustomerAddress customerAddress = existingCustomerAddressList.Find(i => item.ID == i.ID);
        if (customerAddress != null)
        {
          customerAddress = CustomerAddressDTO.CopyToEntity(item, customerAddress);
          _customerAddressDS.UpdateSystemFieldsByOpType(customerAddress, OperationType.Update);
          await _customerAddressDS.UpdateAsync(customerAddress, customerAddress.ID, token);
        }
        else
        {
          int contactKeyidentity = _identityDataService.GetIdentityNo(tenantId, (int)EntityTypeEnum.BACustomerAddress, BusinessEntityConstants.CustomerPrefix, 1000);
          customerAddress = new BACustomerAddress();
          customerAddress = CustomerAddressDTO.CopyToEntity(item, customerAddress);
          customerAddress.ID = Guid.NewGuid();
          customerAddress.ERPCustomerKey = customer.ERPCustomerKey;
          customerAddress.ERPConnectorKey = customer.ERPConnectorKey;
          _customerAddressDS.UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
          await _customerAddressDS.AddAsync(customerAddress);
        }
      }
      foreach (var item in existingCustomerAddressList)
      {
        CustomerAddressDTO customerContact = custDetailDTO.CustomerAddressList.Find(i => item.ID == i.ID);
        if (customerContact == null)
        {
          _customerAddressDS.UpdateSystemFieldsByOpType(item, OperationType.Update);
          item.Deleted = true;
          await _customerAddressDS.UpdateAsync(item, item.ID, token);
        }
      }

    }

    #endregion PUT

        #region Delete

        /// <summary>
        /// Method will delete customer and its associated data.
        /// </summary>
        /// <param name="baCustomerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task DeleteCustomerAsync(Guid baCustomerId, CancellationToken token = default(CancellationToken)) {
            BACustomer customer = await _customerRepo.GetAsync(baCustomerId, token);
            if(customer != null) {
                customer.Deleted = true;
                base.UpdateSystemFieldsByOpType(customer, OperationType.Update);
                await _customerRepo.UpdateAsync(customer, baCustomerId, token);

                await _customerContactDS.DeleteCustomerAsync(baCustomerId, false, token);

                await _customerAddressDS.DeleteCustomerAddressAsync(baCustomerId, false, token);

                _unitOfWork.SaveAll();
            }
        }

        #endregion Delete
    }
}
