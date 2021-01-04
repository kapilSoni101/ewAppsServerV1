/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 26 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 26 August 2019
 */

using System;
using System.Collections.Generic;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DS {
    /// <summary>
    /// This class implements standard business logic and operations for support comment.
    /// </summary>
    public class SupportCommentDS:BaseDS<SupportComment>, ISupportCommentDS {

        #region Local member

        ISupportCommentRepository _supportCommentRepository;
        IUserSessionManager _userSessionManager;

        #endregion Local member

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportCommentDS"/> class.
        /// </summary>
        /// <param name="supportCommentRepository">The support comment repository.</param>
        /// <param name="userSessionManager">The user session manager.</param>
        /// <param name="cacheService">The cache service.</param>
        public SupportCommentDS(ISupportCommentRepository supportCommentRepository, IUserSessionManager userSessionManager) : base(supportCommentRepository) {
            _supportCommentRepository = supportCommentRepository;
            _userSessionManager = userSessionManager;
        }
        #endregion Constructor

        /// <summary>Adds the comment list based on operation type of each comment item.</summary>
        /// <param name="supportId">Parent support ticket id.</param>
        /// <param name="supportCommentDTOList">The updated support comment list.</param>
        /// /// <param name="parentOpType">If None commit all comment list changes into persistance storage like database otherwise caller method has to commit all changes.</param>
        /// <returns>Return true if all comment items are add or update or delete (as per operation type).</returns>
        /// <exception cref="ewApps.Core.ExceptionService.EwpSecurityException">Raise security exception if login user doesn't have permission to manage support ticket.</exception>
        public bool ManageCommentList(Guid supportId, List<SupportCommentDTO> supportCommentDTOList, AppPortal.Common.SupportLevelEnum supportLevel, OperationType parentOpType = OperationType.None) {
            UserSession userSession = _userSessionManager.GetSession();

            for(int i = 0; supportCommentDTOList != null && i < supportCommentDTOList.Count; i++) {
                // if( Check Permission for UpdateSupportTicket. ) {

                // ToDo: // Map SupportCommentDTO to SupportComment entity using AutoMapper
                SupportComment supportComment = null;

                if(supportCommentDTOList[i].OperationType == (int)OperationType.Add) {
                    supportComment = new SupportComment();
                    supportComment.SupportId = supportId;
                    supportComment.CommentText = supportCommentDTOList[i].CommentText;
                    supportComment.CreatorId = userSession.TenantUserId;
                    // ToDo: Add user level in user session.
                    supportComment.CreatorLevel = (short)supportLevel;

                    UpdateSystemFieldsByOpType(supportComment, OperationType.Add);
                    Add(supportComment);
                }
                else if(supportCommentDTOList[i].OperationType == (int)OperationType.Update) {
                    supportComment = Get(supportCommentDTOList[i].CommentId);
                    supportComment.CommentText = supportCommentDTOList[i].CommentText;

                    UpdateSystemFieldsByOpType(supportComment, OperationType.Update);
                    Update(supportComment, supportComment.ID);
                }
                else if(supportCommentDTOList[i].OperationType == (int)OperationType.Delete) {
                    supportComment = Get(supportCommentDTOList[i].CommentId);

                    UpdateSystemFieldsByOpType(supportComment, OperationType.Delete);
                    Delete(supportComment);
                }

                if(parentOpType == OperationType.None) {
                    Save();
                }
            }
            return true;
        }

        /// <inheritdoc/>
        public IEnumerable<SupportCommentDTO> GetCommentListBySupportId(Guid supportId) {
            return _supportCommentRepository.GetCommentListBySupportId(supportId);
        }         
    }
}
