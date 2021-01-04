using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {

  public class CustBAItemMasterViewDTO {

    /// <summary>
    /// 
    /// </summary>
    public string ERPConnectorKey {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string ERPItemKey {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string ItemType {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string ItemName {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string BarCode {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public decimal Price {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public int PriceUnit {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string PriceUniText {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int ShippingType {
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
    public string ManagedItem {
      get; set;
    }

    /// <summary>
    /// Application active flag.
    /// </summary>
    public string Active {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal PurchaseLength {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int PurchaseLengthUnit {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string PurchaseLengthUnitText {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal PurchaseWidth {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int PurchaseWidthUnit {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string PurchaseWidthUnitText {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal PurchaseHeight {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int PurchaseHeightUnit {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string PurchaseHeightUnitText {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal PurchaseVolume {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int PurchaseVolumeUnit {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string PurchaseVolumeUnitText {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal PurchaseWeight {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int PurchaseWeightUnit {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string PurchaseWeightUnitText {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal SalesLength {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public int SalesLengthUnit {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string SalesLengthUnitText {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal SalesWidth {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int SalesWidthUnit {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string SalesWidthUnitText {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal SalesHeight {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int SalesHeightUnit {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string SalesHeightUnitText {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal SalesVolume {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public int SalesVolumeUnit {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string SalesVolumeUnitText {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public decimal SalesWeight {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public int SalesWeightUnit {
      get; set;
    }

    /// <summary>
    ///
    /// </summary>
    public string SalesWeightUnitText {
      get; set;
    }

    /// <summary>
    ///
    /// </summary>
    public string Remarks {
      get; set;
    }


        public DateTime UpdatedOn {
            get; set;
        }

        public string UpdatedByName {
            get; set;
        }

        [NotMapped]
    public List<NotesViewDTO> NotesList
    {
      get; set;
    }
        [NotMapped]
        public List<CustBAItemAttachmentDTO > AttachmentList {
            get; set;
        }

    }
}
