using System;
using System.Collections.Generic;
using System.Text;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO
{

  /// <summary>
  /// 
  /// </summary>
  public class BAContractAttachmentSyncDTO {
       
        public Guid ID {
            get; set;
        }

        public string ERPConnectorKey {
            get; set;
        }
        
        public string ERPContractAttachmentKey {
            get; set;
        }

        public Guid ContractId {
            get; set;
        }

        
        public string ERPContractKey {
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
        public static BAContractAttachment MapToEntity(BAContractAttachmentSyncDTO model) {
            BAContractAttachment entity = new BAContractAttachment();
            entity.ERPConnectorKey = model.ERPConnectorKey;
            entity.ERPContractKey = model.ERPContractKey;
            entity.ERPContractAttachmentKey = model.ERPContractAttachmentKey;
            entity.ContractId = model.ContractId;
            entity.FreeText = model.FreeText;
            entity.Name = model.Name;
            entity.AttachmentDate = model.AttachmentDate;

            return entity;
        }
    }
}
