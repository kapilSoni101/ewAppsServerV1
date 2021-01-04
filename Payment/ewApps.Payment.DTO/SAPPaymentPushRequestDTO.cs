using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {

    /// <summary>
    /// Class is used to push the payment to the SAP, Once made a payment against invoices.
    /// </summary>
    public class SAPPaymentPushRequestDTO {

        // CardCode
        public string ERPCustomerKey {
            get; set;
        }

        // PostingDate
        public DateTime PaymentDate {
            get; set;
        }

        // Reference
        public string BAPaymentTransId {
            get; set;
        }

        //  Remarks
        public string Notes {
            get; set;
        }

        /// <summary>
        /// Invoices.
        /// </summary>
        public List<BAPaymentInvoiceSyncDTO> ARInvoices {
            get; set;
        }

        /// <summary>
        /// Credit card
        /// </summary>
        public List<BAPaymentCreditCardsSyncDTO> CreditCards {
            get; set;
        }

        public BAPaymentBankTransferSyncDTO BankTransfer {
            get; set;
        }

        public List<BAPaymentChecksDetailDTO> Checks {
            get; set;
        }
    }

    public class BAPaymentInvoiceSyncDTO {

        //InvoiceNo
        public string ERPARInvoiceId {
            get; set;
        }

        // TotalPayment
        public decimal TransactionAmount {
            get; set;
        }
    }

    public class BAPaymentCreditCardsSyncDTO {


        // CardValidUntil
        public int CardExpiryMonth {
            get; set;
        }

        public int CardExpiryYear {
            get; set;
        }

        public string ERPCreditCardId {
            get; set;
        }


        public string CreditCardNo {
            get; set;
        }

        public decimal Amount {
            get; set;
        }

        //public decimal AdditionalPaymentAmount {
        //  get; set;
        //}
        // I-Internet
        public string TransactionType {
            get; set;
        }

        // Always 1 
        public int NumOfCreditPayments {
            get; set;
        }

        // Always 1 
        public int NumOfPayments {
            get; set;
        }

        // FirstPaymentDate
        public DateTime PaymentTransactionDate {
            get; set;
        }

        // FirstPaymentAmount
        public Decimal TransactionAmount {
            get; set;
        }

        //OwnerIdNum
        public string ContactId {
            get; set;
        }

        public string Voucher {
            get; set;
        }

        //OwnerPhone
        public string ContactPhone {
            get; set;
        }

    }

    public class BAPaymentBankTransferSyncDTO {

        // GL Account should be  map by Customer ID on SAP Side
        //public string GLAccount {
        //  get; set;
        //}

        public string ERPCustomerBankKey {
            get; set;
        }

        public DateTime TransferDate {
            get; set;
        }

        //  Reference 
        public string BAPaymentTransId {
            get; set;
        }

        // Amount
        public decimal TransactionAmount {
            get; set;
        }

    }

    public class BAPaymentChecksDetailDTO {
        public string BankAccount {
            get; set;
        }
        public string BankCode {
            get; set;
        }
        public int CheckNumber {
            get; set;
        }
        public decimal Amount {
            get; set;
        }

        public string Details {
            get; set;
        }

        public string DueDate {
            get; set;
        }
    }

}
