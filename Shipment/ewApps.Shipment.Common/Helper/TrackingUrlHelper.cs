using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Shipment.Common {
    public class TrackingUrlHelper {

        public static string GetTrackingUrl(string trackingNumber, string carrierCode) {
            string url = string.Empty;
            if(carrierCode == "FedEx") {
                url = @"https://www.fedex.com/apps/fedextrack/?action=track&trackingnumber=" + trackingNumber + "&requester=MB/";
            }
            else if(carrierCode == "UPS") {
                url = @"https://www.ups.com/track?loc=en_US&tracknum=" + trackingNumber + "&requester=MB/";
            }
            return url;
        }
    }
}
