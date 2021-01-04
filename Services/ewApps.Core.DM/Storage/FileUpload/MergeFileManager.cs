using System;
using System.Collections.Generic;

namespace ewApps.Core.DMService {

  /// <summary>
  /// It is used to add the file in merge list.
  /// Also remove the filter list once all are merge.
  /// </summary>
  public class MergeFileManager {

    private static MergeFileManager instance;
    private List<string> MergeFileList;

    private MergeFileManager() {
      try {
        MergeFileList = new List<string>();
      }
      catch { }
    }

    public static MergeFileManager Instance
    {
      get
      {
        if (instance == null)
          instance = new MergeFileManager();
        return instance;
      }
    }

    /// <summary>
    /// Add file in Mergelist.
    /// </summary>
    /// <param name="BaseFileName">Name of the file to add in Merge file, Every file has unique name.</param>
    public void AddFile(string BaseFileName) {
      MergeFileList.Add(BaseFileName);
    }

    /// <summary>
    /// Method check whethere file with a name already exist.
    /// </summary>
    /// <param name="BaseFileName">name of the file to check where it exist in Merge file.</param>
    /// <returns>return true if file exist in Mergelist.</returns>
    public bool InUse(string BaseFileName) {
      return MergeFileList.Contains(BaseFileName);
    }

    /// <summary>
    /// Remove the file from the merge list.
    /// </summary>
    /// <param name="BaseFileName">Remove the file with the name.</param>
    /// <returns>return true if file exist in Mergelist.</returns>
    public bool RemoveFile(string BaseFileName) {
      return MergeFileList.Remove(BaseFileName);
    }

  }
}
