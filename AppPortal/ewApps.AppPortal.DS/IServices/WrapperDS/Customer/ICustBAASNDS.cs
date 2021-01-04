using ewApps.AppPortal.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.AppPortal.DS
{
  public interface ICustBAASNDS
  {
    Task<IEnumerable<CustBAASNDTO>> GetASNListByBusinessTenantIdAsyncForCust(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken));

    Task<CustBAASNViewDTO> GetASNDetailByASNIdAsyncForCust(Guid asnId, CancellationToken cancellationToken = default(CancellationToken));


  }
}
