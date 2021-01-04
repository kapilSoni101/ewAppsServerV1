//using System;
//using System.Collections.Generic;
//using System.IO;

//namespace ewApps.Core.StorageService {
//  /// <summary>
//  /// Class is responsible to generate the file viewable object.
//  /// object contains file basic information with filestream or fileurl.
//  /// Fileurl is temporary url it does not expose actual path of saved file.
//  /// </summary>
  
//  public class FileBrowserViewerService {

//    /// <summary>
//    /// return FileStorage information required for showing the file in browser.
//    /// </summary>
//    /// <param name="storageId">Unique id of storaed file.</param>
//    /// <param name="storageProviderType">Storaed file location.</param>
//    /// <returns></returns>
//    public FileResultData GetFileInfo(Guid storageId, Dictionary<string, object> strResult, StorageServiceProviderType storageProviderType) {
//      FileResultData fileResultData = new FileResultData();
//      try {
//        // Getting file meta data.
//        FileData fileMetadata = GetFileMetaData(storageId, strResult);

//        // Handling the audio type file.
//        if(FileSupport.IsAudioMimeType(fileMetadata.MimeType)) {
//          fileResultData = GetAudioFileDetail(fileMetadata, storageId, storageProviderType);
//        } // Handling the video type file.
//        else if(FileSupport.IsVideoMimeType(fileMetadata.MimeType)) {
//          fileResultData = GetVideoFileDetail(fileMetadata, storageId, storageProviderType);
//        }
//        // Handling the image type file.
//        else if(FileSupport.IsImageMimeType(fileMetadata.MimeType)) {
//          fileResultData = GetImageFileDetail(fileMetadata, storageId, storageProviderType);
//        }
//        // Handling the document type file.
//        else if(FileSupport.IsDocumnetType(fileMetadata.MimeType)) {
//          fileResultData = GetDocumentFileDetail(fileMetadata, storageId, storageProviderType);
//        }

//        return fileResultData;
//      }
//      catch(Exception ex) {
//        ServiceExceptionHandler.HandleException(ref ex);
//        return fileResultData;
//      }
//    }

//    #region Document File Handling

//    private FileResultData GetDocumentFileDetail(FileData fileMetadata, Guid storageId, StorageServiceProviderType storageProviderType) {
//      FileResultData fileDetailInfo = new FileResultData();
//      fileDetailInfo.FileName = fileMetadata.FileName;
//      fileDetailInfo.MimeType = fileMetadata.MimeType;
//      if(FileSupport.IsDocumnetOfficeType(fileMetadata.MimeType) || FileSupport.IsDocumentOpenOfficeType(fileMetadata.FileName)) {
//        fileDetailInfo.FileUrl = GetPublicTempPath(storageId, fileMetadata.FilePath, fileMetadata.FileName);
//        fileDetailInfo.UseUrl = true;
//        fileDetailInfo.FileStream = null;
//      }
//      else {
//        fileDetailInfo.FileStream = GetFileStream(storageId, fileMetadata.FileName);
//      }

//      return fileDetailInfo;
//    }

//    #endregion Document File Handling

//    #region Audio File Handling

//    private FileResultData GetAudioFileDetail(FileData fileMetadata, Guid storageId, StorageServiceProviderType storageProviderType) {
//      FileResultData fileDetailInfo = new FileResultData();
//      fileDetailInfo.FileName = fileMetadata.FileName;
//      fileDetailInfo.MimeType = fileMetadata.MimeType;
//      fileDetailInfo.FileStream = GetFileStream(storageId, fileMetadata.FileName);

//      return fileDetailInfo;
//    }

//    #endregion Audio File Handling

//    #region Video File Handling

//    private FileResultData GetVideoFileDetail(FileData fileMetadata, Guid storageId, StorageServiceProviderType storageProviderType) {
//      FileResultData fileStorageInfo = new FileResultData();
//      fileStorageInfo.FileName = fileMetadata.FileName;
//      fileStorageInfo.MimeType = fileMetadata.MimeType;
//      fileStorageInfo.FileStream = GetFileStream(storageId, fileMetadata.FileName);

//      return fileStorageInfo;
//    }

//    #endregion Video File Handling

//    #region Image File Handling

//    private FileResultData GetImageFileDetail(FileData filePayload, Guid storageId, StorageServiceProviderType storageProviderType) {
//      FileResultData fileDetailInfo = new FileResultData();
//      fileDetailInfo.FileName = filePayload.FileName;
//      fileDetailInfo.MimeType = filePayload.MimeType;
//      fileDetailInfo.FileStream = GetFileStream(storageId, filePayload.FileName);

//      return fileDetailInfo;
//    }

//    #endregion Image File Handling

//    #region Support

//    /// <summary>
//    /// return file stream of stored file.
//    /// </summary>
//    /// <param name="storageId"></param>
//    /// <param name="storageProviderType"></param>
//    /// <returns></returns>
//    private FileStream GetFileStream(Guid storageId, string fileName) {
//      // Getting the storgae service provider object.
//      IStorageService service = StorageServiceFactory.GetDefaultStorageService();
//      // Getting filestream of file.
//      FileStream fileStream = service.GetFile(storageId, fileName).Result;
//      if(fileStream != null)
//        return fileStream;
//      else
//        RaiseFileNotFoundException();

//      return null;
//    }

//    /// <summary>
//    /// Get the meta data of stored file and return the object.
//    /// </summary>
//    /// <param name="storageId"></param>
//    /// <param name="storageProviderType"></param>
//    /// <returns></returns>
//    private FileData GetFileMetaData(Guid storageId, Dictionary<string, object> strResult) {
//      FileData filePayload = new FileData();
//      //IStorageService service = StorageServiceFactory.GetDefaultStorageService();
//      //Dictionary<string, object> strResult = service.QueryFileMetadata(storageId).Result;
//      // If file not found then raising the exception.
//      if(strResult.Count == 0)
//        RaiseFileNotFoundException();

//      filePayload.FileName = Convert.ToString(strResult["Filename"]);
//      string mimeType = FileSupport.GetFileMimeType(filePayload.FileName);
//      filePayload.MimeType = mimeType;
//      filePayload.FilePath = Convert.ToString(strResult["FilePath"]);

//      return filePayload;
//    }

//    /// <summary>
//    /// Copy the original file into generated temp location path and return the temp browser viewable public path.
//    /// </summary>
//    /// <param name="storageId"></param>
//    /// <param name="originalPath"></param>
//    /// <param name="fileName"></param>
//    /// <returns>Return the temp office document path.</returns>
//    private string GetPublicTempPath(Guid storageId, string originalPath, string fileName) {
//      if(!File.Exists(originalPath))
//        RaiseFileNotFoundException();
//      // Generating the temp path for office document.
//      string filePath = Path.Combine(ConfigHelper.GetTempDocumentRootPath(), storageId.ToString());
//      if(!Directory.Exists(filePath))
//        Directory.CreateDirectory(filePath);

//      filePath = Path.Combine(filePath, fileName);

//      // Copy the original file into generated temp location path.
//      if(File.Exists(filePath))
//        File.Delete(filePath);
//      System.IO.File.Copy(originalPath, filePath);
//      // TODO: add the temp document url into the config file.
//      string tempurl = Path.Combine(ConfigHelper.GetDocumentUrl().Replace("Services/Documents", "TempDocuments"), storageId.ToString(), fileName);

//      return GetOfficeViewerUrl() + tempurl;
//    }

//    /// <summary>
//    /// return office document viewer supportive url.
//    /// </summary>
//    /// <returns></returns>
//    private string GetOfficeViewerUrl() {
//      return "https://view.officeapps.live.com/op/embed.aspx?src=";
//    }

//    /// <summary>
//    /// Generate filenotfound exception and raise it.
//    /// </summary>
//    private void RaiseFileNotFoundException() {
//      IList<EwpErrorData> errorDataList = new List<EwpErrorData>();
//      EwpErrorData errorData = new EwpErrorData() {
//        ErrorSubType = (int)ValidationErrorSubType.InvalidFieldValue,
//        Data = "DocumentNotFound",
//        Message = string.Format(ServerMessages.FileNotFound)
//      };
//      errorDataList.Add(errorData);
//      throw new EwpValidationException(ServerMessages.FileNotFound, errorDataList);
//    }

//    #endregion Support

//  }
//}
