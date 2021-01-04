/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 26 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 26 August 2019
 */

namespace ewApps.AppPortal.DS{

  /// <summary>
  /// enum for defining the event of the paymnet notification.
  /// </summary>
  public enum PaymentNotificationEvent :long {

    /// <summary>
    /// Business User set password
    /// </summary>
    SetPasswordBusinessUserInvitationEmail = 1,

    /// <summary>
    /// Primary Business partner User set password
    /// </summary>
    SetPasswordPrimaryPartnerUserInvitationEmail = 2,

    /// <summary>
    /// Business partner User set password
    /// </summary>
    SetPasswordPartnerUserInvitationEmail = 3,

    /// <summary>
    /// Business User forgot password
    /// </summary>
    BusinessForgotPasswordEmail = 4,

    /// <summary>
    /// Business partner User forgot password
    /// </summary>
    BusinessPartnerForgotPasswordEmail = 5,

    /// <summary>
    /// New invoice is added by the business user.
    /// </summary>
    InvoiceAdded = 6,

    /// <summary>
    /// Invoice is deleted by the business user.
    /// </summary>
    InvoiceDeleted = 7,

    /// <summary>
    /// Payment is done by the partner user either partial or full.
    /// </summary>
    PaymentDone = 8,

    /// <summary>
    /// Payment made by partner is void by business.
    /// </summary>
    PaymentVoid = 9,

    /// <summary>
    /// Payment made by partner is refunded by business.
    /// </summary>
    PaymentRefund = 10,

    /// <summary>
    /// System cancel the refund.
    /// </summary>
    SystemCancelRefundForBusiness = 11,

    /// <summary>
    /// System cancel the refund.
    /// </summary>
    SystemCancelRefundForBusinessPartner = 12

  }
}
