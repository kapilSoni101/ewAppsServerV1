using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {
    public class CustomerContactDTO {
        /// <summary>
        /// Unique id of customer.
        /// </summary>
        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ERPContactKey {
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
            public string FirstName {
                get; set;
            }

            /// <summary>
            /// 
            /// </summary>
            public string LastName {
                get; set;
            }

            /// <summary>
            /// 
            /// </summary>
            public string Title {
                get; set;
            }

            /// <summary>
            /// 
            /// </summary>
            public string Position {
                get; set;
            }

            /// <summary>
            /// 
            /// </summary>
            public string Address {
                get; set;
            }

            /// <summary>
            /// 
            /// </summary>
            public string Telephone {
                get; set;
            }

            /// <summary>
            /// 
            /// </summary>
            public string Email {
                get; set;
            }

        /// <summary>
        /// Maps model properties to entity.
        /// </summary>
        /// <param name="model">model with all required properties.</param>
        /// <returns>Customer entity</returns>
        public static BACustomerContact MapToEntity(CustomerContactDTO model, string ERPCustomerKey, string ERPConnectorKey) {

            BACustomerContact entity = new BACustomerContact() {

                Address = model.Address,
                Email = model.Email,
                Position = model.Position,
                ERPContactKey = model.ERPContactKey,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Telephone = model.Telephone,
                Title = model.Title,
                CustomerId = model.CustomerId,
                ERPConnectorKey = ERPConnectorKey,
                ERPCustomerKey = ERPCustomerKey,

            };

            return entity;
        }

        public static BACustomerContact CopyToEntity(CustomerContactDTO model, BACustomerContact entity, string ERPCustomerKey, string ERPConnectorKey) {


            entity.Address = model.Address;
            entity.Email = model.Email;
            entity.Position = model.Position;
            entity.ERPContactKey = model.ERPContactKey;
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Telephone = model.Telephone;
            entity.Title = model.Title;
            entity.CustomerId = model.CustomerId;
            entity.ERPConnectorKey = ERPConnectorKey;
            entity.ERPCustomerKey = ERPCustomerKey;


            return entity;
        }

    }
}
