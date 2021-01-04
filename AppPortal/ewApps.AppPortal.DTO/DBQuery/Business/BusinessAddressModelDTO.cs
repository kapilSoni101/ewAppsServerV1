/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 12 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 12 September 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// 
    /// </summary>
    public class BusinessAddressModelDTO: BaseDTO {

        public new Guid ID {
            get; set;
        }

        public override Guid CreatedBy {
            get; set;
        }


        public override DateTime? CreatedOn {
            get; set;
        }


        public override Guid UpdatedBy {
            get; set;
        }


        public override DateTime? UpdatedOn {
            get; set;
        }


        public override bool Deleted {
            get; set;
        }


        public override Guid TenantId {
            get; set;
        }


        public string Label {
            get; set;
        }

        public Guid BusinessId {
            get; set;
        }


        public string AddressStreet1 {
            get; set;
        }

        public string AddressStreet2 {
            get; set;
        }

        public string AddressStreet3 {
            get; set;
        }


        public string City {
            get; set;
        }

        public string State {
            get; set;
        }

        public string ZipCode {
            get; set;
        }

        public string Country {
            get; set;
        }

        public string FaxNumber {
            get; set;
        }

        /// <summary>
        /// Phone.
        /// </summary>
        public string Phone {
            get; set;
        }
        public int AddressType {
            get; set;
        }
        /// <summary>
        /// Maps model properties to entity.
        /// </summary>
        /// <param name="model">model with all required properties.</param>
        /// <returns>Customer entity</returns>
        public static BusinessAddress MapToEntity(BusinessAddressModelDTO model) {

            BusinessAddress entity = new BusinessAddress() {
                BusinessId = model.BusinessId,
                ID = model.ID,
                TenantId = model.TenantId,
                Label = model.Label,
                AddressStreet1 = model.AddressStreet1,
                AddressStreet2 = model.AddressStreet2,
                AddressStreet3 = model.AddressStreet3,
                City = model.City,
                Country = model.Country,
                State = model.State,
                Phone = model.Phone,
                ZipCode = model.ZipCode,
                AddressType = model.AddressType,
                FaxNumber = model.FaxNumber,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn,
                Deleted = model.Deleted
            };

            return entity;
        }
    }
}
