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
using System.IO;
using ewApps.Core.CommonService;
using PhotoSauce.MagicScaler;

namespace ewApps.Core.DMService {
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