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
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.ExceptionService;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Http;
using AppPortalConstants = ewApps.AppPortal.Common.AppPortalConstants;
using SupportLevelEnum = ewApps.AppPortal.Common.SupportLevelEnum;

namespace ewApps.AppPortal.DS {
    public class SupportTicketDSNew:BaseDS<SupportTicket>, ISupportTicketDSNew {

        #region Local member

        protected string _defaultPrefix = "TKT";

        private ISupportTicketRepository _supportTicketRepository;
        private IUniqueIdentityGeneratorDS _identityDS = null;
        private ISupportCommentDS _supportCommentDS;
        private ILevelTransitionHistoryDS _levelTransitionDS;
        private IUserSessionManager _userSessionManager;
        private IDMDocumentDS _documentDS;
        private ISupportTicketAssigneeHelper _supportTicketAssigneeHelper;
        private SupportLevelEnum _supportLevel;


        // IMapper _mapper;

        #endregion Local member

        public SupportTicketDSNew(IUniqueIdentityGeneratorDS identityDS, ISupportTicketRepository supportTicketRepository, ISupportCommentDS supportCommentDS,
            ILevelTransitionHistoryDS levelTransitionDS, IUserSessionManager userSessionManager,
            IDMDocumentDS documentDS, ISupportTicketAssigneeHelper supportTicketAssigneeHelper)
            : base(supportTicketRepository) {
            //_mapper = mapper;
            _supportTicketRepository = supportTicketRepository;
            _supportCommentDS = supportCommentDS;
            _levelTransitionDS = levelTransitionDS;
            _userSessionManager = userSessionManager;
            _identityDS = identityDS;
            _documentDS = documentDS;
            _supportTicketAssigneeHelper = supportTicketAssigneeHelper;
            _supportLevel = GetMappedSupportLevel((UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), _userSessionManager.GetSession().UserType.ToString(), true));
        }

        public SupportLevelEnum AppSupportLevel {
            get; set;
        }

        //public AppKeyEnum ApplicationKey {
        //    get;
        //    set;
        //}

        // In Use
        public SupportTicket AddSupportTicket(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel, AddUpdateDocumentModel documentModel, HttpRequest httpRequest) {
            SupportTicket supportTicket = new SupportTicket();

            SupportSession userSession = GetSession();
            supportTicket.AppKey = supportTicketDTO.AppKey; //ApplicationKey.ToString();
            supportTicket.CreatorId = userSession.AppUserId;
            supportTicket.CurrentLevel = (short)GetNextLevel(supportLevel);
            supportTicket.GenerationLevel = (short)supportLevel;
            supportTicket.Priority = supportTicketDTO.Priority;
            supportTicket.Status = (short)AppPortal.Common.SupportStatusTypeEnum.Open;
            supportTicket.CustomerId = supportTicketDTO.CustomerId;
            supportTicket.Description = supportTicketDTO.Description;
            supportTicket.Title = supportTicketDTO.Title;
            supportTicket.IdentityNumber = GenerateTicketNumber();
      supportTicket.PortalId = supportTicketDTO.PortalId;
      supportTicket.AppId = supportTicketDTO.AppId;
      supportTicket.PublisherTenantId = supportTicketDTO.PublisherTenantId;
      supportTicket.BusinessTenantId = supportTicketDTO.BusinessTenantId;
      supportTicket.BusinessPartnerTenantId = supportTicketDTO.BusinessPartnerTenantId;


      ValidateSupportTicketOnAddUpdate(supportTicket);

            // Check if login user has Add support ticket permission.
            HasPermission(SupportPermissionTypeEnum.Add, supportTicket);

            // Update System fields.
            UpdateSystemFieldsByOpType(supportTicket, OperationType.Add);

            // Add support ticket entity into database.
            supportTicket = Add(supportTicket);

            if(supportTicketDTO.SupportCommentList.Count > 0) {
                _supportCommentDS.ManageCommentList(supportTicket.ID, supportTicketDTO.SupportCommentList, supportLevel, OperationType.Add);
            }

            // Create level transition history.
            LevelTransitionHistory levelTransitionHistory = new LevelTransitionHistory();
            levelTransitionHistory.AppKey = supportTicketDTO.AppKey;// ApplicationKey.ToString();
            levelTransitionHistory.SourceLevel = supportTicket.GenerationLevel;
            levelTransitionHistory.TargetLevel = supportTicket.CurrentLevel;
            levelTransitionHistory.Status = (short)AppPortal.Common.SupportStatusTypeEnum.Open;
            levelTransitionHistory.SupportId = supportTicket.ID;

            // Add an entry in LevelTransitionHistory
            _levelTransitionDS.Add(levelTransitionHistory);

            documentModel.DocOwnerEntityId = supportTicket.ID;

            _documentDS.AddDocumentToStorage(documentModel, httpRequest);

            Save();

            return supportTicket;
        }

        // In Use
        public bool UpdateSupportTicket(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel, AddUpdateDocumentModel documentModel = null, HttpRequest httpRequest = null) {
            bool levelChange = false, statusChange = false;

            SupportTicket savedSupportTicket = Get(supportTicketDTO.SupportTicketId.Value);

            // Map SupportTicket from SupportAddUpdateDTO
            SupportTicket changedSupportTicket = Get(supportTicketDTO.SupportTicketId.Value);
            // string appKey = GetAppKeyBySupportLevel(supportLevel);
            // SupportSession userSession = GetSession("");
            changedSupportTicket.Title = supportTicketDTO.Title;
            changedSupportTicket.Description = supportTicketDTO.Description;
            changedSupportTicket.CurrentLevel = supportTicketDTO.CurrentLevel;
            changedSupportTicket.Priority = supportTicketDTO.Priority;
            changedSupportTicket.Status = supportTicketDTO.Status;

            short oldStatus = GetOrignalValue<short>(changedSupportTicket.ID, SupportTicket.StatusPropertyName);

            if(oldStatus != changedSupportTicket.Status) {
                statusChange = true;
                CheckStatusChangePermission(changedSupportTicket, savedSupportTicket);
            }

            short oldCurrentLevel = GetOrignalValue<short>(changedSupportTicket.ID, SupportTicket.CurrentLevelPropertyName);

            if(oldCurrentLevel != changedSupportTicket.CurrentLevel) {
                levelChange = true;
                ValidateLevelTransition(changedSupportTicket, savedSupportTicket);
            }

            UpdateSystemFieldsByOpType(changedSupportTicket, OperationType.Update);

            Update(changedSupportTicket, changedSupportTicket.ID);

            if(supportTicketDTO.SupportCommentList.Count > 0) {
                //List<SupportCommentDTO> commentList = new List<SupportCommentDTO>();
                //commentList.AddRange(supportTicketDTO.SupportCommentList);
                _supportCommentDS.ManageCommentList(changedSupportTicket.ID, supportTicketDTO.SupportCommentList, _supportLevel, OperationType.Update);
            }

            if(levelChange || statusChange) {
                // Create level transition history.
                LevelTransitionHistory levelTransitionHistory = new LevelTransitionHistory();
                levelTransitionHistory.AppKey = supportTicketDTO.AppKey;//                ApplicationKey.ToString();
                levelTransitionHistory.SourceLevel = oldCurrentLevel;
                levelTransitionHistory.TargetLevel = changedSupportTicket.CurrentLevel;
                levelTransitionHistory.Status = (short)changedSupportTicket.Status;
                levelTransitionHistory.SupportId = savedSupportTicket.ID;

                // Add an entry in LevelTransitionHistory
                _levelTransitionDS.Add(levelTransitionHistory);
            }

            documentModel.DocOwnerEntityId = changedSupportTicket.ID;

            _documentDS.AddDocumentToStorage(documentModel, httpRequest);
            Save();

            return true;
        }

        // In Use
        public async Task< SupportTicketDetailDTO> GetSupportTicketDetailById(Guid supportId, bool includeDeleted, CancellationToken token = default(CancellationToken)) {
            SupportSession supportSession = GetSession("");
            // Get Support Ticket Detail
            SupportTicketDetailDTO supportTicketDetail = _supportTicketRepository.GetSupportTicketDetailById(supportId, includeDeleted, token);

            supportTicketDetail.RequesterSupportLevel = (int)AppSupportLevel;

            // Fill Assignee List
            supportTicketDetail.AssigneeList = await _supportTicketAssigneeHelper.FillAssigneeList(supportTicketDetail, supportSession.SupportLevel);

            // Get Comment List
            supportTicketDetail.SupportCommentList = _supportCommentDS.GetCommentListBySupportId(supportId);

            supportTicketDetail.SupportCommentList = UpdateCommentorName(supportTicketDetail, supportTicketDetail.SupportCommentList);

            //Get document list.
            supportTicketDetail.DocumentList = _documentDS.GetDocumentsByTicketId(supportId);

            return supportTicketDetail;
        }

        // In Use
        public async Task <List<SupportMyTicketDTO>> GetUserSupportTicketByCreatorAndPartnerAndTenantId(string appKey, Guid tenantId, int generationLevel, Guid creatorId, Guid? partnerId, bool onlyDeleted, CancellationToken token = default(CancellationToken)) {
            List<SupportMyTicketDTO> supportTicketList = await _supportTicketRepository.GetUserSupportTicketByCreatorAndCustomerAndTenantId(appKey, tenantId, generationLevel, creatorId, partnerId, onlyDeleted, token);

            for(int i = 0; i < supportTicketList.Count; i++) {
                supportTicketList[i].AssigneeName = await _supportTicketAssigneeHelper.GetAssigneeName(supportTicketList[i].CustomerId, supportTicketList[i].TenantId, supportTicketList[i].CreaterFullName, supportTicketList[i].CurrentLevel, supportTicketList[i].GenerationLevel);
            }
            return supportTicketList;
        }

        public void Delete(Guid supportId) {
            SupportTicket supportTicket = Get(supportId);
            supportTicket.Deleted = true;
            UpdateSystemFieldsByOpType(supportTicket, OperationType.Delete);
            _supportTicketRepository.Update(supportTicket, supportTicket.ID);
            Save();
        }

    ///// <inheritdoc/>
    //public List<SupportTicketDTO> GetOtherLevelTicketList(SupportLevelEnum requesterLevel, string appKey, Guid tenantId, bool onlyDeleted) {
    //    // Get all support ticket by Application Id and Tenant Id and GenerationLevel=1
    //    List<SupportTicketDTO> supportTicketList = _supportTicketRepository.GetSupportTicketByAppAndTenantIdAndLevel(appKey, tenantId, (short)requesterLevel, onlyDeleted);
    //    for(int i = 0; i < supportTicketList.Count; i++) {
    //        //supportTicketList[i].AssigneeName = _supportTicketAssigneeHelper.GetAssigneeName(supportTicketList[i]);
    //        supportTicketList[i].AssigneeName = _supportTicketAssigneeHelper.GetAssigneeName(supportTicketList[i].CustomerId, supportTicketList[i].TenantId, supportTicketList[i].CreaterFullName, supportTicketList[i].CurrentLevel, supportTicketList[i].GenerationLevel);
    //    }
    //    return supportTicketList;
    //}


    public async Task<List<SupportTicketDTO>> GetSupportTicketAssignedToLevel2CustomerList(Guid level2TenantId, bool includeDeleted, int generationLevel, CancellationToken token = default(CancellationToken))
    {
      List<SupportTicketDTO> supportTicketList = _supportTicketRepository.GetSupportTicketAssignedToLevel2CustomerList(level2TenantId, includeDeleted, generationLevel, token);

      for (int i = 0; i < supportTicketList.Count; i++)
      {       
        supportTicketList[i].AssigneeName = await _supportTicketAssigneeHelper.GetAssigneeName(supportTicketList[i].CustomerId, supportTicketList[i].TenantId, supportTicketList[i].CreaterFullName, supportTicketList[i].CurrentLevel, supportTicketList[i].GenerationLevel);
      }
      return supportTicketList;
    }

    public async Task<List<SupportTicketDTO>> GetSupportTicketAssignedToLevel2BusinessList(Guid level2TenantId, bool includeDeleted, int generationLevel, CancellationToken token = default(CancellationToken))
    {
      List<SupportTicketDTO> supportTicketList = _supportTicketRepository.GetSupportTicketAssignedToLevel2BusinessList( level2TenantId, includeDeleted, generationLevel, token);

      for (int i = 0; i < supportTicketList.Count; i++)
      {       
        supportTicketList[i].AssigneeName = await _supportTicketAssigneeHelper.GetAssigneeName(supportTicketList[i].CustomerId, supportTicketList[i].TenantId, supportTicketList[i].CreaterFullName, supportTicketList[i].CurrentLevel, supportTicketList[i].GenerationLevel);
      }
      return supportTicketList;
    }

    // New
    public async Task <List<SupportTicketDTO>> GetSupportTicketAssignedToLevel2List(string appKey, Guid level2TenantId, bool includeDeleted, CancellationToken token = default(CancellationToken)) {
            List<SupportTicketDTO> supportTicketList = _supportTicketRepository.GetSupportTicketAssignedToLevel2List(appKey, level2TenantId, includeDeleted, token);

            for(int i = 0; i < supportTicketList.Count; i++) {                
                supportTicketList[i].AssigneeName = await _supportTicketAssigneeHelper.GetAssigneeName(supportTicketList[i].CustomerId, supportTicketList[i].TenantId, supportTicketList[i].CreaterFullName, supportTicketList[i].CurrentLevel, supportTicketList[i].GenerationLevel);
            }
            return supportTicketList;
        }

        // New
        public async Task<List<SupportTicketDTO>> GetSupportTicketAssignedToLevel3List(Guid level3TenantId, bool includeDeleted, CancellationToken token = default(CancellationToken)) {
            List<SupportTicketDTO> supportTicketList = _supportTicketRepository.GetSupportTicketAssignedToLevel3List(level3TenantId, includeDeleted, token);

            for(int i = 0; i < supportTicketList.Count; i++) {               
                supportTicketList[i].AssigneeName =await _supportTicketAssigneeHelper.GetAssigneeName(supportTicketList[i].CustomerId, supportTicketList[i].TenantId, supportTicketList[i].CreaterFullName, supportTicketList[i].CurrentLevel, supportTicketList[i].GenerationLevel);
            }
            return supportTicketList;
        }


        public async Task<List<SupportTicketDTO>>GetSupportTicketAssignedToLevel4List(Guid level4TenantId, bool includeDeleted, CancellationToken token = default(CancellationToken)) {
            List<SupportTicketDTO> supportTicketList = _supportTicketRepository.GetSupportTicketAssignedToLevel4List(level4TenantId, includeDeleted, token);

      for(int i = 0; i < supportTicketList.Count; i++) {               
                supportTicketList[i].AssigneeName = await _supportTicketAssigneeHelper.GetAssigneeName(supportTicketList[i].CustomerId, supportTicketList[i].TenantId, supportTicketList[i].CreaterFullName, supportTicketList[i].CurrentLevel, supportTicketList[i].GenerationLevel);
            }
            return supportTicketList;
        }


        public int SupportTicketAssignedToLevel3(Guid supportTicketId, int level) {
            return _supportTicketRepository.SupportTicketAssignedToLevel3(supportTicketId, level);
        }

       public async Task <BusinessSupportNotificationDTO> GetBusinessSupportNotificationData(SupportAddUpdateDTO supportTicketDTO, CancellationToken token = default(CancellationToken))
{
      // Get BusinessSupportNotificationDTO 
      BusinessSupportNotificationDTO businessSupportNotificationDTO = _supportTicketRepository.GetBusinessSupportNotificationData((Guid)supportTicketDTO.SupportTicketId, false, token);
      return businessSupportNotificationDTO;

    }

        #region Virtual Methods

        /// <summary>
        /// Gets informtation of support user session.
        /// </summary>
        /// <returns>Returns currnet support user session information.</returns>
        /// <remarks>This implementation returns null and should be override by inherited class to get actual support user session information.</remarks>
        public virtual SupportSession GetSession(string appKey = "") {
            UserSession userSession = _userSessionManager.GetSession();
            SupportSession supportSession = new SupportSession(); // Map UserSessoin to SupportSession.

            // ToDo: Add mapping using automapper.
            supportSession.AppId = userSession.AppId;
            supportSession.AppUserId = userSession.TenantUserId;
            supportSession.ID = userSession.ID;
            supportSession.IdentityToken = userSession.IdentityToken;
            supportSession.TenantId = userSession.TenantId;
            supportSession.TenantName = userSession.TenantName;
            supportSession.UserName = userSession.UserName;
            supportSession.UserType = userSession.UserType;
            supportSession.SupportLevel = GetMappedSupportLevel((UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), supportSession.UserType.ToString(), true));

            return supportSession;
        }


        /// <summary>
        /// Check status change permission based on input changed and already saved instances of <see cref="SupportTicket"/>.
        /// </summary>
        /// <param name="changedSupportTicket">An instance of <see cref="SupportTicket"/> with changed values.</param>
        /// <param name="savedSupportTicket">An instance of <see cref="SupportTicket"/> with old saved values.</param>
        public virtual void CheckStatusChangePermission(SupportTicket changedSupportTicket, SupportTicket savedSupportTicket) {
            // Validate Status
            if(changedSupportTicket.Status == (short)SupportStatusTypeEnum.OnHold) {
                // If user is updating existing support ticket, validate Permission_Type.Update
                if(HasPermission(SupportPermissionTypeEnum.Update, changedSupportTicket) == false) {
                    RaiseSecurityException("");
                }
            }
            else if(changedSupportTicket.Status == (short)SupportStatusTypeEnum.Resolved) {
                // If user change Ticket_State like from Pending to Resolve check validate Permission_Type.Resolve
                if(HasPermission(SupportPermissionTypeEnum.Resolve, changedSupportTicket) == false) {
                    RaiseSecurityException("");
                }
            }
            // If user change Ticket_State like from any state to Close check validate Permission_Type.Close
            else if(changedSupportTicket.Status == (short)SupportStatusTypeEnum.Closed) {
                if(HasPermission(SupportPermissionTypeEnum.Close, changedSupportTicket) == false) {
                    RaiseSecurityException("");
                }
            }
        }


        /// <summary>
        /// This method checks login user's authorization of mention permission type.
        /// </summary>
        /// <param name="permissionEnum">Permission type to be authorize.</param>
        /// <param name="changedSupportTicket">Support ticket enttiy, updated by user.</param>
        /// <returns>Returns true if login user has permission other raise security exception.</returns>
        public virtual bool HasPermission(SupportPermissionTypeEnum permissionEnum, SupportTicket changedSupportTicket) {


            SupportSession supportSession = GetSession();
            bool hasPermission = false;
            //bool userBelongToLevel = false;
            Guid oldCreator = GetOrignalValue<Guid>(changedSupportTicket.ID, SupportTicket.CreatorIdPropertyName);
            short oldCurrentLevel = GetOrignalValue<short>(changedSupportTicket.ID, SupportTicket.CurrentLevelPropertyName);

            switch(permissionEnum) {
                case SupportPermissionTypeEnum.Add:
                    // Any user belong to Level1 / Level2/ Level3 can add SupportTicket.
                    if(supportSession.SupportLevel != SupportLevelEnum.Level4) {
                        hasPermission = true;
                    }
                    break;

                case SupportPermissionTypeEnum.Resolve:
                case SupportPermissionTypeEnum.Comment:
                case SupportPermissionTypeEnum.Close:
                    // Owner can resolve ticket.
                    bool everAssignedToLevel = false;
                    if(supportSession.AppUserId.Equals(oldCreator)) {
                        hasPermission = true;
                    }
                    else {
                        //   userBelongToLevel = UserBelongToLevel(supportSession.AppUserId, (SupportLevelEnum)requesterSupportLevel);
                        // Is Ticket assigned to higher level.
                        everAssignedToLevel = TicketAssignedToLevel(changedSupportTicket.ID, supportSession.SupportLevel);
                        // ToDo: Please Review this permission check.
                        // hasPermission = true;
                        if(everAssignedToLevel && oldCurrentLevel == (short)supportSession.SupportLevel) {
                            hasPermission = true;
                        }
                    }
                    break;

                case SupportPermissionTypeEnum.Update:
                    // Is Ticket assigned to higher level.
                    everAssignedToLevel = TicketAssignedToLevel(changedSupportTicket.ID, supportSession.SupportLevel);
                    // ToDo: Please Review this permission check.
                    // hasPermission = true;
                    if(everAssignedToLevel) {
                        hasPermission = true;
                    }
                    break;

                case SupportPermissionTypeEnum.Delete:
                case SupportPermissionTypeEnum.Ownership:
                    // Only owner can delete ticket in any status.
                    if(supportSession.AppUserId.Equals(oldCreator)) {
                        hasPermission = true;
                    }
                    break;

                case SupportPermissionTypeEnum.StatusChange:
                    // Status changed to Resolve & Close will not consider here.
                    // 1) If ticket assigned to current level and login user belong to his level.
                    // userBelongToLevel = UserBelongToLevel(supportSession.AppUserId, requesterSupportLevel);
                    if(oldCurrentLevel == (short)supportSession.SupportLevel) {
                        hasPermission = true;
                    }
                    break;
                case SupportPermissionTypeEnum.View:
                    // Any support user belong to higher level can view if ticket is assigned to his level any time during life span.
                    // Owner can view.
                    // SupportLevelEnum nextLevel = GetNextLevel(requesterSupportLevel);
                    hasPermission = TicketAssignedToLevel(changedSupportTicket.ID, supportSession.SupportLevel);
                    break;
            }

            if(hasPermission) {
                return true;
            }
            else {
                throw new EwpSecurityException(string.Format("User doesn't have {0} permission  on support ticket", permissionEnum.ToString()));
            }

        }

        private bool TicketAssignedToLevel(Guid supportTicketId, SupportLevelEnum supportLevel) {
            // Check an entry of support ticket for TargetLevel.
            return true;
        }

        private void RaiseSecurityException(string errorMessage, IList<EwpErrorData> errorDataList = null) {
            throw new EwpSecurityException(errorMessage, errorDataList);
        }

        private bool ValidateSupportTicketOnAddUpdate(SupportTicket supportTicket) {
            bool valid = true;

            return valid;
        }


        /// <summary>
        /// Generates a new ticket number based on previously generated ticket number.
        /// </summary>
        /// <returns>Returns newly generated ticket number.</returns>
        public virtual string GenerateTicketNumber() {
            //Comment Atul
            int lastSupportTicketSequence = _identityDS.GetIdentityNo(Guid.Empty, (int)AppPortalEntityTypeEnum.SupportTicket, _defaultPrefix, 100001);
            return string.Format("{0}{1}", _defaultPrefix, lastSupportTicketSequence);
            //return null;
        }

        ///<inheritdoc/>
        public virtual SupportLevelEnum GetMappedSupportLevel(UserTypeEnum userType) {
            SupportLevelEnum supportLevel = SupportLevelEnum.None;
            switch(userType) {
                case UserTypeEnum.Platform:
                    supportLevel = SupportLevelEnum.Level4;
                    break;
                case UserTypeEnum.Publisher:
                    supportLevel = SupportLevelEnum.Level3;
                    break;
                case UserTypeEnum.Business:
                    supportLevel = SupportLevelEnum.Level2;
                    break;
                case UserTypeEnum.Customer:
                    supportLevel = SupportLevelEnum.Level1;
                    break;
            }
            return supportLevel;
        }

        /// <summary>
        /// Validates support ticket level transition from one level to another level.
        /// </summary>
        /// <param name="changedSupportTicket">An instance of <see cref="SupportTicket"/> with changed values.</param>
        /// <param name="savedSupportTicket">An instance of <see cref="SupportTicket"/> with old saved values.</param>
        public virtual void ValidateLevelTransition(SupportTicket changedSupportTicket, SupportTicket savedSupportTicket) {
            SupportSession supportSession = GetSession();
            #region Validate New Support Level

            // Add Ticket Case
            if(savedSupportTicket == null) {
                // get next level
                SupportLevelEnum nextLevel = GetNextLevel(supportSession.SupportLevel);
                // In Add case ticket only escalate to next level.
                if(changedSupportTicket.CurrentLevel != (short)nextLevel) {
                    // Raise exception.
                }
            }
            else {
                // If level changed by user
                if(changedSupportTicket.CurrentLevel != savedSupportTicket.CurrentLevel) {
                    SupportLevelEnum oldSupportLevel = (SupportLevelEnum)Enum.Parse(typeof(SupportLevelEnum), savedSupportTicket.CurrentLevel.ToString(), true);
                    SupportLevelEnum newSupportLevel = (SupportLevelEnum)Enum.Parse(typeof(SupportLevelEnum), savedSupportTicket.CurrentLevel.ToString(), true);

                    // Get next level
                    SupportLevelEnum nextLevel = GetNextLevel(oldSupportLevel);

                    // Get previous level
                    SupportLevelEnum previousLevel = GetPreviousLevel(oldSupportLevel);

                    if(newSupportLevel != nextLevel && newSupportLevel != previousLevel) {
                        // Raise exception.
                    }
                }
            }
        }

        /// <summary>
        /// Gets next support level of input support level.
        /// </summary>
        /// <param name="currentLevel">Current support level.</param>
        /// <returns>Return next support level of input support level.</returns>  
        public virtual SupportLevelEnum GetNextLevel(SupportLevelEnum currentLevel) {
            switch(currentLevel) {
                case SupportLevelEnum.Level4:
                    return SupportLevelEnum.None;
                case SupportLevelEnum.Level3:
                    return SupportLevelEnum.Level4;
                case SupportLevelEnum.Level2:
                    return SupportLevelEnum.Level3;
                case SupportLevelEnum.Level1:
                    return SupportLevelEnum.Level2;
            }
            return SupportLevelEnum.None;
        }

        /// <summary>
        /// Gets previous support level of input support level.
        /// </summary>
        /// <param name="currentLevel">Current support level.</param>
        /// <returns>Return previous support level of input support level.</returns>
        public virtual SupportLevelEnum GetPreviousLevel(SupportLevelEnum currentLevel) {
            switch(currentLevel) {
                case SupportLevelEnum.Level2:
                    return SupportLevelEnum.Level1;
                case SupportLevelEnum.Level1:
                    return SupportLevelEnum.None;
                case SupportLevelEnum.Level3:
                    return SupportLevelEnum.Level2;
                case SupportLevelEnum.Level4:
                    return SupportLevelEnum.Level3;
            }
            return SupportLevelEnum.None;
        }



        #endregion


        #endregion

        public IEnumerable<SupportCommentDTO> UpdateCommentorName(SupportTicketDetailDTO supportTicketDTO, IEnumerable<SupportCommentDTO> commentorList) {
            if(commentorList != null) {
                foreach(SupportCommentDTO commentItem in commentorList) {
                    commentItem.CommentorName = GetCommentorName(commentItem.CreatorLevel, supportTicketDTO.GenerationLevel, supportTicketDTO.CreatedByName, supportTicketDTO.TenantName, supportTicketDTO.PublisherName);
                }
            }
            return commentorList;
        }

        // Gets assignee name against support level.
        public string GetCommentorName(short commentGenerationLevel, short ticketGenerationLevel, string ticketCreatedBy, string tenantName, string publisherName) {
            SupportLevelEnum supportLevel = (SupportLevelEnum)Enum.Parse(typeof(SupportLevelEnum), commentGenerationLevel.ToString());
            string name = "";
            switch(supportLevel) {
                case SupportLevelEnum.Level1:
                    name = ticketCreatedBy;
                    break;
                case SupportLevelEnum.Level2:
                    if(ticketGenerationLevel == (short)SupportLevelEnum.Level2) {
                        name = ticketCreatedBy;
                    }
                    else {
                        name = tenantName;
                    }
                    break;
                case SupportLevelEnum.Level3:
                    if(ticketGenerationLevel == (short)SupportLevelEnum.Level1) {
                        name = publisherName;
                    }
                    else if(ticketGenerationLevel == (short)SupportLevelEnum.Level2) {
                        name = publisherName;
                    }
                    else if(ticketGenerationLevel == (short)SupportLevelEnum.Level3) {
                        name = ticketCreatedBy;
                    }
                    break;
                case SupportLevelEnum.Level4:
                    name = AppPortalConstants.SuperAdminKey;
                    break;
                default:
                    name = "";
                    break;
            }
            return name;
        }
    }
}
