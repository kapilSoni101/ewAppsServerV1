///* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
// * Unauthorized copying of this file, via any medium is strictly prohibited
// * Proprietary and confidential
// * 
// * Author: Atul Badgujar <abadgujar@batchmaster.com>
// * Date: 26 August 2019
// * 
// * Contributor/s: Nitin Jain
// * Last Updated On: 26 August 2019
// */

//using System.Collections.Generic;
//using System.Threading.Tasks;
//using ewApps.AppPortal.Data;
//using ewApps.AppPortal.DTO;
//using ewApps.AppPortal.Entity;
//using ewApps.Core.BaseService;
//using ewApps.Core.DMService;
//using ewApps.Core.UniqueIdentityGeneratorService;
//using ewApps.Core.UserSessionService;
//using Microsoft.AspNetCore.Http;
//using SupportLevelEnum = ewApps.AppPortal.Common.SupportLevelEnum;

//namespace ewApps.AppPortal.DS {
//    public class BusinessSetupSupportTicketDS:SupportTicketDSNew, IBusinessSetupSupportTicketDS {

//        /// <summary>
//        /// Initializes a new instance of the <see cref="BusinessSetupSupportTicketDS"/> class member variables and dependencies..
//        /// </summary>
//        /// <param name="identityDS">An instance of <see cref="IIdentityDS"/> to generate system generated numbers..</param>
//        /// <param name="userSessionDS">An instance of <see cref="IUserSessionManager"/> to get requester user's information.</param>
//        /// <param name="appUserDS">An instance of <see cref="ITenantUserDS"/> to get application user related information.</param>
//        /// <param name="supportTicketRepository">An instance of <see cref="ISupportTicketRepository"/> to execute support ticket related operations.</param>
//        /// <param name="supportCommentDS">An instance of <see cref="ISupportCommentDS"/> to execute support comment related operations.</param>
//        /// <param name="levelTransitionDS">An instance of <see cref="ILevelTransitionHistoryDS"/> to execute level/status related operations.</param>
//        /// <param name="userSessionManager">The user session manager.</param>
//        /// <param name="cacheService">The cache service.</param>
//        /// <param name="mapper">The mapper.</param>
//        public BusinessSetupSupportTicketDS(IUniqueIdentityGeneratorDS identityDS, IUserSessionManager userSessionDS,
//          ISupportCommentDS supportCommentDS, ILevelTransitionHistoryDS levelTransitionDS,
//          IUserSessionManager userSessionManager,   IDMDocumentDS documentDS, ISupportTicketRepository supportTicketRepository ,
//            ISupportTicketAssigneeHelper supportTicketAssigneeHelper)
//          : base(identityDS, supportTicketRepository, supportCommentDS, levelTransitionDS, userSessionManager,  documentDS, supportTicketAssigneeHelper) {

//            base.AppSupportLevel = SupportLevelEnum.Level2;
//           // base.ApplicationKey = AppKeyEnum.biz;
//        }

//        #region Public Methods

//        public SupportTicket AddBusinessSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest) {
//            return AddSupportTicket(supportTicketDTO, AppSupportLevel, documentModel, httpRequest);
//        }


//        ///<inheritdoc/>
//        public new bool UpdateBusinessSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest) {
//            return base.UpdateSupportTicket(supportTicketDTO, AppSupportLevel, documentModel, httpRequest);
//        }

//        public async Task <List<SupportMyTicketDTO>> GetLevel2MyTicketList(SupportMyTicketListRequestDTO supportMyTicketListRequestDTO) {
//            string ApplicationKey = "TODOANIL";
//            return await base.GetUserSupportTicketByCreatorAndPartnerAndTenantId(ApplicationKey, supportMyTicketListRequestDTO.TenantId, (short)AppSupportLevel, supportMyTicketListRequestDTO.CreatorId, null, supportMyTicketListRequestDTO.OnlyDeleted);

//        }

//        #endregion Public Method
//    }
//}
