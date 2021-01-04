using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.CommonService;
using Microsoft.Extensions.Options;
//using Microsoft.Extensions.Options;

namespace ewApps.Core.StorageService {

    public class EWAppsStorageService:StorageService, IStorageService {
    //Public url method signature(URL)
    private const string PublicUrlPath = "/files/{0}/public";
    static DMServiceSettings _thumbnailSetting;
    public IOptions<DMServiceSettings> _appSetting;

    public EWAppsStorageService( IOptions<DMServiceSettings> thumbnailSetting) 
// : base(StorageServiceProviderType.EwApps) 
{
      _thumbnailSetting = thumbnailSetting.Value;
      _appSetting = thumbnailSetting;
    }
    
    #region Document Ops

    public override async Task<FileStream> GetFile(Guid storageFileId, string fileName, CancellationToken token = default(CancellationToken)) {
        FileStream fileStream = null;

        StorageOptions stOption = new StorageOptions(_appSetting);
      //      stOption.TenantId = EwAppSessionManager.GetLoginUserSession().TenantId;
      stOption.TenantId = Guid.Empty; 

        // Get storage record
        string dirPath = stOption.GetStorageFileDirectoryPath(storageFileId);
        string filePath = Path.Combine(dirPath, fileName);

        // Get the file path from storageprovider table or gewnerate the path as per saved meta data in STorageProvider table.      
        bool fileExists = File.Exists(filePath);

        // Check the file exists or not
        if(fileExists) {
          fileStream = FileHelper.GetFileStreamFromFilePath(filePath);
          return fileStream;
        }
        return fileStream;


      return null;
    }

    /// <summary>
    /// Generate the document path and Save the document in EwApps Storage.    
    /// </summary>
    /// <param name="blockStream"></param>
    /// <param name="uploadOptions"></param>
    /// <param name="storagOptions"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public override async Task<FileUploadResponseModel> UploadFile(Stream blockStream, UploadOptions uploadOptions, StorageOptions storagOptions, CancellationToken token = default(CancellationToken)) {
      // Validate
      if(uploadOptions == null || storagOptions == null)
        return new FileUploadResponseModel();

        return StoreFile(blockStream, uploadOptions, storagOptions, token);
    }

    /// <summary>
    /// Generate the document path and Save the document in EwApps Storage.    
    /// </summary>
    /// <param name="blockStream"></param>
    /// <param name="uploadOptions"></param>
    /// <param name="storagOptions"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public override async Task<FileUploadResponseModel> UpdateFile(Guid storageFileId, Stream blockStream, UploadOptions uploadOptions, StorageOptions storagOptions, CancellationToken token = default(CancellationToken)) {
        return await Task.Run(() => {
          return UpdateFileToStore(storageFileId, blockStream, uploadOptions, storagOptions, token);
        });

      return new FileUploadResponseModel();
    }

    /// <summary>
    /// Delete the stored document meta data.
    /// </summary>
    /// <param name="storageFileId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public override async Task<bool> DeleteFile(Guid storageFileId, CancellationToken token = default(CancellationToken)) {
      return await Task.Run(() => {
        try {
          // Delete the physical file
          string dirPath = new StorageOptions(_appSetting).GetStorageFileDirectoryPath(storageFileId);
          if(!string.IsNullOrEmpty(dirPath) && Directory.Exists(dirPath))
            Directory.Delete(dirPath, true);

          return true;
        }
        catch {
          return false;
        }
      });
    }

    /// <summary>
    /// Delete storage row and document with specific version number.
    /// If version number is negative then, it will delete all documents.
    /// </summary>
    /// <param name="documentId"></param>
    /// <param name="versionNumber"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public override async Task<bool> DeleteFile(Guid documentId, int versionNumber, CancellationToken token = default(CancellationToken)) {
      return true;
    }

    /// <summary>
    /// Deleteing the document file from physical location.
    /// </summary>
    /// <param name="documentId"></param>
    /// <param name="tenantId"></param>
    private void DeleteDocumentFileAndThumbnail(string documentId, string tenantId) {
      try {
        string sRoot = "";

        //string sRoot = CommonRuntime.Common.ConfigHelper.GetDocumentRootPath();
        string filePath = Path.Combine(sRoot, tenantId, documentId);
        if(Directory.Exists(filePath))
          Directory.Delete(filePath, true);

        sRoot = "";

        //sRoot = CommonRuntime.Common.ConfigHelper.GetThumbnailPath();
        filePath = Path.Combine(sRoot, tenantId, documentId);
        if(Directory.Exists(filePath))
          Directory.Delete(filePath, true);
      }
      catch(System.IO.DirectoryNotFoundException) {
        // Ignore this exception if already deleted.
      }
    }

    #endregion Document Ops

    #region Non-Doc Ops
    /// <summary>
    /// Gets the thumbnail base64.
    /// </summary>
    /// <param name="filePath">The base64 string.</param>
    /// <param name="fileName">Name of the file.</param>
    /// <returns></returns>
    public static string GetThumbnailBase64(string filePath, string fileName, out int width, out int height, int actualWidth = 0, int actualHeight = 0) {
      Bitmap bitmap = null;
      bool isMagicGenerator = false;
      string defaultFileFullPath ="";

      string[] docArr = { ".DOCX", ".DOC", ".DOCM", ".DOT", ".DOTX", ".DOTM", ".DOCB" };
      string[] pdfArr = { ".PDF", ".ACROBAT" };
      string[] audioArr = { ".AA", ".AAC", ".AAX", ".ACT", ".AIFF", ".AMR", ".APE", ".AU", ".AWB", ".DCT", ".DSS", ".DVF", ".FLAC", ".GSM", ".IKLAX", ".IVS", ".M4A", ".M4B", ".MMF", ".MP3", ".MP2", ".MSV", ".WAV", ".WMA", ".MPC", ".MV", ".OGG" };
      string[] vidArr = { ".3GP", ".AVI", ".MOV", ".MP4", ".MPEG", ".RP", ".RM", ".WEBM", ".MKV", ".VOB", ".OGV", ".WMV", ".MP2", ".SVI", ".DRC", ".YUV", ".MPE", ".MSF", ".MNG", ".ASF", ".MPV", ".NSV", ".QT", ".M4P", ".M4V", ".M2V", ".MPG", ".ROQ" };
      string[] zipArr = { ".ISO", ".LBR", ".MAR", ".TAR", ".ZIP", ".7Z", ".GZ", ".CAB", ".DMG", ".JAR", ".PAK", ".RAR", ".ZIPX" };
      string[] excelArr = { ".XLSX", ".XLS", ".XLSM", ".XLSB", ".XLTM", ".XLAM", ".XLA", ".XLB", ".XLC", ".CSV" };
      string[] pptArr = { ".PPT", ".POT", ".PPS", ".PPTX", ".PPTM", ".POTX", ".POTM", ".PPAM", ".PPSX", ".SLDX", ".SLDM" };
      string[] imgArr = { ".JPG", ".JPEG", ".GIF", ".PNG", ".BMP", ".ICO" };

      string fileFullPath = Path.Combine(filePath, fileName);

      //if (!File.Exists(fileFullPath)) {
      //  IList<EwpErrorData> errorDataList = new List<EwpErrorData>();
      //  EwpErrorData errorData = new EwpErrorData() {
      //    ErrorSubType = (int)ValidationErrorSubType.FieldRequired,
      //    Data = "File",
      //    Message = string.Format(ServerMessages.FileNotFound)
      //  };
      //  errorDataList.Add(errorData);
      //  throw new EwpValidationException(ServerMessages.FileNotFound, errorDataList);
      //}

      string base64StringThumb = string.Empty;

      FileInfo fileInfo = new FileInfo(fileName);
      TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));

      //check for audio files  
      if (audioArr.Any(x => x == fileInfo.Extension.ToUpper())) {
        //bitmap =(Bitmap)converter.ConvertFrom(ewApps.Core.Common.Resources.ewAppsResource.audio);
        defaultFileFullPath = Path.Combine(_thumbnailSetting.DefaultImagePath, "audio.png");
        base64StringThumb = GetBase64FromFilePath(defaultFileFullPath);
      }

      else if (pdfArr.Any(x => x == fileInfo.Extension.ToUpper())) {
        //check for pdf files
        //bitmap = (Bitmap)converter.ConvertFrom(ewApps.Core.Common.Resources.ewAppsResource.pdf);
        defaultFileFullPath = Path.Combine(_thumbnailSetting.DefaultImagePath, "pdf.png");
        base64StringThumb = GetBase64FromFilePath(defaultFileFullPath);
      }

      else if (docArr.Any(x => x == fileInfo.Extension.ToUpper())) {
        //check for doc files
//        bitmap = (Bitmap)converter.ConvertFrom(ewApps.Core.Common.Resources.ewAppsResource.doc);
        defaultFileFullPath = Path.Combine(_thumbnailSetting.DefaultImagePath, "doc.png");
        base64StringThumb = GetBase64FromFilePath(defaultFileFullPath);

      }

      else if (zipArr.Any(x => x == fileInfo.Extension.ToUpper())) {
        //check for Zip files
//        bitmap =(Bitmap)converter.ConvertFrom(ewApps.Core.Common.Resources.ewAppsResource.zip);
        defaultFileFullPath = Path.Combine(_thumbnailSetting.DefaultImagePath, "zip.png");
        base64StringThumb = GetBase64FromFilePath(defaultFileFullPath);

      }

      else if (excelArr.Any(x => x == fileInfo.Extension.ToUpper())) {
        //check for Excel files
        // bitmap =(Bitmap)converter.ConvertFrom(ewApps.Core.Common.Resources.ewAppsResource.xls);
        defaultFileFullPath = Path.Combine(_thumbnailSetting.DefaultImagePath, "xls.png");
        base64StringThumb = GetBase64FromFilePath(defaultFileFullPath);
      }

      else if (pptArr.Any(x => x == fileInfo.Extension.ToUpper())) {
        //check for ppt files
        //bitmap = (Bitmap)converter.ConvertFrom(ewApps.Core.Common.Resources.ewAppsResource.ppt);
        defaultFileFullPath = Path.Combine(_thumbnailSetting.DefaultImagePath, "ppt.png");
        base64StringThumb = GetBase64FromFilePath(defaultFileFullPath);
      }

      else if (vidArr.Any(x => x == fileInfo.Extension.ToUpper())) {
/*
        //check if actual height and width provided by client 
        //the we'll store the same else calculate the size
        if (actualHeight == 0) {
          GetVideoFrameSize(fileFullPath, out actualWidth, out actualHeight);
        }

*/
        //  Get thumbnail bitmap for Video or Image
       // bitmap = WindowsThumbnail.GetThumbnailBitmap(fileFullPath, actualWidth, actualHeight);
      }

      else if (imgArr.Any(x => x == fileInfo.Extension.ToUpper())) {
        isMagicGenerator = true;
        string tempPath = Path.Combine(_thumbnailSetting.TempDocumentRootPath, Guid.NewGuid().ToString() + ".jpg");
        //Sanjeev Sir Comment
        //base64StringThumb = MagicScaleThumbnail.ImageGenrator(fileFullPath, tempPath);
      }
      else {
        //Get None Thumbnail for all remaining extensions 
        //bitmap = (Bitmap)converter.ConvertFrom(ewApps.Core.Common.Resources.ewAppsResource.none);
        defaultFileFullPath = Path.Combine(_thumbnailSetting.DefaultImagePath, "none.png");
        base64StringThumb = GetBase64FromFilePath(defaultFileFullPath);
      }

      /*
            if (isMagicGenerator == false) {
              base64StringThumb = SaveJpeg(bitmap);
              bitmap.Dispose();
            }
      */

      //width = _width;
      //height = _height;
      width = 1;
      height = 1;

      return base64StringThumb;

    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileHandle"></param>
    /// <param name="thumbnailParams"></param>
    /// <param name="storageOptions"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public override Stream GenerateThumbnail(Guid storageFileId, ThumbnailParameters thumbnailParams, StorageOptions storageOptions, CancellationToken token = default(CancellationToken)) {
      string tenantId = storageOptions.TenantId.ToString();
      string documentId = storageOptions.DocumentId.ToString();
      string tnId = thumbnailParams.ThumbnailId.ToString();

      // Ewapps support versioning, So save thumbnail with version#.
      string VersionNumber = Convert.ToString(storageOptions.VersionNumber);
      //string fileRootPath = Path.Combine(_thumbnailSetting.TempDocumentRootPath, tenantId, storageFileId.ToString());
      string fileRootPath = Path.Combine(_thumbnailSetting.DocumentRootPath, tenantId, storageFileId.ToString());


      //storageOptions.GetStorageFileDirectoryPath(storageFileId);
      // Generating the base64 string for the saved document.
      string base64String = GetThumbnailBase64(fileRootPath, storageOptions.Filename, out thumbnailParams.Width, out thumbnailParams.Height);
      // Save the thumbnail into a location.
      //return await Task.Run(() => { return SaveThumbnailToPhysicalLocation(tenantId, tnId, base64String, thumbnailParams.FileName, storageFileId.ToString()); });
      return SaveThumbnailToPhysicalLocation(tenantId, tnId, base64String, thumbnailParams.FileName, storageFileId.ToString());
    }

    /// <summary>
    /// Gets stored file's metadata
    /// </summary>
    /// <param name="storageFileId"></param>
    /// <returns>All metadata files in the form of key/value pairs</returns>
    public override async Task<Dictionary<string, object>> QueryFileMetadata(Guid storageFileId, CancellationToken token = default(CancellationToken)) {
      return new Dictionary<string, object>();
    }

    #endregion Non-Doc Ops

    #region Chunk Upload

    /// <summary>
    /// Generate the document path and Save the document in EwApps Storage.    
    /// </summary>
    /// <param name="fileStream"></param>
    /// <param name="uploadOptions"></param>
    /// <param name="storagOptions"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public FileUploadResponseModel StoreFile(Stream fileStream, UploadOptions uploadOptions, StorageOptions storagOptions, CancellationToken token = default(CancellationToken)) {
      // Validate
      if(uploadOptions == null || storagOptions == null)
        return null;

        FileUpload fileUpload = new FileUpload(_appSetting);
        FileUploadResponseModel model = fileUpload.UploadFile(fileStream, uploadOptions, storagOptions);

        return model;
    }

    #endregion Chunk Upload

    #region Support Methods

    /// <summary>
    /// Generate the filepath and return it.
    /// </summary>
    /// <param name="storageOption"></param>
    /// <returns></returns>
    private string GenerateFileFullPath(StorageOptions storageOption) {
      string tenantId = storageOption.TenantId.ToString();
      // Generating the directory path to save the document.
      string filePath = GetStorageFilePath(storageOption.TenantId, storageOption.DocumentId, storageOption.VersionNumber);
      if(!Directory.Exists(filePath))
        Directory.CreateDirectory(filePath);

      return Path.Combine(filePath, storageOption.Filename);
    }

    /// <summary>
    /// Save the document into a passed file path.
    /// </summary>
    /// <param name="fileStream"></param>
    /// <param name="fileFullPath"></param>
    private void SaveFileIntoPhysicalLocation(Stream fileStream, string fileFullPath) {
      byte[] buffer = new byte[1024];
      using(System.IO.FileStream output = new FileStream(fileFullPath, FileMode.Create)) {
        int readBytes = 0;
        while((readBytes = fileStream.Read(buffer, 0, buffer.Length)) > 0) {
          output.Write(buffer, 0, readBytes);
        }
      }
    }

    /// <summary>
    /// Generate the document path and Save the document in EwApps Storage.    
    /// </summary>
    /// <param name="blockStream"></param>
    /// <param name="uploadOptions"></param>
    /// <param name="storagOptions"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private FileUploadResponseModel UpdateFileToStore(Guid storageFileId, Stream blockStream, UploadOptions uploadOptions, StorageOptions storagOptions, CancellationToken token = default(CancellationToken)) {

      if(uploadOptions == null)
        return new FileUploadResponseModel();
      Guid fileHanldeId = storageFileId;
      StorageOptions ewAppsStorageOption = storagOptions as StorageOptions;
      if(ewAppsStorageOption == null || fileHanldeId == Guid.Empty)
        return new FileUploadResponseModel();

      FileUpload fUpload = new FileUpload(_appSetting);
      FileUploadResponseModel fileModel = fUpload.UploadFile(blockStream, uploadOptions, storagOptions);
      fileModel.StorageId = storageFileId;

      return fileModel;
    }

    /// <summary>
    /// Save to thumbnail into physical location.
    /// </summary>
    /// <param name="thumbnailId">The thumbnail identifier.</param>
    /// <param name="base64String">The base64 string.</param>
    /// <param name="fileName">Name of the file.</param>
    private Stream SaveThumbnailToPhysicalLocation(string tenantId, string thumbnailId, string base64String, string fileName, string storageId) {

      // Save the file to physical location with Guid directory
      string filePath = GenerateThumbnailRootPath(tenantId, storageId); // Path.Combine(ewApps.CommonRuntime.Common.ConfigHelper.GetThumbnailPath(), tenantId, thumbnailId, storageId);

      // Create thumbnail directory
      if(!Directory.Exists(filePath))
        Directory.CreateDirectory(filePath);

      // File full path
      string fileFullPath = Path.Combine(filePath, fileName);

      byte[] imageBytes = Convert.FromBase64String(base64String);
      MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

      // Convert byte[] to Image
      ms.Write(imageBytes, 0, imageBytes.Length);
      System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
      image.Save(fileFullPath);


      return ms;
    }

    private string GenerateThumbnailRootPath(string tenantId, string storageId) {
      return Path.Combine(_thumbnailSetting.ThumbnailRootPath, tenantId.ToString(), storageId.ToString());

//      return Path.Combine(AppSettingHelper.GetThumbnailPath(), tenantId.ToString(), storageId.ToString());
    }

    /// <summary>
    /// Generates file path, given its constituents.
    /// FilePath = <root>/<tenantId>/<docId>/<versionNumber>
    /// All parts (above) required
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="docId"></param>
    /// <param name="versionNumber"></param>
    /// <returns></returns>
    public static string GetStorageFilePath(Guid tenantId, Guid docId, int versionNumber) {
      string sRoot = _thumbnailSetting.DocumentRootPath;
      // CommonRuntime.Common.ConfigHelper.GetDocumentRootPath();
      // string sRoot = CommonRuntime.Common.ConfigHelper.GetDocumentRootPath();
      string sTenantId = tenantId.ToString();
      string sDocId = docId.ToString();
      string sVersionNumber = versionNumber.ToString();
      string filePath = Path.Combine(sRoot, sTenantId, sDocId, sVersionNumber);
      return filePath;
    }

    public static string GetBase64FromFilePath(string filePath) {
      byte[] bytes = System.IO.File.ReadAllBytes(filePath);
      return Convert.ToBase64String(bytes);
    }

    #endregion Support Methods

  }
}
/*
 
    //#region PublicUrl
    //protected override PublicUrlModel GeneratePublicUrlFromStorageProvider(Guid storageFileId, ExpirationModel expirationModel) {
    //  try {
    //    //GenerateRandomKey by StorageFileID as it is Unique
    //    string publicUrlKey = Guid.NewGuid().ToString().Replace("-", "");
    //    string publicUrl = string.Format(PublicUrlPath, publicUrlKey);
    //    bool isAbsoluteURL = false;
    //    PublicUrlModel model = new PublicUrlModel();
    //    model.PublicUrlKey = publicUrlKey;
    //    model.PublicUrl = publicUrl;
    //    model.IsAbsoluteUrl = isAbsoluteURL;
    //    return model;
    //  }
    //  catch(Exception ex) {
    //    DataServiceExceptionHandler.HandleException(ref ex);
    //  }

    //  return null;

    //}

    //protected override StorageServiceProviderType GetStorageProviderType() {
    //  return StorageService.GetDefaultStorageProviderType();
    //}

    //#endregion
    /// <summary>
    /// Delete storage row and document with specific version number.
    /// If version number is negative then, it will delete all documents.
    /// </summary>
    /// <param name="documentId"></param>
    /// <param name="versionNumber"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public override async Task<bool> DeleteDocument(Guid documentId, int versionNumber, CancellationToken token = default(CancellationToken))
    {

      // Delete storage record for this file
      EwAppsStorageDataService storageDS = new EwAppsStorageDataService();
      DataTable storageDt = null;
      if (versionNumber >= 0)
      {
        storageDt = storageDS.GetStorageAsDT(documentId, versionNumber);
      }
      else
        storageDt = storageDS.GetStorageByDocumentIdAsDT(documentId);

      for (int i = 0; i < storageDt.Rows.Count; i++)
      {
        Guid id = storageDt.Rows[i].Field<Guid>("StorageFileId");
        EwAppsStorage storage = new EwAppsStorage();
        storage.StorageFileId = id;
        storageDS.Delete(storage);

        string filePath = storageDt.Rows[i].Field<string>("FilePath");
        // Delete the physical file
        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
          File.Delete(filePath);
      }
      return true;
    }

    /// <summary>
    /// Generate the thumbnail ans save into a define path.
    /// </summary>
    /// <param name="documentUrl"></param>
    /// <param name="thumbnailParams"></param>
    /// <returns>return generated thumbnail path.</returns>
    public override async Task<string> GenerateThumbnail(ThumbnailParameters thumbnailParams, StorageOptions storageOptions, CancellationToken token = default(CancellationToken))
    {
      string tenantId = storageOptions.TenantId.ToString();
      string documentId = storageOptions.DocumentId.ToString();
      string tnId = thumbnailParams.ThumbnailId.ToString();
      // Ewapps support versioning, So save thumbnail with version#.
      string versionNumber = Convert.ToString(storageOptions.VersionNumber);
      string fileRootPath = Path.Combine(CommonRuntime.Common.ConfigHelper.GetDocumentRootPath(), tenantId, documentId, versionNumber);
      // Generating the base64 string for the saved document.
      string base64String = ewApps.Utils.Thumbnail.ThumbnailGenerator.GetThumbnailBase64(fileRootPath, storageOptions.Filename, out thumbnailParams.Width, out thumbnailParams.Height);
      // Save the thumbnail into a location.
      SaveThumbnailToPhysicalLocation(tenantId, tnId, base64String, thumbnailParams.FileName, versionNumber);

      return Path.Combine(ewApps.CommonRuntime.Common.ConfigHelper.GetThumbnailPath(), tenantId, tnId, versionNumber, thumbnailParams.FileName);
    }

 */
