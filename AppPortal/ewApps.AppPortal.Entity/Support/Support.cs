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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace ewApps.AppPortal.Entity {

  [Table("Support", Schema = "ap")]
  public class Support {
[Key]
    public Guid ID {
      get; set;
    }


    [Required]
    public Guid SupportId
    {
      get; set;
    }
   
    public Guid AppId
    {
      get; set;
    }
    [Required]
    public Guid TenantId
    {
      get; set;
    }
 
    public string TicketNo
    {
      get; set;
    }
    [Required]
    public int Portaltype
    {
      get; set;
    }
    [Required]
    public int Status
    {
      get; set;
    }
   
    public string PhoneNo
    {
      get; set;
    }
    [Required]
    public string IssueDesc
    {
      get; set;
    }

  
    public  Guid CreatedBy {
      get; set;
    }

    public  DateTime? CreatedOn {
      get; set;
    }

    public  Guid UpdatedBy {
      get; set;
    }

    public  DateTime? UpdatedOn {
      get; set;
    }

    public  bool Deleted {
      get; set;
    }
  }
}
