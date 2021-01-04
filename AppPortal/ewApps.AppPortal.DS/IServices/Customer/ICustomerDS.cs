/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal <sagrawal@eworkplaceapps.com>
 * Date: 08 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 22 August 2019
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {

    /// <summary>
    ///  Responsible for exposing all the methods that are intrecting with the DB for retriving the data related to BACustomer entity.
    /// </summary>
    public interface ICustomerDS:IBaseDS<Customer> {


        /// <summary>
        /// add customer data.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customerSignupDTO"></param>
        /// <returns></returns>
        Task AddCustomerAsync(CustomerSignUpReqDTO customerSignupDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buspartnertenantid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<CustConfigurationViewDTO> GetConfigurationDetailAsync(Guid buspartnertenantid, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="custCreditCardRequestDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<CustCreditCardResponseDTO> ValidateCreditCardDetailAsync(CustCreditCardRequestDTO custCreditCardRequestDTO, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Update Customer Configuration branding
        /// </summary>
        /// <param name="custConfigurationUpdateDTO"></param>
        /// <param name="token"></param>
        Task UpdatetConfigurationDetailAsync(CustConfigurationUpdateDTO custConfigurationUpdateDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Save account detail.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customeAccDetail"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddCustomerAccountDetail(Guid customerId, CustomeAccDetailDTO customeAccDetail, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the customer all info by id
        /// </summary>
        /// <param name="customerId">cust id</param>
        /// <param name="token">token info</param>
        /// <returns>customr detai dto</returns>
        Task<CustCustomerDetailDTO> GetCustomerDetailByIdAsyncForCust(Guid customerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer by business partner id.
        /// </summary>
        /// <param name="busPartnerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Customer> GetCustomerByBusinesPartnerIdAsync(Guid busPartnerId, CancellationToken token = default(CancellationToken));
  }
}
