using System;
using System.Collections.Generic;
using System.Text;
using ewApps.AppMgmt.Entity;

namespace ewApps.AppMgmt.DTO {

    /// <summary>
    /// This class contains application information.
    /// </summary>
    public class AppInfoDTO {

        /// <summary>
        /// The Unique Application Id.
        /// </summary>
        public Guid Id {
            get; set;
        }

        /// <summary>
        /// Identity number.
        /// </summary>
        public string IdentityNumber {
            get; set;
        }

        /// <summary>
        /// The name  of application.
        /// </summary>
        public string Name {
            get; set;
        }

        /// <summary>
        /// Theme Identifier.
        /// </summary>
        public Guid ThemeId {
            get; set;
        }

        /// <summary>
        /// App active status identifier.
        /// </summary>
        public bool Active {
            get; set;
        }

        /// <summary>
        /// App unique identification key.
        /// </summary>
        public string AppKey {
            get; set;
        }

        public static AppInfoDTO MapFromApp(App app) {
            AppInfoDTO appInfo = new AppInfoDTO();
            appInfo.Active = app.Active;
            appInfo.AppKey = app.AppKey;
            appInfo.Id = app.ID;
            appInfo.IdentityNumber = app.IdentityNumber;
            appInfo.Name = app.Name;
            appInfo.ThemeId = app.ThemeId;
            return appInfo;
        }
    }
}
