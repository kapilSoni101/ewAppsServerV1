/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 14 Aug 2018
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 14 Aug 2018
 */
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.Data {

    public class ThemeRepository:BaseRepository<Theme, AppMgmtDbContext>, IThemeRepository {
        #region Constructor
        public ThemeRepository(AppMgmtDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {

        }
        #endregion

        #region public methods 
        public async Task<IEnumerable<Theme>> GetEntityAsync() {
            IEnumerable<Theme> theme;

            theme = _context.Theme.Where(i => i.Deleted == false).Select(i => i) as IEnumerable<Theme>;
            return theme;
        }
        #endregion
    }
}
