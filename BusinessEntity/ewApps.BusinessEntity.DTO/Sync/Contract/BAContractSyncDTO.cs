using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {

    /// <summary>
    /// Represents properties for BAContract entity.
    /// </summary>  
    public class BAContractSyncDTO {

        public string ERPConnectorKey {
            get; set;
        }

        public string ERPContractKey {
            get; set;
        }

        public string ERPDocNum {
            get; set;
        }

        public string ERPCustomerKey {
            get; set;
        }


        public Guid CustomerId {
            get; set;
        }


        public string CustomerName {
            get; set;
        }

        public string ContactPerson {
            get; set;
        }

        public string CustomerRefNo {
            get; set;
        }

        public string BPCurrency {
            get; set;
        }

        public string TelephoneNo {
            get; set;
        }

        public string Email {
            get; set;
        }

        public int DocumentNo {
            get; set;
        }

        public string AgreementMethod {
            get; set;
        }


        public DateTime? StartDate {
            get; set;
        }

        public DateTime? EndDate {
            get; set;
        }

        public string BPProject {
            get; set;
        }

        public DateTime? TerminationDate {
            get; set;
        }

        public DateTime? SigningDate {
            get; set;
        }

        public string Description {
            get; set;
        }

        public string AgreementType {
            get; set;
        }

        public string PaymentTerms {
            get; set;
        }


        public string PaymentMethod {
            get; set;
        }

        public int ShippingType {
            get; set;
        }

        public string ShippingTypeText {
            get; set;
        }

        public int Status {
            get; set;
        }


        public string StatusText {
            get; set;
        }

        public string Remarks {
            get; set;
        }


        public string Owner {
            get; set;
        }

        public string OpType {
            get; set;
        }
        /// <summary>
        /// sales order item list.
        /// </summary>
        public List<BAContractItemSyncDTO> LineItems {
            get; set;
        }

        /// <summary>
        /// sales order AttachmentList list.
        /// </summary>
        public List<BAContractAttachmentSyncDTO> Attachments {
            get; set;
        }

        /// <summary>
        /// Map model object to entity object and return it.
        /// </summary>
        /// <returns></returns>
        public static BAContract MapToEntity(BAContractSyncDTO model) {
            BAContract entity = new BAContract();
            entity.ERPConnectorKey = model.ERPConnectorKey;
            entity.ERPContractKey = model.ERPContractKey;
            entity.ERPDocNum = model.ERPDocNum;
            entity.ERPCustomerKey = model.ERPCustomerKey;
            entity.CustomerId = model.CustomerId;
            entity.CustomerName = model.CustomerName;
            entity.CustomerRefNo = model.CustomerRefNo;
            entity.ContactPerson = model.ContactPerson;
            entity.PaymentMethod = model.PaymentMethod;
            entity.PaymentTerms = model.PaymentTerms;
            entity.AgreementMethod = model.AgreementMethod;
            entity.AgreementType = model.AgreementType;
            entity.BPCurrency = model.BPCurrency;
            entity.BPProject = model.BPProject;
            entity.Description = model.Description;
            entity.Owner = model.Owner;
            entity.DocumentNo = model.DocumentNo;
            entity.Email = model.Email;
            entity.EndDate = model.EndDate;
            entity.Remarks = model.Remarks;
            entity.SigningDate = model.SigningDate;
            entity.StartDate = model.StartDate;
            entity.TelephoneNo = model.TelephoneNo;
            entity.TerminationDate = model.TerminationDate;
            entity.ShippingType = model.ShippingType;
            entity.ShippingTypeText = model.ShippingTypeText;
            entity.Status = model.Status;
            entity.StatusText = model.StatusText;

            return entity;
        }
    /// <summary>
    /// Map model object to entity object and return it.
    /// </summary>
    /// <returns></returns>
    public static BAContract MapToEntity(BAContractSyncDTO model, BAContract entity)
    {
      entity.ContactPerson = model.ContactPerson;
      entity.PaymentMethod = model.PaymentMethod;
      entity.PaymentTerms = model.PaymentTerms;
      entity.AgreementMethod = model.AgreementMethod;
      entity.AgreementType = model.AgreementType;
      entity.BPCurrency = model.BPCurrency;
      entity.BPProject = model.BPProject;
      entity.Description = model.Description;
      entity.Owner = model.Owner;
      entity.DocumentNo = model.DocumentNo;
      entity.Email = model.Email;
      entity.EndDate = model.EndDate;
      entity.Remarks = model.Remarks;
      entity.SigningDate = model.SigningDate;
      entity.StartDate = model.StartDate;
      entity.TelephoneNo = model.TelephoneNo;
      entity.TerminationDate = model.TerminationDate;
      entity.ShippingType = model.ShippingType;
      entity.ShippingTypeText = model.ShippingTypeText;
      entity.Status = model.Status;
      entity.StatusText = model.StatusText;

      return entity;
    }
  }
}
