using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DS;
using ewApps.BusinessEntity.DTO;
using ewApps.Core.ExceptionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.BusinessEntity.WebApi.Controllers.Business
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusCustomerController : ControllerBase
    {
        #region Local Members

        IBACustomerDS _customerDS;
        IBAARInvoiceDS _invoiceDS;

        #endregion Local Members

        #region Constructor

        /// <summary>
        /// initalize constructor.
        /// </summary>
        /// <param name="customerDS"></param>
        /// <param name="invoiceDS">Invoice Dataservice.</param>
        public BusCustomerController(IBACustomerDS customerDS, IBAARInvoiceDS invoiceDS) {
            _customerDS = customerDS;
            _invoiceDS = invoiceDS;
        }

        #endregion Constructor

        #region Put

        /// <summary>
        /// Update BAcustomer.
        /// </summary>
        /// <param name="custDTO">Customer detail object.</param>
        /// <param name="token"></param>
        /// <returns>return business detail  model.</returns>
        [Route("detail/update")]
        [HttpPut]
        public async Task<bool> UpdateCustomerDetail([FromBody]BACustomerDTO custDTO, CancellationToken token = default(CancellationToken)) {
            return await _customerDS.UpdateCustomerDetail(custDTO, token);
        }

        /// <summary>
        /// Update BAcustomer and customer contact.
        /// </summary>
        /// <param name="custDTO">Customer and contact detail object.</param>
        /// <param name="token"></param>
        /// <returns>return business detail  model.</returns>
        [Route("detail/customercontactupdate")]
        [HttpPut]
        public async Task<bool> UpdateCustomerAndCustomerContactDetail([FromBody]BusBACustomerDetailDTO custDTO, CancellationToken token = default(CancellationToken)) {
            return await _customerDS.UpdateCustomerAndContactDetailAsync(custDTO, token);
        }

        /// <summary>
        /// Update BAcustomer.
        /// </summary>
        /// <param name="custDTO">Customer detail object.</param>
        /// <param name="token"></param>
        /// <returns>return business detail  model.</returns>
        [Route("setupapp/updatecustomerDetail")]
        [HttpPut]
        public async Task<bool> UpdateCustomerDetailForBizSetupApp([FromBody]BusCustomerUpdateDTO custDTO, CancellationToken token = default(CancellationToken)) {
            return await _customerDS.UpdateCustomerDetailForBizSetupApp(custDTO, token);
        }

        #endregion Post

        #region Delete

        /// <summary>
        /// Delete BAcustomer and associtaed data.
        /// </summary>
        /// <param name="bacustomerId">BACustomerId.</param>
        /// <param name="token"></param>
        /// <returns>return business detail  model.</returns>
        [Route("delete/{bacustomerId}")]
        [HttpPut]
        public async Task<bool> DeleteCustomer([FromRoute]Guid bacustomerId, CancellationToken token = default(CancellationToken)) {
            bool isExist = await _invoiceDS.IsInvoiceExistsAsync(bacustomerId, token);
            if(isExist) {
                EwpError error = new EwpError();
                error.ErrorType = ErrorType.Validation;
                EwpErrorData errorData = new EwpErrorData();
                errorData.ErrorSubType = (int)ValidationErrorSubType.SelfReference;
                errorData.Message = Common.BusinessEntityConstants.CustomerDeleteException;
                error.EwpErrorDataList.Add(errorData);
                throw new EwpValidationException(Common.BusinessEntityConstants.CustomerDeleteException, error.EwpErrorDataList);                
            }
            await _customerDS.DeleteCustomerAsync(bacustomerId, token);
            return true;
        }

        #endregion Delete
    }
}