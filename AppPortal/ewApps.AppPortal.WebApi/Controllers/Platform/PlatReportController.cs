/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 13 August 2019
 * 
 * Contributor/s: Sanjeev Khanna
 * Last Updated On: 13 August 2019
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Report.DTO;
using ewApps.Report.QDS;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

    /// <summary>
    /// PlatReport controller expose all PlatReport related APIs, It allow add/update/delete operation on PlatReport entity.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlatReportController : ControllerBase
    {
        #region Local Member

        IQApplicationReportDS _appRptDS;
        IQAppUserReportDS _appUserRptDS;
        IQSupportTicketReportDS _ticketRptDS;
        IQTenantReportDS _tenantRptDS;
        IQSubcriptionReportDS _subsRptDS;
        IQPublisherReportDS _pubRptDS;
        IQTransactionJournalReportDS _transactionJournalReportDS;


        #endregion Local Member

        #region Constructor

        public PlatReportController(
        IQApplicationReportDS appRptDS,
        IQAppUserReportDS appUserRptDS,
        IQSupportTicketReportDS ticketRptDS,
        IQTenantReportDS tenantRptDS,
        IQSubcriptionReportDS subsRptDS,
     IQPublisherReportDS pubRptDS,
    IQTransactionJournalReportDS transactionJournalReportDS
        ) {
            _appRptDS = appRptDS;
            _appUserRptDS = appUserRptDS;
            _ticketRptDS = ticketRptDS;
            _tenantRptDS = tenantRptDS;
            _subsRptDS = subsRptDS;
            _pubRptDS = pubRptDS;
            _transactionJournalReportDS = transactionJournalReportDS;
        }

        #endregion Constructor

        #region Get

        [Route("applications")]
        [HttpPut]
        public async Task<List<PlatApplicationReportDTO>> GetPFApplicationListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _appRptDS.GetPFApplicationListAsync(filter, token);
        }

        [Route("publishers")]
        [HttpPut]
        public async Task<List<PlatPublisherReportDTO>> GetPFPublisherListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _pubRptDS.GetPFPublisherListAsync(filter, token);

        }

        [Route("appusers")]
        [HttpPut]
        public async Task<List<PlatAppUserReportDTO>> GetAllPFAppUserListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _appUserRptDS.GetAllPFAppUserListByUserTypeAsync(filter, token);
        }

        [Route("tickets")]
        [HttpPut]
        public async Task<List<PlatSupportTicketReportDTO>> GetPFSupportTicketListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _ticketRptDS.GetPFSupportTicketListAsync(filter, token);
        }

        [Route("subscriptions")]
        [HttpPut]
        public async Task<List<PlatSubscriptionReportDTO>> GetPlatSubscriptionListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _subsRptDS.GetPlatSubscriptionListAsync(filter, token);
        }


        [Route("publishernames/applications/{appId}")]
        [HttpGet]
        public async Task<List<NameDTO>> GetPublisherNameListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            return await _pubRptDS.GetPublisherNameListByAppIdAsync(appId, token);
        }

        [Route("servicesnames/applications/{appId}")]
        [HttpGet]
        public async Task<List<NameDTO>> GetPFServiceNameListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            return await _tenantRptDS.GetPFServiceNameListByAppIdAsync(appId, token);
        }

        [Route("tenantnames/applications/{appId}")]
        [HttpGet]
        public async Task<List<NameDTO>> GetPFTenantNameListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            return await _tenantRptDS.GetPFTenantNameListByAppIdAsync(appId, token);
        }

        [Route("applicationnames/publishers/{publisherId}")]
        [HttpGet]
        public async Task<List<NameDTO>> GetPFApplicaitionNameListAsync(Guid publisherId, CancellationToken token = default(CancellationToken)) {
            return await _appRptDS.GetPFApplicaitionNameListAsync(publisherId, token);
        }

        [Route("usersnames/publishers/{tenantId}")]
        [HttpGet]
        public async Task<List<NameDTO>> GetPFUserNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _appUserRptDS.GetPFUserNameListByTenantIdAsync(tenantId, token);
        }

        [Route("tenantnames/publisher/{publisherId}")]
        [HttpGet]
        public async Task<List<NameDTO>> GetPFTenantNameListByPublisherIdAsync(Guid publisherId, CancellationToken token = default(CancellationToken)) {
            return await _tenantRptDS.GetPFTenantNameListByPublisherIdAsync(publisherId, token);
        }

        [Route("activetenantnames/publisher/{publisherId}")]
        [HttpGet]
        public async Task<List<NameDTO>> GetPFActiveTenantNameListByPublisherIdAsync(Guid publisherId, CancellationToken token = default(CancellationToken)) {
            return await _tenantRptDS.GetPFActiveTenantNameListByPublisherIdAsync(publisherId, token);
        }

        [Route("gettransactionjournallist")]
        [HttpPut]
        // public async Task<List<TransactionJournalReportDTO>> GetTransactionJournalListAsync(ReportFilterDTO filter) {
        public async Task<List<TransactionJournalReportDTO>> GetTransactionJournalListAsync(ListFilterDTO filter) {
            return await _transactionJournalReportDS.GetTransactionJournalListAsync(filter);
        }

        [Route("tenantsreportonplatform")]
        [HttpPut]
        public async Task<List<PlatformTenantDTO>> GetTenantReportListOnPlatformAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _tenantRptDS.GetTenantReportListOnPlatformAsync(filter, token);
        }

        /// <summary>
        /// Get vendor list for publisher.
        /// </summary>
        /// <returns></returns>
        [Route("applications/{id}")]
        [HttpGet]
        public async Task<List<NameDTO>> GetBusinessApplications(Guid id) {
            return await _appRptDS.GetBusinessAppSubscriptionInfoDTOAsync(id);
        }

        [Route("servicesnames/subscription/{planId}")]
        [HttpGet]
        public async Task<List<NameDTO>> GetPFServiceNameListByPlanIdAsync(Guid planId, CancellationToken token = default(CancellationToken)) {
            return await _tenantRptDS.GetPFServiceNameListByPlanIdAsync(planId, token);
        }



        #endregion Get


    }
}