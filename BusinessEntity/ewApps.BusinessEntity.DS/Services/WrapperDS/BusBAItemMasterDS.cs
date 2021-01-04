using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Http;

namespace ewApps.BusinessEntity.DS {

    public class BusBAItemMasterDS:IBusBAItemMasterDS {

        #region Local Members

        private IBAItemMasterDS _itemMasterDS;
        private IBAItemAttachmentDS _itemAttachmentDS;
        IDMDocumentDS _dMDocumentDS;
        IUniqueIdentityGeneratorDS _uniqueIdentityGeneratorDS;
        IBAItemAttachmentDS _bAItemAttachmentDS;
        IUserSessionManager _userSessionMenager;
        IUnitOfWork _unitOfWork;

        #endregion Local Members 

        #region Constructor

        public BusBAItemMasterDS(IBAItemMasterDS itemMasterDS, IBAItemAttachmentDS itemAttachmentDS, IUniqueIdentityGeneratorDS uniqueIdentityGeneratorDS, IDMDocumentDS dMDocumentDS, IBAItemAttachmentDS bAItemAttachmentDS, IUserSessionManager userSessionManager, IUnitOfWork unitOfWork) {
            _itemMasterDS = itemMasterDS;
            _itemAttachmentDS = itemAttachmentDS;
            _uniqueIdentityGeneratorDS = uniqueIdentityGeneratorDS;
            _dMDocumentDS = dMDocumentDS;
            _bAItemAttachmentDS = bAItemAttachmentDS;
            _userSessionMenager = userSessionManager;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Gets the item master list by bus tenant identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<IEnumerable<BusBAItemMasterDTO>> GetItemMasterListByBusTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            IEnumerable<BusBAItemMasterDTO> itemMasterList = await _itemMasterDS.GetItemMasterListByBusTenantIdAsync(tenantId, ItemTypeConstants.CustomerItemType, token);
            return itemMasterList;
        }


        /// <summary>
        /// Gets the item master list by bus tenant identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<BusBAItemMasterViewDTO> GetItemMasterByBusTenantIdAndItemIdAsync(Guid tenantId, Guid itemId, CancellationToken token = default(CancellationToken)) {
            BusBAItemMasterViewDTO itemMaster = await _itemMasterDS.GetItemMasterByBusTenantIdAndItemIdAsync(tenantId, itemId, ItemTypeConstants.CustomerItemType, token);
            itemMaster.AttachmentList = (await _itemAttachmentDS.GetItemAttachmentListByItemIdAsync(itemId, token)).ToList();
            return itemMaster;
        }

        #endregion Get

        #region Add Item Master

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseModelDTO> AddItemMasterByBusinessAsync(HttpRequest httpRequest, string request, CancellationToken token = default(CancellationToken)) {
            return await _itemMasterDS.AddItemMasterByBusinessAsync(httpRequest, request, token);
        }

        public async Task<ResponseModelDTO> AddItemMasterByBusinessPayAppWithoutAttchAsync(BusBAItemMasterViewDTO busBAItemMasterViewDTO, CancellationToken token = default(CancellationToken)) {
            return await _itemMasterDS.AddItemMasterByBusinessPayAppWithoutAttchAsync(busBAItemMasterViewDTO, token);
        }

        #endregion add Item Master

        #region Update item Master

        public async Task<ResponseModelDTO> UpdateItemMasterByBusinessPaymentAppWithAttchAsync(HttpRequest httpRequest, string request, Guid itemId, CancellationToken token = default(CancellationToken)) {
            return await _itemMasterDS.UpdateItemMasterByBusinessPaymentAppWithAttchAsync(httpRequest, request, itemId, token);
        }

        public async Task<ResponseModelDTO> UpdateItemMasterByBusinessPaymentAppWithoutAttchAppAsync(BusBAItemMasterViewDTO busBAItemMasterViewDTO, Guid itemId, CancellationToken token = default(CancellationToken)) {
            return await _itemMasterDS.UpdateItemMasterByBusinessPaymentAppWithoutAttchAppAsync(busBAItemMasterViewDTO, itemId, token);
        }

        #endregion Update item master

        #region Vendor Methods

        public async Task<IEnumerable<BusBAItemMasterDTO>> GetVendorsItemMasterListByBusTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _itemMasterDS.GetItemMasterListByBusTenantIdAsync(tenantId, ItemTypeConstants.VendorItemType, token);
        }

        public async Task<BusBAItemMasterViewDTO> GetVendorsItemMasterByBusTenantIdAndItemIdAsync(Guid tenantId, Guid itemId, CancellationToken token = default(CancellationToken)) {
            BusBAItemMasterViewDTO itemMaster = await _itemMasterDS.GetItemMasterByBusTenantIdAndItemIdAsync(tenantId, itemId, ItemTypeConstants.VendorItemType, token);
            itemMaster.AttachmentList = (await _itemAttachmentDS.GetItemAttachmentListByItemIdAsync(itemId, token)).ToList();
            return itemMaster;
        }

        #endregion

    }

}
