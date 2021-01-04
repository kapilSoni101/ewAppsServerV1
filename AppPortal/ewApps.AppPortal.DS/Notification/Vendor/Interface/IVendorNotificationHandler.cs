using ewApps.AppPortal.DTO;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.AppPortal.DS {

    public interface IVendorNotificationHandler {

        Task GenerateNewVendorNewEmailIdInvitedNotification(BusinessAccountNotificationDTO notificationDTO);

        Task SendVendorSetupUserOnBoardNotificationAsync(VendorOnBoardNotificationDTO vendorOnBoardNotificationDTO, CancellationToken cancellationToken = default(CancellationToken));

    Task GenerateVendorUserForgotPasswordAsync(BusinessAccountNotificationDTO businessNotificationDTO);
  }

}
