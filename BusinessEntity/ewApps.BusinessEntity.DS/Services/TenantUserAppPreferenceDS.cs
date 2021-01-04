/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Gaurav Katiyar <gkatiyar@eworkplaceapps.com>
 * Date: 18 December 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 18 December 2019
 */
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.BusinessEntity.DS {
    /// <summary>
    /// This class implements standard business logic and operations for TenantUserAppPreference entity.
    /// </summary>
    public class TenantUserAppPreferenceDS:BaseDS<TenantUserAppPreference>, ITenantUserAppPreferenceDS {
        #region Local Member 

        ITenantUserAppPreferenceRepository _tenantUserAppPreferenceRepository;
        IUserSessionManager _userSessionManager;
        IUnitOfWork _beUnitOfWork;

        #endregion

        #region Constructor 

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="tenantUserAppPreferenceRep"></param>
        public TenantUserAppPreferenceDS(ITenantUserAppPreferenceRepository tenantUserAppPreferenceRep, IUnitOfWork beUnitOfWork) : base(tenantUserAppPreferenceRep) {
            _tenantUserAppPreferenceRepository = tenantUserAppPreferenceRep;
            _beUnitOfWork = beUnitOfWork;
        }

        #endregion

        #region Manual mapping
        private static PreferenceViewDTO ToPreferenceDto(TenantUserAppPreference tenantUserAppPreference) {
            return new PreferenceViewDTO {
                ID = tenantUserAppPreference.ID,
                CreatedBy = tenantUserAppPreference.CreatedBy,
                CreatedOn = tenantUserAppPreference.CreatedOn,
                UpdatedBy = tenantUserAppPreference.UpdatedBy,
                UpdatedOn = tenantUserAppPreference.UpdatedOn,
                Deleted = tenantUserAppPreference.Deleted,
                TenantId = tenantUserAppPreference.TenantId,
                TenantUserId = tenantUserAppPreference.TenantUserId,
                EmailPreference = tenantUserAppPreference.EmailPreference,
                ASPreference = tenantUserAppPreference.ASPreference,
                SMSPreference = tenantUserAppPreference.SMSPreference
            };
        }
        #endregion

        #region Get

        ///<inheritdoc/>
       public async Task<PreferenceViewDTO> GetUserPreferenceListByAppIdAsync(Guid appid, CancellationToken token = default(CancellationToken)) {
        UserSession userSession = _userSessionManager.GetSession();
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepository.FindAsync(a => a.TenantUserId == userSession.TenantUserId && a.AppId == appid && a.TenantId == userSession.TenantId && a.Deleted == false, true);
            PreferenceViewDTO preferenceViewDTO = ToPreferenceDto(tenantUserAppPreference);
            return preferenceViewDTO;
        }

        /////<inheritdoc/>
        //public async Task<PreferenceViewDTO> GetBusPayPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken)) {
        //    UserSession userSession = _userSessionManager.GetSession();
        //    TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepository.FindAsync(a => a.TenantUserId == userSession.TenantUserId && a.AppId == appid && a.TenantId == userSession.TenantId && a.Deleted == false, true);
        //    //PreferenceViewDTO preferenceViewDTO = _mapper.Map<PreferenceViewDTO>(tenantUserAppPreference);
        //    PreferenceViewDTO preferenceViewDTO = ToPreferenceDto(tenantUserAppPreference);
        //    return preferenceViewDTO;
        //}


        /////<inheritdoc/>

        //public async Task<PreferenceViewDTO> GetCustPayPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken)) {
        //    UserSession userSession = _userSessionManager.GetSession();
        //    TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepository.FindAsync(a => a.TenantUserId == userSession.TenantUserId && a.AppId == appid && a.TenantId == userSession.TenantId && a.Deleted == false, true);
        //    //PreferenceViewDTO preferenceViewDTO = _mapper.Map<PreferenceViewDTO>(tenantUserAppPreference);
        //    PreferenceViewDTO preferenceViewDTO = ToPreferenceDto(tenantUserAppPreference);
        //    return preferenceViewDTO;
        //}

        /////<inheritdoc/>
        //public async Task<PreferenceViewDTO> GetVendorPayPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken)) {
        //    UserSession userSession = _userSessionManager.GetSession();
        //    TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepository.FindAsync(a => a.TenantUserId == userSession.TenantUserId && a.AppId == appid && a.TenantId == userSession.TenantId && a.Deleted == false, true);
        //    //PreferenceViewDTO preferenceViewDTO = _mapper.Map<PreferenceViewDTO>(tenantUserAppPreference);
        //    PreferenceViewDTO preferenceViewDTO = ToPreferenceDto(tenantUserAppPreference);
        //    return preferenceViewDTO;
        //}


        #endregion Get


        #region Update preference
        ///<inheritdoc/>
        public async Task UpdatePreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken)) {
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepository.FindAsync(a => a.ID == preferenceUpdateDTO.ID && a.Deleted == false, true);
            tenantUserAppPreference.EmailPreference = preferenceUpdateDTO.EmailPreference;
            tenantUserAppPreference.ASPreference = preferenceUpdateDTO.ASPreference;
            tenantUserAppPreference.SMSPreference = preferenceUpdateDTO.SMSPreference;

            UpdateSystemFieldsByOpType(tenantUserAppPreference, OperationType.Update);
            await _tenantUserAppPreferenceRepository.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
            _beUnitOfWork.Save();
        }

        /////<inheritdoc/>
        //public async Task UpdateBusPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken)) {
        //    TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepository.FindAsync(a => a.ID == preferenceUpdateDTO.ID && a.Deleted == false, true);
        //    tenantUserAppPreference.EmailPreference = preferenceUpdateDTO.EmailPreference;
        //    tenantUserAppPreference.ASPreference = preferenceUpdateDTO.ASPreference;
        //    tenantUserAppPreference.SMSPreference = preferenceUpdateDTO.SMSPreference;

        //    UpdateSystemFieldsByOpType(tenantUserAppPreference, OperationType.Update);
        //    await _tenantUserAppPreferenceRepository.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
        //    _beUnitOfWork.Save();
        //}

        /////<inheritdoc/>
        //public async Task UpdateCustPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken)) {
        //    TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepository.FindAsync(a => a.ID == preferenceUpdateDTO.ID && a.Deleted == false, true);
        //    tenantUserAppPreference.EmailPreference = preferenceUpdateDTO.EmailPreference;
        //    tenantUserAppPreference.ASPreference = preferenceUpdateDTO.ASPreference;
        //    tenantUserAppPreference.SMSPreference = preferenceUpdateDTO.SMSPreference;

        //    UpdateSystemFieldsByOpType(tenantUserAppPreference, OperationType.Update);
        //    await _tenantUserAppPreferenceRepository.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
        //    _beUnitOfWork.Save();
        //}

        /////<inheritdoc/>
        //public async Task UpdateVendorPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken)) {
        //    TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepository.FindAsync(a => a.ID == preferenceUpdateDTO.ID && a.Deleted == false, true);
        //    tenantUserAppPreference.EmailPreference = preferenceUpdateDTO.EmailPreference;
        //    tenantUserAppPreference.ASPreference = preferenceUpdateDTO.ASPreference;
        //    tenantUserAppPreference.SMSPreference = preferenceUpdateDTO.SMSPreference;

        //    UpdateSystemFieldsByOpType(tenantUserAppPreference, OperationType.Update);
        //    await _tenantUserAppPreferenceRepository.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
        //    _beUnitOfWork.Save();
        //}

        #endregion

        #region Add preference


        /////<inheritdoc/>
        //public async Task<bool> AddBusPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken))
        //{
        //    TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepository.FindAsync(a => a.ID == preferenceUpdateDTO.ID && a.Deleted == false, true);
        //    tenantUserAppPreference.EmailPreference = preferenceUpdateDTO.EmailPreference;
        //    tenantUserAppPreference.ASPreference = preferenceUpdateDTO.ASPreference;
        //    tenantUserAppPreference.SMSPreference = preferenceUpdateDTO.SMSPreference;

        //    UpdateSystemFieldsByOpType(tenantUserAppPreference, OperationType.Add);
        //    await _tenantUserAppPreferenceRepository.AddAsync(tenantUserAppPreference, token);
        //    _paymentUnitOfWork.Save();
        //    return true;
        //}

        ///<inheritdoc/>
        public async Task<bool> AddBEAppPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken)) {

            TenantUserAppPreference tenantUserAppPreference = new TenantUserAppPreference();
            tenantUserAppPreference.ID = Guid.NewGuid();
            tenantUserAppPreference.AppId = preferenceUpdateDTO.AppId;
            //ToDo: nitin-Why created by is used from dto.
            // We can not use system methods because sometimes session is not present.           
            tenantUserAppPreference.Deleted = false;
            tenantUserAppPreference.EmailPreference = preferenceUpdateDTO.EmailPreference;
            tenantUserAppPreference.SMSPreference = preferenceUpdateDTO.SMSPreference;
            tenantUserAppPreference.ASPreference = preferenceUpdateDTO.ASPreference;
            tenantUserAppPreference.TenantId = preferenceUpdateDTO.TenantId;
            tenantUserAppPreference.TenantUserId = preferenceUpdateDTO.TenantUserId;
            tenantUserAppPreference.CreatedBy = preferenceUpdateDTO.CreatedBy;
            tenantUserAppPreference.CreatedOn = DateTime.UtcNow;
            tenantUserAppPreference.UpdatedBy = tenantUserAppPreference.CreatedBy;
            tenantUserAppPreference.UpdatedOn = tenantUserAppPreference.CreatedOn;
            await AddAsync(tenantUserAppPreference, token);


            //TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepository.FindAsync(a => a.ID == preferenceUpdateDTO.ID && a.Deleted == false, true);
            //tenantUserAppPreference.EmailPreference = preferenceUpdateDTO.EmailPreference;
            //tenantUserAppPreference.ASPreference = preferenceUpdateDTO.ASPreference;
            //tenantUserAppPreference.SMSPreference = preferenceUpdateDTO.SMSPreference;
            //UpdateSystemFieldsByOpType(tenantUserAppPreference, OperationType.Add);
            //await _tenantUserAppPreferenceRepository.AddAsync(tenantUserAppPreference, token);

            _beUnitOfWork.Save();
            return true;
        }

        #endregion
    }
}
