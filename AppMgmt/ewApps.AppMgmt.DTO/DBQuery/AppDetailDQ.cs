﻿using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DTO {
    /// <summary>
    /// Application details Data trasnfer object.
    /// </summary>
    public class AppDetailDQ: BaseDQ {

        /// <summary>
        /// Unique identifier for the table.
        /// </summary>
        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// Name of the application.
        /// </summary>
        public string Name {
            get; set;
        }

        /// <summary>
        /// Delete flag for the application.
        /// </summary>
        public new bool Deleted {
            get; set;
        }

        /// <summary>
        /// Theme Uniqueidentifier.
        /// </summary>
        public Guid ThemeId {
            get; set;
        }

        /// <summary>
        /// App related data.
        /// </summary>
        public string AppData {
            get; set;
        }

        /// <summary>
        /// Application active flag.
        /// </summary>
        public bool Active {
            get; set;
        }

        /// <summary>
        /// Count of services belongs to the application
        /// </summary>
        public int ServiceCount {
            get; set;
        }

        /// <summary>
        /// Puchase count of the application
        /// </summary>
        public int PurchaseCount {
            get; set;
        }

        /// <summary>
        /// Created by userId.
        /// </summary>
        public new Guid CreatedBy {
            get; set;
        }

        /// <summary>
        /// Created by Name.
        /// </summary>
        public String CreaterName {
            get; set;
        }

        /// <summary>
        /// Apllication created date.
        /// </summary>
        public new DateTime? CreatedOn {
            get; set;
        }

        /// <summary>
        /// Application modifier name.
        /// </summary>
        public new Guid UpdatedBy {
            get; set;
        }

        /// <summary>
        /// Application modification date
        /// </summary>
        public new DateTime? UpdatedOn {
            get; set;
        }

        /// <summary>
        /// app thumbnailId.
        /// </summary>
        public Guid? ThumbnailId {
            get; set;
        }

        /// <summary>
        /// app thumbnail filename.
        /// </summary>
        public string FileName {
            get; set;
        }

        /// <summary>
        /// Application thumbnail url
        /// </summary>
        public string ThumbnailUrl {
            get; set;
        }

        /// <summary>
        /// Application identity number
        /// </summary>
        public string IdentityNumber {
            get; set;
        }

        /// <summary>
        /// Application identity number
        /// </summary>
        public string AppKey {
            get; set;
        }

        /// <summary>
        /// Name of the theme.
        /// </summary>
        public string ThemeKey {
            get; set;
        }


        /// <summary>
        /// Application Inactive Comment
        /// </summary>
        public string InactiveComment {
            get; set;
        }
        /// <summary>
        /// Count of publisher belongs to the application
        /// </summary>
        public int PublisherCount {

            get;
            set;
        }

        /// <summary>
        ///App ID
        /// </summary>
        //[NotMapped]
        public Guid AppID {

            get;
            set;
        }
    }
}
