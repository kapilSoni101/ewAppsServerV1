using ewApps.AppPortal.DTO;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DS {
    public interface INotificationDS {
        void SendErrorEmail(ErrorLogEmailDTO errorlogEmailDTO, UserSession userSession);
    }
}