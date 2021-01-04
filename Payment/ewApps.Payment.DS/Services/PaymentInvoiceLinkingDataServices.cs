using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Payment.Common;
using ewApps.Payment.Data;
using ewApps.Payment.Entity;
using Microsoft.Extensions.Options;

namespace ewApps.Payment.DS {
    /// <summary>
    /// It will create link for each invoice transection and payment.
    /// </summary>
    public class PaymentInvoiceLinkingDataServices:BaseDS<PaymentInvoiceLinking>, IPaymentInvoiceLinkingDataServices {

        #region Local variables

        IPaymentInvoiceLinkingRepository _eRepository;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Constrtuctor with reporitory objects and setting object using DI.
        /// </summary>
        public PaymentInvoiceLinkingDataServices(IPaymentInvoiceLinkingRepository paymentRep) : base(paymentRep) {
            _eRepository = paymentRep;
        }

        #endregion Constructor

    }
}
