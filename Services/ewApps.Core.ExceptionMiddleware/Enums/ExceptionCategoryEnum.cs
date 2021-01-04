//===============================================================================
// Copyright © eWorkplace Apps.  All rights reserved.
// eWorkplace Apps Common Tools
// Main Author: Sanjeev Khanna
// Original Date: March. 23, 2012
//===============================================================================

namespace ewApps.Core.ExceptionService {
  
  /// <summary>
  /// Specifies the exception category. 
  /// </summary>
  public enum ExceptionCategoryEnum {

    /// <summary>
    /// Specifies that the no category.
    /// </summary>
    None = 0,

    /// <summary>
    /// Specifies that the exception is PassThrough category.
    /// It passes through the exception without any change, sort of Rethrow. 
    /// It may log the exception.
    /// </summary>
    PassThrough = 1,

    /// <summary>
    /// Specifies that the exception is Rethrow category.
    /// It rethrows an exception which is different than the original, in data or type.
    /// </summary>
    Rethrow = 2,

    /// <summary>
    /// Specifies that the exception is Wrap category.
    /// Rethrows a different exception with the original exception as an inner exception.
    /// </summary>
    Wrap = 3,

    /// <summary>
    /// Specifies that the exception is New category.
    /// New exception generated at this layer. The other three were generated at a lower layer.
    /// This category is always wrapped. 
    /// </summary>
    New = 4

  }
}
