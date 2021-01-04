using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public interface IBizNotificationHandler {

        /// <summary>
        /// Business payment user on board notification
        /// </summary>
        /// <param name="businessPayUserOnBoardNotificationDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SendBizPaymentUserOnBoardNotificationAsync(BusinessOnBoardNotificationDTO businessPayUserOnBoardNotificationDTO, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Business customer user on board notification
        /// </summary>
        /// <param name="businessCustUserOnBoardNotificationDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SendBizCustUserOnBoardNotificationAsync(BusinessOnBoardNotificationDTO businessCustUserOnBoardNotificationDTO, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Business setup user on board notification
        /// </summary>
        /// <param name="businessSetupOnBoardNotificationDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SendBizSetupUserOnBoardNotificationAsync(BusinessOnBoardNotificationDTO businessSetupOnBoardNotificationDTO, CancellationToken cancellationToken = default(CancellationToken));
        
        Task GenerateAppReomoveNotification(BusinessUserNotificationGeneralDTO businessUserNotificationGeneralDTO, CancellationToken cancellationToken = default(CancellationToken));

        Task SendBusinessSupportNotificationAsync(BusinessSupportNotificationDTO businessOnBoardNotificationDTO, CancellationToken cancellationToken = default(CancellationToken));

        Task SendBizPaymentUserOnNotesAddedNotificationAsync(BusinessNotesNotificationDTO businessNotesNotificationDTO, long bizNotificationEventEnum, CancellationToken cancellationToken = default(CancellationToken));

        Task SendBizCustomertUserOnNotesAddedNotificationAsync(BusinessNotesNotificationDTO businessNotesNotificationDTO, long bizNotificationEventEnum, CancellationToken cancellationToken = default(CancellationToken));

        Task SendCustUserOnNotesAddedNotificationAsync(BusinessNotesNotificationDTO businessNotesNotificationDTO, long bizNotificationEventEnum, CancellationToken cancellationToken = default(CancellationToken));
        Task GenerateBusinessNewUserInviteNotification(BusinessAccountNotificationDTO businessNotificationDTO);

        Task GenerateAppPermissionChangeNotification(BusinessUserPermissionNotificationGeneralDTO businessUserPermissionNotificationGeneralDTO, CancellationToken cancellationToken = default(CancellationToken));

        Task GenerateBusinessUserAccountStatusChangedNotification(BusinessAccountNotificationDTO businessNotificationDTO, long bizNotificationEventEnum, CancellationToken cancellationToken = default(CancellationToken));

        Task SendEmailForContactUs(ContactUsDTO ContactUsDTO);

        Task GenerateAppAddAndReomoveNotification(BusinessUserNotificationAppAccessUpdateDTO businessUserNotificationGeneralDTO, List<AppShortInfoDTO> appShortInfoDTOs, CancellationToken cancellationToken = default(CancellationToken));

    }

  }
