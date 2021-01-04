/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Rajesh Thakur <rthakur@eworkplaceapps.com>
 * Date: 02 January 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 02 January 2019
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AppManagement.DTO;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.DS {

    /// <summary>
    ///  This class implements standard business logic and operations for ServiceAttribute entity.
    /// </summary>
    public class AppServiceAttributeDS:BaseDS<AppServiceAttribute>, IAppServiceAttributeDS {

        #region Local Member 

       
        IUnitOfWork _unitOfWork;
        IAppServiceAttributeRepository _serviceAttributeRepository;
        IUserSessionManager _sessionmanager;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="serviceAttributeRepository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="cacheService"></param>
        public AppServiceAttributeDS(IAppServiceAttributeRepository serviceAttributeRepository, IUnitOfWork unitOfWork, IUserSessionManager sessionmanager) : base(serviceAttributeRepository) {
            
            _unitOfWork = unitOfWork;
            _serviceAttributeRepository = serviceAttributeRepository;
            _sessionmanager = sessionmanager;
        }

        #endregion

        #region Get

        /// <summary>
        /// Get the Appservice attribute list by service id.
        /// </summary>
        /// <param name="appServiceId">ServiceId</param>
        /// <param name="token"></param>
        public async Task<List<AppServiceAttributeDTO>> GetAppServiceAttributeByAppServiceAsync(Guid appServiceId, CancellationToken token = default(CancellationToken)) {
            List<AppServiceAttribute> list = await _serviceAttributeRepository.GetAppServiceAttributeByAppServiceAsync(appServiceId, token);
            AppServiceAttributeDTO dto;
            List<AppServiceAttributeDTO> listDto = new List<AppServiceAttributeDTO>();
            for(int i = 0; i < list.Count; i++) {
                dto = AppServiceAttributeDTO.MapAppServiceAttributeToDTO(list[i]);
                listDto.Add(dto);
            }

            return listDto;
        }

        ///<inheritdoc/>
        public async Task <List<AppServiceAttribute>> GetAppServiceAttributeListByServiceIdAsync(Guid appServiceId, bool skipDeleted, CancellationToken token = default(CancellationToken)) {
            return await _serviceAttributeRepository.GetAppServiceAttributeListByServiceIdAsync(appServiceId, skipDeleted, token);
        }

        ///<inheritdoc/>
        public List<AppServiceAttributeDTO> GetAppServiceAttributeListByServiceId(Guid appServiceId, bool onlyActive, bool skipDeleted, CancellationToken token = default(CancellationToken)) {
            return _serviceAttributeRepository.GetAppServiceAttributeListByServiceIdAsync(appServiceId, onlyActive, skipDeleted, token).Result;
        }

        ///<inheritdoc/>
        public async Task<List<AppServiceAttribute>> GetAppServiceAttributeListAsync(List<PubBusinessSubsPlanAppServiceDTO> pubBusinessSubsPlanAppServiceDTO, CancellationToken token = default(CancellationToken)) {

            List<AppServiceAttribute> appservices = await _serviceAttributeRepository.GetAppServiceAttributeListAsync(pubBusinessSubsPlanAppServiceDTO, token);
            return appservices;
        }


        #endregion Get

    }
}
