using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.BusinessEntity.DTO {
    public class BusBAASNViewDTO {

        public Guid ID {
            get; set;
        }

        //    public ERPConnectorKey: string;
        public string ERPConnectorKey {
            get; set;
        }


        ////public ASNNo: string;
        public string ERPASNKey {
            get; set;
        }

        //public ERPCustomerKey: string;
        public string ERPCustomerKey {
            get; set;
        }

        public Guid CustomerId {
            get; set;
        }

        public string CustomerName {
            get; set;
        }

        //public DeliveryNo: string;
        public string DeliveryNo {
            get; set;
        }


        //public ShipDate: Date;
        public DateTime ShipDate {
            get; set;
        }

        //public ExpectedDate: Date;
        public DateTime ExpectedDate {
            get; set;
        }

        //public TrackingNo: string;
        public string TrackingNo {
            get; set;
        }

        //public ShipmentType: number;
        public int ShipmentType {
            get; set;
        }

        //public ShipmentTypeText: string;
        public string ShipmentTypeText {
            get; set;
        }

        //public ShipmentPlan: string;
        public string ShipmentPlan {
            get; set;
        }

        //public PackagingSlipNo: string;
        public string PackagingSlipNo {
            get; set;
        }


        //public TotalAmount: number;
        public decimal? TotalAmount {
            get; set;
        }

        //public Discount: number;
        public decimal? Discount {
            get; set;
        }

        //public Freight: number;
        public decimal? Freight {
            get; set;
        }

        //public Tax: number;
        public decimal? Tax {
            get; set;
        }

        //public ShipTo: string;
        [NotMapped]
        public string ShipTo {
            get; set;
        }

        ////public Currency: string;
        //[NotMapped]
        //public string Currency {
        //    get; set;
        //}

        public string ERPDocNum {
            get; set;
        }

        public string LocalCurrency {
            get; set;
        }

        //public ItemList: Array<ASNItemModel>;
        [NotMapped]
        public IEnumerable<BusBAASNItemDTO> ItemList {
            get; set;
        }
        [NotMapped]
        public IEnumerable<BusBAASNAttachmentDTO> AttachmentList {
            get; set;
        }
    }
}
