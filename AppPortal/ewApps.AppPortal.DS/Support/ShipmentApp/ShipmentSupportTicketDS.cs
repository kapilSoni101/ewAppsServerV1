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

//using System;
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
//    public class ShipmentSupportTicketDS:SupportTicketDSNew, IShipmentSupportTicketDS {

        
//        IUserSessionManager _userSessionDS = null;
//        ISupportTicketAssigneeHelper _supportTicketAssigneeHelper;

//        public ShipmentSupportTicketDS(IUniqueIdentityGeneratorDS identityDS, 
//                  IUserSessionManager userSessionManager, ISupportCommentDS supportCommentDS, ILevelTransitionHistoryDS levelTransitionDS,
                  
//                  ISupportTicketAssigneeHelper supportTicketAssigneeHelper, IDMDocumentDS documentDS,
//                  ISupportTicketRepository supportTicketRepository)
//                  : base(identityDS, supportTicketRepository, supportCommentDS, levelTransitionDS, userSessionManager,  documentDS, supportTicketAssigneeHelper) {
            
//            _userSessionDS = userSessionManager;            
//            _supportTicketAssigneeHelper = supportTicketAssigneeHelper;

//            base.AppSupportLevel = AppSupportLevel;
//            //base.ApplicationKey = ApplicationKey;
//        }

//        public SupportLevelEnum AppSupportLevel {
//            get {
//                return SupportLevelEnum.Level2;
//            }
//            set {
//                throw new InvalidOperationException("CurrentSupportLevel property can't be change.");
//            }
//        }


//        //public AppKeyEnum ApplicationKey {
//        //    get {
//        //        return AppKeyEnum.ship;
//        //    }
//        //    set {
//        //        throw new InvalidOperationException("ApplicationKey property can't be change.");
//        //    }
//        //}

//        public SupportTicket AddShipmentSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest) {
//            return base.AddSupportTicket(supportTicketDTO, AppSupportLevel, documentModel, httpRequest);
//        }


//        public bool UpdateShipmentSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest) {
//            return base.UpdateSupportTicket(supportTicketDTO, AppSupportLevel, documentModel, httpRequest);
//        }

//        public async Task <List<SupportMyTicketDTO>> GetShipmentMyTicketList(Guid tenantId, Guid creatorId, Guid? partnerId, bool onlyDeleted) {
//            string ApplicationKey = "TODOANIL";
//            return await GetUserSupportTicketByCreatorAndPartnerAndTenantId(ApplicationKey.ToString(), tenantId, (int)AppSupportLevel, creatorId, partnerId, onlyDeleted);
//        }

//    }
//}
