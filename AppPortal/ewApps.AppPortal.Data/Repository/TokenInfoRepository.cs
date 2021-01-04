using System;
using System.Collections.Generic;
using System.Text;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.Data {
   public class TokenInfoRepository : BaseRepository<TokenInfo, AppPortalDbContext> , ITokenInfoRepository {


        /// <summary>
        /// Initializes a new instance of the <see cref="TokenInfoRepository"/> class.
        /// </summary>
        public TokenInfoRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }
    }
}
