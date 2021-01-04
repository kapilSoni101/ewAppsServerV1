/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 31 Oct 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 31 Oct 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DS {

    //ViewSettings DS Class responsible for All Add,Update,Get and Delete operation in View Settings Entity
    public class ViewSettingsDS:BaseDS<ViewSettings>, IViewSettingsDS {

        #region Local Variable
        IViewSettingsRepository _viewSettingsRepository;
        IUserSessionManager _userSessionManager;
        IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewSettingsDS"/> class with its dependencies.
        /// </summary>
        /// <param name="viewSettingsRepository">An instance of <see cref="IViewSettingsRepository"/> to communicate with storage.</param>
        public ViewSettingsDS(IViewSettingsRepository viewSettingsRepository, IUnitOfWork unitOfWork, IUserSessionManager userSessionManager) : base(viewSettingsRepository) {
            _viewSettingsRepository = viewSettingsRepository;
            _unitOfWork = unitOfWork;
            _userSessionManager = userSessionManager;
        }
        #endregion


        #region Add And Update 
        ///<inheritdoc/>
        public async Task<ResponseModelDTO> AddUpdateViewSettings(ViewSettingDTO viewSettingDTO, CancellationToken token = default(CancellationToken)) {

            //Object Initialization
            UserSession userSession = _userSessionManager.GetSession();
            ViewSettings viewSettings = await GetAsync(viewSettingDTO.ID);
            List<ViewSettings> listViewSettins = new List<ViewSettings>();
            ResponseModelDTO responseModel = new ResponseModelDTO();
            if(viewSettings != null) {

                viewSettings.ScreenId = viewSettingDTO.ScreenId;
                viewSettings.TenantUserId = userSession.TenantUserId;
                viewSettings.ViewName = viewSettingDTO.ViewName;
                viewSettings.ViewSettingJson = viewSettingDTO.ViewSettingJson;
                viewSettings.AppKey = viewSettingDTO.AppKey;
                viewSettings.IsDefault = viewSettingDTO.IsDefault;

                // If IsDefault is True Then Update All Remaining isDafault False Basis of ScreenId,TenantID,TenantUserId
                if(viewSettingDTO.IsDefault) {
                    listViewSettins = (await FindAllAsync(v => v.ID != viewSettingDTO.ID && v.ScreenId == viewSettingDTO.ScreenId && v.AppKey == viewSettingDTO.AppKey && v.TenantUserId == userSession.TenantUserId && v.TenantId == userSession.TenantId && v.Deleted == false)).ToList();
                    foreach(ViewSettings vs in listViewSettins)
                        vs.IsDefault = false;
                }

                //Update System Default Field 
                UpdateSystemFieldsByOpType(viewSettings, OperationType.Update);
                await UpdateAsync(viewSettings, viewSettings.ID);
                responseModel.IsSuccess = true;


            }
            else {
                viewSettings = new ViewSettings();
                viewSettings.ScreenId = viewSettingDTO.ScreenId;
                viewSettings.TenantUserId = userSession.TenantUserId;
                viewSettings.ViewName = viewSettingDTO.ViewName;
                viewSettings.ViewSettingJson = viewSettingDTO.ViewSettingJson;
                viewSettings.AppKey = viewSettingDTO.AppKey;
                viewSettings.IsDefault = viewSettingDTO.IsDefault;

                UpdateSystemFieldsByOpType(viewSettings, OperationType.Add);
                await AddAsync(viewSettings);

                // If IsDefault is True Then Update All Remaining isDafault False Basis of ScreenId,TenantID,TenantUserId
                if(viewSettingDTO.IsDefault) {
                    listViewSettins = (await FindAllAsync(v => v.ID != viewSettingDTO.ID && v.ScreenId == viewSettingDTO.ScreenId && v.AppKey == viewSettingDTO.AppKey && v.TenantUserId == userSession.TenantUserId && v.TenantId == userSession.TenantId && v.Deleted == false)).ToList();
                    foreach(ViewSettings vs in listViewSettins)
                        vs.IsDefault = false;
                }

                responseModel.IsSuccess = true;
            }
            _unitOfWork.SaveAll();
            return responseModel;
        } 
        #endregion

        #region Delete
		//<inheritdoc/>
        public async Task<ResponseModelDTO> DeleteViewSettings(Guid id, CancellationToken token = default(CancellationToken)) {

            UserSession userSession = _userSessionManager.GetSession();
            ResponseModelDTO responseModel = new ResponseModelDTO();

            // Get entity if exists
            ViewSettings viewSettings = await FindAsync(v => v.ID == id && v.TenantUserId == userSession.TenantUserId && v.TenantId == userSession.TenantId && v.Deleted == false);
            if(viewSettings != null) {
                responseModel.IsSuccess = true;
                Delete(viewSettings);
                // Save Data
                _unitOfWork.SaveAll();
            }
            else {
                responseModel.IsSuccess = false;
            }
            return responseModel;
        }
        #endregion

        #region Get View Settings 
        public async Task<List<ViewSettingDTO>> GetViewSettingsListAsync(string screenkey, string appkey, CancellationToken token = default(CancellationToken)) {
            //Getting User Sesion
            UserSession userSession = _userSessionManager.GetSession();
            return await _viewSettingsRepository.GetViewSettingsListAsync(screenkey, appkey, userSession.TenantId, userSession.TenantUserId, token);
        } 
        #endregion

    }
}
