using System;

namespace ewApps.Core.DMService {
  public class StorageServiceCapabilities {
    /// <summary>
    /// This class maintains a list of capabilitie of a storage provider.
    /// Note that a field may be initialized by only a class in this assembly.
    /// </summary>
    /// 

    internal bool _canUploadFileStream = false;
    public bool CanUploadFileStream {
      get {
        return _canUploadFileStream;
      }
      private set {
      }
    }

    internal bool _canUploadFromFilePath = false;
    public bool CanUploadFromFilePath {
      get {
        return _canUploadFromFilePath;
      }
      private set {
      }
    }

    internal bool _canGenerateThumbnail = false;
    public bool CanGenerateThumbnail {
      get {
        return _canGenerateThumbnail;
      }
      private set {
      }
    }
  }
}
