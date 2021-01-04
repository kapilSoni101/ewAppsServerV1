using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.StorageService {

  /// <summary>
  /// Sorter file with the name and order of the file it exist in list.
  /// </summary>
  public class SortedFile {

    /// <summary>
    /// Sort order of the file.
    /// </summary>
    public int FileOrder
    {
      get; set;
    }

    /// <summary>
    ///  Name of the file.
    /// </summary>
    public String FileName
    {
      get; set;
    }

  }
}

