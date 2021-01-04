///* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
// * Unauthorized copying of this file, via any medium is strictly prohibited
// * Proprietary and confidential
// * 
// * Author: Pulkit Agarwal <pagrawal@eworkplaceapps.com>
// * Date: 19 December 2018
// * 
// * Contributor/s: Nitin Jain
// * Last Updated On: 19 December 2018
// */

//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;
//using AutoMapper;
//using ewApps.Support.Common;
//using ewApps.Core.Data;
//using ewApps.Core.DTO;
//using ewApps.Core.Entity;
//using ewApps.Core.ExceptionService;
//using ewApps.Core.UserSessionService;
//using ewApps.Support.Common;
//using ewApps.Support.DTO;
//using ewApps.Support.Entity;
//using Microsoft.AspNetCore.Http;
//using ewApps.Core.Common;
//using ewApps.Support.Data;
//using SupportLevelEnum = ewApps.Support.Common.SupportLevelEnum;
//using ewApps.AppPortal.DS;

//namespace ewApps.Core.DS {
//    /// <summary>
//    /// This class implements standard business logic and operations for support Ticket.
//    /// </summary>
//    public class SupportTicketDS:BaseDS<SupportTicket>, ISupportTicketDS {

//        #region Local member
//        protected string _appKey = "";
//        protected string _defaultPrefix = "TKT";

//        private ISupportTicketRepository _supportTicketRepository;
//        private IIdentityDS _identityDS = null;
//        private ISupportCommentDS _supportCommentDS;
//        private ILevelTransitionHistoryDS _levelTransitionDS;
//        private IUserSessionManager _userSessionManager;
//        private IDMDocumentDS _documentDS;
//        private ISupportTicketAssigneeHelper _supportTicketAssigneeHelper;

//        IMapper _mapper;

//        #endregion Local member

//        #region Constructor

//        /// <summary>
//        /// Initializes the member variables and dependencies.
//        /// </summary>
//        /// <param name="identityDS">An instance of <see cref="IIdentityDS"/> to generates support ticket number.</param>
//        /// <param name="supportTicketRepository">An instance of <see cref="ISupportTicketRepository"/> to communicate with storage.</param>
//        /// <param name="supportCommentDS">An instance of <see cref="ISupportCommentDS"/> to perform <see cref="SupportComment"/> related operations.</param>
//        /// <param name="levelTransitionDS">An instance of <see cref="ILevelTransitionHistoryDS"/> to perform <see cref="LevelTransitionHistory> related operations."/></param>
//        /// <param name="userSessionManager">An instance of <see cref="IUserSessionManager"/> to get requester user session information.</param>
//        /// <param name="cacheService">An instance of <see cref="ICacheService"/> to get and set data to/from Cache.</param>
//        /// <param name="mapper">An instance of <see cref="IMapper"/> to get mapping defination between entities.</param>
//        public SupportTicketDS(IIdentityDS identityDS, ISupportTicketRepository supportTicketRepository, ISupportCommentDS supportCommentDS, ILevelTransitionHistoryDS levelTransitionDS, IUserSessionManager userSessionManager, ICacheService cacheService, IMapper mapper, IDMDocumentDS documentDS, ISupportTicketAssigneeHelper supportTicketAssigneeHelper) : base(supportTicketRepository, cacheService) {
//            _mapper = mapper;
//            _supportTicketRepository = supportTicketRepository;
//            _supportCommentDS = supportCommentDS;
//            _levelTransitionDS = levelTransitionDS;
//            _userSessionManager = userSessionManager;
//            _identityDS = identityDS;
//            _documentDS = documentDS;
//            _supportTicketAssigneeHelper = supportTicketAssigneeHelper;
//        }
//        #endregion Constructor


//        #region Abstract Methods

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        public virtual SupportApp GetSupportAppModel() {
//            return null;
//        }

//        /// <summary>
//        /// Gets List of <see cref="SupportUser"/> belong to requested support level.
//        /// </summary>
//        /// <param name="supportLevel">Support level to get support users.</param>
//        /// <returns>Return list of all support users that belong to requested support level.</returns>
//        /// <remarks>This implementation returns null and should be override by inherited class to get actual list of support users.</remarks>
//        public virtual List<SupportUser> GetSupportUsersByLevel(SupportLevelEnum supportLevel) {
//            return null;
//        }

//        /// <summary>
//        /// Gets informtation of support user session.
//        /// </summary>
//        /// <returns>Returns currnet support user session information.</returns>
//        /// <remarks>This implementation returns null and should be override by inherited class to get actual support user session information.</remarks>
//        public virtual SupportSession GetSession(string appKey = "") {
//            return null;
//        }

//        /// <summary>
//        /// Defines mapping between user type and support level.
//        /// </summary>
//        /// <param name="userType">User type to get corresponding support level.</param>
//        /// <returns>Returns support level corresponding to input user type.</returns>
//        public virtual Support.Common.SupportLevelEnum GetMappedSupportLevel(UserTypeEnum userType) {
//            return SupportLevelEnum.None;
//        }
//        #endregion

//        #region Add/Update/Delete Methods

//        /// <summary>
//        /// This metohd is for internal use and direct use will raise <see cref="NotImplementedException"/> exception.
//        /// </summary>
//        public override Task<SupportTicket> AddAsync(SupportTicket t, CancellationToken token = default(CancellationToken)) {
//            throw new NotImplementedException();
//        }

//        /// <summary>
//        /// This method validates the inpurt <paramref name="supportTicket"/> information to aginst defined permissions and business rules and add it.
//        /// </summary>
//        /// <param name="supportTicket">Support ticket information to be add.</param>
//        /// <returns>Returns an instance of <see cref="SupportTicket"/> with newly added ticket information.</returns>
//        public async virtual Task<SupportTicket> AddAsync(SupportTicket supportTicket) {
//            // Check Add Support Ticket Permission
//            HasPermission(SupportPermissionTypeEnum.Add, supportTicket);

//            // Validates assignee by calling GetAssigneeList(...) method.
//            ValidateLevelTransition(supportTicket, null);

//            // Generate new ticket number
//            string newTicketNumber = GenerateTicketNumber();

//            supportTicket.IdentityNumber = newTicketNumber;

//            // Add support object in database by calling core ISupportDataService.Add() method.
//            return await base.AddAsync(supportTicket);
//        }

//        /// <summary>
//        /// This metohd is for internal use and direct use will raise <see cref="NotImplementedException"/> exception.
//        /// </summary>
//        public override SupportTicket Update(SupportTicket t, object key) {
//            throw new NotImplementedException();
//        }

//        public virtual void Update(SupportTicket changedSupportTicket) {
//            // Check if status ticket changed.

//            // Get Saved entity.
//            SupportTicket savedSupportTicket = Get(changedSupportTicket.ID);

//            // validate assigned level
//            ValidateLevelTransition(changedSupportTicket, savedSupportTicket);

//            short oldStatus = GetOrignalValue<short>(changedSupportTicket.ID, SupportTicket.StatusPropertyName);

//            // If status = Resolve
//            if(oldStatus != changedSupportTicket.Status && changedSupportTicket.Status == (short)Support.Common.SupportStatusTypeEnum.Resolved) {
//                HasPermission(SupportPermissionTypeEnum.Resolve, changedSupportTicket);
//            }
//            // If status = Close
//            else if(oldStatus != changedSupportTicket.Status && changedSupportTicket.Status == (short)Support.Common.SupportStatusTypeEnum.Closed) {
//                HasPermission(SupportPermissionTypeEnum.Close, changedSupportTicket);
//            }

//            // Check if user has update permission.
//            HasPermission(SupportPermissionTypeEnum.Update, changedSupportTicket);

//            base.Update(changedSupportTicket, changedSupportTicket.ID);
//        }

//        // Delete Method
//        public virtual void Delete(SupportTicket supportTicket) {
//            // Validate permission
//            HasPermission(SupportPermissionTypeEnum.Delete, supportTicket);
//            // Delete support object in database by calling core ISupportDataService.Add() method.
//            supportTicket.Deleted = true;
//            base.Update(supportTicket, supportTicket.ID);
//            Save();
//            //base.Delete(supportTicket);
//        }

//        /// <summary>
//        /// This method adds support ticket and related comments.
//        /// </summary>
//        /// <param name="supportTicketDTO">Support ticket and comment information to be update.</param>
//        /// <param name="supportLevel">Support level where current user belong.</param>
//        /// <returns>Returns saved SupportTicket entity instance with updated information.</returns>
//        /// <exception cref="ewApps.Core.ExceptionService.EwpSecurityException">If login user doesn't have permission to add support ticket, raise security exception.</exception>
//        public SupportTicket AddSupportTicket(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel, AddUpdateDocumentModel documentModel, HttpRequest httpRequest) {
//            SupportTicket supportTicket = new SupportTicket();

//            SupportSession userSession = GetSession();
//            supportTicket.AppKey = _appKey;
//            supportTicket.CreatorId = userSession.AppUserId;
//            supportTicket.CurrentLevel = (short)GetNextLevel(supportLevel);
//            supportTicket.GenerationLevel = (short)supportLevel;
//            supportTicket.Priority = supportTicketDTO.Priority;
//            supportTicket.Status = (short)Support.Common.SupportStatusTypeEnum.Open;
//            supportTicket.CustomerId = supportTicketDTO.CustomerId;
//            supportTicket.Description = supportTicketDTO.Description;
//            supportTicket.Title = supportTicketDTO.Title;
//            supportTicket.IdentityNumber = GenerateTicketNumber();

//            ValidateSupportTicketOnAddUpdate(supportTicket);

//            // Check if login user has Add support ticket permission.
//            HasPermission(SupportPermissionTypeEnum.Add, supportTicket);

//            // Update System fields.
//            UpdateSystemFieldsByOpType(supportTicket, OperationType.Add);

//            // Add support ticket entity into database.
//            supportTicket = Add(supportTicket);

//            if(supportTicketDTO.SupportCommentList.Count > 0) {
//                //List<SupportCommentDTO> commentList = new List<SupportCommentDTO>();
//                //commentList.AddRange(supportTicketDTO.SupportCommentList);
//                _supportCommentDS.ManageCommentList(supportTicket.ID, supportTicketDTO.SupportCommentList, supportLevel, ewApps.Core.Common.OperationType.Add);
//            }

//            // Create level transition history.
//            LevelTransitionHistory levelTransitionHistory = new LevelTransitionHistory();
//            levelTransitionHistory.AppKey = _appKey;
//            levelTransitionHistory.SourceLevel = supportTicket.GenerationLevel;
//            levelTransitionHistory.TargetLevel = supportTicket.CurrentLevel;
//            levelTransitionHistory.Status = (short)Support.Common.SupportStatusTypeEnum.Open;
//            levelTransitionHistory.SupportId = supportTicket.ID;

//            // Add an entry in LevelTransitionHistory
//            _levelTransitionDS.Add(levelTransitionHistory);

//            documentModel.DocOwnerEntityId = supportTicket.ID;

//            _documentDS.AddDocumentToStorage(documentModel, httpRequest);

//            Save();

//            return supportTicket;
//        }

//        /// <summary>
//        /// Updates support ticket information and related comments.
//        /// </summary>
//        /// <param name="supportTicketDTO">Support ticket and comment information to be update.</param>
//        /// <returns>Returns true if all changed information updated sucessfully.</returns>
//        /// <exception cref="ewApps.Core.ExceptionService.EwpSecurityException">If login user doesn't have permission to add support ticket, raise security exception.</exception>
//        /// <remarks cref="SupportAddUpdateDTO"></remarks>
//        public bool UpdateSupportTicket(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel) {
//            bool levelChange = false, statusChange = false;

//            SupportTicket savedSupportTicket = Get(supportTicketDTO.SupportTicketId.Value);

//            // Map SupportTicket from SupportAddUpdateDTO
//            SupportTicket changedSupportTicket = Get(supportTicketDTO.SupportTicketId.Value);
//            string appKey = GetAppKeyBySupportLevel(supportLevel);
//            SupportSession userSession = GetSession(appKey);
//            changedSupportTicket.Title = supportTicketDTO.Title;
//            changedSupportTicket.Description = supportTicketDTO.Description;
//            changedSupportTicket.CurrentLevel = supportTicketDTO.CurrentLevel;
//            changedSupportTicket.Priority = supportTicketDTO.Priority;
//            changedSupportTicket.Status = supportTicketDTO.Status;

//            short oldStatus = GetOrignalValue<short>(changedSupportTicket.ID, SupportTicket.StatusPropertyName);

//            if(oldStatus != changedSupportTicket.Status) {
//                statusChange = true;
//                CheckStatusChangePermission(changedSupportTicket, savedSupportTicket);
//            }

//            short oldCurrentLevel = GetOrignalValue<short>(changedSupportTicket.ID, SupportTicket.CurrentLevelPropertyName);

//            if(oldCurrentLevel != changedSupportTicket.CurrentLevel) {
//                levelChange = true;
//                ValidateLevelTransition(changedSupportTicket, savedSupportTicket);
//            }

//            UpdateSystemFieldsByOpType(changedSupportTicket, OperationType.Update);

//            Update(changedSupportTicket);

//            if(supportTicketDTO.SupportCommentList.Count > 0) {
//                //List<SupportCommentDTO> commentList = new List<SupportCommentDTO>();
//                //commentList.AddRange(supportTicketDTO.SupportCommentList);
//                _supportCommentDS.ManageCommentList(changedSupportTicket.ID, supportTicketDTO.SupportCommentList, supportLevel, OperationType.Update);
//            }

//            if(levelChange || statusChange) {
//                // Create level transition history.
//                LevelTransitionHistory levelTransitionHistory = new LevelTransitionHistory();
//                levelTransitionHistory.AppKey = _appKey;
//                levelTransitionHistory.SourceLevel = oldCurrentLevel;
//                levelTransitionHistory.TargetLevel = changedSupportTicket.CurrentLevel;
//                levelTransitionHistory.Status = (short)changedSupportTicket.Status;
//                levelTransitionHistory.SupportId = savedSupportTicket.ID;

//                // Add an entry in LevelTransitionHistory
//                _levelTransitionDS.Add(levelTransitionHistory);
//            }

//            Save();

//            return true;
//        }

//        /// <summary>
//        /// Deletes support ticket that matches given support ticket id.
//        /// </summary>
//        /// <param name="supportId">A unique support ticket id.</param>
//        public virtual void Delete(Guid supportId) {
//            SupportTicket supportTicket = Get(supportId);
//            Delete(supportTicket);
//        }

//        /// <inheritdoc/>
//        public List<SupportMyTicketDTO> GetUserSupportTicketByCreatorAndCustomerAndTenantId(string appKey, Guid tenantId, int generationLevel, Guid creatorId, Guid? customerId, bool onlyDeleted) {
//            return _supportTicketRepository.GetUserSupportTicketByCreatorAndCustomerAndTenantId(appKey, tenantId, generationLevel, creatorId, customerId, onlyDeleted);
//        }

//        /// <inheritdoc/>
//        public List<SupportTicketDTO> GetLevel2TicketList(string appKey, Guid tenantId, bool onlyDeleted) {
//            // Get all support ticket by Application Id and Tenant Id and GenerationLevel=1
//            List<SupportTicketDTO> supportTicketList = _supportTicketRepository.GetSupportTicketByAppAndTenantIdAndLevel(appKey, tenantId, (short)SupportLevelEnum.Level1, onlyDeleted);
//            for(int i = 0; i < supportTicketList.Count; i++) {
//                supportTicketList[i].AssigneeName = _supportTicketAssigneeHelper.GetAssigneeName(supportTicketList[i].CustomerId, supportTicketList[i].TenantId, supportTicketList[i].CreaterFullName, supportTicketList[i].CurrentLevel, supportTicketList[i].GenerationLevel);
//            }
//            return supportTicketList;
//        }

//        /// <inheritdoc/>
//        public List<SupportTicketDTO> GetLevel3TicketList(bool onlyDeleted) {
//            // Get all support ticket by Application Id and Tenant Id and GenerationLevel=2
//            //List<SupportTicketDTO> supportTicketList = _supportTicketRepository.GetSupportTicketByAppAndTenantIdAndLevel(appKey, Guid.Empty, (short)SupportLevelEnum.Level2, false);

//            List<SupportTicketDTO> escalatedTicketList = _supportTicketRepository.GetSupportTicketByEscalationLevel((short)SupportLevelEnum.Level3, onlyDeleted);

//            for(int i = 0; i < escalatedTicketList.Count; i++) {
//                //escalatedTicketList[i].AssigneeName = _supportTicketAssigneeHelper.GetAssigneeName(escalatedTicketList[i]);
//                escalatedTicketList[i].AssigneeName = _supportTicketAssigneeHelper.GetAssigneeName(escalatedTicketList[i].CustomerId, escalatedTicketList[i].TenantId, escalatedTicketList[i].CreaterFullName, escalatedTicketList[i].CurrentLevel, escalatedTicketList[i].GenerationLevel);
//            }

//            return escalatedTicketList;
//        }
//        /// <inheritdoc/>
//        public List<SupportTicketDTO> GetLevel4TicketList(bool onlyDeleted) {
//            // Get all support ticket by Application Id and Tenant Id and GenerationLevel=2
//            //List<SupportTicketDTO> supportTicketList = _supportTicketRepository.GetSupportTicketByAppAndTenantIdAndLevel(appKey, Guid.Empty, (short)SupportLevelEnum.Level2, false);

//            List<SupportTicketDTO> escalatedTicketList = _supportTicketRepository.GetSupportTicketByEscalationLevel((short)SupportLevelEnum.Level4, onlyDeleted);

//            for(int i = 0; i < escalatedTicketList.Count; i++) {
//                //escalatedTicketList[i].AssigneeName = _supportTicketAssigneeHelper.GetAssigneeName(escalatedTicketList[i]);
//                escalatedTicketList[i].AssigneeName = _supportTicketAssigneeHelper.GetAssigneeName(escalatedTicketList[i].CustomerId, escalatedTicketList[i].TenantId, escalatedTicketList[i].CreaterFullName, escalatedTicketList[i].CurrentLevel, escalatedTicketList[i].GenerationLevel);
//            }

//            return escalatedTicketList;
//        }

//        /// <inheritdoc/>
//        public SupportTicketDetailDTO GetSupportTicketDetailById(Guid supportId, bool includeDeleted) {
//            SupportSession supportSession = GetSession();
//            SupportTicketDetailDTO supportTicketDetail = _supportTicketRepository.GetSupportTicketDetailById(supportId, includeDeleted);
//            //_supportTicketAssigneeHelper.FillAssigneeList(supportTicketDetail,supportSession.SupportLevel);
//            //supportTicketDetail.SupportCommentList = _supportCommentDS.GetCommentListBySupportId(supportId);
//            //supportTicketDetail.DocumentList = _documentDS.GetDocumentsByTicketId(supportId);

//            return supportTicketDetail;
//        }

//        #endregion

//        #region Public Methods

//        /// <summary>
//        /// Generates a new ticket number based on previously generated ticket number.
//        /// </summary>
//        /// <returns>Returns newly generated ticket number.</returns>
//        public virtual string GenerateTicketNumber() {
//            int lastSupportTicketSequence = _identityDS.GetIdentityNo(Guid.Empty, (int)CoreEntityTypeEnum.SupportTicket, _defaultPrefix, 100001);
//            return string.Format("{0}-{1}", _defaultPrefix, lastSupportTicketSequence);
//        }

//        /// <summary>
//        /// Gets previous support level of input support level.
//        /// </summary>
//        /// <param name="currentLevel">Current support level.</param>
//        /// <returns>Return previous support level of input support level.</returns>
//        public virtual SupportLevelEnum GetPreviousLevel(SupportLevelEnum currentLevel) {
//            switch(currentLevel) {
//                case SupportLevelEnum.Level2:
//                    return SupportLevelEnum.Level1;
//                case SupportLevelEnum.Level1:
//                    return SupportLevelEnum.None;
//                case SupportLevelEnum.Level3:
//                    return SupportLevelEnum.Level2;
//                case SupportLevelEnum.Level4:
//                    return SupportLevelEnum.Level3;
//            }
//            return SupportLevelEnum.None;
//        }


//        /// <summary>
//        /// Gets next support level of input support level.
//        /// </summary>
//        /// <param name="currentLevel">Current support level.</param>
//        /// <returns>Return next support level of input support level.</returns>  
//        public virtual SupportLevelEnum GetNextLevel(SupportLevelEnum currentLevel) {
//            switch(currentLevel) {
//                case SupportLevelEnum.Level4:
//                    return SupportLevelEnum.None;
//                case SupportLevelEnum.Level3:
//                    return SupportLevelEnum.Level4;
//                case SupportLevelEnum.Level2:
//                    return SupportLevelEnum.Level3;
//                case SupportLevelEnum.Level1:
//                    return SupportLevelEnum.Level2;
//            }
//            return SupportLevelEnum.None;
//        }

//        /// <inheritdoc/>
//        public int SupportTicketAssignedToLevel3(Guid supportTicketId, int level) {
//            return _supportTicketRepository.SupportTicketAssignedToLevel3(supportTicketId, level);
//        }


//        #endregion

//        #region Validation and Permission Methods


//        /// <summary>
//        /// Validates support ticket entity against defined validation rules.
//        /// 1) 
//        /// </summary>
//        /// <param name="changedSupportTicket">An instance of <see cref="SupportTicket"/> with updated values.</param>
//        /// <returns>Returns true if all validation rules has been passed otherwise raise <see cref="EwpValidationException"/> exception.</returns>
//        /// <exception cref="EwpValidationException">Raise validation exception with failed rules in form of <see cref="EwpError"/>.</exception>
//        public virtual bool Validate(SupportTicket changedSupportTicket) {
//            SupportTicket savedSupportTicket = null;
//            if(changedSupportTicket.ID.Equals(Guid.Empty) == false) {
//                savedSupportTicket = Get(changedSupportTicket.ID);
//            }
//            else {
//                if(changedSupportTicket.Status != (short)Support.Common.SupportStatusTypeEnum.Open) {
//                    // Raise exception;
//                }
//            }

//            // Validate assignee
//            ValidateLevelTransition(changedSupportTicket, savedSupportTicket);

//            if(changedSupportTicket.CurrentLevel != savedSupportTicket.CurrentLevel) {
//                CheckStatusChangePermission(changedSupportTicket, savedSupportTicket);
//            }

//            return true;
//        }

//        /// <summary>
//        /// Check status change permission based on input changed and already saved instances of <see cref="SupportTicket"/>.
//        /// </summary>
//        /// <param name="changedSupportTicket">An instance of <see cref="SupportTicket"/> with changed values.</param>
//        /// <param name="savedSupportTicket">An instance of <see cref="SupportTicket"/> with old saved values.</param>
//        public virtual void CheckStatusChangePermission(SupportTicket changedSupportTicket, SupportTicket savedSupportTicket) {
//            // Validate Status
//            if(changedSupportTicket.Status == (short)SupportStatusTypeEnum.OnHold) {
//                // If user is updating existing support ticket, validate Permission_Type.Update
//                if(HasPermission(SupportPermissionTypeEnum.Update, changedSupportTicket) == false) {
//                    RaiseSecurityException("");
//                }
//            }
//            else if(changedSupportTicket.Status == (short)SupportStatusTypeEnum.Resolved) {
//                // If user change Ticket_State like from Pending to Resolve check validate Permission_Type.Resolve
//                if(HasPermission(SupportPermissionTypeEnum.Resolve, changedSupportTicket) == false) {
//                    RaiseSecurityException("");
//                }
//            }
//            // If user change Ticket_State like from any state to Close check validate Permission_Type.Close
//            else if(changedSupportTicket.Status == (short)SupportStatusTypeEnum.Closed) {
//                if(HasPermission(SupportPermissionTypeEnum.Close, changedSupportTicket) == false) {
//                    RaiseSecurityException("");
//                }
//            }
//        }

//        /// <summary>
//        /// Validates support ticket level transition from one level to another level.
//        /// </summary>
//        /// <param name="changedSupportTicket">An instance of <see cref="SupportTicket"/> with changed values.</param>
//        /// <param name="savedSupportTicket">An instance of <see cref="SupportTicket"/> with old saved values.</param>
//        public virtual void ValidateLevelTransition(SupportTicket changedSupportTicket, SupportTicket savedSupportTicket) {
//            SupportSession supportSession = GetSession();
//            #region Validate New Support Level

//            // Add Ticket Case
//            if(savedSupportTicket == null) {
//                // get next level
//                SupportLevelEnum nextLevel = GetNextLevel(supportSession.SupportLevel);
//                // In Add case ticket only escalate to next level.
//                if(changedSupportTicket.CurrentLevel != (short)nextLevel) {
//                    // Raise exception.
//                }
//            }
//            else {
//                // If level changed by user
//                if(changedSupportTicket.CurrentLevel != savedSupportTicket.CurrentLevel) {
//                    SupportLevelEnum oldSupportLevel = (SupportLevelEnum)Enum.Parse(typeof(SupportLevelEnum), savedSupportTicket.CurrentLevel.ToString(), true);
//                    SupportLevelEnum newSupportLevel = (SupportLevelEnum)Enum.Parse(typeof(SupportLevelEnum), savedSupportTicket.CurrentLevel.ToString(), true);

//                    // Get next level
//                    SupportLevelEnum nextLevel = GetNextLevel(oldSupportLevel);

//                    // Get previous level
//                    SupportLevelEnum previousLevel = GetPreviousLevel(oldSupportLevel);

//                    if(newSupportLevel != nextLevel && newSupportLevel != previousLevel) {
//                        // Raise exception.
//                    }
//                }
//            }

//            #endregion
//        }

//        /// <summary>
//        /// This method checks login user's authorization of mention permission type.
//        /// </summary>
//        /// <param name="permissionEnum">Permission type to be authorize.</param>
//        /// <param name="changedSupportTicket">Support ticket enttiy, updated by user.</param>
//        /// <returns>Returns true if login user has permission other raise security exception.</returns>
//        public virtual bool HasPermission(SupportPermissionTypeEnum permissionEnum, SupportTicket changedSupportTicket) {


//            SupportSession supportSession = GetSession();
//            bool hasPermission = false;
//            //bool userBelongToLevel = false;
//            Guid oldCreator = GetOrignalValue<Guid>(changedSupportTicket.ID, SupportTicket.CreatorIdPropertyName);
//            short oldCurrentLevel = GetOrignalValue<short>(changedSupportTicket.ID, SupportTicket.CurrentLevelPropertyName);

//            switch(permissionEnum) {
//                case SupportPermissionTypeEnum.Add:
//                    // Any user belong to Level1 / Level2/ Level3 can add SupportTicket.
//                    if(supportSession.SupportLevel != SupportLevelEnum.Level4) {
//                        hasPermission = true;
//                    }
//                    break;

//                case SupportPermissionTypeEnum.Resolve:
//                case SupportPermissionTypeEnum.Comment:
//                case SupportPermissionTypeEnum.Close:
//                    // Owner can resolve ticket.
//                    bool everAssignedToLevel = false;
//                    if(supportSession.AppUserId.Equals(oldCreator)) {
//                        hasPermission = true;
//                    }
//                    else {
//                        //   userBelongToLevel = UserBelongToLevel(supportSession.AppUserId, (SupportLevelEnum)requesterSupportLevel);
//                        // Is Ticket assigned to higher level.
//                        everAssignedToLevel = TicketAssignedToLevel(changedSupportTicket.ID, supportSession.SupportLevel);
//                        // ToDo: Please Review this permission check.
//                        // hasPermission = true;
//                        if(everAssignedToLevel && oldCurrentLevel == (short)supportSession.SupportLevel) {
//                            hasPermission = true;
//                        }
//                    }
//                    break;

//                case SupportPermissionTypeEnum.Update:
//                    // Is Ticket assigned to higher level.
//                    everAssignedToLevel = TicketAssignedToLevel(changedSupportTicket.ID, supportSession.SupportLevel);
//                    // ToDo: Please Review this permission check.
//                    // hasPermission = true;
//                    if(everAssignedToLevel) {
//                        hasPermission = true;
//                    }
//                    break;

//                case SupportPermissionTypeEnum.Delete:
//                case SupportPermissionTypeEnum.Ownership:
//                    // Only owner can delete ticket in any status.
//                    if(supportSession.AppUserId.Equals(oldCreator)) {
//                        hasPermission = true;
//                    }
//                    break;

//                case SupportPermissionTypeEnum.StatusChange:
//                    // Status changed to Resolve & Close will not consider here.
//                    // 1) If ticket assigned to current level and login user belong to his level.
//                    // userBelongToLevel = UserBelongToLevel(supportSession.AppUserId, requesterSupportLevel);
//                    if(oldCurrentLevel == (short)supportSession.SupportLevel) {
//                        hasPermission = true;
//                    }
//                    break;
//                case SupportPermissionTypeEnum.View:
//                    // Any support user belong to higher level can view if ticket is assigned to his level any time during life span.
//                    // Owner can view.
//                    // SupportLevelEnum nextLevel = GetNextLevel(requesterSupportLevel);
//                    hasPermission = TicketAssignedToLevel(changedSupportTicket.ID, supportSession.SupportLevel);
//                    break;
//            }

//            if(hasPermission) {
//                return true;
//            }
//            else {
//                throw new EwpSecurityException(string.Format("User doesn't have {0} permission  on support ticket", permissionEnum.ToString()));
//            }

//        }

//        #endregion

//        #region Private Methods

//        private bool TicketAssignedToLevel(Guid supportTicketId, SupportLevelEnum supportLevel) {
//            // Check an entry of support ticket for TargetLevel.
//            return true;
//        }

//        private void RaiseSecurityException(string errorMessage, IList<EwpErrorData> errorDataList = null) {
//            throw new EwpSecurityException(errorMessage, errorDataList);
//        }

//        private bool ValidateSupportTicketOnAddUpdate(SupportTicket supportTicket) {
//            bool valid = true;

//            return valid;
//        }

//        private string GetAppKeyBySupportLevel(SupportLevelEnum supportLevel) {
//            string appKey = "";
//            switch(supportLevel) {

//                case SupportLevelEnum.Level1:
//                case SupportLevelEnum.Level2:
//                    appKey = AppKeyEnum.pay.ToString();
//                    break;
//                case SupportLevelEnum.Level3:
//                    appKey = AppKeyEnum.pub.ToString();
//                    break;

//            }
//            return appKey;
//        }

//        #endregion


//    }
//}
