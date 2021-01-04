using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public class PublisherUpdateDS:IPublisherUpdateDS {
        IPublisherAppSettingDS _publisherAppSettingDS;
        IPublisherDS _publisherDS;
        IUserSessionManager _userSessionManager;
        IPublisherAddressDS _publisherAddressDS;
        IEntityThumbnailDS _entityThumbnailDS;
        AppPortalAppSettings _appPortalAppSettings;
        IPubBusinessSubsPlanDS _pubBusinessSubsPlanDS;
        IPubBusinessSubsPlanAppServiceDS _pubBusinessSubsPlanAppServiceDS;
        IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherUpdateDS"/> class.
        /// </summary>
        /// <param name="entityThumbnailDS">The entity thumbnail ds.</param>
        /// <param name="publisherAddressDS">The publisher address ds.</param>
        /// <param name="userSessionManager">The user session manager.</param>
        /// <param name="publisherDS">The publisher ds.</param>
        /// <param name="publisherAppSettingDS">The publisher application setting ds.</param>
        /// <param name="pubBusinessSubsPlanDS">The pub business subs plan application service ds.</param>
        public PublisherUpdateDS(IEntityThumbnailDS entityThumbnailDS, IPublisherAddressDS publisherAddressDS, IUserSessionManager userSessionManager, IPublisherDS publisherDS, IPublisherAppSettingDS publisherAppSettingDS, IPubBusinessSubsPlanDS pubBusinessSubsPlanDS, IPubBusinessSubsPlanAppServiceDS pubBusinessSubsPlanAppServiceDS, IUnitOfWork unitOfWork, IOptions<AppPortalAppSettings> appPortalAppSettingOption) {
            _publisherAppSettingDS = publisherAppSettingDS;
            _publisherDS = publisherDS;
            _pubBusinessSubsPlanDS = pubBusinessSubsPlanDS;
            _userSessionManager = userSessionManager;
            _publisherAddressDS = publisherAddressDS;
            _entityThumbnailDS = entityThumbnailDS;
            _appPortalAppSettings = appPortalAppSettingOption.Value;
            _pubBusinessSubsPlanAppServiceDS = pubBusinessSubsPlanAppServiceDS;
            _unitOfWork = unitOfWork;
        }

        public async Task UpdatePublisherAsync(PublisherUpdateReqDTO publisherUpdateReqDTO, CancellationToken cancellationToken = default(CancellationToken)) {

            // Get publisher , usersession and tenant.
            Publisher publisher = await _publisherDS.FindAsync(i => i.ID == publisherUpdateReqDTO.Id && i.Deleted == false, cancellationToken);
            UserSession userSession = _userSessionManager.GetSession();
            if(publisher != null) {
                // get all newly added application.

                IEnumerable<Guid> newAppIds = publisherUpdateReqDTO.ApplicationList.FindAll(i => i.OpType == OperationType.Add).Select(k => k.AppID);

                //var result = publisherUpdateReqDTO.ApplicationList.SelectMany(w => w.AppSubscriptionList, (app, susPlan) => { if(susPlan.OpType == OperationType.Add) { new { susPlan.Id } });
                IEnumerable<Guid> newPlanIds = publisherUpdateReqDTO.ApplicationList.SelectMany((w, index) => w.AppSubscriptionList.FindAll(k => k.OpType == OperationType.Add)).Select(i => i.Id);

                PublisherPreUpdateReqDTO publisherPreUpdateReqDTO = new PublisherPreUpdateReqDTO();
                publisherPreUpdateReqDTO.AppIdList = newAppIds;
                publisherPreUpdateReqDTO.SuscriptionPlanIdList = newPlanIds;

                string methodUri = "tenant/publisherupdate/predata";
                List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
                headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
                RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, methodUri, publisherPreUpdateReqDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

                ServiceExecutor appServiceExecutor = new ServiceExecutor(_appPortalAppSettings.AppMgmtApiUrl);
                PublisherPreUpdateRespDTO pubAppSettingAppInfoList = await appServiceExecutor.ExecuteAsync<PublisherPreUpdateRespDTO>(requestOptions, false);

                publisher.ApplyPoweredBy = publisherUpdateReqDTO.ApplyPoweredBy;
                publisher.CanUpdateCopyright = publisherUpdateReqDTO.CanUpdateCopyright;
                publisher.ContactPersonDesignation = publisherUpdateReqDTO.ContactPersonDesignation;
                publisher.ContactPersonEmail = publisherUpdateReqDTO.ContactPersonEmail;
                publisher.ContactPersonName = publisherUpdateReqDTO.ContactPersonName;
                publisher.ContactPersonPhone = publisherUpdateReqDTO.ContactPersonPhone;
                //  publisher.Copyright = publisherUpdateReqDTO.Copyright;
                //publisher.CopyrightAccessType = publisherUpdateReqDTO.CopyrightAccessType;
                publisher.CustomizedCopyright = publisherUpdateReqDTO.CustomizedCopyright;
                publisher.InactiveComment = publisherUpdateReqDTO.InactiveComment;
                publisher.PoweredBy = publisherUpdateReqDTO.PoweredBy;
                publisher.Name = publisherUpdateReqDTO.PublisherName;
                publisher.Website = publisherUpdateReqDTO.Website;
                publisher.Active = publisherUpdateReqDTO.Active;

                if(publisher.Active != publisherUpdateReqDTO.Active) {
                    // Generate publisher status change notification.
                    //string status = string.Empty;
                    //if(publisherUpdateReqDTO.Active) {
                    //    status = StatusEnum.Active.ToString();
                    //}
                    //else {
                    //    status = StatusEnum.Inactive.ToString();
                    //}
                    // ToDo: platform copyright.
                    // await GeneratePublisherActiveInActiveNotification(userSession, publisher.TenantId, publisher.Name, publisher.Copyright, status);
                }

                await _publisherDS.UpdateAsync(publisher, publisher.ID, cancellationToken);

                // Update address of the publisher.
                //await _publisherAddressDS.AddUpdatePublisherAddressListAsync(publisherUpdateReqDTO.AddressList, publisher.TenantId, publisher.ID, cancellationToken);
                await ManagePublisherAddressList(publisherUpdateReqDTO.AddressList, publisher.TenantId, publisher.ID, cancellationToken);

                // Update Publisher AppSetting 
                foreach(PubAppSettingDTO pubAppSettingApplication in publisherUpdateReqDTO.ApplicationList) {

                    if(pubAppSettingApplication.OpType == Core.BaseService.OperationType.Add) {
                        AppInfoDTO appInfoDTO = pubAppSettingAppInfoList.ApplicationList.First(k => k.Id == pubAppSettingApplication.AppID);

                        PublisherAppSetting publisherAppSetting = new PublisherAppSetting();
                        publisherAppSetting.Active = true;
                        publisherAppSetting.AppId = pubAppSettingApplication.AppID;
                        publisherAppSetting.CopyrightsText = publisher.Copyright;
                        publisherAppSetting.Customized = false;
                        publisherAppSetting.InactiveComment = publisherUpdateReqDTO.InactiveComment;
                        publisherAppSetting.Deleted = false;
                        // publisherAppSetting.CopyrightsText = "";
                        ThumbnailAddAndUpdateDTO thumbnail = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(pubAppSettingApplication.AppID);
                        if(thumbnail != null) {
                            publisherAppSetting.ThumbnailId = (Guid)thumbnail.ID;
                        }
                        // Get theme id from app management.
                        publisherAppSetting.ThemeId = appInfoDTO.ThemeId;
                        // Get AppName from app management.
                        publisherAppSetting.Name = pubAppSettingApplication.AppName;

                        _publisherAppSettingDS.UpdateSystemFieldsByOpType(publisherAppSetting, OperationType.Add);
                        publisherAppSetting.TenantId = publisher.TenantId;
                        await _publisherAppSettingDS.AddAsync(publisherAppSetting, cancellationToken);

                        //await ManageSubscriptionPlanAndServicesAsync(pubAppSettingApplication.AppSubscriptionList, pubAppSettingAppInfoList.SubsriptionPlanInfoList, publisher.TenantId, cancellationToken);
                    }
                    else if(pubAppSettingApplication.OpType == Core.BaseService.OperationType.Delete) {
                        // Delete Publisher App Setting And Its all subscriptions
                        PublisherAppSetting publisherAppSetting = await _publisherAppSettingDS.GetByAppIdAndPublisherTenantIdAsync(pubAppSettingApplication.AppID, publisher.TenantId, cancellationToken);
                        publisherAppSetting.Deleted = true;
                        _publisherAppSettingDS.UpdateSystemFieldsByOpType(publisherAppSetting, OperationType.Update);
                        await _publisherAppSettingDS.UpdateAsync(publisherAppSetting, publisherAppSetting.ID, cancellationToken);

                        // Delete all subscription entiries
                        //await ManageSubscriptionPlanAndServicesAsync(pubAppSettingApplication.AppSubscriptionList, pubAppSettingAppInfoList.SubsriptionPlanInfoList, publisher.TenantId, cancellationToken);

                    }
                    if(pubAppSettingApplication.OpType != Core.BaseService.OperationType.None) {
                        await ManageSubscriptionPlanAndServicesAsync(pubAppSettingApplication.AppSubscriptionList, pubAppSettingAppInfoList.SuscriptionPlanServiceInfoList, publisher.TenantId, cancellationToken);
                    }

                }

                // Update Primary User Information to AppMgmt.
                TenantUpdateForPublisherDTO tenantUpdateForPublisherDTO = new TenantUpdateForPublisherDTO();
                tenantUpdateForPublisherDTO.UserId = publisherUpdateReqDTO.PrimaryUserId;
                tenantUpdateForPublisherDTO.FirstName = publisherUpdateReqDTO.PrimaryUserFirstName;
                tenantUpdateForPublisherDTO.LastName = publisherUpdateReqDTO.PrimaryUserLastName;
                tenantUpdateForPublisherDTO.FullName = publisherUpdateReqDTO.PrimaryUserFullName;
                tenantUpdateForPublisherDTO.TenantId = publisherUpdateReqDTO.TenantId;
                tenantUpdateForPublisherDTO.TenantName = publisherUpdateReqDTO.PublisherName;
                tenantUpdateForPublisherDTO.TenantActiveState = publisherUpdateReqDTO.Active;

                methodUri = "tenant/publisherupdate";
                headerParams = new List<KeyValuePair<string, string>>();
                headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
                requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, methodUri, tenantUpdateForPublisherDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

                appServiceExecutor = new ServiceExecutor(_appPortalAppSettings.AppMgmtApiUrl);
                await appServiceExecutor.ExecuteAsync<bool>(requestOptions, false);

                _unitOfWork.SaveAll();
            }

        }

        private async Task ManageSubscriptionPlanAndServicesAsync(List<SubscriptionPlanInfoDTO> subscriptionPlanInfoDTOs, List<SubsPlanServiceInfoDTO> subscriptionPlanServiceInfos, Guid publisherTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            foreach(SubscriptionPlanInfoDTO subsPlanInfo in subscriptionPlanInfoDTOs) {
                // Filter service by plan id
                List<SubsPlanServiceInfoDTO> filteredServiceList = subscriptionPlanServiceInfos.FindAll(k => k.SubscriptionPlanId == subsPlanInfo.Id);

                switch(subsPlanInfo.OpType) {
                    case OperationType.Add:
                        PubBusinessSubsPlan pubBusinessSubsPlan = new PubBusinessSubsPlan();
                        pubBusinessSubsPlan.IdentityNumber = subsPlanInfo.IdentityNumber;
                        pubBusinessSubsPlan.Active = subsPlanInfo.Active;
                        pubBusinessSubsPlan.AllowUnlimitedTransaction = subsPlanInfo.AllowUnlimitedTransaction;
                        pubBusinessSubsPlan.AppId = subsPlanInfo.AppId;
                        pubBusinessSubsPlan.AutoRenewal = subsPlanInfo.AutoRenewal;
                        pubBusinessSubsPlan.BusinessUserCount = subsPlanInfo.BusinessUserCount;
                        pubBusinessSubsPlan.CustomerUserCount = subsPlanInfo.CustomerUserCount;
                        pubBusinessSubsPlan.Deleted = false;
                        pubBusinessSubsPlan.EndDate = subsPlanInfo.EndDate;
                        pubBusinessSubsPlan.OneTimePlan = subsPlanInfo.OneTimePlan;
                        pubBusinessSubsPlan.OtherFeatures = subsPlanInfo.OtherFeatures;
                        pubBusinessSubsPlan.PaymentCycle = subsPlanInfo.PaymentCycle;
                        pubBusinessSubsPlan.PlanName = subsPlanInfo.PlanName;
                        pubBusinessSubsPlan.PriceInDollar = subsPlanInfo.PriceInDollar;
                        pubBusinessSubsPlan.ShipmentCount = subsPlanInfo.ShipmentCount;
                        pubBusinessSubsPlan.ShipmentUnit = subsPlanInfo.ShipmentUnit;
                        pubBusinessSubsPlan.StartDate = subsPlanInfo.StartDate;
                        pubBusinessSubsPlan.SubscriptionPlanId = subsPlanInfo.Id;
                        pubBusinessSubsPlan.Term = subsPlanInfo.Term;
                        pubBusinessSubsPlan.TermUnit = subsPlanInfo.TermUnit;
                        pubBusinessSubsPlan.TransactionCount = subsPlanInfo.TransactionCount;
                        pubBusinessSubsPlan.UserPerCustomerCount = subsPlanInfo.UserPerCustomerCount;
                        _pubBusinessSubsPlanDS.UpdateSystemFieldsByOpType(pubBusinessSubsPlan, OperationType.Add);
                        pubBusinessSubsPlan.TenantId = publisherTenantId;
                        await _pubBusinessSubsPlanDS.AddAsync(pubBusinessSubsPlan, cancellationToken);

                        await AddSubscriptionPlanServiceAndAttributeAsync(filteredServiceList, publisherTenantId, pubBusinessSubsPlan.ID, pubBusinessSubsPlan.AppId, cancellationToken);
                        break;
                    case OperationType.Update:
                        pubBusinessSubsPlan = await _pubBusinessSubsPlanDS.GetByPublisherTenantAndAppAndPlanIdAsync(publisherTenantId, subsPlanInfo.AppId, subsPlanInfo.Id, cancellationToken);
                        pubBusinessSubsPlan.Active = subsPlanInfo.Active;
                        pubBusinessSubsPlan.AllowUnlimitedTransaction = subsPlanInfo.AllowUnlimitedTransaction;
                        pubBusinessSubsPlan.AppId = subsPlanInfo.AppId;
                        pubBusinessSubsPlan.AutoRenewal = subsPlanInfo.AutoRenewal;
                        pubBusinessSubsPlan.BusinessUserCount = subsPlanInfo.BusinessUserCount;
                        pubBusinessSubsPlan.CustomerUserCount = subsPlanInfo.CustomerUserCount;
                        pubBusinessSubsPlan.Deleted = false;
                        pubBusinessSubsPlan.EndDate = subsPlanInfo.EndDate;
                        pubBusinessSubsPlan.OneTimePlan = subsPlanInfo.OneTimePlan;
                        pubBusinessSubsPlan.OtherFeatures = subsPlanInfo.OtherFeatures;
                        pubBusinessSubsPlan.PaymentCycle = subsPlanInfo.PaymentCycle;
                        pubBusinessSubsPlan.PlanName = subsPlanInfo.PlanName;
                        pubBusinessSubsPlan.PriceInDollar = subsPlanInfo.PriceInDollar;
                        pubBusinessSubsPlan.ShipmentCount = subsPlanInfo.ShipmentCount;
                        pubBusinessSubsPlan.ShipmentUnit = subsPlanInfo.ShipmentUnit;
                        pubBusinessSubsPlan.StartDate = subsPlanInfo.StartDate;
                        pubBusinessSubsPlan.SubscriptionPlanId = subsPlanInfo.Id;
                        pubBusinessSubsPlan.Term = subsPlanInfo.Term;
                        pubBusinessSubsPlan.TermUnit = subsPlanInfo.TermUnit;
                        pubBusinessSubsPlan.TransactionCount = subsPlanInfo.TransactionCount;
                        pubBusinessSubsPlan.UserPerCustomerCount = subsPlanInfo.UserPerCustomerCount;
                        _pubBusinessSubsPlanDS.UpdateSystemFieldsByOpType(pubBusinessSubsPlan, OperationType.Update);
                        await _pubBusinessSubsPlanDS.UpdateAsync(pubBusinessSubsPlan, pubBusinessSubsPlan.ID, cancellationToken);

                        break;
                    case OperationType.Delete:
                        pubBusinessSubsPlan = await _pubBusinessSubsPlanDS.GetByPublisherTenantAndAppAndPlanIdAsync(publisherTenantId, subsPlanInfo.AppId, subsPlanInfo.Id, cancellationToken);
                        pubBusinessSubsPlan.Deleted = true;
                        _pubBusinessSubsPlanDS.UpdateSystemFieldsByOpType(pubBusinessSubsPlan, OperationType.Update);
                        await _pubBusinessSubsPlanDS.UpdateAsync(pubBusinessSubsPlan, pubBusinessSubsPlan.ID, cancellationToken);
                        List<PubBusinessSubsPlanAppService> planServiceList = await _pubBusinessSubsPlanAppServiceDS.GetListByPubBusinessSubsPlanIdAsync(pubBusinessSubsPlan.ID, cancellationToken);
                        foreach(PubBusinessSubsPlanAppService pubBusinessAppService in planServiceList) {
                            pubBusinessAppService.Deleted = true;
                            _pubBusinessSubsPlanAppServiceDS.UpdateSystemFieldsByOpType(pubBusinessAppService, OperationType.Update);
                            await _pubBusinessSubsPlanAppServiceDS.UpdateAsync(pubBusinessAppService, pubBusinessAppService.ID, cancellationToken);
                        }
                        break;
                }

            }
        }

        private async Task AddSubscriptionPlanServiceAndAttributeAsync(List<SubsPlanServiceInfoDTO> subsPlanServiceInfoDTOs, Guid publisherTenantId, Guid pubBusinessSubsPlanId, Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            foreach(SubsPlanServiceInfoDTO planService in subsPlanServiceInfoDTOs) {
                foreach(SubsPlanServiceAttributeInfoDTO attributeInfo in planService.ServiceAttributeList) {
                    PubBusinessSubsPlanAppService pubBusinessSubsPlanAppService = new PubBusinessSubsPlanAppService();
                    pubBusinessSubsPlanAppService.AppId = appId;
                    pubBusinessSubsPlanAppService.AppServiceId = planService.AppServiceId;
                    pubBusinessSubsPlanAppService.AppServiceAttributeId = attributeInfo.AppServiceAttributeId;

                    pubBusinessSubsPlanAppService.SubsPlanAppServiceId = planService.SubscriptionPlanServiceId;
                    pubBusinessSubsPlanAppService.SubsPlanAppServiceAttributeId = attributeInfo.SubsPlanServiceAttributeId;
                    pubBusinessSubsPlanAppService.ServiceName = planService.ServiceName;
                    pubBusinessSubsPlanAppService.ServiceAttributeName = attributeInfo.AttributeName;

                    pubBusinessSubsPlanAppService.PubBusinessSubsPlanId = pubBusinessSubsPlanId;
                    _pubBusinessSubsPlanAppServiceDS.UpdateSystemFieldsByOpType(pubBusinessSubsPlanAppService, OperationType.Add);
                    pubBusinessSubsPlanAppService.TenantId = publisherTenantId;
                    await _pubBusinessSubsPlanAppServiceDS.AddAsync(pubBusinessSubsPlanAppService, cancellationToken);
                }
            }
        }

        private async Task ManagePublisherAddressList(List<PublisherAddressDTO> publisherAddressDTOs, Guid publisherTenantId, Guid publisherId, CancellationToken cancellationToken = default(CancellationToken)) {
            foreach(PublisherAddressDTO publisherAddressDTO in publisherAddressDTOs) {
                if(publisherAddressDTO.OpType == OperationType.Add) {
                    PublisherAddress publisherAddress = new PublisherAddress();
                    publisherAddress.AddressStreet1 = publisherAddressDTO.AddressStreet1;
                    publisherAddress.AddressStreet2 = publisherAddressDTO.AddressStreet2;
                    publisherAddress.AddressStreet3 = publisherAddressDTO.AddressStreet3;
                    publisherAddress.AddressType = publisherAddressDTO.AddressType;
                    publisherAddress.City = publisherAddressDTO.City;
                    publisherAddress.Country = publisherAddressDTO.Country;
                    publisherAddress.FaxNumber = publisherAddressDTO.FaxNumber;
                    publisherAddress.Label = publisherAddressDTO.Label;
                    publisherAddress.Phone = publisherAddressDTO.Phone;
                    publisherAddress.PublisherId = publisherId;
                    publisherAddress.State = publisherAddressDTO.State;
                    publisherAddress.ZipCode = publisherAddressDTO.ZipCode;
                    _publisherAddressDS.UpdateSystemFieldsByOpType(publisherAddress, OperationType.Add);
                    publisherAddress.TenantId = publisherTenantId;
                    await _publisherAddressDS.AddAsync(publisherAddress, cancellationToken);
                }
                else if(publisherAddressDTO.OpType == OperationType.Update) {
                    PublisherAddress publisherAddress = await _publisherAddressDS.FindAsync(i => i.ID == publisherAddressDTO.ID && i.Deleted == false, cancellationToken);
                    publisherAddress.AddressStreet1 = publisherAddressDTO.AddressStreet1;
                    publisherAddress.AddressStreet2 = publisherAddressDTO.AddressStreet2;
                    publisherAddress.AddressStreet3 = publisherAddressDTO.AddressStreet3;
                    publisherAddress.AddressType = publisherAddressDTO.AddressType;
                    publisherAddress.City = publisherAddressDTO.City;
                    publisherAddress.Country = publisherAddressDTO.Country;
                    publisherAddress.FaxNumber = publisherAddressDTO.FaxNumber;
                    publisherAddress.Label = publisherAddressDTO.Label;
                    publisherAddress.Phone = publisherAddressDTO.Phone;
                    //publisherAddress.PublisherId = publisherAddressDTO.PublisherId;
                    publisherAddress.State = publisherAddressDTO.State;
                    publisherAddress.ZipCode = publisherAddressDTO.ZipCode;
                    _publisherAddressDS.UpdateSystemFieldsByOpType(publisherAddress, OperationType.Update);
                    await _publisherAddressDS.UpdateAsync(publisherAddress, publisherAddress.ID, cancellationToken);

                }
                else if(publisherAddressDTO.OpType == OperationType.Delete) {
                    PublisherAddress publisherAddress = await _publisherAddressDS.FindAsync(i => i.ID == publisherAddressDTO.ID && i.Deleted == false, cancellationToken);
                    publisherAddress.Deleted = true;
                    _publisherAddressDS.UpdateSystemFieldsByOpType(publisherAddress, OperationType.Update);
                    await _publisherAddressDS.UpdateAsync(publisherAddress, publisherAddress.ID, cancellationToken);
                }
            }

        }
    }
}