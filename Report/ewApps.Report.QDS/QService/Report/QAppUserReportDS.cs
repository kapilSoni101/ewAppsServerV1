/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 19 December 2018
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.Common;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {

    /// <summary>
    /// This class Contain Business Login of AppUser Report 
    /// </summary>
    public class QAppUserReportDS:BaseDS<BaseDTO>, IQAppUserReportDS {

    #region Local Member
    IQAppUserReportRepository _appUserRptRepos;
    IUserSessionManager _userSessionManager;
    #endregion

    #region Constructor
    /// <summary>
    ///  Constructor Initialize the Base Variable
    /// </summary>
    /// <param name="portalUserReportRepository"></param>
    /// <param name="cacheService"></param>
    public QAppUserReportDS(IQAppUserReportRepository appUserRptRepos,  IUserSessionManager userSessionManager) : base(appUserRptRepos) {
      _appUserRptRepos = appUserRptRepos;
      _userSessionManager = userSessionManager;
  }

    #endregion Constructor

    #region Get

    ///<inheritdoc/>
    public async Task<List<PlatAppUserReportDTO>> GetAllPFAppUserListByUserTypeAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
      UserSession session = _userSessionManager.GetSession();
      List<PlatAppUserReportDTO> appUserDTOs = await _appUserRptRepos.GetAllPFAppUserListByUserTypeAsync(filter, (int)UserTypeEnum.Platform, session.TenantId, session.AppId, token);
      foreach (PlatAppUserReportDTO item in appUserDTOs) {
        // Get feature count.        
        item.Permissions = GetPublisherPermissionCountByBitMask(item.PermissionBitMask);

      }
      return appUserDTOs;
    }

    ///<inheritdoc/>
    public async Task<List<PubAppUserReportDTO>> GetAllPubAppUserListByUserTypeAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
      UserSession session = _userSessionManager.GetSession();
      List<PubAppUserReportDTO> appUserDTOs =  await _appUserRptRepos.GetAllPubAppUserListByUserTypeAsync(filter, (int)UserTypeEnum.Publisher, session.TenantId, filter.CustomerId, session.AppId, token);
      foreach (PubAppUserReportDTO item in appUserDTOs)
      {
        // Get feature count.        
        item.Permissions = GetPublisherPermissionCountByBitMask(item.PermissionBitMask);
       
      }
      return appUserDTOs;
    }

    ///<inheritdoc/>
    public async Task<List<BizAppUserReportDTO>> GetBizPayAppUserListByUserTypeAsync(ReportFilterDTO filter,Guid appId, CancellationToken token = default(CancellationToken)) {
      UserSession session = _userSessionManager.GetSession();
      List<BizAppUserReportDTO> appUserDTOs = await _appUserRptRepos.GetBizPayAppUserListByUserTypeAsync(filter, (int)UserTypeEnum.Business, session.TenantId, filter.CustomerId, appId, token);
      foreach (BizAppUserReportDTO item in appUserDTOs) {
        // Get feature count.        
        item.Permissions = GetPublisherPermissionCountByBitMask(item.PermissionBitMask);

      }
      return appUserDTOs;
    }

        ///<inheritdoc/>
        public async Task<List<BizAppUserReportDTO>> GetBizCustAppUserListByUserTypeAsync(ReportFilterDTO filter, Guid appId, CancellationToken token = default(CancellationToken)) {
            UserSession session = _userSessionManager.GetSession();
            List<BizAppUserReportDTO> appUserDTOs = await _appUserRptRepos.GetBizCustAppUserListByUserTypeAsync(filter, (int)UserTypeEnum.Business, session.TenantId, filter.CustomerId, appId, token);
            foreach(BizAppUserReportDTO item in appUserDTOs) {
                // Get feature count.        
                item.Permissions = GetPublisherPermissionCountByBitMask(item.PermissionBitMask);

            }
            return appUserDTOs;
        }

        ///<inheritdoc/>
        public async Task<List<PartAppUserReportDTO>> GetPartCustAppUserListAsync(ReportFilterDTO filter, Guid appId, Guid businesspartnertenantid, CancellationToken token = default(CancellationToken)) {
      UserSession session = _userSessionManager.GetSession();
      List<PartAppUserReportDTO> appUserDTOs = await _appUserRptRepos.GetPartCustAppUserListAsync(filter, (int)UserTypeEnum.Customer, session.TenantId, filter.CustomerId, appId, token);
      appUserDTOs = appUserDTOs.Where(ul => ul.BusinessPartnerTenantId == businesspartnertenantid).ToList();
      foreach (PartAppUserReportDTO item in appUserDTOs) {
        // Get feature count.        
        item.Permissions = GetPublisherPermissionCountByBitMask(item.PermissionBitMask);

      }
      return appUserDTOs;
    }

        ///<inheritdoc/>
        public async Task<List<PartAppUserReportDTO>> GetPartPayAppUserListAsync(ReportFilterDTO filter, Guid appId, Guid businesspartnertenantid, CancellationToken token = default(CancellationToken)) {
            UserSession session = _userSessionManager.GetSession();
            List<PartAppUserReportDTO> appUserDTOs = await _appUserRptRepos.GetPartPayAppUserListAsync(filter, (int)UserTypeEnum.Customer, session.TenantId, filter.CustomerId, appId, token);
            appUserDTOs = appUserDTOs.Where(ul => ul.BusinessPartnerTenantId == businesspartnertenantid).ToList();
            foreach(PartAppUserReportDTO item in appUserDTOs) {
                // Get feature count.        
                item.Permissions = GetPublisherPermissionCountByBitMask(item.PermissionBitMask);

            }
            return appUserDTOs;
        }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetUserNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
      return await _appUserRptRepos.GetUserNameListByTenantIdAsync(tenantId, token);
    }

    ///<inheritdoc/>
    public async Task<List<NameDTO>> GetPFUserNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
      return await _appUserRptRepos.GetPFUserNameListByTenantIdAsync(tenantId, token);
    }

    ///<inheritdoc/>
    /// <summary>
    /// Method returns the feature count.
    /// </summary>
    /// <param name="permissionbitmask"></param>
    /// <returns></returns>
    private int GetPublisherPermissionCountByBitMask(long permissionbitmask)
    {
      int enumCount = Enum.GetNames(typeof(PublisherPermissionEnum)).Length;
      List<PublisherPermissionEnum> list = new List<PublisherPermissionEnum>();
      PublisherPermissionEnum bitmask = (PublisherPermissionEnum)permissionbitmask;
      foreach (PublisherPermissionEnum item in Enum.GetValues(typeof(PublisherPermissionEnum)))
      {
        if ((bitmask & item) == item)
        {
          list.Add(item);
        }
      }
      int count = 0;
      if (list.Count == enumCount)
      {
        count = enumCount - 2;
      }
      else
      {
        count = list.Count - 1;
      }
      return count;
    }


    #endregion
  }
}
