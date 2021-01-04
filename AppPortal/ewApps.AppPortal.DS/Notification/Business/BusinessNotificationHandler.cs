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

    /// <summary>DataService implementing Support notification service</summary>
    public class BusinessNotificationHandler:IBusinessNotificationHandler {

        #region Local member

        IUnitOfWork _unitOfWork;
        IBusinessNotificationService _businessNotificationDataService;
        ITokenInfoDS _tokenInfoDataService;
        IEmailService _emailService;
        ExceptionAppSettings _exceptionAppSetting;

        #endregion Local member

        #region Constructor

        /// <summary>Initilizing the objects</summary>
        public BusinessNotificationHandler(IEmailService emailService,
        IOptions<ExceptionAppSettings> exceptionAppSetting, IBusinessNotificationService supportNotificationDataService,
        ITokenInfoDS tokenInfoDataService, IUnitOfWork unitOfWork) {
            _businessNotificationDataService = supportNotificationDataService;
            _tokenInfoDataService = tokenInfoDataService;
            _unitOfWork = unitOfWork;
            _exceptionAppSetting = exceptionAppSetting.Value;
            _emailService = emailService;
        }

        #endregion Constructor

        #region Public Methods

        #region Old

        #region Invitation

        ///<inheritdoc/>
        public async Task InviteBusinessUser(BusinessAccountNotificationDTO businessNotificationDTO) {


            #region TokenInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = businessNotificationDTO.PasswordCode;
            tokenDataDTO.Email = businessNotificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = businessNotificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = businessNotificationDTO.UserSession.TenantId;
            tokenDataDTO.AppKey = businessNotificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = (int)UserTypeEnum.Business;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = businessNotificationDTO.InvitedUserAppKey;
            tokenInfo.TenantUserId = businessNotificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = businessNotificationDTO.UserSession.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)BusinessTokenTypeEnum.BusinessUserWithNewEmailIdInvite;

            _tokenInfoDataService.Add(tokenInfo);
            _unitOfWork.Save();

            #endregion TokenInfo

            // Create notification information.
            Dictionary<string, string> eventData = new Dictionary<string, string>();
            eventData.Add("publisherName", businessNotificationDTO.PublisherName);
            eventData.Add("appName", " ");
            eventData.Add("portalType", " : Business Portal");
            eventData.Add("hostName", businessNotificationDTO.PublisherName);
            eventData.Add("businessUserName", businessNotificationDTO.InvitedUserFullName);
            eventData.Add("inviteeCompany", businessNotificationDTO.InvitedUserEmail + "'s Account ");
            eventData.Add("userType", ((int)UserTypeEnum.Business).ToString());
            eventData.Add("appId", businessNotificationDTO.UserSession.AppId.ToString());
            eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());
            eventData.Add("appuserid", businessNotificationDTO.InvitedUserId.ToString());
            eventData.Add("appkey", businessNotificationDTO.InvitedUserAppKey);

            // Create deeplink info.
            Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            deeplinkInfo.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());
            deeplinkInfo.Add("appuserid", businessNotificationDTO.InvitedUserId.ToString());
            deeplinkInfo.Add("identityserveruserid", businessNotificationDTO.InvitedUserIdentityUserId.ToString());
            deeplinkInfo.Add("loginEmail", businessNotificationDTO.InvitedUserEmail);
            deeplinkInfo.Add("usertype", Convert.ToString((int)UserTypeEnum.Business));
            deeplinkInfo.Add("code", businessNotificationDTO.PasswordCode);
            deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            deeplinkInfo.Add("subDomain", businessNotificationDTO.SubDomain);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Payment, (long)BusinessNotificationEventEnum.BusinessUserInvitation, false, eventDataDict, businessNotificationDTO.UserSession.TenantUserId, true);
        }

        ///<inheritdoc/>
        public async Task InviteBusinessPartnerUser(BusinessAccountNotificationDTO businessNotificationDTO) {

            #region TokenInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = businessNotificationDTO.PasswordCode;
            tokenDataDTO.Email = businessNotificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = businessNotificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = businessNotificationDTO.UserSession.TenantId;
            tokenDataDTO.AppKey = businessNotificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = (int)UserTypeEnum.Customer;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = businessNotificationDTO.InvitedUserAppKey;
            tokenInfo.TenantUserId = businessNotificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = businessNotificationDTO.UserSession.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)BusinessTokenTypeEnum.BusinessUserWithNewEmailIdInvite;

            _tokenInfoDataService.Add(tokenInfo);
            _unitOfWork.Save();

            #endregion TokenInfo

            //// Create notification information.
            //Dictionary<string, string> eventData = new Dictionary<string, string>();
            //eventData.Add("publisherName", businessNotificationDTO.PublisherName);
            //eventData.Add("appName", businessNotificationDTO.ApplicationName);
            //eventData.Add("portalType", UserTypeEnum.Business.ToString());
            //eventData.Add("hostName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessUserName", businessNotificationDTO.InvitedUserFullName);
            //eventData.Add("inviteeCompany", businessNotificationDTO.PublisherName);
            //eventData.Add("userType", ((int)UserTypeEnum.Customer).ToString());
            //eventData.Add("appId", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());
            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appkey", businessNotificationDTO.App.AppKey);

            //// Create deeplink info.
            //Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            //deeplinkInfo.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());
            //deeplinkInfo.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //deeplinkInfo.Add("identityserveruserid", businessNotificationDTO.TenantUser.IdentityUserId.ToString());
            //deeplinkInfo.Add("loginEmail", businessNotificationDTO.TenantUser.Email);
            //deeplinkInfo.Add("usertype", Convert.ToString((int)UserTypeEnum.Business));
            //deeplinkInfo.Add("code", businessNotificationDTO.TenantUser.Code);
            //deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            //deeplinkInfo.Add("subDomain", businessNotificationDTO.SubDomain);

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessPartnerUserInvitation, false, eventDataDict, businessNotificationDTO.UserSession.TenantUserId, true);
        }

        ///<inheritdoc/>
        public async Task ForgotPasswordBusiness(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region TokenInfo

            //TokenDataDTO tokenDataDTO = new TokenDataDTO();
            //tokenDataDTO.Code = businessNotificationDTO.TenantUser.Code;
            //tokenDataDTO.Email = businessNotificationDTO.TenantUser.Email;
            //tokenDataDTO.IdentityUserId = businessNotificationDTO.TenantUser.IdentityUserId;
            //tokenDataDTO.TenantId = businessNotificationDTO.UserSession.TenantId;
            //tokenDataDTO.AppKey = businessNotificationDTO.App.AppKey;

            //TokenInfo tokenInfo = new TokenInfo();
            //tokenInfo.ID = Guid.NewGuid();
            //tokenInfo.AppKey = businessNotificationDTO.App.AppKey;
            //tokenInfo.TenantUserId = businessNotificationDTO.TenantUser.ID;
            //tokenInfo.CreatedDate = DateTime.UtcNow;
            //tokenInfo.TenantId = businessNotificationDTO.UserSession.TenantId;
            //tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            //tokenInfo.TokenType = (int)BusinessTokenTypeEnum.BusinessUserWithNewEmailIdInvite;

            //_tokenInfoDataService.Add(tokenInfo);
            //_unitOfWork.Save();

            //#endregion TokenInfo

            //// Create notification information.
            //Dictionary<string, string> eventData = new Dictionary<string, string>();
            //eventData.Add("publisherName", businessNotificationDTO.PublisherName);
            //eventData.Add("appName", businessNotificationDTO.ApplicationName);
            //eventData.Add("portalType", UserTypeEnum.Business.ToString());
            //eventData.Add("hostName", businessNotificationDTO.PublisherName);
            //eventData.Add("portalUserName", businessNotificationDTO.TenantUser.FullName);
            //eventData.Add("inviteeCompany", businessNotificationDTO.PublisherName);
            //eventData.Add("userType", ((int)UserTypeEnum.BusinessPartner).ToString());
            //eventData.Add("appId", businessNotificationDTO.UserSession.AppId.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());
            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appkey", businessNotificationDTO.App.AppKey);


            //// Create deeplink info.
            //Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            //deeplinkInfo.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());
            //deeplinkInfo.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //deeplinkInfo.Add("identityserveruserid", businessNotificationDTO.TenantUser.IdentityUserId.ToString());
            //deeplinkInfo.Add("loginEmail", businessNotificationDTO.TenantUser.Email);
            //deeplinkInfo.Add("usertype", Convert.ToString((int)UserTypeEnum.Business));
            //deeplinkInfo.Add("code", businessNotificationDTO.TenantUser.Code);
            //deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            //deeplinkInfo.Add("subDomain", businessNotificationDTO.SubDomain);

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.ForgotPasswordBusiness, false, eventDataDict, businessNotificationDTO.UserSession.TenantUserId, true);
        }

        ///<inheritdoc/>
        public async Task ForgotPasswordBusinessPartner(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region TokenInfo

            //TokenDataDTO tokenDataDTO = new TokenDataDTO();
            //tokenDataDTO.Code = businessNotificationDTO.TenantUser.Code;
            //tokenDataDTO.Email = businessNotificationDTO.TenantUser.Email;
            //tokenDataDTO.IdentityUserId = businessNotificationDTO.TenantUser.IdentityUserId;
            //tokenDataDTO.TenantId = businessNotificationDTO.UserSession.TenantId;
            //tokenDataDTO.AppKey = businessNotificationDTO.App.AppKey;

            //TokenInfo tokenInfo = new TokenInfo();
            //tokenInfo.ID = Guid.NewGuid();
            //tokenInfo.AppKey = businessNotificationDTO.App.AppKey;
            //tokenInfo.TenantUserId = businessNotificationDTO.TenantUser.ID;
            //tokenInfo.CreatedDate = DateTime.UtcNow;
            //tokenInfo.TenantId = businessNotificationDTO.UserSession.TenantId;
            //tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            //tokenInfo.TokenType = (int)BusinessTokenTypeEnum.BusinessUserWithNewEmailIdInvite;

            //_tokenInfoDataService.Add(tokenInfo);
            //_unitOfWork.Save();

            //#endregion TokenInfo

            //// Create notification information.
            //Dictionary<string, string> eventData = new Dictionary<string, string>();
            //eventData.Add("publisherName", businessNotificationDTO.PublisherName);
            //eventData.Add("appName", businessNotificationDTO.ApplicationName);
            //eventData.Add("portalType", UserTypeEnum.Business.ToString());
            //eventData.Add("hostName", businessNotificationDTO.PublisherName);
            //eventData.Add("portalUserName", businessNotificationDTO.TenantUser.FullName);
            //eventData.Add("inviteeCompany", businessNotificationDTO.PublisherName);
            //eventData.Add("userType", ((int)UserTypeEnum.BusinessPartner).ToString());
            //eventData.Add("appId", businessNotificationDTO.UserSession.AppId.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());
            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appkey", businessNotificationDTO.App.AppKey);


            //// Create deeplink info.
            //Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            //deeplinkInfo.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());
            //deeplinkInfo.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //deeplinkInfo.Add("identityserveruserid", businessNotificationDTO.TenantUser.IdentityUserId.ToString());
            //deeplinkInfo.Add("loginEmail", businessNotificationDTO.TenantUser.Email);
            //deeplinkInfo.Add("usertype", Convert.ToString((int)UserTypeEnum.Business));
            //deeplinkInfo.Add("code", businessNotificationDTO.TenantUser.Code);
            //deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            //deeplinkInfo.Add("subDomain", businessNotificationDTO.SubDomain);

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.ForgotPasswordBusinessPartner, false, eventDataDict, businessNotificationDTO.UserSession.TenantUserId, true);
        }

        #endregion Invitation

        #endregion Old

        #region New

        #region Business User management

        //1
        ///<inheritdoc/>
        public async Task GenerateBusinessPrimaryUserSetPasswordSucessNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments

            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessUserName"/>
            ////<xsl:param name="subDomain"/>
            ////<xsl:param name="invitedUserEmailID"/>
            ////<xsl:param name="portalURL"/>
            ////<xsl:param name="copyrightText"/>

            //#endregion xslt arguments

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessUserName", businessNotificationDTO.TenantUser.FullName);
            //eventData.Add("subDomain", businessNotificationDTO.SubDomain);
            //eventData.Add("invitedUserEmailID", businessNotificationDTO.TenantUser.Email);
            //eventData.Add("portalURL", businessNotificationDTO.PortalURL);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.TenantId.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessPrimaryUserSetPasswordSucess, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
        }

        //2
        ///<inheritdoc/>
        public async Task GenerateBusinessOnBoardNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments

            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessCompanyName"/>
            ////<xsl:param name="copyrightText"/>

            //#endregion xslt arguments

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessCompanyName", businessNotificationDTO.BusinessCompanyName);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.TenantId.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.NewBusinessOnBoard, false, eventDataDict, businessNotificationDTO.TenantUser.ID, false);
        }

        #region Business User Account

        //3
        // InUse
        ///<inheritdoc/>
        public async Task GenerateBusinessNewUserInviteNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            #region xslt arguments

            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="invitedUserName"/>
            //<xsl:param name="businessInvitingUserName"/>
            //<xsl:param name="subDomain"/>
            //<xsl:param name="portalURL"/>
            //<xsl:param name="copyrightText"/>

            #endregion xslt arguments

            #region TokeInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = businessNotificationDTO.PasswordCode;
            tokenDataDTO.Email = businessNotificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = businessNotificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = businessNotificationDTO.TenantId;
            tokenDataDTO.AppKey = businessNotificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = (int)UserTypeEnum.Business;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = businessNotificationDTO.InvitedUserAppKey;
            tokenInfo.TenantUserId = businessNotificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = businessNotificationDTO.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)BusinessTokenTypeEnum.BusinessUserWithNewEmailIdInvite;
            tokenInfo.UserType = (int)UserTypeEnum.Business;

            _tokenInfoDataService.Add(tokenInfo);
            _unitOfWork.SaveAll();

            #endregion TokeInfo

            // Create notification information
            Dictionary<string, string> eventData = new Dictionary<string, string>();

            eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            eventData.Add("businessCompanyName", businessNotificationDTO.BusinessCompanyName);
            eventData.Add("invitedUserName", businessNotificationDTO.InvitedUserFullName);
            eventData.Add("businessInvitingUserName", businessNotificationDTO.InvitorUserName);
            eventData.Add("subDomain", businessNotificationDTO.SubDomain);
            eventData.Add("portalURL", businessNotificationDTO.PortalURL);
            eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            eventData.Add("appuserid", businessNotificationDTO.InvitedUserId.ToString());
            eventData.Add("appid", businessNotificationDTO.InvitedUserAppId.ToString());
            eventData.Add("tenantid", businessNotificationDTO.TenantId.ToString());

            Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            deeplinkInfo.Add("tenantid", businessNotificationDTO.TenantId.ToString());
            deeplinkInfo.Add("appuserid", businessNotificationDTO.InvitedUserId.ToString());
            deeplinkInfo.Add("identityserveruserid", businessNotificationDTO.InvitedUserIdentityUserId.ToString());
            deeplinkInfo.Add("loginEmail", businessNotificationDTO.InvitedUserEmail);
            deeplinkInfo.Add("usertype", UserTypeEnum.Business.ToString());
            deeplinkInfo.Add("code", businessNotificationDTO.PasswordCode);
            deeplinkInfo.Add("partnerid", " 123 ");
            deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            deeplinkInfo.Add("subDomain", businessNotificationDTO.SubDomain);
            deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(businessNotificationDTO.UserSession));

            // Send notification.
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)BusinessNotificationEventEnum.BusinessUserWithNewEmailIdInvite;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = businessNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _businessNotificationDataService.GenerateNotificationAsync(generateNotificationDTO);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessUserWithNewEmailIdInvite, false, eventDataDict, businessNotificationDTO.UserSession.TenantUserId);
        }

        //4
        // InUse
        ///<inheritdoc/>
        public async Task GenerateBusinessUserWithExistingEmailIdInviteNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            #region xslt arguments
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="invitedUserName"/>
            //<xsl:param name="businessInvitingUserName"/>
            //<xsl:param name="emailID"/>
            //<xsl:param name="portalName"/>
            //<xsl:param name="subDomain"/>
            //<xsl:param name="invitedUserEmailId"/>
            //<xsl:param name="portalURL"/>
            //<xsl:param name="copyrightText"/>
            #endregion xslt arguments

            #region TokeInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = businessNotificationDTO.PasswordCode;
            tokenDataDTO.Email = businessNotificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = businessNotificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = businessNotificationDTO.TenantId;
            tokenDataDTO.AppKey = businessNotificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = (int)UserTypeEnum.Business;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = businessNotificationDTO.InvitedUserAppKey;
            tokenInfo.TenantUserId = businessNotificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = businessNotificationDTO.UserSession.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)BusinessTokenTypeEnum.BusinessUserWithExistingEmailIdInvite;
            tokenInfo.UserType = (int)UserTypeEnum.Business;

            _tokenInfoDataService.Add(tokenInfo);
            _unitOfWork.Save();

            #endregion TokeInfo

            // Create notification information
            Dictionary<string, string> eventData = new Dictionary<string, string>();

            eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            eventData.Add("businessCompanyName", businessNotificationDTO.BusinessCompanyName);
            eventData.Add("invitedUserName", businessNotificationDTO.InvitedUserFullName);
            eventData.Add("businessInvitingUserName", businessNotificationDTO.InvitorUserName);
            eventData.Add("emailID", businessNotificationDTO.InvitedUserEmail);
            eventData.Add("portalName", businessNotificationDTO.PortalName);
            eventData.Add("subDomain", businessNotificationDTO.SubDomain);
            eventData.Add("invitedUserEmailId", businessNotificationDTO.InvitedUserEmail);
            eventData.Add("portalURL", businessNotificationDTO.PortalURL);
            eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            eventData.Add("appuserid", businessNotificationDTO.InvitedUserId.ToString());
            eventData.Add("appid", businessNotificationDTO.InvitedUserAppId.ToString());
            eventData.Add("tenantid", businessNotificationDTO.TenantId.ToString());

            Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            deeplinkInfo.Add("tenantid", businessNotificationDTO.TenantId.ToString());
            deeplinkInfo.Add("appuserid", businessNotificationDTO.InvitedUserId.ToString());
            deeplinkInfo.Add("identityserveruserid", businessNotificationDTO.InvitedUserIdentityUserId.ToString());
            deeplinkInfo.Add("loginEmail", businessNotificationDTO.InvitedUserEmail);
            deeplinkInfo.Add("usertype", UserTypeEnum.Business.ToString());
            deeplinkInfo.Add("code", businessNotificationDTO.PasswordCode);
            deeplinkInfo.Add("partnerid", "123 ");
            deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            deeplinkInfo.Add("subDomain", businessNotificationDTO.SubDomain);
            deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());
            //deeplinkInfo.Add("tenantLanguage", tenantLanguage);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(businessNotificationDTO.UserSession));

            // Send notification.
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)BusinessNotificationEventEnum.BusinessUserWithExistingEmailIdInvite;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = businessNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _businessNotificationDataService.GenerateNotificationAsync(generateNotificationDTO);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessUserWithExistingEmailIdInvite, false, eventDataDict, businessNotificationDTO.UserSession.TenantUserId);
        }

        #endregion Business User Account

        //5
        ///<inheritdoc/>
        public async Task GenerateBusinessUserSetPasswordSucessNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments

            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessUserName"/>
            ////<xsl:param name="subDomain"/>
            ////<xsl:param name="invitedUserEmailID"/>
            ////<xsl:param name="portalURL"/>
            ////<xsl:param name="copyrightText"/>

            //#endregion xslt arguments

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessUserName", businessNotificationDTO.TenantUser.FullName);
            //eventData.Add("subDomain", businessNotificationDTO.SubDomain);
            //eventData.Add("invitedUserEmailID", businessNotificationDTO.TenantUser.Email);
            //eventData.Add("portalURL", businessNotificationDTO.PortalURL);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.TenantId.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessUserSetPasswordSucess, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
        }

        //6
        ///<inheritdoc/>
        public async Task GenerateBusinessUserOnBoardNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments

            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessCompanyName"/>
            ////<xsl:param name="userName"/>
            ////<xsl:param name="copyrightText"/>

            //#endregion xslt arguments

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessCompanyName", businessNotificationDTO.BusinessCompanyName);
            //eventData.Add("userName", businessNotificationDTO.TenantUser.FullName);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.TenantId.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.NewBusinessUserOnBoard, false, eventDataDict, businessNotificationDTO.TenantUser.ID, false);
        }

        //7
        ///<inheritdoc/>
        public async Task GenerateApplicationAssignedToBusinessUserNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments
            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessUserName"/>
            ////<xsl:param name="applicationName"/>
            ////<xsl:param name="subDomain"/>
            ////<xsl:param name="portalURL"/>
            ////<xsl:param name="copyrightText"/>
            //#endregion xslt arguments

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessUserName", businessNotificationDTO.TenantUser.FullName);
            //eventData.Add("applicationName", businessNotificationDTO.ApplicationName);
            //eventData.Add("subDomain", businessNotificationDTO.SubDomain);
            //eventData.Add("portalURL", businessNotificationDTO.PortalURL);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.ApplicationAssignedToBusinessUser, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
        }

        //8
        ///<inheritdoc/>
        public async Task GenerateApplicationDeAssignedToBusinessUserNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments

            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessUserName"/>
            ////<xsl:param name="applicationName"/>
            ////<xsl:param name="copyrightText"/>

            //#endregion xslt arguments

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessUserName", businessNotificationDTO.TenantUser.FullName);
            //eventData.Add("applicationName", businessNotificationDTO.ApplicationName);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.ApplicationDeAssignedToBusinessUser, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
        }

        //9
        ///<inheritdoc/>
        public async Task GenerateBusinessUserAccountStatusChangedNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments
            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessUserName"/>
            ////<xsl:param name="businessUserNameChange"/>
            ////<xsl:param name="status"/>
            ////<xsl:param name="copyrightText"/>
            //#endregion xslt arguments

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessUserName", businessNotificationDTO.TenantUser.FullName);
            //eventData.Add("businessUserNameChange", businessNotificationDTO.InvitorUserName);
            //eventData.Add("status", businessNotificationDTO.Status);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessUserMarkActiveInActive, false, eventDataDict, businessNotificationDTO.TenantUser.ID);

        }

        //10
        ///<inheritdoc/>
        public async Task GenerateBusinessPermissionChangedNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments
            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessUserName"/>
            ////<xsl:param name="businessUserNameChange"/>
            ////<xsl:param name="copyrightText"/>
            //#endregion xslt arguments

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessUserName", businessNotificationDTO.TenantUser.FullName);
            //eventData.Add("businessUserNameChange", businessNotificationDTO.InvitorUserName);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessUserPermissionChanged, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
        }

        //11
        ///<inheritdoc/>
        public async Task GenerateBusinessUserDeletedNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments
            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessUserName"/>
            ////<xsl:param name="businessUserNameChange"/>
            ////<xsl:param name="copyrightText"/>
            //#endregion xslt arguments

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessUserName", businessNotificationDTO.TenantUser.FullName);
            //eventData.Add("businessUserNameChange", businessNotificationDTO.InvitorUserName);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessUserDeleted, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
        }


        #endregion Business User management

        #region Business Partner User

        //12
        // InUse
        ///<inheritdoc/>
        public async Task GenerateBusinessPartnerPrimaryUserNewEmailIdInvitedNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            #region xslt arguments
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="businessPartnerType"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="invitedUserName"/>
            //<xsl:param name="applicationName"/>
            //<xsl:param name="subDomain"/>
            //<xsl:param name="invitedUserEmailId"/>
            //<xsl:param name="portalURL"/>
            //<xsl:param name="copyrightText"/>
            #endregion xslt arguments

            #region TokeInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = businessNotificationDTO.PasswordCode;
            tokenDataDTO.Email = businessNotificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = businessNotificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = businessNotificationDTO.TenantId;
            tokenDataDTO.AppKey = businessNotificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = (int)UserTypeEnum.Customer;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = businessNotificationDTO.InvitedUserAppKey;
            tokenInfo.TenantUserId = businessNotificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = businessNotificationDTO.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)BusinessTokenTypeEnum.BusinessPartnerPrimaryNewUserInvite;
            tokenInfo.UserType = (int)UserTypeEnum.Customer;

            _tokenInfoDataService.Add(tokenInfo);
            _unitOfWork.Save();

            #endregion TokeInfo

            // Create notification information
            Dictionary<string, string> eventData = new Dictionary<string, string>();

            eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            eventData.Add("businessCompanyName", businessNotificationDTO.BusinessCompanyName);
            eventData.Add("businessPartnerType", businessNotificationDTO.PartnerType);
            eventData.Add("businessPartnerCompanyName", businessNotificationDTO.BusinessPartnerCompanyName);
            eventData.Add("invitedUserName", businessNotificationDTO.InvitedUserFullName);
            eventData.Add("applicationName", businessNotificationDTO.ApplicationName);
            eventData.Add("subDomain", businessNotificationDTO.SubDomain);
            eventData.Add("invitedUserEmailId", businessNotificationDTO.InvitedUserEmail);
            eventData.Add("portalURL", businessNotificationDTO.PortalURL);
            eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            eventData.Add("appuserid", businessNotificationDTO.InvitedUserId.ToString());
            eventData.Add("appid", businessNotificationDTO.InvitedUserAppId.ToString());
            eventData.Add("tenantid", businessNotificationDTO.TenantId.ToString());

            eventData.Add("appkey", businessNotificationDTO.InvitedUserAppKey);

            Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            deeplinkInfo.Add("tenantid", businessNotificationDTO.TenantId.ToString());
            deeplinkInfo.Add("appuserid", businessNotificationDTO.InvitedUserId.ToString());
            deeplinkInfo.Add("identityserveruserid", businessNotificationDTO.InvitedUserIdentityUserId.ToString());
            deeplinkInfo.Add("loginEmail", businessNotificationDTO.InvitedUserEmail);
            deeplinkInfo.Add("usertype", UserTypeEnum.Business.ToString());
            deeplinkInfo.Add("code", businessNotificationDTO.PasswordCode);
            deeplinkInfo.Add("partnerid", " 123 ");
            deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            deeplinkInfo.Add("subDomain", businessNotificationDTO.SubDomain);
            deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(businessNotificationDTO.UserSession));

            // Send notification.
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)BusinessNotificationEventEnum.BusinessPartnerPrimaryNewUserInvite;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = businessNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _businessNotificationDataService.GenerateNotificationAsync(generateNotificationDTO);

            // await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessPartnerPrimaryNewUserInvite, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
        }

        //13
        ///<inheritdoc/>
        public async Task GenerateBusinessPartnerPrimaryUserExistingEmailIdInvitedNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments
            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessCompanyName"/>
            ////<xsl:param name="businessPartnerType"/>
            ////<xsl:param name="businessPartnerCompanyName"/>
            ////<xsl:param name="invitedUserName"/>
            ////<xsl:param name="emailID"/>
            ////<xsl:param name="portalName"/>
            ////<xsl:param name="applicationName"/>
            ////<xsl:param name="domainURL"/>
            ////<xsl:param name="subDomain"/>
            ////<xsl:param name="invitedUserEmailId"/>
            ////<xsl:param name="portalURL"/>
            ////<xsl:param name="copyrightText"/>
            //#endregion xslt arguments

            //#region TokeInfo

            //TokenDataDTO tokenDataDTO = new TokenDataDTO();
            //tokenDataDTO.Code = businessNotificationDTO.TenantUser.Code;
            //tokenDataDTO.Email = businessNotificationDTO.TenantUser.Email;
            //tokenDataDTO.IdentityUserId = businessNotificationDTO.TenantUser.IdentityUserId;
            //tokenDataDTO.TenantId = businessNotificationDTO.UserSession.TenantId;
            //tokenDataDTO.AppKey = businessNotificationDTO.App.AppKey;

            //TokenInfo tokenInfo = new TokenInfo();
            //tokenInfo.ID = Guid.NewGuid();
            //tokenInfo.AppKey = businessNotificationDTO.App.AppKey;
            //tokenInfo.TenantUserId = businessNotificationDTO.TenantUser.ID;
            //tokenInfo.CreatedDate = DateTime.UtcNow;
            //tokenInfo.TenantId = businessNotificationDTO.UserSession.TenantId;
            //tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            //tokenInfo.TokenType = (int)BusinessTokenTypeEnum.BusinessPartnerPrimaryExistingUserInvite;

            //_tokenInfoDataService.Add(tokenInfo);
            //_unitOfWork.Save();

            //#endregion TokeInfo

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessCompanyName", businessNotificationDTO.BusinessCompanyName);
            //eventData.Add("businessPartnerType", businessNotificationDTO.PartnerType);
            //eventData.Add("businessPartnerCompanyName", businessNotificationDTO.BusinessPartnerCompanyName);
            //eventData.Add("invitedUserName", businessNotificationDTO.TenantUser.FullName);
            //eventData.Add("emailID", businessNotificationDTO.TenantUser.Email);
            //eventData.Add("portalName", businessNotificationDTO.PortalName);
            //eventData.Add("applicationName", businessNotificationDTO.ApplicationName);
            //eventData.Add("subDomain", businessNotificationDTO.SubDomain);
            //eventData.Add("invitedUserEmailId", businessNotificationDTO.TenantUser.Email);
            //eventData.Add("portalURL", businessNotificationDTO.PortalURL);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());

            //eventData.Add("appkey", businessNotificationDTO.App.AppKey);

            //Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            //deeplinkInfo.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());
            //deeplinkInfo.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //deeplinkInfo.Add("identityserveruserid", businessNotificationDTO.TenantUser.IdentityUserId.ToString());
            //deeplinkInfo.Add("loginEmail", businessNotificationDTO.TenantUser.Email);
            //deeplinkInfo.Add("usertype", UserTypeEnum.Business.ToString());
            //deeplinkInfo.Add("code", businessNotificationDTO.TenantUser.Code);
            //deeplinkInfo.Add("partnerid", " 123 ");
            //deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            //deeplinkInfo.Add("subDomain", businessNotificationDTO.SubDomain);
            //deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessPartnerPrimaryExistingUserInvite, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
        }

        //14
        ///<inheritdoc/>
        public async Task GenerateBusinessPartnerOtherUserNewEmailIdInvitedNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments
            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessCompanyName"/>
            ////<xsl:param name="businessPartnerType"/>
            ////<xsl:param name="businessPartnerCompanyName"/>
            ////<xsl:param name="invitedUserName"/>
            ////<xsl:param name="applicationName"/>
            ////<xsl:param name="subDomain"/>
            ////<xsl:param name="invitedUserEmailId"/>
            ////<xsl:param name="portalURL"/>
            ////<xsl:param name="copyrightText"/>
            //#endregion xslt arguments

            //#region TokeInfo

            //TokenDataDTO tokenDataDTO = new TokenDataDTO();
            //tokenDataDTO.Code = businessNotificationDTO.TenantUser.Code;
            //tokenDataDTO.Email = businessNotificationDTO.TenantUser.Email;
            //tokenDataDTO.IdentityUserId = businessNotificationDTO.TenantUser.IdentityUserId;
            //tokenDataDTO.TenantId = businessNotificationDTO.UserSession.TenantId;
            //tokenDataDTO.AppKey = businessNotificationDTO.App.AppKey;

            //TokenInfo tokenInfo = new TokenInfo();
            //tokenInfo.ID = Guid.NewGuid();
            //tokenInfo.AppKey = businessNotificationDTO.App.AppKey;
            //tokenInfo.TenantUserId = businessNotificationDTO.TenantUser.ID;
            //tokenInfo.CreatedDate = DateTime.UtcNow;
            //tokenInfo.TenantId = businessNotificationDTO.UserSession.TenantId;
            //tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            //tokenInfo.TokenType = (int)BusinessTokenTypeEnum.BusinessPartnerOtherUserNewEmailId;

            //_tokenInfoDataService.Add(tokenInfo);
            //_unitOfWork.Save();

            //#endregion TokeInfo

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessCompanyName", businessNotificationDTO.BusinessCompanyName);
            //eventData.Add("businessPartnerType", businessNotificationDTO.PartnerType);
            //eventData.Add("businessPartnerCompanyName", businessNotificationDTO.BusinessPartnerCompanyName);
            //eventData.Add("invitedUserName", businessNotificationDTO.InvitorUserName);
            //eventData.Add("applicationName", businessNotificationDTO.ApplicationName);
            //eventData.Add("subDomain", businessNotificationDTO.SubDomain);
            //eventData.Add("invitedUserEmailId", businessNotificationDTO.TenantUser.Email);
            //eventData.Add("portalURL", businessNotificationDTO.PortalURL);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());

            //eventData.Add("appkey", businessNotificationDTO.App.AppKey);

            //Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            //deeplinkInfo.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());
            //deeplinkInfo.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //deeplinkInfo.Add("identityserveruserid", businessNotificationDTO.TenantUser.IdentityUserId.ToString());
            //deeplinkInfo.Add("loginEmail", businessNotificationDTO.TenantUser.Email);
            //deeplinkInfo.Add("usertype", UserTypeEnum.Business.ToString());
            //deeplinkInfo.Add("code", businessNotificationDTO.TenantUser.Code);
            //deeplinkInfo.Add("partnerid", " 123 ");
            //deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            //deeplinkInfo.Add("subDomain", businessNotificationDTO.SubDomain);
            //deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessPartnerOtherUserNewEmailId, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
        }

        //15
        ///<inheritdoc/>
        public async Task GenerateBusinessPartnerOtherUserExistingEmailIdInvitedNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments
            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessCompanyName"/>
            ////<xsl:param name="businessPartnerType"/>
            ////<xsl:param name="businessPartnerCompanyName"/>
            ////<xsl:param name="invitedUserName"/>
            ////<xsl:param name="emailID"/>
            ////<xsl:param name="portalName"/>
            ////<xsl:param name="applicationName"/>
            ////<xsl:param name="domainURL"/>
            ////<xsl:param name="subDomain"/>
            ////<xsl:param name="invitedUserEmailId"/>
            ////<xsl:param name="portalURL"/>
            ////<xsl:param name="copyrightText"/>
            //#endregion xslt arguments

            //#region TokeInfo

            //TokenDataDTO tokenDataDTO = new TokenDataDTO();
            //tokenDataDTO.Code = businessNotificationDTO.TenantUser.Code;
            //tokenDataDTO.Email = businessNotificationDTO.TenantUser.Email;
            //tokenDataDTO.IdentityUserId = businessNotificationDTO.TenantUser.IdentityUserId;
            //tokenDataDTO.TenantId = businessNotificationDTO.UserSession.TenantId;
            //tokenDataDTO.AppKey = businessNotificationDTO.App.AppKey;

            //TokenInfo tokenInfo = new TokenInfo();
            //tokenInfo.ID = Guid.NewGuid();
            //tokenInfo.AppKey = businessNotificationDTO.App.AppKey;
            //tokenInfo.TenantUserId = businessNotificationDTO.TenantUser.ID;
            //tokenInfo.CreatedDate = DateTime.UtcNow;
            //tokenInfo.TenantId = businessNotificationDTO.UserSession.TenantId;
            //tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            //tokenInfo.TokenType = (int)BusinessTokenTypeEnum.BusinessPartnerOtherUserExistingEmailId;

            //_tokenInfoDataService.Add(tokenInfo);
            //_unitOfWork.Save();

            //#endregion TokeInfo

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessCompanyName", businessNotificationDTO.BusinessCompanyName);
            //eventData.Add("businessPartnerType", businessNotificationDTO.PartnerType);
            //eventData.Add("businessPartnerCompanyName", businessNotificationDTO.BusinessPartnerCompanyName);
            //eventData.Add("invitedUserName", businessNotificationDTO.InvitorUserName);
            //eventData.Add("emailID", businessNotificationDTO.TenantUser.Email);
            //eventData.Add("portalName", businessNotificationDTO.PortalName);
            //eventData.Add("applicationName", businessNotificationDTO.ApplicationName);
            //eventData.Add("subDomain", businessNotificationDTO.SubDomain);
            //eventData.Add("invitedUserEmailId", businessNotificationDTO.TenantUser.Email);
            //eventData.Add("portalURL", businessNotificationDTO.PortalURL);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());

            //eventData.Add("appkey", businessNotificationDTO.App.AppKey);

            //Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            //deeplinkInfo.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());
            //deeplinkInfo.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //deeplinkInfo.Add("identityserveruserid", businessNotificationDTO.TenantUser.IdentityUserId.ToString());
            //deeplinkInfo.Add("loginEmail", businessNotificationDTO.TenantUser.Email);
            //deeplinkInfo.Add("usertype", UserTypeEnum.Business.ToString());
            //deeplinkInfo.Add("code", businessNotificationDTO.TenantUser.Code);
            //deeplinkInfo.Add("partnerid", " 123 ");
            //deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            //deeplinkInfo.Add("subDomain", businessNotificationDTO.SubDomain);
            //deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessPartnerOtherUserExistingEmailId, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
        }

        //16
        ///<inheritdoc/>
        public async Task GenerateApplicationDeAssignedToBusinessPartnerUserNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments

            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessPartnerType"/>
            ////<xsl:param name="businessPartnerUserName"/>
            ////<xsl:param name="applicationName"/>
            ////<xsl:param name="copyrightText"/>

            //#endregion xslt arguments

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessPartnerType", businessNotificationDTO.PartnerType);
            //eventData.Add("businessPartnerUserName", businessNotificationDTO.TenantUser.FullName);
            //eventData.Add("applicationName", businessNotificationDTO.ApplicationName);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.ApplicationDeAssignedToBusinessPartnerUser, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
        }

        //17
        ///<inheritdoc/>
        public async Task GenerateApplicationRemovedForCustomerToBusinessPartnerUserNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments

            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessPartnerType"/>
            ////<xsl:param name="businessPartnerCompanyName"/>
            ////<xsl:param name="applicationName"/>
            ////<xsl:param name="copyrightText"/>

            //#endregion xslt arguments

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessPartnerType", businessNotificationDTO.PartnerType);
            //eventData.Add("businessPartnerUserName", businessNotificationDTO.TenantUser.FullName);
            //eventData.Add("applicationName", businessNotificationDTO.ApplicationName);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.ApplicationRemovedForCustomerToBusinessPartnerUser, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
        }

        //18
        ///<inheritdoc/>
        public async Task GenerateApplicationRemovedForCustomerToBusinessUserNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments

            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessCompanyName"/>
            ////<xsl:param name="businessPartnerCompanyName"/>
            ////<xsl:param name="businessPartnerType"/>
            ////<xsl:param name="businessUserNameChange"/>
            ////<xsl:param name="applicationName"/>
            ////<xsl:param name="copyrightText"/>

            //#endregion xslt arguments

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessCompanyName", businessNotificationDTO.BusinessCompanyName);
            //eventData.Add("businessPartnerCompanyName", businessNotificationDTO.BusinessPartnerCompanyName);
            //eventData.Add("businessPartnerType", businessNotificationDTO.PartnerType);
            //eventData.Add("businessUserNameChange", businessNotificationDTO.InvitorUserName);
            //eventData.Add("applicationName", businessNotificationDTO.ApplicationName);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.ApplicationRemovedForCustomerToBusinessUser, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
        }

        //19
        ///<inheritdoc/>
        public async Task GenerateBusinessPartnerDeletedToCustomerUserNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments
            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessCompanyName"/>
            ////<xsl:param name="businessPartnerCompanyName"/>
            ////<xsl:param name="businessPartnerType"/>
            ////<xsl:param name="businessUserNameChange"/>
            ////<xsl:param name="copyrightText"/>
            //#endregion xslt arguments

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessCompanyName", businessNotificationDTO.BusinessCompanyName);
            //eventData.Add("businessPartnerCompanyName", businessNotificationDTO.BusinessPartnerCompanyName);
            //eventData.Add("businessPartnerType", businessNotificationDTO.PartnerType);
            //eventData.Add("businessUserNameChange", businessNotificationDTO.InvitorUserName);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("businesspartnertenantid", businessNotificationDTO.BusinessPartnerTenantId.ToString());
            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessPartnerDeletedToPartnerUser, false, eventDataDict, businessNotificationDTO.UserSession.TenantUserId, false);
        }

        //20
        ///<inheritdoc/>
        public async Task GenerateBusinessPartnerDeletedToBusinessUserNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments
            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessCompanyName"/>
            ////<xsl:param name="businessPartnerCompanyName"/>
            ////<xsl:param name="businessPartnerType"/>
            ////<xsl:param name="businessUserNameChange"/>
            ////<xsl:param name="copyrightText"/>
            //#endregion xslt arguments

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessCompanyName", businessNotificationDTO.BusinessCompanyName);
            //eventData.Add("businessPartnerCompanyName", businessNotificationDTO.BusinessPartnerCompanyName);
            //eventData.Add("businessPartnerType", businessNotificationDTO.PartnerType);
            //eventData.Add("businessUserNameChange", businessNotificationDTO.InvitorUserName);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessPartnerDeletedToBusinessUser, false, eventDataDict, businessNotificationDTO.UserSession.TenantUserId, false);
        }

        //21
        ///<inheritdoc/>
        public async Task GenerateExistingBusinessPartnerUserDeletedToPartnerUserNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments
            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessCompanyName"/>
            ////<xsl:param name="businessPartnerUserName"/>
            ////<xsl:param name="businessPartnerType"/>
            ////<xsl:param name="copyrightText"/>
            //#endregion xslt arguments

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessCompanyName", businessNotificationDTO.BusinessCompanyName);
            //eventData.Add("businessPartnerUserName", businessNotificationDTO.TenantUser.FullName);
            //eventData.Add("businessPartnerType", businessNotificationDTO.PartnerType);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.ExistingBusinessPartnerUserDeletedToPartnerUser, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
        }

        //22
        ///<inheritdoc/>
        public async Task GenerateBusinessPartnerUserDeletedToCustomerUserNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            //#region xslt arguments
            ////<xsl:param name="publisherCompanyName"/>
            ////<xsl:param name="businessPartnerCompanyName"/>
            ////<xsl:param name="businessPartnerUserName"/>
            ////<xsl:param name="businessCompanyName"/>
            ////<xsl:param name="businessUserNameChange"/>
            ////<xsl:param name="copyrightText"/>
            //#endregion xslt arguments

            //// Create notification information
            //Dictionary<string, string> eventData = new Dictionary<string, string>();

            //eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            //eventData.Add("businessPartnerCompanyName", businessNotificationDTO.BusinessPartnerCompanyName);
            //eventData.Add("businessPartnerUserName", businessNotificationDTO.TenantUser.FullName);
            //eventData.Add("businessCompanyName", businessNotificationDTO.BusinessCompanyName);
            //eventData.Add("businessUserNameChange", businessNotificationDTO.InvitorUserName);
            //eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            //eventData.Add("appuserid", businessNotificationDTO.TenantUser.ID.ToString());
            //eventData.Add("appid", businessNotificationDTO.App.ID.ToString());
            //eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());

            //// Creates list of dictionary for event data.
            //Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            //eventDataDict.Add("EventData", eventData);
            //eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.ExistingBusinessPartnerUserDeletedToBusinessUser, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
        }

        #endregion Business Partner User

        #region Forgot Password

        //23
        // InUse
        ///<inheritdoc/>
        public async Task GenerateBusinessForgotPasswordAsync(BusinessAccountNotificationDTO businessNotificationDTO) {

            #region xslt arguments
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="businessUserName"/>
            //<xsl:param name="businessUserID"/>
            //<xsl:param name="copyrightText"/>
            #endregion xslt arguments

            #region TokeInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = businessNotificationDTO.PasswordCode;
            tokenDataDTO.Email = businessNotificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = businessNotificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = businessNotificationDTO.TenantId;
            tokenDataDTO.AppKey = businessNotificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = businessNotificationDTO.UserType;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = businessNotificationDTO.InvitedUserAppKey;
            tokenInfo.TenantUserId = businessNotificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = businessNotificationDTO.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)BusinessTokenTypeEnum.BusinessUserForgotPassword;
            tokenInfo.UserType = businessNotificationDTO.UserType;

            _tokenInfoDataService.Add(tokenInfo);
            _unitOfWork.Save();

            #endregion TokeInfo

            // Create notification information
            Dictionary<string, string> eventData = new Dictionary<string, string>();

            eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            eventData.Add("businessCompanyName", businessNotificationDTO.BusinessCompanyName);
            eventData.Add("businessUserName", businessNotificationDTO.InvitedUserFullName);
            eventData.Add("businessUserID", businessNotificationDTO.InvitedUserEmail);
            eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

            eventData.Add("appuserid", businessNotificationDTO.InvitedUserId.ToString());
            eventData.Add("appid", businessNotificationDTO.InvitedUserAppId.ToString());
            eventData.Add("tenantid", businessNotificationDTO.TenantId.ToString());

            Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            deeplinkInfo.Add("tenantid", businessNotificationDTO.TenantId.ToString());
            deeplinkInfo.Add("appuserid", businessNotificationDTO.InvitedUserId.ToString());
            deeplinkInfo.Add("identityserveruserid", businessNotificationDTO.InvitedUserIdentityUserId.ToString());
            deeplinkInfo.Add("loginEmail", businessNotificationDTO.InvitedUserEmail);
            deeplinkInfo.Add("usertype", UserTypeEnum.Business.ToString());
            deeplinkInfo.Add("code", businessNotificationDTO.PasswordCode);
            deeplinkInfo.Add("partnerid", "123");
            deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            deeplinkInfo.Add("subDomain", businessNotificationDTO.SubDomain);
            deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(businessNotificationDTO.UserSession));

            // Send notification.
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)BusinessNotificationEventEnum.BusinessUserForgotPassword;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = businessNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _businessNotificationDataService.GenerateNotificationAsync(generateNotificationDTO);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessUserForgotPassword, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
        }



        #endregion Forgot Password

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

        #endregion New

        #endregion Public Methods

        #region Private Method

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

        #endregion Private Method
    }
}
