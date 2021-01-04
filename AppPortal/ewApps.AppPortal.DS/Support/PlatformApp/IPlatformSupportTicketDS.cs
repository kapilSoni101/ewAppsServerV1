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
//  public interface IPlatformSupportTicketDS:ISupportTicketDS {

   
//    /// <summary>
//    /// Updates support ticket information with changed values.
//    /// </summary>
//    /// <param name="supportTicketDTO">An instance of <see cref="SupportAddUpdateDTO"/> with all changed information.</param>
//    /// <param name="supportLevel">Support level from where it is updated.</param>
//    /// <returns>Returns <c>true</c> if update operation is sucessful; otherwise returns <c>false</c>.</returns>
//    new bool UpdatePlatformSupportTicket(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel);

//    SupportTicketDetailDTO GetLevel4SupportTicketDetailById(Guid supportId, bool includeDeleted);

//  }
//}