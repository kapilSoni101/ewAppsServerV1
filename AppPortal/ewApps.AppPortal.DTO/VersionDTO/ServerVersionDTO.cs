/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 23 May 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 23 May 2019
 */

namespace ewApps.AppPortal.DTO {
    public class ServerVersionDTO {

        /// <summary>
        /// Application version number
        /// </summary>
        public string ServerVersionNo {
            get; set;
        }

        /// <summary>
        /// Payment Connector version number
        /// </summary>
        public PayConnectorVersionDTO PayConnectorVersionDTO {
            get; set;
        }

        /// <summary>
        /// Shipment Connector version number
        /// </summary>
        public ShipConnectorVersionDTO ShipConnectorVersionDTO {
            get; set;
        }

        /// <summary>
        /// Payment Connector version number
        /// </summary>
        public IdentityServerVersionDTO IdentityServerVersionDTO {
            get; set;
        }

        

    }
}
