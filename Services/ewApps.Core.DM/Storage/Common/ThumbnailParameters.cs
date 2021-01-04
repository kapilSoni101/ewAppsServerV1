using System;

namespace ewApps.Core.DMService {
  public class ThumbnailParameters {
    /// <summary>
    /// This class is base class for Thumbnail generation parameters
    /// Each provider will have its derived class.
    /// Note that the options set here are not binding. They may be ignored by the provider. 
    /// </summary>
    /// 

    /// <summary>
    /// If 0, auto calculate maintaing the original file's aspect ratio.
    /// If both 0, return the original size,
    /// </summary>
    public int Width = 0;
    public int Height = 0;
    public string FileName = "";
    public string ThumbnailId = "";

  }
}
