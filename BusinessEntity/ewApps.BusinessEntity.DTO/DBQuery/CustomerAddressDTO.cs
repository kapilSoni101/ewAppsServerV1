using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {
    public class CustomerAddressDTO {
        /// <summary>
        /// Unique id of customer.
        /// </summary>
        public new Guid ID {
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
        public string Label {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int ObjectType {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ObjectTypeText {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string AddressName {
            get; set;
        }

        public string AddressStreet1 {
            get;set;
        }

        public string AddressStreet2 {
            get; set;
        }

        public string AddressStreet3 {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Line1 {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Line2 {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Line3 {
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
        /// Maps model properties to entity.
        /// </summary>
        /// <param name="model">model with all required properties.</param>
        /// <returns>Customer entity</returns>
        public static BACustomerAddress CopyToEntity(CustomerAddressDTO model, BACustomerAddress entity) {

            entity.Line1 = model.AddressStreet1;
            entity.Line2 = model.AddressStreet2;
            entity.Line3 = model.AddressStreet3;
            entity.City = model.City;
            entity.Country = model.Country;
            //entity.Line3 = model.Line3;
            entity.AddressName = model.AddressName;
            entity.Label = model.Label;
            entity.ObjectType = model.ObjectType;
            entity.ObjectTypeText = model.ObjectTypeText;
            entity.CustomerId = model.CustomerId;
            entity.State = model.State;
            entity.StreetNo = model.AddressStreet2;
            entity.Street = model.AddressStreet1;
            entity.ZipCode = model.ZipCode;

            return entity;
        }

    }
}
