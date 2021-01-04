using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {

    public class BADeliveryAttachmentSyncDTO {


        public Guid ID {
            get; set;
        }

        public string ERPConnectorKey {
            get; set;
        }

        public string ERPDeliveryAttachmentKey {
            get; set;
        }

        public Guid DeliveryId {
            get; set;
        }

        public string ERPDeliveryKey {
            get; set;
        }
        public string Name {
            get; set;
        }


        public string FreeText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? AttachmentDate {
            get; set;
        }

        /// <summary>
        /// Map model object to entity object and return it.
        /// </summary>
        /// <returns></returns>
        public static BADeliveryAttachment MapToEntity(BADeliveryAttachmentSyncDTO model) {
            BADeliveryAttachment entity = new BADeliveryAttachment();
            entity.ERPConnectorKey = model.ERPConnectorKey;
            entity.ERPDeliveryKey = model.ERPDeliveryKey;
            entity.ERPDeliveryAttachmentKey = model.ERPDeliveryAttachmentKey;
            entity.DeliveryId = model.DeliveryId;
            entity.FreeText = model.FreeText;
            entity.Name = model.Name;
            entity.AttachmentDate = model.AttachmentDate;

            return entity;
        }
    }
}
