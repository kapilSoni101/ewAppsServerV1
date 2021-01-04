/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 11 Nov 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 11 Nov 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {

    /// <summary>
    /// This class Contain Business Logic of ItemMaster Report 
    /// </summary>
    public class QItemMasterReportDS:BaseDS<BaseDTO>, IQItemMasterReportDS {

        #region Local Member
        IQItemMasterReportRepository _itemMasterReportRepository;
        IUserSessionManager _userSessionManager;
        #endregion

        #region Constructor
        /// <summary>
        ///  Constructor Initialize the Base Variable
        /// </summary>
        /// <param name="itemMasterReportRepository"></param>
        public QItemMasterReportDS(IQItemMasterReportRepository itemMasterReportRepository, IUserSessionManager userSessionManager) : base(itemMasterReportRepository) {
            _itemMasterReportRepository = itemMasterReportRepository;
            _userSessionManager = userSessionManager;
        }

        #endregion Constructor

        #region Business
        ///<inheritdoc/>
        public async Task<List<VendItemMasterReportDTO>> GetBizVendItemMasterListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            UserSession us = _userSessionManager.GetSession();
            List<VendItemMasterReportDTO> vendItemMasterReportDTO = new List<VendItemMasterReportDTO>();
            vendItemMasterReportDTO =  await _itemMasterReportRepository.GetBizVendItemMasterListByTenantIdAsync(filter, us.TenantId, token);
            for(int i = 0; i < vendItemMasterReportDTO.Count(); i++) {
                vendItemMasterReportDTO.ElementAt(i).Size = String.Format("{0:0.00}", vendItemMasterReportDTO.ElementAt(i).SalesLength) + " " + vendItemMasterReportDTO.ElementAt(i).SalesLengthUnitText + " x " + String.Format("{0:0.00}", vendItemMasterReportDTO.ElementAt(i).SalesWidth) + " " + vendItemMasterReportDTO.ElementAt(i).SalesWidthUnitText + " x " + String.Format("{0:0.00}", vendItemMasterReportDTO.ElementAt(i).SalesHeight) + " " + vendItemMasterReportDTO.ElementAt(i).SalesHeightUnitText;
            }
            return vendItemMasterReportDTO;
        }
        #endregion

        ///<inheritdoc/>
        public async Task<List<PartItemMasterReportDTO>> GetCustItemMasterListByTenantIdAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            UserSession us = _userSessionManager.GetSession();
            List<PartItemMasterReportDTO> partItemMasterReportDTO = await _itemMasterReportRepository.GetCustItemMasterListByTenantIdAsync(filter, us.TenantId, token);
            for(int i = 0; i < partItemMasterReportDTO.Count(); i++) {
                partItemMasterReportDTO.ElementAt(i).Size = String.Format("{0:0.00}", partItemMasterReportDTO.ElementAt(i).SalesLength) + " " + partItemMasterReportDTO.ElementAt(i).SalesLengthUnitText + " x " + String.Format("{0:0.00}", partItemMasterReportDTO.ElementAt(i).SalesWidth) + " " + partItemMasterReportDTO.ElementAt(i).SalesWidthUnitText + " x " + String.Format("{0:0.00}", partItemMasterReportDTO.ElementAt(i).SalesHeight) + " " + partItemMasterReportDTO.ElementAt(i).SalesHeightUnitText;
            }
            return partItemMasterReportDTO;
            
        }
        }
}
