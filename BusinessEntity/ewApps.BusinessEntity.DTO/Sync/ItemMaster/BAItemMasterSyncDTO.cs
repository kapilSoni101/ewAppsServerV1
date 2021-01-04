using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;
namespace ewApps.BusinessEntity.DTO
{

  /// <summary>
  /// 
  /// </summary>
  public class BAItemMasterSyncDTO
  {

    /// <summary>
    /// 
    /// </summary>
    public string ERPConnectorKey
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string ERPItemKey
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string ItemType
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string ItemName
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string BarCode
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public decimal Price
    {
      get; set;
    }

    public decimal PriceFC
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int PriceUnit
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string PriceUniText
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int ShippingType
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string ShippingTypeText
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string ManagedItem
    {
      get; set;
    }

    /// <summary>
    /// Application active flag.
    /// </summary>
    public string Active
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal PurchaseLength
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int PurchaseLengthUnit
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public string PurchaseLengthUnitText
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal PurchaseWidth
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int PurchaseWidthUnit
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string PurchaseWidthUnitText
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal PurchaseHeight
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int PurchaseHeightUnit
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string PurchaseHeightUnitText
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal PurchaseVolume
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int PurchaseVolumeUnit
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string PurchaseVolumeUnitText
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal PurchaseWeight
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int PurchaseWeightUnit
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string PurchaseWeightUnitText
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal SalesLength
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public int SalesLengthUnit
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string SalesLengthUnitText
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal SalesWidth
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int SalesWidthUnit
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string SalesWidthUnitText
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal SalesHeight
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public int SalesHeightUnit
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string SalesHeightUnitText
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public decimal SalesVolume
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public int SalesVolumeUnit
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public string SalesVolumeUnitText
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public decimal SalesWeight
    {
      get; set;
    }
    /// <summary>
    /// 
    /// </summary>
    public int SalesWeightUnit
    {
      get; set;
    }

    /// <summary>
    ///
    /// </summary>
    public string SalesWeightUnitText
    {
      get; set;
    }

    /// <summary>
    ///
    /// </summary>
    public string Remarks
    {
      get; set;
    }

    public string OpType
    {
      get; set;
    }

    /// <summary>
    /// sales order AttachmentList list.
    /// </summary>
    public List<BAItemMasterAttachmentSyncDTO> Attachments
    {
      get; set;
    }

    /// <summary>
    /// Maps model properties to entity.
    /// </summary>
    /// <param name="model">model with all required properties.</param>
    /// <returns>BAItemMaster entity</returns>
    public static BAItemMaster MapToEntity(BAItemMasterSyncDTO model)
    {
      if (model.Active.Equals("Y"))
      {
        model.Active = "Active";
      }
      else
      {
        model.Active = "Inactive";
      }
      BAItemMaster entity = new BAItemMaster()
      {
        Active = model.Active,
        BarCode = model.BarCode,
        ERPConnectorKey = model.ERPConnectorKey,
        ERPItemKey = model.ERPItemKey,
        ItemName = model.ItemName,
        ItemType = model.ItemType,
        ManagedItem = model.ManagedItem,
        Price = model.Price,
        PriceFC = model.Price,
        PriceUnit = 0,
        PriceUniText = "",
        PurchaseHeight = model.PurchaseHeight,
        PurchaseHeightUnit = model.PurchaseHeightUnit,
        PurchaseHeightUnitText = model.PurchaseHeightUnitText,
        PurchaseLength = model.PurchaseLength,
        PurchaseLengthUnit = model.PurchaseLengthUnit,
        PurchaseLengthUnitText = model.PurchaseLengthUnitText,
        PurchaseVolume = model.PurchaseVolume,
        PurchaseVolumeUnit = model.PurchaseVolumeUnit,
        PurchaseVolumeUnitText = model.PurchaseVolumeUnitText,
        PurchaseWeight = model.PurchaseWeight,
        PurchaseWeightUnit = model.PurchaseWeightUnit,
        PurchaseWeightUnitText = model.PurchaseWeightUnitText,
        PurchaseWidth = model.PurchaseWidth,
        PurchaseWidthUnit = model.PurchaseWidthUnit,
        PurchaseWidthUnitText = model.PurchaseWidthUnitText,
        Remarks = model.Remarks,
        SalesHeight = model.SalesHeight,
        SalesHeightUnit = model.SalesHeightUnit,
        SalesHeightUnitText = model.SalesHeightUnitText,
        SalesLength = model.SalesLength,
        SalesLengthUnit = model.SalesLengthUnit,
        SalesLengthUnitText = model.SalesLengthUnitText,
        SalesVolume = model.SalesVolume,
        SalesVolumeUnit = model.SalesVolumeUnit,
        SalesVolumeUnitText = model.SalesVolumeUnitText,
        SalesWeight = model.SalesWeight,
        SalesWeightUnit = model.SalesWeightUnit,
        SalesWeightUnitText = model.SalesWeightUnitText,
        SalesWidth = model.SalesWidth,
        SalesWidthUnit = model.SalesWidthUnit,
        SalesWidthUnitText = model.SalesWidthUnitText,
        ShippingType = model.ShippingType,
        ShippingTypeText = model.ShippingTypeText

      };

      return entity;
    }
    /// <summary>
    /// Maps model properties to entity.
    /// </summary>
    /// <param name="model">model with all required properties.</param>
    /// <returns>BAItemMaster entity</returns>
    public static BAItemMaster MapToEntity(BAItemMasterSyncDTO model, BAItemMaster entity)
    {
      if (model.Active.Equals("Y"))
      {
        model.Active = "Y";
      }
      else
      {
        model.Active = "N";
      }
      entity.Active = model.Active;
      entity.BarCode = model.BarCode;
      entity.ItemName = model.ItemName;
      entity.ItemType = model.ItemType;
      entity.ManagedItem = model.ManagedItem;
      entity.Price = model.Price;
      entity.PriceFC = model.Price;
      entity.PriceUnit = 0;
      entity.PriceUniText = "";
      entity.PurchaseHeight = model.PurchaseHeight;
      entity.PurchaseHeightUnit = model.PurchaseHeightUnit;
      entity.PurchaseHeightUnitText = model.PurchaseHeightUnitText;
      entity.PurchaseLength = model.PurchaseLength;
      entity.PurchaseLengthUnit = model.PurchaseLengthUnit;
      entity.PurchaseLengthUnitText = model.PurchaseLengthUnitText;
      entity.PurchaseVolume = model.PurchaseVolume;
      entity.PurchaseVolumeUnit = model.PurchaseVolumeUnit;
      entity.PurchaseVolumeUnitText = model.PurchaseVolumeUnitText;
      entity.PurchaseWeight = model.PurchaseWeight;
      entity.PurchaseWeightUnit = model.PurchaseWeightUnit;
      entity.PurchaseWeightUnitText = model.PurchaseWeightUnitText;
      entity.PurchaseWidth = model.PurchaseWidth;
      entity.PurchaseWidthUnit = model.PurchaseWidthUnit;
      entity.PurchaseWidthUnitText = model.PurchaseWidthUnitText;
      entity.Remarks = model.Remarks;
      entity.SalesHeight = model.SalesHeight;
      entity.SalesHeightUnit = model.SalesHeightUnit;
      entity.SalesHeightUnitText = model.SalesHeightUnitText;
      entity.SalesLength = model.SalesLength;
      entity.SalesLengthUnit = model.SalesLengthUnit;
      entity.SalesLengthUnitText = model.SalesLengthUnitText;
      entity.SalesVolume = model.SalesVolume;
      entity.SalesVolumeUnit = model.SalesVolumeUnit;
      entity.SalesVolumeUnitText = model.SalesVolumeUnitText;
      entity.SalesWeight = model.SalesWeight;
      entity.SalesWeightUnit = model.SalesWeightUnit;
      entity.SalesWeightUnitText = model.SalesWeightUnitText;
      entity.SalesWidth = model.SalesWidth;
      entity.SalesWidthUnit = model.SalesWidthUnit;
      entity.SalesWidthUnitText = model.SalesWidthUnitText;
      entity.ShippingType = model.ShippingType;
      entity.ShippingTypeText = model.ShippingTypeText;

      return entity;
    }

  }
}
