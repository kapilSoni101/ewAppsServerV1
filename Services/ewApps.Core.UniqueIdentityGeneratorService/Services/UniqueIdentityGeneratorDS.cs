/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agrawal <sagrawal@eworkplaceapps.com>
 * Date: 16 November 2018
 * 
 * Contributor/s: Sourabh Agrawal
 * Last Updated On: 09 August 2019
 */
using System;
using ewApps.Core.BaseService;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
//using ewApps.Core.Data;
//using ewApps.Core.Entity;
//using ewApps.Core.UserSessionService;

namespace ewApps.Core.UniqueIdentityGeneratorService {

  /// <summary>
  /// This class implements standard business logic and operations for Identity entity.
  /// </summary>
  public class UniqueIdentityGeneratorDS : BaseDS<UniqueIdentityGenerator>,  IUniqueIdentityGeneratorDS {

    #region Local Member

    IUniqueIdentityGeneratorRepository _identityRepo;
    IUserSessionManager _sessionManager;

    #endregion

    #region Constructor 

    /// <summary>
    /// Initialinzing local variables
    /// </summary>
    /// <param name="identityRepo">identity repository reference</param>
    /// <param name="unitOfWork">Unit of work reference</param>
    /// <param name="sessionManager">session manager reference</param>
    public UniqueIdentityGeneratorDS(IUniqueIdentityGeneratorRepository identityRepo, IUserSessionManager sessionManager
    ) : base(identityRepo)
    {
      _identityRepo = identityRepo;
      //_unitOfWork = unitOfWork;
      _sessionManager = sessionManager;
    }

    #endregion

    #region public methods 

    #region GET

    ///<inheritdoc/>
    public int GetIdentityNo(Guid TenantId, int EntityType, string PrefixString, int identityNumber)
    {
      UserSession session = _sessionManager.GetSession();
      int newIdentityNo = 0;

      // get exixting entity
      UniqueIdentityGenerator exitingIdentity = _identityRepo.Find(i => i.TenantId == TenantId && i.EntityType == EntityType && i.PrefixString == PrefixString);

      // get entity if acive state is false
      UniqueIdentityGenerator identityByActiveState = _identityRepo.Find(i => i.TenantId == TenantId && i.EntityType == EntityType && i.Active == true);

      // if UniqueIdentityGenerator is not exixt then we add other than we update it.
      if (exitingIdentity == null)
      {
        // update exixting entity  state
        if (identityByActiveState != null)
        {
          identityByActiveState.Active = false;
          if (session != null)
          {
            UpdateSystemFieldsByOpType(identityByActiveState, OperationType.Update);
          }
          else
          {
            identityByActiveState.CreatedOn = DateTime.UtcNow;
            identityByActiveState.UpdatedOn = DateTime.UtcNow;
          }
          _identityRepo.Update(identityByActiveState, identityByActiveState.ID);
        }
        // insert entity
        UniqueIdentityGenerator identity = new UniqueIdentityGenerator();
        if (session != null)
        {
          UpdateSystemFieldsByOpType(identity, OperationType.Add);
        }
        else
        {
          identity.CreatedOn = DateTime.UtcNow;
          identity.UpdatedOn = DateTime.UtcNow;
        }
        identity.Active = true;
        identity.TenantId = TenantId;
        identity.PrefixString = PrefixString;
        identity.EntityType = EntityType;
        if (identityNumber == 0)
        {
          newIdentityNo = 1;
        }
        else
        {
          newIdentityNo = identityNumber;
        }

        identity.IdentityNumber = Convert.ToString(newIdentityNo);
        _identityRepo.Add(identity);
      }
      else
      {
        // update exixting entity  state
        if (identityByActiveState != null)
        {
          identityByActiveState.Active = false;
          if (session != null)
          {
            UpdateSystemFieldsByOpType(identityByActiveState, OperationType.Update);
          }
          _identityRepo.Update(identityByActiveState, identityByActiveState.ID);
        }
        // update identityno
        exitingIdentity.Active = true;
        if (identityNumber > Convert.ToInt32(exitingIdentity.IdentityNumber))
        {
          newIdentityNo = identityNumber;
        }
        else
        {
          newIdentityNo = Convert.ToInt32(exitingIdentity.IdentityNumber) + 1;
        }

        exitingIdentity.IdentityNumber = Convert.ToString(newIdentityNo);
        if (session != null)
        {
          UpdateSystemFieldsByOpType(exitingIdentity, OperationType.Update);
        }
        _identityRepo.Update(exitingIdentity, exitingIdentity.ID);
      }

      Save();

      return newIdentityNo;
    }

    #endregion

    #endregion

  }
}
