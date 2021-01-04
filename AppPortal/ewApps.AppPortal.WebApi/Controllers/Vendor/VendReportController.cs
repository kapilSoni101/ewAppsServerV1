/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 6 February 2020
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 6 February 2020
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Report.DTO;
using ewApps.Report.QDS;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi.Controllers.Vendor {
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    ///Vend Report controller expose all Vend Report related APIs, It allow add/update/delete operation on Vend Report.
    /// </summary>
    public class VendReportController : ControllerBase
    {

        #region Local Variable

        IQSupportTicketReportDS _ticketRptDS;
        IQInvoiceReportDS _invoiceRptDS;        
        IQSalesOrdersReportDS _salesOrdersReportDS;      
        IQItemMasterReportDS _itemMasterReportDS;
        #endregion

        #region Constructor
        public VendReportController(     
       IQSupportTicketReportDS ticketRptDS,
       IQInvoiceReportDS invoiceRptDS,       
        IQSalesOrdersReportDS salesOrdersReportDS,        
        IQItemMasterReportDS itemMasterReportDS
       ) {
            
            _ticketRptDS = ticketRptDS;
            _invoiceRptDS = invoiceRptDS;           
            _salesOrdersReportDS = salesOrdersReportDS;           
            _itemMasterReportDS = itemMasterReportDS;
        }
        #endregion

       
    }
}