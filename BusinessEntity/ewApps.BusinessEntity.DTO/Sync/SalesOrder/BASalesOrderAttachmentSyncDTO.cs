using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {

    public class BASalesOrderAttachmentSyncDTO {

        public string ERPConnectorKey {
            get; set;
        }

        
        public string ERPSalesOrderAttachmentKey {
            get; set;
        }

        public Guid SalesOrderId {
            get; set;
        }

       
        public string ERPSalesOrderKey {
            get; set;
        }

        public string Name {
            get; set;
        }

        public string FreeText {
            get; set;
        }

        
        public DateTime? AttachmentDate {
            get; set;
        }

        /// <summary>
        /// Map model object to entity object and return it.
        /// </summary>
        /// <returns></returns>
        public static BASalesOrderAttachment MapToEntity(BASalesOrderAttachmentSyncDTO model) {
            BASalesOrderAttachment entity = new BASalesOrderAttachment();
            entity.ERPConnectorKey = model.ERPConnectorKey;
            entity.ERPSalesOrderKey = model.ERPSalesOrderKey;
            entity.ERPSalesOrderAttachmentKey = model.ERPSalesOrderAttachmentKey;
            entity.SalesOrderId = model.SalesOrderId;
            entity.FreeText = model.FreeText;
            entity.Name = model.Name;
            entity.AttachmentDate = model.AttachmentDate;

            return entity;
        }
    }
}
