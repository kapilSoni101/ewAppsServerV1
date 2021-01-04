using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.PreferenceDTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public class TenantUserAppPreferenceDS:BaseDS<TenantUserAppPreference>, ITenantUserAppPreferenceDS {

        #region Local Member
        IUserSessionManager _userSessionManager;
        ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;
        ITenantUserAppPreferenceRepository _tenantUserAppPreferenceRepo;
        AppPortalAppSettings _appPortalAppSettings;
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userSessionManager"></param>
        /// <param name="tenantUserAppPreferenceRepo"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public TenantUserAppPreferenceDS(IUserSessionManager userSessionManager, ITenantUserAppPreferenceRepository tenantUserAppPreferenceRepo, IOptions<AppPortalAppSettings> appPortalAppSettings, IUnitOfWork unitOfWork, IMapper mapper) : base(tenantUserAppPreferenceRepo) {
            _userSessionManager = userSessionManager;
            _appPortalAppSettings = appPortalAppSettings.Value;
            _tenantUserAppPreferenceRepo = tenantUserAppPreferenceRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        #endregion Constructor


        #region Get Platform Preference List

        ///<inheritdoc/>
        ///<param name="appid"></param>
        ///<param name="token"></param>
        public async Task<PreferenceViewDTO> GetPlatformPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepo.FindAsync(a => a.TenantUserId == userSession.TenantUserId && a.AppId == appid && a.TenantId == userSession.TenantId && a.Deleted == false, true);
            PreferenceViewDTO preferenceViewDTO = _mapper.Map<PreferenceViewDTO>(tenantUserAppPreference);
            return preferenceViewDTO;
        }

        #endregion Get Platform Preference List


        #region Get Publisher Preference List

        ///<inheritdoc/>
        ///<param name="appid"></param>
        ///<param name="token"></param>
        public async Task<PreferenceViewDTO> GetPublisherPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepo.FindAsync(a => a.TenantUserId == userSession.TenantUserId && a.AppId == appid && a.TenantId == userSession.TenantId && a.Deleted == false, true);
            PreferenceViewDTO preferenceViewDTO = _mapper.Map<PreferenceViewDTO>(tenantUserAppPreference);
            return preferenceViewDTO;
        }

        #endregion Get Publisher Preference List


        #region Get Business Setup Preference

        ///<inheritdoc/>
        ///<param name="appid"></param>
        ///<param name="token"></param>
        public async Task<PreferenceViewDTO> GetBusSetupPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepo.FindAsync(a => a.TenantUserId == userSession.TenantUserId && a.AppId == appid && a.TenantId == userSession.TenantId, true);
            PreferenceViewDTO preferenceViewDTO = _mapper.Map<PreferenceViewDTO>(tenantUserAppPreference);
            return preferenceViewDTO;
        }

        #endregion Get Business Setup Preference


        #region Get Business Customer App Preference

        ///<inheritdoc/>
        ///<param name="appid"></param>
        ///<param name="token"></param>
        public async Task<PreferenceViewDTO> GetBusCustPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepo.FindAsync(a => a.TenantUserId == userSession.TenantUserId && a.AppId == appid && a.TenantId == userSession.TenantId, true);
            PreferenceViewDTO preferenceViewDTO = _mapper.Map<PreferenceViewDTO>(tenantUserAppPreference);
            return preferenceViewDTO;
        }

        #endregion Get Business Customer App Preference

        #region Get Business Payment App Preference

        ///<inheritdoc/>
        ///<param name="appid"></param>
        ///<param name="token"></param>
        public async Task<PreferenceViewDTO> GetBusPayPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            // Preparing api calling process model.
            string baseuri = _appPortalAppSettings.PaymentApiUrl;
            string requesturl = "paymentpreference/getbuspaypreference/" + appid;
            PreferenceViewDTO preferenceViewDTO = new PreferenceViewDTO();
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = null;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Get;
            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            preferenceViewDTO = await httpRequestProcessor.ExecuteAsync<PreferenceViewDTO>(requestOptions, false);

            return preferenceViewDTO;
        }

        #endregion Get Business Payment App Preference

        #region Get Business Vendor App Preference

        public async Task<PreferenceViewDTO> GetBusinessPreferenceListByAppIdAsync(Guid appid, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepo.FindAsync(a => a.Deleted == false && a.TenantUserId == userSession.TenantUserId && a.AppId == appid && a.TenantId == userSession.TenantId, true);
            PreferenceViewDTO preferenceViewDTO = _mapper.Map<PreferenceViewDTO>(tenantUserAppPreference);
            return preferenceViewDTO;
        }

        public async Task<PreferenceViewDTO> GetBusVendorPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepo.FindAsync(a => a.Deleted == false && a.TenantUserId == userSession.TenantUserId && a.AppId == appid && a.TenantId == userSession.TenantId, true);
            PreferenceViewDTO preferenceViewDTO = _mapper.Map<PreferenceViewDTO>(tenantUserAppPreference);
            return preferenceViewDTO;
        }


        #endregion


        #region Get Customer Setup Preference

        ///<inheritdoc/>
        ///<param name="appid"></param>
        ///<param name="token"></param>
        public async Task<PreferenceViewDTO> GetCustSetupPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepo.FindAsync(a => a.TenantUserId == userSession.TenantUserId && a.AppId == appid && a.TenantId == userSession.TenantId && a.Deleted == false, true);
            PreferenceViewDTO preferenceViewDTO = _mapper.Map<PreferenceViewDTO>(tenantUserAppPreference);
            return preferenceViewDTO;
        }

        #endregion Get Customer Setup Preference

        #region Get Customer Customer App Preference

        ///<inheritdoc/>
        ///<param name="appid"></param>
        ///<param name="token"></param>
        public async Task<PreferenceViewDTO> GetCustCustAppPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepo.FindAsync(a => a.TenantUserId == userSession.TenantUserId && a.AppId == appid && a.TenantId == userSession.TenantId && a.Deleted == false, true);
            PreferenceViewDTO preferenceViewDTO = _mapper.Map<PreferenceViewDTO>(tenantUserAppPreference);
            return preferenceViewDTO;
        }

        #endregion Get Customer Customer App Preference


        #region Get customer Payment App Preference

        ///<inheritdoc/>
        ///<param name="appid"></param>
        ///<param name="token"></param>
        public async Task<PreferenceViewDTO> GetCustPayAppPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            // Preparing api calling process model.
            string baseuri = _appPortalAppSettings.PaymentApiUrl;
            string requesturl = "paymentpreference/getcustpaypreference/" + appid;
            PreferenceViewDTO preferenceViewDTO = new PreferenceViewDTO();
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = null;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Get;
            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            preferenceViewDTO = await httpRequestProcessor.ExecuteAsync<PreferenceViewDTO>(requestOptions, false);

            return preferenceViewDTO;
        }

        #endregion Get Customer Payment App Preference




        #region Update platform Preference 

        /// <summary>
        /// Update platform Preference list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdatePlatformPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken)) {
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepo.FindAsync(a => a.ID == preferenceUpdateDTO.ID && a.Deleted == false, true);
            tenantUserAppPreference.EmailPreference = preferenceUpdateDTO.EmailPreference;
            tenantUserAppPreference.ASPreference = preferenceUpdateDTO.ASPreference;
            tenantUserAppPreference.SMSPreference = preferenceUpdateDTO.SMSPreference;
            UpdateSystemFieldsByOpType(tenantUserAppPreference, OperationType.Update);
            await _tenantUserAppPreferenceRepo.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
            _unitOfWork.Save();
        }

        #endregion Update platform Preference 


        #region Update publisher Preference 

        /// <summary>
        /// Update publisher Preference list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdatePublisherPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken)) {
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepo.FindAsync(a => a.ID == preferenceUpdateDTO.ID && a.Deleted == false, true);
            tenantUserAppPreference.EmailPreference = preferenceUpdateDTO.EmailPreference;
            tenantUserAppPreference.ASPreference = preferenceUpdateDTO.ASPreference;
            tenantUserAppPreference.SMSPreference = preferenceUpdateDTO.SMSPreference;
            UpdateSystemFieldsByOpType(tenantUserAppPreference, OperationType.Update);
            await _tenantUserAppPreferenceRepo.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
            _unitOfWork.Save();
        }

        #endregion Update Publisher Setup Preference 


        #region Update Business Setup Preference 


        public async Task UpdateBusinessPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken)) {
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepo.FindAsync(a => a.ID == preferenceUpdateDTO.ID && a.Deleted == false, true);
            tenantUserAppPreference.EmailPreference = preferenceUpdateDTO.EmailPreference;
            tenantUserAppPreference.ASPreference = preferenceUpdateDTO.ASPreference;
            tenantUserAppPreference.SMSPreference = preferenceUpdateDTO.SMSPreference;
            UpdateSystemFieldsByOpType(tenantUserAppPreference, OperationType.Update);
            await _tenantUserAppPreferenceRepo.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
            _unitOfWork.Save();
        }


        public async Task UpdateBusSetupPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken)) {
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepo.FindAsync(a => a.ID == preferenceUpdateDTO.ID && a.Deleted == false, true);
            tenantUserAppPreference.EmailPreference = preferenceUpdateDTO.EmailPreference;
            tenantUserAppPreference.ASPreference = preferenceUpdateDTO.ASPreference;
            tenantUserAppPreference.SMSPreference = preferenceUpdateDTO.SMSPreference;
            UpdateSystemFieldsByOpType(tenantUserAppPreference, OperationType.Update);
            await _tenantUserAppPreferenceRepo.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
            _unitOfWork.Save();
        }

        #endregion Update Business Setup Preference



        #region Update Business Customer App Preference 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdateBusCustPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken)) {
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepo.FindAsync(a => a.ID == preferenceUpdateDTO.ID && a.Deleted == false, true);
            tenantUserAppPreference.EmailPreference = preferenceUpdateDTO.EmailPreference;
            tenantUserAppPreference.ASPreference = preferenceUpdateDTO.ASPreference;
            tenantUserAppPreference.SMSPreference = preferenceUpdateDTO.SMSPreference;
            UpdateSystemFieldsByOpType(tenantUserAppPreference, OperationType.Update);
            await _tenantUserAppPreferenceRepo.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
            // if(saveChanges) {
            _unitOfWork.Save();
            // }
        }

        #endregion Update Business Customer App Preference



        #region Update Business Payment App Preference

        /// <summary>
        /// Update Business Payment Preference
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>        
        /// <returns></returns>
        public async Task UpdateBusPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            // API Call for Upadate preference values
            string baseuri = _appPortalAppSettings.PaymentApiUrl;
            string requesturl = "paymentpreference/updatebuspaypreference/";
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = preferenceUpdateDTO;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Put;
            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            httpRequestProcessor.Execute<bool>(requestOptions, false);
        }

        #endregion Update Business Payment App Preference


        #region Update Custome portal Customer App Preference 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdateCustCustPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken)) {
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepo.FindAsync(a => a.ID == preferenceUpdateDTO.ID && a.Deleted == false, true);
            tenantUserAppPreference.EmailPreference = preferenceUpdateDTO.EmailPreference;
            tenantUserAppPreference.ASPreference = preferenceUpdateDTO.ASPreference;
            tenantUserAppPreference.SMSPreference = preferenceUpdateDTO.SMSPreference;
            UpdateSystemFieldsByOpType(tenantUserAppPreference, OperationType.Update);
            await _tenantUserAppPreferenceRepo.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
            _unitOfWork.Save();
        }

        #endregion Update Customer portal Customer App Preference


        #region Update Customer Payment App Preference

        /// <summary>
        /// Update Customer Payment app Preference list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>        
        /// <returns></returns>
        public async Task UpdateCustPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            // API Call for Upadate preference values
            string baseuri = _appPortalAppSettings.PaymentApiUrl;
            string requesturl = "paymentpreference/updatecustpaypreference/";
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = preferenceUpdateDTO;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Put;
            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            httpRequestProcessor.Execute<bool>(requestOptions, false);
        }

        #endregion Update Customer Payment App Preference

    }
}
