/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 31 January 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.ExceptionService;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// Class provide PublisherAppSettingDS add/update/delete and supportive methods.
    /// </summary>
    public class PublisherAppSettingDS:BaseDS<PublisherAppSetting>, IPublisherAppSettingDS {

        #region Properties
        IPublisherAppSettingRepository _publisherAppSettingRepository;
        IEntityThumbnailDS _entityThumbnailDS;
        IUnitOfWork _unitOfWork;

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="publisherAppSettingRepository"></param>
        public PublisherAppSettingDS(IPublisherAppSettingRepository publisherAppSettingRepository, IEntityThumbnailDS entityThumbnailDS, IUnitOfWork unitOfWork) : base(publisherAppSettingRepository) {
            _publisherAppSettingRepository = publisherAppSettingRepository;
            _entityThumbnailDS = entityThumbnailDS;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        #region Get

        /// <inheritdoc/>
        public async Task<PublisherAppSetting> GetByAppIdAndPublisherTenantIdAsync(Guid appId, Guid pubTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _publisherAppSettingRepository.GetByAppIdAndPublisherTenantIdAsync(appId, pubTenantId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> GetAppNameListByPublisherIdAsync(Guid publisherId, CancellationToken cancellationToken = default(CancellationToken)) {
            return _publisherAppSettingRepository.GetAppNameListByPublisherId(publisherId);
        }
        #endregion

        #region Update

        /// <inheritdoc/>
        public async Task UpdatePublisherAppSettingFromAppAsync(AppAndServiceDTO appAndServiceDTO, CancellationToken token = default(CancellationToken)) {

            List<PublisherAppSetting> publisherAppSettingList = (await _publisherAppSettingRepository.FindAllAsync(p => p.AppId == appAndServiceDTO.AppDetailDTO.ID, false)).ToList();

            for(int i = 0; i < publisherAppSettingList.Count; i++) {
                PublisherAppSetting publisherAppSetting = publisherAppSettingList[i];
                if(publisherAppSetting.Customized == false) {

                    publisherAppSetting.ThemeId = appAndServiceDTO.AppDetailDTO.ThemeId;
                    publisherAppSetting.Name = appAndServiceDTO.AppDetailDTO.Name;
                    if(appAndServiceDTO.ThumbnailAddAndUpdateDTO != null) {
                        publisherAppSetting.ThumbnailId = appAndServiceDTO.ThumbnailAddAndUpdateDTO.ID;
                    }
                    else {
                        publisherAppSetting.ThumbnailId = null;
                    }

                    UpdateSystemFieldsByOpType(publisherAppSetting, OperationType.Update);

                    // Validate entity 
                    ValidateOnAddAndUpdate(publisherAppSetting);

                    // Save Publisher App Setting  
                    await _publisherAppSettingRepository.UpdateAsync(publisherAppSetting, publisherAppSetting.ID);
                }
            }
        }

        ///<inheritdoc/>
        public async Task UpdateAppAsync(AppAndServiceDTO appAndServiceDTO, CancellationToken token = default(CancellationToken)) {
            // Update publisher app setting    
            await UpdatePublisherAppSettingAsync(appAndServiceDTO.AppDetailDTO, appAndServiceDTO, token);
            _unitOfWork.Save();
        }
        ///<inheritdoc/>
        private async Task UpdatePublisherAppSettingAsync(AppDetailDTO appDTO, AppAndServiceDTO appAndServiceDTO, CancellationToken token = default(CancellationToken)) {


            // Add apllication
            if(appDTO != null) {

                PublisherAppSetting publisherAppSetting = _publisherAppSettingRepository.Get(appDTO.ID);
                if(!publisherAppSetting.Customized)
                    publisherAppSetting.Customized = CheckCustomizedFlag(appDTO, publisherAppSetting, appAndServiceDTO);
                publisherAppSetting.ThemeId = appDTO.ThemeId;
                publisherAppSetting.Name = appDTO.Name;
                publisherAppSetting.Active = appDTO.Active;
                publisherAppSetting.InactiveComment = appDTO.InactiveComment;
                Guid thumbnailID = getThumbnailIDFromAppDetailDTO(appAndServiceDTO);
                publisherAppSetting.ThumbnailId = thumbnailID;

                UpdateSystemFieldsByOpType(publisherAppSetting, OperationType.Update);

                // Validate entity 
                ValidateOnAddAndUpdate(publisherAppSetting);

                // Save Application  
                await _publisherAppSettingRepository.UpdateAsync(publisherAppSetting, publisherAppSetting.ID);
            }
        }

        private bool CheckCustomizedFlag(AppDetailDTO appDTO, PublisherAppSetting publisherAppSetting, AppAndServiceDTO appAndServiceDTO) {

            bool checkCustomizedFlag = false;

            if(publisherAppSetting.Name != appDTO.Name)
                return true;
            if(publisherAppSetting.ThemeId != appDTO.ThemeId)
                return true;
            if(publisherAppSetting.Active != appDTO.Active)
                return true;
            if(publisherAppSetting.InactiveComment != appDTO.InactiveComment)
                return true;
            if(appAndServiceDTO.ThumbnailAddAndUpdateDTO != null) {
                ThumbnailAddAndUpdateDTO thumbDTO = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(appAndServiceDTO.AppDetailDTO.ID);
                if(thumbDTO != null) {
                    return false;
                }
                else {
                    return true;
                }
            }
            return checkCustomizedFlag;
        }

        private Guid getThumbnailIDFromAppDetailDTO(AppAndServiceDTO appAndServiceDTO) {


            if(appAndServiceDTO.ThumbnailAddAndUpdateDTO != null) {

                ThumbnailAddAndUpdateDTO thumbDTO = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(appAndServiceDTO.ThumbnailAddAndUpdateDTO.ThumbnailOwnerEntityId);
                if(thumbDTO != null) {

                    if(appAndServiceDTO.ThumbnailAddAndUpdateDTO.OperationType == (int)OperationType.Update) {
                        _entityThumbnailDS.UpdateThumbnail(appAndServiceDTO.ThumbnailAddAndUpdateDTO);
                    }
                    return thumbDTO.ID;
                }
                else {
                    appAndServiceDTO.ThumbnailAddAndUpdateDTO.ID = Guid.NewGuid();
                    _entityThumbnailDS.AddThumbnail(appAndServiceDTO.ThumbnailAddAndUpdateDTO);
                    return appAndServiceDTO.ThumbnailAddAndUpdateDTO.ID;
                }

            }
            else {
                if(appAndServiceDTO.AppDetailDTO != null) {
                    ThumbnailAddAndUpdateDTO thumbDTO = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(appAndServiceDTO.AppDetailDTO.ID);

                    if(thumbDTO != null) {
                        if(appAndServiceDTO.AppDetailDTO.ThumbnailId != null) {
                            _entityThumbnailDS.DeleteThumbnail((Guid)appAndServiceDTO.AppDetailDTO.ThumbnailId);
                            // check platform thumbnail if plaform has             
                            EntityThumbnail thumb = _entityThumbnailDS.Find(t => t.OwnerEntityId == appAndServiceDTO.AppDetailDTO.AppID);
                            if(thumb != null) {
                                return thumb.ID;
                            }
                            return Guid.Empty;
                        }
                    }
                    else {
                        if(appAndServiceDTO.AppDetailDTO.ThumbnailId == null)
                            return Guid.Empty;
                    }
                }
                return Guid.Empty;
            }

        }

        #endregion Update

        #region VALIDATION

        private bool ValidateOnAddAndUpdate(PublisherAppSetting entity) {
            IList<EwpErrorData> brokenRules = new List<EwpErrorData>();

            // Validate PublisherAppSetting entity field values.
            entity.Validate(out brokenRules);

            // Raise validation exception if any validation is failed.
            ExceptionUtils.RaiseValidationException(entity.Name, brokenRules);

            return true;
        }

        #endregion VALIDATION

    }
}
