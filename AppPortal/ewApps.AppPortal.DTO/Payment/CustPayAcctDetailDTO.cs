using System.Collections.Generic;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
  public class CustPayAcctDetailDTO:BaseDTO {

    public CustPayAcctDetailDTO() {
      CustVCACHPayAttrList = new List<CustVCACHPayAttrDTO>();
      VCCreditCardPayAttrList = new List<CreditCardDetailDTO>();
    }

    public List<CustVCACHPayAttrDTO> CustVCACHPayAttrList {
      get; set;
    }
    public List<CreditCardDetailDTO> VCCreditCardPayAttrList {
      get; set;
    }
  }
}
