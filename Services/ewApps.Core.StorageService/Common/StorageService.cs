using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.Xml.Linq;

namespace ewApps.Core.StorageService {

  public abstract class StorageService:IStorageService {
    /// <summary>
    /// This class is base class for Storage services.
    /// FileHandle is the unique handle to the file in the StorageProvider store.
    /// Its type is string. If a storage provider uses a different type (int or Guid etc)
    /// that will be serialized and de-serialized to string before use.
    /// </summary>

    // Provider type, initialized in the derived class
    //private StorageServiceProviderType _provider = StorageServiceProviderType.EwApps;
    //public StorageServiceProviderType Provider {
    //  get {
    //    return _provider;
    //  }
    //  private set {
    //  }
    //}

    //public StorageService(StorageServiceProviderType provider) {
    //  this._provider = provider;
    //}

    #region Document Ops

    /// <summary>
    /// Download document
    /// </summary>
    /// <param name="fileHandle"></param>
    /// <param name="token"></param>
    /// <returns>FileStream if successful</returns>
    public abstract Task<FileStream> GetFile(Guid storageFileId, string fileName, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Upload document
    /// </summary>
    /// <param name="fileStream"></param>
    /// <param name="uploadOptions"></param>
    /// <param name="storagOptions"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public abstract Task<FileUploadResponseModel> UploadFile(Stream blockStream, UploadOptions uploadOptions, StorageOptions storagOptions, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Overwrite a stored file. This is essentially a two step process:
    /// (1) Delete the current stored file
    /// (2) Upload the updated file
    /// This will generate a NEW file handle
    /// </summary>
    public abstract Task<FileUploadResponseModel> UpdateFile(Guid storageFileId, Stream fileStream, UploadOptions uploadOptions, StorageOptions storagOptions, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Delete file
    /// </summary>
    /// <param name="fileHandle"></param>
    /// <param name="token"></param>
    /// <returns>True if successful</returns>
    public abstract Task<bool> DeleteFile(Guid storageFileId, CancellationToken token = default(CancellationToken));

    public abstract Task<bool> DeleteFile(Guid documentId, int versionNumber, CancellationToken token = default(CancellationToken));

    #endregion Document Ops

    #region Non-Doc Ops

    /// <summary>
    /// Generate a thumbnail for an uploaded file.
    /// The thumbnail may be generated only for certail mime types.
    /// Like, Image files, Video files.
    /// For other files, it will return null.
    /// </summary>
    /// <param name="fileHandle"></param>
    /// <param name="thumbnailParams"></param>
    /// <param name="storageOptions"></param>
    /// <param name="token"></param>
    /// <returns>Thumbnail stream</returns>
    public abstract Stream GenerateThumbnail(Guid storageFileId, ThumbnailParameters thumbnailParams, StorageOptions storageOptions, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Gets stored file's metadata
    /// </summary>
    /// <param name="filehandle"></param>
    /// <returns>All metadata files in the form of key/value pairs</returns>
    public abstract Task<Dictionary<string, object>> QueryFileMetadata(Guid storageFileId, CancellationToken token = default(CancellationToken));

    #endregion Non-Doc Ops

    #region PublicUrl
    /// <summary>
    /// Generates the public URL and Save in the table
    /// </summary>
    /// <param name="storageFileId"></param>
    /// <param name="storageServiceProvider"></param>
    /// <param name="expirationModel"></param>
    /// <returns></returns>
    //public string GeneratePublicUrl(Guid storageFileId, ExpirationModel expirationModel) {
    //  //Get the URL from the underline storage provider
    //  PublicUrlModel model = GeneratePublicUrlFromStorageProvider(storageFileId, expirationModel);
    //  //Save the public URL in database
    //  PublicUrlDataService publicUrlService = new PublicUrlDataService();
    //  StorageServiceProviderType ssProvider = GetStorageProviderType();
    //  string url = publicUrlService.AddPublicUrl(model, storageFileId, ssProvider, expirationModel);
    //  return url;
    //}

//    protected abstract PublicUrlModel GeneratePublicUrlFromStorageProvider(Guid storageFileId, ExpirationModel expirationModel);
//    protected abstract StorageServiceProviderType GetStorageProviderType();

    /*UNUSED
     * /// <summary>
        /// Gets the Publicurl from table
        /// </summary>
        /// <param name="storageFileId"></param>
        /// <returns></returns>
        public string GetPublicUrl(Guid storageFileId)
        {
          //create the dataservice instance
          PublicUrlDataService service = new PublicUrlDataService();
          string url = service.GetPublicUrl(storageFileId);
          return url;
        }
     */
    #endregion

    #region Static Methods

    //public static StorageServiceProviderType GetDefaultStorageProviderType() {
    //  return StorageServiceProviderType.EwApps;
    //}

    #endregion Static Methods

  }
}
