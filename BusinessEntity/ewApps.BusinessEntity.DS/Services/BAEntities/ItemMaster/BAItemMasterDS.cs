using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using System.Linq;
using System.Linq.Expressions;
using ewApps.BusinessEntity.QData;
using Microsoft.AspNetCore.Http;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.BusinessEntity.Common;
using ewApps.Core.UserSessionService;
using ewApps.Core.DMService;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// This class implements standard business logic and operations for BAItemMaster entity.
    /// </summary>
    public class BAItemMasterDS:BaseDS<BAItemMaster>, IBAItemMasterDS {

        #region Local Member

        IBAItemMasterRepository _itemMasterRepo;
        IQBAItemMasterRepository _iQBAItemMasterRepo;
        IUniqueIdentityGeneratorDS _uniqueIdentityGeneratorDS;
        IUserSessionManager _userSessionMenager;
        IDMDocumentDS _dMDocumentDS;
        IBAItemAttachmentDS _bAItemAttachmentDS;
        IUnitOfWork _unitOfWork;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initialinzing local variables .
        /// </summary>
        /// <param name="itemMasterRepo"></param>
        /// <param name="unitOfWork"></param>
        public BAItemMasterDS(IBAItemMasterRepository itemMasterRepo, IQBAItemMasterRepository iQBAItemMasterRepo, IUniqueIdentityGeneratorDS uniqueIdentityGeneratorDS, IUserSessionManager userSessionManager, IDMDocumentDS dMDocumentDS, IBAItemAttachmentDS bAItemAttachmentDS, IUnitOfWork unitOfWork) : base(itemMasterRepo) {
            _itemMasterRepo = itemMasterRepo;
            _iQBAItemMasterRepo = iQBAItemMasterRepo;
            _uniqueIdentityGeneratorDS = uniqueIdentityGeneratorDS;
            _userSessionMenager = userSessionManager;
            _dMDocumentDS = dMDocumentDS;
            _bAItemAttachmentDS = bAItemAttachmentDS;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        #region Add

        ///<inheritdoc/>
        public async Task AdditemMasterListAsync(List<BAItemMasterSyncDTO> itemMasterList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken)) {
            List<string> itemAddERPKeyList = new List<string>();
            List<string> itemUpdateERPKeyList = new List<string>();
            for(int i = 0; i < itemMasterList.Count; i++) {

                if(isBulkInsert) {
                    await AddItemMasterAsync(itemMasterList[i], tenantId, tenantUserId);
                }
                else {
                    if(itemMasterList[i].OpType.Equals("Inserted")) {
                        bool oldData = await AddUpdateItemMasterAsync(itemMasterList[i], tenantId, tenantUserId);
                        if(oldData) {
                            itemAddERPKeyList.Add(itemMasterList[i].ERPItemKey);
                        }
                    }
                    else if(itemMasterList[i].OpType.Equals("Modified")) {
                        itemUpdateERPKeyList.Add(itemMasterList[i].ERPItemKey);
                        await AddUpdateItemMasterAsync(itemMasterList[i], tenantId, tenantUserId);
                    }
                }
            }
            //save data
            _unitOfWork.SaveAll();


        }

        /// <summary>
        /// Add item master data.
        /// </summary>
        /// <param name="itemMasterSyncDTO"></param>
        /// <param name="tenantId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> AddItemMasterAsync(BAItemMasterSyncDTO itemMasterSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {
            BAItemMaster itemMaster = await FindAsync(im => im.ERPItemKey == itemMasterSyncDTO.ERPItemKey && im.TenantId == tenantId);
            if(itemMaster != null) {
                return false;
            }
            itemMaster = BAItemMasterSyncDTO.MapToEntity(itemMasterSyncDTO);

            //UpdateSystemFieldsByOpType(itemMaster, OperationType.Add);

            itemMaster.ID = Guid.NewGuid();
            itemMaster.CreatedBy = tenantUserId;
            itemMaster.UpdatedBy = tenantUserId;
            itemMaster.CreatedOn = DateTime.UtcNow;
            itemMaster.UpdatedOn = DateTime.UtcNow;
            itemMaster.Deleted = false;
            itemMaster.TenantId = tenantId;

            await AddAsync(itemMaster, token);

            //Add customer address detail.
            if(itemMasterSyncDTO.Attachments != null && itemMasterSyncDTO.Attachments.Count > 0) {
                await _bAItemAttachmentDS.AddItemAttachmentListAsync(itemMasterSyncDTO.Attachments, tenantId, tenantUserId, itemMaster.ID);
            }

            return true;
        }

    #endregion Add    
    private async Task<bool> AddUpdateItemMasterAsync(BAItemMasterSyncDTO itemMasterSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      BAItemMaster itemMaster = await FindAsync(im => im.ERPItemKey == itemMasterSyncDTO.ERPItemKey && im.TenantId == tenantId);
      if (itemMaster != null)
      {
        await UpdateItemMasterAsync(itemMasterSyncDTO, tenantId, tenantUserId);
      }
      else
      {
        await AddItemMasterAsync(itemMasterSyncDTO, tenantId, tenantUserId);
      }
      return true;
    }

    /// <summary>
    /// Add item master data.
    /// </summary>
    /// <param name="itemMasterSyncDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="tenantUserId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task<bool> UpdateItemMasterAsync(BAItemMasterSyncDTO itemMasterSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {
            BAItemMaster itemMaster = await FindAsync(im => im.ERPItemKey == itemMasterSyncDTO.ERPItemKey && im.TenantId == tenantId);

            itemMaster = BAItemMasterSyncDTO.MapToEntity(itemMasterSyncDTO, itemMaster);

            UpdateSystemFieldsByOpType(itemMaster, OperationType.Update);


            await UpdateAsync(itemMaster, itemMaster.ID, token);

            if(itemMasterSyncDTO.Attachments != null && itemMasterSyncDTO.Attachments.Count > 0) {
                //await _bAItemAttachmentDS.AddItemAttachmentListAsync(itemMasterSyncDTO.Attachments, tenantId, tenantUserId, itemMaster.ID);
            }

            return true;
        }

        #region Get for Business

        /// <summary>
        /// Gets the item master list by bus tenant identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<IEnumerable<BusBAItemMasterDTO>> GetItemMasterListByBusTenantIdAsync(Guid tenantId, string itemType, CancellationToken token = default(CancellationToken)) {
            IEnumerable<BusBAItemMasterDTO> itemMasterList = _itemMasterRepo.GetItemMasterListByBusTenantId(tenantId, itemType);
            return itemMasterList;
        }

        /// <summary>
        /// Gets the item master list by bus tenant identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<BusBAItemMasterViewDTO> GetItemMasterByBusTenantIdAndItemIdAsync(Guid tenantId, Guid itemId, string itemType, CancellationToken token = default(CancellationToken)) {
            return _iQBAItemMasterRepo.GetItemMasterByBusTenantIdAndItemId(tenantId, itemId, itemType);
        }

        #endregion Get for Business


        #region Add Item Master Business payment app

        /// <summary>
        /// Add item master business portal payment application
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseModelDTO> AddItemMasterByBusinessAsync(HttpRequest httpRequest, string request, CancellationToken token = default(CancellationToken)) {

            BusBAItemMasterViewDTO busBAItemMasterViewDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<BusBAItemMasterViewDTO>(request);
            BAItemMaster itemMaster = new BAItemMaster();

            itemMaster = BusBAItemMasterViewDTO.MapToEntity(busBAItemMasterViewDTO);
            itemMaster.TenantId = _userSessionMenager.GetSession().TenantId;
            itemMaster.ERPConnectorKey = busBAItemMasterViewDTO.ItemType;// TO-DO

            int identity = _uniqueIdentityGeneratorDS.GetIdentityNo(Guid.Empty, (int)EntityTypeEnum.BAItemMaster, "IMS", 1000);
            itemMaster.ERPItemKey = BusinessEntityConstants.ItemMasterPrefix + identity;

            UpdateSystemFieldsByOpType(itemMaster, OperationType.Add);

            if(busBAItemMasterViewDTO.listAttachment != null && busBAItemMasterViewDTO.listAttachment.Count > 0) {
                int i = 0;
                Guid storageId = Guid.Empty;
                foreach(IFormFile file in httpRequest.Form.Files) {
                    busBAItemMasterViewDTO.listAttachment[i].DocumentId = Guid.NewGuid();
                    _dMDocumentDS.UploadDocumentFileToStorage(file.OpenReadStream(), busBAItemMasterViewDTO.listAttachment[i], true);
                    await AddItemMasterAttachmentAsync(busBAItemMasterViewDTO.listAttachment[i].DocumentId, itemMaster, file, token);
                    i = i + 1;
                }
            }
            // add Item master detail
            await AddAsync(itemMaster, token);
            _unitOfWork.Save();

            ResponseModelDTO resDto = new ResponseModelDTO();
            return resDto;
        }


        private async Task AddItemMasterAttachmentAsync(Guid attachmentId, BAItemMaster itemMaster, IFormFile file, CancellationToken token) {
            int identity = _uniqueIdentityGeneratorDS.GetIdentityNo(Guid.Empty, (int)EntityTypeEnum.BAItemMaster, "IMS", 1000);
            BAItemMasterAttachment attachment = new BAItemMasterAttachment();
            attachment.ID = attachmentId;
            attachment.ItemId = itemMaster.ID;
            attachment.Name = file.FileName;
            attachment.ERPItemAttachmentKey = "IMS" + identity;
            attachment.ERPItemKey = itemMaster.ERPItemKey;
            attachment.TenantId = itemMaster.TenantId;
            attachment.CreatedBy = itemMaster.CreatedBy;
            attachment.UpdatedBy = itemMaster.UpdatedBy;
            attachment.CreatedOn = itemMaster.UpdatedOn;
            attachment.UpdatedOn = itemMaster.UpdatedOn;
            attachment.AttachmentDate = DateTime.UtcNow;
            await _bAItemAttachmentDS.AddAsync(attachment, token);
        }


        public async Task<ResponseModelDTO> AddItemMasterByBusinessPayAppWithoutAttchAsync(BusBAItemMasterViewDTO busBAItemMasterViewDTO, CancellationToken token = default(CancellationToken)) {

            BAItemMaster itemMaster = new BAItemMaster();
            itemMaster = BusBAItemMasterViewDTO.MapToEntity(busBAItemMasterViewDTO);
            itemMaster.TenantId = _userSessionMenager.GetSession().TenantId;
            itemMaster.ERPConnectorKey = busBAItemMasterViewDTO.ItemType;// TO-DO

            int identity = _uniqueIdentityGeneratorDS.GetIdentityNo(Guid.Empty, (int)EntityTypeEnum.BAItemMaster, "IMS", 1000);
            itemMaster.ERPItemKey = BusinessEntityConstants.ItemMasterPrefix + identity;

            UpdateSystemFieldsByOpType(itemMaster, OperationType.Add);
            // add Item master detail
            await AddAsync(itemMaster, token);
            _unitOfWork.Save();

            ResponseModelDTO resDto = new ResponseModelDTO();
            return resDto;
        }

        #endregion Add Item Master Business payment app


        #region Update item master


        public async Task<ResponseModelDTO> UpdateItemMasterByBusinessPaymentAppWithAttchAsync(HttpRequest httpRequest, string request, Guid itemId, CancellationToken token = default(CancellationToken)) {

            BusBAItemMasterViewDTO busBAItemMasterViewDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<BusBAItemMasterViewDTO>(request);
            BAItemMaster itemMaster = new BAItemMaster();

            // itemMaster = BusBAItemMasterViewDTO.MapToEntity(busBAItemMasterViewDTO);
            // Get ItemMaster Entity Information
            itemMaster = _itemMasterRepo.Get(itemId);
            itemMaster = BusBAItemMasterViewDTO.UpdateMapToEntity(busBAItemMasterViewDTO, itemMaster);

            itemMaster.TenantId = _userSessionMenager.GetSession().TenantId;
            itemMaster.ERPConnectorKey = busBAItemMasterViewDTO.ItemType;// TO-DO

            // int identity = _uniqueIdentityGeneratorDS.GetIdentityNo(Guid.Empty, (int)EntityTypeEnum.BAItemMaster, "IMS", 1000);
            // itemMaster.ERPItemKey = BusinessEntityConstants.ItemMasterPrefix + identity;

            UpdateSystemFieldsByOpType(itemMaster, OperationType.Update);

            if(busBAItemMasterViewDTO.listAttachment != null && busBAItemMasterViewDTO.listAttachment.Count > 0) {
                int i = 0;
                Guid storageId = Guid.Empty;
                foreach(IFormFile file in httpRequest.Form.Files) {
                    //busBAItemMasterViewDTO.listAttachment[i].DocumentId = Guid.NewGuid();
                    _dMDocumentDS.UploadDocumentFileToStorage(file.OpenReadStream(), busBAItemMasterViewDTO.listAttachment[i], true);
                    await UpdateItemMasterAttachmentAsync(busBAItemMasterViewDTO.listAttachment[i].DocumentId, itemMaster, file, token);
                    i = i + 1;
                }
            }
            // add Item master detail
            await UpdateAsync(itemMaster, itemMaster.ID, token);
            _unitOfWork.Save();

            ResponseModelDTO resDto = new ResponseModelDTO();
            return resDto;
        }

        private async Task UpdateItemMasterAttachmentAsync(Guid attachmentId, BAItemMaster itemMaster, IFormFile file, CancellationToken token) {
            int identity = _uniqueIdentityGeneratorDS.GetIdentityNo(Guid.Empty, (int)EntityTypeEnum.BAItemMaster, "IMS", 1000);
            BAItemMasterAttachment attachment = new BAItemMasterAttachment();
            attachment.ID = attachmentId;
            attachment.ItemId = itemMaster.ID;
            attachment.Name = file.FileName;
            attachment.ERPItemAttachmentKey = "IMS" + identity;
            attachment.ERPItemKey = itemMaster.ERPItemKey;
            attachment.TenantId = itemMaster.TenantId;
            attachment.CreatedBy = itemMaster.CreatedBy;
            attachment.UpdatedBy = itemMaster.UpdatedBy;
            attachment.CreatedOn = itemMaster.UpdatedOn;
            attachment.UpdatedOn = itemMaster.UpdatedOn;
            attachment.AttachmentDate = DateTime.UtcNow;
            await _bAItemAttachmentDS.UpdateAsync(attachment, token);
        }


        public async Task<ResponseModelDTO> UpdateItemMasterByBusinessPaymentAppWithoutAttchAppAsync(BusBAItemMasterViewDTO busBAItemMasterViewDTO, Guid itemId, CancellationToken token = default(CancellationToken)) {

            BAItemMaster itemMaster = new BAItemMaster();

            //Get ItemMaster Entity Information
            itemMaster = _itemMasterRepo.Get(itemId);

            itemMaster = BusBAItemMasterViewDTO.UpdateMapToEntity(busBAItemMasterViewDTO, itemMaster);
            itemMaster.TenantId = _userSessionMenager.GetSession().TenantId;
            itemMaster.ERPConnectorKey = busBAItemMasterViewDTO.ItemType;// TO-DO

            // int identity = _uniqueIdentityGeneratorDS.GetIdentityNo(Guid.Empty, (int)EntityTypeEnum.BAItemMaster, "IMS", 1000);
            // itemMaster.ERPItemKey = BusinessEntityConstants.ItemMasterPrefix + identity;

            UpdateSystemFieldsByOpType(itemMaster, OperationType.Update);
            // Update Item master detail
            await UpdateAsync(itemMaster, itemMaster.ID, token);
            _unitOfWork.Save();

            ResponseModelDTO resDto = new ResponseModelDTO();
            return resDto;
        }

        #endregion update item master


        #region Get for Cust

        /// <summary>
        /// Gets the item master list by bus tenant identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>

        public async Task<IEnumerable<CustBAItemMasterDTO>> GetItemMasterListByBusTenantIdAsyncForCust(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            IEnumerable<CustBAItemMasterDTO> itemMasterList = _itemMasterRepo.GetItemMasterListByBusTenantIdForCust(tenantId);
            return itemMasterList;
        }

        /// <summary>
        /// Gets the item master list by bus tenant identifier asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="itemId">item id identifier</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<CustBAItemMasterViewDTO> GetItemMasterByBusTenantIdAndItemIdAsyncForCust(Guid tenantId, Guid itemId, CancellationToken token = default(CancellationToken)) {
            return _iQBAItemMasterRepo.GetItemMasterByBusTenantIdAndItemIdForCust(tenantId, itemId);
        }

        #endregion Get for Cust

    }
}