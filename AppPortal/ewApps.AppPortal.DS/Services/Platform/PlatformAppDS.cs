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
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.ExceptionService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This class implements standard business logic and operations for app entity.
    /// </summary>
    public class PlatformAppDS:IPlatformAppDS {

        #region Local Member

        IPublisherAccess _appAccess;
        IQPlatformAndUserDS _qPlatformDS;
        AppPortalAppSettings _appSettings;
        IPublisherAppSettingDS _publisherAppSettingDS;
        IUnitOfWork _unitOfWork;
        IUserSessionManager _userSessionManager;
        IEntityThumbnailDS _entityThumbnailDS;
        IPublisherRepository _publisherRepository;

        #endregion Local Member

        #region Constructor 

        /// <summary>
        /// Initializing local variables
        /// </summary>
        /// <param name="appAccess">The application access.</param>
        /// <param name="qPlatformDS">The q platform ds.</param>
        /// <param name="appSettings">The application settings.</param>
        /// <param name="publisherAppSettingDS">The publisher application setting ds.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="userSessionManager">The user session manager.</param>
        /// <param name="entityThumbnailDS">The entity thumbnail ds.</param>
        public PlatformAppDS(IPublisherAccess appAccess, IQPlatformAndUserDS qPlatformDS, IOptions<AppPortalAppSettings> appSettings, IPublisherAppSettingDS publisherAppSettingDS, 
            IUnitOfWork unitOfWork , IUserSessionManager userSessionManager, IEntityThumbnailDS entityThumbnailDS, IPublisherRepository publisherRepository) {
            _appAccess = appAccess;
            _qPlatformDS = qPlatformDS;
            _appSettings = appSettings.Value;
            _publisherAppSettingDS = publisherAppSettingDS;
            _unitOfWork = unitOfWork;
            _userSessionManager = userSessionManager;
            _entityThumbnailDS = entityThumbnailDS;
            _publisherRepository = publisherRepository;
        }

        #endregion Constructor        

        #region Get

        ///<inheritdoc/>
        public async Task<List<AppDetailDTO>> GetAppDetailsAsync() {
            return await _qPlatformDS.GetAppDetailsAsync();
        }

        ///<inheritdoc/>
        public async Task<AppDetailDTO> GetAppDetailsByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            return await _qPlatformDS.GetAppDetailsByAppIdAsync(appId, token);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<StringDTO>> GetPublisherListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            return await _publisherRepository.GetPublisherListByAppIdAsync(appId, token);
        }

        ///<inheritdoc/>
        public async Task<List<string>> GetBusinessNameByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            return await GetBusinessNameAsync(appId,token);
        }

        /// <Inheritdoc/>
        private async Task<List<string>> GetBusinessNameAsync(Guid appId, CancellationToken token = default(CancellationToken)) {

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            string methodUrl = "app/businessname/" + appId; 
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUrl, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParams);

            ServiceExecutor serviceExecutor = new ServiceExecutor(_appSettings.AppMgmtApiUrl);
            return await serviceExecutor.ExecuteAsync<List<string>>(requestOptions, false);
        }



        ///<inheritdoc/>
        public async Task<List<AppServiceDTO>> GetAppServiceNameAsync(Guid appId, CancellationToken token = default(CancellationToken)) {

            return await GetAppServiceByAppId(appId, token);
           
        }

        ///<inheritdoc/>
        public async Task<AppAndServiceDTO> GetAppDetailsWithServicesAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            return await GetAppDetailsWithServices(appId, token);
        }

        private async Task<AppAndServiceDTO> GetAppDetailsWithServices(Guid appId, CancellationToken token = default(CancellationToken)) {

            AppAndServiceDTO appAndServiceDTO = new AppAndServiceDTO();

            //// Get app details and services
            appAndServiceDTO = await GetAppDetailsApi(appId);

            // Get thumbnail details
            appAndServiceDTO.ThumbnailAddAndUpdateDTO = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(appId);

            return appAndServiceDTO;
        }

        private async Task<AppAndServiceDTO> GetAppDetailsApi(Guid appId) {

            UserSession session = _userSessionManager.GetSession();

            string baseuri = _appSettings.AppMgmtApiUrl;
            string requesturl = "app/appdetails/" + appId;

            AppAndServiceDTO appAndServiceDTO = new AppAndServiceDTO();

            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = null;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Get;

            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            appAndServiceDTO = await httpRequestProcessor.ExecuteAsync<AppAndServiceDTO>(requestOptions, false);

            return appAndServiceDTO;
        }

        /// <Inheritdoc/>
        private async Task<List<AppServiceDTO>> GetAppServiceByAppId(Guid appId, CancellationToken token = default(CancellationToken)) {

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            string methodUrl = "app/servicename/"+ appId;
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUrl, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParams);

            ServiceExecutor serviceExecutor = new ServiceExecutor(_appSettings.AppMgmtApiUrl);
            return await serviceExecutor.ExecuteAsync<List<AppServiceDTO>>(requestOptions, false);
        }

        #endregion Get

        #region Security

        ///<inheritdoc/>
        public IEnumerable<bool> GetLoginUsersAppPermission() {
            return _appAccess.AccessList(Guid.Empty);
        }

        // Check the permission for update operation.
        private void CheckSecurityOnUpdate() {
            if(!_appAccess.CheckAccess((int)OperationType.Update, Guid.Empty)) {

                // Raise security exception
                List<EwpErrorData> errorDataList = new List<EwpErrorData>() {
            new EwpErrorData() {
              ErrorSubType = (int)SecurityErrorSubType.Update,
              Message = string.Format("", "PlatformApp")
              }
            };

                throw new EwpSecurityException("", errorDataList);
            }
        }


        #endregion Security

        #region Update

        ///<inheritdoc/>
        public async Task UpdateAppAsync(AppAndServiceDTO appAndServiceDTO) {

            CheckSecurityOnUpdate();
            await UpdateAppAndServiceAsync(appAndServiceDTO);
            await _publisherAppSettingDS.UpdatePublisherAppSettingFromAppAsync(appAndServiceDTO);
            _unitOfWork.SaveAll();
        }       

        private async Task<ResponseModelDTO> UpdateAppAndServiceAsync(AppAndServiceDTO appAndServiceDTO) {

            UserSession session = _userSessionManager.GetSession();

            string baseuri = _appSettings.AppMgmtApiUrl;
            string requesturl = "app/update";

            ResponseModelDTO responseModelDTO = new ResponseModelDTO();

            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = appAndServiceDTO;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Put;

            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            responseModelDTO = await httpRequestProcessor.ExecuteAsync<ResponseModelDTO>(requestOptions, false);

            return responseModelDTO;

        }

        #endregion Update

        
             

    }
}

