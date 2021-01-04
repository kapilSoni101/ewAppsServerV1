///* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
// * Unauthorized copying of this file, via any medium is strictly prohibited
// * Proprietary and confidential
// * 
// * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
// * Date: 19 December 2018
// * 
// * Contributor/s: Nitin Jain
// * Last Updated On: 19 December 2018
// */

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using ewApps.Support.Common;
//using ewApps.Core.Data;
//using ewApps.Core.DS;
//using ewApps.Core.DTO;
//using ewApps.Core.Entity;
//using ewApps.Core.UserSessionService;
//using ewApps.Support.Common;
//using ewApps.Support.DTO;
//using ewApps.Support.Entity;
//using ewApps.Core.Common;
//using ewApps.Support.Data;
//using SupportLevelEnum = ewApps.Support.Common.SupportLevelEnum;
//using ewApps.Platform.DS;

//namespace ewApps.AppPortal.DS {

//    /// <summary>
//    /// This class provides all support ticket operations related to iPayment application.
//    /// </summary>
//    /// <seealso cref="ewApps.Core.DS.SupportTicketDS" />
//    /// <seealso cref="ewApps.AppPortal.DS.IPaymentSupportTicketDS" />
//    public class PaymentSupportTicketDS:SupportTicketDS, IPaymentSupportTicketDS {

//        ITenantUserDS _appUserDS = null;
//        ITenantUserAppLinkingDataService _userAppLinkingDS = null;
//        IUserSessionManager _userSessionDS = null;
//        // ISupportTicketDS _supportTicketDS;
//        ISupportTicketRepository _supportTicketRepository;
//        ISupportCommentDS _supportCommentDS;
//        ILevelTransitionHistoryDS _levelTransitionDS;
//        IIdentityDS _identityDS;
//        IMapper _mapper;
//        ISupportNotificationHandler _supportNotificationHandler;
//        IPublisherSettingDataService _publisherSettingDataService;
//        IPublisherDataService _publisherDataService;
//        IAppDataService _appDataService;
//        ITenantDataService _tenantDataService;
//        ICustomerDataService _customerDataService;
//        IDMDocumentDS _documentDS;
//        ITenantLinkingDS _tenantLinkingDS;
//        ISupportTicketAssigneeHelper _supportTicketAssigneeHelper;
//        SupportTicketDSNew _supportTicketDSNew;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="PaymentSupportTicketDS"/> class member variables and dependencies..
//        /// </summary>
//        /// <param name="identityDS">An instance of <see cref="IIdentityDS"/> to generate system generated numbers..</param>
//        /// <param name="userSessionDS">An instance of <see cref="IUserSessionManager"/> to get requester user's information.</param>
//        /// <param name="appUserDS">An instance of <see cref="ITenantUserDS"/> to get application user related information.</param>
//        /// <param name="supportTicketDS">An instance of <see cref="ISupportTicketDS"/> to execute support ticket related operations.</param>
//        /// <param name="supportTicketRepository">An instance of <see cref="ISupportTicketRepository"/> to execute support ticket related operations.</param>
//        /// <param name="supportCommentDS">An instance of <see cref="ISupportCommentDS"/> to execute support comment related operations.</param>
//        /// <param name="levelTransitionDS">An instance of <see cref="ILevelTransitionHistoryDS"/> to execute level/status related operations.</param>
//        /// <param name="userSessionManager">The user session manager.</param>
//        /// <param name="customerDataService">The customer data service.</param>
//        /// <param name="appDataService">The application data service.</param>
//        /// <param name="tenantDataService">The tenant data service.</param>
//        /// <param name="publisherSettingDataService">The publisher setting data service.</param>
//        /// <param name="supportNotificationHandler">The support notification handler.</param>
//        /// <param name="cacheService">The cache service.</param>
//        /// <param name="mapper">The mapper.</param>
//        public PaymentSupportTicketDS(IIdentityDS identityDS, IUserSessionManager userSessionDS, ITenantUserDS appUserDS,
//          ISupportCommentDS supportCommentDS, ILevelTransitionHistoryDS levelTransitionDS,
//          IUserSessionManager userSessionManager, ICustomerDataService customerDataService, IAppDataService appDataService, ITenantDataService tenantDataService, IPublisherSettingDataService publisherSettingDataService, ISupportNotificationHandler supportNotificationHandler, ICacheService cacheService, IMapper mapper, IDMDocumentDS documentDS, ITenantUserAppLinkingDataService userAppLinkingDS,
//    IPublisherDataService publisherDataService, ITenantLinkingDS tenantLinkingDS, ISupportTicketAssigneeHelper supportTicketAssigneeHelper, ISupportTicketRepository supportTicketRepository)
//          : base(identityDS, supportTicketRepository, supportCommentDS, levelTransitionDS, userSessionManager, cacheService, mapper, documentDS, supportTicketAssigneeHelper) {
//            _mapper = mapper;
//            _appUserDS = appUserDS;
//            _userSessionDS = userSessionDS;
//            // _supportTicketDS = supportTicketDS;
//            //_supportTicketRepository = supportTicketRepository;
//            _supportCommentDS = supportCommentDS;
//            _levelTransitionDS = levelTransitionDS;
//            _appKey = AppKeyEnum.pay.ToString();
//            _identityDS = identityDS;
//            _supportNotificationHandler = supportNotificationHandler;
//            _publisherSettingDataService = publisherSettingDataService;
//            _appDataService = appDataService;
//            _tenantDataService = tenantDataService;
//            _customerDataService = customerDataService;
//            _userAppLinkingDS = userAppLinkingDS;
//            _documentDS = documentDS;
//            _publisherDataService = publisherDataService;
//            _tenantLinkingDS = tenantLinkingDS;
//            _supportTicketAssigneeHelper = supportTicketAssigneeHelper;
//            _supportTicketDSNew = new SupportTicketDSNew(identityDS, supportTicketRepository, supportCommentDS, levelTransitionDS, userSessionDS, cacheService, mapper, documentDS, supportTicketAssigneeHelper);
//        }

//        ///<inheritdoc/>
//        public override SupportApp GetSupportAppModel() {
//            return new SupportApp() { ApplicationKey = AppKeyEnum.pay.ToString(), ApplicationName = AppKeyEnum.pay.ToString() };
//        }

//        ///<inheritdoc/>
//        public override List<SupportUser> GetSupportUsersByLevel(Common.SupportLevelEnum supportLevel) {
//            List<TenantUser> appUserList = _appUserDS.GetAll().ToList();
//            List<SupportUser> supportUserList = new List<SupportUser>(); // Map AppUser to SupportUser.
//            return supportUserList;
//        }

//        ///<inheritdoc/>
//        public override SupportSession GetSession(string appKey = "") {
//            UserSession userSession = _userSessionDS.GetSession();
//            TenantUser appUser = _appUserDS.Get(userSession.TenantUserId);
//            App payApp = null;
//            //if(string.IsNullOrEmpty(appKey) == false) {
//            //  payApp = _appDataService.GetAppByAppKey(appKey).Result;
//            //}
//            //else {
//            //  payApp = _appDataService.GetAppByAppKey(AppKeyEnum.pay.ToString()).Result;
//            //}
//            // UserAppLinking userAppLinking = _userAppLinkingDS.GetUserAppLinkingByAppIdAndUserId(payApp.ID, appUser.ID);
//            SupportSession supportSession = new SupportSession(); // Map UserSessoin to SupportSession.

//            // ToDo: Add mapping using automapper.
//            supportSession.AppId = userSession.AppId;
//            supportSession.AppUserId = userSession.TenantUserId;
//            supportSession.ID = userSession.ID;
//            supportSession.IdentityToken = userSession.IdentityToken;
//            supportSession.TenantId = userSession.TenantId;
//            supportSession.TenantName = userSession.TenantName;
//            supportSession.UserName = userSession.UserName;
//            supportSession.UserType = userSession.UserType;
//            supportSession.SupportLevel = GetMappedSupportLevel((UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), supportSession.UserType.ToString(), true));
//            return supportSession;
//        }

//        ///<inheritdoc/>
//        public override SupportLevelEnum GetMappedSupportLevel(UserTypeEnum userType) {
//            SupportLevelEnum supportLevel = SupportLevelEnum.None;
//            switch(userType) {
//                case UserTypeEnum.Publisher:
//                    supportLevel = SupportLevelEnum.Level3;
//                    break;
//                case UserTypeEnum.Business:
//                    supportLevel = SupportLevelEnum.Level2;
//                    break;
//                case UserTypeEnum.BusinessPartner:
//                    supportLevel = SupportLevelEnum.Level1;
//                    break;
//            }
//            return supportLevel;
//        }

//        #region Public Methods

//        ///<inheritdoc/>
//        public new SupportTicket AddSupportTicket(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel) {
//            // Get ticket entity.
//            //SupportTicket supportTicket = base.AddSupportTicket(supportTicketDTO, supportLevel, null, null);
//            SupportTicket supportTicket = _supportTicketDSNew.AddSupportTicket(supportTicketDTO, supportLevel, null, null);

//            #region Notification      

//            // Send Notification.
//            SendAddTicketNotification(supportTicketDTO, supportTicket);

//            #endregion Notification

//            return supportTicket;
//        }

//        ///<inheritdoc/>
//        public bool UpdateSupportTicket(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel) {
//            // Get ticket entity.
//            SupportTicket supportTicketOld = Get((Guid)supportTicketDTO.SupportTicketId);
//            // Get all the events for which notification needs to be raised.
//            List<SupportNotificationEventEnum> eventList = GetNotificationEvents(supportTicketDTO, supportTicketOld);
//            // Store the data before updating the data needed in creating notification data.
//            Dictionary<string, string> notificationOldData = new Dictionary<string, string>();
//            notificationOldData.Add("oldPriority", ((Common.SupportPriorityEnum)supportTicketOld.Priority).ToString());
//            notificationOldData.Add("oldStatus", ((SupportStatusTypeEnum)supportTicketOld.Status).ToString());
//            string oldSupportLevel = GetPaymentAssignedToName(supportTicketOld.CurrentLevel, supportTicketDTO.TenantId, supportTicketOld.CreatedBy);
//            string newSupportLevel = GetPaymentAssignedToName(supportTicketDTO.CurrentLevel, supportTicketDTO.TenantId, supportTicketOld.CreatedBy);
//            notificationOldData.Add("oldAssignedTo", oldSupportLevel);
//            notificationOldData.Add("newAssignedTo", newSupportLevel);

//            // Update ticket all the logical operation for the update is done in this method.
//            //ToDo: Replace this method with SupportTicketDSNew method.
//            bool result = base.UpdateSupportTicket(supportTicketDTO, supportLevel);

//            // Loop over the events to send the notifications.
//            foreach(SupportNotificationEventEnum item in eventList) {
//                Task task = SendUpdateNotificationAsync(supportTicketDTO, item, notificationOldData);
//            }

//            return result;
//        }

//        public SupportTicketDetailDTO GetLevel1PartnerSupportTicketDetailById(Guid supportId, bool includeDeleted) {
//            //SupportSession supportSession = GetSession(AppKeyEnum.pay.ToString());
//            //// Get Support Ticket Detail
//            //SupportTicketDetailDTO supportTicketDetail = GetSupportTicketDetailById(supportId, includeDeleted);

//            //// Fill Assignee List
//            //supportTicketDetail.AssigneeList = _supportTicketAssigneeHelper.FillAssigneeList(supportTicketDetail, supportSession.SupportLevel);

//            //// Get Comment List
//            //supportTicketDetail.SupportCommentList = _supportCommentDS.GetCommentListBySupportId(supportId);

//            ////Get document list.
//            //supportTicketDetail.DocumentList = _documentDS.GetDocumentsByTicketId(supportId);

//            //return supportTicketDetail;
//            return _supportTicketDSNew.GetSupportTicketDetailById(supportId, includeDeleted);


//        }

//        public SupportTicketDetailDTO GetLevel2BusinessSupportTicketDetailById(Guid supportId, bool includeDeleted) {
//            //SupportSession supportSession = GetSession(AppKeyEnum.pay.ToString());
//            //// Get Support Ticket Detail
//            //SupportTicketDetailDTO supportTicketDetail = GetSupportTicketDetailById(supportId, includeDeleted);

//            //// Fill Assignee List
//            //supportTicketDetail.AssigneeList = _supportTicketAssigneeHelper.FillAssigneeList(supportTicketDetail, supportSession.SupportLevel);

//            //// Get Comment List
//            //supportTicketDetail.SupportCommentList = _supportCommentDS.GetCommentListBySupportId(supportId);

//            ////Get document list.
//            //supportTicketDetail.DocumentList = _documentDS.GetDocumentsByTicketId(supportId);

//            //return supportTicketDetail;
//            return _supportTicketDSNew.GetSupportTicketDetailById(supportId, includeDeleted);

//        }

//        #endregion Public Methods

//        #region Private methods

//        #region Send Notitfications

//        /// <summary>
//        /// Send notitfication for the ticket add events.
//        /// </summary>
//        /// <param name="supportTicketDTO"></param>
//        /// <param name="supportTicket"></param>
//        private void SendAddTicketNotification(SupportAddUpdateDTO supportTicketDTO, SupportTicket supportTicket) {

//            // Get all the entities needed to generate th enotifications.
//            SupportNotificationDTO supportNotificationDTO = new SupportNotificationDTO();
//            Dictionary<string, string> notificationEvent = new Dictionary<string, string>();
//            supportNotificationDTO.UserSession = _userSessionDS.GetSession();
//            Tenant Tenant = _tenantDataService.Get(supportNotificationDTO.UserSession.TenantId);
//            TenantUser appUser = _appUserDS.Get(supportTicket.CreatedBy);

//            // Create the notification dTO for the notification.
//            supportNotificationDTO.PublisherName = "eworkplace";
//            supportNotificationDTO.AppName = _appDataService.Find(a => a.AppKey == _appKey).Name;
//            supportNotificationDTO.SupportTicket = supportTicket;
//            if(supportTicketDTO.SupportCommentList != null && supportTicketDTO.SupportCommentList.Count > 0) {
//                supportNotificationDTO.SupportCommentDTO = supportTicketDTO.SupportCommentList.First();
//            }

//            if(supportTicket.GenerationLevel == (int)SupportLevelEnum.Level1) {
//                //ToDo: Get Customer from new table structure.
//                Customer customer = null;// _customerDataService.Get(appUser.ParentRefId);
//                if(customer != null) {
//                    notificationEvent.Add("customerName", customer.Name);
//                    notificationEvent.Add("customerRefId", customer.CustomerRefId);
//                }
//                else {
//                    notificationEvent.Add("customerName", "");
//                    notificationEvent.Add("customerRefId", "");
//                }
//                notificationEvent.Add("customerUserName", appUser.FullName);
//                notificationEvent.Add("tenantName", Tenant.Name);
//                notificationEvent.Add("companyid", Tenant.IdentityNumber);
//                supportNotificationDTO.NotificationEventData = notificationEvent;
//                _supportNotificationHandler.AddLevel1TicketAsync(supportNotificationDTO);
//            }
//            else if(supportTicket.GenerationLevel == (int)SupportLevelEnum.Level2) {
//                notificationEvent.Add("tenantName", Tenant.Name);
//                notificationEvent.Add("tenantIdentity", Tenant.IdentityNumber);
//                notificationEvent.Add("createdBy", appUser.FullName);
//                supportNotificationDTO.NotificationEventData = notificationEvent;
//                // Send notitfication.
//                _supportNotificationHandler.AddLevel2TicketAsync(supportNotificationDTO);
//            }
//        }

//        /// <summary>
//        /// Send nottification for update ticket events.
//        /// </summary>
//        /// <param name="supportAddUpdateDTO"></param>
//        /// <param name="eventValue"></param>
//        /// <param name="notificationOldData"></param>
//        private async Task SendUpdateNotificationAsync(SupportAddUpdateDTO supportAddUpdateDTO, SupportNotificationEventEnum eventValue, Dictionary<string, string> notificationOldData) {
//            // Get all the entities needed to generate the notifications  data.     
//            SupportNotificationDTO supportNotificationDTO = new SupportNotificationDTO();
//            SupportTicket supportTicket = Get((Guid)supportAddUpdateDTO.SupportTicketId);
//            //PublisherSetting publisherSettings = _publisherSettingDataService.GetAll().FirstOrDefault();
//            supportNotificationDTO.UserSession = _userSessionDS.GetSession();
//            Tenant Tenant = _tenantDataService.Get(supportAddUpdateDTO.TenantId);
//            TenantUser createrAppUser = _appUserDS.Get(supportTicket.CreatedBy);
//            TenantUser modifierAppUser = _appUserDS.Get(supportTicket.UpdatedBy);
//            App app = _appDataService.Find(a => a.AppKey == _appKey);
//            Dictionary<string, string> eventData = new Dictionary<string, string>();
//            // Create the notification dTO for the notification.
//            supportNotificationDTO.AppName = _appDataService.Find(a => a.AppKey == _appKey).Name;
//            supportNotificationDTO.AppId = app.ID;
//            supportNotificationDTO.PublisherName = "eworkplace";
//            int supportLevelChange = SupportTicketAssignedToLevel3(supportTicket.ID, (int)SupportLevelEnum.Level3);
//            if(supportLevelChange == 0) {
//                supportNotificationDTO.IsPublisherAssinged = false;
//            }
//            else {
//                supportNotificationDTO.IsPublisherAssinged = true;
//            }
//            if(supportAddUpdateDTO.SupportCommentList != null && supportAddUpdateDTO.SupportCommentList.Count > 0) {
//                int countComment = supportAddUpdateDTO.SupportCommentList.Count;
//                supportNotificationDTO.SupportCommentDTO = supportAddUpdateDTO.SupportCommentList[countComment - 1];
//            }
//            supportNotificationDTO.SupportTicket = supportTicket;
//            supportNotificationDTO.UserSession = _userSessionDS.GetSession();

//            ////ToDo: Uncomment this code and use new table strucutre instead of ParentRefId.
//            //// Common event data collection.
//            //if(createrAppUser.ParentRefId != null && createrAppUser.ParentRefId != Guid.Empty) {
//            //  Customer customer = _customerDataService.Get(createrAppUser.ParentRefId);
//            //  if(customer != null) {
//            //    eventData.Add("customerName", customer.Name);
//            //    eventData.Add("customerId", customer.CustomerRefId);
//            //  }
//            //}
//            //else {
//            //  eventData.Add("customerName", "");
//            //  eventData.Add("customerId", "");
//            //}
//            eventData.Add("companyName", Tenant.Name);
//            eventData.Add("companyId", Tenant.IdentityNumber);
//            eventData.Add("createdBy", createrAppUser.FullName);
//            eventData.Add("modifiedBy", modifierAppUser.FullName);

//            // Depending upon event send notification after adding event specific data.
//            switch(eventValue) {
//                case SupportNotificationEventEnum.TicketIsAssigned:
//                    supportNotificationDTO.NotificationEventData = eventData;
//                    await _supportNotificationHandler.TicketIsAssingedAsync(supportNotificationDTO);
//                    break;
//                case SupportNotificationEventEnum.TicketStatusChanged:
//                    eventData.Add("oldStatus", notificationOldData["oldStatus"]);
//                    supportNotificationDTO.NotificationEventData = eventData;
//                    await _supportNotificationHandler.TicketStatusIsChangedAsync(supportNotificationDTO);
//                    break;
//                case SupportNotificationEventEnum.TicketPriorityChanged:
//                    eventData.Add("oldPriority", notificationOldData["oldPriority"]);
//                    supportNotificationDTO.NotificationEventData = eventData;
//                    await _supportNotificationHandler.TicketPriorityIsChangedAsync(supportNotificationDTO);
//                    break;
//                case SupportNotificationEventEnum.TicketReassigned:
//                    eventData.Add("oldAssignedTo", notificationOldData["oldAssignedTo"]);
//                    eventData.Add("newAssignedTo", notificationOldData["newAssignedTo"]);
//                    supportNotificationDTO.NotificationEventData = eventData;
//                    await _supportNotificationHandler.TicketIsReassingedAsync(supportNotificationDTO);
//                    break;
//                case SupportNotificationEventEnum.TicketNewCommentAdded:
//                    supportNotificationDTO.NotificationEventData = eventData;
//                    await _supportNotificationHandler.CommentIsAddToTicketAsync(supportNotificationDTO);
//                    break;
//                case SupportNotificationEventEnum.TicketNewAttachmentAdded:
//                    //TODO: Get the attchment count
//                    eventData.Add("attachmentCount", "2");
//                    supportNotificationDTO.NotificationEventData = eventData;
//                    await _supportNotificationHandler.AttachmentIsAddedToTicketAsync(supportNotificationDTO);
//                    break;
//                default:
//                    break;
//            }
//        }

//        ///// <inheritdoc/>
//        //public SupportTicketDetailDTO GetLevel3SupportTicketDetailById(Guid supportId, bool includeDeleted) {
//        //    //SupportSession supportSession = GetSession(AppKeyEnum.pub.ToString());
//        //    //SupportTicketDetailDTO supportTicketDetail = _supportTicketRepository.GetSupportTicketDetailById(supportId, includeDeleted);
//        //    //supportTicketDetail.AssigneeList = _supportTicketAssigneeHelper.FillAssigneeList(supportTicketDetail, supportSession.SupportLevel);
//        //    //supportTicketDetail.SupportCommentList = _supportCommentDS.GetCommentListBySupportId(supportId);

//        //    ////Get document list.
//        //    //supportTicketDetail.DocumentList = _documentDS.GetDocumentsByTicketId(supportId);

//        //    //return supportTicketDetail;
//        //    return _supportTicketDSNew.GetSupportTicketDetailById(supportId, includeDeleted);
//        //}

//        #endregion Send Notitfications

//        #region Notification Support Methods

//        private string GetPaymentAssignedToName(short currentLevel, Guid tenantId, Guid createdBy) {
//            string name = string.Empty;
//            if(currentLevel == (short)SupportLevelEnum.Level3) {
//                //name = ewApps.Support.Common.Constants.SuperAdminKey;
//                Guid? pubTenantId = _tenantLinkingDS.Find(k => k.BusinessTenantId == tenantId && k.BusinessPartnerTenantId == null).PublisherTenantId;
//                name = _publisherDataService.Find(p => p.TenantId == pubTenantId).Name;
//            }
//            else if(currentLevel == (short)SupportLevelEnum.Level2) {
//                Tenant tenant = _tenantDataService.Get(tenantId);
//                name = tenant.Name;
//            }
//            else if(currentLevel == (short)SupportLevelEnum.Level1) {
//                //AppUser appUser = _appUserDS.Get(createdBy);
//                //Customer customer = _customerDataService.Get(appUser.ParentRefId);
//                //name = customer.Name;
//                name = _tenantDataService.GetBusinessPartnerTenantByPartnerUserId(createdBy).Name;
//            }
//            return name;
//        }

//        /// <summary>
//        /// On the basis of tickets old and new data we find all the changes made in the ticke.
//        /// </summary>
//        /// <param name="supportAddUpdateDTO"></param>
//        /// <param name="supportTicket"></param>
//        /// <returns> list of all the notitfication events to raised for the cureent update request. </returns>
//        private List<SupportNotificationEventEnum> GetNotificationEvents(SupportAddUpdateDTO supportAddUpdateDTO, SupportTicket supportTicket) {
//            List<SupportNotificationEventEnum> eventList = new List<SupportNotificationEventEnum>();
//            // Find priority is changed.
//            if(supportTicket.Priority != supportAddUpdateDTO.Priority) {
//                eventList.Add(SupportNotificationEventEnum.TicketPriorityChanged);
//            }
//            // Find status is changed
//            if(supportTicket.Status != supportAddUpdateDTO.Status) {
//                eventList.Add(SupportNotificationEventEnum.TicketStatusChanged);
//            }
//            // Find comment is added.
//            if(supportAddUpdateDTO.SupportCommentList != null && supportAddUpdateDTO.SupportCommentList.Count > 0) {
//                foreach(var item in supportAddUpdateDTO.SupportCommentList) {
//                    if(item.OperationType == (int)OperationType.Add) {
//                        eventList.Add(SupportNotificationEventEnum.TicketNewCommentAdded);
//                        break;
//                    }
//                }
//            }
//            // Assignment and Reassignemnet cases.
//            if(supportAddUpdateDTO.CurrentLevel != supportTicket.CurrentLevel) {
//                if(supportAddUpdateDTO.CurrentLevel == supportTicket.GenerationLevel) {
//                    eventList.Add(SupportNotificationEventEnum.TicketReassigned);
//                }
//                else {
//                    int supportLevelChange = SupportTicketAssignedToLevel3(supportTicket.ID, supportAddUpdateDTO.CurrentLevel);
//                    if(supportLevelChange == 0) {
//                        eventList.Add(SupportNotificationEventEnum.TicketIsAssigned);
//                    }
//                    else {
//                        eventList.Add(SupportNotificationEventEnum.TicketReassigned);
//                    }
//                }
//            }
//            return eventList;
//        }

//        ///// <summary>
//        ///// Method helps in finding the assigned to level name.
//        ///// </summary>
//        ///// <param name="currentLevel"></param>
//        ///// <param name="tenantId"></param>
//        ///// <param name="createdBy"></param>
//        ///// <returns>Returns the assigned to name.</returns>
//        //private string GetAssignedToName(short currentLevel, Guid tenantId, Guid createdBy) {
//        //  string name = string.Empty;
//        //  if(currentLevel == (short)SupportLevelEnum.Level3) {
//        //    //name = ewApps.Support.Common.Constants.SuperAdminKey;
//        //    Guid? pubTenantId = _tenantLinkingDS.Find(k => k.BusinessTenantId == tenantId && k.BusinessPartnerTenantId == null).PublisherTenantId;
//        //    name = _publisherDataService.Find(p => p.TenantId == pubTenantId).Name;
//        //  }
//        //  else if(currentLevel == (short)SupportLevelEnum.Level2) {
//        //    Tenant tenant = _tenantDataService.Get(tenantId);
//        //    name = tenant.Name;
//        //  }
//        //  else if(currentLevel == (short)SupportLevelEnum.Level1) {
//        //    //AppUser appUser = _appUserDS.Get(createdBy);
//        //    //Customer customer = _customerDataService.Get(appUser.ParentRefId);
//        //    //name = customer.Name;
//        //    name = _tenantDataService.GetBusinessPartnerTenantByPartnerUserId(createdBy).Name;
//        //  }
//        //  return name;
//        //}

//        #endregion Notification Support Methods

//        #endregion Private methods

//    }
//}
