using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ewApps.AppPortal.WebApi {

  public class PublisherLiveUpdateConstants {

    #region Event Name
   
    public const string UserPermissionChangeEvent = "UserPermissionChange";
    public const string DeleteUserEvent = "UserDelete";

    #endregion Event Name

    #region Handler Name

    public const string PublisherLiveUpdateHandler = "PublisherLiveUpdate";   

    #endregion Handler Name

  }
}
