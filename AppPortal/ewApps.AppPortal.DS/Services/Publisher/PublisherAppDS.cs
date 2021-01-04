/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author : Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 05 September 2019
 * 
 * Contributor/s: 
 * Last Updated On: 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.AppPortal.QData;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public class PublisherAppDS:IPublisherAppDS {

        #region Local Member  

        IUserSessionManager _userSessionManager;
        IPublisherDS _publisherDS;
        IPublisherAppSettingDS _publisherAppSettingDS;
        IQPublisherAndUserDS _qPublisherDS;
        AppPortalAppSettings _appPortalAppSettings;
        IPubBusinessSubsPlanAppServiceDS _pubBusinessSubPlanAppServiceDS;
        IEntityThumbnailDS _entityThumbnailDS;
        IUnitOfWork _unitOfWork;
        IBusinessExtDS _businessExtDS;
        IQPublisherAndUserRepository _qPublisherRepository;

        #endregion

        #region Constructor 

        /// <summary>
        /// Constructor to init service class.
        /// </summary>
        /// <param name="publisherDS">Publisher dataservice.</param>
        /// <param name="qPublisherDS"></param>
        /// <param name="publisherAppSettingDS">publisherAppSettingDS object</param>
        /// <param name="userSessionManager">User session manager.</param>
        public PublisherAppDS(IPublisherDS publisherDS, IQPublisherAndUserDS qPublisherDS,
                        IPublisherAppSettingDS publisherAppSettingDS, IUserSessionManager userSessionManager, IOptions<AppPortalAppSettings> appPortalAppSettingsOps,
                        IPubBusinessSubsPlanAppServiceDS pubBusinessSubPlanAppServiceDS, IEntityThumbnailDS entityThumbnailDS, IUnitOfWork unitOfWork,
                        IBusinessExtDS businessExtDS, IQPublisherAndUserRepository qPublisherRepository) {

            _publisherDS = publisherDS;
            _qPublisherDS = qPublisherDS;
            _publisherAppSettingDS = publisherAppSettingDS;
            _userSessionManager = userSessionManager;
            _appPortalAppSettings = appPortalAppSettingsOps.Value;
            _pubBusinessSubPlanAppServiceDS = pubBusinessSubPlanAppServiceDS;
            _entityThumbnailDS = entityThumbnailDS;
            _unitOfWork = unitOfWork;
            _businessExtDS = businessExtDS;
            _qPublisherRepository = qPublisherRepository;
        }

        #endregion Constructor   

        #region Support

        ///<inheritdoc/>
        public async Task<List<BusinessApplicationDTO>> GetPublisherAppServicesAndSubscriptionDetailsAsync(string sourcesubdomain, bool includeInactive, CancellationToken token = default(CancellationToken)) {
            List<BusinessApplicationDTO> businessApplicationDTOs = new List<BusinessApplicationDTO>();
            List<AppDQ> apps = null;
            UserSession session = _userSessionManager.GetSession();
            Guid pubTenantId = Guid.Empty;
            if(session != null) {
                pubTenantId = session.TenantId;
            }

            #region Get publisher subscribed application

            apps = await _qPublisherDS.GetPublisherSubscribedAppSubdomainAsync(sourcesubdomain, includeInactive, token);

            #endregion Get publisher subscribed application


            Entity.Publisher pub = await _publisherDS.GetPublisherByPublisherTenantIdAsync(pubTenantId, token);
            if(pubTenantId == Guid.Empty) {
                pubTenantId = pub.TenantId;
            }
            string Language = "";
            string Currency = "";
            string DateTimeFormat = "";
            string TimeZone = "";
            if(pub != null) {
                Language = pub.Language; //pubAppstting[0].Language;
                if(pub.CurrencyCode != null)
                    Currency = pub.CurrencyCode.Value.ToString(); //pubAppstting[0].Currency;
                DateTimeFormat = pub.DateTimeFormat;
                TimeZone = pub.TimeZone;
            }
            List<TenantAppServiceDQ> listAppServiceDTO = null;
            if(pubTenantId != Guid.Empty) {
                //listAppServiceDTO = await _qPublisherDS.GetPublisherAppServiceByTenantIdAsync(pubTenantId, token);
            }

            #region Adding active subscribed application & services

            foreach(AppDQ item in apps) {
                if(item.Deleted || (!includeInactive && !item.Active) || item.AppSubscriptionMode != 2)
                    continue;
                BusinessApplicationDTO model = new BusinessApplicationDTO();
                model.AppId = item.ID;
                model.Name = item.Name;
                model.ThemeId = item.ThemeId;
                model.AppActive = item.Active;

                model.Language = Language;
                model.Currency = Currency;
                model.DateTimeFormat = DateTimeFormat;
                model.TimeZone = TimeZone;

                //model.appServices = FilterAppServiceAndAttributeList(listAppServiceDTO, item.ID, pubTenantId);

                // GetAppSubscription
                //model.appSubscriptions = await _qPublisherDS.GetAppSubscriptionAsync(model.AppId, pubTenantId, token);
                model.appSubscriptions = await _qPublisherDS.GetSubscriptionPlanListByAppAndPubTenantIdAsync(model.AppId, pubTenantId, BooleanFilterEnum.True, token);
                //model.appSubscriptions = new List<TenantApplicationSubscriptionDQ>();
                model.appServices = new List<AppServiceDTO>();
                businessApplicationDTOs.Add(model);
            }

            #endregion Adding active subscribed application & services

            return businessApplicationDTOs;
        }

        ///<inheritdoc/>
        public async Task<List<BusinessApplicationDTO>> GetPublisherAppServicesAndSubscriptionDetailsOnPlatformAsync(string sourcesubdomain, bool includeInactive,Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {
            List<BusinessApplicationDTO> businessApplicationDTOs = new List<BusinessApplicationDTO>();
            List<AppDQ> apps = null;
            UserSession session = _userSessionManager.GetSession();
            //Guid pubTenantId = Guid.Empty;
            //if(session != null) {
            //    pubTenantId = session.TenantId;
            //}

            #region Get publisher subscribed application

            apps = await _qPublisherDS.GetPublisherSubscribedAppSubdomainAsync(sourcesubdomain, includeInactive, token);

            #endregion Get publisher subscribed application


            Entity.Publisher pub = await _publisherDS.GetPublisherByPublisherTenantIdAsync(publisherTenantId, token);
            if(publisherTenantId == Guid.Empty) {
                publisherTenantId = pub.TenantId;
            }
            string Language = "";
            string Currency = "";
            string DateTimeFormat = "";
            string TimeZone = "";
            if(pub != null) {
                Language = pub.Language; //pubAppstting[0].Language;
                if(pub.CurrencyCode != null)
                    Currency = pub.CurrencyCode.Value.ToString(); //pubAppstting[0].Currency;
                DateTimeFormat = pub.DateTimeFormat;
                TimeZone = pub.TimeZone;
            }
            List<TenantAppServiceDQ> listAppServiceDTO = null;
            if(publisherTenantId != Guid.Empty) {
                //listAppServiceDTO = await _qPublisherDS.GetPublisherAppServiceByTenantIdAsync(pubTenantId, token);
            }

            #region Adding active subscribed application & services

            foreach(AppDQ item in apps) {
                if(item.Deleted || (!includeInactive && !item.Active) || item.AppSubscriptionMode != 2)
                    continue;
                BusinessApplicationDTO model = new BusinessApplicationDTO();
                model.AppId = item.ID;
                model.Name = item.Name;
                model.ThemeId = item.ThemeId;
                model.AppActive = item.Active;

                model.Language = Language;
                model.Currency = Currency;
                model.DateTimeFormat = DateTimeFormat;
                model.TimeZone = TimeZone;

                //model.appServices = FilterAppServiceAndAttributeList(listAppServiceDTO, item.ID, pubTenantId);

                // GetAppSubscription
                //model.appSubscriptions = await _qPublisherDS.GetAppSubscriptionAsync(model.AppId, pubTenantId, token);
                model.appSubscriptions = await _qPublisherDS.GetSubscriptionPlanListByAppAndPubTenantIdAsync(model.AppId, publisherTenantId, BooleanFilterEnum.True, token);
                //model.appSubscriptions = new List<TenantApplicationSubscriptionDQ>();
                model.appServices = new List<AppServiceDTO>();
                businessApplicationDTOs.Add(model);
            }

            #endregion Adding active subscribed application & services

            return businessApplicationDTOs;
        }

        /// <summary>
        /// Filter the service and attributes by appid and return it.
        /// </summary>
        /// <param name="listService">Collection of services subscribed by a publisher.</param>
        /// <param name="appId"></param>
        private List<AppServiceDTO> FilterAppServiceAndAttributeList(List<TenantAppServiceDQ> listService, Guid appId, Guid tenantId) {
            List<AppServiceDTO> appServiceList = new List<AppServiceDTO>();
            Dictionary<Guid, AppServiceDTO> dict = new Dictionary<Guid, AppServiceDTO>();
            AppServiceDTO srvcDto = null;
            AppServiceAttributeDTO appAttr = null;
            for(int i = 0; i < listService.Count; i++) {
                if(listService[i].AppId == appId) {
                    // Find the service if  exist.
                    if(dict.TryGetValue(listService[i].AppServiceId, out srvcDto)) {
                        // Add attribute.
                        if(listService[i].AttributeId != Guid.Empty) {
                            appAttr = new AppServiceAttributeDTO();
                            appAttr.Active = true;
                            appAttr.ID = listService[i].AttributeId;
                            appAttr.Name = listService[i].AttributeName;
                            appAttr.TenantId = tenantId;
                            srvcDto.AppServiceAttributeList.Add(appAttr);
                        }
                    }
                    else {
                        srvcDto = new AppServiceDTO();
                        srvcDto.Active = true;
                        srvcDto.ID = listService[i].AppServiceId;
                        srvcDto.Name = listService[i].Name;
                        srvcDto.TenantId = tenantId;

                        srvcDto.AppServiceAttributeList = new List<AppServiceAttributeDTO>();
                        // Add attribute.
                        if(listService[i].AttributeId != Guid.Empty) {
                            appAttr = new AppServiceAttributeDTO();
                            appAttr.Active = true;
                            appAttr.ID = listService[i].AttributeId;
                            appAttr.Name = listService[i].AttributeName;
                            appAttr.TenantId = tenantId;
                            srvcDto.AppServiceAttributeList.Add(appAttr);
                        }
                        // Add service.
                        appServiceList.Add(srvcDto);
                        dict.Add(listService[i].AppServiceId, srvcDto);
                    }
                }
            }

            return appServiceList;
        }

        #endregion Support

        #region Get Methods

        /// <Inheritdoc/>
        public async Task<List<AppInfoDTO>> GetApplicationListAsync(bool active, int subscriptionMode) {

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            string methodUrl = string.Format("app/list/{0}/{1}", active.ToString(), subscriptionMode.ToString());
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUrl, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.AppMgmtApiUrl);
            return await serviceExecutor.ExecuteAsync<List<AppInfoDTO>>(requestOptions, false);
        }


        ///<inheritdoc/>
        public async Task<List<AppDetailDTO>> GetAppDetailsPublisherAsync(Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {

            return await _qPublisherDS.GetAppDetailsPublisherAsync(publisherTenantId, token);
        }

        ///<inheritdoc/>
        public async Task<AppAndServiceDTO> GetAppDetailsWithServicesPublisherAsync(Guid publisherAppSettingID, CancellationToken token = default(CancellationToken)) {

            AppAndServiceDTO appAndServiceDTO = new AppAndServiceDTO();
            Guid tenantId = Guid.Empty;
            UserSession session = _userSessionManager.GetSession();
            if(session != null) {
                tenantId = session.TenantId;
            }

            // Get Publisher App Setting
            appAndServiceDTO = await _qPublisherDS.GetAppDetailsWithServicesPublisherAsync(publisherAppSettingID, appAndServiceDTO, token);

            List<PubBusinessSubsPlanAppService> pubBusinessSubsPlanAppService = await _pubBusinessSubPlanAppServiceDS.GetListPubBusinessSubsPlanAppServiceAsync(appAndServiceDTO.AppDetailDTO.AppID, tenantId);

            // Get App Services
            // appAndServiceDTO.AppServiceDTOs = await GetServiceListAsync(pubBusinessSubsPlanAppService);

            // Get App Services attribute
            appAndServiceDTO.AppServiceModelList = await GetServiceAttributeListAsync(pubBusinessSubsPlanAppService);
            appAndServiceDTO.AppServiceDTOs = await GetServiceAttributeListAsync(pubBusinessSubsPlanAppService);





            //Get thumbnail details
            if(appAndServiceDTO.AppDetailDTO.ThumbnailId != null)
                appAndServiceDTO.ThumbnailAddAndUpdateDTO = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(appAndServiceDTO.AppDetailDTO.ThumbnailId.Value);

            return appAndServiceDTO;

        }

        /// <Inheritdoc/>
        private async Task<List<AppServiceDTO>> GetServiceListAsync(List<PubBusinessSubsPlanAppService> pubBusinessSubsPlanAppService) {

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            string methodUrl = "app/servicelist";
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, methodUrl, pubBusinessSubsPlanAppService, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.AppMgmtApiUrl);
            return await serviceExecutor.ExecuteAsync<List<AppServiceDTO>>(requestOptions, false);
        }

        /// <Inheritdoc/>
        private async Task<List<AppServiceDTO>> GetServiceAttributeListAsync(List<PubBusinessSubsPlanAppService> pubBusinessSubsPlanAppService) {

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            string methodUrl = "app/serviceattributelist";
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, methodUrl, pubBusinessSubsPlanAppService, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.AppMgmtApiUrl);
            return await serviceExecutor.ExecuteAsync<List<AppServiceDTO>>(requestOptions, false);
        }


        ///<inheritdoc/>
        public async Task<List<AppServiceDTO>> GetAppServicesNameByAppIdAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {

            List<AppServiceDTO> appServiceDTO = new List<AppServiceDTO>();
            List<PubBusinessSubsPlanAppService> pubBusinessSubsPlanAppService = await _pubBusinessSubPlanAppServiceDS.GetListPubBusinessSubsPlanAppServiceAsync(appId, publisherTenantId);

            // Get App Services
            appServiceDTO = await GetServiceListAsync(pubBusinessSubsPlanAppService);
            return appServiceDTO;
        }

        ///<inheritdoc/>
        public async Task<List<string>> GetBusinessNameByAppIdAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {
            return await GetBusinessNameAsync(appId, publisherTenantId, token);
        }

        /// <Inheritdoc/>
        private async Task<List<string>> GetBusinessNameAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            string methodUrl = "app/businessname/" + appId + "/" + publisherTenantId;
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUrl, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.AppMgmtApiUrl);
            return await serviceExecutor.ExecuteAsync<List<string>>(requestOptions, false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> GetAppNameListByPublisheIdAsync(Guid publisherTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            IEnumerable<string> appNameList = await _publisherAppSettingDS.GetAppNameListByPublisherIdAsync(publisherTenantId, cancellationToken);
            return appNameList;
        }

        #endregion

        #region Update

        ///<inheritdoc/>
        public async Task UpdateAppAsync(AppAndServiceDTO appAndServiceDTO, CancellationToken token = default(CancellationToken)) {

            // Check update permission
            // CheckSecurityOnUpdating();

            //update publisher app setting application
            await _publisherAppSettingDS.UpdateAppAsync(appAndServiceDTO, token);
            _unitOfWork.SaveAll();

        }
        #endregion Update
    }
}
