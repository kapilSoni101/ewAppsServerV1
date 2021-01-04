//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using ewApps.Support.Common;
//using ewApps.Core.Entity;
//using ewApps.Core.ExceptionService;
//using ewApps.Support.Common;
//using ewApps.Support.Entity;
//using ewApps.Core.Common;
//using SupportLevelEnum = ewApps.Support.Common.SupportLevelEnum;
//using ewApps.AppPortal.DS;

//namespace ewApps.Core.DS {
//  public abstract class SupportService:BaseDS<SupportTicket> {

//    //    private ISupportRepository _supportRepository;
//    private string _appKey = "";
//    private IIdentityDS _identityDS = null;
//    ISupportTicketDS _supportTicketDS = null;
//    private string _defaultPrefix = "TKT";
//    private SupportSession _supportSession;

//    // Constructor
//    // Note: As each application has single derived class level is not pass in constructor.
//    public SupportService(string appKey, IIdentityDS identityDS, ISupportTicketDS supportTicketDS) : base(null, null) {
//      _supportTicketDS = supportTicketDS;
//      _appKey = appKey;
//      _identityDS = identityDS;
//      _supportSession = GetSession();
//    }

//    #region Abstract Methods

//    public abstract SupportApp GetSupportAppModel();
//    public abstract List<SupportUser> GetSupportUsersByLevel(SupportLevelEnum supportLevel);
//    public abstract SupportSession GetSession();

//    #endregion
//    public override Task<SupportTicket> AddAsync(SupportTicket t, CancellationToken token = default(CancellationToken)) {
//      throw new NotImplementedException();
//    }

//    // Add Method
//    public async virtual Task<SupportTicket> AddAsync(SupportTicket supportTicket) {
//      // Check Add Support Ticket Permission
//      HasPermission(SupportPermissionTypeEnum.Add, supportTicket);

//      // Validates assignee by calling GetAssigneeList(...) method.
//      ValidateLevelTransition(supportTicket, null);

//      // Generate new ticket number
//      string newTicketNumber = GenerateTicketNumber();

//      supportTicket.IdentityNumber = newTicketNumber;

//      // Add support object in database by calling core ISupportDataService.Add() method.
//      return await base.AddAsync(supportTicket);
//    }

//    public override SupportTicket Update(SupportTicket t, object key) {
//      throw new NotImplementedException();
//    }

//    public virtual void Update(SupportTicket changedSupportTicket) {
//      // Check if status ticket changed.

//      // Get Saved entity.
//      SupportTicket savedSupportTicket = Get(changedSupportTicket.ID);

//      // validate assigned level
//      ValidateLevelTransition(changedSupportTicket, savedSupportTicket);

//      short oldStatus = GetOrignalValue<short>(changedSupportTicket.ID, SupportTicket.StatusPropertyName);

//      // If status = Resolve
//      if(oldStatus != changedSupportTicket.Status && changedSupportTicket.Status == (short)SupportStatusTypeEnum.Resolved) {
//        HasPermission(SupportPermissionTypeEnum.Resolve, changedSupportTicket);
//      }
//      // If status = Close
//      else if(oldStatus != changedSupportTicket.Status && changedSupportTicket.Status == (short)SupportStatusTypeEnum.Closed) {
//        HasPermission(SupportPermissionTypeEnum.Close, changedSupportTicket);
//      }

//      // Check if user has update permission.
//      HasPermission(SupportPermissionTypeEnum.Update, changedSupportTicket);

//      base.Update(changedSupportTicket, changedSupportTicket.ID);
//    }

//    // Delete Method
//    public virtual void Delete(SupportTicket supportTicket) {
//      // Validate permission
//      HasPermission(SupportPermissionTypeEnum.Delete, supportTicket);

//      // Delete support object in database by calling core ISupportDataService.Add() method.
//      _supportTicketDS.Delete(supportTicket);
//    }


//    public virtual string GenerateTicketNumber() {
//      // ToDo: Recheck this logic.
//      int lastSupportTicketSequence = _identityDS.GetIdentityNo(Guid.Empty, (int)CoreEntityTypeEnum.SupportTicket, _defaultPrefix, 100001);
//      return string.Format("{0}-{1}", _defaultPrefix, lastSupportTicketSequence);
//    }

//    public virtual Support.Common.SupportLevelEnum GetPreviousLevel(SupportLevelEnum currentLevel) {
//      switch(currentLevel) {
//        case SupportLevelEnum.Level2:
//          return SupportLevelEnum.Level3;
//        case SupportLevelEnum.Level1:
//          return SupportLevelEnum.Level2;
//        case SupportLevelEnum.Level3:
//          return SupportLevelEnum.None;
//      }
//      return SupportLevelEnum.None;
//    }

//    public virtual SupportLevelEnum GetNextLevel(SupportLevelEnum currentLevel) {
//      switch(currentLevel) {
//        case SupportLevelEnum.Level3:
//          return SupportLevelEnum.Level2;
//        case SupportLevelEnum.Level2:
//          return SupportLevelEnum.Level1;
//        case SupportLevelEnum.Level1:
//          return SupportLevelEnum.None;
//      }
//      return SupportLevelEnum.None;
//    }

//    #region Validation and Permission Methods

//    public virtual bool Validate(SupportTicket changedSupportTicket) {
//      SupportTicket savedSupportTicket = null;
//      if(changedSupportTicket.ID.Equals(Guid.Empty) == false) {
//        savedSupportTicket = _supportTicketDS.Get(changedSupportTicket.ID);
//      }
//      else {
//        if(changedSupportTicket.Status != (short)SupportStatusTypeEnum.Open) {
//          // Raise exception;
//        }
//      }

//      // Validate assignee
//      ValidateLevelTransition(changedSupportTicket, savedSupportTicket);

//      if(changedSupportTicket.CurrentLevel != savedSupportTicket.CurrentLevel) {
//        CheckTicketLevelTransitionPermission(changedSupportTicket, savedSupportTicket);
//      }

//      return true;
//    }

//    public virtual void CheckTicketLevelTransitionPermission(SupportTicket changedSupportTicket, SupportTicket savedSupportTicket) {
//      // Validate Status
//      if(changedSupportTicket.Status == (short)SupportStatusTypeEnum.OnHold) {
//        // If user is updating existing support ticket, validate Permission_Type.Update
//        if(HasPermission(SupportPermissionTypeEnum.Update, changedSupportTicket) == false) {
//          RaiseSecurityException("");
//        }
//      }
//      else if(changedSupportTicket.Status == (short)SupportStatusTypeEnum.Resolved) {
//        // If user change Ticket_State like from Pending to Resolve check validate Permission_Type.Resolve
//        if(HasPermission(SupportPermissionTypeEnum.Resolve, changedSupportTicket) == false) {
//          RaiseSecurityException("");
//        }
//      }
//      // If user change Ticket_State like from any state to Close check validate Permission_Type.Close
//      else if(changedSupportTicket.Status == (short)SupportStatusTypeEnum.Closed) {
//        if(HasPermission(SupportPermissionTypeEnum.Close, changedSupportTicket) == false) {
//          RaiseSecurityException("");
//        }
//      }
//    }

//    public virtual void ValidateLevelTransition(SupportTicket changedSupportTicket, SupportTicket savedSupportTicket) {

//      #region Validate New Support Level

//      // Add Ticket Case
//      if(savedSupportTicket == null) {
//        // get next level
//        SupportLevelEnum nextLevel = GetNextLevel(_supportSession.SupportLevel);
//        // In Add case ticket only escalate to next level.
//        if(changedSupportTicket.CurrentLevel != (short)nextLevel) {
//          // Raise exception.
//        }
//      }
//      else {
//        // If level changed by user
//        if(changedSupportTicket.CurrentLevel != savedSupportTicket.CurrentLevel) {
//          SupportLevelEnum oldSupportLevel = (SupportLevelEnum)Enum.Parse(typeof(SupportLevelEnum), savedSupportTicket.CurrentLevel.ToString(), true);
//          SupportLevelEnum newSupportLevel = (SupportLevelEnum)Enum.Parse(typeof(SupportLevelEnum), savedSupportTicket.CurrentLevel.ToString(), true);

//          // Get next level
//          SupportLevelEnum nextLevel = GetNextLevel(oldSupportLevel);

//          // Get previous level
//          SupportLevelEnum previousLevel = GetPreviousLevel(oldSupportLevel);

//          if(newSupportLevel != nextLevel && newSupportLevel != previousLevel) {
//            // Raise exception.
//          }
//        }
//      }

//      #endregion
//    }

//    /// <summary>
//    /// This method checks login user's authorization of mention permission type.
//    /// </summary>
//    /// <param name="permissionEnum">Permission type to be authorize.</param>
//    /// <param name="changedSupportTicket">Support ticket enttiy, updated by user.</param>
//    /// <returns>Returns true if login user has permission other raise security exception.</returns>
//    public virtual bool HasPermission(SupportPermissionTypeEnum permissionEnum, SupportTicket changedSupportTicket) {
//      SupportTicket oldSupportTicket = null;
//      if(permissionEnum != SupportPermissionTypeEnum.Add) {
//        oldSupportTicket = _supportTicketDS.Get(changedSupportTicket.ID);
//        //SupportLevelEnum requesterSupportLevel = (SupportLevelEnum)Enum.Parse(typeof(SupportLevelEnum), .ToString(), true);
//      }

//      SupportSession supportSession = GetSession();
//      bool hasPermission = false;
//      //bool userBelongToLevel = false;

//      switch(permissionEnum) {
//        case SupportPermissionTypeEnum.Add:
//          // Any user belong to Level1 or Level2 can add SupportTicket.
//          if(supportSession.SupportLevel != SupportLevelEnum.Level1) {
//            hasPermission = true;
//          }
//          break;

//        case SupportPermissionTypeEnum.Resolve:
//        case SupportPermissionTypeEnum.Comment:
//        case SupportPermissionTypeEnum.Update:
//          // Owner can resolve ticket.
//          if(supportSession.AppUserId.Equals(oldSupportTicket.CreatorId)) {
//            hasPermission = true;
//          }
//          else {
//            //   userBelongToLevel = UserBelongToLevel(supportSession.AppUserId, (SupportLevelEnum)requesterSupportLevel);
//            // Is Ticket assigned to higher level.
//            bool everAssignedToLevel = TicketAssignedToLevel(oldSupportTicket.ID, supportSession.SupportLevel);

//            if(everAssignedToLevel && oldSupportTicket.CurrentLevel == (short)supportSession.SupportLevel) {
//              hasPermission = true;
//            }
//          }
//          break;

//        case SupportPermissionTypeEnum.Delete:
//        case SupportPermissionTypeEnum.Ownership:
//          // Only owner can delete ticket in any status.
//          if(supportSession.AppUserId.Equals(oldSupportTicket.CreatorId)) {
//            hasPermission = true;
//          }
//          break;

//        case SupportPermissionTypeEnum.StatusChange:
//          // Status changed to Resolve & Close will not consider here.
//          // 1) If ticket assigned to current level and login user belong to his level.
//          // userBelongToLevel = UserBelongToLevel(supportSession.AppUserId, requesterSupportLevel);
//          if(oldSupportTicket.CurrentLevel == (short)supportSession.SupportLevel) {
//            hasPermission = true;
//          }
//          break;
//        case SupportPermissionTypeEnum.View:
//          // Any support user belong to higher level can view if ticket is assigned to his level any time during life span.
//          // Owner can view.
//          // SupportLevelEnum nextLevel = GetNextLevel(requesterSupportLevel);
//          hasPermission = TicketAssignedToLevel(oldSupportTicket.ID, supportSession.SupportLevel);
//          break;
//      }

//      if(hasPermission) {
//        return true;
//      }
//      else {
//        throw new EwpSecurityException(string.Format("User doesn't have {0} permission  on support ticket", permissionEnum.ToString()));
//      }

//    }

//    #endregion

//    #region Private Methods

//    private bool TicketAssignedToLevel(Guid supportTicketId, SupportLevelEnum supportLevel) {
//      // Check an entry of support ticket for TargetLevel.
//      return true;
//    }

//    private void RaiseSecurityException(string errorMessage, IList<EwpErrorData> errorDataList = null) {
//      throw new EwpSecurityException(errorMessage, errorDataList);
//    }

//    #endregion

//    #region Not Used

//    public virtual List<SupportGroup> GetSupportGroupsByTicketState(SupportLevelEnum currentLevel, SupportStatusTypeEnum statusType) {
//      //if (statusType==StatusTypeEnum.New || statusType==StatusTypeEnum.Delete) {
//      //  throw new InvalidOperationException("Invalid operation type");
//      //}

//      SupportLevelEnum supportGroupLevel = SupportLevelEnum.None;
//      if(statusType == SupportStatusTypeEnum.Open) {
//        supportGroupLevel = GetNextLevel(currentLevel);
//      }
//      else {
//        supportGroupLevel = GetPreviousLevel(currentLevel);
//      }

//      // Get all support group by AppKey and Level
//      List<SupportGroup> supportGroupList = new List<SupportGroup>();//= GetSupportGroup(_appKey, supportGroupLevel);

//      return supportGroupList;
//    }

//    public virtual int GetCurrentLevel(Guid supportId) {
//      SupportTicket supportEntity = _supportTicketDS.Get(supportId);
//      return supportEntity.CurrentLevel;
//    }

//    //// Level is required if derived class is one for all levels.
//    //public List<SupportGroup> GetSupportGroup(SupportLevelEnum supportLevel) {
//    //  // Get from support service database table.
//    //  // return GetSupportGroup(_appKey, supportLevel);
//    //  return null;
//    //}

//    private bool UserBelongToLevel(Guid supportUserId, SupportLevelEnum supportLevel) {
//      List<SupportUser> supportUserList = GetSupportUsersByLevel(supportLevel);
//      return supportUserList.Any(i => i.ID == supportUserId);
//    }

//    private int CompareLevel(SupportLevelEnum firstLevel, SupportLevelEnum secondLevel) {
//      int compareResult = Convert.ToInt16(firstLevel).CompareTo(secondLevel);
//      // 1 2
//      if(compareResult < 0) {
//        compareResult = 1;
//      }
//      else if(compareResult > 0) {
//        compareResult = -1;
//      }
//      else {
//        compareResult = 0;
//      }
//      return compareResult;
//    }


//    public virtual bool Escalate(SupportTicket updatedEntity) {
//      SupportTicket existingSupportEntity = _supportTicketDS.Get(updatedEntity.ID);
//      SupportLevelEnum nextLevel = (SupportLevelEnum)Enum.Parse(typeof(SupportLevelEnum), GetNextLevel((SupportLevelEnum)updatedEntity.CurrentLevel).ToString(), true);
//      existingSupportEntity.CurrentLevel = (short)nextLevel;
//      _supportTicketDS.Update(updatedEntity, updatedEntity.ID);
//      return true;
//    }

//    public virtual bool DeEscalate(SupportTicket updatedEntity) {
//      SupportTicket existingSupportEntity = _supportTicketDS.Get(updatedEntity.ID);
//      SupportLevelEnum previousLevel = (SupportLevelEnum)Enum.Parse(typeof(SupportLevelEnum), GetPreviousLevel((SupportLevelEnum)updatedEntity.CurrentLevel).ToString(), true);
//      existingSupportEntity.CurrentLevel = (short)previousLevel;
//      _supportTicketDS.Update(updatedEntity, updatedEntity.ID);
//      return true;
//    }


//    public bool Resolve(SupportTicket supportTicket) {
//      //if (HasPermission(SupportPermissionTypeEnum.Resolve, supportTicket.ID){
//      //  supportTicket.Status=(int)SupportPermissionTypeEnum.Resolve;
//      //}
//      //else {
//      //  throw new EwpSecurityException("You don\'t have permission to resolve support ticket.");
//      //}

//      //if (ValidateAssignee(supportTicket)==false) {
//      //  throw new Exception("Invalid assignee list");
//      //}

//      //  Update(supportTicket);
//      return true;
//    }

//    public bool Close(SupportTicket supportTicket) {
//      //if (HasPermission(SupportPermissionTypeEnum.Close, supportTicket.ID)) {
//      //  supportTicket.Status=(int)StatusTypeEnum.Close;
//      //}
//      //else {
//      //  throw new Exception("You don\'t have permission to close support ticket.");
//      //}

//      //if (ValidateAssignee(supportTicket)==false) {
//      //  throw new Exception("Invalid assignee list");
//      //}

//      //Update(supportTicket);
//      return true;
//    }


//    #endregion

//  }
//}
