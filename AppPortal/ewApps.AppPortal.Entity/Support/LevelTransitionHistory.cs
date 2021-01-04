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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Entity {
    /// <summary>
    /// LevelTransitionHistory table represting all the Level Transition History.
    /// </summary>
    [Table("LevelTransitionHistory", Schema ="ap")]

  public class LevelTransitionHistory :BaseEntity {

    /// <summary>
    /// The entity name.
    /// </summary>
    public const string EntityName = "LevelTransitionHistory";

    /// <summary>
    /// AppKey
    /// </summary>
    [Required]
    public string AppKey {
      get; set;
    }
    /// <summary>
    /// SourceLevel
    /// </summary>
    // ToDo: Change type from int to short.
    [Required]
    public int SourceLevel {
      get; set;
    }
    /// <summary>
    /// TargetLevel
    /// </summary>
    [Required]
    public int TargetLevel {
      get; set;
    }
    /// <summary>
    /// Status
    /// </summary>
    [Required]
    public short Status {
      get; set;
    }
    /// <summary>
    /// SupportId
    /// </summary>
    [Required]
    public Guid SupportId {
      get; set;
    }

  }
}
