/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 19 December 2018
 */

using System;
using ewApps.Core.BaseService;

namespace ewApps.Report.DTO {
    public class PlatformTenantDTO : BaseDTO {
        public new Guid ID {
            get; set;
        }

        public string PublisherIdentityNumber {
            get; set;
        }        

        public string PublisherName {
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

        public string TimeZone {
            get;
            set;
        }

        public string BackendERP {
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

        public new bool Deleted {
            get; set;
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

