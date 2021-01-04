/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 12 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 12 September 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This class implements standard business logic and operations for About Us entity.
    /// </summary>
    public class AboutUsDS:IAboutUsDS {

        #region Local member

        IUserSessionManager _userSessionManager;
        DMServiceSettings _appSettings;
        AppPortalAppSettings _appPortalAppSettings;
        IPlatformDS _platformDS;
        IEntityThumbnailDS _entityThumbnailDS;
        IPublisherDS _publisherDS;
        IPublisherAppSettingDS _publisherAppSettingDS;

        #endregion Local member

        #region Constructor

        /// <summary>
        /// 
        /// </summary>     
        /// <param name="tenantLinkingDS"></param>
        public AboutUsDS(IUserSessionManager userSessionManager, IOptions<DMServiceSettings> appSettings, IOptions<AppPortalAppSettings> appPortalAppSettings, IPlatformDS platformDS, IEntityThumbnailDS entityThumbnailDS, IPublisherDS publisherDS, IPublisherAppSettingDS publisherAppSettingDS) {
            _userSessionManager = userSessionManager;
            _appSettings = appSettings.Value;
            _platformDS = platformDS;
            _entityThumbnailDS = entityThumbnailDS;
            _appPortalAppSettings = appPortalAppSettings.Value;
            _publisherDS = publisherDS;
            _publisherAppSettingDS = publisherAppSettingDS;
        }

        #endregion Constructor

        #region Platform 

        #region About Us On Platform 

        ///<inheritdoc/>
        public async Task<AboutUsDTO> GetPlatformAboutUsDetailsAsync() {
            UserSession session = _userSessionManager.GetSession();
            AboutUsDTO aboutUsDTO = new AboutUsDTO();


            Platform platform = await _platformDS.FindAsync(a => a.TenantId == session.TenantId);
            aboutUsDTO.ApplicationName = "";
            aboutUsDTO.CompanyName = session.TenantName;
            aboutUsDTO.CopyRightText = platform.Copyright;
            aboutUsDTO.PortalName = "Platform Portal";

            List<EntityThumbnail> platformThumbnail = _entityThumbnailDS.FindAllAsync(t => t.OwnerEntityId == session.TenantId).Result.ToList();
            if(platformThumbnail != null && platformThumbnail.Count > 0) {
                aboutUsDTO.LogoURL = System.IO.Path.Combine(_appSettings.ThumbnailUrl, platformThumbnail[0].TenantId.ToString(), platformThumbnail[0].ID.ToString(), platformThumbnail[0].FileName);
            }
            aboutUsDTO.PolicyURL = "";
            aboutUsDTO.TermsURL = "";
            aboutUsDTO.VersionNumber = "1.2.3";
            return aboutUsDTO;
        }
        #endregion About Us On Platform 

       

        #endregion Platform

        #region Publisher About Us Details
        ///<inheritdoc/>
        public async Task<AboutUsDTO> GetPublisherAboutUsDetails() {
            UserSession session = _userSessionManager.GetSession();
            AboutUsDTO aboutUsDTO = new AboutUsDTO();
            TenantInfoDTO tenantInfoDTO = new TenantInfoDTO();

            //Get TenantLinking Details
            tenantInfoDTO = await GetTenantLinkingDetails(session.ID, session.TenantId,TenantType.Publisher);

            Guid PlatformTenantId = (tenantInfoDTO.PlatformTenantId).Value;

            if(tenantInfoDTO != null) {

                ////Get Tenant Details
                //tenantInfoDTO = await GetTenantDetails(session.ID, session.TenantId);

                 //TenantAppSetting tenantAppSetting = _tenantAppSettingDS.FindAllAsync(a => a.TenantId == tenantLinking.PlatformTenantId).Result.FirstOrDefault();
                 Platform platform = await _platformDS.FindAsync(a => a.TenantId == PlatformTenantId);
                aboutUsDTO.ApplicationName = "";
                aboutUsDTO.CompanyName = tenantInfoDTO.TenantName;
                aboutUsDTO.CopyRightText = platform.Copyright;
                aboutUsDTO.PortalName = "Publisher Portal";

                //Get Platform Thumbnail 
                List<EntityThumbnail> platformThumbnail = _entityThumbnailDS.FindAllAsync(t => t.OwnerEntityId == PlatformTenantId).Result.ToList();
                if(platformThumbnail != null && platformThumbnail.Count > 0) {
                    aboutUsDTO.LogoURL = System.IO.Path.Combine(_appSettings.ThumbnailUrl, platformThumbnail[0].TenantId.ToString(), platformThumbnail[0].ID.ToString(), platformThumbnail[0].FileName);
                }
                aboutUsDTO.PolicyURL = "";
                aboutUsDTO.TermsURL = "";
                aboutUsDTO.VersionNumber = "1.2.3";
            }

            return aboutUsDTO;
        }
        #endregion Publisher About Us Details

        //For App Ship To Payment  
        #region Business About Us Detail 
        ///<inheritdoc/>
        public async Task<AboutUsDTO> GetBusinessAboutUsDetails(Guid appId) {           

            UserSession session = _userSessionManager.GetSession();
            AboutUsDTO aboutUsDTO = new AboutUsDTO();
            TenantInfoDTO tenantInfoDTO = new TenantInfoDTO();            

            //Get TenantLinking Details
            tenantInfoDTO = await GetTenantLinkingDetails(session.ID, session.TenantId, TenantType.Buisness);

            if(tenantInfoDTO != null) {
                
                //Get Publisher 
                Publisher publisher = await _publisherDS.FindAsync(a => a.TenantId == tenantInfoDTO.PublisherTenantId);

                //Get AppInfo
                string payKey;
                List<AppInfoDTO> appInfoDTOs = new List<AppInfoDTO>();
                appInfoDTOs = await GetAppInfoByAppId(session.ID,appId);
                if(appInfoDTOs != null) {
                    foreach(AppInfoDTO appInfoDTO in appInfoDTOs) {
                        if(appInfoDTO.AppKey.Equals(AppKeyEnum.biz.ToString(),StringComparison.CurrentCultureIgnoreCase) || appInfoDTO.AppKey.Equals(AppKeyEnum.custsetup.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                            aboutUsDTO.ApplicationName = "";
                            if(publisher.LogoThumbnailId != null) {
                                EntityThumbnail thumbnail = (await _entityThumbnailDS.FindAllAsync(t => t.ID == publisher.LogoThumbnailId)).FirstOrDefault();
                                if(thumbnail != null) {
                                    aboutUsDTO.LogoURL = System.IO.Path.Combine(_appSettings.ThumbnailUrl, thumbnail.TenantId.ToString(), thumbnail.ID.ToString(), thumbnail.FileName);
                                }
                            }
                        }
                        else {
                            //Get PublisherAppsetting
                            PublisherAppSetting app = await _publisherAppSettingDS.FindAsync(a => a.AppId == appId && a.TenantId == publisher.TenantId);
                            aboutUsDTO.ApplicationName = app.Name;
                            if(app.ThumbnailId != null && app.ThumbnailId != Guid.Empty) {
                                EntityThumbnail appThumbnail = await _entityThumbnailDS.GetAsync((Guid)app.ThumbnailId);
                                if(appThumbnail != null) {
                                    aboutUsDTO.LogoURL = System.IO.Path.Combine(_appSettings.ThumbnailUrl, appThumbnail.TenantId.ToString(), appThumbnail.ID.ToString(), appThumbnail.FileName);
                                }
                            }
                        }
                    }
                }              

                
                aboutUsDTO.CompanyName = publisher.Name;
                aboutUsDTO.CopyRightText = publisher.Copyright;

               
                aboutUsDTO.PolicyURL = "";
                aboutUsDTO.TermsURL = "";
                aboutUsDTO.VersionNumber = "1.2.3";

                // UserType dependent things
                if(session.UserType == (int)UserTypeEnum.Business) {
                    aboutUsDTO.PortalName = "Business Portal";
                }
                else if(session.UserType == (int)UserTypeEnum.Customer) {
                    aboutUsDTO.PortalName = "Customer Portal";
                }
            }
            return aboutUsDTO;

        }
        #endregion

        //Get TenantLinking Details 
        private async Task<TenantInfoDTO> GetTenantLinkingDetails(Guid ID, Guid tenantId,TenantType tenantType) {

            TenantInfoDTO tenantInfoDTO = new TenantInfoDTO();

            // Preparing api calling process model.
            #region Get Tenantlinking Information
            string methodUri = "Tenant/gettenantlinkinginfo/" + tenantId + "/" + tenantType;
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUri, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            tenantInfoDTO = await httpRequestProcessor.ExecuteAsync<TenantInfoDTO>(requestOptions, false);
            #endregion

            return tenantInfoDTO;
        }

        //Get TenantLinking Details 
        private async Task<List<AppInfoDTO>> GetAppInfoByAppId(Guid ID,Guid appId) {

            List<AppInfoDTO> appInfoDTO = new List<AppInfoDTO>();
            List<Guid> appIds = new List<Guid>();
            appIds.Add(appId);
            

            // Preparing api calling process model.
            #region Get Tenantlinking Information
            string methodUri = "App/getappinfo";
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, methodUri, appIds, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            appInfoDTO = await httpRequestProcessor.ExecuteAsync<List<AppInfoDTO>>(requestOptions, false);
            #endregion

            return appInfoDTO;
        }


        //For App Ship To Payment  
        #region Vendor About Us Detail 
        ///<inheritdoc/>
        public async Task<AboutUsDTO> GetVendorAboutUsDetails(Guid appId) {

            UserSession session = _userSessionManager.GetSession();
            AboutUsDTO aboutUsDTO = new AboutUsDTO();
            TenantInfoDTO tenantInfoDTO = new TenantInfoDTO();

            //Get TenantLinking Details
            tenantInfoDTO = await GetTenantLinkingDetails(session.ID, session.TenantId, TenantType.Buisness);

            if(tenantInfoDTO != null) {

                //Get Publisher 
                Publisher publisher = await _publisherDS.FindAsync(a => a.TenantId == tenantInfoDTO.PublisherTenantId);

                //Get AppInfo
                string payKey;
                List<AppInfoDTO> appInfoDTOs = new List<AppInfoDTO>();
                appInfoDTOs = await GetAppInfoByAppId(session.ID, appId);
                if(appInfoDTOs != null) {
                    foreach(AppInfoDTO appInfoDTO in appInfoDTOs) {
                        if(appInfoDTO.AppKey.Equals(AppKeyEnum.biz.ToString(), StringComparison.CurrentCultureIgnoreCase) || appInfoDTO.AppKey.Equals(AppKeyEnum.custsetup.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                            aboutUsDTO.ApplicationName = "";
                            if(publisher.LogoThumbnailId != null) {
                                EntityThumbnail thumbnail = (await _entityThumbnailDS.FindAllAsync(t => t.ID == publisher.LogoThumbnailId)).FirstOrDefault();
                                if(thumbnail != null) {
                                    aboutUsDTO.LogoURL = System.IO.Path.Combine(_appSettings.ThumbnailUrl, thumbnail.TenantId.ToString(), thumbnail.ID.ToString(), thumbnail.FileName);
                                }
                            }
                        }
                        else {
                            //Get PublisherAppsetting
                            PublisherAppSetting app = await _publisherAppSettingDS.FindAsync(a => a.AppId == appId && a.TenantId == publisher.TenantId);
                            aboutUsDTO.ApplicationName = app.Name;
                            if(app.ThumbnailId != null && app.ThumbnailId != Guid.Empty) {
                                EntityThumbnail appThumbnail = await _entityThumbnailDS.GetAsync((Guid)app.ThumbnailId);
                                if(appThumbnail != null) {
                                    aboutUsDTO.LogoURL = System.IO.Path.Combine(_appSettings.ThumbnailUrl, appThumbnail.TenantId.ToString(), appThumbnail.ID.ToString(), appThumbnail.FileName);
                                }
                            }
                        }
                    }
                }

                aboutUsDTO.CompanyName = publisher.Name;
                aboutUsDTO.CopyRightText = publisher.Copyright;


                aboutUsDTO.PolicyURL = "";
                aboutUsDTO.TermsURL = "";
                aboutUsDTO.VersionNumber = "1.2.3";

                // UserType dependent things
                if(session.UserType == (int)UserTypeEnum.Vendor) {
                    aboutUsDTO.PortalName = "Vendor Portal";
                }
                
            }
            return aboutUsDTO;

        }
        #endregion



    }
}
