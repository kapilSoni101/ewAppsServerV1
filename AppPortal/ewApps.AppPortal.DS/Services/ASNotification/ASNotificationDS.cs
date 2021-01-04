/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil nigam <anigam@eworkplaceapps.com>
 * Date: 06 Nov 2019

 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Linq.Expressions;

namespace ewApps.AppPortal.DS {
    public class ASNotificationDS:BaseDS<ASNotification>, IASNotificationDS {

        IASNotificationRepository _aSNotificationRespository;
        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appPortalAppSettings;
        IUnitOfWork _unitOfWork;
        IPublisherAppSettingDS _publisherAppSettingDS;

        public ASNotificationDS(IPublisherAppSettingDS publisherAppSettingDS, IASNotificationRepository aSNotificationRespository, IOptions<AppPortalAppSettings> appPortalAppSettings, IUserSessionManager userSessionManager, IUnitOfWork unitOfWork) : base(aSNotificationRespository) {
            _aSNotificationRespository = aSNotificationRespository;
            _appPortalAppSettings = appPortalAppSettings.Value;
            _userSessionManager = userSessionManager;
            _unitOfWork = unitOfWork;
        }

        #region Get

        public async Task<List<ASNotificationDTO>> GetASNotificationList(Guid AppId, int fromCount, int toCount, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            // Get as notification from app portal
            List<ASNotificationDTO> appPortalASNotification = await GetASNotificationFromAppPortal(AppId, fromCount, toCount, userSession, token);
            // Get as notification from business entity
            List<ASNotificationDTO> businessEntityASNotification = await GetASNotificationFromBusinessEntity(AppId, fromCount, toCount);
            // Get as notification from payment entity
            List<ASNotificationDTO> paymentEntityASNotification = await GetASNotificationFromPaymentEntity(AppId, fromCount, toCount);
            // Get filtered data of notification
            return await GetFilteredDataASNotification(appPortalASNotification, businessEntityASNotification, paymentEntityASNotification, fromCount, toCount);
        }

        public async Task<List<ASNotificationDTO>> GetBizSetupASNotificationListAsync(List<KeyValuePair<string, Guid>> appIdList, int fromCount, int toCount, CancellationToken token = default(CancellationToken)) {

            Guid bizSetupAppId = appIdList.First(i => i.Key == AppKeyEnum.biz.ToString()).Value;

            #region Get AS Generated For Biz Setup App
            UserSession userSession = _userSessionManager.GetSession();

            // Get as notification from app portal
            List<ASNotificationDTO> appPortalASNotification = await GetASNotificationFromAppPortal(bizSetupAppId, fromCount, toCount, userSession, token);

            // Get as notification from business entity
            List<ASNotificationDTO> businessEntityASNotification = await GetASNotificationFromBusinessEntity(bizSetupAppId, fromCount, toCount);

            // Get as notification from payment entity
            List<ASNotificationDTO> paymentEntityASNotification = await GetASNotificationFromPaymentEntity(bizSetupAppId, fromCount, toCount);

            #endregion

            int appIndex = appIdList.FindIndex(i => i.Key == AppKeyEnum.pay.ToString());
            if(appIndex >= 0) {
                // Get as notification from payment entity
                // paymentEntityASNotification.Concat(await GetASNotificationFromPaymentEntityAsync(appIdList[appIndex].Value, fromCount, toCount, (int)EntityTypeEnum.SupportTicket));
                appPortalASNotification.AddRange(await _aSNotificationRespository.GetASNotificationListByAppAndTenantAndUserAndEntityTypeAsync(appIdList[appIndex].Value, userSession.TenantId, userSession.TenantUserId, (int)EntityTypeEnum.SupportTicket, fromCount, toCount, token));
            }

            appIndex = appIdList.FindIndex(i => i.Key == AppKeyEnum.cust.ToString());

            if(appIndex >= 0) {
                // Get as notification from app portal entity
                // appPortalASNotification.Concat(await GetASNotificationFromAppPortalAsync(appIdList[appIndex].Value, fromCount, toCount, (int)EntityTypeEnum.SupportTicket, token));
                appPortalASNotification.AddRange(await _aSNotificationRespository.GetASNotificationListByAppAndTenantAndUserAndEntityTypeAsync(appIdList[appIndex].Value, userSession.TenantId, userSession.TenantUserId, (int)EntityTypeEnum.SupportTicket, fromCount, toCount, token));
            }


            // Get filtered data of notification
            return await GetFilteredDataASNotification(appPortalASNotification, businessEntityASNotification, paymentEntityASNotification, fromCount, toCount);
        }

        private async Task<List<ASNotificationDTO>> GetASNotificationFromAppPortal(Guid appId, int fromCount, int toCount, UserSession userSession, CancellationToken token = default(CancellationToken)) {
            return await _aSNotificationRespository.GetASNotificationList(appId, userSession.TenantId, userSession.TenantUserId, fromCount, toCount, token);
        }


        private async Task<List<ASNotificationDTO>> GetASNotificationFromBusinessEntity(Guid AppId, int fromCount, int toCount) {

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            string methodUrl = "asnotification/bizentity/" + AppId + "/" + fromCount + "/" + toCount;
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUrl, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
            return await serviceExecutor.ExecuteAsync<List<ASNotificationDTO>>(requestOptions, false);

        }

        private async Task<List<ASNotificationDTO>> GetASNotificationFromPaymentEntity(Guid AppId, int fromCount, int toCount) {

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            string methodUrl = "asnotification/paymententity/" + AppId + "/" + fromCount + "/" + toCount;
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUrl, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.PaymentApiUrl);
            return await serviceExecutor.ExecuteAsync<List<ASNotificationDTO>>(requestOptions, false);

        }

        private async Task<List<ASNotificationDTO>> GetASNotificationFromPaymentEntityAsync(Guid AppId, int fromCount, int toCount, int entityType) {

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            string methodUrl = $"asnotification/paymententity/{AppId.ToString()}/{fromCount}/{toCount}/{entityType.ToString()}";
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUrl, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.PaymentApiUrl);
            return await serviceExecutor.ExecuteAsync<List<ASNotificationDTO>>(requestOptions, false);

        }

        private async Task<List<ASNotificationDTO>> GetASNotificationFromAppPortalAsync(Guid appId, int fromCount, int toCount, int entityType, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            return await _aSNotificationRespository.GetASNotificationListAsync(appId, userSession.TenantId, userSession.TenantUserId, fromCount, toCount, entityType, token);
        }

        private async Task<List<ASNotificationDTO>> GetFilteredDataASNotification(List<ASNotificationDTO> appPortal, List<ASNotificationDTO> bizEntityPortal, List<ASNotificationDTO> paymentEntityPortal, int fromCount, int toCount) {
            List<ASNotificationDTO> aSNotificationList = new List<ASNotificationDTO>();
            if(appPortal != null)
                aSNotificationList.AddRange(appPortal);
            if(bizEntityPortal != null)
                aSNotificationList.AddRange(bizEntityPortal);
            if(paymentEntityPortal != null)
                aSNotificationList.AddRange(paymentEntityPortal);

            // if user want to show all notification
            if(toCount == 0)
                return aSNotificationList.OrderByDescending(s => s.CreatedOn).ToList();
            else
                // if user want to show limited notification
                return aSNotificationList.OrderByDescending(s => s.CreatedOn).ToList().Take(toCount).ToList();
        }

        public async Task<List<ASNotificationDTO>> GetUnreadASNotificationList(Guid AppId, CancellationToken token = default(CancellationToken)) {
            // Get unread as notification from app portal
            List<ASNotificationDTO> appPortalASNotification = await GetUnreadASNotificationFromAppPortal(AppId, token);
            // Get unread as notification from payment entity
            List<ASNotificationDTO> paymentEntityASNotification = await GetUnreadASNotificationFromPaymentEntity(AppId);
            // Get unread as notification from payment entity
            List<ASNotificationDTO> businessEntityASNotification = await GetUnreadASNotificationFromBusinessEntity(AppId);
            // Get filtered data of notification
            return await GetFilteredDataUnreadASNotification(appPortalASNotification, paymentEntityASNotification, businessEntityASNotification);
        }

        private async Task<List<ASNotificationDTO>> GetUnreadASNotificationFromAppPortal(Guid AppId, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            return await _aSNotificationRespository.GetUnreadASNotificationFromAppPortal(AppId, userSession.TenantId, userSession.TenantUserId, token);
        }

        private async Task<List<ASNotificationDTO>> GetUnreadASNotificationFromPaymentEntity(Guid AppId) {

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            string methodUrl = "asnotification/paymententity/" + AppId;
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUrl, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.PaymentApiUrl);
            return await serviceExecutor.ExecuteAsync<List<ASNotificationDTO>>(requestOptions, false);

        }
        private async Task<List<ASNotificationDTO>> GetUnreadASNotificationFromBusinessEntity(Guid AppId) {

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            string methodUrl = "asnotification/bizentity/" + AppId;
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUrl, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
            return await serviceExecutor.ExecuteAsync<List<ASNotificationDTO>>(requestOptions, false);

        }
        private async Task<List<ASNotificationDTO>> GetFilteredDataUnreadASNotification(List<ASNotificationDTO> appPortal, List<ASNotificationDTO> paymentEntityPortal, List<ASNotificationDTO> bizEntityPortal) {
            List<ASNotificationDTO> aSNotificationList = new List<ASNotificationDTO>();
            if(appPortal != null)
                aSNotificationList.AddRange(appPortal);
            if(paymentEntityPortal != null)
                aSNotificationList.AddRange(paymentEntityPortal);
            if(bizEntityPortal != null)
                aSNotificationList.AddRange(bizEntityPortal);

            return aSNotificationList;

        }



        #endregion Get

        #region Update

        public async Task<ResponseModelDTO> ReadASNotification(Guid Id) {
            ResponseModelDTO responseModelDTO = new ResponseModelDTO();
            // read notification from appportal
            responseModelDTO = await ReadAppPortalASNotification(Id);
            if(!responseModelDTO.IsSuccess) {
                // read notification from Biz Entity 
                responseModelDTO = await ReadBizEntityASNotification(Id);

                if(!responseModelDTO.IsSuccess) {
                    // read notification from Payment Entity 
                    responseModelDTO = await ReadPaymentEntityASNotification(Id);
                }
            }
            return responseModelDTO;
        }

        private async Task<ResponseModelDTO> ReadAppPortalASNotification(Guid Id, CancellationToken token = default(CancellationToken)) {
            ResponseModelDTO responseModelDTO = new ResponseModelDTO();
            // Get entity if exists
            ASNotification aSNotification = await FindAsync(a => a.ID == Id);
            if(aSNotification == null) {
                responseModelDTO.IsSuccess = false;
            }
            else {
                aSNotification.ReadState = true;
                Update(aSNotification, Id);

                // Save Data
                _unitOfWork.SaveAll();
                responseModelDTO.IsSuccess = true;
            }
            return responseModelDTO;
        }
        private async Task<ResponseModelDTO> ReadBizEntityASNotification(Guid Id) {

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            string methodUrl = "asnotification/readasnotification/" + Id;
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, methodUrl, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
            return await serviceExecutor.ExecuteAsync<ResponseModelDTO>(requestOptions, false);

        }
        private async Task<ResponseModelDTO> ReadPaymentEntityASNotification(Guid Id) {

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            string methodUrl = "asnotification/readasnotification/" + Id;
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, methodUrl, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.PaymentApiUrl);
            return await serviceExecutor.ExecuteAsync<ResponseModelDTO>(requestOptions, false);

        }
        #endregion Update
    }
}
