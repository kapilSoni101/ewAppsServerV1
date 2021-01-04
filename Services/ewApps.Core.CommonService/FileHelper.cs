/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul badgujar <abadgujar@batchmaster.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Imaging;
using System.Reflection;
using System.Linq;

namespace ewApps.Core.CommonService
{


  /// <summary>
  /// It provides abstract methods for File I/O operations.
  /// </summary>
  public static class FileHelper
  {

    #region public methods 
    /// <summary>
    /// Get stream from physical location of a file.
    /// </summary>
    /// <remarks>
    /// Always require full file path. Ex. "C:\Temp\abc.doc".
    /// </remarks>
    /// <param name="filePath">Source file full path.</param>
    /// <returns>File stream object.</returns>
    public static FileStream GetFileStreamFromFilePath(string filePath) {
      if (!File.Exists(filePath))
        throw new FileNotFoundException(string.Format("File does not exists at '{0}' this location.", filePath));
      FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
      return fileStream;
    }

    /// <summary>
    /// Get bytes from file path.
    /// </summary>
    /// <param name="filePath">Source file full path.</param>
    /// <returns>Bytes array.</returns>
    public static Byte[] GetBytesFromFilePath(string filePath) {
      if (!File.Exists(filePath))
        throw new FileNotFoundException(string.Format("File does not exists at '{0}' this location.", filePath));
      byte[] byteArray = System.IO.File.ReadAllBytes(filePath);
      return byteArray;
    }

    /// <summary>
    /// Save file stream object on given location.
    /// </summary>
    /// <remarks>
    /// If given file path does not exists then it creates a new directory for a given location.
    /// </remarks>
    /// <param name="stream">Source file stream object.</param>
    /// <param name="fileFullPath">Target file path.</param>   
    public static void SaveStreamToFile(Stream stream, string fileFullPath) {
      // If target directory does not exists.
      CreateDirectory(fileFullPath);

      byte[] buffer = new byte[16345];
      using (FileStream fs = new FileStream(fileFullPath,
                          FileMode.Create, FileAccess.Write, FileShare.None)) {
        int read;
        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0) {
          fs.Write(buffer, 0, read);
        }
      }
    }


    /// <summary>
    /// Save bytes array into file on a given location.
    /// </summary>
    /// <remarks>
    /// If given file path does not exists then it creates a new directory for a given location.
    /// </remarks>
    /// <param name="fileFullPath">Target file path.</param>
    /// <param name="bytes">bytes array of file.</param>
    public static void SaveBytesToFile(Byte[] bytes, string fileFullPath) {
      // If bytes array is null or length is zero.
      //if (bytes == null || bytes.Length == 0) throw new FileNotFoundException("The attached file does not exist at '{0}' this location.");

      // If target directory not exists.
      CreateDirectory(fileFullPath);

      // Save bytes into file.
      using (FileStream fs = new FileStream(fileFullPath, FileMode.CreateNew)) {
        fs.Write(bytes, 0, bytes.Length);
      }
    }

    /// <summary>
    /// Deletes file from a given location.
    /// </summary>
    /// <param name="fileFullPath">File full path.</param>
    public static void DeleteFile(string fileFullPath) {
      if (File.Exists(fileFullPath)) { //throw new FileNotFoundException(string.Format("The attached file does not exist at '{0}' this location.", fileFullPath));                
        File.Delete(fileFullPath);
      }
    }

    /// <summary>
    /// Deletes the directory.
    /// </summary>
    /// <param name="directoryPath">The directory path.</param>
    public static void DeleteDirectory(string directoryPath) {

      if (Directory.Exists(directoryPath)) { //throw new FileNotFoundException(string.Format("The attached file does not exist at '{0}' this location.", fileFullPath));                
        Directory.Delete(directoryPath);
      }

    }

    /// <summary>
    /// Gets the file extention.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns></returns>
    public static string GetFileExtention(string fileName) {
      string ext = Path.GetExtension(fileName);
      ext = !string.IsNullOrEmpty(ext) ? ext.Substring(1) : ext;
      return ext;
    }

    /// <summary>
    /// Get file name without extention.
    /// </summary>
    /// <param name="fileName">File Name.</param>
    /// <returns>File name without extention. Ex. if file name is test.doc then output is test.</returns>
    public static string GetFileNameWithoutExt(string fileName) {
      return Path.GetFileNameWithoutExtension(fileName);
    }

    /// <summary>
    /// Get size of directory in GB.
    /// </summary>
    /// <param name="directory">Directory path.</param>
    /// <returns>Total size of files exits in directory and subdirectroy</returns>
    public static double GetDirectorySizeInGB(string directory) {
      double size = GetDirectorySizeInBytes(directory);
      return size / (1024 * 1024 * 1024);
    }

    /// <summary>
    /// Get size of directory in MB.
    /// </summary>
    /// <param name="directory">Directory path.</param>
    /// <returns>Total size of files exits in directory and subdirectroy</returns>
    public static double GetDirectorySizeInMB(string directory) {
      double size = GetDirectorySizeInBytes(directory);
      return size / (1024 * 1024);
    }

    /// <summary>
    /// Get size of directory in Bytes.
    /// </summary>
    /// <param name="directory">Directory path.</param>
    /// <returns>Total size of files exits in directory and subdirectroy</returns>
    public static long GetDirectorySizeInBytes(string directory) {
      long size = 0;

      if (!Directory.Exists(directory)) return size;

      foreach (string dir in Directory.GetDirectories(directory)) {
        size += GetDirectorySizeInBytes(dir);
      }

      foreach (FileInfo file in new DirectoryInfo(directory).GetFiles()) {
        size += file.Length;
      }
      return size;
    }

    /// <summary>
    /// Get bytes array from string .
    /// </summary>
    /// <param name="text">Input string.</param>
    /// <returns>Bytes array.</returns>  
    public static byte[] GetBytes(string text) {
      return ASCIIEncoding.UTF8.GetBytes(text);
    }

    /// <summary>
    /// Create directory of given path.
    /// </summary>
    /// <param name="path">Directory full path.</param>
    public static void CreateDirectory(string path) {
      // If target directory not exists.
      string targetDir = System.IO.Path.GetDirectoryName(path);
      if (!System.IO.Directory.Exists(targetDir))
        Directory.CreateDirectory(targetDir);
    }


    /// <summary>
    /// Convert byte to kb.It always round decimal value. 
    /// </summary>
    /// <param name="byes">Size in byes.</param>
    /// <returns>Size in KB</returns>
    public static int GetByteSizeInKB(long byes) {
      double kbSize = (double)byes / 1024;
      if (kbSize <= .5)
        return (int)Math.Ceiling(kbSize);
      else
        return (int)Math.Round((double)byes / 1024, MidpointRounding.AwayFromZero);
    }

    /// <summary>
    /// Convert byte to kb.It always round decimal value. 
    /// </summary>
    /// <param name="byes">Size in mb.</param>
    /// <returns>Size in MB</returns>
    public static int GetByteSizeInMB(long byes) {
      return (int)Math.Round((double)byes / (1024 * 1024), MidpointRounding.AwayFromZero);
    }

    /// <summary>
    /// Moves a specified file to a new location.
    /// </summary>
    /// <param name="sourceFile"> The name of the file to move.</param>
    /// <param name="targetFile">The new path for the file.</param>
    /// <remarks>If target directory doesn't exist create target directory.</remarks>
    public static void MoveFile(string sourceFile, string targetFile) {
      string targetDirectory = Path.GetDirectoryName(targetFile);
      // Check that target directory exists.
      if (Directory.Exists(targetDirectory) == false)
        Directory.CreateDirectory(targetDirectory);
      if (File.Exists(targetFile))
        File.Delete(targetFile);
      File.Move(sourceFile, targetFile);
    }

    /// <summary>
    ///  Converts the bitmap to Base64 string.
    /// </summary>
    /// <param name="image">The image that convert to string.</param>
    /// <param name="format">The image format that conver to string.</param>
    /// <returns>Returns converted Base64 string.</returns>
    public static string GetImageBase64(System.Drawing.Image image, ImageFormat format) {

      using (MemoryStream ms = new MemoryStream()) {
        // Convert Image to byte[]
        image.Save(ms, format);
        byte[] imageBytes = ms.ToArray();

        // Convert byte[] to Base64 String
        string base64String = Convert.ToBase64String(imageBytes);
        image.Dispose();
        return base64String;
      }

    }

    /// <summary>
    /// Converts base64 to image.
    /// </summary>
    /// <param name="base64String">The base64 string that convert to Image.</param>
    /// <returns>Returns converted Image.</returns>
    public static System.Drawing.Image GetImage(string base64String) {
      // Convert Base64 String to byte[]
      byte[] imageBytes = Convert.FromBase64String(base64String);
      MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

      // Convert byte[] to Image
      ms.Write(imageBytes, 0, imageBytes.Length);
      System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
      return image;
    }

    /// <summary>
    /// Gets the image format from file extension.
    /// </summary>
    /// <param name="extension">The file extension to get ImageFormat.</param>
    /// <returns>Returns ImageFormat enum if given file extension matched otherwise return null.</returns>
    public static ImageFormat GetImageFormat(string extension) {
      ImageFormat result = null;
      PropertyInfo prop = typeof(ImageFormat).GetProperties().Where(p => p.Name.Equals(extension, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
      if (prop != null) {
        result = prop.GetValue(prop, null) as ImageFormat;
      }
      return result;
    }

    /// <summary>
    /// Gets the MIME type from raw format.
    /// </summary>
    /// <param name="rawFormatId">The raw format identifier.</param>
    /// <returns>
    /// Returns Image mime type from given image object.
    /// </returns>
    public static string GetImageMIMEType(Guid rawFormatId) {
      var imgguid = rawFormatId;
      foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders()) {
        if (codec.FormatID == imgguid)
          return codec.MimeType;
      }
      return "image/unknown";

    }

    /// <summary>
    /// Get image extension from mime type
    /// </summary>
    /// <param name="mimeType">Image mime type</param>
    /// <returns>Image extension</returns>
    public static string GetDefaultExtension(string mimeType) {
      string result;
      RegistryKey key;
      object value;
      key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + mimeType, false);
      value = key != null ? key.GetValue("Extension", null) : null;
      result = value != null ? value.ToString() : string.Empty;
      return result;
    }

    /// <summary>
    /// Gets the base64 file size in kb.
    /// </summary>
    /// <param name="base64String">The base64 string.</param>
    /// <returns></returns>
    public static double GetBase64FileSizeInKB(string base64String) {
      return Math.Ceiling((double)(base64String.Length * 4) / 3) / 1000;
      //return (base64String.Length * 3 / 4) / 1000;
    }

    /// <summary>
    /// Compresses the specified directory selected.
    /// </summary>
    /// <param name="directorySelected">The directory selected.</param>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    public static string CompressFile(DirectoryInfo directorySelected, string filePath) {
      string fileName = string.Empty;
      foreach (FileInfo fileToCompress in directorySelected.GetFiles()) {
        using (FileStream originalFileStream = fileToCompress.OpenRead()) {
          if ((File.GetAttributes(fileToCompress.FullName) &
             FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz") {
            using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".gz")) {
              using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                 CompressionMode.Compress)) {
                originalFileStream.CopyTo(compressionStream);
                fileName = fileToCompress.Name + ".gz";
              }
            }
          }

        }
      }
      return fileName;
    }

    public static string ReadFileAsText(string filePath) {
      string text;
      using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
        using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8)) {
          text = streamReader.ReadToEnd();
        }
      }
      return text;
    }

    /// <summary>
    /// Make string from stream object.
    /// </summary>
    /// <param name="s">Stream object.</param>
    /// <returns>String.</returns>
    public static string StreamToString(this System.IO.Stream s) {
      StringBuilder sb = new StringBuilder();

      int streamLength = Convert.ToInt32(s.Length);
      Byte[] streamArray = new Byte[streamLength];


      for (int i = 0; i < streamLength; i++) {
        sb.Append(Convert.ToChar(streamArray[i]));
      }

      return sb.ToString();
    }

    #endregion
  }
}

