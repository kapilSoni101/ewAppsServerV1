using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {
    /// <summary>
    /// 
    /// </summary>
    public class BACustomerPaymentDetailSyncDTO {
        /// <summary>
        /// 
        /// </summary>
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid CustomerId {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ERPCustomerKey {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int CreditCardType {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string CreditCardTypeText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string CreditCardNo {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ExpirationDate {
            get; set;
        }


        /// <summary>
        /// IDNumber
        /// </summary>
        public int IDNumber {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string BankCountry {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string BankName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string BankCode {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Account {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string BICSWIFTCode {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string BankAccountName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Branch {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Default {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ABARouteNumber {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int AccountType {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AccountTypeText {
            get; set;
        }
        /// <summary>
        /// Maps model properties to entity.
        /// </summary>
        /// <param name="model">model with all required properties.</param>
        /// <returns>Customer entity</returns>
        public static BACustomerPaymentDetail MapToEntity(BACustomerPaymentDetailSyncDTO model) {

            BACustomerPaymentDetail entity = new BACustomerPaymentDetail() {

                ABARouteNumber = model.ABARouteNumber,
                Account = model.Account,
                AccountType = model.AccountType,
                AccountTypeText = model.AccountTypeText,
                BankAccountName = model.BankAccountName,
                BankCode = model.BankCode,
                BankCountry = model.BankCountry,
                BankName = model.BankName,
                BICSWIFTCode = model.BICSWIFTCode,
                CustomerId = model.CustomerId,
                ERPConnectorKey = model.ERPConnectorKey,
                ERPCustomerKey = model.ERPCustomerKey,
                Branch = model.Branch,
                CreditCardNo = model.CreditCardNo,
                CreditCardType = model.CreditCardType,
                CreditCardTypeText = model.CreditCardTypeText,
                Default = model.Default,
                ExpirationDate = model.ExpirationDate,
                IDNumber = model.IDNumber

            };

            return entity;
        }
    }
}
