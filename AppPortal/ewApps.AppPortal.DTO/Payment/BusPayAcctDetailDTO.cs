using System.Collections.Generic;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
  public class BusPayAcctDetailDTO:BaseDTO {

    public BusPayAcctDetailDTO() {
      BusVCACHPayAttr = new BusVCACHPayAttrDTO();
      VCCreditCardPayAttrList = new List<VCCreditCardPayAttrDTO>();
    }

    public BusVCACHPayAttrDTO BusVCACHPayAttr {
      get; set;
    }

    public List<VCCreditCardPayAttrDTO> VCCreditCardPayAttrList {
      get; set;
    }
  }
}