/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.EmailService;
using ewApps.Core.ExceptionService;
using ewApps.Core.NotificationService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This class implements all the notification triggered from publiser.
    /// </summary>
    public class PublisherNotificationHandler:IPublisherNotificationHandler {

        #region Local member variable 

        IPublisherNotificationService _pubNotificationService;
        ITokenInfoDS _tokenInfoDS;
        IUnitOfWork _unitOfWork;
        ExceptionAppSettings _exceptionAppSetting;
        IEmailService _emailService;
        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appPortalAppSettings;

        #endregion Local member variable 

        #region Contructor

        /// <summary>
        /// Initilizes all the required dependecies and member variables.
        /// </summary>
        /// <param name="pubNotificationService"><see cref="IPublisherNotificationService"/> to send the notification data.</param>
        /// <param name="unitOfWork">To Track the db changes and save changes in the database.</param>
        /// <param name="tokenInfoDS">Stores the toekn data to be sent to identity server while processing the deeplink</param>
        /// <param name="exceptionAppSetting">An instance of <see cref="ExceptionAppSettings"/> to get exception setting information.</param>
        public PublisherNotificationHandler(IEmailService emailService,IPublisherNotificationService pubNotificationService, IUnitOfWork unitOfWork, ITokenInfoDS tokenInfoDS, IOptions<ExceptionAppSettings> exceptionAppSetting, IUserSessionManager userSessionManager, IOptions<AppPortalAppSettings> appPortalAppSettings) {
            _pubNotificationService = pubNotificationService;
            _tokenInfoDS = tokenInfoDS;
            _unitOfWork = unitOfWork;
            _exceptionAppSetting = exceptionAppSetting.Value;
            _emailService = emailService;
            _userSessionManager = userSessionManager;
            _appPortalAppSettings = appPortalAppSettings.Value;
        }

        #endregion Contructor

        #region Public methods 

        #region Account

        //4
        ///<inheritdoc/>
        public async Task SendPublisherUserNewEmailInvite(PublisherNotificationDTO publisherNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //publisherCompanyName
            //invitedUser
            //publisherInvitinguserName
            //subDomain
            //invitedUserEmailId
            //publisherPortalURL
            //copyrightText
            //activationLink

            #endregion XSlt Parameters Commented

            // Generate TokenInfo data for the notification.
            #region TokeInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = publisherNotificationDTO.PasswordCode;
            tokenDataDTO.Email = publisherNotificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = publisherNotificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = publisherNotificationDTO.InvitedUserTenantId;
            tokenDataDTO.AppKey = publisherNotificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = (int)UserTypeEnum.Publisher;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = tokenDataDTO.AppKey;
            tokenInfo.TenantUserId = publisherNotificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = tokenDataDTO.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)PublisherTokenTypeEnum.PublisherUserWithNewEmailInvite;
            tokenInfo.UserType = (int)UserTypeEnum.Publisher;

            _tokenInfoDS.Add(tokenInfo);
            _unitOfWork.SaveAll();

            #endregion TokeInfo

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appid", publisherNotificationDTO.InvitedUserAppId.ToString());
            eventData.Add("appuserid", publisherNotificationDTO.InvitedUserId.ToString());

            eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
            eventData.Add("invitedUser", publisherNotificationDTO.InvitedUserFullName);
            eventData.Add("publisherInvitinguserName", publisherNotificationDTO.InvitorUserFullName);
            eventData.Add("subDomain", publisherNotificationDTO.SubDomain);
            eventData.Add("invitedUserEmailId", publisherNotificationDTO.InvitedUserEmail);
            eventData.Add("publisherPortalURL", publisherNotificationDTO.PublisherPortalURL);
            eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

            #endregion EventData

            // Generate the deeplink data for the notification.
            #region Deeplink Data

            Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            deeplinkInfo.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
            deeplinkInfo.Add("appuserid", publisherNotificationDTO.InvitedUserId.ToString());
            deeplinkInfo.Add("identityserveruserid", publisherNotificationDTO.InvitedUserIdentityUserId.ToString());
            deeplinkInfo.Add("usertype", Convert.ToString((int)UserTypeEnum.Publisher));
            deeplinkInfo.Add("code", publisherNotificationDTO.PasswordCode);
            deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());
            deeplinkInfo.Add("tenantLanguage", publisherNotificationDTO.TenantLanguage);
            deeplinkInfo.Add("subDomain", publisherNotificationDTO.SubDomain);

            #endregion Deeplink Data
            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Publisher;
            generateNotificationDTO.EventId = (long)PublisherNotificationEvent.InvitePublisherUserWithNewEmail;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        //5
        ///<inheritdoc/>
        public async Task SendPublisherUserExistingEmailInvite(PublisherNotificationDTO publisherNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //     < xsl:param name = "publisherCompanyName" />
            //< xsl:param name = "invitedUser" />
            // < xsl:param name = "publisherInvitinguserName" />
            //  < xsl:param name = "domainScreenURL" />
            //   < xsl:param name = "subDomain" />
            //    < xsl:param name = "invitedUserEmailId" />
            //     < xsl:param name = "publisherPortalURL" />
            //      < xsl:param name = "copyrightText" />
            //       < xsl:param name = "emailId" />
            //        < xsl:param name = "portalName" />

            #endregion XSlt Parameters Commented

            // Generate TokenInfo data for the notification.
            #region TokeInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = publisherNotificationDTO.PasswordCode;
            tokenDataDTO.Email = publisherNotificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = publisherNotificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = publisherNotificationDTO.InvitedUserTenantId;
            tokenDataDTO.AppKey = publisherNotificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = (int)UserTypeEnum.Publisher;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = tokenDataDTO.AppKey;
            tokenInfo.TenantUserId = publisherNotificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = tokenDataDTO.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)PublisherTokenTypeEnum.PublisherUserWithExistingEmailInvite;
            tokenInfo.UserType = (int)UserTypeEnum.Publisher;

            _tokenInfoDS.Add(tokenInfo);
            _unitOfWork.SaveAll();

            #endregion TokeInfo

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appid", publisherNotificationDTO.InvitedUserAppId.ToString());
            eventData.Add("appuserid", publisherNotificationDTO.InvitedUserAppId.ToString());

            eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
            eventData.Add("invitedUser", publisherNotificationDTO.InvitedUserFullName);
            eventData.Add("publisherInvitinguserName", publisherNotificationDTO.InvitorUserFullName);
            eventData.Add("subDomain", publisherNotificationDTO.SubDomain);
            eventData.Add("invitedUserEmailId", publisherNotificationDTO.InvitedUserEmail);
            eventData.Add("publisherPortalURL", publisherNotificationDTO.PublisherPortalURL);
            eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);
            eventData.Add("emailId", publisherNotificationDTO.InvitedUserEmail);
            eventData.Add("portalName", publisherNotificationDTO.PortalName);

            #endregion EventData

            // Generate the deeplink data for the notification.
            #region Deeplink Data

            Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            deeplinkInfo.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
            deeplinkInfo.Add("appuserid", publisherNotificationDTO.InvitedUserId.ToString());
            deeplinkInfo.Add("identityserveruserid", publisherNotificationDTO.InvitedUserIdentityUserId.ToString());
            deeplinkInfo.Add("usertype", Convert.ToString((int)UserTypeEnum.Publisher));
            deeplinkInfo.Add("code", publisherNotificationDTO.PasswordCode);
            deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());
            deeplinkInfo.Add("tenantLanguage", publisherNotificationDTO.TenantLanguage);
            deeplinkInfo.Add("subDomain", publisherNotificationDTO.SubDomain);

            #endregion Deeplink Data

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Publisher;
            generateNotificationDTO.EventId = (long)PublisherNotificationEvent.InvitePublisherUserWithExistingEmail;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        //15
        ///<inheritdoc/>
        public async Task SendPublisherForgotPasswordEmail(PublisherNotificationDTO publisherNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //     < xsl:param name = "publisherCompanyName" />
            //< xsl:param name = "publisherUserName" />
            // < xsl:param name = "publisherUserEmailId" />
            //  < xsl:param name = "activationLink" />
            //   < xsl:param name = "copyrightText" />

            #endregion XSlt Parameters Commented

            // Generate TokenInfo data for the notification.
            #region TokeInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = publisherNotificationDTO.PasswordCode;
            tokenDataDTO.Email = publisherNotificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = publisherNotificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = publisherNotificationDTO.InvitedUserTenantId;
            tokenDataDTO.AppKey = publisherNotificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = (int)UserTypeEnum.Publisher;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = tokenDataDTO.AppKey;
            tokenInfo.TenantUserId = publisherNotificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = tokenDataDTO.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)PublisherTokenTypeEnum.PublisherUserForgotPassword;
            tokenInfo.UserType = (int)UserTypeEnum.Publisher;

            _tokenInfoDS.Add(tokenInfo);
            _unitOfWork.SaveAll();

            #endregion TokeInfo

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data for getting the receipent list.
            eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appuserid", publisherNotificationDTO.InvitedUserId.ToString());

            eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
            eventData.Add("publisherUserName", publisherNotificationDTO.InvitedUserFullName);
            eventData.Add("publisherUserEmailId", publisherNotificationDTO.InvitedUserEmail);
            eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

            #endregion EventData

            // Generate the deeplink data for the notification.
            #region Deeplink Data

            Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            deeplinkInfo.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
            deeplinkInfo.Add("appuserid", publisherNotificationDTO.InvitedUserId.ToString());
            deeplinkInfo.Add("identityserveruserid", publisherNotificationDTO.InvitedUserIdentityUserId.ToString());
            deeplinkInfo.Add("usertype", Convert.ToString((int)UserTypeEnum.Publisher));
            deeplinkInfo.Add("code", publisherNotificationDTO.PasswordCode);
            deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());
            deeplinkInfo.Add("tenantLanguage", publisherNotificationDTO.TenantLanguage);
            deeplinkInfo.Add("subDomain", publisherNotificationDTO.SubDomain);

            #endregion Deeplink Data

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Publisher;
            generateNotificationDTO.EventId = (long)PublisherNotificationEvent.PublisherUserForgotPassword;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        //16
        ///<inheritdoc/>
        public async Task SendBusinessPrimaryUserWithNewEmailInvite(PublisherNotificationDTO publisherNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //< xsl:param name = "publisherCompanyName" />
            //   < xsl:param name = "businessCompanyName" />
            //    < xsl:param name = "invitedUser" />
            //      < xsl:param name = "subDomain" />
            //       < xsl:param name = "invitedUserEmailId" />
            //        < xsl:param name = "businessPortalURL" />
            //         < xsl:param name = "copyrightText" />
            //     < xsl:param name = "activationLink" />

            #endregion XSlt Parameters Commented

            // Generate TokenInfo data for the notification.
            #region TokeInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = publisherNotificationDTO.PasswordCode;
            tokenDataDTO.Email = publisherNotificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = publisherNotificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = publisherNotificationDTO.InvitedUserTenantId;
            tokenDataDTO.AppKey = publisherNotificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = (int)UserTypeEnum.Business;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = tokenDataDTO.AppKey;
            tokenInfo.TenantUserId = publisherNotificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = tokenDataDTO.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)PublisherTokenTypeEnum.BusinessPrimaryUserWithNewEmailInvite;
            tokenInfo.UserType = (int)UserTypeEnum.Business;

            _tokenInfoDS.Add(tokenInfo);
            _unitOfWork.SaveAll();

            #endregion TokeInfo

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appid", publisherNotificationDTO.InvitedUserAppId.ToString());
            eventData.Add("appuserid", publisherNotificationDTO.InvitedUserId.ToString());

            eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
            eventData.Add("businessCompanyName", publisherNotificationDTO.BusinessCompanyName);
            eventData.Add("invitedUser", publisherNotificationDTO.InvitedUserFullName);
            eventData.Add("subDomain", publisherNotificationDTO.SubDomain);
            eventData.Add("invitedUserEmailId", publisherNotificationDTO.InvitedUserEmail);
            eventData.Add("businessPortalURL", publisherNotificationDTO.BusinessPortalURL);
            eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

            #endregion EventData

            #region Deeplink Data

            Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            deeplinkInfo.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
            deeplinkInfo.Add("appuserid", publisherNotificationDTO.InvitedUserId.ToString());
            deeplinkInfo.Add("identityserveruserid", publisherNotificationDTO.InvitedUserIdentityUserId.ToString());
            deeplinkInfo.Add("usertype", Convert.ToString((int)UserTypeEnum.Publisher));
            deeplinkInfo.Add("code", publisherNotificationDTO.PasswordCode);
            deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());
            deeplinkInfo.Add("tenantLanguage", publisherNotificationDTO.TenantLanguage);
            deeplinkInfo.Add("subDomain", publisherNotificationDTO.SubDomain);

            #endregion Deeplink Data

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)PublisherNotificationEvent.InviteBusinessPrimaryUserWithNewEmail;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        //18
        ///<inheritdoc/>
        public async Task SendBusinessPrimaryUserWithExistingEmailInvite(PublisherNotificationDTO publisherNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //     < xsl:param name = "publisherCompanyName" />
            //< xsl:param name = "businessCompanyName" />
            // < xsl:param name = "invitedUser" />
            //   < xsl:param name = "subDomain" />
            //    < xsl:param name = "invitedUserEmailId" />
            //     < xsl:param name = "businessPortalURL" />
            //      < xsl:param name = "copyrightText" />
            //       < xsl:param name = "emailId" />
            //        < xsl:param name = "portalName" />
            //  < xsl:param name = "domainScreenURL" />

            #endregion XSlt Parameters Commented

            // Generate TokenInfo data for the notification.
            #region TokeInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = publisherNotificationDTO.PasswordCode;
            tokenDataDTO.Email = publisherNotificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = publisherNotificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = publisherNotificationDTO.InvitedUserTenantId;
            tokenDataDTO.AppKey = publisherNotificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = (int)UserTypeEnum.Business;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = tokenDataDTO.AppKey;
            tokenInfo.TenantUserId = publisherNotificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = tokenDataDTO.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)PublisherTokenTypeEnum.BusinessPrimaryUserWithExistingEmailInvite;
            tokenInfo.UserType = (int)UserTypeEnum.Business;

            _tokenInfoDS.Add(tokenInfo);
            _unitOfWork.SaveAll();

            #endregion TokeInfo

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appid", publisherNotificationDTO.InvitedUserAppId.ToString());
            eventData.Add("appuserid", publisherNotificationDTO.InvitedUserId.ToString());

            eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
            eventData.Add("businessCompanyName", publisherNotificationDTO.BusinessCompanyName);
            eventData.Add("invitedUser", publisherNotificationDTO.InvitedUserFullName);
            eventData.Add("subDomain", publisherNotificationDTO.SubDomain);
            eventData.Add("invitedUserEmailId", publisherNotificationDTO.InvitedUserEmail);
            eventData.Add("businessPortalURL", publisherNotificationDTO.BusinessPortalURL);
            eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);
            eventData.Add("emailId", publisherNotificationDTO.InvitedUserEmail);
            eventData.Add("portalName", publisherNotificationDTO.PortalName);

            #endregion EventData

            #region Deeplink Data

            Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            deeplinkInfo.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
            deeplinkInfo.Add("appuserid", publisherNotificationDTO.InvitedUserId.ToString());
            deeplinkInfo.Add("identityserveruserid", publisherNotificationDTO.InvitedUserIdentityUserId.ToString());
            deeplinkInfo.Add("usertype", Convert.ToString((int)UserTypeEnum.Publisher));
            deeplinkInfo.Add("code", publisherNotificationDTO.PasswordCode);
            deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());
            deeplinkInfo.Add("tenantLanguage", publisherNotificationDTO.TenantLanguage);
            deeplinkInfo.Add("subDomain", publisherNotificationDTO.SubDomain);

            #endregion Deeplink Data

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)PublisherNotificationEvent.InviteBusinessPrimaryUserWithExistingEmail;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        #endregion Account

   
         
        ///<inheritdoc/>
        public async Task NewPublisherOnboard(PublisherNotificationDTO publisherNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //     < xsl:param name = "publisherCompanyName" />
            //< xsl:param name = "platformCompanyName" />
            // < xsl:param name = "copyrightText" />

            #endregion XSlt Parameters Commented

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("emailPrefrence", ((long)PlatformEmailPreferenceEnum.NewPublisherOnboared).ToString());

            eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
            eventData.Add("platformCompanyName", publisherNotificationDTO.PlatformCompanyName);
            eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

            #endregion EventData

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)PublisherNotificationEvent.PublisherUserOnBoard;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = false;
            await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        
    

        //7
       
   
        //9
        ///<inheritdoc/>
        public async Task BusinessMarkAsActiveInactiveToPublisher(PublisherNotificationDTO publisherNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //< xsl:param name = "businessCompanyName" />
            //< xsl:param name = "publisherCompanyName" />
            //< xsl:param name = "publisherUserChanged" />
            //< xsl:param name = "status" />
            //< xsl:param name = "administratorMessage" />
            //< xsl:param name = "copyrightText" />

            #endregion XSlt Parameters Commented

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appid", publisherNotificationDTO.InvitedUserAppId.ToString());

            eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
            eventData.Add("businessCompanyName", publisherNotificationDTO.BusinessCompanyName);
            eventData.Add("publisherUserChanged", publisherNotificationDTO.InvitorUserFullName);
            eventData.Add("status", publisherNotificationDTO.Status);
            eventData.Add("administratorMessage", publisherNotificationDTO.AdministratorMessage);
            eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

            #endregion EventData

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)PublisherNotificationEvent.BusinessStatusChangedToPubUser;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

   
  

        //13
        ///<inheritdoc/>
        public async Task BusinessDeletedToBusinessPartnerUser(PublisherNotificationDTO publisherNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //< xsl:param name = "publisherCompanyName" />
            //< xsl:param name = "businessPartnerType" />  
            //< xsl:param name = "businessPartnerCompanyName" />
            //< xsl:param name = "businessCompanyName" />
            //< xsl:param name = "portalType" />
            //< xsl:param name = "copyrightText" />

            #endregion XSlt Parameters Commented

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());

            eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
           // eventData.Add("businessPartnerType", publisherNotificationDTO.PublisherNotificationDic["businessPartnerType"]);
           // eventData.Add("businessPartnerCompanyName", publisherNotificationDTO.PublisherNotificationDic["businessPartnerCompanyName"]);
            eventData.Add("businessCompanyName", publisherNotificationDTO.BusinessCompanyName);
           // eventData.Add("portalType", publisherNotificationDTO.PublisherNotificationDic["portalType"]);
            eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

            #endregion EventData

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

           // await _pubNotificationService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)PublisherNotificationEvent.BusinessDeletedBusinessPartnerUser, false, eventDataDict, publisherNotificationDTO.UserSession.TenantUserId);

        }

        ///<inheritdoc/>
        public async Task BusinessDeletedToPublisherUser(PublisherNotificationDTO publisherNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //< xsl:param name = "publisherCompanyName" />
            //< xsl:param name = "publisherUserChanged" />
            //< xsl:param name = "businessCompanyName" />
            //< xsl:param name = "copyrightText" />

            #endregion XSlt Parameters Commented

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appid", publisherNotificationDTO.InvitedUserAppId.ToString());

            eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
            eventData.Add("publisherUserChanged", publisherNotificationDTO.InvitorUserFullName);
            eventData.Add("businessCompanyName", publisherNotificationDTO.BusinessCompanyName);
            eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

            #endregion EventData

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)PublisherNotificationEvent.BusinessIsDeletedToPubUser;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = false;
            await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

    

        //20
        ///<inheritdoc/>
        public async Task PublisherUserPermissionChanged(PublisherPermissionNotificationDTO publisherPermissionNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

              //<xsl:param name="platformCompanyName"/>
              //<xsl:param name="publisherName"/>
              //<xsl:param name="publisherUserName"/>
              //<xsl:param name="publisherUserNameChange"/>
              //<xsl:param name="subDomain"/>
              //<xsl:param name="portalUrl"/>
              //<xsl:param name="copyright"/>

            #endregion XSlt Parameters Commented

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();

            //DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(publisherPermissionNotificationDTO.DateTimeFormat);

            //publisherPermissionNotificationDTO.DateTime = DateTime.SpecifyKind(publisherPermissionNotificationDTO.DateTime, DateTimeKind.Utc);
            //publisherPermissionNotificationDTO.DateTime = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(publisherPermissionNotificationDTO.DateTime, publisherPermissionNotificationDTO.TimeZone, false);
            //string formatDate = DateTimeHelper.FormatDate(publisherPermissionNotificationDTO.DateTime, dtPickList.JSDateTimeFormat);
            //eventData.Add("dateTime", formatDate);

            // Data fro getting the receipent list.
            eventData.Add("platformCompanyName", publisherPermissionNotificationDTO.PlatformCompanyName);            
            eventData.Add("publisherName", publisherPermissionNotificationDTO.PublisherCompanyName);
            eventData.Add("publisherUserName", publisherPermissionNotificationDTO.InvitedUserFullName);
            eventData.Add("publisherUserNameChange", publisherPermissionNotificationDTO.InvitorUserFullName);
            eventData.Add("dateTime", DateTime.Now.ToString());

            eventData.Add("tenantId", publisherPermissionNotificationDTO.InvitedUserTenantId.ToString());

            eventData.Add("tenantUserId", publisherPermissionNotificationDTO.InvitedUserId.ToString());           
            eventData.Add("subDomain", publisherPermissionNotificationDTO.SubDomain);
            eventData.Add("portalUrl", string.Format(_appPortalAppSettings.PublisherPortalClientURL, publisherPermissionNotificationDTO.UserSession.Subdomain));
            eventData.Add("copyright", publisherPermissionNotificationDTO.CopyRigthText);

            eventData.Add("asAppId", publisherPermissionNotificationDTO.UserSession.AppId.ToString());
            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.Publisher).ToString());
            eventData.Add("asTargetEntityId", publisherPermissionNotificationDTO.UserSession.TenantId.ToString());

            #endregion EventData

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = PubNavigationKeyEnum.UserDetailsUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherPermissionNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)PublisherNotificationEvent.PublisherUserPermissionChanged;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = publisherPermissionNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        //21
        ///<inheritdoc/>
        public async Task NewPublisherUserOnBoard(PublisherNotificationDTO publisherNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //< xsl:param name = "publisherCompanyName" />
            //< xsl:param name = "newUserName" />
            //< xsl:param name = "copyrightText" />

            #endregion XSlt Parameters Commented

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("emailPrefrence", ((long)PublisherEmailPreferenceEnum.NewPublisherUserOnboard).ToString());

            eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
            eventData.Add("newUserName", publisherNotificationDTO.InvitedUserFullName);
            eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

            #endregion EventData

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)PublisherNotificationEvent.PublisherUserOnBoard;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = false;
            await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        //22
        ///<inheritdoc/>
        public async Task PublisherUserDeleted(PublisherNotificationDTO publisherNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //< xsl:param name = "publisherCompanyName" />
            //< xsl:param name = "publisherUserName" />
            //< xsl:param name = "publisherUserChanged" />
            //< xsl:param name = "copyrightText" />

            #endregion XSlt Parameters Commented

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appid", publisherNotificationDTO.InvitedUserAppId.ToString());
            eventData.Add("appuserid", publisherNotificationDTO.InvitedUserId.ToString());

            eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
            eventData.Add("publisherUserName", publisherNotificationDTO.InvitorUserFullName);
            eventData.Add("publisherUserChanged", publisherNotificationDTO.InvitorUserFullName);
            eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

            #endregion EventData

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)PublisherNotificationEvent.PublisherUserDeleted;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

     

        //24
        ///<inheritdoc/>
        public async Task PublisherApplicationStatusChangedToBusinessPartnerUser(PublisherNotificationDTO publisherNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //< xsl:param name = "businessCompanyName" />
            //< xsl:param name = "businessPartnerType" /> 
            //< xsl:param name = "publisherCompanyName" />
            //< xsl:param name = "businessPartnerCompanyName" />
            //< xsl:param name = "status" />
            //< xsl:param name = "applicationName" />
            //< xsl:param name = "administratorMessage" />      
            //< xsl:param name = "copyrightText" />

            #endregion XSlt Parameters Commented

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
            //eventData.Add("businessPartnerTenantId", publisherNotificationDTO.PublisherNotificationDic["businessPartnerTenantId"]);
            eventData.Add("appid", publisherNotificationDTO.InvitedUserAppId.ToString());

            //eventData.Add("businessCompanyName", publisherNotificationDTO.BusinessCompanyName);
            //eventData.Add("businessPartnerType", publisherNotificationDTO.PublisherNotificationDic["businessPartnerType"]);
            //eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
            //eventData.Add("businessPartnerCompanyName", publisherNotificationDTO.PublisherNotificationDic["businessPartnerCompanyName"]);
            //eventData.Add("status", publisherNotificationDTO.PublisherNotificationDic["status"]);
            //eventData.Add("applicationName", publisherNotificationDTO.AppLicationName);
            //eventData.Add("administratorMessage", publisherNotificationDTO.PublisherNotificationDic["administratorMessage"]);
            //eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

            #endregion EventData

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

           // await _pubNotificationService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)PublisherNotificationEvent.PublisherApplicationStatusChangedToBusinessPartnerUser, false, eventDataDict, publisherNotificationDTO.UserSession.TenantUserId);

        }

        //25
        ///<inheritdoc/>
        public async Task PublisherApplicationStatusChangedToPublisherUser(PublisherNotificationDTO publisherNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //< xsl:param name = "publisherCompanyName" />
            //< xsl:param name = "applicationName" /> 
            //< xsl:param name = "publisherUserNameChanged" />
            //< xsl:param name = "status" />
            //< xsl:param name = "administratorMessage" />    
            //< xsl:param name = "copyrightText" />

            #endregion XSlt Parameters Commented

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appid", publisherNotificationDTO.InvitedUserAppId.ToString());

            eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
            eventData.Add("applicationName", publisherNotificationDTO.AppLicationName);
            eventData.Add("publisherUserNameChanged", publisherNotificationDTO.InvitorUserFullName);
            eventData.Add("status", publisherNotificationDTO.Status);
            eventData.Add("administratorMessage", publisherNotificationDTO.AdministratorMessage);
            eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

            #endregion EventData

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)PublisherNotificationEvent.ApplicationStatusChangedToPubUser;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = false;
            await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        }

        //2
        ///<inheritdoc/>
        public async Task SendEmailForContactUs(ContactUsDTO ContactUsDTO) {

            UserSession session = _userSessionManager.GetSession();

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //<xsl:param name="osName"/>
            //<xsl:param name="browserName"/>
            //<xsl:param name="appVersion"/>
            //<xsl:param name="userName"/>
            //<xsl:param name="userEmail"/>
            //<xsl:param name="phoneNumber"/>
            //<xsl:param name="portal"/>
            //<xsl:param name="accountName"/>
            //<xsl:param name="application"/>
            //<xsl:param name="timeOfAction"/>
            //<xsl:param name="comments"/>
            //<xsl:param name="copyrightText"/>

            #endregion XSlt Parameters Commented

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("osName", ContactUsDTO.OS.ToString());
            eventData.Add("browserName", ContactUsDTO.Browser.ToString());
            eventData.Add("appVersion", ContactUsDTO.AppVersion);

            eventData.Add("userName", ContactUsDTO.Name);
            eventData.Add("userEmail", ContactUsDTO.Email);
            eventData.Add("phoneNumber", ContactUsDTO.Phone.ToString());
            eventData.Add("portal", ContactUsDTO.PortalName);
            eventData.Add("timeOfAction", DateTime.Now.ToString());
            eventData.Add("copyrightText", ContactUsDTO.CopyRightText);
            eventData.Add("comments", ContactUsDTO.Message);
            eventData.Add("accountName", ContactUsDTO.CompanyName);
            eventData.Add("application", ContactUsDTO.ApplicationName);

            #endregion EventData

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(session));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Publisher;
            generateNotificationDTO.EventId = (long)PublisherNotificationEvent.ContactUsToPlatformUser;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = session.TenantUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        /// <inheritdoc/>
        public void SendErrorEmail(ErrorLogEmailDTO errorlogEmailDTO, UserSession userSession) {
            Tuple<string, string> emailBody = errorlogEmailDTO.GetErrorEmailDetail();
            AdhocEmailDTO adhocEmailDTO = new AdhocEmailDTO();
            adhocEmailDTO.EmailAddress = _exceptionAppSetting.ReceiverEmail;
            adhocEmailDTO.MessagePart1 = emailBody.Item1;
            adhocEmailDTO.MessagePart2 = emailBody.Item2;
            adhocEmailDTO.DeliveryType = (int)NotificationDeliveryType.Email;

            if(userSession != null) {
                adhocEmailDTO.AppId = userSession.AppId;
            }

            _emailService.SendAdhocEmail(adhocEmailDTO);
        }

        #endregion Public methods 

        private NotificationUserSessionDTO MapNotitificationUserSession(UserSession userSession) {
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


        #region Old Methods


        /////<inheritdoc/>
        //public async Task PublisherApplicationStatusChangedToBusinessUser(PublisherNotificationDTO publisherNotificationDTO) {

        //    // Parameters needed in xslt
        //    #region XSlt Parameters Commented

        //    //< xsl:param name = "businessCompanyName" />
        //    //< xsl:param name = "publisherCompanyName" /> 
        //    //< xsl:param name = "status" />
        //    //< xsl:param name = "applicationName" />
        //    //< xsl:param name = "administratorMessage" />
        //    //< xsl:param name = "copyrightText" />

        //    #endregion XSlt Parameters Commented

        //    // Generated notification data
        //    #region EventData

        //    Dictionary<string, string> eventData = new Dictionary<string, string>();
        //    // Data fro getting the receipent list.
        //    eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
        //    eventData.Add("appid", publisherNotificationDTO.InvitedUserAppId.ToString());

        //    eventData.Add("businessCompanyName", publisherNotificationDTO.BusinessCompanyName);
        //    eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
        //    eventData.Add("status", publisherNotificationDTO.Status);
        //    eventData.Add("applicationName", publisherNotificationDTO.AppLicationName);
        //    eventData.Add("administratorMessage", publisherNotificationDTO.AdministratorMessage);
        //    eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

        //    #endregion EventData

        //    // Creates list of dictionary for event data.
        //    Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
        //    eventDataDict.Add("EventData", eventData);
        //    eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

        //    // Send notification
        //    GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
        //    generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
        //    generateNotificationDTO.EventId = (long)PublisherNotificationEvent.PublisherApplicationStatusChangedToBusinessUser;
        //    generateNotificationDTO.EventInfo = eventDataDict;
        //    generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
        //    generateNotificationDTO.UseCacheForTemplate = false;
        //    await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        //}

        /////<inheritdoc/>
        //public async Task PublisherUserMarkAsActiveInActive(PublisherNotificationDTO publisherNotificationDTO) {

        //    // Parameters needed in xslt
        //    #region XSlt Parameters Commented

        //    //< xsl:param name = "publisherCompanyName" />
        //    //< xsl:param name = "publisherUserName" />
        //    //< xsl:param name = "publisherUserChanged" />
        //    //< xsl:param name = "status" />
        //    //< xsl:param name = "copyrightText" />

        //    #endregion XSlt Parameters Commented

        //    // Generated notification data
        //    #region EventData

        //    Dictionary<string, string> eventData = new Dictionary<string, string>();
        //    // Data fro getting the receipent list.
        //    eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
        //    eventData.Add("appid", publisherNotificationDTO.InvitedUserAppId.ToString());
        //    eventData.Add("appuserid", publisherNotificationDTO.InvitedUserId.ToString());
        //    eventData.Add("emailPrefrence", ((long)PublisherEmailPreferenceEnum.MyActiveInactive).ToString());

        //    eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
        //    eventData.Add("publisherUserName", publisherNotificationDTO.InvitedUserFullName);
        //    eventData.Add("publisherUserChanged", publisherNotificationDTO.InvitorUserFullName);
        //    eventData.Add("status", publisherNotificationDTO.Status);
        //    eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

        //    #endregion EventData

        //    // Creates list of dictionary for event data.
        //    Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
        //    eventDataDict.Add("EventData", eventData);
        //    eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

        //    // Send notification
        //    GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
        //    generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
        //    generateNotificationDTO.EventId = (long)PublisherNotificationEvent.PublisherUserMarkAsActiveInactive;
        //    generateNotificationDTO.EventInfo = eventDataDict;
        //    generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
        //    generateNotificationDTO.UseCacheForTemplate = false;
        //    await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        //}


        /////<inheritdoc/>
        //public async Task BusinessDeletedToBusinessUser(PublisherNotificationDTO publisherNotificationDTO) {

        //    // Parameters needed in xslt
        //    #region XSlt Parameters Commented

        //    //< xsl:param name = "publisherCompanyName" />
        //    //< xsl:param name = "platformCompanyName" />
        //    //< xsl:param name = "businessCompanyName" />
        //    //< xsl:param name = "copyrightText" />

        //    #endregion XSlt Parameters Commented

        //    // Generated notification data
        //    #region EventData

        //    Dictionary<string, string> eventData = new Dictionary<string, string>();
        //    // Data fro getting the receipent list.
        //    eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());

        //    eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
        //    eventData.Add("platformCompanyName", publisherNotificationDTO.PlatformCompanyName);
        //    eventData.Add("businessCompanyName", publisherNotificationDTO.BusinessCompanyName);
        //    eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

        //    #endregion EventData

        //    // Creates list of dictionary for event data.
        //    Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
        //    eventDataDict.Add("EventData", eventData);
        //    eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

        //    // Send notification
        //    GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
        //    generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
        //    generateNotificationDTO.EventId = (long)PublisherNotificationEvent.BusinessDeletedToBusinesUser;
        //    generateNotificationDTO.EventInfo = eventDataDict;
        //    generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
        //    generateNotificationDTO.UseCacheForTemplate = false;
        //    await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        //}


        ////10
        /////<inheritdoc/>
        //public async Task BusinessMarkAsActiveInactiveToBusiness(PublisherNotificationDTO publisherNotificationDTO) {

        //    // Parameters needed in xslt
        //    #region XSlt Parameters Commented

        //    //< xsl:param name = "businessCompanyName" />
        //    //< xsl:param name = "publisherCompanyName" />
        //    //< xsl:param name = "status" />
        //    //< xsl:param name = "administratorMessage" />
        //    //< xsl:param name = "copyrightText" />

        //    #endregion XSlt Parameters Commented

        //    // Generated notification data
        //    #region EventData

        //    Dictionary<string, string> eventData = new Dictionary<string, string>();
        //    // Data fro getting the receipent list.
        //    eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());

        //    eventData.Add("businessCompanyName", publisherNotificationDTO.BusinessCompanyName);
        //    eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
        //    eventData.Add("status", publisherNotificationDTO.Status);
        //    eventData.Add("administratorMessage", publisherNotificationDTO.AdministratorMessage);
        //    eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

        //    #endregion EventData

        //    // Creates list of dictionary for event data.
        //    Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
        //    eventDataDict.Add("EventData", eventData);
        //    eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

        //    // Send notification
        //    GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
        //    generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
        //    generateNotificationDTO.EventId = (long)PublisherNotificationEvent.BusinessActiveInactiveToBusinesUser;
        //    generateNotificationDTO.EventInfo = eventDataDict;
        //    generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
        //    generateNotificationDTO.UseCacheForTemplate = false;
        //    await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        //}

        ////11
        /////<inheritdoc/>
        //public async Task BusinessMarkAsActiveInactiveToBusinessPartner(PublisherNotificationDTO publisherNotificationDTO) {

        //    // Parameters needed in xslt
        //    #region XSlt Parameters Commented

        //    //< xsl:param name = "businessCompanyName" />
        //    //< xsl:param name = "businessPartnerType" />
        //    //< xsl:param name = "portalType" />
        //    //< xsl:param name = "publisherCompanyName" />
        //    //< xsl:param name = "businessPartnerCompanyName" />
        //    //< xsl:param name = "status" />
        //    //< xsl:param name = "administratorMessage" />
        //    //< xsl:param name = "copyrightText" />

        //    #endregion XSlt Parameters Commented

        //    // Generated notification data
        //    #region EventData

        //    Dictionary<string, string> eventData = new Dictionary<string, string>();
        //    // Data fro getting the receipent list.
        //    eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
        //    // eventData.Add("businessPartnerTenantId", publisherNotificationDTO.PublisherNotificationDic["businessPartnerTenantId"]);

        //    eventData.Add("businessCompanyName", publisherNotificationDTO.BusinessCompanyName);
        //    // eventData.Add("businessPartnerType", publisherNotificationDTO.PublisherNotificationDic["businessPartnerType"]);
        //    //eventData.Add("portalType", publisherNotificationDTO.PublisherNotificationDic["portalType"]);
        //    eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
        //    // eventData.Add("businessPartnerCompanyName", publisherNotificationDTO.PublisherNotificationDic["businessPartnerCompanyName"]);
        //    eventData.Add("status", publisherNotificationDTO.Status);
        //    eventData.Add("administratorMessage", publisherNotificationDTO.AdministratorMessage);
        //    eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

        //    #endregion EventData

        //    // Creates list of dictionary for event data.
        //    Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
        //    eventDataDict.Add("EventData", eventData);
        //    eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

        //    //  await _pubNotificationService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)PublisherNotificationEvent.BusinessActiveInactiveToBusinessPartnerUser, false, eventDataDict, publisherNotificationDTO.UserSession.TenantUserId);

        //}





        ////8
        /////<inheritdoc/>
        //public async Task BusinessExistingSubscriptionPlanChangedToBusinessUser(PublisherNotificationDTO publisherNotificationDTO) {

        //    // Parameters needed in xslt
        //    #region XSlt Parameters Commented

        //    //< xsl:param name = "publisherCompanyName" />
        //    //< xsl:param name = "applicationName" /> 
        //    //< xsl:param name = "businessCompanyName" />
        //    //< xsl:param name = "oldsubscriptionPlanName" />
        //    //< xsl:param name = "newSubscriptionPlanName" />
        //    //< xsl:param name = "subDomain" />
        //    //< xsl:param name = "invitedUserEmailId" />
        //    //< xsl:param name = "businessPortalURL" />
        //    //< xsl:param name = "copyrightText" />

        //    #endregion XSlt Parameters Commented

        //    // Generated notification data
        //    #region EventData

        //    Dictionary<string, string> eventData = new Dictionary<string, string>();
        //    // Data for getting the receipent list.
        //    eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
        //    eventData.Add("appid", publisherNotificationDTO.InvitedUserAppId.ToString());

        //    eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
        //    eventData.Add("businessCompanyName", publisherNotificationDTO.BusinessCompanyName);
        //    eventData.Add("applicationName", publisherNotificationDTO.AppLicationName);
        //    eventData.Add("oldsubscriptionPlanName", publisherNotificationDTO.OldSubscriptionPlanName);
        //    eventData.Add("newSubscriptionPlanName", publisherNotificationDTO.NewSubscriptionPlanName);
        //    eventData.Add("subDomain", publisherNotificationDTO.SubDomain);
        //    eventData.Add("businessPortalURL", publisherNotificationDTO.BusinessPortalURL);
        //    eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

        //    #endregion EventData

        //    // Creates list of dictionary for event data.
        //    Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
        //    eventDataDict.Add("EventData", eventData);
        //    eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

        //    GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
        //    generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
        //    generateNotificationDTO.EventId = (long)PublisherNotificationEvent.BusinessExistingSubscriptionPlanChangedToBusinessUser;
        //    generateNotificationDTO.EventInfo = eventDataDict;
        //    generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
        //    generateNotificationDTO.UseCacheForTemplate = false;
        //    await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        //}


        /////<inheritdoc/>
        //public async Task ApplicationSubscribedToBusiness(PublisherNotificationDTO publisherNotificationDTO) {

        //    // Parameters needed in xslt
        //    #region XSlt Parameters Commented

        //    //< xsl:param name = "publisherCompanyName" />
        //    //< xsl:param name = "applicationName" />
        //    //< xsl:param name = "businessCompanyName" />
        //    //< xsl:param name = "subDomain" />
        //    //< xsl:param name = "applicationURL" />
        //    //< xsl:param name = "copyrightText" />

        //    #endregion XSlt Parameters Commented

        //    // Generated notification data
        //    #region EventData

        //    Dictionary<string, string> eventData = new Dictionary<string, string>();
        //    // Data fro getting the receipent list.
        //    eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
        //    eventData.Add("appid", publisherNotificationDTO.InvitedUserAppId.ToString());

        //    eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
        //    eventData.Add("applicationName", publisherNotificationDTO.AppLicationName);
        //    eventData.Add("businessCompanyName", publisherNotificationDTO.BusinessCompanyName);
        //    eventData.Add("subDomain", publisherNotificationDTO.SubDomain);
        //    eventData.Add("applicationURL", publisherNotificationDTO.ApplicationURL);
        //    eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

        //    #endregion EventData

        //    // Creates list of dictionary for event data.
        //    Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
        //    eventDataDict.Add("EventData", eventData);
        //    eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

        //    // Send notification
        //    GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
        //    generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
        //    generateNotificationDTO.EventId = (long)PublisherNotificationEvent.ApplicationSubscribedToBusiness;
        //    generateNotificationDTO.EventInfo = eventDataDict;
        //    generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
        //    generateNotificationDTO.UseCacheForTemplate = false;
        //    await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        //}


        /////<inheritdoc/>
        //public async Task PublisherUserSetPasswordSucess(PublisherNotificationDTO publisherNotificationDTO) {

        //    // Parameters needed in xslt
        //    #region XSlt Parameters Commented

        //    //< xsl:param name = "publisherCompanyName" />
        //    //< xsl:param name = "publisherUserName" />
        //    //< xsl:param name = "subDomain" />
        //    //< xsl:param name = "invitedUserEmailId" />
        //    //< xsl:param name = "publisherPortalURL" />
        //    //< xsl:param name = "copyrightText" />
        //    //< xsl:param name = "platformCompanyName" />

        //    #endregion XSlt Parameters Commented

        //    // Generated notification data
        //    #region EventData

        //    Dictionary<string, string> eventData = new Dictionary<string, string>();
        //    // Data fro getting the receipent list.
        //    eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
        //    eventData.Add("appid", publisherNotificationDTO.InvitedUserAppId.ToString());
        //    eventData.Add("appuserid", publisherNotificationDTO.InvitedUserId.ToString());

        //    eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
        //    eventData.Add("publisherUserName", publisherNotificationDTO.InvitedUserFullName);
        //    eventData.Add("subDomain", publisherNotificationDTO.SubDomain);
        //    eventData.Add("invitedUserEmailId", publisherNotificationDTO.InvitedUserEmail);
        //    eventData.Add("publisherPortalURL", publisherNotificationDTO.PublisherPortalURL);
        //    eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);
        //    eventData.Add("platformCompanyName", publisherNotificationDTO.PlatformCompanyName);

        //    #endregion EventData

        //    // Creates list of dictionary for event data.
        //    Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
        //    eventDataDict.Add("EventData", eventData);
        //    eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

        //    // Send notification
        //    GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
        //    generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
        //    generateNotificationDTO.EventId = (long)PublisherNotificationEvent.PublisherUserSetPasswordSucess;
        //    generateNotificationDTO.EventInfo = eventDataDict;
        //    generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
        //    generateNotificationDTO.UseCacheForTemplate = false;
        //    await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        //}

        /////<inheritdoc/>
        //public async Task BusinessExistingSubscriptionPlanChangedToPublisherUser(PublisherNotificationDTO publisherNotificationDTO) {

        //    // Parameters needed in xslt
        //    #region XSlt Parameters Commented

        //    //< xsl:param name = "publisherCompanyName" />
        //    //< xsl:param name = "applicationName" />  
        //    //< xsl:param name = "businessCompanyName" />
        //    //< xsl:param name = "oldsubscriptionPlanName" />
        //    //< xsl:param name = "newSubscriptionPlanName" />
        //    //< xsl:param name = "publisherUserNameChanged" />      
        //    //< xsl:param name = "copyrightText" />

        //    #endregion XSlt Parameters Commented

        //    // Generated notification data
        //    #region EventData

        //    Dictionary<string, string> eventData = new Dictionary<string, string>();
        //    // Data fro getting the receipent list.
        //    eventData.Add("tenantid", publisherNotificationDTO.InvitedUserTenantId.ToString());
        //    eventData.Add("appid", publisherNotificationDTO.InvitedUserAppId.ToString());

        //    eventData.Add("publisherCompanyName", publisherNotificationDTO.PublisherCompanyName);
        //    eventData.Add("businessCompanyName", publisherNotificationDTO.BusinessCompanyName);
        //    eventData.Add("applicationName", publisherNotificationDTO.AppLicationName);
        //    eventData.Add("oldsubscriptionPlanName", publisherNotificationDTO.OldSubscriptionPlanName);
        //    eventData.Add("newSubscriptionPlanName", publisherNotificationDTO.NewSubscriptionPlanName);
        //    eventData.Add("publisherUserNameChanged", publisherNotificationDTO.InvitorUserFullName);
        //    eventData.Add("copyrightText", publisherNotificationDTO.CopyRigthText);

        //    #endregion EventData

        //    // Creates list of dictionary for event data.
        //    Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
        //    eventDataDict.Add("EventData", eventData);
        //    eventDataDict.Add("UserSession", MapNotitificationUserSession(publisherNotificationDTO.UserSession));

        //    // Send notification
        //    GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
        //    generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
        //    generateNotificationDTO.EventId = (long)PublisherNotificationEvent.BusinessExistingSubscriptionPlanChangedToPublisherUser;
        //    generateNotificationDTO.EventInfo = eventDataDict;
        //    generateNotificationDTO.LoggedinUserId = publisherNotificationDTO.InvitedUserId;
        //    generateNotificationDTO.UseCacheForTemplate = false;
        //    generateNotificationDTO.NotificationToLoginUser = false;
        //    await _pubNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        //}


        #endregion

    }
}
