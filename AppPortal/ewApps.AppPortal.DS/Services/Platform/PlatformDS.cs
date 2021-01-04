using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.DBQuery;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// 
    /// </summary>
    public class PlatformDS:BaseDS<Entity.Platform>, IPlatformDS {

        #region Local member

        IPlatformRepository _platformRepository;
        IEntityThumbnailDS _entityThumbnailDS;
        IPublisherDS _publisherDS;
        IQPlatformAndUserDS _qPlatformDS;
        AppPortalAppSettings _appPortalAppSettings;
        IUnitOfWork _unitOfWork;
        IUserSessionManager _userSessionManager;

        #endregion 

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platformRepository"></param>
        /// <param name="entityThumbnailDS"></param>
        public PlatformDS(IPlatformRepository platformRepository, IEntityThumbnailDS entityThumbnailDS, IPublisherDS publisherDS, IOptions<AppPortalAppSettings> appPortalAppSettings, IUnitOfWork unitOfWork, IQPlatformAndUserDS qPlatformDS, IUserSessionManager userSessionManager) : base(platformRepository) {
            _platformRepository = platformRepository;
            _entityThumbnailDS = entityThumbnailDS;
            _publisherDS = publisherDS;
            _appPortalAppSettings = appPortalAppSettings.Value;
            _unitOfWork = unitOfWork;
            _qPlatformDS = qPlatformDS;
            _userSessionManager = userSessionManager;
        }

        #endregion

        #region GET Platform Branding

        /// <summary>
        /// get platform branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>PlatformBranding Model</returns>
        public async Task<PlatformBrandingDQ> GetPlatformBrandingAsync(Guid tenantId, Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            // PlatformBrandingDTO platformBrandingDTO = await _platformRepository.GetPlatformBrandingAsync(tenantId, appId, cancellationToken);
            PlatformBrandingDQ platformBrandingDQ = await _qPlatformDS.GetPlatformBrandingAsync(tenantId, appId, cancellationToken);
            // Get platform thumbnail Model
            platformBrandingDQ.ThumbnailAddUpdateModel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(tenantId);
            return platformBrandingDQ;
        }

        #endregion

        #region Theme
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

        #region Update Platform Branding
        ///<inheritdoc/>
        public void UpdatePlatformBranding(PlatformBrandingDQ platformBrandingDQ) {

            UserSession session = _userSessionManager.GetSession();

            //Get Platform Entity Information
            Entity.Platform platform = _platformRepository.Get(platformBrandingDQ.ID);
            platform.Name = platformBrandingDQ.Name;
            platform.Copyright = platformBrandingDQ.Copyright;
            platform.PoweredBy = platformBrandingDQ.PoweredBy;
            if(platformBrandingDQ.ThumbnailAddUpdateModel != null) {
                platform.LogoThumbnailId = platformBrandingDQ.ThumbnailAddUpdateModel.ID;
            }
            else {
                platform.LogoThumbnailId = Guid.Empty;
            }

            // platform Thumbnail Update
            if(platformBrandingDQ.ThumbnailAddUpdateModel != null) {
                if(platformBrandingDQ.ThumbnailAddUpdateModel.OperationType == (int)OperationType.Add) {
                    _entityThumbnailDS.AddThumbnail(platformBrandingDQ.ThumbnailAddUpdateModel);
                }
                else if(platformBrandingDQ.ThumbnailAddUpdateModel.OperationType == (int)OperationType.Update) {
                    _entityThumbnailDS.UpdateThumbnail(platformBrandingDQ.ThumbnailAddUpdateModel);
                }
            }
            else {
                if(platformBrandingDQ.ThumbnailId != null && platformBrandingDQ.ThumbnailId != Guid.Empty) {
                    _entityThumbnailDS.DeleteThumbnail(platformBrandingDQ.ThumbnailId);
                }
            }

            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "branding/platformupdate/";
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = platformBrandingDQ;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Put;
            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            List<Guid?> publiserTenantIdList = httpRequestProcessor.Execute<List<Guid?>>(requestOptions, false);

            //foreach(Guid? item in publiserTenantIdList) {}
            for(var i = 0; i < publiserTenantIdList.Count; i++) {
                Entity.Publisher publisher = _publisherDS.FindAsync(p => p.TenantId == publiserTenantIdList[i]).Result;
                if(publisher != null) {
                    //if(publisher.ApplyPoweredBy == true)
                        publisher.PoweredBy = platformBrandingDQ.PoweredBy;
                    if(publisher.CustomizedCopyright == false)
                        publisher.Copyright = platformBrandingDQ.Copyright;
                    if(publisher.CustomizedLogoThumbnail == false) {
                        if(platformBrandingDQ.ThumbnailAddUpdateModel != null) {
                            publisher.LogoThumbnailId = platformBrandingDQ.ThumbnailAddUpdateModel.ID;
                        }
                        else {
                            publisher.LogoThumbnailId = Guid.Empty;
                        }
                    }
                }
            }

            UpdateSystemFieldsByOpType(platform, OperationType.Update);
            Update(platform, platform.ID);
            _unitOfWork.SaveAll();

        }

        #endregion


        #region GET Platform connector list

        /// <summary>
        /// get platform connector list
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<ConnectorDQ>> GetPlatformConnectorListAsync(CancellationToken token = default(CancellationToken)) {
            // List<PlatformConnectorDQ> platformConnectorDQ = await _connectorDS.GetConnctorListAsync();
            UserSession userSession = _userSessionManager.GetSession();


            string baseuri = _appPortalAppSettings.BusinessEntityApiUrl;
            string requesturl = "ERPConnector/getconnector";
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = null;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Get;
            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            List<ConnectorDQ> platformConnectorDQ = httpRequestProcessor.Execute<List<ConnectorDQ>>(requestOptions, false);

            return platformConnectorDQ;
        }

        #endregion
    }
}