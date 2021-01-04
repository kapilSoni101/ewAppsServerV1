using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DS {
    [Flags]
    public enum BizNotificationEventEnum:long {

        #region Payment

        BusinessUserWithNewEmailIdInvite = 1, // Not Implemented

        BusinessUserWithExistingEmailIdInvite = 2, // Not Implemented

        BusinessPartnerPrimaryNewUserInvite = 3, // Not Implemented

        BusinessUserForgotPassword = 4, // Not Implemented

        // New Business User Is on-board?
        NewBusinessPaymentUserIsOnboard = 5,

        // Business User account status is changed.
        BusinessUserAccountStatusChanged = 6,

        // New Customer is created
        // This will handle only in Standalone mode.
        AddCustomerForBizUser = 7,

        // Existing Customer is updated.
        UpdateCustomerForBizUser = 8,

        // New Vendor is created
        // This will handle only in Standalone mode.
        AddVendorForBizUser = 9,

        // Existing Vendor is updated.
        UpdateVendorForBizUser = 10,

        // Application Subscription is updated
        ApplicationSubscriptionUpdated = 11,

        // Application Setup is updated.
        BizUserBizSetupAppSetupUpdated = 12,

        // My Business Portal Setup Permission Changed.
        BizUserMyBizSetupAppPermissionsChanged = 13,

        // Application(s) access updated for me.
        BizUserBizSetupAppAccessUpdated = 14,

        // New Ticket raised by Customer.      
        NewCustomerTicketIsReceived = 15,

        // Note: On Customer ticket update two email will generate, One for biz user and another for customer user.
        ExistingCustomerTicketIsUpdatedForBusiness = 16,

        // This event correspond to Customer Portal - My Ticket is Updated.
        ExistingCustomerTicketIsUpdatedForCustomer = 17,

        // New Ticket raised by Vendor.      
        NewVendorTicketIsReceived = 18,

        // Note: On Customer ticket update two email will generate, One for biz user and another for vendor user.
        ExistingVendorTicketIsUpdatedForBusiness = 19,

        // This event correspond to Vendor Portal - My Ticket is Updated.
        ExistingVendorTicketIsUpdatedForVendor = 20,

        // New Ticket raised by Business User.
        SupportTicketRaisedByBusinessUser = 21,


        BusinessUserAppAccessRemoved = 22,
        BusinessUserPaymentAppPermissionsChanged = 23,
        BusinessUserPaymentStatusChanged = 24,
        // Not in use.
        ExistingCustomerUpdatedForBusinessPaymentUser = 25,

        #endregion Payment

        #region SetUp

        NewBusinessSetupUserOnboard = 26,
        ExistingBusinessUserDeleted = 27,
        ApplicationAccessUpdatedBusinessUser = 28,



        ApplicationForFirstTimeSubscribed = 29,
        ApplicationSubscriptionGoingToExpireTodayMidnight = 30,
        ApplicationSubscriptionRenews = 31,





        #endregion SetUp

        //BusinessUserForgotPassword = 32,

        CustomerPaymentApplicationAccessUpdated = 32,

        #region Customer App

        NewBusinessCustomerAppUserIsOnboard = 33,
        MyBusinessCustomerPortalAppAccessIsRemoved = 34,
        MyBusinessCustomerPortalAppPermissionsChanged = 35,
        CustomersCustomerPortalApplicationAccessUpdated = 36,

        #endregion


        AddNoteOnSalesQuotationForBizUser = 37,
        AddNoteOnSalesQuotationForCustomerUser = 38,
        AddNoteOnARInvoiceForBizUser = 39,
        AddNoteOnAPInvoiceForCustomerUser = 40,
        AddNoteOnDeliveryForBizUser = 41,
        AddNoteOnDeliveryForCustomerUser = 42,
        AddNoteOnASNForBizUser = 43,
        AddNoteOnASNForCustomerUser = 44,
        AddNoteOnContractForBizUser = 45,
        AddNoteOnContractForCustomerUser = 46,

        MyTicketIsUpdated = 47,

        BizCustAppUserOnBoard = 48,
        BizCustAppAccessRemoved = 49,
        BizCustAppPermissionChanged = 50,
        AddNoteOnSalesOrderForBizUser = 51,
        ContactUsNotification = 52,
        BusinessUserAppAccessAddAndRemoved = 53


    }
}
