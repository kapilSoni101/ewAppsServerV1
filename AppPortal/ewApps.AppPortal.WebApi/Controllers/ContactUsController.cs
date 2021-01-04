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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {

        #region local member

        IContactUsDS _contactUsDS;

        #endregion local member

        #region constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactUsDS"></param>
        public ContactUsController(IContactUsDS contactUsDS) {
            _contactUsDS = contactUsDS;
        }

        #endregion constructor

        /// <summary>
        /// send notification from contact us 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("pubcontactus")]
        public async Task<ResponseModelDTO> SendNotificationToPlatformForContactUs([FromBody]ContactUsDTO contactUsDTO, CancellationToken token = default(CancellationToken)) {
            ResponseModelDTO responseModelDTO = await _contactUsDS.SendNotificationToPlatformForContactUs(contactUsDTO, token);
            return responseModelDTO;
           

        }

        /// <summary>
        /// send notification from contact us 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("buscontactus")]
        public async Task<ResponseModelDTO> SendNotificationToPublisherForBusinessContactUs([FromBody]ContactUsDTO contactUsDTO, CancellationToken token = default(CancellationToken)) {
            ResponseModelDTO responseModelDTO = await _contactUsDS.SendNotificationToPublisherForContactUs(contactUsDTO, token);
            return responseModelDTO;


        }

        /// <summary>
        /// send notification from contact us 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("custcontactus")]
        public async Task<ResponseModelDTO> SendNotificationToPublisherForCustomerContactUs([FromBody]ContactUsDTO contactUsDTO, CancellationToken token = default(CancellationToken)) {
            ResponseModelDTO responseModelDTO = await _contactUsDS.SendNotificationToPublisherForCustomerContactUs(contactUsDTO, token);
            return responseModelDTO;


        }

        /// <summary>
        /// send notification from contact us 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getplatemailreceipent")]
        public async Task<UserEmailDTO> GetPlatformEmailRecipent( CancellationToken token = default(CancellationToken)) {
            UserEmailDTO userEmailDTO = await _contactUsDS.GetPlatEmailRecipent(token);
            return userEmailDTO;

        }

        /// <summary>
        /// send notification from contact us 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getpubemailreceipent")]
        public async Task<UserEmailDTO> GetPubEmailRecipent( CancellationToken token = default(CancellationToken)) {
            UserEmailDTO userEmailDTO = await _contactUsDS.GetPubEmailRecipent(token);
            return userEmailDTO;

        }
    }
}