using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {

    public class BAARInvoiceAttachmentSyncDTO {

        public Guid ID {
            get; set;
        }

        public string ERPConnectorKey {
            get; set;
        }


        public string ERPARInvoiceAttachmentKey {
            get; set;
        }

        public Guid ARInvoiceId {
            get; set;
        }


        public string ERPARInvoiceKey {
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
        public static BAARInvoiceAttachment MapToEntity(BAARInvoiceAttachmentSyncDTO model) {
            BAARInvoiceAttachment entity = new BAARInvoiceAttachment();
            entity.ERPConnectorKey = model.ERPConnectorKey;
            entity.ERPARInvoiceAttachmentKey = model.ERPARInvoiceAttachmentKey;
            entity.ERPARInvoiceKey = model.ERPARInvoiceKey;
            entity.ARInvoiceId = model.ARInvoiceId;
            entity.FreeText = model.FreeText;
            entity.Name = model.Name;
            entity.AttachmentDate = model.AttachmentDate;

            return entity;
        }
    }
}
