using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class PublisherViewDTO {

        public Guid Id {
            get; set;
        }

        public string IdentityNumber {
            get; set;
        }
               
        public string PublisherName {
            get; set;
        }

        public string SubDomainName {
            get; set;
        }

        public DateTime? JoinedOn {
            get; set;
        }

        public DateTime CreatedOn {
            get; set;
        }

        public Guid CreatedBy {
            get; set;
        }

        public string CreatedByName {
            get; set;
        }

        public string Website {
            get; set;
        }

        public bool Active {
            get; set;
        }


        public Guid PrimaryUserId {
            get; set;
        }

        public string PrimaryUserFirstName {
            get; set;
        }

        public string PrimaryUserLastName {
            get; set;
        }

        public string PrimaryUserFullName {
            get; set;
        }

        public string PrimaryUserEmail {
            get; set;
        }

        public string PoweredBy {
            get; set;
        }

        public string Copyright {
            get; set;
        }

        public bool ApplyPoweredBy {
            get; set;
        }

        public bool CanUpdateCopyright {
            get; set;
        }

        public bool CustomizedCopyright {
            get; set;
        }

        public string ContactPersonName {
            get; set;
        }

        public string ContactPersonDesignation {
            get; set;
        }

        public string ContactPersonEmail {
            get; set;
        }

        public string ContactPersonPhone {
            get; set;
        }

        public Guid TenantId {
            get; set;
        }

        [NotMapped]
        public List<PubAppSettingDTO> ApplicationList {
            get; set;
        }

        [NotMapped]
        public List<PublisherAddressDTO> AddressList {
            get; set;
        }

    }
}
