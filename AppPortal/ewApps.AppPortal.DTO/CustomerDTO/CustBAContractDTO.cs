using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Represents properties for BAContract entity.
    /// </summary>  
    public class CustBAContractDTO
  {

        public Guid ID {
            get; set;
        }

        public string ERPConnectorKey {
            get; set;
        }

        public string ERPContractKey {
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


        public string ERPDocNum {
            get; set;
        }


    }
}
