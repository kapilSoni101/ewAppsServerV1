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
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DS {
    public class ContactUsDS:IContactUsDS {

        #region Local Variable 
        IPublisherNotificationHandler _publisherNotificationHandler;
        IBizNotificationHandler _bizNotificationHandler;
        ICustNotificationHandler _custNotificationHandler;
        IQContactUsDS _contactUsDS;
        IUserSessionManager _userSessionManager;
        #endregion

        #region Constructor
        public ContactUsDS(IPublisherNotificationHandler publisherNotificationHandler, IBizNotificationHandler bizNotificationHandler, ICustNotificationHandler custNotificationHandler, IQContactUsDS contactUsDS,IUserSessionManager userSessionManager) {
            _publisherNotificationHandler = publisherNotificationHandler;
            _bizNotificationHandler = bizNotificationHandler;
            _custNotificationHandler = custNotificationHandler;
            _contactUsDS = contactUsDS;
            _userSessionManager = userSessionManager;
        }
        #endregion

        public async Task<ResponseModelDTO> SendNotificationToPlatformForContactUs(ContactUsDTO contactUsDTO, CancellationToken token = default(CancellationToken)) {
            ResponseModelDTO responseModelDTO = new ResponseModelDTO();
            await _publisherNotificationHandler.SendEmailForContactUs(contactUsDTO);
            responseModelDTO.IsSuccess = true;
            return responseModelDTO;
        }

        public async Task<ResponseModelDTO> SendNotificationToPublisherForContactUs(ContactUsDTO contactUsDTO, CancellationToken token = default(CancellationToken)) {
            ResponseModelDTO responseModelDTO = new ResponseModelDTO();
            await _bizNotificationHandler.SendEmailForContactUs(contactUsDTO);
            responseModelDTO.IsSuccess = true;
            return responseModelDTO;
        }

        public async Task<ResponseModelDTO> SendNotificationToPublisherForCustomerContactUs(ContactUsDTO contactUsDTO, CancellationToken token = default(CancellationToken)) {
            ResponseModelDTO responseModelDTO = new ResponseModelDTO();
            await _bizNotificationHandler.SendEmailForContactUs(contactUsDTO);
            responseModelDTO.IsSuccess = true;
            return responseModelDTO;
        }

        public async Task<UserEmailDTO> GetPlatEmailRecipent(CancellationToken token = default(CancellationToken)) {
            return await _contactUsDS.GetPlatEmailRecipent(token);
        }

        public async Task<UserEmailDTO> GetPubEmailRecipent(CancellationToken token = default(CancellationToken)) {
            UserSession session = _userSessionManager.GetSession();
            return await _contactUsDS.GetPubEmailRecipent(session.TenantId, token);
        }

    }
}
