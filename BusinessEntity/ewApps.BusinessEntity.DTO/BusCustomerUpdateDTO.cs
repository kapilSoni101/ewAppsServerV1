using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {
    public class BusCustomerUpdateDTO {
        /// <summary>
        /// Unique id of customer.
        /// </summary>
        public new Guid ID {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string PartnerType {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerName {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ERPCustomerKey {
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
        public string Language {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedOn {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid CreatedBy {
            get; set;
        }
    /// <summary>
    /// 
    /// </summary>
    public string FederalTaxID
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Tel1
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string Tel2
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string Website
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string MobilePhone
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string Email
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public int Status {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string TimeZone {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string DateTimeFormat {
            get; set;
        }

        public int CurrencyCode {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string GroupValue {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string GroupSeperator {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string DecimalSeperator {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int DecimalPrecision {
            get; set;
        }


        /// <summary>
        /// 
        /// </summary>
        public List<CustomerContactDTO> CustomerContactList {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<CustomerAddressDTO> BillToAddressList {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<CustomerAddressDTO> ShipToAddressList {
            get; set;
        }

    }

}
