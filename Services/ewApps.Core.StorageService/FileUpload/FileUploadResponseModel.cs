using System;

namespace ewApps.Core.StorageService {

  /// <summary>
  /// Class contains the rsponse model properties after uploading the file into server.
  /// </summary>
  public class FileUploadResponseModel {

    /// <summary>
    /// Uploaded filename.
    /// </summary>
    public string UploadFileName;

    /// <summary>
    /// File save into physical location with a storageid, Its unique id for each stored files.
    /// </summary>
    public Guid StorageId = Guid.Empty;

    /// <summary>
    /// Saved physical location of stored file.
    /// </summary>
    public string StorageFilePath;

    /// <summary>
    /// Tell use whether file is stored or Blocks uploading are InProcess.
    /// For example: In case of chunk uploading, After uploading each block, Status will be InProcess untill all blocks are uploaded.
    /// Once all blocks uploaded and merge into a file and save into actual physical location then status will be Uploaded.
    /// </summary>
    public FileStorageStatus UploadStorageStatus = FileStorageStatus.InProcess;

  }

}
