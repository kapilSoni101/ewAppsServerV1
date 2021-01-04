using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {

    public class BAAPInvoiceAttachmentSyncDTO {

        public Guid ID {
            get; set;
        }

        public string ERPConnectorKey {
            get; set;
        }


        public string ERPAPInvoiceAttachmentKey {
            get; set;
        }

        public Guid APInvoiceId {
            get; set;
        }


        public string ERPAPInvoiceKey {
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
        public static BAAPInvoiceAttachment MapToEntity(BAAPInvoiceAttachmentSyncDTO model) {
            BAAPInvoiceAttachment entity = new BAAPInvoiceAttachment();
            entity.ERPConnectorKey = model.ERPConnectorKey;
            entity.ERPAPInvoiceAttachmentKey = model.ERPAPInvoiceAttachmentKey;
            entity.ERPAPInvoiceKey = model.ERPAPInvoiceKey;
            entity.APInvoiceId = model.APInvoiceId;
            entity.FreeText = model.FreeText;
            entity.Name = model.Name;
            entity.AttachmentDate = model.AttachmentDate;

            return entity;
        }
    }
}
