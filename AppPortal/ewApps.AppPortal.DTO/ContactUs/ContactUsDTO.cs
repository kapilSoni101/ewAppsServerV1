using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class ContactUsDTO {

        public string OS {
            get;set;
        }

        public string Browser {
            get; set;
        }

        public string AppVersion {
            get; set;
        }

        public string Name {
            get; set;
        }

        public string Email {
            get; set;
        }

        public int Phone {
            get; set;
        }


        public string PortalName {
            get; set;
        }

        public string CompanyName {
            get; set;
        }

        [NotMapped]
        public string ApplicationName {
            get; set;
        }

        public DateTime TimeOfAction {
            get; set;
        }
        public string Message {
            get; set;
        }

        public string CopyRightText {
            get;set;
        }

    }
}
