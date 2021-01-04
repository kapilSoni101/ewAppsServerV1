using System;

namespace ewApps.Core.DMService {

  public enum CheckSumAlgorithm {
    None, MD5
  }

  public class UploadOptions {
    /// <summary>
    /// Base class options used in uploading
    /// Each provider will have its derived class.
    /// Note that the options set here are not binding. They may be ignored by the provider. 
    /// </summary>
    /// 

    /// <summary>
    /// Timeout for file uploading. 0 means no timeout
    /// </summary>
    public int Timeout = 0;  // In millisecs

    #region Block(Chunk) Upload

    /// <summary>
    /// Upload file block is complete file.
    /// </summary>
    public bool FullBlockUpload = false;

    /// <summary>
    /// Block size of file
    /// </summary>
    public int BlockSize = 1024 * 8;

    /// <summary>
    /// Total block of a file to received.
    /// </summary>
    public int TotalBlockCount = 0;

    /// <summary>
    /// Block Index of a  of a file.
    /// </summary>
    public int BlockIndex = 0;

    /// <summary>
    /// Its unqiueid for a file. All block(chunks) of a file will pass UploadId with each block when uploading a file.
    /// </summary>
    public Guid UploadId = Guid.Empty;

    public string FileChecksum = null;  // Same for all blocks

    public CheckSumAlgorithm ChecksumAlgorithm = CheckSumAlgorithm.None;

    #endregion Block(Chunk) Upload

    /// <summary>
    /// To add custom properties for UploadOption.
    /// </summary>
    public System.Collections.Generic.Dictionary<string, object> CustomData = new System.Collections.Generic.Dictionary<string, object>();

  }
}
