using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class PublisherUpdateReqDTO {

        public Guid Id {
            get; set;
        }

        public string IdentityNumber {
            get; set;
        }

        public string PublisherName {
            get; set;
        }



        public string Website {
            get; set;
        }

        public bool Active {
            get; set;
        }

        public Guid TenantId {
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

        public bool CustomizedCopyright {
            get; set;
        }


        public int CopyrightAccessType {
            get; set;
        }





        public string InactiveComment {
            get; set;
        }





        public List<PubAppSettingDTO> ApplicationList {
            get; set;
        }

        public List<PublisherAddressDTO> AddressList {
            get; set;
        }


    }
}
