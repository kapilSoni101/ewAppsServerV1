using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DS {
    [System.Flags]
    public enum CustNotificationEventEnum {

        NewCustomerPaymentUserOnboard = 0,
        NewCustomerCustomerUserOnboard = 1,
        CustomerUserAppAccessRemoved = 2,
        CustomerUserPaymentAppPermissionsChanged = 3,

        CustomerUserCustomerAppPermissionsChanged = 4,
        customerUserForgotPassword = 5,
        AddNoteOnARInvoiceForBizUser = 6,
        AddNoteOnAPInvoiceForCustomerUser = 7,
        AddCustomerTicketForBusinessUser = 8,
        AddNoteOnSalesQuotationForBizUser = 9,
        AddNoteOnSalesQuotationForCustomerUser = 10,
        AddNoteOnDeliveryForBizUser = 11,
        AddNoteOnDeliveryForCustomerUser = 12,
        AddNoteOnASNForBizUser = 13,
        AddNoteOnASNForCustomerUser = 14,
        AddNoteOnContractForBizUser = 15,
        AddNoteOnContractForCustomerUser = 16,
        AddNoteOnSalesOrderForCustomerUser = 17,
        ForgotPasswordBusinessPartner = 18,



        #region Setup
        NewCustomerSetupUserOnboard = 19,
        #endregion

        UpdateCustomerTicketForBusinessUser = 20,

            ContactUsNotification = 21
    }

}
