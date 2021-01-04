using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
//using Microsoft.Extensions.Options;

namespace ewApps.Core.DMService {
    /// <summary>
    /// Enums to define the current file storage status.
    /// </summary>
    public enum FileStorageStatus {
    InProcess = 1, Uploaded = 2
  }

  public enum BlockCacheType {
    InMemory, OnDisk, Hybrid
  }

  public class FileUpload {

    static DMServiceSettings _thumbnailSetting;

    //public FileUpload() {

    //}

    public FileUpload(IOptions<DMServiceSettings> appSetting) {
      _thumbnailSetting = appSetting.Value;
    }

    /// <summary>
    /// Upload the document into a physical location and return the StorageResponseModel.
    /// </summary>
    /// <param name="blockStream"></param>
    /// <param name="stOption"></param>
    /// <param name="upOption"></param>
    /// <returns></returns>
    internal FileUploadResponseModel UploadFile(Stream blockStream, UploadOptions upOption, StorageOptions stOption) {
      BlockCacheType cacheType = GetBlockCacheType(upOption);

      if (HasSingleBlock(upOption))
        // If there is only single chunk to upload then directly save the document into the permenant location.
        return SaveSingleBlockFile(blockStream, upOption, stOption);
      //else if (cacheType == BlockCacheType.InMemory)
      //    return AddFileStreamBlockIntoMemory(blockStream, upOption, stOption);
      else
        return UploadBlockFile(blockStream, upOption, stOption, cacheType);
    }

    private bool HasSingleBlock(UploadOptions upOption) {
      bool b = (upOption.BlockIndex == 1 && upOption.TotalBlockCount == upOption.BlockIndex) || upOption.FullBlockUpload;
      return b;
    }

    private BlockCacheType GetBlockCacheType(UploadOptions upOption) {
      BlockCacheType cacheType;
      if ((upOption.BlockIndex == 1 && upOption.TotalBlockCount == upOption.BlockIndex) || upOption.FullBlockUpload)
        // If there is only single chunk to upload then directly save the document into the permenant location.
        return cacheType = BlockCacheType.InMemory;

      int fileSize = upOption.BlockSize * upOption.TotalBlockCount;
      if (fileSize < 1000000)
        cacheType = BlockCacheType.InMemory;
      else if (fileSize < 20000000)
        cacheType = BlockCacheType.Hybrid;
      else
        cacheType = BlockCacheType.OnDisk;

      return cacheType;
    }

    /// <summary>
    /// Store the block(chunk) of a file in a temp location. then call Merge method to Merge all block(chunk) of a file.
    /// </summary>
    /// <param name="upOption"></param>
    /// <param name="stOption"></param>  
    FileUploadResponseModel UploadBlockFile(Stream blockStream, UploadOptions upOption, StorageOptions stOption, BlockCacheType cacheType) {
      var tempFile = GetBlockTempFilePath(upOption, stOption);
      string blockFileFullPath = tempFile.BlockFileFullPath;
      // Save the file to temp location
      SaveFile(blockStream, blockFileFullPath);

      string[] blockFilesList = Directory.GetFiles(tempFile.BlockFileDirctoryPath);
      FileUploadResponseModel fileUploadResponseModel;
      // If we have received all the file chunks then proceed to merge them.
      if (IsLastBlock(upOption, blockFilesList))
        fileUploadResponseModel = MergeFile(blockFilesList, tempFile.BlockFileDirctoryPath, upOption, stOption);
      else
        fileUploadResponseModel = GenerateResponseModel(FileStorageStatus.InProcess, Guid.Empty, "", stOption.Filename);

      return fileUploadResponseModel;
    }

    private bool IsLastBlock(UploadOptions upOption, string[] tempFileBlockList) {
      return (tempFileBlockList.Length == upOption.TotalBlockCount);
    }

    private FileSaveInfo GetBlockTempFilePath(UploadOptions upOption, StorageOptions stOption) {
      // string fileName, Guid docRefId, int FileChunkIndex, int FileTotalChunk
      Guid tenantId = stOption.TenantId;
      string strUploadId = upOption.UploadId.ToString();
      // Save the file to temp location with tenant/docrefid directory
      //string tempBlockFilePath = Path.Combine(ConfigHelper.GetTempDocumentRootPath(), tenantId.ToString(), strUploadId);

      //string tempBlockFilePath = Path.Combine(_thumbnailSetting.TempDocumentRootPath, tenantId.ToString(), strUploadId);
      string tempBlockFilePath = Path.Combine(_thumbnailSetting.TempDocumentRootPath, tenantId.ToString(), strUploadId);


      //string tempBlockFilePath = Path.Combine("", tenantId.ToString(), strUploadId);


      // Create temp directory
      if(!Directory.Exists(tempBlockFilePath))
        Directory.CreateDirectory(tempBlockFilePath);

      FileInfo fileforext = new FileInfo(stOption.Filename);
      // Generating the temprory file name. 
      // For example DocumentReffd_FileChunkIndex.ext
      string tempBlocFilekName = string.Format("{0}_{1}{2}", strUploadId, upOption.BlockIndex, fileforext.Extension);
      // Generating the temprory file path for chunk files.
      string blockFileFullPath = Path.Combine(tempBlockFilePath, tempBlocFilekName);

      FileSaveInfo tempFileinfo = new FileSaveInfo();
      tempFileinfo.BlockFileFullPath = blockFileFullPath;
      tempFileinfo.BlockFileDirctoryPath = tempBlockFilePath;
      return tempFileinfo;
    }

    private FileSaveInfo GetStorageFilePath(StorageOptions stOption, Guid storageId) {
      string rootPath = stOption.GetStorageFileDirectoryPath(storageId);
      if (!Directory.Exists(rootPath))
        Directory.CreateDirectory(rootPath);

      FileSaveInfo fInfo = new FileSaveInfo();

      // Define the full path to save at actual location.      
      fInfo.BlockFileDirctoryPath = rootPath;
      fInfo.BlockFileFullPath = Path.Combine(rootPath, stOption.Filename);

      return fInfo;
    }

    // Hari: Just use the same tuple that I used. Why one more data structure?
    /// <summary>
    /// Using for returning temp file info.
    /// </summary>
    private class FileSaveInfo {
      public string BlockFileDirctoryPath;
      public string BlockFileFullPath;
    }


    #region Merge File

    /// <summary>    
    /// Objective = Loop through files in folder, look for all matching parts of file. If found, merge and return Response model.
    /// </summary>
    /// <param name="blocFilekPathList"></param>
    /// <param name="tempChunkDirecotryName">temp directory name where all block file resides.</param>
    /// <param name="upOption"></param>
    /// <param name="stOption"></param>  
    /// <returns></returns>
    FileUploadResponseModel MergeFile(string[] blocFilekPathList, string tempChunkDirecotryName, UploadOptions upOption, StorageOptions stOption) {  // Hari: MergeFiles(...); Discuss
      try {
        FileStorageStatus fileUploadStatus = FileStorageStatus.InProcess;
        Guid storageId = Guid.NewGuid();
        // Generating the file directory path, where file will permenent reside.
        // StorageId        

        // Define the full path to save at actual location.
        string baseFileName = GetStorageFilePath(stOption, storageId).BlockFileFullPath;
        string storageFilePath = baseFileName;
        // use a singleton to stop overlapping processes
        if (!MergeFileManager.Instance.InUse(baseFileName)) {
          MergeFileManager.Instance.AddFile(baseFileName);

          if (File.Exists(baseFileName))
            File.Delete(baseFileName);

          // Add each file located into a list so we can get them into 
          // the correct order for rebuilding the file
          var sortedFileBlockList = GetSortedFileBlock(blocFilekPathList, tempChunkDirecotryName, upOption.UploadId);

          // Hari: I think this goes in a separate method
          using (FileStream outputStream = new FileStream(baseFileName, FileMode.Create)) {
            // merge each file chunk back into one contiguous file stream
            foreach (var blockFile in sortedFileBlockList) {
              try {
                using (FileStream inputStream = new FileStream(blockFile.FileName, FileMode.Open)) {
                  inputStream.CopyTo(outputStream);
                }
              }

              catch (IOException) {
                // handle                                
              }
            }

            // Hari: Wrong style;  first calculateChecksum(FS), then compare;  --- Outside the loop?
            // After merging the file check file stream validity.

          }
          using (FileStream outputStream = new FileStream(baseFileName, FileMode.Open)) {
            if (!CompareChecksums(outputStream, upOption)) {
              // throw exception.
            }
          }

          fileUploadStatus = FileStorageStatus.Uploaded;
          // Unlock the file from singleton.
          // Hari: MergeFileManager???
          MergeFileManager.Instance.RemoveFile(baseFileName);
          // Deleteing generated temp directory
          Directory.Delete(tempChunkDirecotryName, true);
        }

        return GenerateResponseModel(fileUploadStatus, storageId, storageFilePath, stOption.Filename);
      }
      catch (Exception ex) {
        throw ex;
      }
    }

    private List<SortedFile> GetSortedFileBlock(string[] blocFilekPathList, string tempChunkDirecotryName, Guid uploadId) {
      try {
        // Add each file located into a list so we can get them into 
        // the correct order for rebuilding the file
        List<SortedFile> MergeList = new List<SortedFile>();
        string strUploadId = uploadId.ToString();
        int FileIndex = 0;
        SortedFile sFile;
        foreach (string File in blocFilekPathList) {
          sFile = new SortedFile();
          sFile.FileName = File;
          string f = File.Replace(tempChunkDirecotryName + "\\", "");
          // Finding the file order and setting the index for sorting                        
          int.TryParse(f.Substring(strUploadId.Length + 1, 1), out FileIndex);
          sFile.FileOrder = FileIndex;
          MergeList.Add(sFile);
        }
        // Sort by the file-part number to merge back in the correct order.
        MergeList.OrderBy(s => s.FileOrder);
        return MergeList;
      }
      catch (Exception ex) {
        throw ex;
      }
    }

    #endregion Merge File

    #region Save Document Directly

    /// <summary>
    /// Save the document into the permenant location and return the Storage information.
    /// </summary>
    /// <param name="fileStream"></param>
    /// <param name="stOption"></param>
    /// <param name="upOption"></param>
    /// <returns></returns>
    FileUploadResponseModel SaveSingleBlockFile(Stream fileStream, UploadOptions upOption, StorageOptions stOption) {
      // Check whether file is valid.
      if (!CompareChecksums(fileStream, upOption)) {
        // throw exception.
      }

      Guid storageId = stOption.useDocumentIdAsStrorageId && stOption.DocumentId != Guid.Empty ? stOption.DocumentId : Guid.NewGuid();
      string fileFullPath = GetStorageFilePath(stOption, storageId).BlockFileFullPath;
      SaveFile(fileStream, fileFullPath);

      return GenerateResponseModel(FileStorageStatus.Uploaded, storageId, fileFullPath, stOption.Filename);
    }

    #endregion Save Document Directly    

    #region In-Memory Save & Merge

    private static Dictionary<string, Dictionary<string, Stream>> inMemoryFile = new Dictionary<string, Dictionary<string, Stream>>();

    /// <summary>
    /// Adding file block in memory.
    /// </summary>
    /// <param name="blockStream"></param>
    /// <param name="uploadId">Unique id for all file blocks, It will be passed from client.</param>
    /// <param name="fileName"></param>
    /// <param name="FileBlockIndex"></param>
    public FileUploadResponseModel AddFileStreamBlockIntoMemory(Stream blockStream, UploadOptions upOption, StorageOptions stOption) {
      string fileName = stOption.Filename;
      FileInfo fileforext = new FileInfo(fileName);
      string strUploadId = upOption.UploadId.ToString();
      int FileBlockIndex = upOption.BlockIndex;
      // Generating the temprory file name. 
      // For example DocumentReffd_FileChunkIndex.ext
      string tempChunkFileName = string.Format("{0}_{1}{2}", strUploadId, FileBlockIndex, fileforext.Extension);
      Dictionary<string, Stream> fileDict;
      Stream fileData;

      fileData = blockStream;// ReadMemoryStream(blockStream);

      // If document reference already in memory dictionary then update dictionary.
      if (inMemoryFile.TryGetValue(strUploadId, out fileDict)) {
        /// If block doesn't exist then add it.
        if (!fileDict.ContainsKey(tempChunkFileName)) {
          fileDict.Add(tempChunkFileName, fileData);
          //inMemoryFile.Add(strUploadId, fileDict);
        }
      }
      else {
        fileDict = new Dictionary<string, Stream>();
        fileDict.Add(tempChunkFileName, fileData);
        inMemoryFile.Add(strUploadId, fileDict);
      }

      Dictionary<string, Stream> blockFileDict;
      if (inMemoryFile.TryGetValue(strUploadId, out blockFileDict) && IsMemoryStreamLastBlock(blockFileDict, upOption)) {
        // If we have received all the file block(chunks) then proceed to merge them.
        return MergeInMemoryFileStream(upOption, stOption, blockFileDict);
      }
      else {
        return GenerateResponseModel(FileStorageStatus.InProcess, Guid.Empty, "", stOption.Filename);
      }

    }

    private static byte[] ReadFully(Stream input) {
      byte[] buffer = new byte[16 * 1024];
      using (MemoryStream ms = new MemoryStream()) {
        int read;
        while ((read = input.Read(buffer, 0, buffer.Length)) > 0) {
          ms.Write(buffer, 0, read);
        }
        return ms.ToArray();
      }
    }

    private static MemoryStream ReadMemoryStream(Stream input) {
      byte[] buffer = new byte[16 * 1024];
      using (MemoryStream ms = new MemoryStream()) {
        int read;
        while ((read = input.Read(buffer, 0, buffer.Length)) > 0) {
          ms.Write(buffer, 0, read);
        }
        return ms;
      }
    }

    /// <summary>
    ///  If we have received all the file block(chunks) then return true.
    /// </summary>
    /// <param name="blockFileDict"></param>
    /// <param name="upOption"></param>
    /// <returns></returns>
    private bool IsMemoryStreamLastBlock(Dictionary<string, Stream> blockFileDict, UploadOptions upOption) {
      return blockFileDict != null && blockFileDict.Count == upOption.TotalBlockCount;
    }

    /// <summary>    
    /// Objective = Loop through all stream reside in in-memory, look for all matching parts of file. If found, merge and return Response model.
    /// </summary>
    /// <param name="FileName"></param>
    /// <returns></returns>
    public FileUploadResponseModel MergeInMemoryFileStream(UploadOptions upOption, StorageOptions stOption, Dictionary<string, Stream> blockFileDict) {
      try {
        FileStorageStatus fileUploadStatus = FileStorageStatus.InProcess;
        string strUploadId = upOption.UploadId.ToString();

        // If we have received all the file block(chunks) then proceed to merge them.             
        Guid storageId = Guid.NewGuid();
        // Generating the file directory path, where file will permenent reside.
        // DocumentRootPath/TenantId/StorageId
        string rootPath = stOption.GetStorageFileDirectoryPath(storageId);
        if (!Directory.Exists(rootPath)) {
          Directory.CreateDirectory(rootPath);
        }

        // Define the full path to save at actual location.
        string baseFileName = Path.Combine(rootPath, stOption.Filename);
        string storageFilePath = baseFileName;
        MergeFileManager.Instance.AddFile(baseFileName);
        if (File.Exists(baseFileName))
          File.Delete(baseFileName);

        // Add each file located into a list so we can get them into 
        // the correct order for rebuilding the file                           
        var MergeOrder = GetSortedFile(blockFileDict, stOption.Filename, strUploadId);
        using (Stream FS = new FileStream(baseFileName, FileMode.Create)) {
          // merge each file chunk back into one contiguous file stream
          foreach (var chunk in MergeOrder) {
            try {
              /*
              byte[] buffer = new byte[1024];
              using (Stream fileChunk = blockFileDict[chunk.FileName])
              {
                  fileChunk.Position = 0;
                  int readBytes = 0;
                  while ((readBytes = fileChunk.Read(buffer, 0, buffer.Length)) > 0)
                  {
                      FS.Write(buffer, 0, readBytes);
                  }
                  fileChunk.Flush();
              }*/
              using (Stream inputStream = blockFileDict[chunk.FileName]) {
                inputStream.Position = 0;
                inputStream.CopyTo(FS);
              }
              //FS.Write(inputByte, 0, inputByte.Length);
            }
            catch (IOException) {
              // handle                                
            }
          }
          // After merging the file check file stream validity.

        }
        using (FileStream outputStream = new FileStream(baseFileName, FileMode.Open)) {
          if (!CompareChecksums(outputStream, upOption)) {
            // throw exception.
          }
        }
        fileUploadStatus = FileStorageStatus.Uploaded;
        // Unlock the file from singleton.
        MergeFileManager.Instance.RemoveFile(baseFileName);
        // Removing entry from in-Memory dictionary.
        inMemoryFile.Remove(strUploadId);



        return GenerateResponseModel(fileUploadStatus, storageId, storageFilePath, stOption.Filename);
      }
      catch (Exception ex) {
        throw ex;
      }
    }

    private List<SortedFile> GetSortedFile(Dictionary<string, Stream> fileDict, string fileName, string strUploadId) {

      List<SortedFile> mergeList = new List<SortedFile>();
      int FileIndex = 0;
      FileInfo fileforext = new FileInfo(fileName);
      foreach (string File in fileDict.Keys) {
        SortedFile sFile = new SortedFile();
        sFile.FileName = File;
        // Generate uniquename was docRefId_index.ext
        string f = File.Replace(strUploadId + "_", "");
        f = File.Replace(fileforext.Extension, "");
        // Finding the file order and setting the index for sorting                        
        int.TryParse(f, out FileIndex);
        sFile.FileOrder = FileIndex;
        mergeList.Add(sFile);
      }
      // Sort by the file-part number to merge back in the correct order.
      mergeList.OrderBy(s => s.FileOrder);

      return mergeList;
    }


    #endregion In-Memory Save & Merge

    #region Support

    /// <summary>
    /// return true if file is valid.
    /// </summary>
    /// <param name="blockStream"></param>
    /// <param name="upOption"></param>
    /// <returns></returns>
    private bool CompareChecksums(Stream fileStream, UploadOptions upOption) {

      if (upOption.ChecksumAlgorithm == CheckSumAlgorithm.MD5) {
        using (var md5 = MD5.Create()) {

          using (FileStream fs = new FileStream(@"C:\Users\pwagrawal\Desktop\UPLOAD-FILE-MBS\People-10mb.docx", FileMode.Open)) {
            upOption.FileChecksum = System.Text.Encoding.Default.GetString(md5.ComputeHash(fs));
          }

          if (System.Text.Encoding.Default.GetString(md5.ComputeHash(fileStream)) == upOption.FileChecksum)
            return true;
          else
            return false;
        }
      }


      return true;
    }

    /// <summary>
    /// Generate the response model and return it.
    /// </summary>
    /// <param name="fileUploadStatus"></param>
    /// <param name="storageId"></param>
    /// <param name="filePath"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    FileUploadResponseModel GenerateResponseModel(FileStorageStatus fileUploadStatus, Guid storageId, string filePath, string fileName) {
      FileUploadResponseModel responseModel = new FileUploadResponseModel();
      responseModel.StorageId = storageId;
      responseModel.StorageFilePath = filePath;
      responseModel.UploadStorageStatus = fileUploadStatus;
      responseModel.UploadFileName = fileName;

      return responseModel;
    }

    /// <summary>
    /// Save the file into the the fileFullPath location.
    /// </summary>
    /// <param name="fileStream"></param>
    /// <param name="fileFullPath"></param>
    void SaveFile(Stream fileStream, string fileFullPath) {
      byte[] buffer = new byte[1024];
      using (System.IO.FileStream output = new FileStream(fileFullPath, FileMode.Create)) {
        int readBytes = 0;
        while ((readBytes = fileStream.Read(buffer, 0, buffer.Length)) > 0) {
          output.Write(buffer, 0, readBytes);
        }
      }
    }

    void WriteStreamToFile(Stream inputfileStream, Stream output) {
      byte[] buffer = new byte[1024];

      int readBytes = 0;
      while ((readBytes = inputfileStream.Read(buffer, 0, buffer.Length)) > 0) {
        output.Write(buffer, 0, readBytes);
      }
    }

    #endregion Support   

  }
}
/*
 * 
    /// <summary>    
    /// Objective = Loop through files in folder, look for all matching parts of file. If found, merge and return Response model.
    /// </summary>
    /// <param name="FileName"></param>
    /// <returns></returns>
    FileResponseModel MergeFile(Guid Tenantid, string FileName, string tempChunkDirecotryName, string docRefIdd, int FileCount)
    {
      try
      {
        bool rslt = false;
        int FileIndex = 0;
        Guid storageId = Guid.Empty;
        string storageFilePath = "";
        string[] FilesList = Directory.GetFiles(tempChunkDirecotryName);
        // If we have received all the file chunks then proceed to merge them.
        if (FilesList.Length == FileCount)
        {
          

string rootPath;
storageId = Guid.NewGuid();
          // Generating the file directory path, where file will permenent reside.
          // DocumentRootPath/TenantId/StorageId
          rootPath = GetStorageFilePath(Tenantid, storageId); //Path.Combine(rootPath, Tenantid.ToString(), storageId.ToString());
          if (!Directory.Exists(rootPath))
          {
            Directory.CreateDirectory(rootPath);
          }

          // Define the full path to save at actual location.
          string baseFileName = Path.Combine(rootPath, FileName);
storageFilePath = baseFileName;
          // use a singleton to stop overlapping processes
          if (!MergeFileManager.Instance.InUse(baseFileName))
          {
            MergeFileManager.Instance.AddFile(baseFileName);
            if (File.Exists(baseFileName))
              File.Delete(baseFileName);
            // Add each file located into a list so we can get them into 
            // the correct order for rebuilding the file
            List<SortedFile> MergeList = new List<SortedFile>();
            foreach (string File in FilesList)
            {
              SortedFile sFile = new SortedFile();
sFile.FileName = File;
              string f = File.Replace(tempChunkDirecotryName + "\\", "");
// Finding the file order and setting the index for sorting                        
int.TryParse(f.Substring(docRefIdd.Length + 1, 1), out FileIndex);
              sFile.FileOrder = FileIndex;
              MergeList.Add(sFile);
            }
            // Sort by the file-part number to merge back in the correct order.
            MergeList.OrderBy(s => s.FileOrder);
            var MergeOrder = MergeList;
            using (FileStream FS = new FileStream(baseFileName, FileMode.Create))
            {
              // merge each file chunk back into one contiguous file stream
              foreach (var chunk in MergeOrder)
              {
                try
                {
                  using (FileStream fileChunk = new FileStream(chunk.FileName, FileMode.Open))
                  {
                    fileChunk.CopyTo(FS);
                  }
                }
                catch (IOException)
                {
                  // handle                                
                }
              }
            }
            rslt = true;
            // Unlock the file from singleton.
            MergeFileManager.Instance.RemoveFile(baseFileName);
            // Deleteing generated temp directory
            Directory.Delete(tempChunkDirecotryName, true);
          }
        }
        return new FileResponseModel()
{
  StatusCode = 1,
          // It is for temprory purpose, If all content merge and rslt is true then return a of successfuly all content loaded. 
          Message = rslt ? "Successfully Loaded" : "Uploading...",
          StorageId = storageId,
          storageFilePath = storageFilePath,
          StorageStatus = rslt ? FileStorageStatus.Uploaded : FileStorageStatus.InProcess,
          DocumentFileName = FileName
        };
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
 
     internal static StorageResponseModel UploadDocumentInChunk(HttpRequest httpRequest)
    {
      string filename = string.Empty;
      string filePath = string.Empty;
      string fileFullPath = string.Empty;
      IList<EwpErrorData> errorDataList = new List<EwpErrorData>();
      if (httpRequest.Files.Count <= 0)
      {
        EwpErrorData errorData = new EwpErrorData()
        {
          ErrorSubType = (int)ValidationErrorSubType.InvalidFieldValue,
          Data = "File",
          Message = string.Format(ServerMessages.FileNotFound)
        };
        errorDataList.Add(errorData);
        throw new EwpValidationException(ServerMessages.FileNotFound, errorDataList);
      }

      string FileChunkIndex = httpRequest.Headers["FileChunkIndex"];

      if (string.IsNullOrEmpty(FileChunkIndex))
      {
        EwpErrorData errorData = new EwpErrorData()
        {
          ErrorSubType = (int)ValidationErrorSubType.InvalidFieldValue,
          Data = "File",
          Message = string.Format("Incomplete request")
        };
        errorDataList.Add(errorData);
        throw new EwpValidationException("Incomplete request", errorDataList);
      }

      string FileTotalChunk = httpRequest.Headers["FileTotalChunk"];

      if (string.IsNullOrEmpty(FileChunkIndex))
      {
        EwpErrorData errorData = new EwpErrorData()
        {
          ErrorSubType = (int)ValidationErrorSubType.InvalidFieldValue,
          Data = "File",
          Message = string.Format("Incomplete request")
        };
        errorDataList.Add(errorData);
        throw new EwpValidationException("Incomplete request", errorDataList);
      }

      string DocumentRefId = httpRequest.Headers["DocumentRefId"];
      Guid refId = string.IsNullOrEmpty(DocumentRefId) ? Guid.Empty : new Guid(DocumentRefId);
      if (string.IsNullOrEmpty(DocumentRefId) || refId == Guid.Empty)
      {
        EwpErrorData errorData = new EwpErrorData()
        {
          ErrorSubType = (int)ValidationErrorSubType.InvalidFieldValue,
          Data = "File",
          Message = string.Format("Incomplete request")
        };
        errorDataList.Add(errorData);
        throw new EwpValidationException("Incomplete request", errorDataList);
      }

      foreach (string file in httpRequest.Files)
      {

        return UploadFileChunk(httpRequest.Files[file], refId, Convert.ToInt32(FileChunkIndex), Convert.ToInt32(FileTotalChunk));
      }

      return new StorageResponseModel()
      {
        StatusCode = 1,
        // It is for temprory purpose, If all content merge and rslt is true then return a of successfuly all content loaded. 
        Message = "File Uploaded"
      };
    }


/// <summary>
    /// Method is responsible to store the chunk of file in a temp location. then call Merge method to Merge all chunk of files.
    /// </summary>
    /// <param name="httpRequest">Its current request which is processing.</param>
    /// <param name="postedFile">Current processing file.</param>
    /// <param name="tenantId">Login user tenantid</param>
    /// <param name="docRefId">Client will paas this id to create folder in temp directory. Its batter to work inside the current process doc id. it will give some performance benifit to process current request.</param>
    static StorageResponseModel UploadFileChunk(HttpPostedFile postedFile, Guid docRefId, int FileChunkIndex, int FileTotalChunk)
    {
      Guid tenantId = EwAppSessionManager.GetSession().TenantId; //new Guid("C8535247-27AE-42E7-94F9-29241F444426"); //EwAppSessionManager.GetSession().TenantId;      
      string strDocuRefId = docRefId.ToString();
      // Save the file to temp location with tenant/docrefid directory
      string filePath = Path.Combine(ConfigHelper.GetTempDocumentRootPath(), tenantId.ToString(), docRefId.ToString());

      // Create temp directory
      if (!Directory.Exists(filePath))
        Directory.CreateDirectory(filePath);

      FileInfo fileforext = new FileInfo(postedFile.FileName);
      // Generating the temprory file name. For example DocumentReffd_FileChunkIndex.ext
      string tempChunkFileName = string.Format("{0}_{1}{2}", strDocuRefId, FileChunkIndex, fileforext.Extension);
      // Generating the temprory file path for chunk files.
      string fileFullPath = Path.Combine(filePath, tempChunkFileName);

      // Save the file to temp location
      postedFile.SaveAs(fileFullPath);
      // Calling method to merge files.
      return new StorageUtils().MergeFile(tenantId, postedFile.FileName, filePath, strDocuRefId, FileTotalChunk);
    }
 */
