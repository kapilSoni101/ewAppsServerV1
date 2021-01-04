using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.ExceptionService {

  public static class ErrorMessages {

    #region Common Messages

    /// <exclude/>
    public const string FieldIsRequired = "The field, {0}, is required.";

    /// <exclude/>
    public const string FieldIsLessThenOrEqualCharacters = "{0} should be less than or equal to {1} characters.";

    /// <exclude/>
    public const string LengthExeeded = "{0} should be less than or equal to {1} characters.";

    /// <exclude/>
    public const string Permission = "You do not have permission to {0} {1}.";

    // Common
    ///<exclude />
    public const string MethodNotSupported = "This method is not supported.";
    /// <exclude/>
    public const string ReferenceItemExists = "This item is being referred to elsewhere . Remove the references and then try again.";
    /// <exclude/>
    public const string DataConcurrency = "This item has been updated or deleted by another user. Please reload the item and then retry your operation.";
    /// <exclude/>
    public const string SQLConTimeOut = "Database connection is timeout. Please try again after some time.";
    /// <exclude/>
    public const string MailServerUnavailable = "Mail service is unavailable.";
    /// <exclude/>
    public const string NullEntity = "Null entity in update operation.";
    /// <exclude/>
    public const string InvalidVersionNo = "You are running an older version of Issue Tracker. In order to connect to the Issue Tracker server, please update the application.";
    /// <exclude/>
    public const string LowDiskSpace = "There is not enough space on the disk.";
    /// <exclude/>
    public const string InvalidEntity = "Invalid Entity '{0}'.";
    /// <exclude/>
    public const string TokenIsRequired = "Token is required.";
    /// <exclude/>
    public const string InvalidLoginToken = "Invalid Login Token.";
    /// <exclude/>
    public const string InvalidRequest = "Invalid Request";
    /// <exclude/>
    public const string InvalidArgument = "Invalid Argument";
    /// <exclude/>
    public const string InvalidParameterValue = "{0} parameter has some invalid value.";

    /// <exclude/>
    public const string ConcurrentMessage = " Selected {0} might have been updated or deleted by another user, please try again.";

    /// <exclude/>
    public const string FieldConcurrentMessage = "{0} value has been updated by another user.";

    /// <exclude/>
    public const string DatabaseOperationErrorMessage = "Error occurred during {0} DB operation.";

    /// <exclude/>
    public const string GeneralDBErrorMessage  = "Error occurred during database operation.";

    /// <exclude/>
    public const string DuplicateAddressLabelErrorMessage = "Address with same label already exists.";

    #endregion
  }
}
