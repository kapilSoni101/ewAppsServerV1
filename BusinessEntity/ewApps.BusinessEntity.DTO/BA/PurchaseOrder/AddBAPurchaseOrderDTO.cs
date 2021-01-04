using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.DMService;

namespace ewApps.BusinessEntity.DTO {   

    public class AddBAPurchaseOrderDTO {

        /// <summary>
        /// Represents the ERPSalesOrderKey.
        /// </summary>
        public string ERPPurchaseOrderKey {
            get; set;
        }

        /// <summary>
        /// Represents the ERPCustomerKey.
        /// </summary>
        public string ERPVendorKey
    {
            get; set;
        }

        /// <summary>
        /// Represents the Customer Id.
        /// </summary>
        public Guid VendorID
    {
            get; set;
        }

        /// <summary>
        /// Represents the customer name.
        /// </summary>
        public string VendorName {
            get; set;
        }

        /// <summary>
        /// Represents the contact person name of customer.
        /// </summary>
        public string ContactPerson {
            get; set;
        }

        /// <summary>
        /// Represents the customer ref no.
        /// </summary>
        public string VendorRefNo
    {
            get; set;
        }

        /// <summary>
        /// Represents the LocalCurrency.
        /// </summary> 
        public string LocalCurrency {
            get; set;
        }

        /// <summary>
        /// Represents the status.
        /// </summary>
        public int Status {
            get; set;
        }

        /// <summary>
        /// Represents the status text.
        /// </summary>
        public string StatusText {
            get; set;
        }

        /// <summary>
        /// Represents the posting date of purchase inquiry.
        /// </summary>
        public DateTime PostingDate {
            get; set;
        }

        /// <summary>
        /// Represents the delivery date of purchase inquiry.
        /// </summary>
        public DateTime DeliveryDate {
            get; set;
        }

        /// <summary>
        /// Represents the document date
        /// </summary>
        public DateTime DocumentDate {
            get; set;
        }

        /// <summary>
        /// Represents the PickAndPackRemarks
        /// </summary>
        public string PickAndPackRemarks {
            get; set;
        }

        /// <summary>
        /// Represents the SalesEmployee
        /// </summary>
        public string SalesEmployee {
            get; set;
        }
        /// <summary>
        /// Represents the owner
        /// </summary>
        public string Owner {
            get; set;
        }

        /// <summary>
        /// Represents the TotalBeforeDiscount
        /// </summary>
        public decimal TotalBeforeDiscount {
            get; set;
        }

        /// <summary>
        /// Represents the Discount
        /// </summary>
        public decimal Discount {
            get; set;
        }

        /// <summary>
        /// Represents the Freight
        /// </summary>
        public decimal Freight {
            get; set;
        }

        /// <summary>
        /// Represents the Tax
        /// </summary>
        public decimal Tax {
            get; set;
        }

        /// <summary>
        /// Represents the TotalPaymentDue
        /// </summary>
        public decimal TotalPaymentDue {
            get; set;
        }

        /// <summary>
        /// Represents the Remarks
        /// </summary>
        public string Remarks {
            get; set;
        }

        /// <summary>
        /// Represents the ShipFromAddress
        /// </summary>
        public string ShipFromAddress {
            get; set;
        }

        /// <summary>
        /// Represents the ShipFromAddressKey
        /// </summary>
        public string ShipFromAddressKey {
            get; set;
        }

        /// <summary>
        /// Represents the ERPShipToAddressKey
        /// </summary>
        public string ERPShipToAddressKey {
            get; set;
        }

        /// <summary>
        /// Represents the ShipToAddress
        /// </summary>
        public string ShipToAddress {
            get; set;
        }

        /// <summary>
        /// Represents the ERPBillToAddressKey
        /// </summary>
        public string ERPBillToAddressKey {
            get; set;
        }

        /// <summary>
        /// Represents the BillToAddress
        /// </summary>
        public string BillToAddress {
            get; set;
        }

        /// <summary>
        /// Represents the ShippingType
        /// </summary>
        public int ShippingType {
            get; set;
        }

        /// <summary>
        /// Purcahse order item list.
        /// </summary>
        public List<BAPurchaseOrderItemDTO> OrderItemList {
            get;set;
        }

        [NotMapped]
        public List<AddUpdateDocumentModel> listAttachment {
            get; set;
        }

        /// <summary>
        /// Represents the ShippingText
        /// </summary>
        public string ShippingText {
            get; set;
        }

        /// <summary>
        /// Map properties to Purchase Order entity.
        /// </summary>
        /// <param name="entity">BAPurchaseOrder database entity.</param>
        public void MapPropertiesToEntity(BAPurchaseOrder entity) {
            entity.BillToAddress = this.BillToAddress;
            entity.ContactPerson = this.ContactPerson;
            entity.VendorId = this.VendorID;
            entity.VendorName = this.VendorName;
            entity.VendorRefNo = this.VendorRefNo;
            entity.DeliveryDate = this.DeliveryDate;
            entity.Discount = this.Discount;
            entity.DocumentDate = this.DocumentDate;
            entity.ERPBillToAddressKey = this.ERPBillToAddressKey;
            //entity.ERPConnectorKey = this.ERPConnectorKey;
            entity.ERPVendorKey = this.ERPVendorKey;
            entity.ERPPurchaseOrderKey = this.ERPPurchaseOrderKey;
            entity.ERPShipToAddressKey = this.ERPShipToAddressKey;
            entity.Freight = this.Freight;
            entity.LocalCurrency = this.LocalCurrency;
            entity.Owner = this.Owner;
            entity.PickAndPackRemarks = this.PickAndPackRemarks;
            entity.PostingDate = this.PostingDate;
            entity.Remarks = this.Remarks;
            entity.SalesEmployee = this.SalesEmployee;
            entity.ShipFromAddress = this.ShipFromAddress;
            entity.ShipFromAddressKey = this.ShipFromAddressKey;
            entity.ShippingTypeText = this.ShippingText;
            entity.ShippingType = this.ShippingType;
            entity.ShipToAddress = this.ShipToAddress;
            entity.Status = this.Status;
            entity.StatusText = this.StatusText;
            entity.Tax = this.Tax;
            entity.TotalBeforeDiscount = this.TotalBeforeDiscount;
            entity.TotalPaymentDue = this.TotalPaymentDue;            
        }

    }

}
