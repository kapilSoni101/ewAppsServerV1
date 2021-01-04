/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 15 Jan 2020
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 15 Jan 2020
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public interface IContactUsDS {

        Task<ResponseModelDTO> SendNotificationToPlatformForContactUs(ContactUsDTO contactUsDTO, CancellationToken token = default(CancellationToken));


        Task<ResponseModelDTO> SendNotificationToPublisherForContactUs(ContactUsDTO contactUsDTO, CancellationToken token = default(CancellationToken));

        Task<ResponseModelDTO> SendNotificationToPublisherForCustomerContactUs(ContactUsDTO contactUsDTO, CancellationToken token = default(CancellationToken));


        Task<UserEmailDTO> GetPlatEmailRecipent(CancellationToken token = default(CancellationToken));

        Task<UserEmailDTO> GetPubEmailRecipent(CancellationToken token = default(CancellationToken));
    }
}
