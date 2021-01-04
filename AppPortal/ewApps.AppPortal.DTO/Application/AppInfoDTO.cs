using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// This class contains application information.
    /// </summary>
    public class AppInfoDTO {

        /// <summary>
        /// The Unique Application Id.
        /// </summary>
        public Guid Id {
            get; set;
        }
        
        /// <summary>
        /// Identity number.
        /// </summary>
        public string IdentityNumber {
            get; set;
        }

        /// <summary>
        /// The name  of application.
        /// </summary>
        public string Name {
            get; set;
        }

        /// <summary>
        /// Theme Identifier.
        /// </summary>
        public Guid ThemeId {
            get; set;
        }

        /// <summary>
        /// App active status identifier.
        /// </summary>
        public bool Active {
            get; set;
        }

        /// <summary>
        /// App unique identification key.
        /// </summary>
        public string AppKey {
            get; set;
        }
        [NotMapped]
        public int OperationType {
            get; set;
        }
    /// <summary>
    /// User appp identifier.
    /// </summary>
    public long PermissionBitMask
    {
      get; set;
    }
  }
}
