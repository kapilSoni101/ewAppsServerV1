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
//using System.Threading;
//using System.Threading.Tasks;
//using ewApps.AppPortal.DTO;
//using ewApps.AppPortal.Entity;
//using ewApps.Core.DMService;
//using Microsoft.AspNetCore.Http;

//namespace ewApps.AppPortal.DS {
//    /// <summary>
//    /// This interface provides methods to perform any operations on payment application support tickets.
//    /// </summary>
//    /// <seealso cref="ISupportTicketDSNew" />
//    public interface IBusinessSetupSupportTicketDS:ISupportTicketDSNew {

//        SupportTicket AddBusinessSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest);


//    /// <summary>
//    /// Updates support ticket information with changed values.
//    /// </summary>
//    /// <param name="supportTicketDTO">An instance of <see cref="SupportAddUpdateDTO"/> with all changed information.</param>
//    /// <param name="supportLevel">Support level from where it is updated.</param>
//    /// <returns>Returns <c>true</c> if update operation is sucessful; otherwise returns <c>false</c>.</returns>
//    bool UpdateBusinessSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest);

//      Task <List<SupportMyTicketDTO>> GetLevel2MyTicketList(SupportMyTicketListRequestDTO supportMyTicketListRequestDTO);

//    }
//}