using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.BusinessEntity.DTO {
    public class BAPurchaseOrderViewDTO {

        public string ERPPurchaseOrderKey {
            get; set;
        }

        public string ERPConnectorKey {
            get; set;
        }

        public string ERPVendorKey {
            get; set;
        }

        public string VendorName {
            get; set;
        }


        public string ContactPerson {
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

        //public string SalesEmployee {
        //    get; set;
        //}


        //public string Owner {
        //    get; set;
        //}

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

        [NotMapped]
        public List<BAPurchaseOrderItemDTO> PurchaseOrderItemList {
            get; set;
        }

        public DateTime CreatedOn {
            get; set;
        }


        public DateTime UpdatedOn {
            get; set;
        }

        public string ERPDocNum {
            get; set;
        }

        [NotMapped]
        public List<BAPurchaseOrderAttachmentDTO> AttachmentList {
            get; set;
        }

  }
}
