using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {

    public class BASalesQuotationAttachmentSyncDTO {
       
        public string ERPConnectorKey {
            get; set;
        }


        public string ERPSalesQuotationAttachmentKey {
            get; set;
        }

        public Guid SalesQuotationId {
            get; set;
        }


        public string ERPSalesQuotationKey {
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
        public static BASalesQuotationAttachment MapToEntity(BASalesQuotationAttachmentSyncDTO model) {
            BASalesQuotationAttachment entity = new BASalesQuotationAttachment();
            entity.ERPConnectorKey = model.ERPConnectorKey;
            entity.ERPSalesQuotationKey = model.ERPSalesQuotationKey;
            entity.ERPSalesQuotationAttachmentKey = model.ERPSalesQuotationAttachmentKey;
            entity.SalesQuotationId = model.SalesQuotationId;
            entity.FreeText = model.FreeText;
            entity.Name = model.Name;
            entity.AttachmentDate = model.AttachmentDate;

            return entity;
        }
    }
}
