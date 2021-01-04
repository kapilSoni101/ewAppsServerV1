using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// Class contains detail model of tenant.
    /// </summary>
    public class UpdateTenantModelDQ:BaseDQ {
        /// <summary>
        /// TenantId.
        /// </summary>
        public new Guid ID {
            get; set;
        }

        public string VarId {
            get; set;
        }

        public string Name {
            get; set;
        }

        public string SubDomainName {
            get; set;
        }

        public bool Active {
            get; set;
        }

        public new DateTime CreatedOn {
            get; set;
        }

        public string Currency {
            get; set;
        }

        public string CreatedByName {
            get; set;
        }

        public string IdentityNumber {
            get; set;
        }

        public new Guid CreatedBy {
            get; set;
        }

        public new Guid UpdatedBy {
            get; set;
        }

        /// <summary>
        /// Tenant activation (joined date).
        /// </summary>
        public DateTime? JoinedOn {
            get; set;
        }       

        #region Tenant Primary User 

        [NotMapped]
        public string PrimaryUserEmail {
            get; set;
        }

        [NotMapped]
        public string PrimaryUserFirstName {
            get; set;
        }

        [NotMapped]
        public string PrimaryUserLastName {
            get; set;
        }

        /// <summary>
        /// Tenant home application Primary user joined date.
        /// </summary>
        [NotMapped]
        public DateTime? UserActivationDate {
            get; set;
        }

        /// <summary>
        /// Tenant primary user Id
        /// </summary>
        [NotMapped]
        public Guid PrimaryUserId {
            get; set;
        }

        #endregion Tenant Primary User

        /// <summary>
        /// Class contains detail subscription information.
        /// </summary>
        [NotMapped]
        public List<UpdateBusinessAppSubscriptionDTO> Subscriptions {
            get; set;
        }

        /// <summary>
        /// Map properties to business model and return it.
        /// </summary>
        /// <returns></returns>
        public UpdateBusinessTenantModelDQ MapProperties(UpdateBusinessTenantModelDQ busModel) {
            //UpdateBusinessTenantModelDQ busModel = new UpdateBusinessTenantModelDQ();
            busModel.Active = this.Active;
            busModel.CreatedBy = this.CreatedBy;
            busModel.CreatedByName = this.CreatedByName;
            busModel.CreatedOn = this.CreatedOn;
            busModel.Currency = this.Currency;
            busModel.Deleted = this.Deleted;
            busModel.ID = this.ID;
            //busModel.ID = this.IdentityNumber;
            busModel.JoinedOn = this.JoinedOn;
            busModel.Name = this.Name;
            busModel.PrimaryUserEmail = this.PrimaryUserEmail;
            busModel.PrimaryUserFirstName = this.PrimaryUserFirstName;
            busModel.PrimaryUserId = this.PrimaryUserId;
            busModel.PrimaryUserLastName = this.PrimaryUserLastName;
            busModel.SubDomainName = this.SubDomainName;
            busModel.Subscriptions = this.Subscriptions;
            busModel.TenantId = this.TenantId;
            busModel.UpdatedBy = this.UpdatedBy;
            busModel.UpdatedOn = this.UpdatedOn;
            busModel.UserActivationDate = this.UserActivationDate;
            busModel.VarId = this.VarId;            

            return busModel;
        }

    }
}
