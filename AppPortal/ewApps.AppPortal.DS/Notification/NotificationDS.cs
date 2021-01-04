using System;
using System.Collections.Generic;
using System.Text;
using ewApps.AppPortal.DTO;
using ewApps.Core.EmailService;
using ewApps.Core.ExceptionService;
using ewApps.Core.NotificationService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
  public   class NotificationDS :INotificationDS {
        ExceptionAppSettings _exceptionAppSetting;
        IEmailService _emailService;

        public NotificationDS(IEmailService emailService, IOptions<ExceptionAppSettings> exceptionAppSetting) {
            _emailService = emailService;
            _exceptionAppSetting = exceptionAppSetting.Value;
        }

        /// <inheritdoc/>
        public void SendErrorEmail(ErrorLogEmailDTO errorlogEmailDTO, UserSession userSession) {
            Tuple<string, string> emailBody = errorlogEmailDTO.GetErrorEmailDetail();
            AdhocEmailDTO adhocEmailDTO = new AdhocEmailDTO();
            adhocEmailDTO.EmailAddress = _exceptionAppSetting.ReceiverEmail;
            adhocEmailDTO.MessagePart1 = emailBody.Item1;
            adhocEmailDTO.MessagePart2 = emailBody.Item2;
            adhocEmailDTO.DeliveryType = (int)NotificationDeliveryType.Email;

            if(userSession != null) {
                adhocEmailDTO.AppId = userSession.AppId;
            }

            _emailService.SendAdhocEmail(adhocEmailDTO);
        }

    }
}
