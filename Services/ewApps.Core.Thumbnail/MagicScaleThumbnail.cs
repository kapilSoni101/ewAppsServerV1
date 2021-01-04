

using System;
using System.IO;
using PhotoSauce.MagicScaler;

namespace ewApps.Core.CommonService {
  public class MagicScaleThumbnail
  {

    public static string ImageGenrator(string originalFilePath, string tempFilePath)
    {
      using(FileStream outStream = new FileStream(tempFilePath, FileMode.Create))
        MagicImageProcessor.ProcessImage(originalFilePath, outStream, new ProcessImageSettings { Width = 150, Height = 150 });

      byte[] bytes = FileHelper.GetBytesFromFilePath(tempFilePath);
      return Convert.ToBase64String(bytes);
      //return "";
    }
  }
}
/*    using (var outStream = new FileStream(tempFilePath, FileMode.Create))
            //MagicImageProcessor.ProcessImage(originalFilePath, outStream, new ProcessImageSettings { Width = 150, Height = 150 });

            //byte[] bytes = ewApps.CommonRuntime.Common.FileHelper.GetBytesFromFilePath(tempFilePath);
            //byte[] bytes = null; 
            //return Convert.ToBase64String(bytes);
            return "";


//using(var outStream = new FileStream(@"c:\smallimage.jpg", FileMode.Create)) {
//  MagicImageProcessor.ProcessImage(@"c:\bigimage.jpg", outStream, new ProcessImageSettings { Width = 400 });
//}
*/