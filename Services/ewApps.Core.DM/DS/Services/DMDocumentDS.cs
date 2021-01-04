/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar<abadgujar@batchmaster>
 * Date: 22 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 22 August 2019
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ewApps.Core.DMService {

    /// <summary>
    /// Represents all the operations to be performed on document entity.
    /// </summary>
    public class DMDocumentDS:BaseDS<DMDocument>, IDMDocumentDS {

        #region Local Member  

        IDMDocumentRepository _dmDocumentRepository;
        IUserSessionManager _userSessionManager;
        IDMThumbnailDS _dmThumbnailDataService;
        // ThumbnailAppSettings _thumbnailSetting;
        IUnitOfWorkDM _unitOfWork;
        IStorageService _storageService;
        IDMFileStorageDS _fileStorageDS;
        IOptions<DMServiceSettings> _appSetting;


        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="dmDocumentRepository">DocumentRepository class instance</param>
        /// <param name="cacheService">Cache service class instance</param>    
        /// <param name="userSessionManager">Usersession class instance</param>    
        /// <param name="appSetting">App setting class instance</param>
        /// <param name="dmThumbnailDataService">Thumbnail class instance</param>
        /// <param name="fileStorageDS">Filestorage DS class instance</param>
        /// <param name="storageService">Storage class instance</param>
        public DMDocumentDS(IUnitOfWorkDM unitOfWork, IOptions<DMServiceSettings> appSetting, IDMFileStorageDS fileStorageDS, IStorageService storageService, IDMDocumentRepository dmDocumentRepository, IUserSessionManager userSessionManager, IDMThumbnailDS dmThumbnailDataService) : base(dmDocumentRepository) {
            _dmDocumentRepository = dmDocumentRepository;
            _userSessionManager = userSessionManager;
            _dmThumbnailDataService = dmThumbnailDataService;
            _storageService = storageService;
            _fileStorageDS = fileStorageDS;
            _appSetting = appSetting;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        #region Public Methods

        /// <inheritdoc/>
        public Guid AddDocument(DMDocument doc) {
            DMDocument entity = Add(doc);

            //return id;
            return entity.ID;
        }

        /// <inheritdoc/>
        public string AddDocumentToStorage(AddUpdateDocumentModel model, HttpRequest httpRequest) {
            // Uploading Document is processed in following steps:
            // 1. Validate HttpRequest
            // 2. Setup FolderId
            // 3. Setup Thumbnail entity
            // 4. Create and save Thumbnail
            // 5. Setup Document entity
            // 6. Create and save Document
            string str = null;
            try {
                // Chekck for attachment file in model
                if(model.HasFiles) {
                    model.DocumentId = Guid.Empty;
                    Guid folderId = model.FolderId;
                    UserSession session = _userSessionManager.GetSession();

                    foreach(IFormFile file in httpRequest.Form.Files) {

                        // DocumentReference ID Generate for Temp Path Only
                        Guid docRefId = Guid.NewGuid();

                        // TenantId
                        Guid tenantId = session.TenantId;

                        // Generate Temp file name for thumbnail. Always JPG.
                        string dt = DateTime.UtcNow.ToString();
                        string fn = Regex.Replace(dt, "[^b-lnoq-zB-LNOQ-Z0-9]+", "", RegexOptions.Compiled);
                        fn = fn + ".jpg";

                        // Setup Thumbnail Entity
                        DMThumbnail tn = new DMThumbnail();
                        tn.FileName = fn;
                        tn.DocumentFileName = "tempcomment";
                        tn.FileSizeinKB = file.Length;
                        tn.Height = model.ReqThumbnailHeight;
                        tn.Width = model.ReqThumbnailWidth;
                        tn.MediaType = model.MediaType;
                        tn.FileExtension = ".jpg";
                        tn.CreatedBy = session.TenantUserId;
                        tn.UpdatedBy = tn.CreatedBy;
                        tn.CreatedOn = System.DateTime.UtcNow;
                        tn.UpdatedOn = tn.CreatedOn;
                        tn.TenantId = tenantId;
                        tn.OwnerEntityId = model.DocOwnerEntityId;
                        //          _dmThumbnailDataService.Add(tn);

                        // Setup Document Entity
                        DMDocument doc = new DMDocument();
                        FileInfo fileInfo = new FileInfo(file.FileName);

                        doc.FileName = file.FileName;
                        doc.FileSizeinKB = (float)tn.FileSizeinKB;
                        Guid userId = session.TenantUserId;
                        doc.Description = model.Description;
                        doc.OwnerEntityId = model.DocOwnerEntityId;
                        doc.FileExtension = fileInfo.Extension;
                        doc.Title = model.Title;
                        doc.TenantId = tenantId;

                        str = AddDocumentToStorage(file.OpenReadStream(), doc, tn);

                        Save();

                        _dmThumbnailDataService.SaveAsync();
                        //            _unitOfWork.SaveAll();
                        _fileStorageDS.SaveAsync();
                    }
                }
            }
            catch(Exception excccc) {
            }
            //}

            return str;
        }


        /// <inheritdoc/>
        public string AddDocumentToStorage(Stream fStream, DMDocument doc, DMThumbnail thumb) {
            UserSession session = _userSessionManager.GetSession();
            Guid fileHanlde = Guid.Empty;
            Guid docId = Guid.NewGuid();
            Guid tenantId = session.TenantId;

            // To save the document and generate the thumbnail of stored document. creating the storage provider objects.        
            fileHanlde = UploadDocumentToStorage(fStream, doc);
            doc.FileStorageId = fileHanlde;

            // Adding thumbnail.
            thumb.DocumentId = docId;
            //thumb.OwnerEntityType = (int)CoreEntityTypeEnum.SupportTicket;

            // Get document thumbnail.
            DMThumbnail docThumbnailEntity = _dmThumbnailDataService.Add(thumb);
            doc.CreatedBy = session.TenantUserId;
            doc.UpdatedBy = doc.CreatedBy;
            doc.CreatedOn = DateTime.UtcNow;
            doc.UpdatedOn = doc.CreatedOn;
            doc.AppId = session.AppId;
            doc.ID = docId;
            //doc.OwnerEntityType = (int)CoreEntityTypeEnum.SupportTicket;
            doc.Title = System.IO.Path.GetFileNameWithoutExtension(doc.FileName);

            // Adding document information into document table.
            docId = this.AddDocument(doc);

            // Now generate thumbnail for doc
            GenerateThumbnail(doc, thumb, doc.FileStorageId);

            return fileHanlde.ToString();
        }

        /// <inheritdoc/>
        public List<DocumentResponseModel> GetDocumentsByTicketId(Guid ticketId) {
            // get fromdatadata dreomfrom DB 
            List<DocumentResponseModel> responseModel = _dmDocumentRepository.GetDocumentsByTicketId(ticketId);

            if(responseModel != null) {
                // Make URLs for thumbnail and Document
                foreach(DocumentResponseModel model in responseModel) {
                    model.DocumentURL = Path.Combine(_appSetting.Value.DocumentUrl, model.TenantId.ToString(), model.FileStorageId.ToString(), model.FileName);
                    model.ThumbnailURL = Path.Combine(_appSetting.Value.ThumbnailUrl, model.TenantId.ToString(), model.FileStorageId.ToString(), model.ThumbnailFileName);
                }
            }


            return responseModel;
        }

        /// <inheritdoc/>
        public async Task<FileStream> GetFileStream(Guid documentId, Guid storageFileId, string fileName, Guid tenantId) {
            FileStream fileStream = null;
            string filePath = string.Empty;

            //Check if documentId is not null
            if(storageFileId != Guid.Empty) {

                // get the file to temp location with Guid directory
                filePath = Path.Combine(_appSetting.Value.DocumentRootPath, tenantId.ToString(), storageFileId.ToString());

                bool directoryExists = Directory.Exists(filePath);

                //Check the directory exists or not
                if(directoryExists) {
                    filePath = filePath + "\\" + fileName;
                    fileStream = FileHelper.GetFileStreamFromFilePath(@filePath);
                    return fileStream;
                }
            }
            return fileStream;
        }

        /// <inheritdoc/>
        public async Task<DocumentResponseModel> GetDocumentDetailById(Guid documentId) {
            return await _dmDocumentRepository.GetDocumentDetailById(documentId);
        }

        #endregion Public Methods

        #region Private Methods.

        // Generate docuemnt entity by given model.
        private DMDocument GenerateDocumentEntity(AddUpdateDocumentDTO model, int OwnerEntityType) {
            DMDocument doc = new DMDocument();

            // Get file  info.
            FileInfo fileInfo = new FileInfo(model.DocFileName);
            doc.FileName = model.DocFileName;
            doc.FileSizeinKB = model.DocFileSizeInKB;
            doc.Description = model.Description;
            doc.OwnerEntityType = OwnerEntityType;
            doc.FileExtension = fileInfo.Extension;
            doc.ID = model.DocumentId;

            return doc;
        }

        // Generate thumbanil entity by given model.
        private EntityThumbnail GenerateThumbnailEntity(AddUpdateDocumentDTO model, int OwnerEntityType) {
            EntityThumbnail tn = new EntityThumbnail();

            string dt = DateTime.UtcNow.ToString();
            string fn = Regex.Replace(dt, "[^b-lnoq-zB-LNOQ-Z0-9]+", "", RegexOptions.Compiled);
            fn = fn + ".jpg";
            tn.FileName = fn;
            tn.DocumentFileName = model.DocFileName;
            tn.FileSizeinKB = model.DocFileSizeInKB;
            tn.Height = model.ReqThumbnailHeight;
            tn.Width = model.ReqThumbnailWidth;
            tn.OwnerEntityType = OwnerEntityType;
            tn.MediaType = model.MediaType;
            tn.FileExtension = ".jpg";

            return tn;
        }

        // Method is used to upload document .
        private string UploadDocumentToStorage(AddUpdateDocumentDTO model) {
            // Uploading Document is processed in following steps:
            // 1. Validate HttpRequest
            // 2. Setup FolderId
            // 3. Setup Thumbnail entity
            // 4. Create and save Thumbnail
            // 5. Setup Document entity
            // 6. Create and save Document

            bool add = model.DocumentId == Guid.Empty;
            Guid docRefId = Guid.NewGuid();

            // Generate Temp file name for thumbnail. Always JPG.
            string dt = DateTime.UtcNow.ToString();
            string fn = Regex.Replace(dt, "[^b-lnoq-zB-LNOQ-Z0-9]+", "", RegexOptions.Compiled);
            fn = fn + ".jpg";

            // Setup Thumbnail Entity
            DMThumbnail tn = new DMThumbnail();
            tn.FileName = fn;
            tn.FileSizeinKB = model.DocFileSizeInKB;
            tn.Height = model.ReqThumbnailHeight;
            tn.Width = model.ReqThumbnailWidth;
            tn.MediaType = model.MediaType;
            tn.FileExtension = ".jpg";

            // Setup Document Entity
            DMDocument doc = new DMDocument();
            doc.FileName = model.DocFileName;
            doc.Description = model.Description;
            doc.Title = model.Title;

            return AddDocumentToStorage(null, doc, tn);
        }

        // Upload document information o a physical location.
        private Guid UploadDocumentToStorage(Stream fStream, DMDocument doc) {
            UploadOptions uploadOption = new UploadOptions();
            StorageOptions storageOption = new StorageOptions(_appSetting);
            ThumbnailParameters param = new ThumbnailParameters();
            UserSession session = _userSessionManager.GetSession();

            Guid tenantId = session.TenantId;
            Guid fileHandle = Guid.Empty;
            StorageOptions stOption = storageOption;

            if(stOption != null) {
                stOption.Filename = doc.FileName;
                stOption.DocumentId = doc.ID;
                stOption.TenantId = tenantId;
                stOption.Size = doc.FileSizeinKB;
            }

            uploadOption.FullBlockUpload = true;
            uploadOption.UploadId = Guid.NewGuid();

            // Saving the document into storage provider.
            FileUploadResponseModel model = _storageService.UploadFile(fStream, uploadOption, storageOption).Result;
            fileHandle = model.StorageId;
            StoreFileMetadata(model, stOption);

            return fileHandle;
        }

        public Guid UploadDocumentFileToStorage(Stream fStream, AddUpdateDocumentModel doc, bool useDocumentIdAsStrorageId) {
            UploadOptions uploadOption = new UploadOptions();
            StorageOptions storageOption = new StorageOptions(_appSetting);
            storageOption.useDocumentIdAsStrorageId = useDocumentIdAsStrorageId;
            ThumbnailParameters param = new ThumbnailParameters();
            UserSession session = _userSessionManager.GetSession();

            Guid tenantId = session.TenantId;
            Guid fileHandle = Guid.Empty;
            StorageOptions stOption = storageOption;

            if(stOption != null) {
                stOption.Filename = doc.DocFileName;
                stOption.DocumentId = doc.DocumentId;
                stOption.TenantId = tenantId;
                stOption.Size = doc.DocFileSizeInKB;
            }

            uploadOption.FullBlockUpload = true;
            uploadOption.UploadId = Guid.NewGuid();

            // Saving the document into storage provider.
            FileUploadResponseModel model = _storageService.UploadFile(fStream, uploadOption, storageOption).Result;
            fileHandle = model.StorageId;
            StoreFileMetadata(model, stOption);

            _fileStorageDS.SaveAsync();

            return fileHandle;
        }

        // Save File storage information.
        private Guid SaveStorageInfo(StorageOptions storagOptions, DMFileStorage storage, string fileFullPath, bool add) {
            DMFileStorage file = null;
            StorageOptions ewAppsStorageOption = storagOptions;
            UserSession session = _userSessionManager.GetSession();

            if(ewAppsStorageOption == null)
                return Guid.Empty;

            //storage.DocumentId = ewAppsStorageOption.DocumentId;
            storage.FileName = ewAppsStorageOption.Filename;
            storage.FilePath = fileFullPath;
            storage.Size = (float)ewAppsStorageOption.Size;

            storage.CreatedBy = session.TenantUserId;
            storage.UpdatedBy = storage.CreatedBy;
            storage.CreatedOn = DateTime.UtcNow;
            storage.UpdatedOn = storage.CreatedOn;
            storage.TenantId = session.TenantId;

            if(add) {
                file = _fileStorageDS.Add(storage);
                return file.ID;
            }
            else {
                _fileStorageDS.Update(storage, null);
                return Guid.Empty;
            }
        }

        // Save File meta information.
        private void StoreFileMetadata(FileUploadResponseModel model, StorageOptions storagOptions) {
            if(model.UploadStorageStatus == FileStorageStatus.Uploaded) {
                DMFileStorage storage = new DMFileStorage();
                storage.ID = model.StorageId;
                storage.MimeType = FileSupport.GetFileMimeType(storagOptions.Filename);
                SaveStorageInfo(storagOptions, storage, model.StorageFilePath, true);
            }
        }

        // Generate thumbnail for Doc.
        private void GenerateThumbnail(DMDocument doc, DMThumbnail thumb, Guid storageFileId) {
            ThumbnailParameters param = new ThumbnailParameters();
            StorageOptions stOption = new StorageOptions(_appSetting);

            param.FileName = thumb.FileName;
            param.ThumbnailId = thumb.ID.ToString();
            stOption.Filename = doc.FileName;
            stOption.DocumentId = doc.ID;
            stOption.TenantId = doc.TenantId;
            stOption.Size = doc.FileSizeinKB;

            // Generating the stored document thumbnail and save into a location.
            _storageService.GenerateThumbnail(storageFileId, param, stOption);
        }

        #endregion Private Methods.

        #region Delete

        /// <summary>
        /// To delete the file.
        /// </summary>
        /// <param name="storageId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task DeleteFileAsync(Guid storageId, CancellationToken token = default(CancellationToken)) {
            await _fileStorageDS.DeleteAsync(storageId, token);
            await _storageService.DeleteFile(storageId, token);
            _fileStorageDS.Save();
        }

        #endregion Delete

    }
}