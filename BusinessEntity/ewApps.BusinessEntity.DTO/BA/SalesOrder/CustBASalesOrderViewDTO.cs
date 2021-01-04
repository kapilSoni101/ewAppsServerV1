using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.BusinessEntity.DTO {

    public class CustBASalesOrderViewDTO
  {
        public string ERPSalesOrderKey {
            get; set;
        }

        public string ERPConnectorKey {
            get; set;
        }

        public string ERPCustomerKey {
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


        public string LocalCurrency {
            get; set;
        }

        public int Status {
            get; set;
        }


        public string StatusText {
            get; set;
        }

        public DateTime PostingDate {
            get; set;
        }

        public DateTime DeliveryDate {
            get; set;
        }

        public DateTime DocumentDate {
            get; set;
        }

        public string PickAndPackRemarks {
            get; set;
        }

        public string SalesEmployee {
            get; set;
        }


        public string Owner {
            get; set;
        }

        public decimal TotalBeforeDiscount {
            get; set;
        }

        public decimal Discount {
            get; set;
        }

        public decimal Freight {
            get; set;
        }

        public decimal Tax {
            get; set;
        }

        public decimal TotalPaymentDue {
            get; set;
        }

        public string Remarks {
            get; set;
        }

        public string ShipFromAddress {
            get; set;
        }

        public string ShipFromAddressKey {
            get; set;
        }

        public string ERPShipToAddressKey {
            get; set;
        }

        public string ShipToAddress {
            get; set;
        }

        public string ERPBillToAddressKey {
            get; set;
        }

        public string BillToAddress {
            get; set;
        }

        public int ShippingType {
            get; set;
        }

        public string ShippingTypeText {
            get; set;
        }

        public string ERPDocNum {
            get; set;
        }


        [NotMapped]
        public List<BusBASalesOrderItemDTO> SalesOrderItemList {
            get; set;
        }

        /// <summary>
        /// sales order item list.
        /// </summary>
        [NotMapped]
        public List<CustBASalesOrderAttachmentDTO> AttachmentList {
            get; set;
        }

    }
}
