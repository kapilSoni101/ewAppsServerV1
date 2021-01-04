using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS
{
 public  interface IVendorSignUpDS
  {
    Task<VendorSignUpResDTO> VendorSignUpAsync(List<VendorSignUpReqDTO> vendorSignUpDTOs, CancellationToken token = default(CancellationToken));
  }
}
