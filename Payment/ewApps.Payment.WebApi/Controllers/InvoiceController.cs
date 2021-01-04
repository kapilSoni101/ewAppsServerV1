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
    /// Invoice class is used to get invoice list.
    /// Add/delete invoice method.
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]    
    public class InvoiceController:ControllerBase {

        #region Local Variables

        IPaymentAndInvoiceDS _paInvoiceDS;
        IUserSessionManager _sessionManager;

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Initlize local variables.
        /// </summary>
        /// <param name="paInvoiceDS">Queried invoice service.</param>
        /// <param name="sessionManager">Session manager.</param>
        public InvoiceController(IPaymentAndInvoiceDS paInvoiceDS, IUserSessionManager sessionManager) {
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
        public async Task<BAARInvoiceViewDTO> GetInvoicesByInvoiceIdAsync([FromRoute]Guid invoiceId, CancellationToken token = default(CancellationToken)) {
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
        public async Task<BAARInvoiceViewDTO> GetInvoicesByInvoiceIdAsync([FromRoute]Guid invoiceId, [FromRoute] string erpARInvoiceKey, [FromRoute]bool integratedMode, CancellationToken token = default(CancellationToken)) {
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
        public async Task<List<BAARInvoiceDTO>> GetInvoiceByTenantAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {            
            return await _paInvoiceDS.GetInvoiceByTenantAsync(filter, token);
        }

        /// <summary>
        /// Get invoice list by invoiceIds.
        /// </summary>
        /// <param name="invoiceId">Ids of invoice.</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("getbaarinvoicesbyids")]
        public async Task<List<BAARInvoiceDTO>> GetBAARInvoiceDTOByInvoiceIdAsync([FromBody]List<Guid> invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _paInvoiceDS.GetBAARInvoiceDTOListByInvoiceIdAsync(invoiceId, token);
        }

        /// <summary>
        /// Get invoice list by tenant id.
        /// </summary>
        /// <param name="filter">Filter object to filter invoice by date.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("allcancelinvoices")]
        public async Task<List<BAARInvoiceDTO>> GetCancelInvoiceByTenantAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _paInvoiceDS.GetCancelInvoiceByTenantAsync(filter, token);
        }

        /// <summary>
        /// Get customer invoices by customer tenantid and filter by date.
        /// </summary>
        /// <param name="filter">Contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("customerinvoices")]
        public async Task<List<BAARInvoiceDTO>> GetInvoiceByCustomerAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _paInvoiceDS.GetInvoiceByCustomerAsync(filter, token);
        }

        /// <summary>
        /// Get quick pay invoice list.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("allquickpayinvoices")]
        public async Task<List<BAARInvoiceDTO>> GetQuickPayInvoicesByTenantAsync([FromBody]ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _paInvoiceDS.GetQuickPayInvoicesByTenantAsync(filter, token);
        }

        /// <summary>
        /// Get customer payble invoices list.
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("paybleinvoices/{customerId}")]
        public async Task<List<BAARInvoiceDTO>> GetPaybleInvoiceByCustomerAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _paInvoiceDS.GetCustomerPaybleinvoicesByCustomerIdAsync(customerId, token);
        }

        #endregion Get

    }
}
