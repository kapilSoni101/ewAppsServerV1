/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 16 August 2019
 * 
 * Contributor/s: Sanjeev Khanna
 * Last Updated On: 16 August 2019
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
    /// Report controller expose all Report(Publisher) related APIs, It allow add/update/delete operation on Report entity.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PubReportController:ControllerBase {

        #region Local Member

        IQApplicationReportDS _appRptDS;
        IQAppUserReportDS _appUserRptDS;
        IQSupportTicketReportDS _ticketRptDS;
        IQTenantReportDS _tenantRptDS;
        IQSubcriptionReportDS _subsRptDS;


        #endregion Local Member

        #region Constructor

        public PubReportController(
        IQApplicationReportDS appRptDS,
        IQAppUserReportDS appUserRptDS,
        IQSupportTicketReportDS ticketRptDS,
        IQTenantReportDS tenantRptDS,
        IQSubcriptionReportDS subsRptDS
        ) {
            _appRptDS = appRptDS;
            _appUserRptDS = appUserRptDS;
            _ticketRptDS = ticketRptDS;
            _tenantRptDS = tenantRptDS;
            _subsRptDS = subsRptDS;
        }

        #endregion Constructor

        #region Get

        [Route("applications")]
        [HttpPut]
        public async Task<List<ApplicationReportDTO>> GetApplicationListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _appRptDS.GetApplicationListAsync(filter, token);
        }

        [Route("tenants")]
        [HttpPut]
        public async Task<List<TenantReportDTO>> GetTenantListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _tenantRptDS.GetTenantListAsync(filter, token);
        }

        [Route("subscriptions")]
        [HttpPut]
        public async Task<List<SubscriptionReportDTO>> GetSubscriptionListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _subsRptDS.GetSubscriptionListAsync(filter, token);
        }

        [Route("appusers")]
        [HttpPut]
        public async Task<List<PubAppUserReportDTO>> GetAllPubAppUserListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _appUserRptDS.GetAllPubAppUserListByUserTypeAsync(filter, token);
        }

        [Route("tickets")]
        [HttpPut]
        public async Task<List<PubSupportTicketReportDTO>> GetPubSupportTicketListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _ticketRptDS.GetPubSupportTicketListAsync(filter, token);
        }

        [Route("applicationnames/tenants/{tenantId}")]
        [HttpGet]
        public async Task<List<NameDTO>> GetApplicaitionNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _appRptDS.GetApplicaitionNameListByTenantIdAsync(tenantId, token);
        }

        [Route("subscriptionnames/tenants/{tenantId}")]
        [HttpGet]
        public async Task<List<NameDTO>> GetSubscriptionNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _subsRptDS.GetSubscriptionNameListByTenantIdAsync(tenantId, token);
        }

        [Route("usersnames/tenants/{tenantId}")]
        [HttpGet]
        public async Task<List<NameDTO>> GetUserNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _appUserRptDS.GetUserNameListByTenantIdAsync(tenantId, token);
        }

        [Route("servicesnames/tenants/{tenantId}")]
        [HttpGet]
        public async Task<List<NameDTO>> GetServiceNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _tenantRptDS.GetServiceNameListByTenantIdAsync(tenantId, token);
        }

        [Route("tenantnames/applications/{appId}/{tenantId}")]
        [HttpGet]
        public async Task<List<NameDTO>> GetTenantNameListByAppIdAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _tenantRptDS.GetTenantNameListByAppIdAsync(appId, tenantId, token);
        }

        [Route("subscriptionnames/applications/{appId}")]
        [HttpGet]
        public async Task<List<NameDTO>> GetSubscriptionNameListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            return await _subsRptDS.GetSubscriptionNameListByAppIdAsync(appId, token);
        }

        [Route("servicesnames/applications/{appId}")]
        [HttpGet]
        public async Task<List<NameDTO>> GetServiceNameListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            return await _tenantRptDS.GetServiceNameListByAppIdAsync(appId, token);
        }

        [Route("tenantnames/subscriptions/{subscriptionPlanId}")]
        [HttpGet]
        public async Task<List<NameDTO>> GetTenantNameListBySubscriptionPlanIdAsync(Guid subscriptionPlanId, CancellationToken token = default(CancellationToken)) {
            return await _tenantRptDS.GetTenantNameListBySubscriptionPlanIdAsync(subscriptionPlanId, token);
        }

        #endregion Get
    }
}