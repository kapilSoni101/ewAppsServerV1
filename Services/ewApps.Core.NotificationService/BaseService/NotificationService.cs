using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.DeeplinkServices;
using ewApps.Core.EmailService;
using ewApps.Core.SMSService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.Core.NotificationService {

    #region Abstract Methods Info
    // Derived classes (in their appropriate modules, like ED, DM etc.) 
    // must override following methods. Rest of Notification code is common.
    // ValidateNotificationData
    // GetRecepientList
    // GetSyncRowId
    // GetDeeplinkPayload
    // SetNotificationPayload
    // GetPushPayload
    // GetXMLArgumentsForPush
    // GetEmailPayload
    // GetXMLArgumentsForEmail
    // GenerateASNotificationPayload
    // GetBadgeCount
    // AddBadgeCount

    #endregion

    #region Virtual methods Info
    //Virtual Methods :Has some base implementation but if chnages are required by drive classes it can be override
    //1.GetNotificationInfo
    //2. ToXML
    #endregion

    /// <summary>
    /// Notification base class, It defines the flow to generate notification data.
    /// </summary>
    public abstract class NotificationService<T>:INotificationService<T> where T : NotificationRecipient, new() {

        #region Constructor and Variables

        private const string _userSessionKey = "UserSession";

        /// <summary>
        /// An instance of deeplink service implementation to generate universal link for email notification.
        /// </summary>
        protected IDeeplinkService _deeplinkService;

        /// <summary>
        /// An instance of email service to send email notification.
        /// </summary>
        protected IEmailService _emailService;

        /// <summary>
        /// An instance of SMS service to send SMS notification.
        /// </summary>
        protected ISMSService _smsService;

        /// <summary>
        /// An instance of <see cref="IASNotificationService"/> to generate related data.
        /// </summary>
        protected IASNotificationService _aSNotificationService;

        /// <summary>
        /// The application settings values that stores notificatoin configuration information.
        /// </summary>
        protected NotificationAppSettings _appSettings;

        /// <summary>
        /// The logger service to log information.
        /// </summary>
        protected ILogger<INotificationService<T>> _loggerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService{T}"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="emailService">The email service.</param>
        /// <param name="smsService">The SMS service.</param>
        /// <param name="appSettingOption">The application setting option.</param>
        public NotificationService(ILogger<INotificationService<T>> loggerService, IEmailService emailService, ISMSService smsService, IOptions<NotificationAppSettings> appSettingOption) {
            _loggerService = loggerService;
            _emailService = emailService;
            _smsService = smsService;
            _appSettings = appSettingOption.Value;
        }


        //ToDo: Notification Review: Does it required?
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService{T}"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="emailService">The email service.</param>
        /// <param name="smsService">The SMS service.</param>
        public NotificationService(ILogger<INotificationService<T>> loggerService, IEmailService emailService, ISMSService smsService) {
            _loggerService = loggerService;
            _emailService = emailService;
            _smsService = smsService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService{T}"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="emailService">The email service.</param>
        /// <param name="smsService">The SMS service.</param>
        /// <param name="aSNotificationService">a s notification service.</param>
        public NotificationService(ILogger<INotificationService<T>> loggerService, IEmailService emailService, ISMSService smsService, IASNotificationService aSNotificationService, IOptions<NotificationAppSettings> appSettingOption) {
            _loggerService = loggerService;
            _emailService = emailService;
            _smsService = smsService;
            _aSNotificationService = aSNotificationService;
            _appSettings = appSettingOption.Value;
        }

        #endregion Constructor and variables

        #region Public Methods

        /// <summary>
        /// Generates notification data in the notification table directly or incirectly by other service.
        /// </summary>
        /// <param name="generateNotificationDTO"></param>
        /// <param name="token">A cancellation token that help to control the execution of async notification process.</param>
        public async Task GenerateNotificationAsync(GenerateNotificationDTO generateNotificationDTO, CancellationToken token = default(CancellationToken)) {
            try {
                // Validate incoming data
                bool isValid = ValidateNotificationData(generateNotificationDTO.ModuleId, generateNotificationDTO.EventId, generateNotificationDTO.EventInfo);

                // Generate Notification Payload from given data
                NotificationPayload<T> notificationPayload = new NotificationPayload<T> {
                    ModuleId = generateNotificationDTO.ModuleId,
                    EventId = generateNotificationDTO.EventId,
                    EventInfo = generateNotificationDTO.EventInfo,
                    LoggedinUserId = generateNotificationDTO.LoggedinUserId,
                    SupportedDeliveryType = GetSupportedNotificationType(generateNotificationDTO.EventId),
                };

                #region Generate Notification Id and other IDs

                notificationPayload = GetNotificationInfo(notificationPayload, token); // virtual method with id generation that can be overrider in drive class if required

                if(notificationPayload.SupportedDeliveryType == NotificationDeliveryType.None) {
                    StringBuilder info = new StringBuilder();
                    info.AppendFormat("Delivery type is None for event no {0}.", notificationPayload.EventId);
                    info.AppendLine();
                    _loggerService.Log(LogLevel.Information, info.ToString());
                    return;
                }

                #endregion

                #region Get RecepientList

                // Get recipient list based on current notification payload.
                List<T> notificationRecipients = GetRecipientList(notificationPayload);

                notificationPayload.RecipientList = notificationRecipients;

                #endregion Get recepientList

                #region Generate DeepLink

                bool hasLinkError = false;
                if((notificationPayload.SupportedDeliveryType & NotificationDeliveryType.Email) == NotificationDeliveryType.Email) {

                    // Generate DeeplinkPayload from Notification Payload.
                    DeeplinkPayload deeplinkPayload = GetDeeplinkPayload(notificationPayload);

                    // Get the deeplink service based on EventID and ModuleID
                    // Generate the branch deeplink by passing required deeplink payload
                    // DeeplinkResultSet deeplinkResultSet =   _deeplinkService.GenerateDeeplinkAsync(deeplinkPayload, token).Result;
                    // _deeplinkService = new DeeplinkService(GetBranchKey(eventId).ToString(), _appSettings.Notification.BranchApiUrl);

                    // Initializes deeplink service to generate required deeplink.
                    _deeplinkService = new DeeplinkService(_appSettings.BranchApiUrl);

                    // Gets deeplinks required for current notification event.
                    DeeplinkResultSet deeplinkResultSet = _deeplinkService.GetDeeplinkList(deeplinkPayload);

                    //The branchlink and URL will be passed back to the flow for using further, So add this data to notificationPayload 
                    //returns jSON that need to be used and also the HasLinkError property for the email.
                    Tuple<string, bool> branchResult = SetDeeplinkResultInNotificationPayload(notificationPayload, deeplinkResultSet);

                    string branchDeeplinkJson = branchResult.Item1;

                    hasLinkError = branchResult.Item2;


                }

                #endregion Generate DeepLink


                #region Get XML Data

                // Generate XML string from the dictionary object that will be used by Push/Email services
                string XMLData = ToXML(notificationPayload);

                #endregion Get XML DAta

                #region Send Notifications

                //int badgeCount;
                //Loop for all recepient for sending push/Email, as each user has its own preferences Data so 
                //extract the information for each user, Generates the push based on user data like badge count and then 
                //add the Push/Email to the Notification Queue
                for(int i = 0; i < notificationRecipients.Count; i++) {

                    #region Get DeliveryTime

                    DateTime deliveryTime = DateTime.UtcNow;

                    #endregion

                    #region Send Push/Alert
                    //// IPushService pushService = new CommonRuntime.PushService.PushService();
                    // Dictionary<string, string> xmlArguments = GetXMLArgumentsForPush(recipientDT.Rows[i], payload);
                    ////PushPayload pushPayload = GetPushPayload(payload, recipientDT.Rows[i], deliveryTime, syncRowId, badgeCount, XMLData, branchDeeplinkJson, silentPush, useCacheForTemplate, xmlArguments);
                    #endregion

                    #region Send Email

                    try {
                        //ocan
                        // Check subscribedEvents is on for a user or not.
                        if((notificationPayload.SupportedDeliveryType & NotificationDeliveryType.Email) == NotificationDeliveryType.Email
                            && Convert.ToBoolean(notificationRecipients[i].EmailPreference)) {

                            if(generateNotificationDTO.NotificationToLoginUser == true || notificationPayload.LoggedinUserId != notificationRecipients[i].TenantUserId) {

                                Dictionary<string, string> xmlArguments = GetXSLArgumentsForEmail(notificationRecipients[i], notificationPayload);

                                EmailPayload emailPayload = GetEmailPayload(notificationPayload, notificationRecipients[i], XMLData, hasLinkError, xmlArguments);

                                if(generateNotificationDTO.EventInfo.ContainsKey(_userSessionKey)) {
                                    NotificationUserSessionDTO userSession = generateNotificationDTO.EventInfo[_userSessionKey] as NotificationUserSessionDTO;
                                    emailPayload.EmailUserSession = MapEmailUserSession(userSession);
                                }

                                bool success = await _emailService.SendEmailAsync(emailPayload, token);
                            }

                        }
                    }
                    catch(Exception ex) {
                        StringBuilder exceptionDetail = new StringBuilder();
                        exceptionDetail.Append("Exception occurred in NotificationService.GenerateNotificationAsync.EmailSection");
                        exceptionDetail.AppendLine();
                        exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                        _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                    }

                    #endregion

                    #region Send SMS

                    try {
                        // Check subscribedEvents is on for a user or not.
                        if((notificationPayload.SupportedDeliveryType & NotificationDeliveryType.SMS) == NotificationDeliveryType.SMS
                            && string.IsNullOrEmpty(notificationRecipients[i].SMSRecipient) == false
                            && Convert.ToBoolean(notificationRecipients[i].SMSPreference)) {

                            if(generateNotificationDTO.NotificationToLoginUser == true || notificationPayload.LoggedinUserId != notificationRecipients[i].TenantUserId) {

                                Dictionary<string, string> xmlArguments = GetXSLArgumentsForSMS(notificationRecipients[i], notificationPayload);

                                if(xmlArguments != null) {
                                    SMSPayload smsPayload = GetSMSPayload(notificationPayload, notificationRecipients[i], XMLData, hasLinkError, xmlArguments);

                                    if(generateNotificationDTO.EventInfo.ContainsKey(_userSessionKey)) {
                                        NotificationUserSessionDTO userSession = generateNotificationDTO.EventInfo[_userSessionKey] as NotificationUserSessionDTO;
                                        smsPayload.SMSUserSession = MapSMSUserSession(userSession);
                                    }

                                    bool success = await _smsService.SendSMSAsync(smsPayload, token);
                                }
                            }
                        }

                    }
                    catch(Exception ex) {
                        StringBuilder exceptionDetail = new StringBuilder();
                        exceptionDetail.Append("Exception occurred in NotificationService.GenerateNotificationAsync.SMSSection");
                        exceptionDetail.AppendLine();
                        exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                        _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                    }
                    #endregion

                    #region Add AS-Notification.

                    try {
                        // Check subscribedEvents is on for a user or not.
                        if((notificationPayload.SupportedDeliveryType & NotificationDeliveryType.ASNotification) == NotificationDeliveryType.ASNotification
                                      && notificationRecipients[i].TenantUserId != Guid.Empty
                                      && Convert.ToBoolean(notificationRecipients[i].ASPreference)) {

                            if(generateNotificationDTO.NotificationToLoginUser == true || notificationPayload.LoggedinUserId != notificationRecipients[i].TenantUserId) {

                                Dictionary<string, string> xmlArguments = GetXSLArgumentsForASNotification(notificationRecipients[i], notificationPayload);

                                if(xmlArguments != null) {
                                    ASNotificationPayload asNotificationPayload = GetASNotificationPayload(notificationPayload, notificationRecipients[i], XMLData, xmlArguments);

                                    ASNotificationDTO aSNotificationDTO = await _aSNotificationService.GetASNotificationAsync(asNotificationPayload, token);

                                    AddASNotification(aSNotificationDTO, notificationPayload);
                                }
                            }
                        }

                    }
                    catch(Exception ex) {
                        StringBuilder exceptionDetail = new StringBuilder();
                        exceptionDetail.Append("Exception occurred in NotificationService.GenerateNotificationAsync.ASSection");
                        exceptionDetail.AppendLine();
                        exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                        _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                    }

                    #endregion

                }

                #endregion
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in NotificationService:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        #endregion

        #region Private Method

        private EmailUserSessionDTO MapEmailUserSession(NotificationUserSessionDTO notificationUserSessionDTO) {
            EmailUserSessionDTO emailUserSessionDTO = new EmailUserSessionDTO();
            emailUserSessionDTO.AppId = notificationUserSessionDTO.AppId;
            emailUserSessionDTO.ID = notificationUserSessionDTO.ID;
            emailUserSessionDTO.IdentityServerId = notificationUserSessionDTO.IdentityServerId;
            emailUserSessionDTO.IdentityToken = notificationUserSessionDTO.IdentityToken;
            emailUserSessionDTO.Subdomain = notificationUserSessionDTO.Subdomain;
            emailUserSessionDTO.TenantId = notificationUserSessionDTO.TenantId;
            emailUserSessionDTO.TenantName = notificationUserSessionDTO.TenantName;
            emailUserSessionDTO.TenantUserId = notificationUserSessionDTO.TenantUserId;
            emailUserSessionDTO.UserName = notificationUserSessionDTO.UserName;
            emailUserSessionDTO.UserType = notificationUserSessionDTO.UserType;
            return emailUserSessionDTO;
        }

        private SMSUserSessionDTO MapSMSUserSession(NotificationUserSessionDTO notificationUserSessionDTO) {
            SMSUserSessionDTO smsUserSessionDTO = new SMSUserSessionDTO();
            smsUserSessionDTO.AppId = notificationUserSessionDTO.AppId;
            smsUserSessionDTO.ID = notificationUserSessionDTO.ID;
            smsUserSessionDTO.IdentityServerId = notificationUserSessionDTO.IdentityServerId;
            smsUserSessionDTO.IdentityToken = notificationUserSessionDTO.IdentityToken;
            smsUserSessionDTO.Subdomain = notificationUserSessionDTO.Subdomain;
            smsUserSessionDTO.TenantId = notificationUserSessionDTO.TenantId;
            smsUserSessionDTO.TenantName = notificationUserSessionDTO.TenantName;
            smsUserSessionDTO.TenantUserId = notificationUserSessionDTO.TenantUserId;
            smsUserSessionDTO.UserName = notificationUserSessionDTO.UserName;
            smsUserSessionDTO.UserType = notificationUserSessionDTO.UserType;
            return smsUserSessionDTO;
        }

        #endregion Private Method

        #region Validation

        /// <summary>
        /// Validates the given parameters to generate the notification like module and event id.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="eventInfo">The event information.</param>
        /// <returns>Returns true if all notification related required information is available.</returns>
        protected abstract bool ValidateNotificationData(int moduleId, long eventId, Dictionary<string, object> eventInfo);


        #endregion

        #region Notification Payload Data

        /// <summary>
        /// Gets notification recipient list based on input nofication payload.
        /// </summary>
        /// <param name="payload">Instance of notification payload use to get nofication recipients.</param>
        /// <returns>
        /// Returns list of notification recipient list.
        /// </returns>
        protected abstract List<T> GetRecipientList(NotificationPayload<T> payload);

        /// <summary>Returns branch api key to be use in deeplink generation based on module and event id.</summary>
        /// <param name="decisionParameters">An object with all decision parameter values.</param>
        /// <returns>Returns branch Api key.</returns>
        protected abstract object GetBranchKey(object decisionParameters);

        /// <summary>
        /// Adds information abt the current notification.
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <param name="token">The token.</param>
        /// <returns>Returns updated notification payload.</returns>
        protected virtual NotificationPayload<T> GetNotificationInfo(NotificationPayload<T> payload, CancellationToken token = default(CancellationToken)) {
            try {
                payload.NotificationId = Guid.NewGuid();
                payload.LinkedNotificationId = Guid.NewGuid();
                payload.TrackingId = Guid.NewGuid();  //Used in Push
                return payload;
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in NotificationService.GetNotificationInfo:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        /// <summary>
        /// Get XML from Additional Info
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <returns>Returns xml string from event data.</returns>
        protected virtual string ToXML(NotificationPayload<T> payload) {
            try {
                string xml = "";
                if(payload.EventInfo.ContainsKey(NotificationConstants.EventDataXmlKey)) {
                    return Convert.ToString(payload.EventInfo[NotificationConstants.EventDataXmlKey]);
                }
                else if(payload.EventInfo[NotificationConstants.EventDataKey].GetType().GenericTypeArguments[1].Name == typeof(string).Name) {
                    xml = XmlSerializationHelper.ToXML((Dictionary<string, string>)payload.EventInfo[NotificationConstants.EventDataKey]);
                }
                else if(payload.EventInfo[NotificationConstants.EventDataKey].GetType().GenericTypeArguments[1].Name == typeof(object).Name) {
                    xml = XmlSerializationHelper.ToXML((Dictionary<string, object>)payload.EventInfo[NotificationConstants.EventDataKey]);
                }
                return xml;

            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in NotificationService.ToXML:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        /// <summary>Gets the SyncRowId from the Database</summary>
        /// <param name="payload">An instance to notification payload to get sync row id.</param>
        /// <returns>Returns sync row id.</returns>
        protected abstract long GetSyncRowId(NotificationPayload<T> payload);

        public virtual NotificationDeliveryType GetSupportedNotificationType(long platformNotificationEvents) {
            return NotificationDeliveryType.Email | NotificationDeliveryType.SMS | NotificationDeliveryType.ASNotification;
        }

        #endregion

        #region Deeplink

        /// <summary>
        /// Create deeplink payload from given notification payload.
        /// Notification derived class has to provide deeplinks payload.
        /// </summary>
        /// <param name="notificationPayload">Notification payload information.</param>
        /// <returns>Returns generated deeplink payload based on provided notification payload.</returns>
        public abstract DeeplinkPayload GetDeeplinkPayload(NotificationPayload<T> notificationPayload);

        /// <summary>
        ///  Sets the Deeplink info back in Notification Payload
        /// </summary>
        /// <param name="notificationPayload">Notification payload information.</param>
        /// <param name="deeplinkResultSet"></param>
        /// <returns>Returns tuple that contains deeplink and deeplink status.</returns>
        public abstract Tuple<string, bool> SetDeeplinkResultInNotificationPayload(NotificationPayload<T> notificationPayload, DeeplinkResultSet deeplinkResultSet);

        #endregion

        #region Push

        /// <summary>
        /// XMLArguments are set from Payload,UserDataRow andConfig file, as Derived class is aware of required arguments, this is a abstract method.
        /// </summary>
        /// <param name="userDataRow">The user data row.</param>
        /// <param name="payload">The payload.</param>
        /// <returns>Returns xml argument list in form dictionary.</returns>
        protected abstract Dictionary<string, string> GetXSLArgumentsForPush(T userDataRow, NotificationPayload<T> payload);

        #endregion

        #region Email

        /// <summary>
        /// Sets the email payload by Notification payload.
        /// </summary>
        /// <param name="notificationPayload">The notification payload.</param>
        /// <param name="recepientDataRow">The recepient data row.</param>
        /// <param name="eventXMLData">The event XML data.</param>
        /// <param name="hasLinkError">if set to <c>true</c> [has link error].</param>
        /// <param name="xmlArguments">The XML arguments.</param>
        /// <returns>Returns updated email payload.</returns>
        public abstract EmailPayload GetEmailPayload(NotificationPayload<T> notificationPayload, T recepientDataRow, string eventXMLData, bool hasLinkError, Dictionary<string, string> xmlArguments);


        /// <summary>
        /// This method provides the current event notification information (in form of Dictionary (i.e. Key-Value pair list)) based on input notificatoin payload.
        /// </summary>
        /// <param name="userDataRow">Notification recipient information.</param>
        /// <param name="payload">Notification payload.</param>
        /// <returns>Returns notification information in form of Dictionary (i.e. Key-Value pair list).</returns>
        protected abstract Dictionary<string, string> GetXSLArgumentsForEmail(T userDataRow, NotificationPayload<T> payload);

        #endregion

        #region SMS

        /// <summary>
        /// Sets the SMS payload from Notification payload.
        /// </summary>
        /// <param name="notificationPayload">The notification payload.</param>
        /// <param name="recepientDataRow">The recepient data row.</param>
        /// <param name="eventXMLData">The event XML data.</param>
        /// <param name="hasLinkError">if set to <c>true</c> [has link error].</param>
        /// <param name="xmlArguments">The XML arguments.</param>
        /// <returns>Returns updated sms payload.</returns>
        public abstract SMSPayload GetSMSPayload(NotificationPayload<T> notificationPayload, T recepientDataRow, string eventXMLData, bool hasLinkError, Dictionary<string, string> xmlArguments);

        /// <summary>
        /// This method provides the current event notification information (in form of Dictionary (i.e. Key-Value pair list)) based on input notificatoin payload.
        /// </summary>
        /// <param name="userDataRow">Notification recipient information.</param>
        /// <param name="payload">Notification payload.</param>
        /// <returns>Returns notification information in form of Dictionary (i.e. Key-Value pair list).</returns>
        protected abstract Dictionary<string, string> GetXSLArgumentsForSMS(T userDataRow, NotificationPayload<T> payload);

        #endregion

        #region AS-Notification

        // ToDo: Make this method abstract.
        /// <summary>
        /// Sets the SMS payload from AS-Notification payload.
        /// </summary>
        /// <returns>Returns updated AS-Notification payload.</returns>
        public virtual ASNotificationPayload GetASNotificationPayload(NotificationPayload<T> notificationPayload, T recepientDataRow, string eventXMLData, Dictionary<string, string> xslArguments) {
            return null;
        }

        // ToDo: Make this method abstract.
        /// <summary>
        /// Adds AS-Notification with input details.
        /// </summary>
        public virtual void GenerateASNotification(NotificationPayload<T> notificationPayload, T recepientDataRow, ASNotificationDTO aSNotificationDTO) {
        }

        // ToDo: Make this method abstract.
        /// <summary>
        /// This method provides the current event notification information (in form of Dictionary (i.e. Key-Value pair list)) based on input notificatoin payload.
        /// </summary>
        /// <param name="userDataRow">Notification recipient information.</param>
        /// <param name="payload">Notification payload.</param>
        /// <returns>Returns notification information in form of Dictionary (i.e. Key-Value pair list).</returns>
        protected virtual Dictionary<string, string> GetXSLArgumentsForASNotification(T userDataRow, NotificationPayload<T> payload) {
            return null;
        }

        // ToDo: Make this method abstract.
        public virtual bool AddASNotification(ASNotificationDTO aSNotificationDTO, NotificationPayload<T> notificationPayload) {
            return false;
        }

        #region Old AS-Methods

        protected virtual ewApps.Core.NotificationService.NotificationLogOpDTO GenerateASNotificationPayload(NotificationPayload<NotificationRecipient> payload, NotificationRecipient recepientDataRow, DateTime deliveryTime, long syncRowId, int badgeCount, string XMLData) {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region BadgeCount

        /// <summary>
        /// Gets the current batch count of push recepient user.
        /// </summary>
        /// <returns>Returns updated badge count.</returns>
        protected abstract int GetBadgeCount(NotificationPayload<T> payload, T userDataRow);

        /// <summary>
        /// Adds the badge count entry and gets the current badge count of recipient user
        /// </summary>
        protected abstract int AddBadgeCount(NotificationPayload<T> payload, T userDataRow);

        #endregion

    }

}
