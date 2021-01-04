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

namespace ewApps.AppPortal.DS {
    public class NotesDS:BaseDS<Notes>, INotesDS {

        INotesRespository _notesRespository;
        IUserSessionManager _userSessionManager;
        IUnitOfWork _unitOfWork;
        IQNotesDS _qNotesDS;
        IBizNotificationHandler _bizNotificationHandler;
        ICustNotificationHandler _custNotificationHandler;
        AppPortalAppSettings _appPortalAppSettings;

        public NotesDS(INotesRespository notesRespository, IUserSessionManager userSessionManager, IUnitOfWork unitOfWork, IQNotesDS qNotesDS, IBizNotificationHandler bizNotificationHandler, IOptions<AppPortalAppSettings> appPortalAppSettings, ICustNotificationHandler custNotificationHandler) : base(notesRespository) {
            _notesRespository = notesRespository;
            _userSessionManager = userSessionManager;
            _unitOfWork = unitOfWork;
            _qNotesDS = qNotesDS;
            _bizNotificationHandler = bizNotificationHandler;
            _appPortalAppSettings = appPortalAppSettings.Value;
            _custNotificationHandler = custNotificationHandler;
        }

        public async Task<Notes> AddNotesAsync(NotesAddDTO notesAddDTO) {

            UserSession userSession = _userSessionManager.GetSession();

            Notes notes = new Notes();
            notes.Content = notesAddDTO.Content;
            notes.Deleted = false;
            notes.EntityId = notesAddDTO.EntityId;
            notes.EntityType = notesAddDTO.EntityType;
            notes.Private = notesAddDTO.Private;
            notes.System = notesAddDTO.System;
            UpdateSystemFieldsByOpType(notes, OperationType.Add);
            await AddAsync(notes);

            _unitOfWork.SaveAll();

            try {
                //Generate Notification On The Basis Of Entity Type
                await GenerateNotificationOnNotesAdd(notesAddDTO.EntityId, notesAddDTO.EntityType, notesAddDTO.Content, userSession);
            }
            catch { }

            return notes;
        }

        /// <summary>
        /// Add notes.
        /// </summary>
        /// <param name="notesList"></param>
        /// <param name="entityId"></param>
        /// <param name="entityType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task AddNotesListAsync(List<NotesAddDTO> notesList, Guid entityId, int entityType, CancellationToken token = default(CancellationToken)) {
            if(notesList != null && notesList.Count > 0) {
                UserSession userSession = _userSessionManager.GetSession();
                NotesAddDTO notesAddDTO;
                for(int i = 0; i < notesList.Count; i++) {
                    notesAddDTO = notesList[i];
                    notesAddDTO.EntityId = entityId;
                    notesAddDTO.EntityType = entityType;
                    Notes notes = new Notes();
                    notes.Content = notesAddDTO.Content;
                    notes.Deleted = false;
                    notes.EntityId = notesAddDTO.EntityId;
                    notes.EntityType = notesAddDTO.EntityType;
                    notes.Private = notesAddDTO.Private;
                    notes.System = notesAddDTO.System;
                    UpdateSystemFieldsByOpType(notes, OperationType.Add);
                    await AddAsync(notes, token);
                }
                _unitOfWork.SaveAll();
                try {
                    for(int i = 0; i < notesList.Count; i++) {
                        notesAddDTO = notesList[i];
                        await GenerateNotificationOnNotesAdd(notesAddDTO.EntityId, notesAddDTO.EntityType, notesAddDTO.Content, userSession);
                    }
                }
                catch { }


            }
        }

        public async Task<List<NotesViewDTO>> GetNotesViewListByEntityId(Guid entityId, Guid tenantId) {
            return await _qNotesDS.GetNotesViewListByEntityId(entityId, tenantId);
        }

        //Generate Notication When New Note Create On Any Entity
        private async Task GenerateNotificationOnNotesAdd(Guid entityId, int entityType, string comment, UserSession userSession) {

            Guid custAppId = new Guid();
            Guid payAppId = new Guid();

            //Get AppInfo
            List<AppInfoDTO> appInfoDTOs = await GetAppInfoByAppKey(userSession.ID);
            if(appInfoDTOs != null) {
                foreach(AppInfoDTO appInfoDTO in appInfoDTOs) {
                    if(appInfoDTO.AppKey.Equals(AppKeyEnum.pay.ToString()))
                        payAppId = appInfoDTO.Id;
                    if(appInfoDTO.AppKey.Equals(AppKeyEnum.cust.ToString()))
                        custAppId = appInfoDTO.Id;
                }
            }

            //Generate Notification On The Basis Of EntityType
            switch(entityType) {
                case (int)EntityTypeEnum.BAARInvoice:
                    await GenerateARInviceForBusiness(entityId, (int)EntityTypeEnum.BAARInvoice, payAppId, comment, userSession);
                    break;
                    //case (int)EntityTypeEnum.BASalesQuotation:
                    //    await GenerateSalesQuotationForBusinessCustomer(entityId, (int)EntityTypeEnum.BASalesQuotation, custAppId, comment, userSession);
                    //    break;
                    //case (int)EntityTypeEnum.BASalesOrder:
                    //    await GenerateSalesOrdersForBusinessCustomer(entityId, (int)EntityTypeEnum.BASalesOrder, custAppId, comment, userSession);
                    //    break;
                    //case (int)EntityTypeEnum.BADelivery:
                    //    await GenerateDeliveryForBusinessCustomer(entityId, (int)EntityTypeEnum.BADelivery, custAppId, comment, userSession);
                    //    break;
                    //case (int)EntityTypeEnum.BAContract:
                    //    await GenerateContractForBusinessCustomer(entityId, (int)EntityTypeEnum.BAContract, custAppId, comment, userSession);
                    //    break;
            }


        }


        /// <summary>
        /// Generate AR Invoice Notification For Business payment
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="appId"></param>
        /// <param name="comment"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        private async Task GenerateARInviceForBusiness(Guid entityId, int entityType, Guid appId, string comment, UserSession userSession) {
            BusinessNotesNotificationDTO businessARInvoiceNotificationDTO = await _qNotesDS.GetBusinessUserListByEntityId(entityId, userSession.TenantId, appId, userSession.TenantUserId, userSession.UserType);
            if(businessARInvoiceNotificationDTO != null) {
                businessARInvoiceNotificationDTO.UserSessionInfo = userSession;
                businessARInvoiceNotificationDTO.Comment = comment;
                businessARInvoiceNotificationDTO.EntityId = entityId;
                businessARInvoiceNotificationDTO.EntityType = entityType;
                if(userSession.UserType == (int)UserTypeEnum.Business) {
                    await _bizNotificationHandler.SendBizPaymentUserOnNotesAddedNotificationAsync(businessARInvoiceNotificationDTO, (long)BizNotificationEventEnum.AddNoteOnARInvoiceForBizUser);
                    await _bizNotificationHandler.SendCustUserOnNotesAddedNotificationAsync(businessARInvoiceNotificationDTO, (long)BizNotificationEventEnum.AddNoteOnAPInvoiceForCustomerUser);
                }
                else {
                    await _custNotificationHandler.SendCustUserOnNotesAddedNotificationAsync(businessARInvoiceNotificationDTO, (long)CustNotificationEventEnum.AddNoteOnAPInvoiceForCustomerUser);
                    await _custNotificationHandler.SendBizUserOnNotesAddedNotificationAsync(businessARInvoiceNotificationDTO, (long)CustNotificationEventEnum.AddNoteOnARInvoiceForBizUser);
                }

            }
        }


        /// <summary>
        /// Genenrate Sales Quotation For Business Customer 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="appId"></param>
        /// <param name="comment"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        private async Task GenerateSalesQuotationForBusinessCustomer(Guid entityId, int entityType, Guid appId, string comment, UserSession userSession) {
            BusinessNotesNotificationDTO businessARInvoiceNotificationDTO = await _qNotesDS.GetBusinessInfoWithQuotationByEntityId(entityId, userSession.TenantId, appId, userSession.TenantUserId, userSession.UserType);
            if(businessARInvoiceNotificationDTO != null) {
                businessARInvoiceNotificationDTO.UserSessionInfo = userSession;
                businessARInvoiceNotificationDTO.Comment = comment;
                businessARInvoiceNotificationDTO.EntityId = entityId;
                businessARInvoiceNotificationDTO.EntityType = entityType;
                if(userSession.UserType == (int)UserTypeEnum.Business)
                    await _bizNotificationHandler.SendBizCustomertUserOnNotesAddedNotificationAsync(businessARInvoiceNotificationDTO, (long)BizNotificationEventEnum.AddNoteOnSalesQuotationForBizUser);
                else
                    await _custNotificationHandler.SendCustUserOnNotesAddedNotificationAsync(businessARInvoiceNotificationDTO, (long)CustNotificationEventEnum.AddNoteOnSalesQuotationForCustomerUser);
            }
        }

        /// <summary>
        /// Generate Sales Order For Business Customer 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="appId"></param>
        /// <param name="comment"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        private async Task GenerateSalesOrdersForBusinessCustomer(Guid entityId, int entityType, Guid appId, string comment, UserSession userSession) {
            BusinessNotesNotificationDTO businessARInvoiceNotificationDTO = await _qNotesDS.GetBusinessInfoWithSalesOrderByEntityId(entityId, userSession.TenantId, appId, userSession.TenantUserId, userSession.UserType);
            if(businessARInvoiceNotificationDTO != null) {
                businessARInvoiceNotificationDTO.UserSessionInfo = userSession;
                businessARInvoiceNotificationDTO.Comment = comment;
                businessARInvoiceNotificationDTO.EntityId = entityId;
                businessARInvoiceNotificationDTO.EntityType = entityType;
                if(userSession.UserType == (int)UserTypeEnum.Business)
                    await _bizNotificationHandler.SendBizCustomertUserOnNotesAddedNotificationAsync(businessARInvoiceNotificationDTO, (long)BizNotificationEventEnum.AddNoteOnSalesOrderForBizUser);
                else
                    await _custNotificationHandler.SendCustUserOnNotesAddedNotificationAsync(businessARInvoiceNotificationDTO, (long)CustNotificationEventEnum.AddNoteOnSalesOrderForCustomerUser);
            }
        }

        /// <summary>
        /// Generate Delivery Notfication For Business Customer 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="appId"></param>
        /// <param name="comment"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        private async Task GenerateDeliveryForBusinessCustomer(Guid entityId, int entityType, Guid appId, string comment, UserSession userSession) {
            BusinessNotesNotificationDTO businessARInvoiceNotificationDTO = await _qNotesDS.GetBusinessInfoWithDeliveryByEntityId(entityId, userSession.TenantId, appId, userSession.TenantUserId, userSession.UserType);
            if(businessARInvoiceNotificationDTO != null) {
                businessARInvoiceNotificationDTO.UserSessionInfo = userSession;
                businessARInvoiceNotificationDTO.Comment = comment;
                businessARInvoiceNotificationDTO.EntityId = entityId;
                businessARInvoiceNotificationDTO.EntityType = entityType;
                if(userSession.UserType == (int)UserTypeEnum.Business)
                    await _bizNotificationHandler.SendBizCustomertUserOnNotesAddedNotificationAsync(businessARInvoiceNotificationDTO, (long)BizNotificationEventEnum.AddNoteOnDeliveryForBizUser);
                else
                    await _custNotificationHandler.SendCustUserOnNotesAddedNotificationAsync(businessARInvoiceNotificationDTO, (long)CustNotificationEventEnum.AddNoteOnDeliveryForCustomerUser);
            }
        }

        /// <summary>
        /// Generate Contract Notification Business Customer 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="appId"></param>
        /// <param name="comment"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        private async Task GenerateContractForBusinessCustomer(Guid entityId, int entityType, Guid appId, string comment, UserSession userSession) {
            BusinessNotesNotificationDTO businessARInvoiceNotificationDTO = await _qNotesDS.GetBusinessInfoWithContractByEntityId(entityId, userSession.TenantId, appId, userSession.TenantUserId, userSession.UserType);
            if(businessARInvoiceNotificationDTO != null) {
                businessARInvoiceNotificationDTO.UserSessionInfo = userSession;
                businessARInvoiceNotificationDTO.Comment = comment;
                businessARInvoiceNotificationDTO.EntityId = entityId;
                businessARInvoiceNotificationDTO.EntityType = entityType;
                if(userSession.UserType == (int)UserTypeEnum.Business)
                    await _bizNotificationHandler.SendBizCustomertUserOnNotesAddedNotificationAsync(businessARInvoiceNotificationDTO, (long)BizNotificationEventEnum.AddNoteOnContractForBizUser);
                else
                    await _custNotificationHandler.SendCustUserOnNotesAddedNotificationAsync(businessARInvoiceNotificationDTO, (long)CustNotificationEventEnum.AddNoteOnContractForCustomerUser);
            }
        }

        /// <summary>
        /// Generate Contract Notification Business Customer 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="appId"></param>
        /// <param name="comment"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        private async Task GenerateDraftDeliveryForBusinessCustomer(Guid entityId, int entityType, Guid appId, string comment, UserSession userSession) {
            BusinessNotesNotificationDTO businessARInvoiceNotificationDTO = await _qNotesDS.GetBusinessInfoWithDraftDeliveryByEntityId(entityId, userSession.TenantId, appId, userSession.TenantUserId, userSession.UserType);
            if(businessARInvoiceNotificationDTO != null) {
                businessARInvoiceNotificationDTO.UserSessionInfo = userSession;
                businessARInvoiceNotificationDTO.Comment = comment;
                businessARInvoiceNotificationDTO.EntityId = entityId;
                businessARInvoiceNotificationDTO.EntityType = entityType;
                if(userSession.UserType == (int)UserTypeEnum.Business)
                    await _bizNotificationHandler.SendBizCustomertUserOnNotesAddedNotificationAsync(businessARInvoiceNotificationDTO, (long)BizNotificationEventEnum.AddNoteOnASNForBizUser);
                else
                    await _custNotificationHandler.SendCustUserOnNotesAddedNotificationAsync(businessARInvoiceNotificationDTO, (long)CustNotificationEventEnum.AddNoteOnASNForCustomerUser);
            }
        }


        //Get Application Details By Appkey 
        private async Task<List<AppInfoDTO>> GetAppInfoByAppKey(Guid ID) {

            List<AppInfoDTO> appInfoDTO = new List<AppInfoDTO>();
            List<string> appkey = new List<string>();
            appkey.Add(AppKeyEnum.pay.ToString().ToLower());
            appkey.Add(AppKeyEnum.cust.ToString().ToLower());

            // Preparing api calling process model.
            #region Get Tenantlinking Information
            string methodUri = "App/getappinfobykey";
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, methodUri, appkey, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            appInfoDTO = await httpRequestProcessor.ExecuteAsync<List<AppInfoDTO>>(requestOptions, false);
            #endregion

            return appInfoDTO;
        }

    }
}
