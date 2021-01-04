using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.UniqueIdentityGeneratorService {

  /// <summary>
  /// This class performs CRUD database operations for document entity.
  /// </summary>
  public class UniqueIdentityGeneratorRepository : BaseRepository<UniqueIdentityGenerator, UniqueIdentityGeneratorDbContext>, IUniqueIdentityGeneratorRepository
  {

    #region Constructor 

    /// <summary>
    ///  Constructor initializing the base variables
    /// </summary>
    /// <param name="context">Core DB context instance.</param>
    /// <param name="sessionManager">Session manager instance</param>
    ///  <param name="connectionManager"></param>
    public UniqueIdentityGeneratorRepository(UniqueIdentityGeneratorDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager)
    {
    }

    #endregion Constructor  

  }
}
