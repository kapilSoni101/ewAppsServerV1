using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO
{
  public class BAPurchaseOrderAttachmentSyncDTO
  {
    public string ERPConnectorKey
    {
      get; set;
    }


    public string ERPPurchaseOrderAttachmentKey
    {
      get; set;
    }

    public Guid PurchaseOrderId
    {
      get; set;
    }


    public string ERPPurchaseOrderKey
    {
      get; set;
    }

    public string Name
    {
      get; set;
    }

    public string FreeText
    {
      get; set;
    }


    public DateTime? AttachmentDate
    {
      get; set;
    }

    /// <summary>
    /// Map model object to entity object and return it.
    /// </summary>
    /// <returns></returns>
    public static BAPurchaseOrderAttachment MapToEntity(BAPurchaseOrderAttachmentSyncDTO model)
    {
      BAPurchaseOrderAttachment entity = new BAPurchaseOrderAttachment();
      entity.ERPConnectorKey = model.ERPConnectorKey;
      entity.ERPPurchaseOrderKey = model.ERPPurchaseOrderKey;
      entity.ERPPurchaseOrderAttachmentKey = model.ERPPurchaseOrderAttachmentKey;
      entity.PurchaseOrderId = model.PurchaseOrderId;
      entity.FreeText = model.FreeText;
      entity.Name = model.Name;
      entity.AttachmentDate = model.AttachmentDate;

      return entity;
    }
  }
}
