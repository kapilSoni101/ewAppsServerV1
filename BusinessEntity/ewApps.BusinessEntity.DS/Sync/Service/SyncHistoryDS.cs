/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Sourabh Agrawal<sagrawal @eworkplaceapps.com>
 * Date:09 september 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    ///  This class implements standard business logic and operations for SyncHistory entity.
    /// </summary>
    public class SyncHistoryDS:BaseDS<SyncHistory>, ISyncHistoryDS {

        #region Local variables

        ISyncHistoryRepository _syncHistoryRepo;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="syncHistoryRepo"></param>
        public SyncHistoryDS(ISyncHistoryRepository syncHistoryRepo) : base(syncHistoryRepo) {
            _syncHistoryRepo = syncHistoryRepo;
        }

        #endregion Constructor
    }
}
