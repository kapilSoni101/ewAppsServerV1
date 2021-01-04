/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 25 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 25 February 2019
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Payment.Data;
using ewApps.Payment.DTO;

namespace ewApps.Payment.DS {
    /// <summary>
    /// Class is used to pay invoice payment.
    /// </summary>
    public class PaymentDataService:BaseDS<ewApps.Payment.Entity.Payment>, IPaymentDataService {

        #region Local variables

        IPaymentRepository _paymentRep;
        IPaymentUnitOfWork _unitOfWork;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Constrtuctor with reporitory objects and setting object using DI.
        /// </summary>
        public PaymentDataService(IPaymentAccess entityAccess, IPaymentRepository paymentRep, IPaymentUnitOfWork unitOfWork) : base(paymentRep) {
            _paymentRep = paymentRep;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

    }
}
