using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ewApps.Core.DMService {
  public interface IStorageService {
    /// <summary>
    /// Interface for StorageService
    /// </summary>
    /// <see cref="StorageService.cs"/>

    Task<bool> DeleteFile(Guid storageFileId, CancellationToken token = default(CancellationToken));
    Task<bool> DeleteFile(Guid documentId, int versionNumber, CancellationToken token = default(CancellationToken));
    Stream GenerateThumbnail(Guid storageFileId, ThumbnailParameters thumbnailParams, StorageOptions storageOptions, CancellationToken token = default(CancellationToken));
    Task<FileStream> GetFile(Guid storageFileId, string fileName, CancellationToken token = default(CancellationToken));
    Task<Dictionary<string, object>> QueryFileMetadata(Guid storageFileId, CancellationToken token = default(CancellationToken));
    Task<FileUploadResponseModel> UploadFile(Stream blockStream, UploadOptions uploadOptions, StorageOptions storagOptions, CancellationToken token = default(CancellationToken));
    Task<FileUploadResponseModel> UpdateFile(Guid storageFileId, Stream blockStream, UploadOptions uploadOptions, StorageOptions storagOptions, CancellationToken token = default(CancellationToken));

//    string GeneratePublicUrl(Guid storageFileId, ExpirationModel expirationModel);
  }
}