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


using ewApps.AppPortal.Data;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {
    /// <summary>
    /// This class implements standard business logic and operations for Level Transition History.
    /// </summary>
    public class LevelTransitionHistoryDS :BaseDS<LevelTransitionHistory>, ILevelTransitionHistoryDS {

    #region Local member

    ILevelTransitionHistoryRepository _levelTransitionHistoryRepository;



    #endregion Local member

    #region Constructor
    /// <summary>
    /// Initializing local variables
    /// </summary>
    /// <param name="levelTransitionHistoryRepository"></param>
    /// <param name="cacheService"></param>

    public LevelTransitionHistoryDS(ILevelTransitionHistoryRepository levelTransitionHistoryRepository) : base(levelTransitionHistoryRepository) {
      _levelTransitionHistoryRepository = levelTransitionHistoryRepository;
    }
    #endregion Constructor

    public override LevelTransitionHistory Add(LevelTransitionHistory entity) {
      UpdateSystemFieldsByOpType(entity, OperationType.Add);
      return base.Add(entity);
    }

    public override LevelTransitionHistory Update(LevelTransitionHistory entity, object key) {
      UpdateSystemFieldsByOpType(entity, OperationType.Update);
      return base.Update(entity, key);
    }

    public override void Delete(LevelTransitionHistory entity) {
      UpdateSystemFieldsByOpType(entity, OperationType.Delete);
      base.Delete(entity);
    }
  }
}
