using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.EmailService {
    /// <summary>
    /// Interface for Push and Alerts
    /// This interface is common for all ewApps Push, from various modules.
    /// </summary>
    public interface IEmailService {
        /// <summary>
        /// This method Generates all XML data,Send it to XSLT and gets the email Body
        /// and generates Email notification
        /// </summary>
        /// <param name="emailPayload">The email payload.</param>
        /// <param name="token">The async task cancellation token.</param>
        /// <returns>Returns true if email queued sucessfully for delivery.</returns>
        Task<bool> SendEmailAsync(EmailPayload emailPayload, CancellationToken token = default(CancellationToken));

        void SendAdhocEmail(AdhocEmailDTO adhocEmailDTO);
    }
}
