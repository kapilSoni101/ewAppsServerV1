using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.DBQuery;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.ExceptionService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// 
    /// </summary>
    public class PublisherDS:BaseDS<Entity.Publisher>, IPublisherDS {

        #region Local member
        IQPublisherAndUserDS _qPublisherDS;
        IEntityThumbnailDS _entityThumbnailDS;
        IPublisherAddressDS _publisherAddressDS;
        IPublisherRepository _publisherRepository;
        AppPortalAppSettings _appPortalAppSettings;
        IUnitOfWork _unitOfWork;
        IUserSessionManager _userSessionManager;
        IPublisherAccess _entityAccess;
        private IUniqueIdentityGeneratorDS _uniqueIdentityGeneratorDS;

        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="qPublisherDS"></param>
        /// <param name="entityThumbnailDS"></param>
        /// <param name="appPortalAppSettings"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="publisherRepository"></param>
        public PublisherDS(IQPublisherAndUserDS qPublisherDS, IEntityThumbnailDS entityThumbnailDS, IPublisherAddressDS publisherAddressDS, IOptions<AppPortalAppSettings> appPortalAppSettings, IUnitOfWork unitOfWork, IPublisherRepository publisherRepository, IUserSessionManager userSessionManager, IPublisherAccess entityAccess, IUniqueIdentityGeneratorDS uniqueIdentityGeneratorDS) : base(publisherRepository) {
            _qPublisherDS = qPublisherDS;
            _entityThumbnailDS = entityThumbnailDS;
            _publisherAddressDS = publisherAddressDS;
            _publisherRepository = publisherRepository;
            _appPortalAppSettings = appPortalAppSettings.Value;
            _unitOfWork = unitOfWork;
            _userSessionManager = userSessionManager;
            _entityAccess = entityAccess;
            _uniqueIdentityGeneratorDS = uniqueIdentityGeneratorDS;
        }
        #endregion

        #region GET Branding

        /// <summary>
        /// get publisher branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>PublisherBrandingDQ Model</returns>
        public async Task<PublisherBrandingDQ> GetPublisherBrandingAsync(Guid tenantId, Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            PublisherBrandingDQ publisherBrandingDQ = await _qPublisherDS.GetPublisherBrandingAsync(tenantId, appId, cancellationToken);
            // Get platform thumbnail Model
            // publisherBrandingDQ.ThumbnailAddUpdateModel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(tenantId);
            publisherBrandingDQ.ThumbnailAddUpdateModel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(publisherBrandingDQ.ThumbnailId);
            return publisherBrandingDQ;
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

        #region Update branding detail

        /// <summary>
        /// Update Publisher branding details
        /// </summary>
        /// <param name="publisherBrandingDQ"></param>
        public async Task UpdatePublisherBranding(PublisherBrandingDQ publisherBrandingDQ) {

            UserSession session = _userSessionManager.GetSession();

            //Get Publisher Entity Information
            Entity.Publisher publisher = _publisherRepository.Get(publisherBrandingDQ.ID);
            publisher.Name = publisherBrandingDQ.Name;
            if(publisher.CanUpdateCopyright == true) {
                publisher.Copyright = publisherBrandingDQ.Copyright;
                publisher.CustomizedCopyright = true;
            }
            publisher.CustomizedLogoThumbnail = publisherBrandingDQ.CustomizedLogoThumbnail;
            //if(publisher.ApplyPoweredBy == false) {
            //    publisher.PoweredBy = null;
            //}
            //else {
            //    publisher.PoweredBy = publisherBrandingDQ.PoweredBy;
            //}
            publisher.PoweredBy = publisherBrandingDQ.PoweredBy;

            // Update Thumbnail 
            if(publisherBrandingDQ.ThumbnailAddUpdateModel != null) {
                if(publisherBrandingDQ.ThumbnailAddUpdateModel.OperationType == (int)OperationType.Add) {
                    _entityThumbnailDS.AddThumbnail(publisherBrandingDQ.ThumbnailAddUpdateModel);
                }
                else if(publisherBrandingDQ.ThumbnailAddUpdateModel.OperationType == (int)OperationType.Update) {
                    _entityThumbnailDS.UpdateThumbnail(publisherBrandingDQ.ThumbnailAddUpdateModel);
                }
            }
            else {
                if(publisherBrandingDQ.ThumbnailId != null && publisherBrandingDQ.ThumbnailId != Guid.Empty) {
                    ThumbnailAddAndUpdateDTO thumbnailmodel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(publisherBrandingDQ.ThumbnailId);
                    if(thumbnailmodel != null && publisherBrandingDQ.TenantId == thumbnailmodel.ThumbnailOwnerEntityId)
                        _entityThumbnailDS.DeleteThumbnail(publisherBrandingDQ.ThumbnailId);
                }
            }
            if(publisherBrandingDQ.CustomizedLogoThumbnail == true) {
                publisher.LogoThumbnailId = publisherBrandingDQ.ThumbnailId;
            }
            else {
                publisher.LogoThumbnailId = publisherBrandingDQ.PlatformThumbnailId;
            }

            // API Call for Upadate other values
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "branding/publisherupdate/";
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = publisherBrandingDQ;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Put;
            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            httpRequestProcessor.Execute<bool>(requestOptions, false);

            UpdateSystemFieldsByOpType(publisher, OperationType.Update);
            Update(publisher, publisher.ID);
            _unitOfWork.SaveAll();

        }

        #endregion

        #region Security

        ///<inheritdoc/>
        public IEnumerable<bool> GetLoginUsersAppPermission() {
            return _entityAccess.AccessList(Guid.Empty);
        }

        ///<inheritdoc/>
        private void CheckSecurityOnAdding() {

            if(!_entityAccess.CheckAccess((int)OperationType.Add, Guid.Empty)) {

                // Raise security exception
                List<EwpErrorData> errorDataList = new List<EwpErrorData>() {
            new EwpErrorData() {
              ErrorSubType = (int)SecurityErrorSubType.Add,
              Message = string.Format("", "Publisher")
              }
            };

                throw new EwpSecurityException("", errorDataList);
            }
        }

        ///<inheritdoc/>
        private void CheckSecurityOnUpdating() {
            if(!_entityAccess.CheckAccess((int)OperationType.Update, Guid.Empty)) {

                // Raise security exception
                List<EwpErrorData> errorDataList = new List<EwpErrorData>() {
            new EwpErrorData() {
              ErrorSubType = (int)SecurityErrorSubType.Update,
              Message = string.Format("", "Publisher")
              }
            };

                throw new EwpSecurityException("", errorDataList);
            }
        }

        ///<inheritdoc/>
        private void CheckSecurityOnDelete() {
            if(!_entityAccess.CheckAccess((int)OperationType.Delete, Guid.Empty)) {
                // Raise security exception
                List<EwpErrorData> errorDataList = new List<EwpErrorData>() {
            new EwpErrorData() {
              ErrorSubType = (int)SecurityErrorSubType.Delete,
              Message = string.Format("", "Publisher")
              }
            };
            }
        }

        #endregion Security

        // Get the next identity number for the publisher entity.
        public string GetNextMaxNo(Guid tenantId, int entityType) {
            int identityNumber = _uniqueIdentityGeneratorDS.GetIdentityNo(tenantId, entityType, AppPortalConstants.PublisherIdPrefix, AppPortalConstants.PublisherIdentityNumberStart);
            return string.Format("{0}{1}", AppPortalConstants.PublisherIdPrefix, Convert.ToString(identityNumber));
        }

        #region GET/UPDATE Configuration

        ///<inheritdoc/>
        public async Task<ConfigurationDTO> GetConfigurationDetailAsync(CancellationToken token = default(CancellationToken)) {
            UserSession session = _userSessionManager.GetSession();
            ConfigurationDTO configurationDTO = await _qPublisherDS.GetConfigurationDetailAsync(session.TenantId, session.AppId, token);

            // Get Publisher Entity Information
            Entity.Publisher publisher = await FindAsync(p => p.TenantId == session.TenantId);

            // Get Publisher Address Entity Information
            List<PublisherAddressDTO> addressDTO = (await _publisherAddressDS.GetAddressListByPublisherIdAndAddressTypeAsync(publisher.ID, (int)PublisherAddressTypeEnum.DefaultPublisherAddress)).ToList();
            if(addressDTO != null || addressDTO.Count > 0) {
                configurationDTO.PublisherAddressDTO = addressDTO;
            }

            //Get Application Entity Information
            if(configurationDTO != null) {
                configurationDTO.AppList = await _qPublisherDS.GetAppsWithServiceAndAttributeAsync(publisher.TenantId, token);
            }

            return configurationDTO;
        }


        /// <summary>
        /// Update configuration details
        /// </summary>
        /// <param name="configurationDTO"></param>
        /// <returns></returns>
        public async Task UpdateConfigurationDetailAsync(ConfigurationDTO configurationDTO) {

            UserSession session = _userSessionManager.GetSession();

            //Get publisher  Entity Information              
            Entity.Publisher publisher = _publisherRepository.Get(configurationDTO.PublisherId);
            publisher.Name = configurationDTO.Name; // Publisher Name Update
            publisher.ContactPersonName = configurationDTO.ContactPersonName;
            publisher.ContactPersonDesignation = configurationDTO.ContactPersonDesignation;
            publisher.ContactPersonEmail = configurationDTO.ContactPersonEmail;
            publisher.ContactPersonPhone = configurationDTO.ContactPersonPhone;
            publisher.Website = configurationDTO.Website;
            
            //Update Publisher System Fields Entity Information 
            UpdateSystemFieldsByOpType(publisher, OperationType.Update);
            
            //Update Publisher Entity Information 
            Update(publisher, publisher.ID);

            // Update Address Data
            Entity.Publisher publisherDetail = Find(p => p.TenantId == configurationDTO.TenantId);
            await _publisherAddressDS.AddUpdatePublisherAddressListAsync(configurationDTO.PublisherAddressDTO, session.TenantId, publisherDetail.ID);

            // API Call for Upadate other values
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "configuration/updatepublisherconfiguration";
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));            
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, configurationDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);           

            _unitOfWork.SaveAll();
        }


        #endregion

        #region Get

        /// <summary>
        /// Get publisher by publisher tenantid.
        /// </summary>
        /// <param name="pubTenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Entity.Publisher> GetPublisherByPublisherTenantIdAsync(Guid pubTenantId, CancellationToken token = default(CancellationToken)) {
            return await _publisherRepository.GetPublisherByPublisherTenantIdAsync(pubTenantId, token);
        }

        #endregion Get


        #region GET Platform connector list

        /// <summary>
        /// get platform connector list
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<ConnectorDQ>> GetPublisherConnectorListAsync(CancellationToken token = default(CancellationToken)) {
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

        /// <summary>
        /// Get Data from Sync time log 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BusBASyncTimeLogDTO>> SyncTimeLogAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "BASync/getsynctimelog/" + tenantId;
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<BusBASyncTimeLogDTO>>(requestOptions, false);
        }

        #endregion

    }
}
