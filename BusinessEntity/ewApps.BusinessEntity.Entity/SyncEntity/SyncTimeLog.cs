/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Sourabh Agrawal<sagrawal @eworkplaceapps.com>
 * Date:09 september 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Entity {

    /// <summary>
    /// 
    /// </summary>
    [Table("SyncTimeLog", Schema = "be")]
    public class SyncTimeLog:BaseEntity {

        /// <summary>
        /// Connector key of sync entity.
        /// </summary>
        [MaxLength(100)]
        public string ERPConnectorKey {
            get; set;
        }
        /// <summary>
        ///  key of sync entity.
        /// </summary>
        [MaxLength(100)]
        public string ERPEntityKey {
            get; set;
        }
        /// <summary>
        /// data receive fromtime.
        /// </summary>
        public DateTime ReceiveFromTime {
            get; set;
        }
        /// <summary>
        /// data receive totime.
        /// </summary>
        public DateTime ReceiveToTime {
            get; set;
        }
        /// <summary>
        /// data send fromtime.
        /// </summary>
        public DateTime SendSyncFromTime {
            get; set;
        }
        /// <summary>
        /// data receive totime.
        /// </summary>
        public DateTime SendSyncToTime {
            get; set;
        }
        /// <summary>
        /// receive row id.
        /// </summary>
        [MaxLength(100)]
        public string LastReceiveRowId {
            get; set;
        }
        /// <summary>
        /// send row id.
        /// </summary>
        [MaxLength(100)]
        public string LastSendRowId {
            get; set;
        }

        /// <summary>
        /// Last Sync Status text.
        /// </summary>        
        public string LastSyncStatusText {
            get; set;
        }

        /// <summary>
        /// Last Sync Status .
        /// </summary>        
        public int LastSyncStatus {
            get; set;
        }
    }
}
