using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {
    /// <summary>
    /// Sync Time Log Details
    /// </summary>
    public class BASyncTimeLogDTO {

        /// <summary>
        /// Erp Connector Key
        /// </summary>
        public string ERPConnectorKey {
            get; set;
        }
        /// <summary>
        /// Erp Entity Key
        /// </summary>
        public string ERPEntityKey {
            get; set;
        }
        /// <summary>
        /// Receivefrom Time
        /// </summary>
        public DateTime ReceiveFromTime {
            get; set;
        }
        /// <summary>
        /// ReceiveTo Time
        /// </summary>
        public DateTime ReceiveToTime {
            get; set;
            }
        /// <summary>
        /// Send SyncFromTime
        /// </summary>
        public DateTime SendSyncFromTime {
            get; set;
        }
        /// <summary>
        /// SendSyncTotime
        /// </summary>
        public DateTime SendSyncToTime {
            get; set;
        }
        /// <summary>
        /// LastReceiveRowId
        /// </summary>
        public string LastReceiveRowId {
            get; set;
        }
        /// <summary>
        /// Last Send Row Id
        /// </summary>
        public string LastSendRowId {
            get; set;
        }

        /// <summary>
        /// Last Sync Status
        /// </summary>
        public int LastSyncStatus {
            get; set;
        }

        /// <summary>
        /// LastSyncStatusText
        /// </summary>
        public string LastSyncStatusText {
            get; set;
        }

    }
}
