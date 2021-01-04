/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agrawal <sagrawal@eworkplaceapps.com>
 * Date: 16 November 2018
 * 
 * Contributor/s: Sourabh Agrawal
 * Last Updated On: 16 November 2018
 */
using ewApps.Core.BaseService;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.Core.UniqueIdentityGeneratorService {

    /// <summary>
    /// Represents properties for Identity entity.
    /// </summary>
    [Table("UniqueIdentityGenerator", Schema = "core")]
    public class UniqueIdentityGenerator:BaseEntity {

        /// <summary>
        /// Source entity type
        /// </summary>
        [Required]
        public int EntityType {
            get; set;
        }

        /// <summary>
        /// Prefix entity
        /// </summary>  
        [Required]
        [MaxLength(100)]
        public string PrefixString {
            get; set;
        }
        /// <summary>
        /// Entity identity number
        /// </summary>    
        [Required]
        [MaxLength(100)]
        public string IdentityNumber {
            get; set;
        }
        /// <summary>
        /// state
        /// </summary>    
        public bool Active {
            get; set;
        }
        [NotMapped]
        public override Guid CreatedBy {
            get => base.CreatedBy;
            set => base.CreatedBy = value;
        }
        [NotMapped]
        public override DateTime? CreatedOn {
            get => base.CreatedOn;
            set => base.CreatedOn = value;
        }
        [NotMapped]
        public override Guid UpdatedBy {
            get => base.UpdatedBy;
            set => base.UpdatedBy = value;
        }
        [NotMapped]
        public override DateTime? UpdatedOn {
            get => base.UpdatedOn;
            set => base.UpdatedOn = value;
        }
        [NotMapped]
        public override bool Deleted {
            get => base.Deleted;
            set => base.Deleted = value;
        }

        ///// <summary>
        ///// Unique id Of identity table.
        ///// </summary>
        //public Guid ID
        //{
        //  get;set;
        //}

        ///// <summary>
        ///// Id Of Tenant
        ///// </summary>
        //public   Guid TenantId
        //{
        //  get; set;
        //}

        ///// <summary>
        ///// Source entity type
        ///// </summary>
        //[Required]
        //public int EntityType
        //{
        //  get; set;
        //}

        ///// <summary>
        ///// Prefix entity
        ///// </summary>  
        //[Required]
        //[MaxLength(100)]
        //public string PrefixString
        //{
        //  get; set;
        //}
        ///// <summary>
        ///// Entity identity number
        ///// </summary>    
        //[Required]
        //[MaxLength(100)]
        //public string IdentityNumber
        //{
        //  get; set;
        //}
        ///// <summary>
        ///// state
        ///// </summary>    
        //public bool Active
        //{
        //  get; set;
        //}

    }
}
