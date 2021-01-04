using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.CommonService;

namespace ewApps.BusinessEntity.DTO {
    /// <summary>
    /// 
    /// </summary>
    public class SignUpBACustomerDTO {

        /// <summary>
        /// 
        /// </summary>
        public string ERPConnectorKey {
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
        public string CustomerName {
            get; set;
        }
        /// <summary>
        /// customer tenant id .
        /// </summary>
        public Guid BusinessPartnerTenantId {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Group {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Currency {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AddressLine1 {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string AddressLine2 {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Street {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string StreetNo {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string City {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZipCode {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string State {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Country {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tel1 {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tel2 {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string MobilePhone {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Fax {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Email {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Website {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int? ShippingType {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ShippingTypeText {
            get; set;
        }


        /// <summary>
        /// 
        /// </summary>
        public int? Status {
            get; set;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Remarks {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int? OpType {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<BACustomerPaymentDetailSyncDTO> CustomerPaymentDetailList {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<SignUpBACustomerContactDTO> CustomerContactList {
            get; set;
        }

        #region Currency Localization Value

        /// <summary>
        ///CurrencyCode 
        /// </summary>
        public int CurrencyCode {
            get; set;
        }
        /// <summary>
        ///GroupValue
        /// </summary>
        public string GroupValue {
            get; set;
        }

        /// <summary>
        ///Powered By  
        /// </summary>
        public string GroupSeperator {
            get; set;
        }

        /// <summary>
        ///DecimalSeperator 
        /// </summary>
        public string DecimalSeperator {
            get; set;
        }


        /// <summary>
        ///DecimalPrecision
        /// </summary>
        public int DecimalPrecision {
            get; set;
        }

        #endregion Currency Localization Value

        public string FederalTaxID {
            get;set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<SignUpBACustomerAddressDTO> CustomerAddressList {
            get; set;
        }


        /// <summary>
        /// Maps model properties to entity.
        /// </summary>
        /// <param name="model">model with all required properties.</param>
        /// <returns>Customer entity</returns>
        public static BACustomer MapToEntity(SignUpBACustomerDTO model) {

            BACustomer entity = new BACustomer() {
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                BusinessPartnerTenantId = model.BusinessPartnerTenantId,
                City = model.City,
                Country = model.Country,
                Currency = PicklistHelper.GetCurrencySymbolById(model.CurrencyCode) ,
                Email = model.Email,
                ERPConnectorKey = model.ERPCustomerKey,
                FederalTaxID = model.FederalTaxID,
                Fax = model.Fax,
                Group = model.Group,
                MobilePhone = model.MobilePhone,
                CustomerName = model.CustomerName,
                ERPCustomerKey = model.ERPCustomerKey,
                State = model.State,
                Status = model.Status,
                StreetNo = model.StreetNo,
                Street = model.Street,
                Tel1 = model.Tel1,
                Tel2 = model.Tel2,
                Website = model.Website,
                ZipCode = model.ZipCode,
                Remarks = model.Remarks,
                ShippingType = model.ShippingType,
                ShippingTypeText = model.ShippingTypeText

            };

            return entity;
        }
    }
}
