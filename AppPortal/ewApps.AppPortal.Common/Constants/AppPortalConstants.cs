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

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.Common {


    /// <summary>
    /// This class provides all the constants used in the application.
    /// </summary>
    public class AppPortalConstants {

        #region Support Constants

        public const string SuperAdminKey = "Super Admin";

        #endregion

        public const string AdminRoleKey = "Admin";
        public const string DefaultLanguage = "en";

        #region Connector Constants

        /// <summary>
        /// Value of SAP connector key.
        /// </summary>
        public const string SAPConnectorKey = "SAP";

        /// <summary>
        /// Value of BME connector key.
        /// </summary>
        public const string BMEConnectorKey = "BME";

        #endregion Connector Constants

        public const string BusinessIdPrefix = "BUS";

        public const string PublisherIdPrefix = "PUB";

        /// <summary>
        /// Starter number for the identity number of the publisher.
        /// </summary>
        public const int PublisherIdentityNumberStart = 100001;


        #region Application Guid

        /// <summary>
        /// Publisher application Guid.
        /// </summary>
        public const string PublisherApplicationId = "67D09A6F-CE95-498C-BF69-33C7D38F9041";

        /// <summary>
        /// Publisher application Guid.
        /// </summary>
        public const string CustomerSetUpApplicationId = "3252C1CF-C74A-4D0D-B0CE-A6271AEFC0A2";

    /// <summary>
    /// Publisher application Guid.
    /// </summary>
    public const string VendorSetUpApplicationId = "283259B7-952C-4F9B-9399-16A28ED08580";

    /// <summary>
    /// Publisher application Guid.
    /// </summary>
    public const string CustomerApplicationId = "8C6FA8CE-6B94-428F-95DE-5ED8859260D2";

    #endregion Application Guid



    //#region Application Keys

    //public const string PlatformAppKey = "plat";
    //public const string PublisherAppKey = "pub";
    //public const string BizAppKey = "biz";
    //public const string PaymentAppKey = "pay";
    //public const string ShipmentAppKey = "ship";
    //#endregion

    #region Url
    public const string PlatForgotPasswordUrl = "plattenantuser/forgot/password";
        public const string PubForgotPasswordUrl = "pubtenantuser/forgot/password";
        public const string BizForgotPasswordUrl = "bustenantuser/forgot/password";
        public const string CustForgotPasswordUrl = "custtenantuser/forgot/password";
        public const string VendForgotPasswordUrl = "vendtenantuser/forgot/password";

    #endregion Url

    #region Message        

    public const string PublisherPortalName = "publisher";
        public const string Inactive = "inactive";
        public const string BusinessPortalName = "business";
        public const string Delete = "delete";

        #endregion Message

        public const string CustomerDeleteException = "You can not delete customer, references exist.";


    }
}
