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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.Core.DMService {
    public class ThumbnailAddAndUpdateDTO {

        /// <summary>
        /// Unique identifier of thumbnail.
        /// </summary>
        /// 
        [Key]
        public Guid ID {
            get;
            set;
        }

        ///// <summary>
        ///// Unique identifier of thumbnail.
        ///// </summary>
        //public Guid ThumbnailId
        //{
        //  get;
        //  set;
        //}

        ///// <summary>
        ///// Unique identifier of Document.
        ///// </summary>
        //public Guid DocumentId
        //{
        //  get;
        //  set;
        //}

        [NotMapped]
        /// <summary>
        /// Owner Entity Type of thumbnail.
        /// </summary>
        public Int32 ThumbnailOwnerEntityType {
            get;
            set;
        }

        [NotMapped]
        /// <summary>
        /// Owner Entity identifier of thumbnail.
        /// </summary>
        public Guid ThumbnailOwnerEntityId {
            get;
            set;
        }

        [NotMapped]
        /// <summary>
        /// File name of thumbnail.
        /// </summary>
        public string ThumbnailFileName {
            get;
            set;
        }

        [NotMapped]
        /// <summary>
        /// File size of thumbnail in KB.
        /// </summary>
        public double ThumbnailFileSizeInKB {
            get;
            set;
        }

        [NotMapped]
        /// <summary>
        /// Height of the thumbnail requested.
        /// </summary>
        public Int32 ReqThumbnailHeight {
            get;
            set;
        }

        [NotMapped]
        /// <summary>
        /// Width of the thumbnail requested.
        /// </summary>
        public Int32 ReqThumbnailWidth {
            get;
            set;
        }

        [NotMapped]
        /// <summary>
        /// Base64String of thumbnail.
        /// </summary>
        public string ThumbnailBase64String {
            get;
            set;
        }

        [NotMapped]
        /// <summary>
        /// Operation type on thumbnail
        /// </summary>
        public int OperationType {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public double Duration {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the type of the media.
        /// </summary>
        /// <value>
        /// The type of the media.
        /// </value>
        public int MediaType {
            get;
            set;
        }

        [NotMapped]
        /// <summary>
        /// Gets or sets a value indicating whether [change thumbnail].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [change thumbnail]; otherwise, <c>false</c>.
        /// </value>
        public bool ChangeThumbnail {
            get;
            set;
        }

        [NotMapped]
        /// <summary>
        /// Gets or sets the thumbnail URL.
        /// </summary>
        /// <value>
        /// The thumbnail URL.
        /// </value>
        public string ThumbnailUrl {
            get;
            set;
        }

        [NotMapped]
        /// <summary>
        /// Gets or sets the document URL.
        /// </summary>
        /// <value>
        /// The document URL.
        /// </value>
        public string DocumentUrl {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the document file.
        /// </summary>
        /// <value>
        /// The name of the document file.
        /// </value>
        public string DocumentFileName {
            get;
            set;
        }





        /// <summary>
        /// Maps the thumbnail details in this class to a thumbnail object.
        /// </summary>
        /// <returns>Thumbnail Details as a Thumbnail object.</returns>
        public EntityThumbnail MapToThumbnail(EntityThumbnail thumbnail) {

            if(this.ID != Guid.Empty)
                thumbnail.ID = this.ID;
            thumbnail.OwnerEntityType = this.ThumbnailOwnerEntityType;
            thumbnail.OwnerEntityId = this.ThumbnailOwnerEntityId;
            thumbnail.FileName = this.ThumbnailFileName;
            thumbnail.FileSizeinKB = this.ThumbnailFileSizeInKB;
            thumbnail.Height = this.ReqThumbnailHeight;
            thumbnail.Width = this.ReqThumbnailWidth;
            //thumbnail.Base64String = this.ThumbnailBase64String;
            thumbnail.Duration = this.Duration;
            thumbnail.MediaType = this.MediaType;
            thumbnail.DocumentFileName = this.DocumentFileName;
            return thumbnail;
        }

        /// <summary>
        /// Maps the thumbnail details in this class from a thumbnail object.
        /// </summary>
        /// <returns></returns>
        public void MapFromThumbnail(EntityThumbnail thumbnail) {
            if(thumbnail.ID != Guid.Empty)
                this.ID = thumbnail.ID;

            this.ThumbnailOwnerEntityType = thumbnail.OwnerEntityType;
            this.ThumbnailOwnerEntityId = thumbnail.OwnerEntityId;
            this.ThumbnailFileName = thumbnail.FileName;
            this.ThumbnailFileSizeInKB = thumbnail.FileSizeinKB;
            this.ReqThumbnailHeight = thumbnail.Height;
            this.ReqThumbnailWidth = thumbnail.Width;
            //this.ThumbnailBase64String=thumbnail.Base64String;
            this.Duration = thumbnail.Duration;
            this.MediaType = thumbnail.MediaType;
            this.DocumentFileName = thumbnail.DocumentFileName;
            //this.DocumentId = thumbnail.DocumentId;

        }
    }
}
