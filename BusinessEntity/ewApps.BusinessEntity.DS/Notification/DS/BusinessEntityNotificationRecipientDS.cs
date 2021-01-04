using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.QData;
using ewApps.Core.BaseService;
using ewApps.Core.NotificationService;

namespace ewApps.BusinessEntity.DS
{
  public class BusinessEntityNotificationRecipientDS : IBusinessEntityNotificationRecipientDS
  {
    IBusinessEntityNotificationRecipientRepository _beNotificationRecipientRepository;
    IQNotificationData _qNotificationData;

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessEntityNotificationRecipientDS"/> class.
    /// </summary>
    /// <param name="businessEntityNotificationRecipientRepository">The business entity notification recipient repository.</param>
    /// <param name="qNotificationData">The q notification data.</param>
    public BusinessEntityNotificationRecipientDS(IBusinessEntityNotificationRecipientRepository businessEntityNotificationRecipientRepository, IQNotificationData qNotificationData)
    {
      _beNotificationRecipientRepository = businessEntityNotificationRecipientRepository;
      _qNotificationData = qNotificationData;
    }

    /// <inheritdoc/>
    public async Task<List<NotificationRecipient>> GetAddARInvoiceNotificationBizRecipientAsync(Guid publisherTenantId, Guid businessTenantId, int userType, int userStatus, CancellationToken cancellationToken = default(CancellationToken))
    {
      List<NotificationRecipient> notificationRecipients = new List<NotificationRecipient>();
      // Get all applications subscribed to business
      List<AppInfoDTO> appInfoList = await _qNotificationData.GetAppListByBusinessTenantIdAsync(publisherTenantId, businessTenantId, cancellationToken);

      //foreach(AppInfoDTO appInfo in appInfoList) {
      //    notificationRecipients.AddRange(await _businessEntityNotificationRecipientRepository.GetARInvoiceNotificationRecipientAsync(appInfo.Id, businessTenantId, invoicePermissionMask, userType, userStatus, cancellationToken));
      //}

      AppInfoDTO customerAppInfo = appInfoList.FirstOrDefault(i => i.AppKey == AppKeyEnum.cust.ToString());
      if (customerAppInfo != null)
      {
        BusinessUserCustomerAppPermissionEnum permissionBitMask = BusinessUserCustomerAppPermissionEnum.ManageARInvoices | BusinessUserCustomerAppPermissionEnum.ViewARInvoices;
        // ToDo: Change preference enum value of BusinessUserCustomerAppPreferenceEnum.
        notificationRecipients.AddRange(await _beNotificationRecipientRepository.GetARInvoiceNotificationBizRecipientForCustAppAsync(customerAppInfo.Id, businessTenantId, (long)permissionBitMask, userType, userStatus, (long)BusinessUserCustomerAppPreferenceEnum.NewARInvoiceIsGenerated, cancellationToken));
      }

      AppInfoDTO paymentAppInfo = appInfoList.FirstOrDefault(i => i.AppKey == AppKeyEnum.pay.ToString());
      if (paymentAppInfo != null)
      {
        BusinessUserPaymentAppPermissionEnum permissionBitMask = BusinessUserPaymentAppPermissionEnum.ViewInvoices;
        // ToDo: Change preference enum value of BusinessUserPaymentAppPreferenceEnum.
        notificationRecipients = await _beNotificationRecipientRepository.GetARInvoiceNotificationBizRecipientForPayAppAsync(paymentAppInfo.Id, businessTenantId, (long)permissionBitMask, userType, userStatus, (long)BusinessUserPaymentAppPreferenceEnum.NewARInvoiceIsGenerated, cancellationToken);
      }

      // check if recipient exist then make first record 
      ILookup<string, NotificationRecipient> groupedNotificationRecipients = notificationRecipients.ToLookup(i => i.Email);

      foreach (var loopupResult in groupedNotificationRecipients)
      {
        int indexer = 0;

        bool emailPref = loopupResult.Any(i => i.EmailPreference == true);
        bool smsPref = loopupResult.Any(i => i.SMSPreference == true);

        foreach (NotificationRecipient recipient in loopupResult)
        {

          if (emailPref == true && indexer == 0)
          {
            recipient.EmailPreference = true;
          }
          else
          {
            recipient.EmailPreference = false;
          }

          if (smsPref == true && indexer == 0)
          {
            recipient.SMSPreference = true;
          }
          else
          {
            recipient.SMSPreference = false;
          }

          indexer++;
        }
      }
      return notificationRecipients;
    }



    public async Task<List<NotificationRecipient>> GetUpdateARInvoiceNotificationBizRecipientAsync(Guid publisherTenantId, Guid businessTenantId, int userType, int userStatus, CancellationToken cancellationToken = default(CancellationToken))
    {
      List<NotificationRecipient> notificationRecipients = new List<NotificationRecipient>();
      // Get all applications subscribed to business
      List<AppInfoDTO> appInfoList = await _qNotificationData.GetAppListByBusinessTenantIdAsync(publisherTenantId, businessTenantId, cancellationToken);

      //foreach(AppInfoDTO appInfo in appInfoList) {
      //    notificationRecipients.AddRange(await _businessEntityNotificationRecipientRepository.GetARInvoiceNotificationRecipientAsync(appInfo.Id, businessTenantId, invoicePermissionMask, userType, userStatus, cancellationToken));
      //}

      AppInfoDTO customerAppInfo = appInfoList.FirstOrDefault(i => i.AppKey == AppKeyEnum.cust.ToString());
      if (customerAppInfo != null)
      {
        BusinessUserCustomerAppPermissionEnum permissionBitMask = BusinessUserCustomerAppPermissionEnum.ManageARInvoices | BusinessUserCustomerAppPermissionEnum.ViewARInvoices;
        // ToDo: Change preference enum value of BusinessUserCustomerAppPreferenceEnum.
        notificationRecipients.AddRange(await _beNotificationRecipientRepository.GetARInvoiceNotificationBizRecipientForCustAppAsync(customerAppInfo.Id, businessTenantId, (long)permissionBitMask, userType, userStatus, (long)BusinessUserCustomerAppPreferenceEnum.ExistingARInvoiceIsUpdated, cancellationToken));
      }

      AppInfoDTO paymentAppInfo = appInfoList.FirstOrDefault(i => i.AppKey == AppKeyEnum.pay.ToString());
      if (paymentAppInfo != null)
      {
        BusinessUserPaymentAppPermissionEnum permissionBitMask = BusinessUserPaymentAppPermissionEnum.ViewInvoices;
        // ToDo: Change preference enum value of BusinessUserPaymentAppPreferenceEnum.
        notificationRecipients = await _beNotificationRecipientRepository.GetARInvoiceNotificationBizRecipientForPayAppAsync(paymentAppInfo.Id, businessTenantId, (long)permissionBitMask, userType, userStatus, (long)BusinessUserPaymentAppPreferenceEnum.ExistingARInvoiceIsUpdated, cancellationToken);
      }

      // check if recipient exist then make first record 
      ILookup<string, NotificationRecipient> groupedNotificationRecipients = notificationRecipients.ToLookup(i => i.Email);

      foreach (var loopupResult in groupedNotificationRecipients)
      {
        int indexer = 0;

        bool emailPref = loopupResult.Any(i => i.EmailPreference == true);
        bool smsPref = loopupResult.Any(i => i.SMSPreference == true);

        foreach (NotificationRecipient recipient in loopupResult)
        {

          if (emailPref == true && indexer == 0)
          {
            recipient.EmailPreference = true;
          }
          else
          {
            recipient.EmailPreference = false;
          }

          if (smsPref == true && indexer == 0)
          {
            recipient.SMSPreference = true;
          }
          else
          {
            recipient.SMSPreference = false;
          }

          indexer++;
        }
      }
      return notificationRecipients;
    }

    /// <inheritdoc/>
    public async Task<List<NotificationRecipient>> GetAddCustomerNotificationRecipientAsync(Guid publisherTenantId, Guid businessTenantId, Guid loginUserId, int userType, int userStatus, CancellationToken cancellationToken = default(CancellationToken))
    {
      List<NotificationRecipient> notificationRecipients = new List<NotificationRecipient>();
      // Get all applications subscribed to business
      List<AppInfoDTO> appInfoList = await _qNotificationData.GetAppListByBusinessTenantIdAsync(publisherTenantId, businessTenantId, cancellationToken);


      foreach (AppInfoDTO appInfo in appInfoList)
      {
        if (appInfo.AppKey == AppKeyEnum.pay.ToString())
        {

          BusinessUserPaymentAppPermissionEnum bizcustomerPermissionMask = BusinessUserPaymentAppPermissionEnum.ManageCustomers | BusinessUserPaymentAppPermissionEnum.ViewCustomers;
          notificationRecipients.AddRange(await _beNotificationRecipientRepository.GetAddCustomerNotificationRecipientAsync(appInfo.Id, businessTenantId, loginUserId,(int)bizcustomerPermissionMask, userType, userStatus, (long)BusinessUserCustomerAppPreferenceEnum.NewCustomerIsCreated, cancellationToken));
        }
        else if (appInfo.AppKey == AppKeyEnum.cust.ToString())
        {
          BusinessUserCustomerAppPermissionEnum bizcustomerPermissionMask = bizcustomerPermissionMask = BusinessUserCustomerAppPermissionEnum.ManageCustomers | BusinessUserCustomerAppPermissionEnum.ViewCustomers;

          notificationRecipients.AddRange(await _beNotificationRecipientRepository.GetAddCustomerNotificationRecipientForCustAppAsync(appInfo.Id, businessTenantId, loginUserId,(int)bizcustomerPermissionMask, userType, userStatus, (long)BusinessUserCustomerAppPreferenceEnum.NewCustomerIsCreated, cancellationToken));
        }
       
      }
      BusinessUserCustomerAppPermissionEnum permissionMask = BusinessUserCustomerAppPermissionEnum.ManageCustomers | BusinessUserCustomerAppPermissionEnum.ViewCustomers;
      Guid bizSetupApplicationId = new Guid(BusinessEntityConstants.BizSetUpApplicationId);
      notificationRecipients.AddRange(await _beNotificationRecipientRepository.GetAddCustomerNotificationRecipientForCustAppAsync(bizSetupApplicationId, businessTenantId, loginUserId, (int)permissionMask, userType, userStatus, (long)BusinessUserCustomerAppPreferenceEnum.ExistingCustomerIsUpdated, cancellationToken));

      // check if recipient exist then make first record 
      ILookup<string, NotificationRecipient> groupedNotificationRecipients = notificationRecipients.ToLookup(i => i.Email);

      foreach (var loopupResult in groupedNotificationRecipients)
      {
        int indexer = 0;

        bool emailPref = loopupResult.Any(i => i.EmailPreference == true);
        bool smsPref = loopupResult.Any(i => i.SMSPreference == true);

        foreach (NotificationRecipient recipient in loopupResult)
        {

          if (emailPref == true && indexer == 0)
          {
            recipient.EmailPreference = true;
          }
          else
          {
            recipient.EmailPreference = false;
          }

          if (smsPref == true && indexer == 0)
          {
            recipient.SMSPreference = true;
          }
          else
          {
            recipient.SMSPreference = false;
          }

          indexer++;
        }
      }
      return notificationRecipients;
    }
    /// <inheritdoc/>
    public async Task<List<NotificationRecipient>> GetUpdateCustomerNotificationRecipientAsync(Guid publisherTenantId, Guid businessTenantId, Guid loginUserId, int userType, int userStatus, CancellationToken cancellationToken = default(CancellationToken))
    {
      List<NotificationRecipient> notificationRecipients = new List<NotificationRecipient>();
      // Get all applications subscribed to business
      List<AppInfoDTO> appInfoList = await _qNotificationData.GetAppListByBusinessTenantIdAsync(publisherTenantId, businessTenantId, cancellationToken);


      foreach (AppInfoDTO appInfo in appInfoList)
      {
        if (appInfo.AppKey == AppKeyEnum.pay.ToString())
        {

          BusinessUserPaymentAppPermissionEnum bizcustomerPermissionMask = BusinessUserPaymentAppPermissionEnum.ManageCustomers | BusinessUserPaymentAppPermissionEnum.ViewCustomers;
          notificationRecipients.AddRange(await _beNotificationRecipientRepository.GetAddCustomerNotificationRecipientAsync(appInfo.Id, businessTenantId, loginUserId,(int)bizcustomerPermissionMask, userType, userStatus, (long)BusinessUserCustomerAppPreferenceEnum.ExistingCustomerIsUpdated, cancellationToken));
        }
        else if (appInfo.AppKey == AppKeyEnum.cust.ToString())
        {
          BusinessUserCustomerAppPermissionEnum bizcustomerPermissionMask = bizcustomerPermissionMask = BusinessUserCustomerAppPermissionEnum.ManageCustomers | BusinessUserCustomerAppPermissionEnum.ViewCustomers;

          notificationRecipients.AddRange(await _beNotificationRecipientRepository.GetAddCustomerNotificationRecipientForCustAppAsync(appInfo.Id, businessTenantId, loginUserId,(int)bizcustomerPermissionMask, userType, userStatus, (long)BusinessUserCustomerAppPreferenceEnum.ExistingCustomerIsUpdated, cancellationToken));
        }
      }

        BusinessUserCustomerAppPermissionEnum permissionMask = BusinessUserCustomerAppPermissionEnum.ManageCustomers | BusinessUserCustomerAppPermissionEnum.ViewCustomers;
        Guid bizSetupApplicationId = new Guid(BusinessEntityConstants.BizSetUpApplicationId);
        notificationRecipients.AddRange(await _beNotificationRecipientRepository.GetAddCustomerNotificationRecipientForCustAppAsync(bizSetupApplicationId, businessTenantId, loginUserId, (int)permissionMask, userType, userStatus, (long)BusinessUserCustomerAppPreferenceEnum.ExistingCustomerIsUpdated, cancellationToken));
      
      // check if recipient exist then make first record 
      ILookup<string, NotificationRecipient> groupedNotificationRecipients = notificationRecipients.ToLookup(i => i.Email);

      foreach (var loopupResult in groupedNotificationRecipients)
      {
        int indexer = 0;

        bool emailPref = loopupResult.Any(i => i.EmailPreference == true);
        bool smsPref = loopupResult.Any(i => i.SMSPreference == true);

        foreach (NotificationRecipient recipient in loopupResult)
        {

          if (emailPref == true && indexer == 0)
          {
            recipient.EmailPreference = true;
          }
          else
          {
            recipient.EmailPreference = false;
          }

          if (smsPref == true && indexer == 0)
          {
            recipient.SMSPreference = true;
          }
          else
          {
            recipient.SMSPreference = false;
          }

          indexer++;
        }
      }
      return notificationRecipients;
    }
    /// <inheritdoc/>
    public async Task<List<NotificationRecipient>> GetAddSalesOrderNotificationRecipientAsync(Guid publisherTenantId, Guid businessTenantId, int userType, int userStatus, CancellationToken cancellationToken = default(CancellationToken))
    {
      List<NotificationRecipient> notificationRecipients = new List<NotificationRecipient>();
      // Get all applications subscribed to business
      List<AppInfoDTO> appInfoList = await _qNotificationData.GetAppListByBusinessTenantIdAsync(publisherTenantId, businessTenantId, cancellationToken);

      //foreach(AppInfoDTO appInfo in appInfoList) {
      // Get Recipient for Business Portal - Customer App
      AppInfoDTO customerAppInfo = appInfoList.FirstOrDefault(i => i.AppKey == AppKeyEnum.cust.ToString());
      if (customerAppInfo != null)
      {
        BusinessUserCustomerAppPermissionEnum permissionBitMask = BusinessUserCustomerAppPermissionEnum.ManageSalesOrders | BusinessUserCustomerAppPermissionEnum.ViewSalesOrders;
        // ToDo: Change preference enum value of BusinessUserCustomerAppPreferenceEnum.
        try
        {
          notificationRecipients = await _beNotificationRecipientRepository.GetSalesOrderNotificationRecipientAsync(customerAppInfo.Id, businessTenantId, (long)permissionBitMask, userType, userStatus, (int)BusinessEntityNotificationEventEnum.BizAddSalesOrder, cancellationToken);
        }
        catch (Exception e)
        {
          throw;
        }

      }

      return notificationRecipients;
    }


    /// <inheritdoc/>
    public async Task<List<NotificationRecipient>> GetARInvoiceNotificationCustomerUserRecipientAsync(Guid publisherTenantId, Guid businessTenantId, Guid businessPartnerTenantId, CancellationToken cancellationToken = default(CancellationToken))
    {
      List<NotificationRecipient> notificationRecipients = new List<NotificationRecipient>();
      // Get all applications subscribed to business
      List<AppInfoDTO> appInfoList = await _qNotificationData.GetAppListByCustomerTenantIdAsync(publisherTenantId, businessTenantId, cancellationToken);

      //foreach(AppInfoDTO appInfo in appInfoList) {
      //    notificationRecipients.AddRange(await _businessEntityNotificationRecipientRepository.GetARInvoiceNotificationRecipientAsync(appInfo.Id, businessTenantId, invoicePermissionMask, userType, userStatus, cancellationToken));
      //}

      AppInfoDTO customerAppInfo = appInfoList.FirstOrDefault(i => i.AppKey == AppKeyEnum.cust.ToString());
      if (customerAppInfo != null)
      {
        CustomerUserCustomerAppPermissionEnum permissionBitMask = CustomerUserCustomerAppPermissionEnum.ViewAPInvoices;
        // ToDo: Change preference enum value of BusinessUserCustomerAppPreferenceEnum.
        notificationRecipients.AddRange(await _beNotificationRecipientRepository.GetARInvoiceCustomerRecipientAsync(customerAppInfo.Id, businessTenantId, businessPartnerTenantId,(int)CustomerUserCustomerAppPreferenceEnum.NewAPInvoiceIsGenerated, (int)permissionBitMask, cancellationToken));
      }

      AppInfoDTO paymentAppInfo = appInfoList.FirstOrDefault(i => i.AppKey == AppKeyEnum.pay.ToString());
      if (paymentAppInfo != null)
      {
        CustomerUserPaymentAppPermissionEnum permissionBitMask = CustomerUserPaymentAppPermissionEnum.ViewInvoices;
        // ToDo: Change preference enum value of BusinessUserPaymentAppPreferenceEnum.
        notificationRecipients = await _beNotificationRecipientRepository.GetARInvoiceCustomerRecipientAsync(paymentAppInfo.Id, businessTenantId, businessPartnerTenantId,(int)CustomerUserPaymentAppPreferenceEnum.NewAPInvoiceIsGenerated, (int)permissionBitMask, cancellationToken);
      }

      // check if recipient exist then make first record 
      ILookup<string, NotificationRecipient> groupedNotificationRecipients = notificationRecipients.ToLookup(i => i.Email);

      foreach (var loopupResult in groupedNotificationRecipients)
      {
        int indexer = 0;

        bool emailPref = loopupResult.Any(i => i.EmailPreference == true);
        bool smsPref = loopupResult.Any(i => i.SMSPreference == true);

        foreach (NotificationRecipient recipient in loopupResult)
        {

          if (emailPref == true && indexer == 0)
          {
            recipient.EmailPreference = true;
          }
          else
          {
            recipient.EmailPreference = false;
          }

          if (smsPref == true && indexer == 0)
          {
            recipient.SMSPreference = true;
          }
          else
          {
            recipient.SMSPreference = false;
          }

          indexer++;
        }
      }
      return notificationRecipients;
    }

    /// <inheritdoc/>
    public async Task<List<NotificationRecipient>> GetNotificationBizRecipientAsync(Guid publisherTenantId, Guid businessTenantId, int userType, int userStatus, long permissionBitMask, long preferenceBitMask, CancellationToken cancellationToken = default(CancellationToken))
    {
      List<NotificationRecipient> notificationRecipients = new List<NotificationRecipient>();
      // Get all applications subscribed to business
      List<AppInfoDTO> appInfoList = await _qNotificationData.GetAppListByBusinessTenantIdAsync(publisherTenantId, businessTenantId, cancellationToken);

      AppInfoDTO customerAppInfo = appInfoList.FirstOrDefault(i => i.AppKey == AppKeyEnum.cust.ToString());
      if (customerAppInfo != null)
      {

        // ToDo: Change preference enum value of BusinessUserCustomerAppPreferenceEnum.
        notificationRecipients = await _beNotificationRecipientRepository.GetARInvoiceNotificationBizRecipientForCustAppAsync(customerAppInfo.Id, businessTenantId, permissionBitMask, userType, userStatus, preferenceBitMask, cancellationToken);
      }


      return notificationRecipients;
    }

    /// <inheritdoc/>
    public async Task<List<NotificationRecipient>> GetAddSalesQuotationNotificationRecipientAsync(Guid publisherTenantId, Guid businessTenantId, int userType, int userStatus, CancellationToken cancellationToken = default(CancellationToken))
    {
      List<NotificationRecipient> notificationRecipients = new List<NotificationRecipient>();
      // Get all applications subscribed to business
      List<AppInfoDTO> appInfoList = await _qNotificationData.GetAppListByBusinessTenantIdAsync(publisherTenantId, businessTenantId, cancellationToken);


      // Get Recipient for Business Portal - Customer App
      AppInfoDTO customerAppInfo = appInfoList.FirstOrDefault(i => i.AppKey == AppKeyEnum.cust.ToString());
      if (customerAppInfo != null)
      {
        BusinessUserCustomerAppPermissionEnum permissionBitMask = BusinessUserCustomerAppPermissionEnum.ManageSalesQuotations | BusinessUserCustomerAppPermissionEnum.ViewSalesQuotations;
        // ToDo: Change preference enum value of BusinessUserCustomerAppPreferenceEnum.
        try
        {
          notificationRecipients = await _beNotificationRecipientRepository.GetSalesQuotationNotificationRecipientAsync(customerAppInfo.Id, businessTenantId, (long)permissionBitMask, userType, userStatus, (int)BusinessEntityNotificationEventEnum.BizAddSalesQuotation, cancellationToken);
        }
        catch (Exception e)
        {
          throw;
        }

      }

      return notificationRecipients;
    }

  }
}
