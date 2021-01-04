//===============================================================================
// Copyright © eWorkplace Apps.  All rights reserved.
// eWorkplace Apps Common Tools
// Main Author: Sanjeev Khanna
// Original Date: Nov. 28 2014
//===============================================================================

namespace ewApps.Core.ExceptionService {


  /// <summary>
  /// Defines enum for System Error Sub Type.
  /// </summary>
  public enum SystemErrorSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0,

    /// <summary>
    /// Connection TimeOut
    /// </summary>
    ConnectionTimeOut = 1
  }

  /// <summary>
  /// Defines enum for InvalidVersion Error Sub Type.
  /// </summary>
  public enum InvalidVersionErrorSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0,

    /// <summary>
    /// Database Version
    /// </summary>
    DatabaseVersion = 1,

    /// <summary>
    /// App Suite Version
    /// </summary>
    AppSuiteVersion = 2,

    /// <summary>
    /// Application Version
    /// </summary>
    ApplicationVersion = 3
  }

  /// <summary>
  /// Defines enum for Database Error Sub Type.
  /// </summary>
  public enum DatabaseErrorSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0,

    /// <summary>
    /// Connection TimeOut
    /// </summary>
    ConnectionTimeOut = 1,

    /// <summary>
    /// SQL Login
    /// </summary>
    SQLLogin = 2,

    /// <summary>
    /// Transaction
    /// </summary>
    Transaction = 3,

    /// <summary>
    /// Deadlock
    /// </summary>
    Deadlock = 4,

   /// <summary>
   /// Unique key violation error
   /// </summary>
      Unique_Key_Violation = 5 
  }

  /// <summary>
  /// Defines enum for Authentication Error Sub Type.
  /// </summary>
  public enum AuthenticationErrorSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0,

    /// <summary>
    /// Invalid login token
    /// </summary>
    InvalidLoginToken = 1,

    /// <summary>
    /// Invalid Email
    /// </summary>
    InvalidEmail = 2,

    /// <summary>
    /// Invalid Password
    /// </summary>
    InvalidPassword = 3,

    /// <summary>
    /// User is locked
    /// </summary>
    UserLocked = 4,

    ///// <summary>
    ///// Invalid email domain
    ///// </summary>
    //InvalidEmailDomain = 5,


    ///// <summary>
    ///// The invalid o authentication access token
    ///// </summary>
    //InvalidOAuthAccessToken = 4,

    ///// <summary>
    ///// The invalid o authentication access token
    ///// </summary>
    //InvalidOperationType = 5

  }

  ///// <summary>
  ///// Defines enum for ReLogin Error Sub Type.
  ///// </summary>
  //public enum ReLoginErrorSubType {
  //    /// <summary>
  //    /// None
  //    /// </summary>
  //    None = 0,
  //    /// <summary>
  //    /// User passowrd change
  //    /// </summary>
  //    PasswordIsChanged = 1,

  //    /// <summary>
  //    /// Tenant status is inactive
  //    /// </summary>
  //    InactiveTenant = 2,

  //    /// <summary>
  //    /// User status is inactive
  //    /// </summary>
  //    InactiveUser = 3,
  //}

  /// <summary>
  /// Defines enum for Security Error Sub Type.
  /// </summary>
  public enum SecurityErrorSubType {
    /// <summary>
    /// None.
    /// </summary>
    None = 0,
    /// <summary>
    /// Do not have permission to add/update/delete.
    /// </summary>
    AccessDenied = 1,
    /// <summary>
    /// Do not have permission to view record.
    /// </summary>
    View = 2,
    /// <summary>
    /// Do not have permission to add record.
    /// </summary>
    Add = 3,
    /// <summary>
    /// Do not have permission to update record.
    /// </summary>
    Update = 4,
    /// <summary>
    /// Do not have permission to delete record.
    /// </summary>
    Delete = 5,
    /// <summary>
    /// Do not have permission to update field value.
    /// </summary>
    UpdateField = 6
  }

  /// <summary>
  /// Defines enum for Validation Error Sub Type.
  /// </summary>
  public enum ValidationErrorSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0,

    /// <summary>
    /// Required
    /// </summary>
    FieldRequired = 1,

    /// <summary>
    /// Length Exceeded
    /// </summary>
    LengthExceeded = 2,

    /// <summary>
    /// Range
    /// </summary>
    OutOfRange = 3,

    /// <summary>
    /// Invalid Data Type
    /// </summary>
    InvalidDataType = 4,

    /// <summary>
    /// When user define filed exceed from supported field
    /// </summary>
    MaxInstanceLimit = 5,

    ///// <summary>
    ///// The refrence exists
    ///// </summary>
    //RefrenceExists = 7,

    /// <summary>
    /// The invlid token info
    /// </summary>
    InvalidTokenInfo = 6,

    /// <summary>
    /// Invalid Field Value
    /// </summary>
    InvalidFieldValue = 7,

    /// <summary>
    /// 
    /// </summary>
    CirculerReference = 8,


    SelfReference = 9,

    InvalidEmailDomain = 10,

        InActive=11

    }

  /// <summary>
  /// Defines enum for Duplicate Error Sub Type.
  /// </summary>
  public enum DuplicateErrorSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0,
  }

  /// <summary>
  /// Defines enum for Concurrency Error Sub Type.
  /// </summary>
  public enum ConcurrencyErrorSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0
  }

  /// <summary>
  /// Defines enum for ImportData Error Sub Type.
  /// </summary>
  public enum ImportDataErrorSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0,

    /// <summary>
    /// File not found
    /// </summary>
    ImportFileNotFound = 1,

    /// <summary>
    /// Oledb connection error
    /// </summary>
    OLEDBConnectionError = 2,

    /// <summary>
    /// Invalid file type
    /// </summary>
    InvalidFileType = 3,

    /// <summary>
    /// Does not have import permission
    /// </summary>
    ImportPermissionDenied = 4,

    /// <summary>
    /// Invalid field value
    /// </summary>
    InvalidFieldValue = 5,

    /// <summary>
    /// Sheet not found in excel file
    /// </summary>
    SheetNotFound = 6,

    /// <summary>
    /// The field required
    /// </summary>
    FieldRequired = 7,

    /// <summary>
    /// The invited employee already exists
    /// </summary>
    InvitedEmployeeAlreadyExists = 8,

    /// <summary>
    /// The invalidExcelFile
    /// </summary>
    InvalidExcelFile = 9
  }

  /// <summary>
  /// Defines enum for ImportData Error Sub Type.
  /// </summary>
  public enum NotImplementedErrorSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0
  }


  /// <summary>
  /// Defines enum for InvalidRequestArgument Error Sub Type.
  /// </summary>
  public enum InvalidRequestArgumentErrorSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0
  }

  /// <summary>
  /// Defines enum for InvalidReference Error Sub Type.
  /// </summary>
  public enum InvalidFieldReferenceErrorSubType {

    /// <summary>
    /// None
    /// </summary>
    None = 0,

    /// <summary>
    /// ReferenceDeleted
    /// </summary>
    ReferenceDeleted = 1,

    /// <summary>
    /// ReferenceExists
    /// </summary>
    ReferenceExists = 2
  }

  /// <summary>
  /// Defines enum for SyncException Error Sub Type.
  /// </summary>
  public enum SyncExceptionSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0,

    /// <summary>
    /// The re initialize
    /// </summary>
    ReInit = 1
  }

  /// <summary>
  /// Defines enum for Ignore Error Sub Type.
  /// </summary>
  public enum IgnoreExceptionSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0
  }

  /// <summary>
  /// Defines enum for ResponseException Error Sub Type.
  /// </summary>
  public enum ResponseExceptionSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0
  }

  /// <summary>
  /// Defines enum for InvalidOperation Error Sub Type.
  /// </summary>
  public enum InvalidOperationExceptionSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0,
    /// <summary>
    /// Method not supported
    /// </summary>
    MethodNotSupported = 1,
    /// <summary>
    /// BusinessRuleVoilation
    /// </summary>
    BusinessRuleViolation = 2,
    /// <summary>
    /// The inactive user
    /// </summary>
    InactiveUser = 3,

    /// <summary>
    /// The minimum member count reached
    /// </summary>
    MinMemberCountReached = 4,

    /// <summary>
    /// The maximum member count reached
    /// </summary>
    MaxMemberCountReached = 5

  }

  /// <summary>
  /// Defines enum for PassThrough Error Sub Type.
  /// </summary>
  public enum PassThroughExceptionSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0
  }

  /// <summary>
  /// Defines enum for Recoverable Error Sub Type.
  /// </summary>
  public enum RecoverableExceptionSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0
  }


  /// <summary>
  /// Defines enum for DataService Error Sub Type.
  /// </summary>
  public enum DataServiceExceptionSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0
  }


  /// <summary>
  /// Defines enum for DataException Error Sub Type.
  /// </summary>
  public enum DataExceptionSubType {
    /// <summary>
    /// None
    /// </summary>
    None = 0
  }

  /// <summary>
  /// Defines enum for push exception error sub type.
  /// </summary>
  public enum PushExceptionSubType {

    /// <summary>
    /// The none
    /// </summary>
    None = 0,

    /// <summary>
    /// The registration error
    /// </summary>
    RegistrationError = 1,

  }

  public enum DowntimeExceptionSubType {
    None = 0,
    DowntimeError = 1
  }


  /// <summary>
  /// Defines enum for push exception error sub type.
  /// </summary>
  public enum OpenfireServerExceptionSubType {

    /// <summary>
    /// The none
    /// </summary>
    None = 0,

    /// <summary>
    /// The openfire service down error
    /// </summary>
    ServiceDownError = 1,

  }


}