//using System;
//using ewApps.Support.Common;
//using ewApps.Core.DS;
//using ewApps.Core.DTO;
//using ewApps.Core.Entity;
//using ewApps.Support.Entity;
//using Microsoft.AspNetCore.Http;
//using ewApps.Support.DTO;

//namespace ewApps.AppPortal.DS {
//  /// <summary>
//  /// This interface provides methods to perform any operations on payment application support tickets.
//  /// </summary>
//  /// <seealso cref="ewApps.Core.DS.ISupportTicketDS" />
//  public interface IPublisherSupportTicketDS:ISupportTicketDS {

//    /// <summary>
//    /// Adds support ticket and it's comment against input <paramref name="supportLevel"/> support level.
//    /// </summary>
//    /// <param name="supportTicketDTO">An instance of <see cref="SupportAddUpdateDTO"/> with all required information.</param>
//    /// <param name="supportLevel">Support level for which ticket is created.</param>
//    /// <returns>Returns newly added <see cref="SupportTicket"/>.</returns>
//    new SupportTicket AddSupportTicket(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel, AddUpdateDocumentModel documentModel, HttpRequest req);

//    /// <summary>
//    /// Updates support ticket information with changed values.
//    /// </summary>
//    /// <param name="supportTicketDTO">An instance of <see cref="SupportAddUpdateDTO"/> with all changed information.</param>
//    /// <param name="supportLevel">Support level from where it is updated.</param>
//    /// <returns>Returns <c>true</c> if update operation is sucessful; otherwise returns <c>false</c>.</returns>
//    new bool UpdatePublisherSupportTicket(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel);


//    /// <summary>
//    /// Gets Level3 <see cref="ewApps.Core.Entity.SupportTicket"/> entity detail (with comments) in form of <see cref="ewApps.Core.DTO.SupportTicketDTO"/> 
//    /// based on unique id of <see cref="ewApps.Core.Entity.SupportTicket"/>.
//    /// </summary>
//    /// <param name="supportId">Support ticket id.</param>
//    /// <param name="includeDeleted">True to include all deleted items.</param>
//    /// <returns>Returns <see cref="ewApps.Core.DTO.SupportTicketDTO"/> that matches given support ticket id.</returns>
//    SupportTicketDetailDTO GetLevel3SupportTicketDetailById(Guid supportId, bool includeDeleted);

//  }
//}