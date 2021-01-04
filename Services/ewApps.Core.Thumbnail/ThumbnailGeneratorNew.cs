using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using ewApps.Core.ThumbnailService;

namespace ewApps.Core.CommonService {
  #region Public Methods
  /// <summary>
  /// This classs impliments the methods to generate thumbanil. It reduce the size of file.
  /// It generate thumbanil for video,images and documents. 
  /// The generated thumbanil dimension wil be 100*100.
  /// The output file extension will be PNG
  /// </summary>
  public class ThumbnailGeneratorNew
  {
    #region Local Members

    private static int _width = 150;
    private static int _height = 150;
    private static string base64StringThumb = string.Empty;

    #endregion Local Members

    private static string[] docArr = { ".DOCX", ".DOC", ".DOCM", ".DOT", ".DOTX", ".DOTM", ".DOCB" };
    private static string[] pdfArr = { ".PDF", ".ACROBAT" };
    private static string[] audioArr = { ".AA", ".AAC", ".AAX", ".ACT", ".AIFF", ".AMR", ".APE", ".AU", ".AWB", ".DCT", ".DSS", ".DVF", ".FLAC", ".GSM", ".IKLAX", ".IVS", ".M4A", ".M4B", ".MMF", ".MP3", ".MP2", ".MSV", ".WAV", ".WMA", ".MPC", ".MV", ".OGG" };
    private static string[] vidArr = { ".3GP", ".AVI", ".MOV", ".MP4", ".MPEG", ".RP", ".RM", ".WEBM", ".MKV", ".VOB", ".OGV", ".WMV", ".MP2", ".SVI", ".DRC", ".YUV", ".MPE", ".MSF", ".MNG", ".ASF", ".MPV", ".NSV", ".QT", ".M4P", ".M4V", ".M2V", ".MPG", ".ROQ" };
    private static string[] zipArr = { ".ISO", ".LBR", ".MAR", ".TAR", ".ZIP", ".7Z", ".GZ", ".CAB", ".DMG", ".JAR", ".PAK", ".RAR", ".ZIPX" };
    private static string[] excelArr = { ".XLSX", ".XLS", ".XLSM", ".XLSB", ".XLTM", ".XLAM", ".XLA", ".XLB", ".XLC", ".CSV" };
    private static string[] pptArr = { ".PPT", ".POT", ".PPS", ".PPTX", ".PPTM", ".POTX", ".POTM", ".PPAM", ".PPSX", ".SLDX", ".SLDM" };
    private static string[] imgArr = { ".JPG", ".JPEG", ".GIF", ".PNG", ".BMP", ".ICO" };



    /// <summary>
    /// Gets the thumbnail base64.
    /// </summary>
    /// <param name="filePath">The base64 string.</param>
    /// <param name="fileName">Name of the file.</param>
    /// <returns></returns>

    public static string GenerateThumbnail(string filePath, string fileName, out int width, out int height)
    {



      FileInfo fileInfo = new FileInfo(fileName);
      string fileFullPath = Path.Combine(filePath, fileName);

      string defaultFilePath = AppSettingHelper.GetDefaultImagePath();
      string defaultFileFullPath = string.Empty;

      // Validate file exits on given path
      //if (!File.Exists(fileFullPath))
      //{
      //  IList<EwpErrorData> errorDataList = new List<EwpErrorData>();
      //  EwpErrorData errorData = new EwpErrorData()
      //  {
      //    ErrorSubType = (int)ValidationErrorSubType.FieldRequired,
      //    Data = "File",
      //    Message = string.Format(ServerMessages.FileNotFound)
      //  };
      //  errorDataList.Add(errorData);
      //  throw new EwpValidationException(ServerMessages.FileNotFound, errorDataList);
      //}


      // ================================Audio File===========================
      // Check for audio files  
      if (audioArr.Any(x => x == fileInfo.Extension.ToUpper()))
      {

        defaultFileFullPath = Path.Combine(defaultFilePath, "audio.png");
        base64StringThumb = GetBase64FromFilePath(defaultFileFullPath);

      }

      // ================================Doc File===========================
      // Check for pdf files
      else if (pdfArr.Any(x => x == fileInfo.Extension.ToUpper()))
      {

        defaultFileFullPath = Path.Combine(defaultFilePath, "pdf.png");
        base64StringThumb = GetBase64FromFilePath(defaultFileFullPath);

      }

      // Check for doc files
      else if (docArr.Any(x => x == fileInfo.Extension.ToUpper()))
      {

        defaultFileFullPath = Path.Combine(defaultFilePath, "doc.png");
        base64StringThumb = GetBase64FromFilePath(defaultFileFullPath);
      }

      // Check for Zip files
      else if (zipArr.Any(x => x == fileInfo.Extension.ToUpper()))
      {

        defaultFileFullPath = Path.Combine(defaultFilePath, "zip.png");
        base64StringThumb = GetBase64FromFilePath(defaultFileFullPath);
      }

      // Check for Excel files
      else if (excelArr.Any(x => x == fileInfo.Extension.ToUpper()))
      {

        defaultFileFullPath = Path.Combine(defaultFilePath, "xls.png");
        base64StringThumb = GetBase64FromFilePath(defaultFileFullPath);
      }

      // Check for ppt files
      else if (pptArr.Any(x => x == fileInfo.Extension.ToUpper()))
      {

        defaultFileFullPath = Path.Combine(defaultFilePath, "ppt.png");
        base64StringThumb = GetBase64FromFilePath(defaultFileFullPath);
      }


      // ================================Video File===========================
      else if (vidArr.Any(x => x == fileInfo.Extension.ToUpper()))
      {
        // Get base64 file from video file
        base64StringThumb = GenerateThumbnailForVideo(fileFullPath);
      }


      // ================================Image File===========================
      else if (imgArr.Any(x => x == fileInfo.Extension.ToUpper()))
      {
        //defaultFileFullPath = Path.Combine(defaultFilePath, "image.jpg");
        base64StringThumb = GenerateThumbnailForImage(fileFullPath);

      }

      // ================================Unknown File===========================
      else
      {
        //Get None Thumbnail for all remaining extensions 
        defaultFileFullPath = Path.Combine(defaultFilePath, "none.png");
        base64StringThumb = GetBase64FromFilePath(defaultFileFullPath);
      }

      width = _width;
      height = _height;
      return base64StringThumb;

    }


    /// <summary>
    /// Genereates the thumbnail for Image file.
    /// </summary>
    /// <param name="fileFullPath">The file.</param>
    /// <returns>Thumbnail file of image</returns>

    public static string GenerateThumbnailForImage(string fileFullPath)
    {
      string tempPath = Path.Combine(AppSettingHelper.GetTempDocumentRootPath(), Guid.NewGuid().ToString() + ".jpg");
      base64StringThumb = MagicScaleThumbnail.ImageGenrator(fileFullPath, tempPath);
      // Delete temp file
      return base64StringThumb;
    }
    //  Guid.NewGuid().ToString() +

    /// <summary>
    /// Genereates the thumbnailfrom Image file.
    /// </summary>
    /// <param name="fileFullPath"></param>
    /// <param name="actualWidth"></param>
    /// <param name="actualHeight"></param>
    /// <returns></returns>
    public static string GenerateThumbnailForVideo(string fileFullPath, int actualWidth = 0, int actualHeight = 0)
    {
      Bitmap bitmap = null;

      //  Get thumbnail bitmap for Video or Image
      //  check if actual height and width provided by client 
      //  the we'll store the same else calculate the size
      if (actualHeight == 0)
      {
        GetVideoHeightAndWidth(fileFullPath, out actualWidth, out actualHeight);
      }
      bitmap = WindowsThumbnail.GetThumbnailBitmap(fileFullPath, actualWidth, actualHeight);
      base64StringThumb = GetBase64StringFromBitmap(bitmap);// GetBase64FromBitmap(bitmap);
      bitmap.Dispose();
      return base64StringThumb;
    }


    /// <summary>
    /// Get video file height and width according to frame.
    /// </summary>
    /// <param name="fileFullPath">file full path.</param>
    /// <param name="width">return video width </param>
    /// <param name="height">return video height</param>
    public static void GetVideoHeightAndWidth(string fileFullPath, out int width, out int height)
    {
      NReco.VideoInfo.FFProbe ffProbe = new NReco.VideoInfo.FFProbe();
      NReco.VideoInfo.MediaInfo videoInfo = ffProbe.GetMediaInfo(fileFullPath);
      NReco.VideoInfo.MediaInfo.StreamInfo streamInfo = videoInfo.Streams.Where(p => p.CodecType.ToLower() == "video").FirstOrDefault();
      width = streamInfo.Width;
      height = streamInfo.Height;
    }

    /// <summary>
    /// Genereates the Base64 for Constant file.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns>Base64 for Constat file</returns>

    public static string GetBase64FromFilePath(string filePath)
    {
      byte[] bytes = System.IO.File.ReadAllBytes(filePath);
      return Convert.ToBase64String(bytes);
    }



    /// <summary>
    /// Generate Base64 from Bitmap.
    /// </summary>
    /// <param name="img"></param>
    /// <returns>Base64 for video file</returns>
    public static string GetBase64FromBitmap(System.Drawing.Image img)
    {
      Int32 thWidth = img.Width;
      Int32 thHeight = img.Height;
      System.Drawing.Image i = img;
      Int32 w = i.Width;
      Int32 h = i.Height;
      Int32 th = thWidth;
      Int32 tw = thWidth;
      if (h > w)
      {
        Double ratio = (Double)w / (Double)h;
        th = thHeight < h ? thHeight : h;
        tw = thWidth < w ? (Int32)(ratio * thWidth) : w;
      }
      else
      {
        Double ratio = (Double)h / (Double)w;
        th = thHeight < h ? (Int32)(ratio * thHeight) : h;
        tw = thWidth < w ? thWidth : w;
      }
      Bitmap target = new Bitmap(tw, th);
      Graphics g = Graphics.FromImage(target);
      g.SmoothingMode = SmoothingMode.HighQuality;
      g.CompositingQuality = CompositingQuality.HighQuality;
      g.InterpolationMode = InterpolationMode.HighQualityBicubic;
      Rectangle rect = new Rectangle(0, 0, tw, th);
      g.DrawImage(i, rect, 0, 0, w, h, GraphicsUnit.Pixel);

      _height = img.Height;
      _width = img.Width;
      //return (Image)target;
      string base64StringThumb = GetBase64StringFromBitmap(img);
      target.Dispose();
      g.Dispose();
      return base64StringThumb;
    }


    /// <summary>
    /// Genereates the thumbnailfrom video file.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns>Thumbnail file of video</returns>
    public static string ConvertMOVToMP4(string filePath, string file)
    {

      //try
      //{
        string fileFullPath = Path.Combine(filePath, file);

        //string TestFilePath = ConfigHelper.GetThumbnailTempPath();

        string newFileNamePath = string.Empty;
        //File Path 
        FileInfo fi = new FileInfo(file);
        string filename = Path.GetFileNameWithoutExtension(fi.Name);
        Random random = new Random();

        //New File Name Creation
        string newfilename = filename + "1" + ".mp4";
        var processInfo = new ProcessStartInfo();
        processInfo.FileName = AppSettingHelper.GetVidoThumbnailConverterUtilsPath();
        //New FileName Path Generation
        newFileNamePath = Path.Combine(filePath, newfilename);
        //{0}  -an  -s hd720  -vcodec libx264  -b:v BITRATE  -vcodec libx264  -pix_fmt yuv420p  -preset slow  -profile:v baseline  -movflags faststart  -y {1}

        //processInfo.Arguments = string.Format("{0} -vcodec libx264 -b -s -t 1 -movflags faststart  {1} ", fileFullPath, newFileNamePath);

        processInfo.Arguments = string.Format(" -i {0} {1} -vcodec copy -acodec copy", fileFullPath, newFileNamePath);

        processInfo.CreateNoWindow = true;
        processInfo.UseShellExecute = false;
        using (var process = new Process())
        {
          process.StartInfo = processInfo;
          process.Start();
          process.WaitForExit();
          return newfilename;
        }
      //}
      //catch
      //{
      //  return null;
      //}

    }

    /// <summary>
    /// Gets the thumbnail URL.
    /// </summary>
    /// <param name="thumbnailId">The thumbnail identifier.</param>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <returns></returns>
    public static string GetThumbnailUrl(Guid thumbnailId, string fileName, string tenantId)
    {
      //ThumbnailURl Generation 
      string thumbnailUrl = Path.Combine(AppSettingHelper.GetThumbnailUrl(), tenantId, thumbnailId.ToString(), fileName);
      return thumbnailUrl;
    }

    public static string GetNotificationThumbnailUrl(string relativeLanguagePath, string fileName)
    {
      return Path.Combine(AppSettingHelper.GetThumbnailUrl(), "NotificationImages", relativeLanguagePath, fileName);
    }



    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Gets the base64 string from bitmap.
    /// </summary>
    /// <param name="bImage">The b image.</param>
    /// <returns></returns>
    private static string GetBase64StringFromBitmap(System.Drawing.Image bImage)
    {
      var base64String = "";
      //try
      //{
        using (MemoryStream ms = new MemoryStream())
        {
          bImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
          byte[] byteImage = ms.ToArray();
          base64String = Convert.ToBase64String(byteImage);
        }
      //}
      //catch (Exception ex)
      //{
      //  //MessageLogger.Instance.LogMessage(ex.Message + "\r\n" + ex.StackTrace, LoggerCategory.Production, null, false);
      //  throw;
      //}
      return base64String;
    }

    #endregion Private Methods

  }
}
