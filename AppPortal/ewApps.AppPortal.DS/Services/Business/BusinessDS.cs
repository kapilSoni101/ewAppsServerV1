/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 08 Aug -2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.DMService;
using ewApps.Core.ExceptionService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    /// <summary>
    /// This class implements standard business logic and operations for business entity.
    /// </summary>
    public class BusinessDS:BaseDS<Business>, IBusinessDS {

        #region Local Member

        IBusinessRepository _businessRepository;
        IUserSessionManager _userSessionManager;
        IBusinessAccess _entityAccess;
        IBusinessAddressDS _businessAddressDS;
        IQBusinessAndUserDS _qBusinessAndUserDS;
        IEntityThumbnailDS _entityThumbnailDS;
        IUnitOfWork _unitOfWork;
        AppPortalAppSettings _appPortalAppSettings;

        #endregion Local Member

        #region Constructor 

        /// <summary>
        /// Initializing local variables
        /// </summary>
        /// <param name="businessRepository">Business data class repository.</param>        
        /// <param name="userSessionManager">User session class.</param>        
        public BusinessDS(IBusinessRepository businessRepository, IUserSessionManager userSessionManager, IBusinessAccess entityAccess, IBusinessAddressDS businessAddressDS, IQBusinessAndUserDS qBusinessAndUserDS, IEntityThumbnailDS entityThumbnailDS, IUnitOfWork unitOfWork, IOptions<AppPortalAppSettings> appPortalAppSettings) : base(businessRepository) {
            _businessRepository = businessRepository;
            _userSessionManager = userSessionManager;
            _entityAccess = entityAccess;
            _businessAddressDS = businessAddressDS;
            _qBusinessAndUserDS = qBusinessAndUserDS;
            _entityThumbnailDS = entityThumbnailDS;
            _unitOfWork = unitOfWork;
            _appPortalAppSettings = appPortalAppSettings.Value;
        }

        #endregion Constructor      

        #region Status

        public async Task UpdateBusinessStatus(BusinessStatusDTO businessStatusDTO) {

            // Get business.
            Business business = await FindAsync(b => b.TenantId == businessStatusDTO.TenantId);
            if(business != null) {
                // Update business status.
                business.Configured = businessStatusDTO.Configured;
                business.IntegratedMode = businessStatusDTO.IntegratedMode;
                UpdateSystemFieldsByOpType(business, OperationType.Update);
                _unitOfWork.SaveAll();
            }
        }

        #endregion Status

        #region Get

        /// <summary>
        /// Get business by tenantid.
        /// </summary>
        /// <returns></returns>
        public async Task<Business> GetBusinessByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _businessRepository.GetBusinessByTenantIdAsync(tenantId, token);
        }

        /// <summary>
        /// Get business model by tenant id.
        /// </summary>
        /// <param name="tenantId">Unique tenant id.</param>
        /// <param name="token"></param>
        /// <returns>return detail business tenant model.</returns>
        public async Task<UpdateBusinessTenantModelDQ> GetBusinessTenantDetailModelDTOAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            UpdateBusinessTenantModelDQ busModel = new UpdateBusinessTenantModelDQ(); //await _businessRepo.GetBusinessTenantDetailModelDTOAsync(tenantId, token);

            Entity.Business bus = await GetBusinessByTenantIdAsync(tenantId, token);
            if(bus != null) {
                busModel.IntegratedMode = bus.IntegratedMode;
                busModel.BusinessId = bus.ID;
                busModel.InitDB = bus.InitDB;
                busModel.ContactPersonDesignation = bus.ContactPersonDesignation;
                busModel.ContactPersonEmail = bus.ContactPersonEmail;
                busModel.ContactPersonName = bus.ContactPersonName;
                busModel.ContactPersonPhone = bus.ContactPersonPhone;
                // setting currency
                busModel.CurrencyCode = bus.CurrencyCode;
                busModel.GroupValue = bus.GroupValue;
                busModel.GroupSeperator = bus.GroupSeperator;
                busModel.DecimalSeperator = bus.DecimalSeperator;
                busModel.DecimalPrecision = bus.DecimalPrecision;

                busModel.Language = bus.Language;
                busModel.TimeZone = bus.TimeZone;
                busModel.DateTimeFormat = bus.DateTimeFormat;
                busModel.IdentityNumber = bus.IdentityNumber;
                busModel.Website = bus.Website;
            }

            return busModel;
        }


        #endregion Get

        #region Business branding
        /// <summary>
        /// Get Branding Setting Detail
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BusinessBrandingDQ> GetBusinessBrandingDetailAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken)) {

            return null;
        }

        #endregion

        #region Validation

        #endregion Validation

        #region Security

        ///<inheritdoc/>
        private void CheckSecurityOnAdd() {
            if(!_entityAccess.CheckAccess((int)OperationType.Add, Guid.Empty)) {
                // Raise security exception
                List<EwpErrorData> errorDataList = new List<EwpErrorData>() {
                new EwpErrorData() {
                    ErrorSubType = (int)SecurityErrorSubType.Add,
                    Message = string.Format("", "Business")
                    }
                };

                throw new EwpSecurityException("", errorDataList);
            }
        }

        ///<inheritdoc/>
        private void CheckSecurityOnUpdate() {
            if(!_entityAccess.CheckAccess((int)OperationType.Update, Guid.Empty)) {

                // Raise security exception 
                List<EwpErrorData> errorDataList = new List<EwpErrorData>() {
                new EwpErrorData() {
                    ErrorSubType = (int)SecurityErrorSubType.Update,
                    Message = string.Format("", "Business")
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
                    Message = string.Format("", "Business")
                    }
                };

                throw new EwpSecurityException("", errorDataList);
            }
        }

        #endregion Security

        #region GET Branding

        /// <summary>
        /// get publisher branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>PublisherBrandingDQ Model</returns>
        public async Task<BusinessBrandingDQ> GetBusinessBrandingAsync(Guid tenantId, Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            BusinessBrandingDQ businessBrandingDQ = await _qBusinessAndUserDS.GetBusinessBrandingAsync(tenantId, appId, cancellationToken);
            // Get platform thumbnail Model
            businessBrandingDQ.ThumbnailAddUpdateModel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(tenantId);
            return businessBrandingDQ;
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

        #region Update branding detail

        /// <summary>
        /// Update Publisher branding details
        /// </summary>
        /// <param name="businessBrandingDQ"></param>
        public async Task UpdateBusinessBranding(BusinessBrandingDQ businessBrandingDQ) {

            UserSession session = _userSessionManager.GetSession();

            //Get Publisher Entity Information
            Entity.Business business = _businessRepository.Get(businessBrandingDQ.ID);
            business.Name = businessBrandingDQ.Name;
            
            // Update Thumbnail 
            if(businessBrandingDQ.ThumbnailAddUpdateModel != null) {
                if(businessBrandingDQ.ThumbnailAddUpdateModel.OperationType == (int)OperationType.Add) {
                    _entityThumbnailDS.AddThumbnail(businessBrandingDQ.ThumbnailAddUpdateModel);
                }
                else if(businessBrandingDQ.ThumbnailAddUpdateModel.OperationType == (int)OperationType.Update) {
                    _entityThumbnailDS.UpdateThumbnail(businessBrandingDQ.ThumbnailAddUpdateModel);
                }
            }
            else {
                if(businessBrandingDQ.ThumbnailId != null && businessBrandingDQ.ThumbnailId != Guid.Empty) {
                    ThumbnailAddAndUpdateDTO thumbnailmodel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(businessBrandingDQ.ThumbnailId);
                    if(thumbnailmodel != null && businessBrandingDQ.TenantId == thumbnailmodel.ThumbnailOwnerEntityId)
                        _entityThumbnailDS.DeleteThumbnail(businessBrandingDQ.ThumbnailId);
                }
            }


            // API Call for Upadate other values
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "branding/businessupdate/";
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = businessBrandingDQ;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Put;
            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            httpRequestProcessor.Execute<bool>(requestOptions, false);

            UpdateSystemFieldsByOpType(business, OperationType.Update);
            Update(business, business.ID);
            _unitOfWork.SaveAll();

        }

        #endregion

        #region Update Business From SAP

        /// <summary>
        /// Update Business From SAP
        /// </summary>
        /// <param name="businessSyncDTO"></param>
        public async Task SyncERPBusiness(BusinessSyncDTO businessSyncDTO) {

            UserSession session = _userSessionManager.GetSession();
            Entity.Business business = await FindAsync(biz => biz.TenantId == session.TenantId);
            if(business != null) {
                business.TelePhone1 = businessSyncDTO.Telephone;
                business.Website = businessSyncDTO.WebSite;
                business.ContactPersonName = businessSyncDTO.ContactName;
                business.Language = PicklistHelper.GetLanguageIdByLanguageName(businessSyncDTO.Language);
                business.CurrencyCode = PicklistHelper.GetCurrencyIdByName(businessSyncDTO.Currency);
                business.DecimalSeperator = PicklistHelper.GetCurrencySeperatorIdByCurrencySeperator(businessSyncDTO.DecSep);
                business.DateTimeFormat = businessSyncDTO.DateSep;
                business.TimeZone = businessSyncDTO.CompanyTimeZone;
                UpdateSystemFieldsByOpType(business, OperationType.Update);
                Update(business, business.ID);
               await _businessAddressDS.AddBusinessAddressFromSAPAsync(businessSyncDTO, session.TenantId, business.ID);
                _unitOfWork.SaveAll();
            }

        }

        #endregion

    }
}
