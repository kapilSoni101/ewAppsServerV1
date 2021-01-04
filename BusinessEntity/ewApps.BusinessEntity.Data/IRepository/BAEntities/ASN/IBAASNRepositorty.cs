/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Sourabh Agrawal<sagrawal @eworkplaceapps.com>
 * Date:05 september 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Data {
   public interface IBAASNRepositorty:IBaseRepository<BAASN> {

        /// <summary>
        /// Gets the asn list by business tenant id and provided filter.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id.</param>
        /// <param name="listDateFilterDTO">The list date filter dto.</param>
        /// <returns>Returns list of ASN list that matches provided </returns>
        IQueryable<BusBAASNDTO> GetASNListByBusinessTenantId(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, string asnType);

        /// <summary>
        /// Gets the asn detail by asn identifier.
        /// </summary>
        /// <param name="asnId">The asn identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns ASN detail with line items that matches provided asn id.</returns>
        Task<BusBAASNViewDTO> GetASNDetailByASNIdAsync(Guid asnId, string asnType, CancellationToken cancellationToken = default(CancellationToken));

        IQueryable<CustBAASNDTO> GetASNListByBusinessTenantIdForCust(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO);

        /// <summary>
        /// Gets the cust asn detail by asn identifier.
        /// </summary>
        /// <param name="asnId">The asn identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns ASN detail with line items that matches provided asn id.</returns>
        Task<CustBAASNViewDTO> GetASNDetailByASNIdAsyncForCust(Guid asnId, CancellationToken cancellationToken = default(CancellationToken));
  }
}
