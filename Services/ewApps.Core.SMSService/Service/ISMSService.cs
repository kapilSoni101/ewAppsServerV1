﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 14 April 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 14 April 2019
 */

using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.SMSService {
    public interface ISMSService {
        bool SendSMS(SMSQueue smsNotification);

        Task<bool> SendSMSAsync(SMSPayload smsPayload, CancellationToken token = default(CancellationToken));
    }
}