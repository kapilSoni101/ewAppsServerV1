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
   public  interface IBAASNItemRepositorty:IBaseRepository<BAASNItem> {
        /// <summary>
        /// Gets the asn item list by asn id.
        /// </summary>
        /// <param name="asnId">The asn identifier.</param>
        /// <returns>Returns ASN Items list that matches provided ASN id.</returns>
        IQueryable<BusBAASNItemDTO> GetASNItemListByASNId(Guid asnId);

        IQueryable<CustBAASNItemDTO> GetASNItemListByASNIdForCust(Guid asnId);
  }
}
