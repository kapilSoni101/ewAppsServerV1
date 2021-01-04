using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.UserSessionService;
using ewApps.Payment.DS;
using ewApps.Payment.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.Payment.WebApi.Controllers {

    /// <summary>
    /// APInvoice class is used to get invoice list.
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]    
    public class VendorInvoiceController:ControllerBase {

        #region Local Variables

        IVendorPaymentAndInvoiceDS _paInvoiceDS;
        IUserSessionManager _sessionManager;

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Initlize local variables.
        /// </summary>
        /// <param name="paInvoiceDS">Queried invoice service.</param>
        /// <param name="sessionManager">Session manager.</param>
        public VendorInvoiceController(IVendorPaymentAndInvoiceDS paInvoiceDS, IUserSessionManager sessionManager) {
            _paInvoiceDS = paInvoiceDS;
            _sessionManager = sessionManager;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get invoice by id.
        /// </summary>
        /// <param name="invoiceId">invoiceId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{invoiceId}")]
        public async Task<BAAPInvoiceViewDTO> GetInvoicesByInvoiceIdAsync([FromRoute]Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _paInvoiceDS.GetInvoiceByInvoiceIdAsync(invoiceId, token);
        }

        /// <summary>
        /// Get invoice by id.
        /// </summary>
        /// <param name="invoiceId">invoiceId</param>
        /// <param name="erpARInvoiceKey">ERPARInvoiceKey</param>
        /// <param name="integratedMode">Application running in integration mode.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getinvoiceandsync/{invoiceId}/{erpARInvoiceKey}")]
        public async Task<BAAPInvoiceViewDTO> GetInvoicesByInvoiceIdAsync([FromRoute]Guid invoiceId, [FromRoute] string erpARInvoiceKey, [FromRoute]bool integratedMode, CancellationToken token = default(CancellationToken)) {
            return await _paInvoiceDS.GetInvoiceByInvoiceIdAsync(invoiceId, token);
        }

        /// <summary>
        /// Get invoice list by tenant id.
        /// </summary>
        /// <param name="filter">Filter object to filter invoice by date.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("allinvoices")]
        public async Task<List<BAAPInvoiceDTO>> GetInvoiceByTenantAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {            
            return await _paInvoiceDS.GetInvoiceByTenantAsync(filter, token);
        }
    /// <summary>
    /// Get customer invoices by customer tenantid and filter by date.
    /// </summary>
    /// <param name="filter">Contains filter criteria.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("vendorinvoices")]
    public async Task<List<BAAPInvoiceDTO>> GetInvoiceByVendorAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken))
    {
      return await _paInvoiceDS.GetInvoiceByVendorAsync(filter, token);
    }


    /// <summary>
    /// Get invoice list by invoiceIds.
    /// </summary>
    /// <param name="invoiceId">Ids of invoice.</param>
    /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
    /// <returns></returns>
    [HttpPut]
        [Route("getbaarinvoicesbyids")]
        public async Task<List<BAAPInvoiceDTO>> GetBAAPInvoiceDTOByInvoiceIdAsync([FromBody]List<Guid> invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _paInvoiceDS.GetBAAPInvoiceDTOListByInvoiceIdAsync(invoiceId, token);
        }

        /// <summary>
        /// Get invoice list by tenant id.
        /// </summary>
        /// <param name="filter">Filter object to filter invoice by date.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("allcancelinvoices")]
        public async Task<List<BAAPInvoiceDTO>> GetCancelInvoiceByTenantAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _paInvoiceDS.GetCancelInvoiceByTenantAsync(filter, token);
        }


        /// <summary>
        /// Get quick pay invoice list.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("allquickpayinvoices")]
        public async Task<List<BAAPInvoiceDTO>> GetQuickPayInvoicesByTenantAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _paInvoiceDS.GetQuickPayInvoicesByTenantAsync(filter, token);
        }

        /// <summary>
        /// Get customer payble invoices list.
        /// </summary>
        /// <param name="vendorId">vendor id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("paybleinvoices/{vendorId}")]
        public async Task<List<BAAPInvoiceDTO>> GetPaybleInvoiceByVendorAsync(Guid vendorId, CancellationToken token = default(CancellationToken)) {
            return await _paInvoiceDS.GetVendorPaybleinvoicesByVendorIdAsync(vendorId, token);
        }

        #endregion Get

    }
}
