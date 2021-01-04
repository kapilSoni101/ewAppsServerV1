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
    [Table("SyncHistory", Schema = "be")]
    public class SyncHistory:BaseEntity {

        /// <summary>
        /// Connector key of sync entity.
        /// </summary>
        [MaxLength(50)]
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        ///  key of sync entity.
        /// </summary>
        [MaxLength(50)]
        public string ERPEntityKey {
            get; set;
        }

        /// <summary>
        /// data receive from time.
        /// </summary>
        public DateTime ActionStartTime {
            get; set;
        }

        /// <summary>
        /// data receive to time.
        /// </summary>
        public DateTime ActionEndTime {
            get; set;
        }

        /// <summary>
        /// data receive to time.
        /// </summary>
        public int StatusCode {
            get; set;
        }

        /// <summary>
        /// data receive to time.
        /// </summary>
        public string StatusMessage {
            get; set;
        }

        /// <summary>
        /// .
        /// </summary>
        public DateTime ReqFromSyncTime {
            get; set;
        }

        /// <summary>
        /// .
        /// </summary>
        public DateTime ReqToSyncTime {
            get; set;
        }

        /// <summary>
        /// .
        /// </summary>
        public int NumItems {
            get; set;
        }

        /// <summary>
        /// .
        /// </summary>
        [MaxLength(100)]
        public string SyncRequestData {
            get; set;
        }

        /// <summary>
        ///.
        /// </summary>
        public string SyncReplyData {
            get; set;
        }

        /// <summary>
        /// .
        /// </summary>
        public int ExecutionTimeInMS {
            get; set;
        }
        /// <summary>
        /// .
        /// </summary>
        public int ResponseChunkSize {
            get; set;
        }

        /// <summary>
        /// Tenan name .
        /// </summary>
        [MaxLength(100)]
        public string TenantName {
            get; set;
        }
    }
}
