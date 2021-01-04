using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.ExceptionService {

  /// <summary>
  /// This class provides all the constants used in the application.
  /// </summary>
  public sealed class Constants
  {

    #region Exception Constants

    /// <summary>
    /// Original Exception flag key.
    /// </summary>  
    public const string IsOriginaleKey = "IsOriginal";

    /// <summary>
    /// Wrapper Exception flag key.
    /// </summary>
    public const string IsWrapperKey = "IsWrapper";

    /// <summary>
    /// Pass Through Exception flag key.
    /// </summary>
    public const string IsPassThroughKey = "IsPassThrough";

    /// <summary>
    /// Re-throw Exception flag key.
    /// </summary>
    public const string IsRethrowKey = "IsRethrow";

    /// <summary>
    /// New Exception flag key.
    /// </summary>
    public const string IsNewKey = "IsNew";

    /// <summary>
    /// Exception Severity key.
    /// </summary>
    public const string SeverityKey = "Severity";

    /// <summary>
    /// Additional message key.
    /// </summary>
    public const string AdditionalMsgKey = "AdditionalMsg";

    /// <summary>
    /// Exception is logged flag key.
    /// </summary>
    public const string IsLoggedKey = "IsLogged";

    /// <summary>
    /// Http status code.
    /// </summary>
    public const string HttpStatusCodeKey = "HttpStatusCode";

    /// <summary>
    /// PassThrough Policy Key Constant. 
    /// </summary>
    public const string PassThroughPolicyKey = "PassThroughPolicy";

    /// <summary>
    /// New Policy Key Constant. 
    /// </summary>
    public const string NewPolicyKey = "NewPolicy";

    /// <summary>
    /// Log Policy Key Constant. 
    /// </summary>
    public const string LogPolicyKey = "LogPolicy";

    /// <summary>
    /// Data Policy Key Constant. 
    /// </summary>
    public const string DataPolicyKey = "DataPolicy";

    /// <summary>
    /// DataService Policy Key Constant. 
    /// </summary>
    public const string DataServicePolicyKey = "DataServicePolicy";

    /// <summary>
    /// DataService Policy Key Constant. 
    /// </summary>
    public const string SyncPolicyKey = "SyncPolicy";

    /// <summary>
    /// Admin role name list.
    /// </summary>
    public const string AdminRoleNames = "'AccountAdmin','ITAdmin','ITIssueAdmin'";

    #endregion Exception Constants.

  }
}
