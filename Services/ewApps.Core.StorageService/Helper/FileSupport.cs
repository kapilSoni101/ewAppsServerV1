using System.IO;
using System.Linq;
using System.Web;

namespace ewApps.Core.StorageService {

  public class FileSupport {


    public static string GetFileMimeType(string fileName) {
      string mType = GetExceptionalMimeType(fileName);
      if(string.IsNullOrEmpty(mType))
        return MimeMapping.MimeUtility.GetMimeMapping(fileName);

      return mType;
    }

    private static string GetExceptionalMimeType(string fileName) {
      string ext = new FileInfo(fileName).Extension.ToLower();
      if(ext == ".csv")
        return "text/plain";
      return "";
    }

    /*
        public static string GenerateFileUrl(string fileName, Guid tenantId, Guid storageId, int mediaType, string mimeType) {
          string sRoot = FileHelper.GetDocumentRootPath();
          string sTenantId = tenantId.ToString();
          string sDocId = storageId.ToString();
          switch ((MediaType)mediaType) {
            case MediaType.Audio:
              return Path.Combine(sRoot, sTenantId, sDocId, fileName);
            case MediaType.Document:
              string filePath = Path.Combine(sRoot, sTenantId, sDocId, fileName);
              if (IsDocumnetOfficeType(mimeType))
                return "https://view.officeapps.live.com/op/embed.aspx?src=" + filePath;
              return filePath;
            case MediaType.Image:
              return Path.Combine(sRoot, sTenantId, sDocId, fileName);
            case MediaType.Video:
              return Path.Combine(sRoot, sTenantId, sDocId, fileName);
          }

          return Path.Combine(sRoot, sTenantId, sDocId, fileName); ;
        }
    */

    /*

     */

    public static bool IsDocumnetType(string mimeType) {
      if(!IsImageMimeType(mimeType) && !IsAudioMimeType(mimeType) && !IsVideoMimeType(mimeType)) {
        return true;
      }
      return false;
    }

    public static bool IsDocumnetOfficeType(string mimeType) {

      if(mimeType == "application/msword" || mimeType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" ||
          mimeType == "application/vnd.openxmlformats-officedocument.wordprocessingml.template" ||
          mimeType == "application/vnd.ms-word.document.macroEnabled.12" ||
          mimeType == "application/vnd.ms-word.template.macroEnabled.12" ||
          mimeType == "application/vnd.ms-excel" ||
          mimeType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || mimeType == "application/vnd.openxmlformats-officedocument.spreadsheetml.template" ||
          mimeType == "application/vnd.ms-excel.sheet.macroEnabled.12" || mimeType == "application/vnd.ms-excel.template.macroEnabled.12" || mimeType == "application/vnd.ms-excel.addin.macroEnabled.12" || mimeType == "application/vnd.ms-excel.sheet.binary.macroEnabled.12" ||
          mimeType == "application/vnd.ms-powerpoint" || mimeType == "application/vnd.openxmlformats-officedocument.presentationml.presentation" || mimeType == "application/vnd.openxmlformats-officedocument.presentationml.template" ||
          mimeType == "application/vnd.openxmlformats-officedocument.presentationml.slideshow" || mimeType == "application/vnd.ms-powerpoint.addin.macroEnabled.12" || mimeType == "application/vnd.ms-powerpoint.presentation.macroEnabled.12" ||
          mimeType == "application/vnd.ms-powerpoint.template.macroEnabled.12" || mimeType == "application/vnd.ms-powerpoint.slideshow.macroEnabled.12") {
        return true;
      }
      return false;
    }

    public static bool IsDocumentOpenOfficeType(string fileName) {
      string ext = new FileInfo(fileName).Extension.ToLower();
      if(ext == ".odt" || ext == ".ods" || ext == ".odp")
        return true;
      return false;
    }

    public static bool IsImageMimeType(string mimeType) {
      if(mimeType == "image/jpeg" || mimeType == "image/gif" || mimeType == "image/png" || mimeType == "image/vnd.microsoft.icon"
          || mimeType == "image/vnd.sealed.png" || mimeType == "image/vnd.sealedmedia.softseal.gif" || mimeType == "image/vnd.sealedmedia.softseal.jpg"
          || mimeType == "image/vnd.svf" || mimeType == "image/cgm" || mimeType == "image/fits" || mimeType == "image/g3fax"
          || mimeType == "image/ief" || mimeType == "image/jp2" || mimeType == "image/jpm"
          || mimeType == "image/jpx" || mimeType == "image/naplps" || mimeType == "image/prs.btif"
          || mimeType == "image/prs.pti" || mimeType == "image/t38" || mimeType == "image/tiff" || mimeType == "image/tiff-fx"
          || mimeType == "image/vnd.adobe.photoshop" || mimeType == "image/vnd.cns.inf2" || mimeType == "image/vnd.djvu"
          || mimeType == "image/vnd.dwg" || mimeType == "image/vnd.dxf" || mimeType == "image/vnd.fastbidsheet"
          || mimeType == "image/vnd.fpx" || mimeType == "image/vnd.fst" || mimeType == "image/vnd.fujixerox.edmics-mmr"
          || mimeType == "image/vnd.fujixerox.edmics-rlc" || mimeType == "image/vnd.globalgraphics.pgb"
          || mimeType == "image/vnd.mix" || mimeType == "image/vnd.ms-modi" || mimeType == "image/vnd.net-fpx"
          || mimeType == "image/vnd.wap.wbmp" || mimeType == "image/vnd.xiff") {
        return true;
      }
      return false;
    }

    public static bool IsAudioMimeType(string mimeType) {
      if(mimeType == "audio/basic" || mimeType == "auido/L24" || mimeType == "audio/mid" || mimeType == "audio/mpeg"
          || mimeType == "audio/mp4" || mimeType == "audio/x-aiff" || mimeType == "audio/x-mpegurl" || mimeType == "audio/vnd.rn-realaudio"
          || mimeType == "audio/ogg" || mimeType == "audio/vorbis" || mimeType == "audio/vnd.wav" || mimeType == "audio/x-scpls" || mimeType == "audio/flac") {
        return true;
      }
      return false;
    }

    public static bool IsVideoMimeType(string mimeType) {

      if(mimeType == "application/vnd.apple.mpegurl" || mimeType == "application/x-mpegurl" || mimeType == "video/3gpp"
          || mimeType == "video/mp4" || mimeType == "video/mpeg" || mimeType == "video/ogg" || mimeType == "video/quicktime"
          || mimeType == "video/webm" || mimeType == "video/x-m4v" || mimeType == "video/ms-asf" || mimeType == "video/x-ms-wmv"
          || mimeType == "video/x-msvideo") {
        return true;
      }
      return false;
    }

  }
}