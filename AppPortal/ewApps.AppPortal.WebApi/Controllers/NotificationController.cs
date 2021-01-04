using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController:ControllerBase {
        INotificationDS _notificationDS;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationController"/> class.
        /// </summary>
        /// <param name="notificationDS">The notification ds.</param>
        public NotificationController(INotificationDS notificationDS) {
            _notificationDS = notificationDS;
        }

        [HttpPost]
        [Route("senderroremail")]
        public void SendErrorEmail(ErrorLogEmailDTO errorlogEmailDTO) {
            _notificationDS.SendErrorEmail(errorlogEmailDTO, null);
        }
    }
}