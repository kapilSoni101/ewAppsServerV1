using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;


namespace ewApps.AppPortal.DS
{
  public class VendorSignUpDS : IVendorSignUpDS
  {

    #region Local Member

    IVendorDS _vendorDS;
    IBusinessDS _businessDS;
    IUnitOfWork _unitOfWork;
    IUserSessionManager _userSessionManager;
    private AppPortalAppSettings _appSettings;

    #endregion Local Member

    #region Constructor

    /// <summary>
    /// Initialinzing local variables .
    /// </summary>
    /// <param name="customerDS"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="appSettings"></param>
    public VendorSignUpDS(IVendorDS vendorDS, IBusinessDS businessDS, IUnitOfWork unitOfWork, IUserSessionManager userSessionManager, IOptions<AppPortalAppSettings> appSettings)
    {
      _vendorDS = vendorDS;
      _businessDS = businessDS;
      _unitOfWork = unitOfWork;
      _userSessionManager = userSessionManager;
      _appSettings = appSettings.Value;

    }

    #endregion Constructor

    #region Signup Customer

    /// <summary>
    /// Signup Customer .
    /// <summary>
    /// </summary>
    /// <param name="customerSignUpDTO"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<VendorSignUpResDTO> VendorSignUpAsync(List<VendorSignUpReqDTO> vendorSignUpDTOs, CancellationToken token = default(CancellationToken))
    {

      VendorSignUpResDTO response;

      // Add Customer Ext;
      for (int i = 0; i < vendorSignUpDTOs.Count; i++)
      {
        await AddVendorExt(vendorSignUpDTOs[i], false);
      }
      // Call Singup on App Mgmt 
      try
      {
        response = await TenantSignUpInAppMgmt(vendorSignUpDTOs, token);
      }
      catch (Exception ex)
      {
        // ToDo: Log Error Here.
        // ToDo: Also call Api to rollback transactions.
        throw;
      }

      // Call Send Notification 

      // Save Data
      _unitOfWork.SaveAll();

      return response;
    }

    #endregion Signup Customer

    #region Private Methods

    private async Task AddVendorExt(VendorSignUpReqDTO vendorSignUpDTO, bool CanUpdateCurrency, CancellationToken token = default(CancellationToken))
    {

      Business business = await _businessDS.FindAsync(bus => bus.TenantId == vendorSignUpDTO.TenantId);
      int decimalPrecision = 0;
      string timeZone = "";
      if (business != null)
      {
        decimalPrecision = business.DecimalPrecision;
        timeZone = business.TimeZone;
      }
      // Add in Customer Extention 
      Vendor vendor = new Vendor();
      vendor.ID = Guid.NewGuid();
      vendor.CreatedBy = vendorSignUpDTO.BusinesPrimaryUserId;
      vendor.UpdatedBy = vendorSignUpDTO.BusinesPrimaryUserId;
      vendor.CreatedOn = DateTime.UtcNow;
      vendor.UpdatedOn = DateTime.UtcNow;
      vendor.BusinessPartnerTenantId = vendorSignUpDTO.BusinesPartnerTenantId;
      vendor.Deleted = false;
      vendor.CanUpdateCurrency = CanUpdateCurrency;
      vendor.Configured = false;
      vendor.Currency = vendorSignUpDTO.Currency;
      vendor.DecimalPrecision = decimalPrecision;
      vendor.TimeZone = timeZone;
      vendor.TenantId = vendorSignUpDTO.TenantId;
      await _vendorDS.AddAsync(vendor, token);
    }


    private async Task<VendorSignUpResDTO> TenantSignUpInAppMgmt(List<VendorSignUpReqDTO> vendorSignUpDTOs, CancellationToken token = default(CancellationToken))
    {
      string baseUrl = _appSettings.AppMgmtApiUrl;
      string reqeustMethod = "tenant/customersignup";

      //UserSession session = _userSessionManager.GetSession();

      //List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      //headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, vendorSignUpDTOs, _appSettings.AppName, _appSettings.IdentityServerUrl);
      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
      var response = await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
      return new VendorSignUpResDTO();
    }

    #endregion Private Methods
  }
}
