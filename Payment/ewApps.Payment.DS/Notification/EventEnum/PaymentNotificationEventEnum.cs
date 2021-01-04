using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DS {
    [Flags]
    public enum PaymentNotificationEventEnum:long {

        BizARInvoicePaymentInitiated = 0,
        BizExistingTransactionStatusUpdated = 1,
        BizTransactionVoidRequested = 2,
        BizTransactionRefundRequested = 3,
        BizPreAuthorizationInitiated = 4,
        BizAdvancePaymentSecuredForBusiness = 5,
        BizAdvancePaymentSecuredForCustomer = 6,
        BizNewRecurringOrder = 7,
        BizExistingRecurringOrdersUpdated = 8,
        CustARInvoicePaymentInitiated = 9,
        CustExistingTransactionStatusUpdated = 10,
        CustTransactionVoidRequested = 11,
        CustTransactionRefundRequested = 12,
        CustPreAuthorizationInitiated = 13,
        CustNewRecurringOrder = 14,
        CustExistingRecurringOrdersUpdated = 15,
        CustAdvancePaymentSecuredForCustomer = 16,
        CustAdvancePaymentSecuredForBusiness = 17,
    }
}