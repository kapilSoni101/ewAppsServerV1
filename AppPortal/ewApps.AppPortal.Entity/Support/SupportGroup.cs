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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Entity {
    /// <summary>
    /// SupportGroup table represting all the Support Group.
    /// </summary>
    [Table("SupportGroup", Schema = "ap")]
  public class SupportGroup:BaseEntity {

    /// <summary>
    /// The entity name.
    /// </summary>
    public const string EntityName = "SupportGroup";
   /// <summary>
    /// Level
    /// </summary>
    [Required]
    public int Level
    {
      get; set;
    }

    /// <summary>
    // DisplayName
    /// </summary>
    [Required]
    public string DisplayName
    {
      get; set;
    }
    /// <summary>
    /// Active
    /// </summary>
    [Required]
    public bool Active
    {
      get; set;
    }
    /// <summary>
    /// AppKey
    /// </summary>
    [Required]
    public string AppKey
    {
      get; set;
    }
  }
}
