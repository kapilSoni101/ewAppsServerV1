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
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ewApps.Core.CommonService {
    public static class AppSettingHelper {

        #region public methods 
        public static string GetDefaultImagePath() {
            return "";
        }

        public static string GetTempDocumentRootPath() {
            return "";
        }

        public static string GetThumbnailUrl() {
            return "";
        }

        public static string GetVidoThumbnailConverterUtilsPath() {
            return "";
        }

        public static string GetGoogleAPIServerKey() {
            return "";
        }

        public static string GetGoogleAPITimeZoneUri() {
            return "";
        }

        public static string GetGoogleAPIGeocodeUri() {
            return "";
        }

        public static string GetBranchAPIUrl() {
            return "";
        }

        public static string GetDeeplinkBaseDesktopUrl() {
            return "";
        }

        public static string GetBranchAPIKey() {
            return "";
        }
        public static string GetThumbnailPath() {

            var builder = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "AppSettings"))
                 .AddJsonFile("appsettings.dev.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            string physicalPath = configuration.GetSection("Thumbnail:ThumbnailUrl").Value;
            return physicalPath;

        }
        #endregion
    }
}
