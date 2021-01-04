using System;
using System.IO;
using Microsoft.Extensions.Options;
//using Microsoft.Extensions.Options;

namespace ewApps.Core.StorageService {

    public class StorageOptions {

    static DMServiceSettings _thumbnailSetting  ;

    //public StorageOptions() {
    //}

    public StorageOptions(IOptions<DMServiceSettings> appSetting)
    {
      _thumbnailSetting = appSetting.Value;
    }

    /// <summary>
    /// Base class for storage options used in storing files when uploading.
    /// Each provider will have its derived class.
    /// Note that some options set here are not binding. They may be ignored by the provider. 
    /// </summary>
    /// 

    // Filename with extension used to store the uploaded file in the storage.
    // This is just a hint, not binding
    public string Filename = "";

    // Document Id and VersionNumber
    // Used: (1) Maybe used to generate storage file name/path; (2) As foreign key in Storage table
    public Guid DocumentId;
    public int VersionNumber = 1;

    // TenantId, maybe used to generate storage file name/path
    public Guid TenantId;

    // File size in bytes. Optional
    public double Size = 0;

    /// <summary>
    /// To add custom properties for StorageOption.
    /// </summary>
    public System.Collections.Generic.Dictionary<string, object> CustomData = new System.Collections.Generic.Dictionary<string, object>();

    /// <summary>
    /// Return application storage path with the tenantid and storageid.
    /// For example: documentRootPath/TenantId/StorageId
    /// </summary>
    /// <param name="storageId"></param>
    /// <returns></returns>
    public virtual string GetStorageFileDirectoryPath(Guid storageId) {
      // Uncoment this code
      string sRoot = _thumbnailSetting.DocumentRootPath;
//ConfigHelper.GetDocumentRootPath();
      //string sRoot = "";
      string sTenantId = this.TenantId.ToString();
      string sDocId = storageId.ToString();
      string filePath = Path.Combine(sRoot, sTenantId, sDocId);
      return filePath;
    }

  }
}
