/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar<abadgujar@batchmaster.com>
 * Date: 5 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 5 September 2019
 */

using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;
using ewApps.Core.UserSessionService;
using ewApps.Payment.Common;
using ewApps.Payment.Data;
using ewApps.Payment.DTO;
using ewApps.Payment.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace ewApps.Payment.DS {
    public class TenantUserAppPreferenceDS:BaseDS<TenantUserAppPreference>, ITenantUserAppPreferenceDS {


        #region Local Member
        IUserSessionManager _userSessionManager;
        IPaymentUnitOfWork _paymentUnitOfWork;
        IMapper _mapper;
        ITenantUserAppPreferenceRepository _tenantUserAppPreferenceRepository;
        // IUnitOfWork _unitOfWork;
        #endregion


        #region Constructor
        public TenantUserAppPreferenceDS(IUserSessionManager userSessionManager, IMapper mapper, ITenantUserAppPreferenceRepository tenantUserAppPreferenceRepository, IPaymentUnitOfWork paymentUnitOfWork) : base(tenantUserAppPreferenceRepository) {
            _paymentUnitOfWork = paymentUnitOfWork;
            _mapper = mapper;
            _tenantUserAppPreferenceRepository = tenantUserAppPreferenceRepository;
            _userSessionManager = userSessionManager;
        }
        #endregion Constructor

        public async Task AddTenantUserAppPreferncesAsync(TenantUserAppManagmentDTO roleLinkingAndPreferneceDTO, int userType) {
            // Add tenant user preference entry.
            TenantUserAppPreference tenantUserAppPreference = new TenantUserAppPreference();
            tenantUserAppPreference.ID = Guid.NewGuid();
            tenantUserAppPreference.AppId = roleLinkingAndPreferneceDTO.AppId;
            //ToDo: nitin-Why created by is used from dto.
            // We can not use system methods because sometimes session is not present.
            tenantUserAppPreference.CreatedBy = roleLinkingAndPreferneceDTO.CreatedBy;
            tenantUserAppPreference.CreatedOn = DateTime.UtcNow;
            tenantUserAppPreference.Deleted = false;
            tenantUserAppPreference.EmailPreference = roleLinkingAndPreferneceDTO.EmailPreference;
            tenantUserAppPreference.SMSPreference = roleLinkingAndPreferneceDTO.SMSPreference;
            tenantUserAppPreference.ASPreference = roleLinkingAndPreferneceDTO.ASPreference;

            //if (userType == (int)UserTypeEnum.Business)
            //{
            //    //tenantUserAppPreference.EmailPreference = (long)BusinessUserPaymentAppPreferenceEnum.All;
            //    //tenantUserAppPreference.SMSPreference = (long)BusinessUserPaymentAppPreferenceEnum.All;
            //    //tenantUserAppPreference.ASPreference = (long)BusinessUserPaymentAppPreferenceEnum.All;
            //    tenantUserAppPreference.EmailPreference = roleLinkingAndPreferneceDTO.EmailPreference;
            //    tenantUserAppPreference.SMSPreference = roleLinkingAndPreferneceDTO.SMSPreference;
            //    tenantUserAppPreference.ASPreference = roleLinkingAndPreferneceDTO.ASPreference;
            //}
            //else if (userType == (int)UserTypeEnum.Customer)
            //{
            //    //tenantUserAppPreference.EmailPreference = (long)CustomerUserPaymentAppPreferenceEnum.All;
            //    //tenantUserAppPreference.SMSPreference = (long)CustomerUserPaymentAppPreferenceEnum.All;
            //    //tenantUserAppPreference.ASPreference = (long)CustomerUserPaymentAppPreferenceEnum.All;
            //    tenantUserAppPreference.EmailPreference = roleLinkingAndPreferneceDTO.EmailPreference;
            //    tenantUserAppPreference.SMSPreference = roleLinkingAndPreferneceDTO.SMSPreference;
            //    tenantUserAppPreference.ASPreference = roleLinkingAndPreferneceDTO.ASPreference;
            //}
            tenantUserAppPreference.TenantId = roleLinkingAndPreferneceDTO.TenantId;
            tenantUserAppPreference.TenantUserId = roleLinkingAndPreferneceDTO.TenantUserId;
            tenantUserAppPreference.UpdatedBy = tenantUserAppPreference.CreatedBy;
            tenantUserAppPreference.UpdatedOn = tenantUserAppPreference.CreatedOn;
            await AddAsync(tenantUserAppPreference);
        }


        #region Get

        ///<inheritdoc/>
        ///<param name="appid"></param>
        ///<param name="token"></param>
        public async Task<PreferenceViewDTO> GetBusPayPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepository.FindAsync(a => a.TenantUserId == userSession.TenantUserId && a.AppId == appid && a.TenantId == userSession.TenantId && a.Deleted == false, true);
            PreferenceViewDTO preferenceViewDTO = _mapper.Map<PreferenceViewDTO>(tenantUserAppPreference);
            return preferenceViewDTO;
        }


        ///<inheritdoc/>
        ///<param name="appid"></param>
        ///<param name="token"></param>
        public async Task<PreferenceViewDTO> GetCustPayPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();

            //TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepository.FindAsync(a => a.TenantUserId == userSession.TenantUserId && a.AppId == appid && a.TenantId == userSession.TenantId && a.Deleted == false, true);

            // ToDo:Nitin: This statement should be fix because multiple preference records are gettting generated.
            TenantUserAppPreference preference = _tenantUserAppPreferenceRepository.GetPreferenceListByAppAndTenantAndUserId(appid, userSession.TenantId, userSession.TenantUserId).OrderByDescending(i => i.UpdatedOn).FirstOrDefault();

            PreferenceViewDTO preferenceViewDTO = new PreferenceViewDTO();

            if(preference != null) {
                preferenceViewDTO = PreferenceViewDTO.MapFromTenantUserPreference(preference);
            }

            return preferenceViewDTO;
        }

        #endregion Get


        #region Update preference


        ///<inheritdoc/>
        public async Task UpdateBusPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken)) {

            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepository.FindAsync(a => a.ID == preferenceUpdateDTO.ID && a.Deleted == false, true);
            tenantUserAppPreference.EmailPreference = preferenceUpdateDTO.EmailPreference;
            tenantUserAppPreference.ASPreference = preferenceUpdateDTO.ASPreference;
            tenantUserAppPreference.SMSPreference = preferenceUpdateDTO.SMSPreference;

            UpdateSystemFieldsByOpType(tenantUserAppPreference, OperationType.Update);
            await _tenantUserAppPreferenceRepository.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
            _paymentUnitOfWork.Save();
        }

        ///<inheritdoc/>
        public async Task UpdateCustPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken)) {

            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepository.FindAsync(a => a.ID == preferenceUpdateDTO.ID && a.Deleted == false, true);
            tenantUserAppPreference.EmailPreference = preferenceUpdateDTO.EmailPreference;
            tenantUserAppPreference.ASPreference = preferenceUpdateDTO.ASPreference;
            tenantUserAppPreference.SMSPreference = preferenceUpdateDTO.SMSPreference;

            UpdateSystemFieldsByOpType(tenantUserAppPreference, OperationType.Update);
            await _tenantUserAppPreferenceRepository.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
            _paymentUnitOfWork.Save();
        }

        public async Task UpdateVendorPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken)) {
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceRepository.FindAsync(a => a.ID == preferenceUpdateDTO.ID && a.Deleted == false, true);
            tenantUserAppPreference.EmailPreference = preferenceUpdateDTO.EmailPreference;
            tenantUserAppPreference.ASPreference = preferenceUpdateDTO.ASPreference;
            tenantUserAppPreference.SMSPreference = preferenceUpdateDTO.SMSPreference;

            UpdateSystemFieldsByOpType(tenantUserAppPreference, OperationType.Update);
            await _tenantUserAppPreferenceRepository.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
            _paymentUnitOfWork.Save();
        }


        #endregion

        #region Add preference

        ///<inheritdoc/>
        public async Task<bool> AddPaymentPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken)) {
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

            _paymentUnitOfWork.Save();
            return true;
        }

        #endregion
    }
}
