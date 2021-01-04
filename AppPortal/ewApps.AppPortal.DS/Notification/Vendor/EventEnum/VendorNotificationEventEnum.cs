namespace ewApps.AppPortal.DS {
    public enum VendorNotificationEventEnum:long {
        VendorInviteWithNewEmail = 1,
        VendorInviteWithExistingEmail = 2,
        VendorUserInviteWithNewEmail = 3,
        VendorUserInviteWithExistingEmail = 4,
        VendorUserForgotPassword = 5,

        VendorUserSetupAppOnboard = 6,
        VendorUserOnboardOnVendorApp = 7,

     

        //New Note added in existing A/P Invoice
    }
}
