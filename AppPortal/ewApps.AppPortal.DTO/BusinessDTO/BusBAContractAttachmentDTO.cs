using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO
{

  /// <summary>
  /// 
  /// </summary>
  public class BusBAContractAttachmentDTO {

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
    }
}
