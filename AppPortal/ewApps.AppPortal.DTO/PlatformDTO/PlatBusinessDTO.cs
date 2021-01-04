/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 14 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 14 August 2019
 */

using System;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
    public class PlatBusinessDTO:BaseDTO {

        public new Guid ID {
            get; set;
        }

        public Guid PublisherID {
            get; set;
        }

        public string PublisherIdentityNumber {
            get; set;
        }

        public Guid PublisherTenantId {
            get; set;
        }

        public string PublisherName {
            get; set;
        }

        public string PublisherSubDomain {
            get; set;
        }

        public string IdentityNumber {
            get; set;
        }

        public string BusinessName {
            get; set;
        }

        public DateTime CreatedOn {
            get; set;
        }

        public int ApplicationCount {
            get; set;
        }

        public string TimeZone {
            get;
            set;
        }

        public string BackendERP {
            get;
            set;
        }


        public int TotalUser {
            get; set;
        }

        public bool Active {
            get; set;
        }

        public string VarId {
            get;
            set;
        }

        public DateTime? UpdatedOn {
            get;
            set;
        }

        public string UpdatedByFullName {
            get;
            set;
        }

        private string _status;
        public string Status {
            get {
                if(Active) {
                    _status = "Active";
                }
                else {
                    _status = "Inactive";
                }
                return _status;
            }
            private set {
                _status = value;
            }
        }
    }
}
