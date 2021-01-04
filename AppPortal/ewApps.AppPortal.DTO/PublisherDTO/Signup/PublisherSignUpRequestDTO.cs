using System;
using System.Collections.Generic;

namespace ewApps.AppPortal.DTO {
   
    public class PublisherSignUpRequestDTO {

        /// <summary>
        /// The name of Publisher.
        /// </summary>
        public string PublisherName {
            get; set;
        }

        /// <summary>
        /// SubDomain for the newly created publisher Tenant.
        /// </summary>
        public string SubDomain {
            get; set;
        }

        /// <summary>
        /// Primary user first name.
        /// </summary>
        public string PrimaryUserFirstName {
            get; set;
        }

        /// <summary>
        /// Primary user last name.
        /// </summary>
        public string PrimaryUserLastName {
            get; set;
        }

        /// <summary>
        /// Primary user full name.
        /// </summary>
        public string PrimaryUserFullName {
            get; set;
        }

        /// <summary>
        /// Primary user email id userd as login userid.
        /// </summary>
        public string PrimaryUserEmail {
            get; set;
        }

        /// <summary>
        /// Address list of the publisher.
        /// </summary>
        public List<PublisherAddressDTO> PublisherAddressDTO {
            get;
            set;
        }

        /// <summary>
        /// Publisher admin user contact person name.
        /// </summary>
        public string ContactPersonName {
            get;
            set;
        }

        /// <summary>
        /// Publisher admin user contact person designation.
        /// </summary>
        public string ContactPersonDesignation {
            get;
            set;
        }

        /// <summary>
        /// Publisher admin user contact person email.
        /// </summary>
        public string ContactPersonEmail {
            get;
            set;
        }

        /// <summary>
        /// Publisher admin user contact person phone.
        /// </summary>
        public string ContactPersonPhone {
            get;
            set;
        }

        /// <summary>
        /// Powerd by text.
        /// </summary>
        public string PoweredBy {
            get;
            set;
        }

        /// <summary>
        /// Copyright text.
        /// </summary>
        public string CopyrightText {
            get;
            set;
        }

        /// <summary>
        /// List of applications given to the publisher.
        /// </summary>
        public List<PubAppSettingDTO> ApplicationList {
            get; set;
        }

        /// <summary>
        /// Website of the publisher.
        /// </summary>
        public string Website {
            get;
            set;
        }

        /// <summary>
        /// Apply Powered by flag.
        /// </summary>
        public bool ApplyPoweredBy {
            get;
            set;
        }

        /// <summary>
        /// Can Update Copyright.
        /// </summary>
        public bool CanUpdateCopyright {
            get;
            set;
        }

        /// <summary>
        /// Plafrotm thimbnail id if set by the user.
        /// </summary>
        public Guid PlatformLogoThumbnailId {
            get;
            set;
        }
    }
}
