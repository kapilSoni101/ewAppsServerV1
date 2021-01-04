using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.Payment.DTO {    
    /// <summary>
    /// Interface class for Add Payment  from EwApps to Vericheck Connector
    /// </summary>
    public class VeriCheckAddPaymentDTO {

        /// <summary>
        /// Unique payment ID
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Application Business Id
        /// </summary>
        public Guid BusinessId {
            get; set;
        }

        /// <summary>
        /// Unique Tenant Id
        /// </summary>
        public Guid TenantId {
            get; set;
        }

        /// <summary>
        /// Application Partner Id
        /// </summary>
        public Guid PartnerId {
            get; set;
        }

        /// <summary>
        /// Invoice Id against this payment
        /// </summary>
        public Guid InvoiceId {
            get; set;
        }
        /// <summary>
        /// Invoice Amount
        /// </summary>
        public decimal TotalAmount {
            get; set;
        }
        /// <summary>
        /// Payment Amount
        /// </summary>

        public decimal AmountPaid {
            get; set;
        }

        /// <summary>
        /// Payment type - credit/debit
        /// </summary>
        // 'credit' or 'debit'
        public string PaymentType {
            get; set;
        }

        /// <summary>
        /// Payment Entry type - Blank
        /// </summary>
        // blank right now
        public string PaymentEntryType {
            get; set;
        }

        /// <summary>
        /// Check Number
        /// </summary>
        public int CheckNumber {
            get; set;
        }

        /// <summary>
        /// Front image copy of the check associated with the account as Base64 encoded string
        /// Needed for POP, ICL, BOC
        /// </summary>
        public string CheckImageFront {
            get; set;
        }
        /// <summary>
        /// Back image copy of the check associated with the account as Base64 encoded string
        /// Needed for POP, ICL, BOC
        /// </summary>
        public string CheckImageBack {
            get; set;
        }

        /// <summary>
        /// Payment note/Description
        /// </summary>
        public string Note {
            get; set;
        }

        /// <summary>
        /// Payment origination/Initiation Date 
        /// </summary>
        public DateTime OriginationDate {
            get; set;
        }
        /// <summary>
        /// Adding customer DTO to get customer detail 
        /// from application when actual payment is made intead of creating it when it is added to application.
        /// COnnector has responsibility to add and update it whenever required.
        /// </summary>
        public CustomerBankInfoDTO Customer {
            get; set;
        }

        public static VeriCheckAddPaymentDTO MapAddPaymentDTOtoVeriCheck(AddPaymentDTO payDto) {
            VeriCheckAddPaymentDTO model = new VeriCheckAddPaymentDTO();
            model.AmountPaid = payDto.AmountPaid;
            model.BusinessId = payDto.BusinessId;
            model.CheckImageBack = payDto.CheckImageBack;
            model.CheckImageFront = payDto.CheckImageFront;
            model.CheckNumber = payDto.CheckNumber;
            model.ID = payDto.ID;
            model.InvoiceId = payDto.InvoiceId;            
            model.PaymentType = payDto.PaymentType;
            model.TenantId = payDto.TenantId;
            model.OriginationDate = payDto.OriginationDate;
            model.PartnerId = payDto.userPaymentInfoModel.CustomerId;
            model.TotalAmount = payDto.TotalAmount;
            model.Note = payDto.Note;

            model.Customer = new CustomerBankInfoDTO();
            CustVCACHPayAttrDTO vcPayModel = payDto.userPaymentInfoModel.SelectedCustVCACHPayAttr;
            model.Customer.BankAccountName = vcPayModel.NameInBank;
            model.Customer.BankName = vcPayModel.BankName;
            model.Customer.BusinessId = payDto.userPaymentInfoModel.TenantId;
            model.Customer.TenantId = payDto.userPaymentInfoModel.TenantId;
            model.Customer.Name = payDto.userPaymentInfoModel.Name;
            model.Customer.ABARoutingNumber = vcPayModel.ABARounting;
            model.Customer.AccountNumber = vcPayModel.AccountNo;
            model.Customer.AccountType = vcPayModel.AccountType == "Saving" ? BankAccountTypeEnum.Savings : BankAccountTypeEnum.Checking;
            model.Customer.BusinessPartnerTenantId = payDto.userPaymentInfoModel.BusinessPartnerTenantId;
            model.Customer.ID = payDto.userPaymentInfoModel.CustomerId;
            model.Customer.PartnerId = payDto.userPaymentInfoModel.CustomerId;
            model.PaymentEntryType = vcPayModel.SECCode;

            return model;
        }


        public static VeriCheckAddPaymentDTO MapAdvancePaymentDTOtoVeriCheck(AddAdvancedPaymentDTO payDto) {
            VeriCheckAddPaymentDTO model = new VeriCheckAddPaymentDTO();
            model.AmountPaid = payDto.AmountPaid;
            model.BusinessId = payDto.BusinessId;
            model.CheckImageBack = payDto.CheckImageBack;
            model.CheckImageFront = payDto.CheckImageFront;
            model.CheckNumber = payDto.CheckNumber;
            model.ID = payDto.ID;
            model.InvoiceId = payDto.InvoiceId;
            model.PaymentType = payDto.PaymentType;
            model.TenantId = payDto.TenantId;
            model.OriginationDate = payDto.OriginationDate;
            model.PartnerId = payDto.userPaymentInfoModel.CustomerId;
            model.TotalAmount = payDto.TotalAmount;
            model.Note = payDto.Note;

            model.Customer = new CustomerBankInfoDTO();
            CustVCACHPayAttrDTO vcPayModel = payDto.userPaymentInfoModel.SelectedCustVCACHPayAttr;
            model.Customer.BankAccountName = vcPayModel.NameInBank;
            model.Customer.BankName = vcPayModel.BankName;
            model.Customer.BusinessId = payDto.userPaymentInfoModel.TenantId;
            model.Customer.TenantId = payDto.userPaymentInfoModel.TenantId;
            model.Customer.Name = payDto.userPaymentInfoModel.Name;
            model.Customer.ABARoutingNumber = vcPayModel.ABARounting;
            model.Customer.AccountNumber = vcPayModel.AccountNo;
            model.Customer.AccountType = vcPayModel.AccountType == "Saving" ? BankAccountTypeEnum.Savings : BankAccountTypeEnum.Checking;
            model.Customer.BusinessPartnerTenantId = payDto.userPaymentInfoModel.BusinessPartnerTenantId;
            model.Customer.ID = payDto.userPaymentInfoModel.CustomerId;
            model.Customer.PartnerId = payDto.userPaymentInfoModel.CustomerId;
            model.PaymentEntryType = vcPayModel.SECCode;

            return model;
        }

    }
}
