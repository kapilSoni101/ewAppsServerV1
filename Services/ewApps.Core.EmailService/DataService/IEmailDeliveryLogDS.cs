/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 02 July 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 02 July 2019
 */
namespace ewApps.Core.EmailService {

    /// <summary>
    /// This interface declares set of operations required for <see cref="EmailDeliveryLog"/>.
    /// </summary>
    public interface IEmailDeliveryLogDS {
        /// <summary>
        /// Adds the specified email delivery log.
        /// </summary>
        /// <param name="emailDeliveryLog">The email delivery log.</param>
        /// <returns>Returns newly added <see cref="EmailDeliveryLog"/> entity.</returns>
        EmailDeliveryLog Add(EmailDeliveryLog emailDeliveryLog);

        /// <summary>
        /// Updates the specified email delivery log.
        /// </summary>
        /// <param name="emailDeliveryLog">The email delivery log entity with updated information.</param>
        /// <returns>Returns updated <see cref="EmailDeliveryLog"/> entity.</returns>
        EmailDeliveryLog Update(EmailDeliveryLog emailDeliveryLog);

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>Returns total number of records get affected in this current change save.</returns>
        int Save();
    }
}
