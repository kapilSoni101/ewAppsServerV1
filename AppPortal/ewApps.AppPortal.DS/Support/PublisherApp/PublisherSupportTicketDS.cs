//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using AutoMapper;
//using ewApps.Core.Common;
//using ewApps.Core.DS;
//using ewApps.Core.Entity;
//using ewApps.Core.UserSessionService;
//using ewApps.Support.Common;
//using ewApps.Support.Data;
//using ewApps.Support.DTO;
//using ewApps.Support.Entity;
//using SupportLevelEnum = ewApps.Support.Common.SupportLevelEnum;

//namespace ewApps.AppPortal.DS {

//  /// <summary>
//  /// This class provides all support ticket operations related to iPayment application.
//  /// </summary>
//  /// <seealso cref="ewApps.Core.DS.SupportTicketDS" />
//  /// <seealso cref="ewApps.AppPortal.DS.IPublisherSupportTicketDS" />
//  public class PublisherSupportTicketDS:SupportTicketDS, IPublisherSupportTicketDS {

//    ITenantUserDS _appUserDS = null;
//    ITenantUserAppLinkingDataService _userAppLinkingDS = null;
//    IUserSessionManager _userSessionDS = null;
//    ISupportTicketDS _supportTicketDS;
//    ISupportTicketRepository _supportTicketRepository;
//    ISupportCommentDS _supportCommentDS;
//    ILevelTransitionHistoryDS _levelTransitionDS;
//    IIdentityDS _identityDS;
//    IMapper _mapper;
//    ISupportNotificationHandler _supportNotificationHandler;
//    IPublisherSettingDataService _publisherSettingDataService;
//    IAppDataService _appDataService;
//    ITenantDataService _tenantDataService;
//    ICustomerDataService _customerDataService;
//    IDMDocumentDS _documentDS;
//    ISupportTicketAssigneeHelper _supportTicketAssigneeHelper;

//    /// <summary>
//    /// Initializes a new instance of the <see cref="PublisherSupportTicketDS"/> class member variables and dependencies..
//    /// </summary>
//    /// <param name="identityDS">An instance of <see cref="IIdentityDS"/> to generate system generated numbers..</param>
//    /// <param name="userSessionDS">An instance of <see cref="IUserSessionManager"/> to get requester user's information.</param>
//    /// <param name="appUserDS">An instance of <see cref="ITenantUserDS"/> to get application user related information.</param>
//    /// <param name="supportTicketDS">An instance of <see cref="ISupportTicketDS"/> to execute support ticket related operations.</param>
//    /// <param name="supportTicketRepository">An instance of <see cref="ISupportTicketRepository"/> to execute support ticket related operations.</param>
//    /// <param name="supportCommentDS">An instance of <see cref="ISupportCommentDS"/> to execute support comment related operations.</param>
//    /// <param name="levelTransitionDS">An instance of <see cref="ILevelTransitionHistoryDS"/> to execute level/status related operations.</param>
//    /// <param name="userSessionManager">The user session manager.</param>
//    /// <param name="customerDataService">The customer data service.</param>
//    /// <param name="appDataService">The application data service.</param>
//    /// <param name="tenantDataService">The tenant data service.</param>
//    /// <param name="publisherSettingDataService">The publisher setting data service.</param>
//    /// <param name="supportNotificationHandler">The support notification handler.</param>
//    /// <param name="cacheService">The cache service.</param>
//    /// <param name="mapper">The mapper.</param>
//    public PublisherSupportTicketDS(IIdentityDS identityDS, IUserSessionManager userSessionDS, ITenantUserDS appUserDS,
//      ISupportTicketDS supportTicketDS, ISupportTicketRepository supportTicketRepository, ISupportCommentDS supportCommentDS, ILevelTransitionHistoryDS levelTransitionDS,
//      IUserSessionManager userSessionManager, ICustomerDataService customerDataService, IAppDataService appDataService, ITenantDataService tenantDataService, IPublisherSettingDataService publisherSettingDataService, ISupportNotificationHandler supportNotificationHandler, ICacheService cacheService,
// IMapper mapper, IDMDocumentDS documentDS, ITenantUserAppLinkingDataService userAppLinkingDS, ISupportTicketAssigneeHelper supportTicketAssigneeHelper)
//      : base(identityDS, supportTicketRepository, supportCommentDS, levelTransitionDS, userSessionManager, cacheService, mapper, documentDS, supportTicketAssigneeHelper) {
//      _mapper = mapper;
//      _appUserDS = appUserDS;
//      _userSessionDS = userSessionDS;
//      _supportTicketDS = supportTicketDS;
//      _supportTicketRepository = supportTicketRepository;
//      _supportCommentDS = supportCommentDS;
//      _levelTransitionDS = levelTransitionDS;
//      _appKey = AppKeyEnum.pub.ToString();
//      _identityDS = identityDS;
//      _supportNotificationHandler = supportNotificationHandler;
//      _publisherSettingDataService = publisherSettingDataService;
//      _appDataService = appDataService;
//      _tenantDataService = tenantDataService;
//      _customerDataService = customerDataService;
//      _userAppLinkingDS = userAppLinkingDS;
//      _documentDS = documentDS;
//      _supportTicketAssigneeHelper = supportTicketAssigneeHelper;
//    }

//    //// <inheritdoc/>
//    //public new List<SupportTicketDTO> GetLevel3TicketList(bool onlyDeleted) {
//    //  // Get all support ticket by Application Id and Tenant Id and GenerationLevel=2
//    //  SupportTicket supportTicket = base.GetLevel3TicketList(onlyDeleted);
//    //  return supportTicket;
//    //}

//    ///<inheritdoc/>
//    public override SupportApp GetSupportAppModel() {
//      return new SupportApp() { ApplicationKey = AppKeyEnum.pub.ToString(), ApplicationName = AppKeyEnum.pub.ToString() };
//    }

//    ///<inheritdoc/>
//    public override List<SupportUser> GetSupportUsersByLevel(Common.SupportLevelEnum supportLevel) {
//      List<TenantUser> appUserList = _appUserDS.GetAll().ToList();
//      List<SupportUser> supportUserList = new List<SupportUser>(); // Map AppUser to SupportUser.
//      return supportUserList;
//    }

//    ///<inheritdoc/>
//    public override SupportSession GetSession(string appKey = "") {
//      UserSession userSession = _userSessionDS.GetSession();
//      TenantUser appUser = _appUserDS.Get(userSession.TenantUserId);
//      App pubApp = null;

//      SupportSession supportSession = new SupportSession(); // Map UserSessoin to SupportSession.

//      // ToDo: Add mapping using automapper.
//      supportSession.AppId = userSession.AppId;
//      supportSession.AppUserId = userSession.TenantUserId;
//      supportSession.ID = userSession.ID;
//      supportSession.IdentityToken = userSession.IdentityToken;
//      supportSession.TenantId = userSession.TenantId;
//      supportSession.TenantName = userSession.TenantName;
//      supportSession.UserName = userSession.UserName;
//      supportSession.UserType = userSession.UserType;
//      supportSession.SupportLevel = GetMappedSupportLevel((UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), supportSession.UserType.ToString(), true));
//      return supportSession;
//    }

//    ///<inheritdoc/>
//    public override SupportLevelEnum GetMappedSupportLevel(UserTypeEnum userType) {
//      SupportLevelEnum supportLevel = SupportLevelEnum.None;
//      switch(userType) {
//        case UserTypeEnum.Publisher:
//          supportLevel = SupportLevelEnum.Level3;
//          break;
//        case UserTypeEnum.Business:
//          supportLevel = SupportLevelEnum.Level2;
//          break;
//        case UserTypeEnum.BusinessPartner:
//          supportLevel = SupportLevelEnum.Level1;
//          break;
//      }
//      return supportLevel;
//    }

//    #region Public Methods

//    ///<inheritdoc/>
//    public new SupportTicket AddSupportTicket(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel) {
//      // Get ticket entity.
//      SupportTicket supportTicket = base.AddSupportTicket(supportTicketDTO, supportLevel, null, null);

//      return supportTicket;
//    }

//    ///<inheritdoc/>
//    public new bool UpdatePublisherSupportTicket(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel) {
//            //// Get ticket entity.
//            //SupportTicket supportTicketOld = Get((Guid)supportTicketDTO.SupportTicketId);
//            //// Get all the events for which notification needs to be raised.
//            //// List<SupportNotificationEventEnum> eventList = GetNotificationEvents(supportTicketDTO, supportTicketOld);
//            //// Store the data before updating the data needed in creating notification data.
//            //Dictionary<string, string> notificationOldData = new Dictionary<string, string>();
//            //notificationOldData.Add("oldPriority", ((Common.SupportPriorityEnum)supportTicketOld.Priority).ToString());
//            //notificationOldData.Add("oldStatus", ((SupportStatusTypeEnum)supportTicketOld.Status).ToString());
//            //string oldSupportLevel = _supportTicketAssigneeHelper.GetPublisherAssignedToName(supportTicketOld.CurrentLevel, supportTicketDTO.TenantId, supportTicketOld.CreatedBy);
//            //string newSupportLevel = _supportTicketAssigneeHelper.GetPublisherAssignedToName(supportTicketDTO.CurrentLevel, supportTicketDTO.TenantId, supportTicketOld.CreatedBy);
//            //notificationOldData.Add("oldAssignedTo", oldSupportLevel);
//            //notificationOldData.Add("newAssignedTo", newSupportLevel);
//            //// Update ticket all the logical operation for the update is done in this method.
//            //bool result = base.UpdateSupportTicket(supportTicketDTO, supportLevel);
//            //// Loop over the events to send the notifications.
//            ////foreach(SupportNotificationEventEnum item in eventList) {
//            ////  Task task = SendUpdateNotificationAsync(supportTicketDTO, item, notificationOldData);
//            ////}
//            bool result = base.UpdateSupportTicket(supportTicketDTO, supportLevel);
//            return result;
//    }
//    /// <summary>
//    /// Method helps in finding the assigned to level name.
//    /// </summary>
//    /// <param name="currentLevel"></param>
//    /// <param name="tenantId"></param>
//    /// <param name="createdBy"></param>
//    /// <returns>Returns the assigned to name.</returns>
//    //private string GetAssignedToName(short currentLevel, Guid tenantId, Guid createdBy) {
//    //  string name = string.Empty;
//    //  if(currentLevel == (short)SupportLevelEnum.Level3) {
//    //    name = ewApps.Support.Common.Constants.SuperAdminKey;
//    //  }
//    //  else if(currentLevel == (short)SupportLevelEnum.Level2) {
//    //    Tenant tenant = _tenantDataService.Get(tenantId);
//    //    name = tenant.Name;
//    //  }
//    //  else if(currentLevel == (short)SupportLevelEnum.Level1) {
//    //    //AppUser appUser = _appUserDS.Get(createdBy);
//    //    //Customer customer = _customerDataService.Get(appUser.ParentRefId);
//    //    //name = customer.Name;
//    //    name = _tenantDataService.GetBusinessPartnerTenantByPartnerUserId(createdBy).Name;
//    //  }
//    //  return name;
//    //}
//    /// <inheritdoc/>
//    public SupportTicketDetailDTO GetLevel3SupportTicketDetailById(Guid supportId, bool includeDeleted) {
//      SupportSession supportSession = GetSession(AppKeyEnum.pub.ToString());
//      SupportTicketDetailDTO supportTicketDetail = _supportTicketRepository.GetSupportTicketDetailById(supportId, includeDeleted);
//      supportTicketDetail.AssigneeList = _supportTicketAssigneeHelper.FillAssigneeList(supportTicketDetail, supportSession.SupportLevel);
//      supportTicketDetail.SupportCommentList = _supportCommentDS.GetCommentListBySupportId(supportId);

//      //Get document list.
//      supportTicketDetail.DocumentList = _documentDS.GetDocumentsByTicketId(supportId);

//      return supportTicketDetail;
//    }
//    #endregion Public Methods

//  }
//}
