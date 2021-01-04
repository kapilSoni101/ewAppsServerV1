using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public class AppPortalDS :IAppPortalDS {


        #region Local Member
        IUserSessionManager _userSessionManager;
        IEntityThumbnailDS _entityThumbnailDS;
        AppPortalAppSettings _appPortalAppSettings;
        IQAppPortalDS _qAppPortalDS;
        IUnitOfWork _unitOfWork;
        #endregion


        #region Constructor 

        public AppPortalDS(IUserSessionManager userSessionManager, IEntityThumbnailDS entityThumbnailDS, IOptions<AppPortalAppSettings> appPortalAppSettings , IQAppPortalDS qAppPortalDS, IUnitOfWork unitOfWork ) {
            _userSessionManager = userSessionManager;
            _entityThumbnailDS = entityThumbnailDS;
            _appPortalAppSettings = appPortalAppSettings.Value;
            _qAppPortalDS = qAppPortalDS;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region GET/Update Ship-App Branding

        /// <summary>
        /// get Ship-App branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>AppPortalBrandingDQ Model</returns>
        public async Task<AppPortalBrandingDQ> GetShipAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            AppPortalBrandingDQ appPortalBranidngDQ = await _qAppPortalDS.GetShipAppBrandingAsync(tenantId, appId, cancellationToken);
            // Get platform thumbnail Model
            appPortalBranidngDQ.ThumbnailAddUpdateModel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(tenantId);
            return appPortalBranidngDQ;
        }

        /// <summary>
        /// Update Ship-App branding details
        /// </summary>
        /// <param name="appPortalBrandingDQ"></param>
        public async Task UpdateShipAppBrandingAsync(AppPortalBrandingDQ appPortalBrandingDQ) {

            UserSession session = _userSessionManager.GetSession();
            
            // Update Thumbnail 
            //if(appPortalBrandingDQ.ThumbnailAddUpdateModel != null) {
            //    if(appPortalBrandingDQ.ThumbnailAddUpdateModel.OperationType == (int)OperationType.Add) {
            //        _entityThumbnailDS.AddThumbnail(appPortalBrandingDQ.ThumbnailAddUpdateModel);
            //    }
            //    else if(appPortalBrandingDQ.ThumbnailAddUpdateModel.OperationType == (int)OperationType.Update) {
            //        _entityThumbnailDS.UpdateThumbnail(appPortalBrandingDQ.ThumbnailAddUpdateModel);
            //    }
            //}
            //else {
            //    if(appPortalBrandingDQ.ThumbnailId != null && appPortalBrandingDQ.ThumbnailId != Guid.Empty) {
            //        ThumbnailAddAndUpdateDTO thumbnailmodel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(appPortalBrandingDQ.ThumbnailId);
            //        if(thumbnailmodel != null && appPortalBrandingDQ.TenantId == thumbnailmodel.ThumbnailOwnerEntityId)
            //            _entityThumbnailDS.DeleteThumbnail(appPortalBrandingDQ.ThumbnailId);
            //    }
            //}

            // API Call for Upadate other values
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "branding/shipappupdate/";
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = appPortalBrandingDQ;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Put;
            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            httpRequestProcessor.Execute<bool>(requestOptions, false);
            
        }

        #endregion

        #region GET/Update Pay-App Branding

        /// <summary>
        /// get Pay-App branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>PublisherBrandingDQ Model</returns>
        public async Task<AppPortalBrandingDQ> GetPayAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            AppPortalBrandingDQ appPortalBranidngDQ = await _qAppPortalDS.GetPayAppBrandingAsync(tenantId, appId, cancellationToken);
            // Get platform thumbnail Model
            appPortalBranidngDQ.ThumbnailAddUpdateModel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(tenantId);
            return appPortalBranidngDQ;
        }

       

        /// <summary>
        /// Update Pay-App branding details
        /// </summary>
        /// <param name="appPortalBrandingDQ"></param>
        public async Task UpdatePayAppBrandingAsync(AppPortalBrandingDQ appPortalBrandingDQ) {

            UserSession session = _userSessionManager.GetSession();

            // Update Thumbnail 
            //if(appPortalBrandingDQ.ThumbnailAddUpdateModel != null) {
            //    if(appPortalBrandingDQ.ThumbnailAddUpdateModel.OperationType == (int)OperationType.Add) {
            //        _entityThumbnailDS.AddThumbnail(appPortalBrandingDQ.ThumbnailAddUpdateModel);
            //    }
            //    else if(appPortalBrandingDQ.ThumbnailAddUpdateModel.OperationType == (int)OperationType.Update) {
            //        _entityThumbnailDS.UpdateThumbnail(appPortalBrandingDQ.ThumbnailAddUpdateModel);
            //    }
            //}
            //else {
            //    if(appPortalBrandingDQ.ThumbnailId != null && appPortalBrandingDQ.ThumbnailId != Guid.Empty) {
            //        ThumbnailAddAndUpdateDTO thumbnailmodel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(appPortalBrandingDQ.ThumbnailId);
            //        if(thumbnailmodel != null && appPortalBrandingDQ.TenantId == thumbnailmodel.ThumbnailOwnerEntityId)
            //            _entityThumbnailDS.DeleteThumbnail(appPortalBrandingDQ.ThumbnailId);
            //    }
            //}

            // API Call for Upadate other values
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "branding/payappupdate/";
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = appPortalBrandingDQ;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Put;
            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            httpRequestProcessor.Execute<bool>(requestOptions, false);

        }

        #endregion

        #region GET/Update Cust-App Branding

        /// <summary>
        /// get Cust-App branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>PublisherBrandingDQ Model</returns>
        public async Task<AppPortalBrandingDQ> GetCustAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            AppPortalBrandingDQ appPortalBranidngDQ = await _qAppPortalDS.GetCustAppBrandingAsync(tenantId, appId, cancellationToken);
            // Get platform thumbnail Model
            appPortalBranidngDQ.ThumbnailAddUpdateModel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(tenantId);
            return appPortalBranidngDQ;
        }



        /// <summary>
        /// Update Cust-App branding details
        /// </summary>
        /// <param name="appPortalBrandingDQ"></param>
        public async Task UpdateCustAppBrandingAsync(AppPortalBrandingDQ appPortalBrandingDQ) {

            UserSession session = _userSessionManager.GetSession();

            // Update Thumbnail 
            //if(appPortalBrandingDQ.ThumbnailAddUpdateModel != null) {
            //    if(appPortalBrandingDQ.ThumbnailAddUpdateModel.OperationType == (int)OperationType.Add) {
            //        _entityThumbnailDS.AddThumbnail(appPortalBrandingDQ.ThumbnailAddUpdateModel);
            //    }
            //    else if(appPortalBrandingDQ.ThumbnailAddUpdateModel.OperationType == (int)OperationType.Update) {
            //        _entityThumbnailDS.UpdateThumbnail(appPortalBrandingDQ.ThumbnailAddUpdateModel);
            //    }
            //}
            //else {
            //    if(appPortalBrandingDQ.ThumbnailId != null && appPortalBrandingDQ.ThumbnailId != Guid.Empty) {
            //        ThumbnailAddAndUpdateDTO thumbnailmodel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(appPortalBrandingDQ.ThumbnailId);
            //        if(thumbnailmodel != null && appPortalBrandingDQ.TenantId == thumbnailmodel.ThumbnailOwnerEntityId)
            //            _entityThumbnailDS.DeleteThumbnail(appPortalBrandingDQ.ThumbnailId);
            //    }
            //}

            // API Call for Upadate other values
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "branding/custappupdate/";
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = appPortalBrandingDQ;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Put;
            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            httpRequestProcessor.Execute<bool>(requestOptions, false);

        }

        #endregion


        #region GET/Update Vend-App Branding

        /// <summary>
        /// get Cust-App branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>PublisherBrandingDQ Model</returns>
        public async Task<AppPortalBrandingDQ> GetVendAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            AppPortalBrandingDQ appPortalBranidngDQ = await _qAppPortalDS.GetVendAppBrandingAsync(tenantId, appId, cancellationToken);
            // Get platform thumbnail Model
            appPortalBranidngDQ.ThumbnailAddUpdateModel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(tenantId);
            return appPortalBranidngDQ;
        }



        /// <summary>
        /// Update Cust-App branding details
        /// </summary>
        /// <param name="appPortalBrandingDQ"></param>
        public async Task UpdateVendAppBrandingAsync(AppPortalBrandingDQ appPortalBrandingDQ) {

            UserSession session = _userSessionManager.GetSession();

            

            // API Call for Upadate other values
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "branding/vendappupdate/";
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = appPortalBrandingDQ;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Put;
            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            httpRequestProcessor.Execute<bool>(requestOptions, false);

        }

        #endregion

        #region Theme

        /// <summary>
        /// Get Theme name and Key
        /// </summary>
        /// <returns></returns>
        public async Task<List<ThemeResponseDTO>> GetThemeNameAndThemeKey() {

            UserSession session = _userSessionManager.GetSession();

            // Preparing api calling process model.
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "theme/getthemenameandthemekey";
            List<ThemeResponseDTO> themeResponseDTO = new List<ThemeResponseDTO>();
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = null;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Get;
            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            themeResponseDTO = await httpRequestProcessor.ExecuteAsync<List<ThemeResponseDTO>>(requestOptions, false);

            return themeResponseDTO;

        }

        #endregion



    }
}
