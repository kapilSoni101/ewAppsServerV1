using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.NotificationService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.BusinessEntity.DS
{
  public class BusinessEntityNotificationHandler : IBusinessEntityNotificationHandler
  {

    public const string manageInvoicePermissionOnBizPaymentApp = "8";
    public const string managecustomerPermissionOnBizPaymentApp = "8";

    BusinessEntityAppSettings _businessEntityAppSettings;
    IBusinessEntityNotificationService _businessEntityNotificationService;
    IUserSessionManager _sessionManager;

    public BusinessEntityNotificationHandler(IBusinessEntityNotificationService businessEntityNotificationService, IUserSessionManager sessionManager, IOptions<BusinessEntityAppSettings> businessEntityAppSettingsOptions)
    {
      //  _bAARInvoiceDS = bAARInvoiceDS;
      _businessEntityAppSettings = businessEntityAppSettingsOptions.Value;
      _sessionManager = sessionManager;
            _businessEntityNotificationService = businessEntityNotificationService;
    }

    public void SendASNToBizUser(ASNNotificationDTO aSNNotificationDTO)
    {

      //<xsl:param name="publisherCompanyName"/>
      //<xsl:param name="businessCompanyName"/>
      //<xsl:param name="customerCompanyName"/>
      //<xsl:param name="customerCompanyID"/>
      //<xsl:param name="totalAmount"/>
      //<xsl:param name="ID"/>
      //<xsl:param name="trackingNumber"/>
      //<xsl:param name="address"/>
      //<xsl:param name="packagingSlipNo"/>
      //<xsl:param name="shippingType"/>
      //<xsl:param name="shippingPlan"/>
      //<xsl:param name="userID"/>
      //<xsl:param name="userName"/>
      //<xsl:param name="subDomain"/>
      //<xsl:param name="portalURL"/>
      //<xsl:param name="copyrightText"/

      Dictionary<string, string> eventData = new Dictionary<string, string>();
      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(aSNNotificationDTO.DateTimeFormat);
      aSNNotificationDTO.ShipDate = DateTime.SpecifyKind(aSNNotificationDTO.ShipDate, DateTimeKind.Utc);
      aSNNotificationDTO.ShipDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(aSNNotificationDTO.ShipDate, aSNNotificationDTO.TimeZone, false);
      string formatDate = DateTimeHelper.FormatDate(aSNNotificationDTO.ShipDate, dtPickList.JSDateTimeFormat);
      eventData.Add("shipDate", formatDate);

      aSNNotificationDTO.ExpectedDate = DateTime.SpecifyKind(aSNNotificationDTO.ExpectedDate, DateTimeKind.Utc);
      aSNNotificationDTO.ExpectedDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(aSNNotificationDTO.ExpectedDate, aSNNotificationDTO.TimeZone, false);
      string formatExpectedDate = DateTimeHelper.FormatDate(aSNNotificationDTO.ExpectedDate, dtPickList.JSDateTimeFormat);
      eventData.Add("expectedDate", formatExpectedDate);

      eventData.Add(NotificationConstants.AppIdKey, aSNNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, aSNNotificationDTO.BusinessTenantId.ToString());
      eventData.Add("publisherCompanyName", aSNNotificationDTO.PublisherName);
      eventData.Add("businessCompanyName", aSNNotificationDTO.BusinessName);
      eventData.Add("customerCompanyName", aSNNotificationDTO.CustomerName);
      eventData.Add("customerCompanyID", aSNNotificationDTO.ERPCustomerKey);
      eventData.Add("ID", aSNNotificationDTO.ERPASNKey);
      eventData.Add("userID", aSNNotificationDTO.UserIdentityNo);
      eventData.Add("userName", aSNNotificationDTO.UserName);
      eventData.Add("totalAmount", aSNNotificationDTO.TotalAmount);
      eventData.Add("trackingNumber", aSNNotificationDTO.TrackingNo);
      eventData.Add("address", aSNNotificationDTO.UserName);
      eventData.Add("packagingSlipNo", aSNNotificationDTO.PackagingSlipNo);
      eventData.Add("shippingType", aSNNotificationDTO.ShippingTypeText);
      eventData.Add("shippingPlan", aSNNotificationDTO.ShipmentPlan);
      eventData.Add("subDomain", aSNNotificationDTO.SubDomainName);
      eventData.Add("portalUrl", _businessEntityAppSettings.BizPayAppUrl);
      eventData.Add("copyrightText", aSNNotificationDTO.Copyright);
      eventData.Add("publisherTenantId", aSNNotificationDTO.PublisherTenantId.ToString());
      //ToDo: Change entity type using enum.
      eventData.Add("asTargetEntityType", "1");
      eventData.Add("asTargetEntityId", aSNNotificationDTO.ID.ToString());
      eventData.Add("asAppId", aSNNotificationDTO.AppId.ToString());

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.InvoiceDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);


      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      //eventDataDict.Add("UserSession", MapNotitificationUserSession(businessOnBoardNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = (long)BusinessEntityNotificationEventEnum.BizAddASN;
      generateNotificationDTO.EventInfo = eventDataDict;
      //generateNotificationDTO.LoggedinUserId =  
      generateNotificationDTO.UseCacheForTemplate = false;
      _businessEntityNotificationService.GenerateNotificationAsync(generateNotificationDTO);
    }

    public void SendUpdateASNNotificationToBizCustomerUserInIntegratedMode(ASNNotificationDTO aSNNotificationDTO)
    {

      //<xsl:param name="publisherCompanyName"/>
      //<xsl:param name="businessCompanyName"/>
      //<xsl:param name="customerCompanyName"/>
      //<xsl:param name="customerCompanyID"/>        
      //<xsl:param name="ID"/>           
      //<xsl:param name="dateTime"/>
      //<xsl:param name="userID"/>
      //<xsl:param name="userName"/>
      //<xsl:param name="subDomain"/>
      //<xsl:param name="portalURL"/>
      //<xsl:param name="copyrightText"/

      Dictionary<string, string> eventData = new Dictionary<string, string>();
      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(aSNNotificationDTO.DateTimeFormat);
      aSNNotificationDTO.DateTime = DateTime.SpecifyKind(aSNNotificationDTO.DateTime, DateTimeKind.Utc);
      aSNNotificationDTO.DateTime = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(aSNNotificationDTO.DateTime, aSNNotificationDTO.TimeZone, false);
      string formatDate = DateTimeHelper.FormatDate(aSNNotificationDTO.DateTime, dtPickList.JSDateTimeFormat);
      eventData.Add("dateTime", formatDate);

      aSNNotificationDTO.ShipDate = DateTime.SpecifyKind(aSNNotificationDTO.ShipDate, DateTimeKind.Utc);
      aSNNotificationDTO.ShipDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(aSNNotificationDTO.ShipDate, aSNNotificationDTO.TimeZone, false);
      string formatShipDate = DateTimeHelper.FormatDate(aSNNotificationDTO.ShipDate, dtPickList.JSDateTimeFormat);
      eventData.Add("shipDate", formatShipDate);

      aSNNotificationDTO.ExpectedDate = DateTime.SpecifyKind(aSNNotificationDTO.ExpectedDate, DateTimeKind.Utc);
      aSNNotificationDTO.ExpectedDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(aSNNotificationDTO.ExpectedDate, aSNNotificationDTO.TimeZone, false);
      string formatExpectedDate = DateTimeHelper.FormatDate(aSNNotificationDTO.ExpectedDate, dtPickList.JSDateTimeFormat);
      eventData.Add("expectedDate", formatExpectedDate);

      eventData.Add(NotificationConstants.AppIdKey, aSNNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, aSNNotificationDTO.BusinessTenantId.ToString());
      eventData.Add("publisherCompanyName", aSNNotificationDTO.PublisherName);
      eventData.Add("businessCompanyName", aSNNotificationDTO.BusinessName);
      eventData.Add("customerCompanyName", aSNNotificationDTO.CustomerName);
      eventData.Add("customerCompanyID", aSNNotificationDTO.ERPCustomerKey);
      eventData.Add("ID", aSNNotificationDTO.ERPASNKey);
      eventData.Add("shippingPlan", aSNNotificationDTO.ShipmentPlan);
      eventData.Add("publisherTenantId", aSNNotificationDTO.PublisherTenantId.ToString());
      eventData.Add("userID", aSNNotificationDTO.UserIdentityNo);
      eventData.Add("userName", aSNNotificationDTO.UserName);
      eventData.Add("subDomain", aSNNotificationDTO.SubDomainName);
      eventData.Add("portalUrl", _businessEntityAppSettings.BizPayAppUrl);
      eventData.Add("copyrightText", aSNNotificationDTO.Copyright);
      //ToDo: Change entity type using enum.
      eventData.Add("asTargetEntityType", "1");
      eventData.Add("asTargetEntityId", aSNNotificationDTO.ID.ToString());
      eventData.Add("asAppId", aSNNotificationDTO.AppId.ToString());

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.InvoiceDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);


      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      //eventDataDict.Add("UserSession", MapNotitificationUserSession(businessOnBoardNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = (long)BusinessEntityNotificationEventEnum.BizUpdateASN;
      generateNotificationDTO.EventInfo = eventDataDict;
      //generateNotificationDTO.LoggedinUserId =  
      generateNotificationDTO.UseCacheForTemplate = false;
      _businessEntityNotificationService.GenerateNotificationAsync(generateNotificationDTO);
    }

    public void SendContractNotificationToBizUser(ContractNotificationDTO contractNotificationDTO)
    {

      //<xsl:param name="publisherCompanyName"/>
      //<xsl:param name="businessCompanyName"/>
      //<xsl:param name="customerCompanyName"/>
      //<xsl:param name="customerCompanyID"/>
      //<xsl:param name="ID"/>
      //<xsl:param name="startDate"/>
      //<xsl:param name="endDate"/>
      //<xsl:param name="terminationDate"/>
      //<xsl:param name="signingDate"/>
      //<xsl:param name="description"/>
      //<xsl:param name="userID"/>
      //<xsl:param name="userName"/>
      //<xsl:param name="subDomain"/>
      //<xsl:param name="portalURL"/>
      //<xsl:param name="copyrightText"/>

      Dictionary<string, string> eventData = new Dictionary<string, string>();

      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(contractNotificationDTO.DateTimeFormat);
      contractNotificationDTO.StartDate = DateTime.SpecifyKind(contractNotificationDTO.StartDate, DateTimeKind.Utc);
      contractNotificationDTO.StartDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(contractNotificationDTO.StartDate, contractNotificationDTO.TimeZone, false);
      string formatDate = DateTimeHelper.FormatDate(contractNotificationDTO.StartDate, dtPickList.JSDateTimeFormat);
      eventData.Add("startDate", formatDate);

      contractNotificationDTO.EndDate = DateTime.SpecifyKind(contractNotificationDTO.EndDate, DateTimeKind.Utc);
      contractNotificationDTO.EndDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(contractNotificationDTO.EndDate, contractNotificationDTO.TimeZone, false);
      string formatEndDate = DateTimeHelper.FormatDate(contractNotificationDTO.EndDate, dtPickList.JSDateTimeFormat);
      eventData.Add("endDate", formatEndDate);

      contractNotificationDTO.TerminationDate = DateTime.SpecifyKind(contractNotificationDTO.TerminationDate, DateTimeKind.Utc);
      contractNotificationDTO.TerminationDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(contractNotificationDTO.TerminationDate, contractNotificationDTO.TimeZone, false);
      string formatTerminationDate = DateTimeHelper.FormatDate(contractNotificationDTO.TerminationDate, dtPickList.JSDateTimeFormat);
      eventData.Add("terminationDate", formatTerminationDate);

      contractNotificationDTO.SigningDate = DateTime.SpecifyKind(contractNotificationDTO.SigningDate, DateTimeKind.Utc);
      contractNotificationDTO.SigningDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(contractNotificationDTO.SigningDate, contractNotificationDTO.TimeZone, false);
      string formatSigningDate = DateTimeHelper.FormatDate(contractNotificationDTO.SigningDate, dtPickList.JSDateTimeFormat);
      eventData.Add("signingDate", formatSigningDate);

      eventData.Add(NotificationConstants.AppIdKey, contractNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, contractNotificationDTO.BusinessTenantId.ToString());
      eventData.Add("publisherCompanyName", contractNotificationDTO.PublisherName);
      eventData.Add("businessCompanyName", contractNotificationDTO.BusinessName);
      eventData.Add("customerCompanyName", contractNotificationDTO.CustomerName);
      eventData.Add("customerCompanyID", contractNotificationDTO.ERPCustomerKey);
      eventData.Add("publisherTenantId", contractNotificationDTO.PublisherTenantId.ToString());
      eventData.Add("ID", contractNotificationDTO.ERPContractKey);
      //eventData.Add("startDate", contractNotificationDTO.StartDate.ToString());
      //eventData.Add("endDate", contractNotificationDTO.EndDate.ToString());
      //eventData.Add("terminationDate", contractNotificationDTO.TerminationDate.ToString());
      //eventData.Add("signingDate", contractNotificationDTO.SigningDate.ToString());
      eventData.Add("description", contractNotificationDTO.Description.ToString());
      eventData.Add("userID", contractNotificationDTO.UserIdentityNo);
      eventData.Add("userName", contractNotificationDTO.UserName);
      eventData.Add("subDomain", contractNotificationDTO.SubDomainName);
      eventData.Add("portalUrl", _businessEntityAppSettings.BizPayAppUrl);
      eventData.Add("copyrightText", contractNotificationDTO.Copyright);
      //ToDo: Change entity type using enum.
      eventData.Add("asTargetEntityType", "1");
      eventData.Add("asTargetEntityId", contractNotificationDTO.ID.ToString());
      eventData.Add("asAppId", contractNotificationDTO.AppId.ToString());

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.InvoiceDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);


      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      //eventDataDict.Add("UserSession", MapNotitificationUserSession(businessOnBoardNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = (long)BusinessEntityNotificationEventEnum.BizAddContract;
      generateNotificationDTO.EventInfo = eventDataDict;
      //generateNotificationDTO.LoggedinUserId =  
      generateNotificationDTO.UseCacheForTemplate = false;
      _businessEntityNotificationService.GenerateNotificationAsync(generateNotificationDTO);
    }


    public void SendUpdateContractToBizCustomerUserInIntegratedMode(ContractNotificationDTO contractNotificationDTO)
    {

      //<xsl:param name="publisherCompanyName"/>
      //<xsl:param name="businessCompanyName"/>
      //<xsl:param name="customerCompanyName"/>
      //<xsl:param name="customerCompanyID"/>
      //<xsl:param name="ID"/>
      //<xsl:param name="dateTime"/>
      //<xsl:param name="userID"/>
      //<xsl:param name="userName"/>
      //<xsl:param name="subDomain"/>
      //<xsl:param name="portalURL"/>
      //<xsl:param name="copyrightText"/>

      Dictionary<string, string> eventData = new Dictionary<string, string>();

      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(contractNotificationDTO.DateTimeFormat);
      contractNotificationDTO.DateTime = DateTime.SpecifyKind(contractNotificationDTO.DateTime, DateTimeKind.Utc);
      contractNotificationDTO.DateTime = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(contractNotificationDTO.DateTime, contractNotificationDTO.TimeZone, false);
      string formatDate = DateTimeHelper.FormatDate(contractNotificationDTO.DateTime, dtPickList.JSDateTimeFormat);
      eventData.Add("dateTime", formatDate);

      contractNotificationDTO.StartDate = DateTime.SpecifyKind(contractNotificationDTO.StartDate, DateTimeKind.Utc);
      contractNotificationDTO.StartDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(contractNotificationDTO.StartDate, contractNotificationDTO.TimeZone, false);
      string formatStartDate = DateTimeHelper.FormatDate(contractNotificationDTO.StartDate, dtPickList.JSDateTimeFormat);
      eventData.Add("startDate", formatStartDate);

      contractNotificationDTO.EndDate = DateTime.SpecifyKind(contractNotificationDTO.EndDate, DateTimeKind.Utc);
      contractNotificationDTO.EndDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(contractNotificationDTO.EndDate, contractNotificationDTO.TimeZone, false);
      string formatEndDate = DateTimeHelper.FormatDate(contractNotificationDTO.EndDate, dtPickList.JSDateTimeFormat);
      eventData.Add("endDate", formatEndDate);

      eventData.Add(NotificationConstants.AppIdKey, contractNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, contractNotificationDTO.BusinessTenantId.ToString());
      eventData.Add("publisherCompanyName", contractNotificationDTO.PublisherName);
      eventData.Add("businessCompanyName", contractNotificationDTO.BusinessName);
      eventData.Add("customerCompanyName", contractNotificationDTO.CustomerName);
      eventData.Add("customerCompanyID", contractNotificationDTO.ERPCustomerKey);
      eventData.Add("ID", contractNotificationDTO.ERPContractKey);
      eventData.Add("publisherTenantId", contractNotificationDTO.PublisherTenantId.ToString());

      eventData.Add("userID", contractNotificationDTO.UserIdentityNo);
      eventData.Add("userName", contractNotificationDTO.UserName);
      eventData.Add("subDomain", contractNotificationDTO.SubDomainName);
      eventData.Add("portalUrl", _businessEntityAppSettings.BizPayAppUrl);
      eventData.Add("copyrightText", contractNotificationDTO.Copyright);
      //ToDo: Change entity type using enum.
      eventData.Add("asTargetEntityType", "1");
      eventData.Add("asTargetEntityId", contractNotificationDTO.ID.ToString());
      eventData.Add("asAppId", contractNotificationDTO.AppId.ToString());

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.InvoiceDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);


      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      //eventDataDict.Add("UserSession", MapNotitificationUserSession(businessOnBoardNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = (long)BusinessEntityNotificationEventEnum.BizUpdateContract;
      generateNotificationDTO.EventInfo = eventDataDict;
      //generateNotificationDTO.LoggedinUserId =  
      generateNotificationDTO.UseCacheForTemplate = false;
      _businessEntityNotificationService.GenerateNotificationAsync(generateNotificationDTO);
    }

    public void SendDeliveryNotificationToBizUser(DeliveryNotificationDTO deliveryNotificationDTO)
    {

      //<xsl:param name="publisherCompanyName"/>
      //<xsl:param name="businessCompanyName"/>
      //<xsl:param name="customerCompanyName"/>
      //<xsl:param name="customerCompanyID"/>
      //<xsl:param name="totalAmount"/>
      //<xsl:param name="ID"/>
      //<xsl:param name="trackingNumber"/>
      //<xsl:param name="address"/>
      //<xsl:param name="packagingSlipNo"/>
      //<xsl:param name="shippingType"/>
      //<xsl:param name="shippingPlan"/>
      //<xsl:param name="userID"/>
      //<xsl:param name="userName"/>
      //<xsl:param name="subDomain"/>
      //<xsl:param name="portalURL"/>
      //<xsl:param name="copyrightText"/

      Dictionary<string, string> eventData = new Dictionary<string, string>();
      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(deliveryNotificationDTO.DateTimeFormat);
      deliveryNotificationDTO.DeliveryDate = DateTime.SpecifyKind(deliveryNotificationDTO.DeliveryDate, DateTimeKind.Utc);
      deliveryNotificationDTO.DeliveryDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(deliveryNotificationDTO.DeliveryDate, deliveryNotificationDTO.TimeZone, false);
      string formatDate = DateTimeHelper.FormatDate(deliveryNotificationDTO.DeliveryDate, dtPickList.JSDateTimeFormat);
      eventData.Add("shipDate", formatDate);


      eventData.Add(NotificationConstants.AppIdKey, deliveryNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, deliveryNotificationDTO.BusinessTenantId.ToString());
      eventData.Add("publisherCompanyName", deliveryNotificationDTO.PublisherName);
      eventData.Add("businessCompanyName", deliveryNotificationDTO.BusinessName);
      eventData.Add("customerCompanyName", deliveryNotificationDTO.CustomerName);
      eventData.Add("customerCompanyID", deliveryNotificationDTO.ERPCustomerKey);
      eventData.Add("ID", deliveryNotificationDTO.ERPDeliveryKey);
      eventData.Add("userID", deliveryNotificationDTO.UserIdentityNo);
      eventData.Add("userName", deliveryNotificationDTO.UserName);
      eventData.Add("totalAmount", deliveryNotificationDTO.TotalPaymentDue);
      eventData.Add("trackingNumber", deliveryNotificationDTO.TrackingNo);
      eventData.Add("address", deliveryNotificationDTO.UserName);
      eventData.Add("shippingType", deliveryNotificationDTO.ShippingTypeText);

      //TO Do Shipping plan is missing in delivery table so we have to remove this in xslt
      eventData.Add("shippingPlan", deliveryNotificationDTO.ShippingTypeText);
      eventData.Add("subDomain", deliveryNotificationDTO.SubDomainName);
      eventData.Add("portalUrl", _businessEntityAppSettings.BizPayAppUrl);
      eventData.Add("copyrightText", deliveryNotificationDTO.Copyright);
      eventData.Add("publisherTenantId", deliveryNotificationDTO.PublisherTenantId.ToString());
      //ToDo: Change entity type using enum.
      eventData.Add("asTargetEntityType", "1");
      eventData.Add("asTargetEntityId", deliveryNotificationDTO.ID.ToString());
      eventData.Add("asAppId", deliveryNotificationDTO.AppId.ToString());

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.InvoiceDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);


      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      //eventDataDict.Add("UserSession", MapNotitificationUserSession(businessOnBoardNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = (long)BusinessEntityNotificationEventEnum.BizAddDelivery;
      generateNotificationDTO.EventInfo = eventDataDict;
      //generateNotificationDTO.LoggedinUserId =  
      generateNotificationDTO.UseCacheForTemplate = false;
      _businessEntityNotificationService.GenerateNotificationAsync(generateNotificationDTO);
    }

    public void SendUpdateDeliveryToBizCustomerUserInIntegratedMode(DeliveryNotificationDTO deliveryNotificationDTO)
    {

      //<xsl:param name="publisherCompanyName"/>
      //<xsl:param name="businessCompanyName"/>
      //<xsl:param name="customerCompanyName"/>
      //<xsl:param name="customerCompanyID"/>
      //<xsl:param name="ID"/>
      //<xsl:param name="dateTime"/>
      //<xsl:param name="userID"/>
      //<xsl:param name="userName"/>
      //<xsl:param name="subDomain"/>
      //<xsl:param name="portalURL"/>
      //<xsl:param name="copyrightText"/>

      Dictionary<string, string> eventData = new Dictionary<string, string>();

      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(deliveryNotificationDTO.DateTimeFormat);
      deliveryNotificationDTO.DateTime = DateTime.SpecifyKind(deliveryNotificationDTO.DateTime, DateTimeKind.Utc);
      deliveryNotificationDTO.DateTime = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(deliveryNotificationDTO.DateTime, deliveryNotificationDTO.TimeZone, false);
      string formatDate = DateTimeHelper.FormatDate(deliveryNotificationDTO.DateTime, dtPickList.JSDateTimeFormat);
      eventData.Add("dateTime", formatDate);

      eventData.Add(NotificationConstants.AppIdKey, deliveryNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, deliveryNotificationDTO.BusinessTenantId.ToString());
      eventData.Add("publisherCompanyName", deliveryNotificationDTO.PublisherName);
      eventData.Add("businessCompanyName", deliveryNotificationDTO.BusinessName);
      eventData.Add("customerCompanyName", deliveryNotificationDTO.CustomerName);
      eventData.Add("customerCompanyID", deliveryNotificationDTO.ERPCustomerKey);
      eventData.Add("ID", deliveryNotificationDTO.ERPDeliveryKey);
      eventData.Add("publisherTenantId", deliveryNotificationDTO.PublisherTenantId.ToString());

      eventData.Add("userID", deliveryNotificationDTO.UserIdentityNo);
      eventData.Add("userName", deliveryNotificationDTO.UserName);
      eventData.Add("subDomain", deliveryNotificationDTO.SubDomainName);
      eventData.Add("portalUrl", _businessEntityAppSettings.BizPayAppUrl);
      eventData.Add("copyrightText", deliveryNotificationDTO.Copyright);
      //ToDo: Change entity type using enum.
      eventData.Add("asTargetEntityType", "1");
      eventData.Add("asTargetEntityId", deliveryNotificationDTO.ID.ToString());
      eventData.Add("asAppId", deliveryNotificationDTO.AppId.ToString());

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.InvoiceDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);


      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      //eventDataDict.Add("UserSession", MapNotitificationUserSession(businessOnBoardNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = (long)BusinessEntityNotificationEventEnum.BizUpdateDelivery;
      generateNotificationDTO.EventInfo = eventDataDict;
      //generateNotificationDTO.LoggedinUserId =  
      generateNotificationDTO.UseCacheForTemplate = false;
      _businessEntityNotificationService.GenerateNotificationAsync(generateNotificationDTO);
    }

    public void SendAddARInvoiceToBizUser(ARInvoiceNotificationDTO aRInvoiceNotificationDTO)
    {
      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(aRInvoiceNotificationDTO.DateTimeFormat);



      Dictionary<string, string> eventData = new Dictionary<string, string>();

      aRInvoiceNotificationDTO.PostingDate = DateTime.SpecifyKind(aRInvoiceNotificationDTO.PostingDate, DateTimeKind.Utc);
      aRInvoiceNotificationDTO.PostingDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(aRInvoiceNotificationDTO.PostingDate, aRInvoiceNotificationDTO.TimeZone, false);
      string formatDate = DateTimeHelper.FormatDate(aRInvoiceNotificationDTO.PostingDate, dtPickList.JSDateTimeFormat);
      eventData.Add("postingDate", formatDate);

      aRInvoiceNotificationDTO.DocumentDate = DateTime.SpecifyKind(aRInvoiceNotificationDTO.DocumentDate, DateTimeKind.Utc);
      aRInvoiceNotificationDTO.UpdatedOn = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(aRInvoiceNotificationDTO.DocumentDate, aRInvoiceNotificationDTO.TimeZone, false);
      formatDate = DateTimeHelper.FormatDate(aRInvoiceNotificationDTO.DocumentDate, dtPickList.JSDateTimeFormat);
      eventData.Add("documentDate", formatDate);
      if (aRInvoiceNotificationDTO.DueDate.HasValue)
      {
        // DateTime? dueDate = aRInvoiceNotificationDTO.DueDate;
        aRInvoiceNotificationDTO.DueDate = DateTime.SpecifyKind(aRInvoiceNotificationDTO.DueDate.Value, DateTimeKind.Utc);
        aRInvoiceNotificationDTO.DueDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(aRInvoiceNotificationDTO.DueDate.Value, aRInvoiceNotificationDTO.TimeZone, false);
        formatDate = DateTimeHelper.FormatDate(aRInvoiceNotificationDTO.DueDate.Value, dtPickList.JSDateTimeFormat);

        eventData.Add("dueDate", formatDate);
      }
      else
      {
        eventData.Add("dueDate", "");
      }



      eventData.Add(NotificationConstants.AppIdKey, aRInvoiceNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, aRInvoiceNotificationDTO.BusinessTenantId.ToString());
      eventData.Add("appKey", aRInvoiceNotificationDTO.AppKey);
      eventData.Add("invoicePermissionMask", manageInvoicePermissionOnBizPaymentApp);
      eventData.Add("publisherName", aRInvoiceNotificationDTO.PublisherName);
      eventData.Add("businessName", aRInvoiceNotificationDTO.BusinessName);
      eventData.Add("customerName", aRInvoiceNotificationDTO.CustomerName);
      eventData.Add("customerNo", aRInvoiceNotificationDTO.ERPCustomerKey);
      eventData.Add("invoiceNo", aRInvoiceNotificationDTO.ERPARInvoiceKey);
      eventData.Add("totalAmountWithCurrency", string.Format("{0} {1}", aRInvoiceNotificationDTO.LocalCurrency, aRInvoiceNotificationDTO.TotalPaymentDue));
      //eventData.Add("postingDate", aRInvoiceNotificationDTO.PostingDate.ToString(aRInvoiceNotificationDTO.DateTimeFormat));
      //eventData.Add("documentDate", aRInvoiceNotificationDTO.DocumentDate.ToString(aRInvoiceNotificationDTO.DateTimeFormat));
      //if (aRInvoiceNotificationDTO.DueDate.HasValue)
      //{
      //  eventData.Add("dueDate", aRInvoiceNotificationDTO.DueDate.Value.ToString(aRInvoiceNotificationDTO.DateTimeFormat));
      //}
      //else
      //{
      //  eventData.Add("dueDate", "");
      //}

      eventData.Add("publisherTenantId", aRInvoiceNotificationDTO.PublisherTenantId.ToString());
      eventData.Add("businessPartnerTenantId", aRInvoiceNotificationDTO.BusinessPartnerTenantId.ToString());
      eventData.Add("createdByName", aRInvoiceNotificationDTO.FullName);
      eventData.Add("createdByNo", aRInvoiceNotificationDTO.UserIdentityNo);
      eventData.Add("subDomain", aRInvoiceNotificationDTO.SubDomainName);
      // eventData.Add("portalUrl", _businessEntityAppSettings.BizPayAppUrl);
      eventData.Add("portalUrl", string.Format(_businessEntityAppSettings.BizPayAppUrl, aRInvoiceNotificationDTO.SubDomainName));
      eventData.Add("copyright", aRInvoiceNotificationDTO.Copyright);
      //ToDo: Change entity type using enum.
      eventData.Add("asTargetEntityType", "1");
      eventData.Add("asTargetEntityId", aRInvoiceNotificationDTO.InvoiceId.ToString());
      eventData.Add("asAppId", aRInvoiceNotificationDTO.AppId.ToString());

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.InvoiceDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);


      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      //eventDataDict.Add("UserSession", MapNotitificationUserSession(businessOnBoardNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = (long)BusinessEntityNotificationEventEnum.BizAddARInvoiceForBusinessUser;
      generateNotificationDTO.EventInfo = eventDataDict;
      //generateNotificationDTO.LoggedinUserId =  
      generateNotificationDTO.UseCacheForTemplate = false;
      _businessEntityNotificationService.GenerateNotificationAsync(generateNotificationDTO);
    }

    public void SendAddARInvoiceToCustomerUserInIntegratedMode(ARInvoiceNotificationDTO aRInvoiceNotificationDTO)
    {
      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(aRInvoiceNotificationDTO.DateTimeFormat);



      Dictionary<string, string> eventData = new Dictionary<string, string>();

      aRInvoiceNotificationDTO.PostingDate = DateTime.SpecifyKind(aRInvoiceNotificationDTO.PostingDate, DateTimeKind.Utc);
      aRInvoiceNotificationDTO.PostingDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(aRInvoiceNotificationDTO.PostingDate, aRInvoiceNotificationDTO.TimeZone, false);
      string formatDate = DateTimeHelper.FormatDate(aRInvoiceNotificationDTO.PostingDate, dtPickList.JSDateTimeFormat);
      eventData.Add("postingDate", formatDate);

      aRInvoiceNotificationDTO.DocumentDate = DateTime.SpecifyKind(aRInvoiceNotificationDTO.DocumentDate, DateTimeKind.Utc);
      aRInvoiceNotificationDTO.UpdatedOn = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(aRInvoiceNotificationDTO.DocumentDate, aRInvoiceNotificationDTO.TimeZone, false);
      formatDate = DateTimeHelper.FormatDate(aRInvoiceNotificationDTO.DocumentDate, dtPickList.JSDateTimeFormat);
      eventData.Add("documentDate", formatDate);
      if (aRInvoiceNotificationDTO.DueDate.HasValue)
      {
        // DateTime? dueDate = aRInvoiceNotificationDTO.DueDate;
        aRInvoiceNotificationDTO.DueDate = DateTime.SpecifyKind(aRInvoiceNotificationDTO.DueDate.Value, DateTimeKind.Utc);
        aRInvoiceNotificationDTO.DueDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(aRInvoiceNotificationDTO.DueDate.Value, aRInvoiceNotificationDTO.TimeZone, false);
        formatDate = DateTimeHelper.FormatDate(aRInvoiceNotificationDTO.DueDate.Value, dtPickList.JSDateTimeFormat);

        eventData.Add("dueDate", formatDate);
      }
      else
      {
        eventData.Add("dueDate", "");
      }


      eventData.Add(NotificationConstants.AppIdKey, aRInvoiceNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, aRInvoiceNotificationDTO.BusinessTenantId.ToString());
      //eventData.Add("appKey", aRInvoiceNotificationDTO.AppKey);
      //eventData.Add("invoicePermissionMask", manageInvoicePermissionOnBizPaymentApp);
      eventData.Add("publisherName", aRInvoiceNotificationDTO.PublisherName);
      eventData.Add("businessName", aRInvoiceNotificationDTO.BusinessName);
      eventData.Add("customerName", aRInvoiceNotificationDTO.CustomerName);
      eventData.Add("customerNo", aRInvoiceNotificationDTO.ERPCustomerKey);
      eventData.Add("invoiceNo", aRInvoiceNotificationDTO.ERPARInvoiceKey);
      eventData.Add("totalAmountWithCurrency", string.Format("{0} {1}", aRInvoiceNotificationDTO.LocalCurrency, aRInvoiceNotificationDTO.TotalPaymentDue));
      //eventData.Add("postingDate", aRInvoiceNotificationDTO.PostingDate.ToString(aRInvoiceNotificationDTO.DateTimeFormat));
      //eventData.Add("documentDate", aRInvoiceNotificationDTO.DocumentDate.ToString(aRInvoiceNotificationDTO.DateTimeFormat));
      //if (aRInvoiceNotificationDTO.DueDate.HasValue)
      //{
      //  eventData.Add("dueDate", aRInvoiceNotificationDTO.DueDate.Value.ToString(aRInvoiceNotificationDTO.DateTimeFormat));
      //}
      //else
      //{
      //  eventData.Add("dueDate", "");
      //}

      eventData.Add("publisherTenantId", aRInvoiceNotificationDTO.PublisherTenantId.ToString());
      eventData.Add("businessPartnerTenantId", aRInvoiceNotificationDTO.BusinessPartnerTenantId.ToString());
      eventData.Add("createdByName", aRInvoiceNotificationDTO.FullName);
      eventData.Add("createdByNo", aRInvoiceNotificationDTO.UserIdentityNo);
      eventData.Add("subDomain", aRInvoiceNotificationDTO.SubDomainName);
      // eventData.Add("portalUrl", _businessEntityAppSettings.CustomerPortalAppUrl);
      eventData.Add("portalUrl", string.Format(_businessEntityAppSettings.CustomerPortalAppUrl, aRInvoiceNotificationDTO.SubDomainName));
      eventData.Add("copyright", aRInvoiceNotificationDTO.Copyright);
      //ToDo: Change entity type using enum.
      eventData.Add("asTargetEntityType", "1");
      eventData.Add("asTargetEntityId", aRInvoiceNotificationDTO.InvoiceId.ToString());
      eventData.Add("asAppId", aRInvoiceNotificationDTO.AppId.ToString());

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.InvoiceDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);


      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      //eventDataDict.Add("UserSession", MapNotitificationUserSession(businessOnBoardNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = (long)BusinessEntityNotificationEventEnum.BizAddARInvoiceForCustomerUser;
      generateNotificationDTO.EventInfo = eventDataDict;
      //generateNotificationDTO.LoggedinUserId =  
      generateNotificationDTO.UseCacheForTemplate = false;
      _businessEntityNotificationService.GenerateNotificationAsync(generateNotificationDTO);

    }

    public void SendBulkAddARInvoiceToBizPaymentUserInIntegratedMode(NotificationCommonDetailDTO notificationCommonDetailDTO, int newInvoiceCount, Guid businessTenantId, string updatedByName, string updatedByUserNo, DateTime updatedDate)
    {
      Dictionary<string, string> eventData = new Dictionary<string, string>();
      eventData.Add(NotificationConstants.AppIdKey, notificationCommonDetailDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, notificationCommonDetailDTO.BusinessTenantId.ToString());
      eventData.Add("appKey", notificationCommonDetailDTO.AppKey);
      eventData.Add("invoicePermissionMask", manageInvoicePermissionOnBizPaymentApp);
      eventData.Add("publisherName", notificationCommonDetailDTO.PublisherName);
      eventData.Add("businessName", notificationCommonDetailDTO.BusinessName);
      eventData.Add("newInvoiceCount", newInvoiceCount.ToString());
      eventData.Add("updatedInvoiceCount", "0");
      eventData.Add("updatedByName", updatedByName);
      eventData.Add("updatedByNo", updatedByUserNo);
      eventData.Add("updatedOn", updatedDate.ToString(notificationCommonDetailDTO.DateTimeFormat));
      eventData.Add("publisherTenantId", notificationCommonDetailDTO.PublisherTenantId.ToString());

      eventData.Add("subDomain", notificationCommonDetailDTO.SubDomainName);
      eventData.Add("portalUrl", _businessEntityAppSettings.BizPayAppUrl);
      eventData.Add("copyright", notificationCommonDetailDTO.Copyright);
      //ToDo: Change entity type using enum.
      eventData.Add("asTargetEntityType", "1");
      eventData.Add("asTargetEntityId", Guid.Empty.ToString());
      eventData.Add("asAppId", notificationCommonDetailDTO.AppId.ToString());


      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      //eventDataDict.Add("UserSession", MapNotitificationUserSession(businessOnBoardNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = (long)BusinessEntityNotificationEventEnum.BizBulkAddARInvoice;
      generateNotificationDTO.EventInfo = eventDataDict;
      //generateNotificationDTO.LoggedinUserId =  
      generateNotificationDTO.UseCacheForTemplate = false;
      _businessEntityNotificationService.GenerateNotificationAsync(generateNotificationDTO);
    }

    public void SendUpdateARInvoiceToBizPaymentUserInIntegratedMode(ARInvoiceNotificationDTO aRInvoiceNotificationDTO)
    {
      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(aRInvoiceNotificationDTO.DateTimeFormat);



      Dictionary<string, string> eventData = new Dictionary<string, string>();

      aRInvoiceNotificationDTO.UpdatedOn = DateTime.SpecifyKind(aRInvoiceNotificationDTO.PostingDate, DateTimeKind.Utc);
      aRInvoiceNotificationDTO.UpdatedOn = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(aRInvoiceNotificationDTO.UpdatedOn, aRInvoiceNotificationDTO.TimeZone, false);
      string formatDate = DateTimeHelper.FormatDate(aRInvoiceNotificationDTO.UpdatedOn, dtPickList.JSDateTimeFormat);
      eventData.Add("updatedOn", formatDate);

      eventData.Add(NotificationConstants.AppIdKey, aRInvoiceNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, aRInvoiceNotificationDTO.BusinessTenantId.ToString());
      // eventData.Add("appKey", aRInvoiceNotificationDTO.AppKey);
      //eventData.Add("appName", aRInvoiceNotificationDTO.AppName);
      //eventData.Add("invoicePermissionMask", manageInvoicePermissionOnBizPaymentApp);
      eventData.Add("publisherName", aRInvoiceNotificationDTO.PublisherName);
      eventData.Add("businessName", aRInvoiceNotificationDTO.BusinessName);
      eventData.Add("customerName", aRInvoiceNotificationDTO.CustomerName);
      eventData.Add("customerNo", aRInvoiceNotificationDTO.ERPCustomerKey);
      eventData.Add("invoiceNo", aRInvoiceNotificationDTO.ERPARInvoiceKey);
      eventData.Add("updatedByName", aRInvoiceNotificationDTO.FullName);
      eventData.Add("updatedByNo", aRInvoiceNotificationDTO.UserIdentityNo);
      //eventData.Add("updatedOn", aRInvoiceNotificationDTO.UpdatedOn.ToString(aRInvoiceNotificationDTO.DateTimeFormat));
      eventData.Add("subDomain", aRInvoiceNotificationDTO.SubDomainName);
      //eventData.Add("portalUrl", _businessEntityAppSettings.BusinessPortalAppUrl);
      eventData.Add("portalUrl", string.Format(_businessEntityAppSettings.BusinessPortalAppUrl, aRInvoiceNotificationDTO.SubDomainName));
      eventData.Add("copyright", aRInvoiceNotificationDTO.Copyright);
      //ToDo: Change entity type using enum.
      eventData.Add("asTargetEntityType", "1");
      eventData.Add("asTargetEntityId", aRInvoiceNotificationDTO.InvoiceId.ToString());
      eventData.Add("asAppId", aRInvoiceNotificationDTO.AppId.ToString());
      eventData.Add("publisherTenantId", aRInvoiceNotificationDTO.PublisherTenantId.ToString());

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.InvoiceDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      //eventDataDict.Add("UserSession", MapNotitificationUserSession(businessOnBoardNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = (long)BusinessEntityNotificationEventEnum.BizUpdateARInvoice;
      generateNotificationDTO.EventInfo = eventDataDict;
      //generateNotificationDTO.LoggedinUserId =  
      generateNotificationDTO.UseCacheForTemplate = false;
      _businessEntityNotificationService.GenerateNotificationAsync(generateNotificationDTO);
    }

    public void SendAddCustomerInIntegratedMode(CustomerNotificationDTO customerNotificationDTO, long bizNotificationEnum)
    {
      UserSession session = _sessionManager.GetSession();
      string tenantUserId = session.TenantUserId.ToString();
      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(customerNotificationDTO.DateTimeFormat);



      Dictionary<string, string> eventData = new Dictionary<string, string>();

      customerNotificationDTO.CreatedOn = DateTime.SpecifyKind(customerNotificationDTO.CreatedOn, DateTimeKind.Utc);
      customerNotificationDTO.CreatedOn = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(customerNotificationDTO.CreatedOn, customerNotificationDTO.TimeZone, false);
      string formatDate = DateTimeHelper.FormatDate(customerNotificationDTO.CreatedOn, dtPickList.JSDateTimeFormat);
      eventData.Add("createdOn", formatDate);

      customerNotificationDTO.UpdatedOn = DateTime.SpecifyKind(customerNotificationDTO.UpdatedOn, DateTimeKind.Utc);
      customerNotificationDTO.UpdatedOn = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(customerNotificationDTO.UpdatedOn, customerNotificationDTO.TimeZone, false);
      formatDate = DateTimeHelper.FormatDate(customerNotificationDTO.UpdatedOn, dtPickList.JSDateTimeFormat);
      eventData.Add("updatedOn", formatDate);

      eventData.Add(NotificationConstants.TenantIdKey, customerNotificationDTO.BusinessTenantId.ToString());
      //eventData.Add("customerPermissionMask", managecustomerPermissionOnBizPaymentApp);
      eventData.Add("publisherName", customerNotificationDTO.PublisherName);
      eventData.Add("publisherTenantId", customerNotificationDTO.PublisherTenantId.ToString());
      eventData.Add("businessName", customerNotificationDTO.BusinessName);
      eventData.Add("customerName", customerNotificationDTO.CustomerName);
      eventData.Add("customerNo", customerNotificationDTO.ERPCustomerKey);
      eventData.Add("createdByName", customerNotificationDTO.UpdatedByName);
      eventData.Add("createdByNo", customerNotificationDTO.UpdatedByNo);
      eventData.Add("updatedByName", customerNotificationDTO.UpdatedByName);
      eventData.Add("updatedByNo", customerNotificationDTO.UpdatedByNo);
      //eventData.Add("createdOn", customerNotificationDTO.CreatedOn.ToString(customerNotificationDTO.DateTimeFormat));
      //eventData.Add("updatedOn", customerNotificationDTO.UpdatedOn.ToString(customerNotificationDTO.DateTimeFormat));
      eventData.Add("subDomain", customerNotificationDTO.SubDomainName);
      //eventData.Add("portalUrl", _businessEntityAppSettings.BusinessPortalAppUrl);
      eventData.Add("portalUrl", string.Format(_businessEntityAppSettings.BusinessPortalAppUrl, customerNotificationDTO.SubDomainName));
      eventData.Add("copyright", customerNotificationDTO.Copyright);
      //ToDo: Change entity type using enum.
      eventData.Add("asTargetEntityType", "1");
      eventData.Add("asTargetEntityId", customerNotificationDTO.CustomerId.ToString());
      eventData.Add("asAppId", Guid.Empty.ToString());
      eventData.Add("loginUserId", tenantUserId);


      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.CustomerDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      //eventDataDict.Add("UserSession", MapNotitificationUserSession(businessOnBoardNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = bizNotificationEnum;
      generateNotificationDTO.EventInfo = eventDataDict;
      //generateNotificationDTO.LoggedinUserId =  
      generateNotificationDTO.UseCacheForTemplate = false;
      _businessEntityNotificationService.GenerateNotificationAsync(generateNotificationDTO);
    }

    public void SendUpdateCustomerInIntegratedMode(CustomerNotificationDTO customerNotificationDTO, long bizNotificationEnum)
    {
      UserSession session = _sessionManager.GetSession();
      string tenantUserId = session.TenantUserId.ToString();
      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(customerNotificationDTO.DateTimeFormat);



      Dictionary<string, string> eventData = new Dictionary<string, string>();

      customerNotificationDTO.CreatedOn = DateTime.SpecifyKind(customerNotificationDTO.CreatedOn, DateTimeKind.Utc);
      customerNotificationDTO.CreatedOn = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(customerNotificationDTO.CreatedOn, customerNotificationDTO.TimeZone, false);
      string formatDate = DateTimeHelper.FormatDate(customerNotificationDTO.CreatedOn, dtPickList.JSDateTimeFormat);
      eventData.Add("createdOn", formatDate);

      customerNotificationDTO.UpdatedOn = DateTime.SpecifyKind(customerNotificationDTO.UpdatedOn, DateTimeKind.Utc);
      customerNotificationDTO.UpdatedOn = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(customerNotificationDTO.UpdatedOn, customerNotificationDTO.TimeZone, false);
      formatDate = DateTimeHelper.FormatDate(customerNotificationDTO.UpdatedOn, dtPickList.JSDateTimeFormat);
      eventData.Add("updatedOn", formatDate);

      eventData.Add(NotificationConstants.TenantIdKey, customerNotificationDTO.BusinessTenantId.ToString());
      eventData.Add("publisherName", customerNotificationDTO.PublisherName);
      eventData.Add("publisherTenantId", customerNotificationDTO.PublisherTenantId.ToString());
      eventData.Add("businessName", customerNotificationDTO.BusinessName);
      eventData.Add("customerName", customerNotificationDTO.CustomerName);
      eventData.Add("customerNo", customerNotificationDTO.ERPCustomerKey);
      eventData.Add("createdByName", customerNotificationDTO.UpdatedByName);
      eventData.Add("createdByNo", customerNotificationDTO.UpdatedByNo);
      eventData.Add("updatedByName", customerNotificationDTO.UpdatedByName);
      eventData.Add("updatedByNo", customerNotificationDTO.UpdatedByNo);
      eventData.Add("subDomain", customerNotificationDTO.SubDomainName);
      //eventData.Add("portalUrl", _businessEntityAppSettings.BizPayAppUrl);
      eventData.Add("portalUrl", string.Format(_businessEntityAppSettings.BizPayAppUrl, customerNotificationDTO.SubDomainName));
      eventData.Add("copyright", customerNotificationDTO.Copyright);
      //ToDo: Change entity type using enum.
      eventData.Add("asTargetEntityType", "1");
      eventData.Add("asTargetEntityId", customerNotificationDTO.CustomerId.ToString());
      eventData.Add("asAppId", Guid.Empty.ToString());
      eventData.Add("loginUserId", tenantUserId);


      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.CustomerDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      //eventDataDict.Add("UserSession", MapNotitificationUserSession(businessOnBoardNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = bizNotificationEnum;
      generateNotificationDTO.EventInfo = eventDataDict;
      //generateNotificationDTO.LoggedinUserId =  
      generateNotificationDTO.UseCacheForTemplate = false;
      _businessEntityNotificationService.GenerateNotificationAsync(generateNotificationDTO);
    }

    public async Task SendAddSalesOrderToBizUserInIntegratedModeAsync(SONotificationDTO soNotificationDTO)
    {

      //< xsl:param name = "standaloneMode" />
      //< xsl:param name = "publisherName" />
      //< xsl:param name = "businessName" />
      //< xsl:param name = "customerName" />
      //< xsl:param name = "customerNo" />   
      //< xsl:param name = "salesOrderNo" />  
      //< xsl:param name = "totalAmountWithCurrency" />
      //< xsl:param name = "postingDate" />
      //< xsl:param name = "documentDate" />
      //< xsl:param name = "deliveryDate" />
      //< xsl:param name = "createdByName" />
      //< xsl:param name = "createdByNo" />
      //< xsl:param name = "subDomain" />
      //< xsl:param name = "portalUrl" />
      //< xsl:param name = "copyright" />
      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(soNotificationDTO.DateTimeFormat);



      Dictionary<string, string> eventData = new Dictionary<string, string>();

      soNotificationDTO.PostingDate = DateTime.SpecifyKind(soNotificationDTO.PostingDate, DateTimeKind.Utc);
      soNotificationDTO.PostingDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(soNotificationDTO.PostingDate, soNotificationDTO.TimeZone, false);
      string formatDate = DateTimeHelper.FormatDate(soNotificationDTO.PostingDate, dtPickList.JSDateTimeFormat);
      eventData.Add("postingDate", formatDate);

      soNotificationDTO.DocumentDate = DateTime.SpecifyKind(soNotificationDTO.DocumentDate, DateTimeKind.Utc);
      soNotificationDTO.DocumentDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(soNotificationDTO.DocumentDate, soNotificationDTO.TimeZone, false);
      formatDate = DateTimeHelper.FormatDate(soNotificationDTO.DocumentDate, dtPickList.JSDateTimeFormat);
      eventData.Add("documentDate", formatDate);

      soNotificationDTO.DeliveryDate = DateTime.SpecifyKind(soNotificationDTO.DeliveryDate, DateTimeKind.Utc);
      soNotificationDTO.DeliveryDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(soNotificationDTO.DeliveryDate, soNotificationDTO.TimeZone, false);
      formatDate = DateTimeHelper.FormatDate(soNotificationDTO.DeliveryDate, dtPickList.JSDateTimeFormat);
      eventData.Add("deliveryDate", formatDate);
      //eventData.Add(NotificationConstants.AppIdKey, aRInvoiceNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, soNotificationDTO.BusinessTenantId.ToString());
      //eventData.Add("appKey", aRInvoiceNotificationDTO.AppKey);
      eventData.Add("salesOrderPermissionMask", manageInvoicePermissionOnBizPaymentApp);
      eventData.Add("publisherName", soNotificationDTO.PublisherName);
      eventData.Add("businessName", soNotificationDTO.BusinessName);
      eventData.Add("customerName", soNotificationDTO.CustomerName);
      eventData.Add("customerNo", soNotificationDTO.ERPCustomerKey);
      eventData.Add("salesOrderNo", soNotificationDTO.ERPSalesOrderKey);
      eventData.Add("totalAmountWithCurrency", string.Format("{0} {1}", soNotificationDTO.LocalCurrency, soNotificationDTO.TotalPaymentDue));
      //eventData.Add("postingDate", soNotificationDTO.PostingDate.ToString(soNotificationDTO.DateTimeFormat));
      // eventData.Add("documentDate", soNotificationDTO.DocumentDate.ToString(soNotificationDTO.DateTimeFormat));
      // if(aRInvoiceNotificationDTO.DeliveryDate) {
      // eventData.Add("deliveryDate", soNotificationDTO.DeliveryDate.ToString(soNotificationDTO.DateTimeFormat));
      //}
      //else {
      //    eventData.Add("dueDate", "");
      //}
      eventData.Add("publisherTenantId", soNotificationDTO.PublisherTenantId.ToString());
      eventData.Add("createdByName", soNotificationDTO.FullName);
      eventData.Add("createdByNo", soNotificationDTO.UserIdentityNo);
      eventData.Add("subDomain", soNotificationDTO.SubDomainName);
      eventData.Add("portalUrl", _businessEntityAppSettings.BizPayAppUrl);
      eventData.Add("copyright", soNotificationDTO.Copyright);
      //ToDo: Change entity type using enum.
      eventData.Add("asTargetEntityType", "1");
      eventData.Add("asTargetEntityId", soNotificationDTO.SalesOrderId.ToString());
      eventData.Add("asAppId", Guid.Empty.ToString());

      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      //eventDataDict.Add("UserSession", MapNotitificationUserSession(businessOnBoardNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = (long)BusinessEntityNotificationEventEnum.BizAddSalesOrder;
      generateNotificationDTO.EventInfo = eventDataDict;
      //generateNotificationDTO.LoggedinUserId =  
      generateNotificationDTO.UseCacheForTemplate = false;
      _businessEntityNotificationService.GenerateNotificationAsync(generateNotificationDTO);
    }


    public async Task SendAddSalesQuotationToBizUserInIntegratedModeAsync(SQNotificationDTO sqNotificationDTO)
    {
      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(sqNotificationDTO.DateTimeFormat);

      Dictionary<string, string> eventData = new Dictionary<string, string>();

      sqNotificationDTO.PostingDate = DateTime.SpecifyKind(sqNotificationDTO.PostingDate, DateTimeKind.Utc);
      sqNotificationDTO.PostingDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(sqNotificationDTO.PostingDate, sqNotificationDTO.TimeZone, false);
      string formatDate = DateTimeHelper.FormatDate(sqNotificationDTO.PostingDate, dtPickList.JSDateTimeFormat);
      eventData.Add("postingDate", formatDate);

      sqNotificationDTO.DocumentDate = DateTime.SpecifyKind(sqNotificationDTO.DocumentDate, DateTimeKind.Utc);
      sqNotificationDTO.DocumentDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(sqNotificationDTO.DocumentDate, sqNotificationDTO.TimeZone, false);
      formatDate = DateTimeHelper.FormatDate(sqNotificationDTO.DocumentDate, dtPickList.JSDateTimeFormat);
      eventData.Add("documentDate", formatDate);

      sqNotificationDTO.DeliveryDate = DateTime.SpecifyKind(sqNotificationDTO.DeliveryDate, DateTimeKind.Utc);
      sqNotificationDTO.DeliveryDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(sqNotificationDTO.DeliveryDate, sqNotificationDTO.TimeZone, false);
      formatDate = DateTimeHelper.FormatDate(sqNotificationDTO.DeliveryDate, dtPickList.JSDateTimeFormat);
      eventData.Add("deliveryDate", formatDate);
      //eventData.Add(NotificationConstants.AppIdKey, aRInvoiceNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, sqNotificationDTO.BusinessTenantId.ToString());
      //eventData.Add("appKey", aRInvoiceNotificationDTO.AppKey);
      eventData.Add("salesOrderPermissionMask", manageInvoicePermissionOnBizPaymentApp);
      eventData.Add("publisherName", sqNotificationDTO.PublisherName);
      eventData.Add("businessName", sqNotificationDTO.BusinessName);
      eventData.Add("customerName", sqNotificationDTO.CustomerName);
      eventData.Add("customerNo", sqNotificationDTO.ERPCustomerKey);
      eventData.Add("salesOrderNo", sqNotificationDTO.ERPSalesQuotationKey);
      eventData.Add("totalAmountWithCurrency", string.Format("{0} {1}", sqNotificationDTO.LocalCurrency, sqNotificationDTO.TotalPaymentDue));

      eventData.Add("publisherTenantId", sqNotificationDTO.PublisherTenantId.ToString());
      eventData.Add("createdByName", sqNotificationDTO.FullName);
      eventData.Add("createdByNo", sqNotificationDTO.UserIdentityNo);
      eventData.Add("subDomain", sqNotificationDTO.SubDomainName);
      eventData.Add("portalUrl", _businessEntityAppSettings.BizPayAppUrl);
      eventData.Add("copyright", sqNotificationDTO.Copyright);
      //ToDo: Change entity type using enum.
      eventData.Add("asTargetEntityType", "1");
      eventData.Add("asTargetEntityId", sqNotificationDTO.SalesQuotationId.ToString());
      eventData.Add("asAppId", Guid.Empty.ToString());

      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      //eventDataDict.Add("UserSession", MapNotitificationUserSession(businessOnBoardNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = (long)BusinessEntityNotificationEventEnum.BizAddSalesOrder;
      generateNotificationDTO.EventInfo = eventDataDict;
      //generateNotificationDTO.LoggedinUserId =  
      generateNotificationDTO.UseCacheForTemplate = false;
      _businessEntityNotificationService.GenerateNotificationAsync(generateNotificationDTO);
    }

    #region Private Methods

    private NotificationUserSessionDTO MapNotitificationUserSession(UserSession userSession)
    {
      NotificationUserSessionDTO NotificationUserSessionDTO = new NotificationUserSessionDTO();
      NotificationUserSessionDTO.AppId = userSession.AppId;
      NotificationUserSessionDTO.ID = userSession.ID;
      NotificationUserSessionDTO.IdentityServerId = userSession.IdentityServerId;
      NotificationUserSessionDTO.IdentityToken = userSession.IdentityToken;
      NotificationUserSessionDTO.Subdomain = userSession.Subdomain;
      NotificationUserSessionDTO.TenantId = userSession.TenantId;
      NotificationUserSessionDTO.TenantName = userSession.TenantName;
      NotificationUserSessionDTO.TenantUserId = userSession.TenantUserId;
      NotificationUserSessionDTO.UserName = userSession.UserName;
      NotificationUserSessionDTO.UserType = userSession.UserType;
      return NotificationUserSessionDTO;
    }

    #endregion
  }
}
