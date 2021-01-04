using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public interface ICustNotificationHandler {
        Task SendCustPaymentUserOnBoardNotificationAsync(CustomerOnBoardNotificationDTO customerOnBoardNotificationDTO);

        Task SendCustCustomerUserOnBoardNotificationAsync(CustomerOnBoardNotificationDTO customerOnBoardNotificationDTO);

        /// <summary>
        /// Customer Setup OnBoard
        /// </summary>
        /// <param name="customerOnBoardNotificationDTO"></param>
        /// <returns></returns>
        Task SendCustSetupUserOnBoardNotificationAsync(CustomerOnBoardNotificationDTO customerOnBoardNotificationDTO);
        
        Task SendCustUserOnNotesAddedNotificationAsync(BusinessNotesNotificationDTO customerNotesNotificationDTO, long custNotificationEventEnum, CancellationToken cancellationToken = default(CancellationToken));

        //Task ForgotPasswordBusinessPartner(BusinessAccountNotificationDTO businessNotificationDTO);

        Task GenerateCustomerForgotPasswordAsync(BusinessAccountNotificationDTO businessNotificationDTO);

        Task SendBizUserOnNotesAddedNotificationAsync(BusinessNotesNotificationDTO businessNotesNotificationDTO, long bizNotificationEventEnum, CancellationToken cancellationToken = default(CancellationToken));

        Task GenerateAppReomoveNotification(CustomerUserNotificationGeneralDTO customerUserNotificationGeneralDTO, CancellationToken cancellationToken = default(CancellationToken));

        Task GenerateAppPermissionChangeNotification(CustomerUserPermissionChangeNotificationGeneralDTO customerUserPermissionChangeNotificationGeneralDTO, CancellationToken cancellationToken = default(CancellationToken));

    Task AddSupportTicketFromCustomerNotificationAsync(BusinessSupportNotificationDTO businessSupportNotificationDTO, CancellationToken cancellationToken = default(CancellationToken));

        Task SendCustomerSupportNotificationAsync(BusinessSupportNotificationDTO businessSupportNotificationDTO, CancellationToken cancellationToken = default(CancellationToken));

        Task SendEmailForContactUs(ContactUsDTO ContactUsDTO);
  }
}
