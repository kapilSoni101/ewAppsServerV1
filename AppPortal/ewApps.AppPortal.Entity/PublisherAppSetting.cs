/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 11 March 2019
 * 
 * Contributor/s: Ishwar Rathore/Anil Nigam
 * Last Updated On: 11 March 2019
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.AppPortal.Entity {

  /// <summary>
  /// Entity responsible for storing data related to the application for the publisher and the lower entity of the publisher.(Publisher refered by the teanntId)
  /// </summary>
  [Table("PublisherAppSetting", Schema = "ap")]
  public class PublisherAppSetting:BaseEntity {

        /// <summary>
        /// Name of the application for the publisher and its lower level entity. 
        /// </summary>
        public string Name {
            get;
            set;
        }

        /// <summary>
        /// Thumbnail id for the publisher (Direct we have to refer this).
        /// </summary>
        public Guid? ThumbnailId {
            get;
            set;
        }

        /// <summary>
        /// Tenant Copyright of the application for the publisher.;
        /// </summary>
        public string CopyrightsText {
            get;
            set;
        }

        /// <summary>
        /// The Id of Application from master data.
        /// </summary>
        [Required]
        public Guid AppId {
            get; set;
        }

        /// <summary>
        /// flag fro the powered by text customization.
        /// </summary>
        public bool Customized {
            get; set;
        }

        /// <summary>
        /// The PublisherSetting status .
        /// </summary>
        public bool Active {
            get; set;
        }

        /// <summary>
        /// The Id of Theme for the particular application of the publisher for its lower entity.
        /// </summary>
        [Required]
        public Guid ThemeId {
            get; set;
        }

        /// <summary>
        /// Application Inactive Comment for the publisher for its lower entity.
        /// </summary>
        public string InactiveComment {
            get;
            set;
        }

        #region IValidator<App> Members

        /// <inheritdoc />
        public bool Validate(out IList<EwpErrorData> brokenRules) {
            brokenRules = BrokenRules(this).ToList<EwpErrorData>();
            return brokenRules.Count > 0;
        }

        /// <inheritdoc />
        public IEnumerable<EwpErrorData> BrokenRules(PublisherAppSetting entity) {
            //Check for application name is required.
            if(string.IsNullOrEmpty(entity.Name))
                yield return new EwpErrorData() {
                    ErrorSubType = (int)ValidationErrorSubType.FieldRequired,
                    Data = "Name",
                    Message = "App name is required."
                };
        }

        #endregion

    }
}
