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
using ewApps.Core.EmailService;
using ewApps.Core.ExceptionService;
using ewApps.Core.NotificationService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This class implements all the notification triggered from Platfrom.
    /// </summary>
    public class PlatformNotificationHandler:IPlatformNotificationHandler {

        #region Local Member

        IPlatformNotificationService _platNotificationService;
        ITokenInfoDS _tokenInfoDS;
        IUnitOfWork _unitOfWork;
        IEmailService _emailService;
        ExceptionAppSettings _exceptionAppSetting;

        #endregion Local Member

        #region Contructor

        /// <summary>
        /// Initilizes all the required dependecies and member variables.
        /// </summary>
        /// <param name="emailService">An instance of email service to send email notification.</param>
        /// <param name="platNotificationService"><see cref="IPlatformNotificationService"/> to send the notification data.</param>
        /// <param name="unitOfWork">To Track the db changes and save changes in the database.</param>
        /// <param name="tokenInfoDS">Stores the toekn data to be sent to identity server while processing the deeplink</param>
        /// <param name="exceptionAppSetting">An instance of <see cref="ExceptionAppSettings"/> to get exception setting information.</param>
        public PlatformNotificationHandler(IEmailService emailService, IPlatformNotificationService platNotificationService, IUnitOfWork unitOfWork, ITokenInfoDS tokenInfoDS, IOptions<ExceptionAppSettings> exceptionAppSetting) {
            _platNotificationService = platNotificationService;
            _tokenInfoDS = tokenInfoDS;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _exceptionAppSetting = exceptionAppSetting.Value;
        }

        #endregion Contructor

        #region public methods 

        #region Account

        #region Platform

        ///<inheritdoc/>
        public async Task SendPlatformUserWithNewEmailInviteAsync(PlatformNotificationDTO platformNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //platformCompanyName
            //invitedUser
            //platformInvitingUser
            //invitedUserEmailId
            //platformPortalURL
            //copyrightText
            //activationLink

            #endregion XSlt Parameters Commented

            // Generate TokenInfo data for the notification.
            #region TokeInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = platformNotificationDTO.PasswordCode;
            tokenDataDTO.Email = platformNotificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = platformNotificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = platformNotificationDTO.InvitedUserTenantId;
            tokenDataDTO.AppKey = platformNotificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = (int)UserTypeEnum.Platform;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = tokenDataDTO.AppKey;
            tokenInfo.TenantUserId = platformNotificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = tokenDataDTO.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)PlatformTokenTypeEnum.PlatformUserWithNewEmailInvite;
            tokenInfo.UserType = (int)UserTypeEnum.Platform;

            _tokenInfoDS.Add(tokenInfo);
            _unitOfWork.SaveAll();

            #endregion TokeInfo

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", platformNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appid", platformNotificationDTO.InvitedUserAppId.ToString());
            eventData.Add("appuserid", platformNotificationDTO.InvitedUserId.ToString());

            eventData.Add("platformCompanyName", platformNotificationDTO.PlatformCompanyName);
            eventData.Add("invitedUser", platformNotificationDTO.InvitedUserFullName);
            eventData.Add("platformInvitingUser", platformNotificationDTO.InvitorUserFullName);
            eventData.Add("invitedUserEmailId", platformNotificationDTO.InvitedUserEmail);
            eventData.Add("platformPortalURL", platformNotificationDTO.PlatformPortalURL);
            eventData.Add("copyrightText", platformNotificationDTO.CopyRigthText);

            #endregion EventData

            // Generate the deeplink data for the notification.
            #region Deeplink Data

            Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            deeplinkInfo.Add("tenantid", platformNotificationDTO.InvitedUserTenantId.ToString());
            deeplinkInfo.Add("appuserid", platformNotificationDTO.InvitedUserId.ToString());
            deeplinkInfo.Add("identityserveruserid", platformNotificationDTO.InvitedUserIdentityUserId.ToString());
            deeplinkInfo.Add("usertype", Convert.ToString((int)UserTypeEnum.Publisher));
            deeplinkInfo.Add("code", platformNotificationDTO.PasswordCode);
            deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());
            deeplinkInfo.Add("tenantLanguage", platformNotificationDTO.TenantLanguage);

            #endregion Deeplink Data

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(platformNotificationDTO.UserSession));

            // Send notification.
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Platform;
            generateNotificationDTO.EventId = (long)PlatformNotificationEvents.PlatformUserWithNewEmailInvite;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = platformNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _platNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        ///<inheritdoc/>
        public async Task SendPlatformUserWithExistingEmailInviteAsync(PlatformNotificationDTO platformNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //platformCompanyName
            //invitedUser
            //platformInvitingUser
            //invitedUserEmailId
            //platformPortalURL
            //copyrightText
            //emailId
            //portalName
            //domainScreenURL

            #endregion XSlt Parameters Commented

            // Generate TokenInfo data for the notification.
            #region TokeInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = platformNotificationDTO.PasswordCode;
            tokenDataDTO.Email = platformNotificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = platformNotificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = platformNotificationDTO.InvitedUserTenantId;
            tokenDataDTO.AppKey = platformNotificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = (int)UserTypeEnum.Platform;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = tokenDataDTO.AppKey;
            tokenInfo.TenantUserId = platformNotificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = tokenDataDTO.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)PlatformTokenTypeEnum.PlatformUserExistingEmailInvite;
            tokenInfo.UserType = (int)UserTypeEnum.Platform;
            

            _tokenInfoDS.Add(tokenInfo);
            _unitOfWork.SaveAll();

            #endregion TokeInfo

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", platformNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appid", platformNotificationDTO.InvitedUserAppId.ToString());
            eventData.Add("appuserid", platformNotificationDTO.InvitedUserId.ToString());

            eventData.Add("platformCompanyName", platformNotificationDTO.PlatformCompanyName);
            eventData.Add("invitedUser", platformNotificationDTO.InvitedUserFullName);
            eventData.Add("platformInvitingUser", platformNotificationDTO.InvitorUserFullName);
            eventData.Add("invitedUserEmailId", platformNotificationDTO.InvitedUserEmail);
            eventData.Add("platformPortalURL", platformNotificationDTO.PlatformPortalURL);
            eventData.Add("copyrightText", platformNotificationDTO.CopyRigthText);
            eventData.Add("emailId", platformNotificationDTO.InvitedUserEmail);
            eventData.Add("portalName", platformNotificationDTO.PortalName);

            #endregion EventData

            // Generate the deeplink data for the notification.
            #region Deeplink Data

            Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            deeplinkInfo.Add("tenantid", platformNotificationDTO.InvitedUserTenantId.ToString());
            deeplinkInfo.Add("appuserid", platformNotificationDTO.InvitedUserId.ToString());
            deeplinkInfo.Add("identityserveruserid", platformNotificationDTO.InvitedUserIdentityUserId.ToString());
            deeplinkInfo.Add("usertype", Convert.ToString((int)UserTypeEnum.Publisher));
            deeplinkInfo.Add("code", platformNotificationDTO.PasswordCode);
            deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());
            deeplinkInfo.Add("tenantLanguage", platformNotificationDTO.TenantLanguage);

            #endregion Deeplink Data

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(platformNotificationDTO.UserSession));

            // Send notification.
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Platform;
            generateNotificationDTO.EventId = (long)PlatformNotificationEvents.PlatformUserExistingEmailInvite;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = platformNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _platNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        ///<inheritdoc/>
        public async Task SendPlatformUserForgotPasswordEmailAsync(PlatformNotificationDTO platformNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //platformCompanyName
            //platformUser
            //platformUserEmailId
            //copyrightText

            #endregion XSlt Parameters Commented

            // Generate TokenInfo data for the notification.
            #region TokeInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = platformNotificationDTO.PasswordCode;
            tokenDataDTO.Email = platformNotificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = platformNotificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = platformNotificationDTO.InvitedUserTenantId;
            tokenDataDTO.AppKey = platformNotificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = (int)UserTypeEnum.Platform;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = tokenDataDTO.AppKey;
            tokenInfo.TenantUserId = platformNotificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = tokenDataDTO.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)PlatformTokenTypeEnum.PlatformUserForgotPassword;
            tokenInfo.UserType = (int)UserTypeEnum.Platform;

            _tokenInfoDS.Add(tokenInfo);
            _unitOfWork.SaveAll();

            #endregion TokeInfo

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data for getting the receipent list.
            eventData.Add("tenantid", platformNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appuserid", platformNotificationDTO.InvitedUserId.ToString());

            eventData.Add("platformCompanyName", platformNotificationDTO.PlatformCompanyName);
            eventData.Add("platformUser", platformNotificationDTO.InvitedUserFullName);
            eventData.Add("platformUserEmailId", platformNotificationDTO.InvitedUserEmail);
            eventData.Add("copyrightText", platformNotificationDTO.CopyRigthText);

            #endregion EventData

            // Generate the deeplink data for the notification.
            #region Deeplink Data

            Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            deeplinkInfo.Add("tenantid", platformNotificationDTO.InvitedUserTenantId.ToString());
            deeplinkInfo.Add("appuserid", platformNotificationDTO.InvitedUserId.ToString());
            deeplinkInfo.Add("identityserveruserid", platformNotificationDTO.InvitedUserIdentityUserId.ToString());
            deeplinkInfo.Add("usertype", Convert.ToString((int)UserTypeEnum.Platform));
            deeplinkInfo.Add("code", platformNotificationDTO.PasswordCode);
            deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());
            deeplinkInfo.Add("tenantLanguage", platformNotificationDTO.TenantLanguage);

            #endregion Deeplink Data

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(platformNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Platform;
            generateNotificationDTO.EventId = (long)PlatformNotificationEvents.PlatformUserForgotPassword;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = platformNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _platNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        #endregion Platform

        #region Publisher

        ///<inheritdoc/>
        public async Task SendPublisherPrimaryUserWithNewEmailInvitateAsync(PlatformNotificationDTO platformNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //publisherCompanyName
            //platformCompanyName
            //invitedUser
            //subDomain
            //invitedUserEmailId 
            //publisherPortalURL
            //copyrightText
            //activationLink

            #endregion XSlt Parameters Commented

            // Generate TokenInfo data for the notification.
            #region TokeInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = platformNotificationDTO.PasswordCode;
            tokenDataDTO.Email = platformNotificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = platformNotificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = platformNotificationDTO.InvitedUserTenantId;
            tokenDataDTO.AppKey = platformNotificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = (int)UserTypeEnum.Publisher;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = tokenDataDTO.AppKey;
            tokenInfo.TenantUserId = platformNotificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = tokenDataDTO.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)PlatformTokenTypeEnum.PublisherPrimaryUserWithNewEmailInvite;
            tokenInfo.UserType = (int)UserTypeEnum.Publisher;

            _tokenInfoDS.Add(tokenInfo);
            _unitOfWork.SaveAll();

            #endregion TokeInfo

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data for getting the receipent list.
            eventData.Add("tenantid", platformNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appid", platformNotificationDTO.InvitedUserAppId.ToString());
            eventData.Add("appuserid", platformNotificationDTO.InvitedUserId.ToString());

            eventData.Add("publisherCompanyName", platformNotificationDTO.PublisherCompanyName);
            eventData.Add("platformCompanyName", platformNotificationDTO.PlatformCompanyName);
            eventData.Add("invitedUser", platformNotificationDTO.InvitedUserFullName);
            eventData.Add("subDomain", platformNotificationDTO.SubDomain);
            eventData.Add("invitedUserEmailId", platformNotificationDTO.InvitedUserEmail);
            eventData.Add("publisherPortalURL", platformNotificationDTO.PublisherPortalURL);
            eventData.Add("copyrightText", platformNotificationDTO.CopyRigthText);

            #endregion EventData

            // Generate the deeplink data for the notification.
            #region Deeplink Data

            Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            deeplinkInfo.Add("tenantid", platformNotificationDTO.InvitedUserTenantId.ToString());
            deeplinkInfo.Add("appuserid", platformNotificationDTO.InvitedUserId.ToString());
            deeplinkInfo.Add("identityserveruserid", platformNotificationDTO.InvitedUserIdentityUserId.ToString());
            deeplinkInfo.Add("usertype", Convert.ToString((int)UserTypeEnum.Publisher));
            deeplinkInfo.Add("code", platformNotificationDTO.PasswordCode);
            deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());
            deeplinkInfo.Add("tenantLanguage", platformNotificationDTO.TenantLanguage);
            deeplinkInfo.Add("subDomain", platformNotificationDTO.SubDomain);

            #endregion Deeplink Data

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(platformNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Platform;
            generateNotificationDTO.EventId = (long)PlatformNotificationEvents.PublisherPrimaryUserWithNewEmailInvite;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = platformNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _platNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        }

        ///<inheritdoc/>
        public async Task SendPublisherPrimaryUserWithExistingEmailInvitateAsync(PlatformNotificationDTO platformNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //publisherCompanyName
            //platformCompanyName
            //invitedUser
            //subDomain
            //invitedUserEmailId
            //publisherPortalURL
            //copyrightText
            //emailId
            //portalName
            //domainScreenURL

            #endregion XSlt Parameters Commented

            // Generate TokenInfo data for the notification.
            #region TokeInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = platformNotificationDTO.PasswordCode;
            tokenDataDTO.Email = platformNotificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = platformNotificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = platformNotificationDTO.InvitedUserTenantId;
            tokenDataDTO.AppKey = platformNotificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = (int)UserTypeEnum.Publisher;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = tokenDataDTO.AppKey;
            tokenInfo.TenantUserId = platformNotificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = tokenDataDTO.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)PlatformTokenTypeEnum.PublisherPrimaryUserWithExistingEmailInvite;
            tokenInfo.UserType = (int)UserTypeEnum.Publisher;

            _tokenInfoDS.Add(tokenInfo);
            _unitOfWork.SaveAll();

            #endregion TokeInfo

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data for getting the receipent list.
            eventData.Add("tenantid", platformNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appid", platformNotificationDTO.InvitedUserAppId.ToString());
            eventData.Add("appuserid", platformNotificationDTO.InvitedUserId.ToString());

            eventData.Add("publisherCompanyName", platformNotificationDTO.PublisherPortalURL);
            eventData.Add("platformCompanyName", platformNotificationDTO.PlatformCompanyName);
            eventData.Add("invitedUser", platformNotificationDTO.InvitedUserFullName);
            eventData.Add("subDomain", platformNotificationDTO.SubDomain);
            eventData.Add("invitedUserEmailId", platformNotificationDTO.InvitedUserEmail);
            eventData.Add("publisherPortalURL", platformNotificationDTO.PublisherPortalURL);
            eventData.Add("emailId", platformNotificationDTO.InvitedUserEmail);
            eventData.Add("portalName", platformNotificationDTO.PortalName);
            eventData.Add("copyrightText", platformNotificationDTO.CopyRigthText);

            #endregion EventData

            // Generate the deeplink data for the notification.
            #region Deeplink Data

            Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            deeplinkInfo.Add("tenantid", platformNotificationDTO.InvitedUserTenantId.ToString());
            deeplinkInfo.Add("appuserid", platformNotificationDTO.InvitedUserId.ToString());
            deeplinkInfo.Add("identityserveruserid", platformNotificationDTO.InvitedUserIdentityUserId.ToString());
            deeplinkInfo.Add("usertype", Convert.ToString((int)UserTypeEnum.Publisher));
            deeplinkInfo.Add("code", platformNotificationDTO.PasswordCode);
            deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());
            deeplinkInfo.Add("tenantLanguage", platformNotificationDTO.TenantLanguage);
            deeplinkInfo.Add("subDomain", platformNotificationDTO.SubDomain);

            #endregion Deeplink Data

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(platformNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Publisher;
            generateNotificationDTO.EventId = (long)PlatformNotificationEvents.PublisherPrimaryUserWithExistingEmailInvite;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = platformNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _platNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        #endregion Publisher

        #endregion Account

        #region Publisher Active\Inactive

        ///<inheritdoc/>
        public async Task PublisherActiveInActiveNotifyToPublisherAsync(PlatformNotificationDTO platformNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //    < xsl:param name = "platformCompanyName" />
            //< xsl:param name = "publisherCompanyName" />
            //  < xsl:param name = "status" />   
            //   < xsl:param name = "copyrightText" />

            #endregion XSlt Parameters Commented

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", platformNotificationDTO.InvitedUserTenantId.ToString());

            eventData.Add("platformCompanyName", platformNotificationDTO.PlatformCompanyName);
            eventData.Add("publisherCompanyName", platformNotificationDTO.PublisherCompanyName);
            eventData.Add("status", platformNotificationDTO.Status);
            eventData.Add("copyrightText", platformNotificationDTO.CopyRigthText);

            #endregion EventData

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(platformNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Platform;
            generateNotificationDTO.EventId = (long)PlatformNotificationEvents.PublisherMarkAsActiveInActiveNotifyPublisherUsers;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = platformNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _platNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        ///<inheritdoc/>
        public async Task PublisherActiveInActiveNotifyToPlatformAsync(PlatformNotificationDTO platformNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //     < xsl:param name = "platformCompanyName" />
            //< xsl:param name = "publisherCompanyName" />
            // < xsl:param name = "platformUserChanged" />
            //  < xsl:param name = "status" />
            //   < xsl:param name = "copyrightText" />


            #endregion XSlt Parameters Commented

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", platformNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("emailPrefrence", ((long)PlatformEmailPreferenceEnum.PublisherActiveInactive).ToString());

            eventData.Add("platformCompanyName", platformNotificationDTO.PlatformCompanyName);
            eventData.Add("publisherCompanyName", platformNotificationDTO.PublisherCompanyName);
            eventData.Add("platformUserChanged", platformNotificationDTO.InvitorUserFullName);
            eventData.Add("status", platformNotificationDTO.Status);
            eventData.Add("copyrightText", platformNotificationDTO.CopyRigthText);

            #endregion EventData

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(platformNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Platform;
            generateNotificationDTO.EventId = (long)PlatformNotificationEvents.PublisherMarkAsActiveInActiveNotifyPlatformUsers;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = platformNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = false;
            await _platNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        #endregion Publisher Active\Inactive

        #region Platform user update

        ///<inheritdoc/>
        public async Task PlatformUserSetPasswordSucessfullyAsync(PlatformNotificationDTO platformNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //< xsl:param name = "platformcompanyname" />
            //< xsl:param name = "platformuser" />
            //< xsl:param name = "inviteduseremailid" />
            //< xsl:param name = "platformportalurl" />
            //< xsl:param name = "copyrightext" />

            #endregion XSlt Parameters Commented

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", platformNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appid", platformNotificationDTO.InvitedUserAppId.ToString());
            eventData.Add("appuserid", platformNotificationDTO.InvitedUserId.ToString());

            eventData.Add("platformCompanyName", platformNotificationDTO.PlatformCompanyName);
            eventData.Add("platformUser", platformNotificationDTO.InvitedUserFullName);
            eventData.Add("invitedUserEmailId", platformNotificationDTO.InvitedUserEmail);
            eventData.Add("platformPortalURL", platformNotificationDTO.PlatformPortalURL);
            eventData.Add("copyrightText", platformNotificationDTO.CopyRigthText);

            #endregion EventData

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(platformNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Platform;
            generateNotificationDTO.EventId = (long)PlatformNotificationEvents.PlatformUserPasswordSetSucess;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = platformNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _platNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        ///<inheritdoc/>
        public async Task PlatformUserMarkActiveInActiveAsync(PlatformNotificationDTO platformNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //< xsl:param name = "platformCompanyName" />
            //< xsl:param name = "platformUser" />
            //< xsl:param name = "status" />
            //< xsl:param name = "platformUserChanged" />
            //< xsl:param name = "copyrightText" />


            #endregion XSlt Parameters Commented

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", platformNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appid", platformNotificationDTO.InvitedUserAppId.ToString());
            eventData.Add("appuserid", platformNotificationDTO.InvitedUserId.ToString());
            eventData.Add("emailPrefrence", ((long)PlatformEmailPreferenceEnum.MyActiveInactive).ToString());

            eventData.Add("platformCompanyName", platformNotificationDTO.PlatformCompanyName);
            eventData.Add("platformUser", platformNotificationDTO.InvitedUserFullName);
            eventData.Add("platformUserChanged", platformNotificationDTO.InvitorUserFullName);
            eventData.Add("status", platformNotificationDTO.Status);
            eventData.Add("copyrightText", platformNotificationDTO.CopyRigthText);

            #endregion EventData

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(platformNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Platform;
            generateNotificationDTO.EventId = (long)PlatformNotificationEvents.PlatformUserMarkAsActiveInActive;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = platformNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _platNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        ///<inheritdoc/>
        public async Task PlatformUserPermissionChangedAsync(PlatformNotificationDTO platformNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //< xsl:param name = "platformCompanyName" />
            //< xsl:param name = "platformUser" />
            //< xsl:param name = "platformUserChanged" />
            //< xsl:param name = "copyrightText" />

            #endregion XSlt Parameters Commented

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", platformNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appid", platformNotificationDTO.InvitedUserAppId.ToString());
            eventData.Add("appuserid", platformNotificationDTO.InvitedUserId.ToString());
            eventData.Add("emailPrefrence", ((long)PlatformEmailPreferenceEnum.MyPermissionChanged).ToString());

            eventData.Add("platformCompanyName", platformNotificationDTO.PlatformCompanyName);
            eventData.Add("platformUser", platformNotificationDTO.InvitedUserFullName);
            eventData.Add("platformUserChanged", platformNotificationDTO.InvitorUserFullName);
            eventData.Add("copyrightText", platformNotificationDTO.CopyRigthText);

            #endregion EventData

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(platformNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Platform;
            generateNotificationDTO.EventId = (long)PlatformNotificationEvents.PlatformUserPermissionChanged;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = platformNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _platNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        ///<inheritdoc/>
        public async Task PlatformUserOnBoardAsync(PlatformNotificationDTO platformNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //< xsl:param name = "platformCompanyName" />
            //< xsl:param name = "newUser" />
            //< xsl:param name = "copyrightText" />

            #endregion XSlt Parameters Commented

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", platformNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("emailPrefrence", ((long)PlatformEmailPreferenceEnum.NewPlatformUserOnboard).ToString());

            eventData.Add("platformCompanyName", platformNotificationDTO.PlatformCompanyName);
            eventData.Add("newUser", platformNotificationDTO.InvitedUserFullName);
            eventData.Add("copyrightText", platformNotificationDTO.CopyRigthText);

            #endregion EventData

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(platformNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Platform;
            generateNotificationDTO.EventId = (long)PlatformNotificationEvents.PlatformUserOnBoard;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = platformNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = false;
            await _platNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        ///<inheritdoc/>
        public async Task PlatformUserDeletedAsync(PlatformNotificationDTO platformNotificationDTO) {

            // Parameters needed in xslt
            #region XSlt Parameters Commented

            //< xsl:param name = "platformCompanyName" />
            //< xsl:param name = "platformUser" />
            //< xsl:param name = "platformUserChanged" />
            //< xsl:param name = "copyrightText" />

            #endregion XSlt Parameters Commented

            // Generated notification data
            #region EventData

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            // Data fro getting the receipent list.
            eventData.Add("tenantid", platformNotificationDTO.InvitedUserTenantId.ToString());
            eventData.Add("appid", platformNotificationDTO.InvitedUserAppId.ToString());
            eventData.Add("appuserid", platformNotificationDTO.InvitedUserId.ToString());

            eventData.Add("platformCompanyName", platformNotificationDTO.PlatformCompanyName);
            eventData.Add("platformUser", platformNotificationDTO.InvitedUserFullName);
            eventData.Add("platformUserChanged", platformNotificationDTO.InvitorUserFullName);
            eventData.Add("copyrightText", platformNotificationDTO.CopyRigthText);

            #endregion EventData

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(platformNotificationDTO.UserSession));

            // Send notification
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Platform;
            generateNotificationDTO.EventId = (long)PlatformNotificationEvents.PlatformUserDeleted;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = platformNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _platNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

        #endregion Platform user update

        #endregion

        ///<inheritdoc/>
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

    }
}
