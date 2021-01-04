using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessEntity.DTO;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;


namespace ewApps.BusinessEntity.DS
{

  /// <summary>
  /// This class implements standard business logic and operations for BAVendor entity.
  /// </summary>
  public class BAVendorDS : BaseDS<BAVendor>, IBAVendorDS
  {

    #region Local Member 

    IBAVendorRepository _repository;
    IUserSessionManager _sessionmanager;
    private IBAVendorAddressDS _vendAddressDS;
    private IBAVendorContactDS _vendContactDS;
    private IUnitOfWork _unitOfWork;
    private BusinessEntityAppSettings _appSettings;
    IUniqueIdentityGeneratorDS _identityDataService;

    #endregion

    #region Constructor

    /// <summary>
    /// Initialinzing local variables
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="sessionmanager"></param>
    public BAVendorDS(IBAVendorRepository repository, IBAVendorAddressDS vendAddressDS, IBAVendorContactDS vendContactDS,
 IUnitOfWork unitOfWork, IUserSessionManager sessionmanager, IUniqueIdentityGeneratorDS identityDataService, IOptions<BusinessEntityAppSettings> appSettings) : base(repository)
    {
      _repository = repository;
      _vendAddressDS = vendAddressDS;
      _vendContactDS = vendContactDS;
      _unitOfWork = unitOfWork;
      _sessionmanager = sessionmanager;
      _identityDataService = identityDataService;
            _appSettings = appSettings.Value;
    }

    #endregion Constructor

    #region Get
    /// <summary>
    /// Get customer list.
    /// </summary>
    /// <param name="tenantId">Tenant id.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<List<BusVendorDTO>> GetVendorListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      return await _repository.GetVendorListByTenantIdAsync(tenantId, token);
    }

    /// <summary>
    /// Get customer list.
    /// </summary>
    /// <param name="tenantId">TenantId</param>
    /// <param name="status">Status of customer</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<List<BusVendorDTO>> GetVendorListByStatusAndTenantIdAsync(Guid tenantId, int status, CancellationToken token = default(CancellationToken))
    {
      return await _repository.GetVendorListByStatusAndTenantIdAsync(tenantId, status, token);
    }

    /// <summary>
    /// Get customer list.
    /// </summary>
    /// <param name="vendorId">Tenant id.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<BusVendorDetailDTO> GetVendorDetailByIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken))
    {
      BusVendorDetailDTO vendorDetailDTO = new BusVendorDetailDTO();

      BusVendorDTO vendor = await _repository.GetVendorDetailByIdAsync(vendorId, token);

      List<VendorAddressDTO> vendorAddressDTOs = await _vendAddressDS.GetVendorAddressListByIdAsync(vendorId, token);

      List<VendorContactDTO> vendorContactDTOs = await _vendContactDS.GetVendorContactListByIdAsync(vendorId, token);
      if (vendorAddressDTOs != null)
      {
        foreach (VendorAddressDTO vendAddress in vendorAddressDTOs)
        {
          if (vendAddress.ObjectTypeText.Equals("B"))
          {
            vendorDetailDTO.BillToAddressList.Add(vendAddress);
          }
          else
          {
            vendorDetailDTO.ShipToAddressList.Add(vendAddress);
          }

        }
      }
      vendorDetailDTO.Vendor = vendor;
      vendorDetailDTO.VendorContactList = vendorContactDTOs;

      return vendorDetailDTO;
    }
    /// <summary>
    /// Get customer list.
    /// </summary>
    /// <param name="tenantId">Tenant id.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<List<BusVendorSetUpAppDTO>> GetVendorListForBizSetupApp(Guid tenantId, bool isDeleted, CancellationToken token = default(CancellationToken))
    {
      return await _repository.GetVendorListForBizSetupApp(tenantId, isDeleted, token);
    }

    /// <summary>
    /// Get customer list.
    /// </summary>
    /// <param name="CustomerId">Tenant id.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<BusVendorSetUpAppViewDTO> GetVendorDetailForBizSetupApp(Guid vendorId, CancellationToken token = default(CancellationToken))
    {
      BusVendorSetUpAppViewDTO vendorDetailDTO = new BusVendorSetUpAppViewDTO();

      vendorDetailDTO = await _repository.GetVendorDetailForBizSetupApp(vendorId, token);

      List<VendorAddressDTO> vendorAddressDTOs = await _vendAddressDS.GetVendorAddressListByIdAsync(vendorId, token);

      List<VendorContactDTO> vendorContactDTOs = await _vendContactDS.GetVendorContactListByIdAsync(vendorId, token);
      if (vendorAddressDTOs != null)
      {
        foreach (VendorAddressDTO vendAddress in vendorAddressDTOs)
        {
          if (vendAddress.ObjectTypeText.Equals("B"))
          {
            vendorDetailDTO.BillToAddressList.Add(vendAddress);
          }
          else
          {
            vendorDetailDTO.ShipToAddressList.Add(vendAddress);
          }

        }
      }
      vendorDetailDTO.VendorContactList = vendorContactDTOs;

      return vendorDetailDTO;
    }

    #endregion Get

    #region Add/Update/Delete

    ///<inheritdoc/>
    public async Task<bool> AddVendorListAsync(List<BAVendorSyncDTO> vendorDetailList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken))
    {
      List<BAVendor> vendorList = new List<BAVendor>();
      List<string> vendorrAddERPKeyList = new List<string>();
      List<string> vendorUpdateERPKeyList = new List<string>();
      // SingUp Customer On BusinessEntity
      for (int i = 0; i < vendorDetailList.Count; i++)
      {
        if (isBulkInsert)
        {
          BAVendor baVendor = await AddVendorAsync(vendorDetailList[i], tenantId, tenantUserId);
          if (baVendor != null)
          {
            vendorList.Add(baVendor);
          }
        }
        else
        {
          if (vendorDetailList[i].OpType.Equals("Inserted"))
          {
            BAVendor baVendor = await AddVendorAsync(vendorDetailList[i], tenantId, tenantUserId);
            if (baVendor != null)
            {
              vendorList.Add(baVendor);
              vendorrAddERPKeyList.Add(baVendor.ERPVendorKey);
            }
          }
          else if (vendorDetailList[i].OpType.Equals("Modified"))
          {
            vendorUpdateERPKeyList.Add(vendorDetailList[i].ERPVendorKey);
            await UpdateVendorAsync(vendorDetailList[i], tenantId, tenantUserId);
          }
        }
      }
      // SingUp Vendor On App Portal
      try
      {
        await VendorSingUpOnAppPortalAsync(vendorList, token);
      }
      catch (Exception ex)
      {
        StringBuilder exceptionDetail = new StringBuilder();
        exceptionDetail.Append("Exception occurred in InitDB-CustomerSignUpDS:-");
        exceptionDetail.AppendLine();
        exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
        Log.Error(ex, exceptionDetail.ToString());
        // TODO: Logo Error
        // TODO: Rollback transaction
        throw;
      }
      // Save Data
      _unitOfWork.SaveAll();
      //if (customerAddERPKeyList.Count > 0)
      //{
      //  OnAddCustomerInIntegratedModeAsync(customerAddERPKeyList, tenantId, (long)BusinessEntityNotificationEventEnum.BizAddCutsomer);
      //}
      //if (customerUpdateERPKeyList.Count > 0)
      //{
      //  OnUpdateCustomerInIntegratedModeAsync(customerUpdateERPKeyList, tenantId, (long)BusinessEntityNotificationEventEnum.BizUpdateCutsomer);
      //}
      return true;
    }


    /// <summary>
    /// Signs up.
    /// </summary>
    /// <param name="customerSyncDTO">The customer synchronize dto.</param>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="tenantUserId">The tenant user identifier.</param>
    /// <param name="token">The token.</param>
    /// <returns></returns>
    public async Task<BAVendor> AddVendorAsync(BAVendorSyncDTO vendorSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {

      try
      {


        // Validate Duplicate Vendor
        BAVendor vendor = await FindAsync(vend => vend.ERPVendorKey == vendorSyncDTO.ERPVendorKey && vend.TenantId == tenantId);
        if (vendor != null)
        {
          return null;
        }

        // Add Customer
        Guid businessPartnerTenantId = Guid.NewGuid();
        Guid VendorId = Guid.NewGuid();
        vendorSyncDTO.BusinessPartnerTenantId = businessPartnerTenantId;
        vendor = BAVendorSyncDTO.MapToEntity(vendorSyncDTO);
        vendor.ID = VendorId;
        vendor.CreatedBy = tenantUserId;//;// Session
        vendor.UpdatedBy = tenantUserId;
        vendor.CreatedOn = DateTime.UtcNow;
        vendor.UpdatedOn = DateTime.UtcNow;
        vendor.Deleted = false;
        vendor.TenantId = tenantId;
        await AddAsync(vendor, token);

        //customerList.Add(customer);

        // Add Customer Address
        if (vendorSyncDTO.VendorAddressList != null && vendorSyncDTO.VendorAddressList.Count > 0)
        {
          foreach (var item in vendorSyncDTO.VendorAddressList)
          {
            item.VendorId = VendorId;
            BAVendorAddress vendorAddress = BAVendorAddressSyncDTO.MapToEntity(item);
            vendorAddress.ID = Guid.NewGuid();
            vendorAddress.CreatedBy = tenantUserId;//;// Session
            vendorAddress.UpdatedBy = tenantUserId;
            vendorAddress.CreatedOn = DateTime.UtcNow;
            vendorAddress.UpdatedOn = DateTime.UtcNow;
            vendorAddress.Deleted = false;
            vendorAddress.TenantId = tenantId;
            await _vendAddressDS.AddAsync(vendorAddress);
          }
        }

        // Add Customer Contact
        if (vendorSyncDTO.VendorContactList != null && vendorSyncDTO.VendorContactList.Count > 0)
        {
          foreach (var item in vendorSyncDTO.VendorContactList)
          {
            item.VendorId = VendorId;
            BAVendorContact vendorContact = BAVendorContactSyncDTO.MapToEntity(item);
            vendorContact.ID = Guid.NewGuid();
            vendorContact.CreatedBy = tenantUserId;//;// Session
            vendorContact.UpdatedBy = tenantUserId;
            vendorContact.CreatedOn = DateTime.UtcNow;
            vendorContact.UpdatedOn = DateTime.UtcNow;
            vendorContact.Deleted = false;
            vendorContact.TenantId = tenantId;
            await _vendContactDS.AddAsync(vendorContact);
          }
        }

        return vendor;
      }
      catch (Exception ex)
      {
        return null;
        // throw;
      }
    }

    /// <summary>
    /// Signs up.
    /// </summary>
    /// <param name="customerSyncDTO">The customer synchronize dto.</param>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="tenantUserId">The tenant user identifier.</param>
    /// <param name="token">The token.</param>
    /// <returns></returns>
    public async Task<BAVendor> UpdateVendorAsync(BAVendorSyncDTO vendorSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {

      try
      {

        BAVendor vendor = await FindAsync(vend => vend.ERPVendorKey == vendorSyncDTO.ERPVendorKey && vend.TenantId == tenantId);

        vendor = BAVendorSyncDTO.MapToEntity(vendorSyncDTO, vendor);
        UpdateSystemFieldsByOpType(vendor, OperationType.Update);


        // update invoice detail
        await UpdateAsync(vendor, vendor.ID, token);
        //customerList.Add(customer);

        // Add Customer Address
        if (vendorSyncDTO.VendorAddressList != null && vendorSyncDTO.VendorAddressList.Count > 0)
        {
          foreach (var item in vendorSyncDTO.VendorAddressList)
          {
            item.VendorId = vendor.ID;
            BAVendorAddress vendorAddress = BAVendorAddressSyncDTO.MapToEntity(item);
            vendorAddress.ID = Guid.NewGuid();
            vendorAddress.CreatedBy = tenantUserId;//;// Session
            vendorAddress.UpdatedBy = tenantUserId;
            vendorAddress.CreatedOn = DateTime.UtcNow;
            vendorAddress.UpdatedOn = DateTime.UtcNow;
            vendorAddress.Deleted = false;
            vendorAddress.TenantId = tenantId;
            await _vendAddressDS.AddAsync(vendorAddress);
          }
        }

        // Add Customer Contact
        if (vendorSyncDTO.VendorContactList != null && vendorSyncDTO.VendorContactList.Count > 0)
        {
          foreach (var item in vendorSyncDTO.VendorContactList)
          {
            item.VendorId = vendor.ID;
            BAVendorContact vendorContact = BAVendorContactSyncDTO.MapToEntity(item);
            vendorContact.ID = Guid.NewGuid();
            vendorContact.CreatedBy = tenantUserId;//;// Session
            vendorContact.UpdatedBy = tenantUserId;
            vendorContact.CreatedOn = DateTime.UtcNow;
            vendorContact.UpdatedOn = DateTime.UtcNow;
            vendorContact.Deleted = false;
            vendorContact.TenantId = tenantId;
            await _vendContactDS.AddAsync(vendorContact);
          }
        }

        return vendor;
      }
      catch (Exception ex)
      {
        return null;
        // throw;
      }

    }

    public async Task<bool> UpdateVendorDetailForBizSetupApp(BusVendorUpdateDTO vendDetailDTO, CancellationToken token = default(CancellationToken))
    {

      Guid vendorId = vendDetailDTO.ID;
      BAVendor vendor = await FindAsync(vend => vend.ID == vendorId);
      vendor.VendorName = vendDetailDTO.VendorName;
      vendor.FederalTaxID = vendDetailDTO.FederalTaxID;
      vendor.Email = vendDetailDTO.Email;
      vendor.MobilePhone = vendDetailDTO.MobilePhone;
      vendor.Tel1 = vendDetailDTO.Tel1;
      vendor.Tel2 = vendDetailDTO.Tel2;
      vendor.Website = vendDetailDTO.Website;
      vendor.Status = vendDetailDTO.Status;

      UpdateSystemFieldsByOpType(vendor, OperationType.Update);
      await UpdateAsync(vendor, vendorId, token);
      await UpdateVendorContactAsync(vendDetailDTO.VendorContactList, vendor, vendor.TenantId, token);
      await this.AddUpdateVendorSetupAddressAsync(vendDetailDTO, vendor, vendor.TenantId, token);

      // Save Data
      _unitOfWork.SaveAll();
      //try
      //{
      //  List<string> customerAddERPKeyList = new List<string>();
      //  customerAddERPKeyList.Add(customer.ERPCustomerKey);
      //  OnUpdateCustomerInIntegratedModeAsync(customerAddERPKeyList, customer.TenantId, (long)BusinessEntityNotificationEventEnum.BizUpdateCutsomer);
      //}
      //catch (Exception ex)
      //{
      //  // throw ex;
      //}

      return true;

    }

    /// <summary>
    /// add/Update customer address list.
    /// </summary>
    /// <param name="vendDetailDTO"></param>
    /// <param name="vendor"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task AddUpdateVendorSetupAddressAsync(BusVendorUpdateDTO vendDetailDTO, BAVendor vendor, Guid tenantId, CancellationToken token)
    {
      List<BAVendorAddress> existingVendorAddressList = await _vendAddressDS.GetVendorAddressEntityListByIdAsync(vendor.ID, token);

      List<VendorAddressDTO> listAddressDTO = new List<VendorAddressDTO>();
      if (vendDetailDTO.BillToAddressList != null)
      {
        listAddressDTO = vendDetailDTO.BillToAddressList;
      }
      if (vendDetailDTO.ShipToAddressList != null)
      {
        listAddressDTO.AddRange(vendDetailDTO.ShipToAddressList);
      }

      foreach (var item in listAddressDTO)
      {
        item.VendorId = vendor.ID;
        BAVendorAddress vendorAddress = existingVendorAddressList.Find(i => item.ID == i.ID);
        if (vendorAddress != null)
        {
          vendorAddress = VendorAddressDTO.CopyToEntity(item, vendorAddress);
          _vendAddressDS.UpdateSystemFieldsByOpType(vendorAddress, OperationType.Update);
          await _vendAddressDS.UpdateAsync(vendorAddress, vendorAddress.ID, token);
        }
        else
        {
          int contactKeyidentity = _identityDataService.GetIdentityNo(tenantId, (int)EntityTypeEnum.BACustomerAddress, BusinessEntityConstants.CustomerPrefix, 1000);
          vendorAddress = new BAVendorAddress();
          vendorAddress = VendorAddressDTO.CopyToEntity(item, vendorAddress);
          vendorAddress.ID = Guid.NewGuid();
          vendorAddress.ERPVendorKey = vendor.ERPVendorKey;
          vendorAddress.ERPConnectorKey = vendor.ERPConnectorKey;
          _vendAddressDS.UpdateSystemFieldsByOpType(vendorAddress, OperationType.Add);
          await _vendAddressDS.AddAsync(vendorAddress);
        }
      }
      foreach (var item in existingVendorAddressList)
      {
        VendorAddressDTO vendorContact = listAddressDTO.Find(i => item.ID == i.ID);
        if (vendorContact == null)
        {
          _vendAddressDS.UpdateSystemFieldsByOpType(item, OperationType.Update);
          item.Deleted = true;
          await _vendAddressDS.UpdateAsync(item, item.ID, token);
        }
      }

    }
    /// <summary>
    /// Update customer contact list.
    /// </summary>
    /// <param name="vendorContactList"></param>
    /// <param name="vendor"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task UpdateVendorContactAsync(List<VendorContactDTO> vendorContactList, BAVendor vendor, Guid tenantId, CancellationToken token)
    {
      List<BAVendorContact> existingVendorContactList = await _vendContactDS.GetVendorContactListByVendorIdAsync(vendor.ID, token);
      if (vendorContactList != null)
      {
        foreach (var item in vendorContactList)
        {
          item.VendorId = vendor.ID;
          BAVendorContact vendorContact = existingVendorContactList.Find(i => item.ID == i.ID);
          if (vendorContact != null)
          {

            vendorContact = VendorContactDTO.CopyToEntity(item, vendorContact, vendor.ERPVendorKey, vendor.ERPConnectorKey);
            _vendContactDS.UpdateSystemFieldsByOpType(vendorContact, OperationType.Update);
            await _vendContactDS.UpdateAsync(vendorContact, vendorContact.ID, token);
          }
          else
          {
            int contactKeyidentity = _identityDataService.GetIdentityNo(tenantId, (int)EntityTypeEnum.BACustomerContact, BusinessEntityConstants.CustomerPrefix, 1000);
            vendorContact = VendorContactDTO.MapToEntity(item, vendor.ERPVendorKey, vendor.ERPConnectorKey);
            vendorContact.ID = Guid.NewGuid();
            vendorContact.ERPContactKey = contactKeyidentity.ToString();
            vendorContact.ERPConnectorKey = vendor.ERPConnectorKey;
            vendorContact.Deleted = false;
            _vendContactDS.UpdateSystemFieldsByOpType(vendorContact, OperationType.Add);
            await _vendContactDS.AddAsync(vendorContact, token);
          }
        }
        foreach (var item in existingVendorContactList)
        {
          VendorContactDTO vendorContact = vendorContactList.Find(i => item.ID == i.ID);
          if (vendorContact == null)
          {
            _vendContactDS.UpdateSystemFieldsByOpType(item, OperationType.Update);
            item.Deleted = true;
            await _vendContactDS.UpdateAsync(item, item.ID, token);
          }
        }
      }
    }

    #endregion Add/Update/Delete

    #region Private Methods

    private async Task VendorSingUpOnAppPortalAsync(List<BAVendor> vendorList, CancellationToken token = default(CancellationToken))
    {
      if (vendorList.Count <= 0)
      {
        return;
      }

      List<VendorSignUpDTO> vendorSignUpDTOs = new List<VendorSignUpDTO>();
      for (int i = 0; i < vendorList.Count; i++)
      {
        VendorSignUpDTO request = new VendorSignUpDTO();
        request.TenantId = vendorList[i].TenantId;
        request.BusinesPartnerTenantId = vendorList[i].BusinessPartnerTenantId;
        request.Currency = Convert.ToString(PicklistHelper.GetCurrencyIdBySymbol(vendorList[i].Currency));
        request.CutomerName = vendorList[i].VendorName;
        request.BusinesPrimaryUserId = vendorList[i].CreatedBy;
        vendorSignUpDTOs.Add(request);
      }

      string baseUrl = _appSettings.AppPortalApiUrl;
      string relativeUrl = "vendor/signup";

      UserSession session = _sessionmanager.GetSession();

      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, relativeUrl, vendorSignUpDTOs, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);
      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
      var response = await httpRequestProcessor.ExecuteAsync<CustomerSignUpResDTO>(requestOptions, false);
    }

    #endregion Private Methods

  }
}