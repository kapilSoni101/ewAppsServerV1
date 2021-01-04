namespace ewApps.Core.SMSService {
    public interface ISMSDeliveryLogDS {
        SMSDeliveryLog Add(SMSDeliveryLog emailDeliveryLog);

        SMSDeliveryLog Update(SMSDeliveryLog emailDeliveryLog);

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>Returns total number of records get affected in this current change save.</returns>
        int Save();
    }
}