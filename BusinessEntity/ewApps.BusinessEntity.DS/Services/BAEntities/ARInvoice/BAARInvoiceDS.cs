/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 16 August 2019.
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.BusinessEntity.QData;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// Class contains invoice supportive methods.
    /// 1) Conatins required get invoice methods.
    /// 2) Conatins add/update/delete methods.
    /// </summary>
    public class BAARInvoiceDS:BaseDS<BAARInvoice>, IBAARInvoiceDS {

        IBAARInvoiceRepository _arInvoiceRepo;
        IBAARInvoiceItemDS _invoiceItemDS;
        IBAARInvoiceAttachmentDS _invoiceAttachmentDS;
        IBACustomerDS _customerDS;
        IUnitOfWork _unitOfWork;
        IBusinessEntityNotificationHandler _businessEntityNotificationHandler;
        IQNotificationDS _qNotificationDS;
        IUniqueIdentityGeneratorDS _identityDataService;
        IDMDocumentDS _documentDS;
        IQBAInvoiceRepository _qBAInvoiceRepository;
        ILogger<BAARInvoiceDS> _logger;
        IUserSessionManager _sessionManager;


        /// <summary>
        /// Default constructor with ARInvoice respository parameter.
        /// </summary>
        /// <param name="arInvoiceRepo"></param>
        /// <param name="invoiceItemDS"></param>
        /// <param name="identityDataService"></param>
        /// <param name="unitOfWork"></param>
        public BAARInvoiceDS(IQNotificationDS qNotificationDS, IBAARInvoiceRepository arInvoiceRepo, IBAARInvoiceItemDS invoiceItemDS, IBAARInvoiceAttachmentDS invoiceAttachmentDS, IBACustomerDS customerDS, IUniqueIdentityGeneratorDS identityDataService, IDMDocumentDS documentDS, IQBAInvoiceRepository qBAInvoiceRepository, IUnitOfWork unitOfWork, IBusinessEntityNotificationHandler businessEntityNotificationHandler, IUserSessionManager sessionManager, ILogger<BAARInvoiceDS> logger) : base(arInvoiceRepo) {
            _arInvoiceRepo = arInvoiceRepo;
            _invoiceItemDS = invoiceItemDS;
            _invoiceAttachmentDS = invoiceAttachmentDS;
            _customerDS = customerDS;
            _identityDataService = identityDataService;
            _qBAInvoiceRepository = qBAInvoiceRepository;
            _unitOfWork = unitOfWork;
            _documentDS = documentDS;
            _businessEntityNotificationHandler = businessEntityNotificationHandler;
            _qNotificationDS = qNotificationDS;
            _sessionManager = sessionManager;
            _logger = logger;
        }

        #region Public methods

        /// <summary>
        /// Whether invoice exists.
        /// </summary>
        /// <param name="erpARInvoiceKey">ERP invoice unique key.</param>        
        /// <returns>return list of sales order entity.</returns>
        public async Task<bool> IsInvoiceExistAsync(string erpARInvoiceKey, CancellationToken token = default(CancellationToken)) {
            return await _arInvoiceRepo.IsInvoiceExistAsync(erpARInvoiceKey, token);
        }

        /// <summary>
        /// Get invoice attachment list.
        /// </summary>
        /// <param name="invoiceId">Invoice id.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BusBAARInvoiceAttachmentDTO>> GetInvoiceAttachmentListByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qBAInvoiceRepository.GetInvoiceAttachmentListByInvoiceIdAsync(invoiceId, cancellationToken);
        }

        #endregion Public Methods

        #region Add

        ///<inheritdoc/>
        public async Task AddInvoiceListAsync(List<BAARInvoiceSyncDTO> invoiceList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken)) {
            List<string> invoiceAddERPKeyList = new List<string>();
            List<string> invoiceUpdateERPKeyList = new List<string>();
            for(int i = 0; i < invoiceList.Count; i++) {
                if(isBulkInsert) {
                    bool oldInvoice = await AddInvoiceAsync(invoiceList[i], tenantId, tenantUserId);
                }
                else {
                    if(invoiceList[i].OpType.Equals("Inserted")) {
                        bool oldInvoice = await AddUpdateInvoiceAsync(invoiceList[i], tenantId, tenantUserId);
                        if(oldInvoice == true) {
                            invoiceAddERPKeyList.Add(invoiceList[i].ERPARInvoiceKey);
                        }
                    }
                    else if(invoiceList[i].OpType.Equals("Modified")) {
                        invoiceUpdateERPKeyList.Add(invoiceList[i].ERPARInvoiceKey);
                        await AddUpdateInvoiceAsync(invoiceList[i], tenantId, tenantUserId);
                    }
                }

            }
            //save data
            _unitOfWork.SaveAll();

            //if(isBulkInsert) {
            //    //   OnBulkAddARInvoiceInIntegratedModeAsync(invoiceERPKeyList, tenantId);
            //}
            //else {
            if(invoiceAddERPKeyList.Count > 0) {
                OnAddARInvoiceInIntegratedModeAsync(invoiceAddERPKeyList, tenantId);
            }
            if(invoiceUpdateERPKeyList.Count > 0) {
                OnUpdateARInvoiceInIntegratedModeAsync(invoiceUpdateERPKeyList, tenantId);
            }

            // }
        }

        /// <summary>
        /// add invoice and its child data .
        /// </summary>
        /// <param name="invoiceSyncDTO"></param>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> AddInvoiceAsync(BAARInvoiceSyncDTO invoiceSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {
            BAARInvoice invoice = await FindAsync(inv => inv.ERPARInvoiceKey == invoiceSyncDTO.ERPARInvoiceKey && inv.TenantId == tenantId);
            if(invoice != null) {
                return false;
            }
            BACustomer customer = await _customerDS.FindAsync(cust => cust.ERPCustomerKey == invoiceSyncDTO.ERPCustomerKey && cust.TenantId == tenantId);
      if (customer == null)
      {
        return false;
      }
      invoiceSyncDTO.CustomerId = customer.ID;
            invoice = BAARInvoiceSyncDTO.MapToEntity(invoiceSyncDTO);

            // UpdateSystemFieldsByOpType(salesOrder, OperationType.Add);
            invoice.ID = Guid.NewGuid();
            invoice.CreatedBy = tenantUserId;//;// Session
            invoice.UpdatedBy = tenantUserId;
            invoice.CreatedOn = DateTime.UtcNow;
            invoice.UpdatedOn = DateTime.UtcNow;
            invoice.Deleted = false;
            invoice.TenantId = tenantId;

            // add customer detail
            await AddAsync(invoice, token);

            //Add customer address detail.
            if(invoiceSyncDTO.InvoiceItemList != null && invoiceSyncDTO.InvoiceItemList.Count > 0) {
                await _invoiceItemDS.AddInvoiceItemListAsync(invoiceSyncDTO.InvoiceItemList, tenantId, tenantUserId, invoice.ID);
            }

            //Add customer address detail.
            if(invoiceSyncDTO.Attachments != null && invoiceSyncDTO.Attachments.Count > 0) {
                await _invoiceAttachmentDS.AddARInvoiceAttachmentListAsync(invoiceSyncDTO.Attachments, tenantId, tenantUserId, invoice.ID);
            }


            return true;

        }
    private async Task<bool> AddUpdateInvoiceAsync(BAARInvoiceSyncDTO invoiceSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)){
      BAARInvoice invoice = await FindAsync(inv => inv.ERPARInvoiceKey == invoiceSyncDTO.ERPARInvoiceKey && inv.TenantId == tenantId);
      if (invoice != null)
      {
        await UpdateInvoiceAsync(invoiceSyncDTO, tenantId, tenantUserId);
      }
      else{
        await AddInvoiceAsync(invoiceSyncDTO, tenantId, tenantUserId);
      }
        return true;
    }
    
      #endregion Add

      #region Add Invoice

      /// <summary>
      /// Add invoice when application run in standalone.
      /// </summary>
      /// <param name="invoiceDTO">Invoice add object.</param>
      /// <param name="token"></param>
      /// <returns></returns>
      public async Task<InvoiceResponseDTO> AddInvoiceAsync(AddBAARInvoiceDTO invoiceDTO, CancellationToken token = default(CancellationToken)) {
            BAARInvoice invoice = null;
            BACustomer customer = await _customerDS.GetAsync(invoiceDTO.CustomerId, token);

            invoice = AddBAARInvoiceDTO.MapToEntity(invoiceDTO);
            int identity = _identityDataService.GetIdentityNo(Guid.Empty, (int)EntityTypeEnum.BAARInvoice, "INV", 1000);
            invoice.ERPARInvoiceKey = BusinessEntityConstants.InvoicePrefix + identity;
            invoice.ERPDocNum = BusinessEntityConstants.InvoicePrefix + identity;


            UpdateSystemFieldsByOpType(invoice, OperationType.Add);
            //invoice.ID = Guid.NewGuid();
            //invoice.CreatedBy = tenantUserId;//;// Session
            //invoice.UpdatedBy = tenantUserId;
            //invoice.CreatedOn = DateTime.UtcNow;
            //invoice.UpdatedOn = DateTime.UtcNow;
            //invoice.Deleted = false;
            //invoice.TenantId = tenantId;

            // add customer detail
            await AddAsync(invoice, token);

            //Add customer address detail.
            if(invoiceDTO.invoiceItems != null && invoiceDTO.invoiceItems.Count > 0) {
                await _invoiceItemDS.AddBAARInvoiceItemListAsync(invoiceDTO.invoiceItems, invoice.TenantId, invoice.CreatedBy, invoice.ID, invoice.ERPARInvoiceKey, token);
            }

            _unitOfWork.Save();

            try {
                UserSession session = _sessionManager.GetSession();
                ARInvoiceNotificationDTO aRInvoiceNotificationDTO = await _qNotificationDS.GetARInvoiceDetailByInvoiceERPKeyAsync(invoice.ERPDocNum, session.TenantId, AppKeyEnum.pay.ToString(), token);
                _businessEntityNotificationHandler.SendAddARInvoiceToBizUser(aRInvoiceNotificationDTO);
                _businessEntityNotificationHandler.SendAddARInvoiceToCustomerUserInIntegratedMode(aRInvoiceNotificationDTO);
            }
            catch(Exception Ex) {
            }

            InvoiceResponseDTO resDto = new InvoiceResponseDTO();
            resDto.InvoiceId = invoice.ID;
            resDto.CustomerId = invoice.CustomerId;
            resDto.ERPInvoiceDocNum = invoice.ERPDocNum;
            resDto.ERPInvoiceKey = invoice.ERPARInvoiceKey;
            resDto.InvoiceEntityType = (int)EntityTypeEnum.BAARInvoice;
            return resDto;
        }


        /// <summary>
        /// Add invoice.
        /// </summary>
        /// <param name="httpRequest">httpRequest object</param>
        /// <param name="token">Cancellation token for async operations</param>   
        public async Task<InvoiceResponseDTO> AddInvoiceAsync(HttpRequest httpRequest, string request, CancellationToken token = default(CancellationToken)) {
            AddBAARInvoiceDTO invoiceDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<AddBAARInvoiceDTO>(request);
            BAARInvoice invoice = null;
            BACustomer customer = await _customerDS.GetAsync(invoiceDTO.CustomerId, token);

            invoice = AddBAARInvoiceDTO.MapToEntity(invoiceDTO);
            int identity = _identityDataService.GetIdentityNo(Guid.Empty, (int)EntityTypeEnum.BAARInvoice, "INV", 1000);
            invoice.ERPARInvoiceKey = BusinessEntityConstants.InvoicePrefix + identity;
            invoice.ERPDocNum = BusinessEntityConstants.InvoicePrefix + identity;


            UpdateSystemFieldsByOpType(invoice, OperationType.Add);
            //invoice.ID = Guid.NewGuid();
            //invoice.CreatedBy = tenantUserId;//;// Session
            //invoice.UpdatedBy = tenantUserId;
            //invoice.CreatedOn = DateTime.UtcNow;
            //invoice.UpdatedOn = DateTime.UtcNow;
            //invoice.Deleted = false;
            //invoice.TenantId = tenantId;

            // add customer detail
            await AddAsync(invoice, token);

            //Add customer address detail.
            if(invoiceDTO.invoiceItems != null && invoiceDTO.invoiceItems.Count > 0) {
                await _invoiceItemDS.AddBAARInvoiceItemListAsync(invoiceDTO.invoiceItems, invoice.TenantId, invoice.CreatedBy, invoice.ID, invoice.ERPARInvoiceKey, token);
            }

            if(invoiceDTO.listAttachment != null && invoiceDTO.listAttachment.Count > 0) {
                int i = 0;
                Guid storageId = Guid.Empty;
                foreach(IFormFile file in httpRequest.Form.Files) {
                    invoiceDTO.listAttachment[i].DocumentId = Guid.NewGuid();
                    _documentDS.UploadDocumentFileToStorage(file.OpenReadStream(), invoiceDTO.listAttachment[i], true);
                    await AddInvoiceAttachmentAsync(invoiceDTO.listAttachment[i].DocumentId, invoice, file, token);
                    i = i + 1;
                }

                //await _invoiceAttachmentDS.AddARInvoiceAttachmentListAsync(invoiceSyncDTO.Attachments, tenantId, tenantUserId, invoice.ID);
            }
            _unitOfWork.Save();

            try {
                UserSession session = _sessionManager.GetSession();
                ARInvoiceNotificationDTO aRInvoiceNotificationDTO = await _qNotificationDS.GetARInvoiceDetailByInvoiceERPKeyAsync(invoice.ERPDocNum, session.TenantId, AppKeyEnum.pay.ToString(), token);
                _businessEntityNotificationHandler.SendAddARInvoiceToBizUser(aRInvoiceNotificationDTO);
                _businessEntityNotificationHandler.SendAddARInvoiceToCustomerUserInIntegratedMode(aRInvoiceNotificationDTO);
            }
            catch { }

            InvoiceResponseDTO resDto = new InvoiceResponseDTO();
            resDto.InvoiceId = invoice.ID;
            resDto.CustomerId = invoice.CustomerId;
            resDto.ERPInvoiceDocNum = invoice.ERPDocNum;
            resDto.ERPInvoiceKey = invoice.ERPARInvoiceKey;
            resDto.InvoiceEntityType = (int)EntityTypeEnum.BAARInvoice;
            return resDto;
        }

        private async Task AddInvoiceAttachmentAsync(Guid attachmentId, BAARInvoice invoice, IFormFile file, CancellationToken token) {
            int identity = _identityDataService.GetIdentityNo(Guid.Empty, (int)EntityTypeEnum.BAARInvoice, "INV", 1000);

            BAARInvoiceAttachment attachment = new BAARInvoiceAttachment();
            attachment.ID = attachmentId;
            attachment.ARInvoiceId = invoice.ID;
            attachment.Name = file.FileName;
            attachment.ERPARInvoiceAttachmentKey = "ATT" + identity;
            attachment.ERPARInvoiceKey = invoice.ERPARInvoiceKey;
            attachment.TenantId = invoice.TenantId;
            attachment.CreatedBy = invoice.CreatedBy;
            attachment.UpdatedBy = invoice.UpdatedBy;
            attachment.CreatedOn = invoice.UpdatedOn;
            attachment.UpdatedOn = invoice.UpdatedOn;
            attachment.AttachmentDate = DateTime.UtcNow;
            await _invoiceAttachmentDS.AddAsync(attachment, token);
        }

        /// <summary>
        /// Update invoice when make payment in standalone.
        /// </summary>
        /// <param name="listinvoiceDTO">Invoice object.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<object> UpdateInvoiceAmountAsync(List<BAARInvoiceEntityDTO> listinvoiceDTO, CancellationToken token = default(CancellationToken)) {

            for(int i = 0; i < listinvoiceDTO.Count; i++) {
                BAARInvoice invoice = await _arInvoiceRepo.GetAsync(listinvoiceDTO[i].ID, token);
                if(invoice != null) {
                    invoice.BalanceDue = listinvoiceDTO[i].BalanceDue;
                    invoice.BalanceDueFC = listinvoiceDTO[i].BalanceDueFC;
                    invoice.AppliedAmount = listinvoiceDTO[i].AppliedAmount;
                    invoice.AppliedAmountFC = listinvoiceDTO[i].AppliedAmountFC;
                    if(listinvoiceDTO[i].UpdatedBy != Guid.Empty) {
                        invoice.UpdatedBy = listinvoiceDTO[i].UpdatedBy;
                    }

                    invoice.UpdatedOn = DateTime.UtcNow;
                    if(invoice.BalanceDue <= 0) {
                        invoice.StatusText = BusinessEntityConstants.ClosedStatus;
                    }
                    await _arInvoiceRepo.UpdateAsync(invoice, invoice.ID, token);
                }
            }
            Save();
            return true;
        }

        /// <summary>
        /// Cancel invoice by invoice id.
        /// </summary>
        /// <param name="invoiceId">Invoiceid</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<object> CancelInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            BAARInvoice invoice = await _arInvoiceRepo.GetAsync(invoiceId, token);
            invoice.StatusText = Core.CommonService.Constants.CanceledInvoiceText;

            await _arInvoiceRepo.UpdateAsync(invoice, invoice.ID, token);
            Save();
            return true;
        }

        #endregion Add Invoice

        #region Delete Attachment

        /// <summary>
        /// Deletes document with all child reference like Thumbnail, Comment etc.
        /// </summary>
        /// <param name="documentId"></param>
        public async Task DeleteInvoiceAttachment(Guid documentId, CancellationToken token = default(CancellationToken)) {
            BAARInvoiceAttachment doc = await _invoiceAttachmentDS.GetAsync(documentId, token);
            if(doc != null) {
                await _invoiceAttachmentDS.DeleteAsync(documentId, token);
                _invoiceAttachmentDS.Save();
                try {

                    await _documentDS.DeleteFileAsync(documentId, token);
                }
                catch(Exception ex) {
                    throw ex;
                }
            }
        }

        #endregion Delete Attachment

        /// <summary>
        /// add invoice and its child data .
        /// </summary>
        /// <param name="invoiceSyncDTO"></param>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> UpdateInvoiceAsync(BAARInvoiceSyncDTO invoiceSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken)) {
            BAARInvoice invoice = await FindAsync(inv => inv.ERPARInvoiceKey == invoiceSyncDTO.ERPARInvoiceKey && inv.TenantId == tenantId);
      if (invoice != null)
      {
        invoice = BAARInvoiceSyncDTO.MapToEntity(invoiceSyncDTO, invoice);

        UpdateSystemFieldsByOpType(invoice, OperationType.Update);


        // update invoice detail
        await UpdateAsync(invoice, invoice.ID, token);
      }

      //Add customer address detail.
      if (invoiceSyncDTO.InvoiceItemList != null && invoiceSyncDTO.InvoiceItemList.Count > 0)
      {
        await _invoiceItemDS.AddInvoiceItemListAsync(invoiceSyncDTO.InvoiceItemList, tenantId, tenantUserId, invoice.ID);
      }

      ////Add customer address detail.
      //if(invoiceSyncDTO.Attachments != null && invoiceSyncDTO.Attachments.Count > 0) {
      //    await _invoiceAttachmentDS.AddARInvoiceAttachmentListAsync(invoiceSyncDTO.Attachments, tenantId, tenantUserId, invoice.ID);
      //}


      return true;

        }

        /// <summary>
        /// Invoice exist for customer.
        /// </summary>
        /// <param name="customerId">CustomerId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> IsInvoiceExistsAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _arInvoiceRepo.IsInvoiceExistsAsync(customerId, token);
        }

        #region Get

        /// <summary>
        /// Get invoice.
        /// </summary>
        /// <param name="erpARInvoiceKey">ERP invoice unique key.</param>        
        /// <returns>return invoice entity.</returns>
        public async Task<BAARInvoice> GetInvoiceByERPInvoiceKeyAsync(string erpARInvoiceKey, CancellationToken token = default(CancellationToken)) {
            return await _arInvoiceRepo.GetInvoiceByERPInvoiceKeyAsync(erpARInvoiceKey, token);
        }

        /// <summary>
        /// Get invoice list.
        /// Invoice list will be filter by Tenant and from/todate.
        /// </summary>
        /// <param name="filter">Contains filter clause to get filter invoice list.</param>
        /// <param name="tenatId">Tenant id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDQ>> GetInvoiceByTenant(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken)) {
            return await _arInvoiceRepo.GetInvoiceByTenant(filter, tenatId, token);
        }

        #endregion Get

        #region Notification

        private void OnAddARInvoiceInIntegratedModeAsync(List<string> invoiceERPKeyList, Guid businessTenantId) {
            for(int i = 0; i < invoiceERPKeyList.Count; i++) {
                ARInvoiceNotificationDTO aRInvoiceNotificationDTO = _qNotificationDS.GetARInvoiceDetailByInvoiceERPKeyAsync(invoiceERPKeyList[i], businessTenantId, AppKeyEnum.pay.ToString()).Result;
                _businessEntityNotificationHandler.SendAddARInvoiceToBizUser(aRInvoiceNotificationDTO);
                _businessEntityNotificationHandler.SendAddARInvoiceToCustomerUserInIntegratedMode(aRInvoiceNotificationDTO);
            }
        }

        //private void OnBulkAddARInvoiceInIntegratedModeAsync(List<string> invoiceERPKeyList, Guid businessTenantId) {
        //    NotificationCommonDetailDTO notificationCommonDetailDTO = _qNotificationDS.GetNotificationCommonDetailDTOAsync(businessTenantId, AppKeyEnum.pay.ToString()).Result;
        //    // ToDo: Change hardcoded parameters.
        //    _businessEntityNotificationHandler.SendBulkAddARInvoiceToBizPaymentUserInIntegratedMode(notificationCommonDetailDTO, invoiceERPKeyList.Count, businessTenantId, "Nitin", "ABCd", DateTime.UtcNow);
        //}

        private void OnUpdateARInvoiceInIntegratedModeAsync(List<string> invoiceERPKeyList, Guid businessTenantId) {
            for(int i = 0; i < invoiceERPKeyList.Count; i++) {
                ARInvoiceNotificationDTO aRInvoiceNotificationDTO = _qNotificationDS.GetARInvoiceDetailByInvoiceERPKeyAsync(invoiceERPKeyList[i], businessTenantId, AppKeyEnum.pay.ToString()).Result;
                _businessEntityNotificationHandler.SendUpdateARInvoiceToBizPaymentUserInIntegratedMode(aRInvoiceNotificationDTO);
            }
        }

        #endregion

    }
}

