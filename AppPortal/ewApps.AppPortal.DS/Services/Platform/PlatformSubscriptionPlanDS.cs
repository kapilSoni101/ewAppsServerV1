/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

  /// <summary>
  /// This class implements standard business logic and operations for PlatformSubscriptionDS.
  /// </summary>
  public class PlatformSubscriptionPlanDS : IPlatformSubscriptionPlanDS {

    #region Local Member

    IPlatformSubscriptionPlanAccess _entityAccess;
    AppPortalAppSettings _appPortalAppSettings;
    IUserSessionManager _userSessionManager;
    IPubBusinessSubsPlanDS _pubBusinessSubPlanDS;

    #endregion

    #region Constructor 

    /// <summary>
    /// Initializing local variables
    /// </summary>
    /// <param name="entityAccess">entity access class reference.</param>
    public PlatformSubscriptionPlanDS(IPlatformSubscriptionPlanAccess entityAccess, IUserSessionManager userSessionManager, IOptions<AppPortalAppSettings> appPortalAppSettings, IPubBusinessSubsPlanDS pubBusinessSubPlanDS) {
      _entityAccess = entityAccess;
      _appPortalAppSettings = appPortalAppSettings.Value;
      _userSessionManager = userSessionManager;
      _pubBusinessSubPlanDS = pubBusinessSubPlanDS;
    }

    #endregion Constructor        

    #region Security

    ///<inheritdoc/>
    public IEnumerable<bool> GetLoginUsersAppPermission() {
      return _entityAccess.AccessList(Guid.Empty);
    }

    ///<inheritdoc/>
    private void CheckSecurityOnAdding() {

      if(!_entityAccess.CheckAccess((int)OperationType.Add, Guid.Empty)) {

        // Raise security exception
        List<EwpErrorData> errorDataList = new List<EwpErrorData>() {
            new EwpErrorData() {
              ErrorSubType = (int)SecurityErrorSubType.Add,
              Message = string.Format("", "AppUser")
              }
            };

        throw new EwpSecurityException("", errorDataList);
      }
    }

    ///<inheritdoc/>
    private void CheckSecurityOnUpdating() {
      if(!_entityAccess.CheckAccess((int)OperationType.Update, Guid.Empty)) {

        // Raise security exception
        List<EwpErrorData> errorDataList = new List<EwpErrorData>() {
            new EwpErrorData() {
              ErrorSubType = (int)SecurityErrorSubType.Update,
              Message = string.Format("", "AppUser")
              }
            };

        throw new EwpSecurityException("", errorDataList);
      }
    }

    ///<inheritdoc/>
    private void CheckSecurityOnDelete() {
      if(!_entityAccess.CheckAccess((int)OperationType.Delete, Guid.Empty)) {
        // Raise security exception
        List<EwpErrorData> errorDataList = new List<EwpErrorData>() {
            new EwpErrorData() {
              ErrorSubType = (int)SecurityErrorSubType.Delete,
              Message = string.Format("", "AppUser")
              }
            };

        throw new EwpSecurityException("", errorDataList);
      }
    }

    #endregion Security

    #region Get Methods

    ///<inheritdoc/>
    public async Task<List<AppServiceDTO>> GetAppServicesDetailWithAttributesAsync(Guid appId, CancellationToken token = default(CancellationToken)) {

      UserSession session = _userSessionManager.GetSession();

      // Preparing api calling process model.
      string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
      string requesturl = "appservice/appserviceswithattribute/" +appId;

      RequestOptions requestOptions = new RequestOptions();
      requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
      requestOptions.Method = requesturl;
      requestOptions.MethodData = null;
      requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
      requestOptions.ServiceRequestType = RequestTypeEnum.Get;
      requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
      return await httpRequestProcessor.ExecuteAsync<List<AppServiceDTO>>(requestOptions, false);
    }

    ///<inheritdoc/>
    public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByTenantIdAsync(BooleanFilterEnum planState, CancellationToken cancellationToken = default(CancellationToken)) {
      UserSession session = _userSessionManager.GetSession();

      // Preparing api calling process model.
      string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
      string requesturl = "subscriptionplan/list/" + planState;

      RequestOptions requestOptions = new RequestOptions();
      requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
      requestOptions.Method = requesturl;
      requestOptions.MethodData = null;
      requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
      requestOptions.ServiceRequestType = RequestTypeEnum.Get;
      requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
      return await httpRequestProcessor.ExecuteAsync<List<SubscriptionPlanInfoDTO>>(requestOptions, false);
    }

    ///<inheritdoc/>
    public async Task<SubscriptionPlanInfoDTO> GetSubscriptionPlaninfoByIdAsync(Guid  planId, CancellationToken cancellationToken = default(CancellationToken))
    {
      UserSession session = _userSessionManager.GetSession();

      // Preparing api calling process model.
      string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
      string requesturl = "subscriptionplan/detail/" + planId;

      RequestOptions requestOptions = new RequestOptions();
      requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
      requestOptions.Method = requesturl;
      requestOptions.MethodData = null;
      requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
      requestOptions.ServiceRequestType = RequestTypeEnum.Get;
      requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
      return await httpRequestProcessor.ExecuteAsync<SubscriptionPlanInfoDTO>(requestOptions, false);
    }

    #endregion Get Methods

    #region Add/Update/Delete

    /// <inheritdoc/>
    public async Task AddSubscriptionPlanWithServiceAttributeAsync(SubscriptionAddUpdateDTO addUdpateDTO, CancellationToken token = new CancellationToken()) {
      UserSession session = _userSessionManager.GetSession();

      string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
      string requesturl = "subscriptionplan/subscriptionplanwithattribute";

      ResponseModelDTO responseModelDTO = new ResponseModelDTO();

      RequestOptions requestOptions = new RequestOptions();
      requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
      requestOptions.Method = requesturl;
      requestOptions.MethodData = addUdpateDTO;
      requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
      requestOptions.ServiceRequestType = RequestTypeEnum.Post;

      requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

      try
      {
        ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
        responseModelDTO = await httpRequestProcessor.ExecuteAsync<ResponseModelDTO>(requestOptions, false);
      }
      catch (Exception ex)
      { 
        throw;
      }
    }

    /// <inheritdoc/>
    public async Task UpdateSubscriptionPlanWithServiceAttributeAsync(SubscriptionAddUpdateDTO addUdpateDTO, CancellationToken token = new CancellationToken()) {
      UserSession session = _userSessionManager.GetSession();

      string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
      string requesturl = "subscriptionplan/subscriptionplanwithattribute";

      ResponseModelDTO responseModelDTO = new ResponseModelDTO();

      RequestOptions requestOptions = new RequestOptions();
      requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
      requestOptions.Method = requesturl;
      requestOptions.MethodData = addUdpateDTO;
      requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
      requestOptions.ServiceRequestType = RequestTypeEnum.Put;

      requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

      try {
        //_pubBusinessSubPLanDS.UpdateByPlanId()

        List<PubBusinessSubsPlan> entityList = (await _pubBusinessSubPlanDS.FindAllAsync(p => p.SubscriptionPlanId == addUdpateDTO.ID)).ToList();

        for (int i = 0; i < entityList.Count; i++) {

          entityList[i].PlanName = addUdpateDTO.PlanName;
          //entityList[i].StartDate = addUdpateDTO.StartDate;
          entityList[i].EndDate = addUdpateDTO.EndDate;
          entityList[i].Term = addUdpateDTO.PlanTerm;

// ToDo: change active for all, only iffor trial plans
//entityList[i].Active = addUdpateDTO.Active;

          //entityList[i].BusinessUserCount = addUdpateDTO.BusinessUserCount;
          //entityList[i].CustomerUserCount = addUdpateDTO.CustomerUserCount;
          //entityList[i].TransactionCount = addUdpateDTO.TransactionCount;

          await _pubBusinessSubPlanDS.UpdateAsync(entityList[i], entityList[i].ID);

          await _pubBusinessSubPlanDS.SaveAsync();
        }

        ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
        responseModelDTO = await httpRequestProcessor.ExecuteAsync<ResponseModelDTO>(requestOptions, false);
      }

      catch (Exception ex) {
        throw;

      }

    }

    /// <inheritdoc/>
    public async Task DeleteSubscriptionPlanWithServiceAttributeAsync(Guid planId, CancellationToken token = new CancellationToken()) {
      UserSession session = _userSessionManager.GetSession();

      string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
      string requesturl = "subscriptionplan/subscriptionplanwithattribute/" +planId;

      ResponseModelDTO responseModelDTO = new ResponseModelDTO();

      RequestOptions requestOptions = new RequestOptions();
      requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
      requestOptions.Method = requesturl;
      requestOptions.MethodData = null;
      requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
      requestOptions.ServiceRequestType = RequestTypeEnum.Delete;

      requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

      try {
        ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
        responseModelDTO = await httpRequestProcessor.ExecuteAsync<ResponseModelDTO>(requestOptions, false);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    #endregion

  }
}

