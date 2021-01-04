/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil Nigam <anigam@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppManagement.DTO;
using ewApps.AppMgmt.Common;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.ExceptionService;
using Microsoft.Extensions.Options;


namespace ewApps.AppMgmt.DS {

    /// <summary>
    /// This class implements standard business logic and operations for app entity.
    /// </summary>
    public class AppDS:BaseDS<App>, IAppDS {

        #region Local Member

        IAppRepository _appRepository;
        DMServiceSettings _thumbnailAppSettings;
        IEntityThumbnailDS _entityThumbnailDS;
        // IAppServiceRepository _appServiceRep;
        IAppServiceAttributeDS _appServiceAttributeDS;
        IAppServiceDS _appServiceDS;
        IUnitOfWork _unitOfWork;
        ITenantDS _tenantDS;


        #endregion

        #region Constructor 

        /// <summary>
        /// Initializing local variables
        /// </summary>
        public AppDS(IAppRepository appRep, IOptions<DMServiceSettings> thumbnailAppSettings, IEntityThumbnailDS entityThumbnailDS,
           IAppServiceAttributeDS appServiceAttributeDS, IAppServiceDS appServiceDS, IUnitOfWork unitOfWork, ITenantDS tenantDS) : base(appRep) {
            _appRepository = appRep;
            _thumbnailAppSettings = thumbnailAppSettings.Value;
            _entityThumbnailDS = entityThumbnailDS;
            //  _appServiceRep = appServiceRep;
            _appServiceAttributeDS = appServiceAttributeDS;
            _appServiceDS = appServiceDS;
            _unitOfWork = unitOfWork;
            _tenantDS = tenantDS;
        }

        #endregion Constructor        

        #region Get

        ///<inheritdoc/>
        public async Task<List<AppDetailDTO>> GetAppDetailsAsync() {
            List<AppDetailDTO> appList = await _appRepository.GetAppDetailsAsync();
            for(int i = 0; i < appList.Count; i++) {
                if(appList[i].ThumbnailId != null) {
                    appList[i].ThumbnailUrl = string.Format(_thumbnailAppSettings.ThumbnailUrl + appList[i].ThumbnailId + "/" + appList[i].FileName);
                }
            }
            return appList;
        }

        /// <inheritdoc/>
        public async Task<App> GetAppByAppKeyAsync(string appKey, CancellationToken token = default(CancellationToken)) {
            return await _appRepository.GetAppByAppKeyAsync(appKey, token);
        }

        /// <summary>
        ///  Get tenant settings by subdomain name and tenanttype.
        /// </summary>
        /// <param name="subdomain"></param>
        /// <param name="tenantType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<AppSettingDQ>> GetAppSettingByTenantTypeAndSubdomainAsync(string subdomain, int tenantType, CancellationToken token = default(CancellationToken)) {
            return await _appRepository.GetAppSettingByTenantTypeAndSubdomainAsync(subdomain, tenantType, token);
        }

        /// <Inheritdoc/>
        public async Task<List<AppInfoDTO>> GetAppInfoListByAppIdListAsync(List<Guid> appIdList) {
            List<App> apps = (await _appRepository.FindAllAsync(a => appIdList.Contains(a.ID) && a.Active == true && a.Deleted == false, true)).ToList();
            List<AppInfoDTO> appInfoList = new List<AppInfoDTO>();
            foreach(App item in apps) {
                AppInfoDTO dto = AppInfoDTO.MapFromApp(item);
                appInfoList.Add(dto);
            }
            return appInfoList;
        }

        /// <Inheritdoc/>
        public async Task<List<AppInfoDTO>> GetAppInfoListByAppKeyListAsync(List<string> appKeyList) {
            List<App> apps = (await _appRepository.FindAllAsync(a => appKeyList.Contains(a.AppKey), false)).ToList();
            List<AppInfoDTO> appInfoList = new List<AppInfoDTO>();
            foreach(App item in apps) {
                AppInfoDTO dto = AppInfoDTO.MapFromApp(item);
                appInfoList.Add(dto);
            }
            return appInfoList;
        }

        /// <Inheritdoc/>
        public async Task<List<AppInfoDTO>> GetApplicationListAsync(bool active, AppSubscriptionModeEnum subscriptionMode) {
            List<App> apps = (await _appRepository.FindAllAsync(a => a.Active == active && a.AppSubscriptionMode == (int)subscriptionMode && a.Constructed == true && a.Deleted == false && a.AppScope == (int)AppScopeEnum.Public, false)).ToList();
            List<AppInfoDTO> appInfoList = new List<AppInfoDTO>();
            foreach(App item in apps) {
                AppInfoDTO dto = AppInfoDTO.MapFromApp(item);
                appInfoList.Add(dto);
            }
            return appInfoList;
        }

        ///<inheritdoc/>
        public async Task<List<AppServiceDTO>> GetServiceNameByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {

            //// get app services
            // List<AppService> appservices = await _appServiceRep.GetAppServicesDetailsAsync(appId);
            List<AppService> appservices = await _appServiceDS.GetAppServicesDetailsAsync(appId);

            List<AppServiceDTO> appServiceDTOs = new List<AppServiceDTO>();
            for(int i = 0; i < appservices.Count; i++) {

                AppServiceDTO appServiceDTO = new AppServiceDTO();
                appServiceDTO.Name = appservices[i].Name;
                appServiceDTO.ServiceKey = appservices[i].ServiceKey;
                appServiceDTO.Active = appservices[i].Active;
                appServiceDTOs.Add(appServiceDTO);
            }
            return appServiceDTOs;
        }

        ///<inheritdoc/>
        public async Task<AppAndServiceDTO> GetAppAndServiceDetailsByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {

            AppAndServiceDTO appAndServiceDTO = new AppAndServiceDTO();

            appAndServiceDTO.AppDetailDTO = new AppDetailDTO();

            // Get app details.
            App app = await _appRepository.GetAsync(appId);

            appAndServiceDTO.AppDetailDTO.ID = app.ID;
            appAndServiceDTO.AppDetailDTO.AppID = app.ID;
            appAndServiceDTO.AppDetailDTO.AppKey = app.AppKey;
            appAndServiceDTO.AppDetailDTO.Active = app.Active;
            appAndServiceDTO.AppDetailDTO.InactiveComment = app.InactiveComment;
            appAndServiceDTO.AppDetailDTO.TenantId = app.TenantId;
            appAndServiceDTO.AppDetailDTO.ThemeId = app.ThemeId;
            appAndServiceDTO.AppDetailDTO.IdentityNumber = app.IdentityNumber;
            appAndServiceDTO.AppDetailDTO.Name = app.Name;
            appAndServiceDTO.AppDetailDTO.UpdatedBy = app.UpdatedBy;
            appAndServiceDTO.AppDetailDTO.UpdatedOn = app.UpdatedOn;

            //// get app services
            // List<AppService> appservices = await _appServiceRep.GetAppServicesDetailsAsync(appId);
            List<AppService> appservices = await _appServiceDS.GetAppServicesDetailsAsync(appId);

            appAndServiceDTO.AppServiceDTOs = new List<AppServiceDTO>();
            for(int i = 0; i < appservices.Count; i++) {

                AppServiceDTO appServiceDTO = new AppServiceDTO();
                appServiceDTO.Name = appservices[i].Name;
                appServiceDTO.ServiceKey = appservices[i].ServiceKey;
                appServiceDTO.Active = appservices[i].Active;
                appAndServiceDTO.AppServiceDTOs.Add(appServiceDTO);
            }

            // Get Attribute list
            appAndServiceDTO.AppServiceModelList = await GetAppServiceListByAppIdAsync(appId, true, false, false);

            return appAndServiceDTO;
        }

        private async Task<List<AppServiceDTO>> GetAppServiceListByAppIdAsync(Guid appId, bool includeAttributeDetail, bool onlyActive, bool includeDeleted) {
            // List<AppServiceDTO> appServiceList = await _appServiceRep.GetAppServiceListByAppIdAsync(appId, onlyActive, includeDeleted);
            List<AppServiceDTO> appServiceList = await _appServiceDS.GetAppServiceListByAppIdAsync(appId, onlyActive, includeDeleted);
            if(includeAttributeDetail && appServiceList != null) {

                for(int i = 0; i < appServiceList.Count; i++) {
                    appServiceList[i].AppServiceAttributeList.AddRange(_appServiceAttributeDS.GetAppServiceAttributeListByServiceId(appServiceList[i].ID, onlyActive, includeDeleted));
                }
            }
            return appServiceList;
        }

        #region Get AppService, BusinessName, ServiceName For Publisher

        public async Task<List<AppServiceDTO>> GetAppServiceAsync(List<PubBusinessSubsPlanAppServiceDTO> pubBusinessSubsPlanAppServiceDTO, CancellationToken token = default(CancellationToken)) {

            List<AppService> appservices = await _appServiceDS.GetAppServiceAsync(pubBusinessSubsPlanAppServiceDTO, token);

            List<AppServiceDTO> dto = new List<AppServiceDTO>();
            for(int i = 0; i < appservices.Count; i++) {

                AppServiceDTO appServiceDTO = new AppServiceDTO();
                appServiceDTO.Name = appservices[i].Name;
                appServiceDTO.ServiceKey = appservices[i].ServiceKey;
                appServiceDTO.Active = appservices[i].Active;
                dto.Add(appServiceDTO);
            }
            return dto;
        }

        
         public async Task<List<AppServiceDTO>> GetAppServiceAttributeListAsync(List<PubBusinessSubsPlanAppServiceDTO> pubBusinessSubsPlanAppServiceDTO, CancellationToken token = default(CancellationToken)) {

            List<AppService> appservices = await _appServiceDS.GetAppServiceAsync(pubBusinessSubsPlanAppServiceDTO, token);
            List<AppServiceAttribute> appServicesAttributeList = await _appServiceAttributeDS.GetAppServiceAttributeListAsync(pubBusinessSubsPlanAppServiceDTO, token);

            List<AppServiceDTO> dto = new List<AppServiceDTO>();
            for(int i = 0; i < appservices.Count; i++) {

                AppServiceDTO appServiceDTO = new AppServiceDTO();

                for(int j = 0; j < appServicesAttributeList.Count; j++) {

                    if(appservices[i].ID == appServicesAttributeList[j].AppServiceId) {
                       
                        appServiceDTO.Name = appservices[i].Name;
                        appServiceDTO.ServiceKey = appservices[i].ServiceKey;
                        appServiceDTO.Active = appservices[i].Active;

                        AppServiceAttributeDTO ob = new AppServiceAttributeDTO();
                        ob.Name = appServicesAttributeList[j].Name;
                        ob.AttributeKey = appServicesAttributeList[j].AttributeKey;
                        ob.Active = appServicesAttributeList[j].Active;

                        appServiceDTO.AppServiceAttributeList.Add(ob); 
                      
                    }                    
                }
                dto.Add(appServiceDTO);
            }

            return dto;
        }

        ///<inheritdoc/>
        public async Task<List<string>> GetBusinessNameByAppIdAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {
            List<string> businessList = await _tenantDS.GetBusinessNameByAppIdAsync(appId, publisherTenantId, token);
            return businessList;
        }



        #endregion Get AppService, BusinessName, ServiceName For Publisher

        ///<inheritdoc/>
        public async Task<List<string>> GetBusinessNameByAppIdPlatAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            List<string> businessList = await _tenantDS.GetBusinessNameByAppIdPlatAsync(appId, token);
            return businessList;
        }



        #endregion Get

        #region Update

        #region UpdateApp From Platform

        ///<inheritdoc/>
        public async Task UpdateAppAndServiceAsync(AppAndServiceDTO appAndServiceDTO) {

            await UpdateAppAndServiceAndThumbnailAsync(appAndServiceDTO);
        }

        #region  Update App

        private async Task UpdateAppAndServiceAndThumbnailAsync(AppAndServiceDTO appAndServiceDTO) {

            // Update application
            await UpdateAppAsync(appAndServiceDTO.AppDetailDTO);

            // Update App Service
            await UpdateAppServiceInfo(appAndServiceDTO.AppServiceModelList, appAndServiceDTO.AppDetailDTO.ID);

            // Update Thumbnail
            await UpdateThumbnailAsync(appAndServiceDTO);

            _unitOfWork.SaveAll();
        }

        private async Task UpdateAppAsync(AppDetailDTO appDTO) {

            // Add apllication
            if(appDTO != null) {
                // Map entity  
                // App app = _mapper.Map<App>(appDTO);
                App app = _appRepository.Get(appDTO.ID);
                app.ThemeId = appDTO.ThemeId;
                app.Name = appDTO.Name;
                app.Active = appDTO.Active;
                app.InactiveComment = appDTO.InactiveComment;
                UpdateSystemFieldsByOpType(app, OperationType.Update);

                // Validate entity 
                ValidateOnAddAndUpdate(app);

                // Save Application  
                await _appRepository.UpdateAsync(app, app.ID);
            }
        }
        #endregion  Update App

        #region  Update AppService

        private async Task UpdateAppServiceInfo(List<AppServiceDTO> updatedAppServiceList, Guid appId) {
            //List<AppService> savedAppService = await _appServiceRep.GetAppServicesDetailsAsync(appId);
            List<AppService> savedAppService = await _appServiceDS.GetAppServicesDetailsAsync(appId);

            if(updatedAppServiceList != null) {
                foreach(AppServiceDTO updatedAppService in updatedAppServiceList) {
                    AppService changedAppService = savedAppService.FirstOrDefault(i => i.ID.Equals(updatedAppService.ID));

                    if(changedAppService != null && changedAppService.Active != updatedAppService.Active) {
                        changedAppService.Active = updatedAppService.Active;
                        _appServiceDS.UpdateSystemFieldsByOpType(changedAppService, OperationType.Update);
                        _appServiceDS.Update(changedAppService, changedAppService.ID);
                    }

                    List<AppServiceAttribute> savedAppServiceAttributeList = await _appServiceAttributeDS.GetAppServiceAttributeListByServiceIdAsync(updatedAppService.ID, true);

                    foreach(AppServiceAttributeDTO updatedAppServiceAttribute in updatedAppService.AppServiceAttributeList) {

                        AppServiceAttribute savedAppServiceAttribute = savedAppServiceAttributeList.FirstOrDefault(i => i.ID.Equals(updatedAppServiceAttribute.ID));
                        if(savedAppServiceAttribute != null && savedAppServiceAttribute.Active != updatedAppServiceAttribute.Active) {
                            savedAppServiceAttribute.Active = updatedAppServiceAttribute.Active;
                            _appServiceAttributeDS.UpdateSystemFieldsByOpType(savedAppServiceAttribute, OperationType.Update);
                            _appServiceAttributeDS.Update(savedAppServiceAttribute, savedAppServiceAttribute.ID);
                        }
                    }
                }
            }
        }

        #endregion  Update AppService

        #region Add Update Thumbnail

        private async Task UpdateThumbnailAsync(AppAndServiceDTO appAndServiceDTO) {

            if(appAndServiceDTO.ThumbnailAddAndUpdateDTO != null) {
                if(appAndServiceDTO.ThumbnailAddAndUpdateDTO.OperationType == (int)OperationType.Add) {
                    _entityThumbnailDS.AddThumbnail(appAndServiceDTO.ThumbnailAddAndUpdateDTO);
                }
                else if(appAndServiceDTO.ThumbnailAddAndUpdateDTO.OperationType == (int)OperationType.Update) {
                    _entityThumbnailDS.UpdateThumbnail(appAndServiceDTO.ThumbnailAddAndUpdateDTO);
                }
            }
            else {
                if(appAndServiceDTO.AppDetailDTO != null) {
                    if(appAndServiceDTO.AppDetailDTO.ThumbnailId != null) {
                        _entityThumbnailDS.DeleteThumbnail((Guid)appAndServiceDTO.AppDetailDTO.ThumbnailId);
                    }
                }
            }
        }

        #endregion Add Update Thumbnail

        #endregion UpdateApp From Platform

        #endregion Update

        #region VALIDATION

        private bool ValidateOnAddAndUpdate(App entity) {
            IList<EwpErrorData> brokenRules = new List<EwpErrorData>();

            // Validate app entity field values.
            entity.Validate(out brokenRules);

            // Raise validation exception if any validation is failed.
            ExceptionUtils.RaiseValidationException(App.EntityName, brokenRules);

            return true;
        }

        #endregion VALIDATION







    }
}

