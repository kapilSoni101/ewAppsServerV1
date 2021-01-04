using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppMgmt.DTO {
    public class ConfigurationDTO {

        #region Tenant's Detail

        /// <summary>
        /// 
        /// </summary>
        public new Guid TenantId {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SubDomainName {
            get; set;
        }

        /// <summary>
        /// Name of the application.
        /// </summary>
        public string Name {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string IdentityNumber {
            get;
            set;
        }

        /// <summary>
        /// Tenant's primary user last invitation date and time.
        /// </summary>        
        public DateTime InvitedOn {
            get;
            set;
        }

        /// <summary>
        /// User name who invite tenant's primary user last.
        /// </summary>
        public string InvitedBy {
            get;
            set;
        }

        /// <summary>
        /// Date and Time when user accept invitation for current application.
        /// </summary>
        public DateTime? JoinedDate {
            get; set;
        }

        /// <summary>
        /// True if tenant is active.
        /// </summary>
        public bool Active {
            get; set;
        }

        #endregion Tenant's Detail


        #region Tenant User / Admin User Detail

        /// <summary>
        /// 
        /// </summary>
        public Guid AdminUserId {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AdminUserFirstName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AdminUserLastName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AdminUserEmail {
            get; set;
        }

        #endregion Tenant User / Admin User Detail

       


        #region  Contact Person Detail

        /// <summary>
        /// 
        /// </summary>
        public Guid Id {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ContactPersonName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ContactPersonDesignation {
            get; set;
        }

        /// <summary>
        /// Tenant's communication email.
        /// </summary>
        public string ContactPersonEmail {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ContactPersonPhone {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Website {
            get; set;
        }

        #endregion  Contact Person Detail



        /// <summary>
        /// Business portal URL of the application for current tenant.
        /// </summary>
        [NotMapped]
        public string PortalUrl {
            get; set;
        }
       


    }
}
