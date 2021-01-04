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
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.DMService;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="SupportTicket"/> including <see cref="SupportComment"/>.
    /// </summary>
    public class SupportTicketDetailDTO
  {

    /// <summary>
    /// System generated unique support id.
    /// </summary>
    public new Guid ID
    {
      get; set;
    }

    /// <summary>
    /// System generated unique support ticket number.
    /// </summary>
    public string SupportIdentityNumber
    {
      get; set;
    }

    /// <summary>
    /// Support ticket creation date and time (in UTC).
    /// </summary>
    public new DateTime CreatedOn
    {
      get; set;
    }

    /// <summary>
    /// Contact email of creator.
    /// </summary>
    public string ContactEmail
    {
      get; set;
    }

    /// <summary>
    /// The name of the creator.
    /// </summary>
    public string CreatedByName
    {
      get; set;
    }

    /// <summary>
    /// The contact phone of creator.
    /// </summary>
    public string ContactPhone
    {
      get; set;
    }

    /// <summary>
    /// System generated unique tenant id.
    /// </summary>
    public new Guid TenantId
    {
      get; set;
    }

    /// <summary>
    /// System generated unique tenant number (in readable form).
    /// </summary>
    public string TenantIdentityNumber
    {
      get; set;
    }

    /// <summary>
    /// The name of the tenant.
    /// </summary>
    public string TenantName
    {
      get; set;
    }
    /// <summary>
    /// System generated unique tenant number (in readable form).
    /// </summary>
    public string PublisherIdentityNumber
    {
      get; set;
    }

    /// <summary>
    /// The name of the tenant.
    /// </summary>
    public string PublisherName
    {
      get; set;
    }

    /// <summary>
    /// The system generated unique customer id.
    /// </summary>
    /// <remarks>It's empty if not applicable.</remarks>
    public Guid CustomerId
    {
      get; set;
    }

    /// <summary>
    /// The system generated customer identity number.
    /// </summary>
    /// <remarks>It's empty if not applicable.</remarks>
    public string CustomerIdentityNumber
    {
      get; set;
    }

    /// <summary>
    /// The customer name.
    /// </summary>
    /// <remarks>It's empty if not applicable.</remarks>
    public string CustomerName
    {
      get; set;
    }

    /// <summary>
    /// The support ticket title.
    /// </summary>
    public string Title
    {
      get; set;
    }

    /// <summary>
    /// The support ticket description.
    /// </summary>
    public string Description
    {
      get; set;
    }

    /// <summary>
    /// Support ticket priority value.
    /// </summary>
    /// <remarks>It should be any value of <see cref="ewApps.Core.Common.SupportPriorityEnum"/>.</remarks>
    public short Priority
    {
      get; set;
    }

    /// <summary>
    /// Support ticket status value.
    /// </summary>
    /// <remarks>It should be any value of <see cref="ewApps.Core.Common.SupportStatusTypeEnum"/>.</remarks>
    public short Status
    {
      get; set;
    }

    /// <summary>
    /// Support ticket's current support level enum value.
    /// </summary>
    /// <remarks>It should be any value of <see cref="ewApps.Core.Common.SupportLevelEnum"/>.</remarks>
    public short CurrentLevel
    {
      get; set;
    }

    /// <summary>
    /// Support ticket's generation support level enum value.
    /// </summary>
    /// <remarks>It should be any value of <see cref="ewApps.Core.Common.SupportLevelEnum"/>.</remarks>
    public short GenerationLevel
    {
      get; set;
    }

    /// <summary>
    /// Support ticket last modified date and time (in UTC).
    /// </summary>
    public new DateTime UpdatedOn
    {
      get; set;
    }
        /// <summary>
        /// Appkey 
        /// </summary>
        public string AppKey {
            get; set;
        }

        /// <summary>
        /// The name of the user who last updated support ticket.
        /// </summary>
        public string UpdatedByName
    {
      get; set;
    }

    /// <summary>
    /// The requester source support level.
    /// </summary>
    [NotMapped]
    public int RequesterSupportLevel
    {
      get; set;
    }

    /// <summary>
    /// Document list associated with a ticket.
    /// </summary>
    [NotMapped]
    public IEnumerable<DocumentResponseModel> DocumentList
    {
      get; set;
    }

    private IEnumerable<SupportCommentDTO> _supportCommentList;

    /// <summary>
    /// The support comment list in form of <see cref="List{SupportCommentDTO}"/>
    /// </summary>
    [NotMapped]
    public IEnumerable<SupportCommentDTO> SupportCommentList
    {
      get {
        //return UpdateCommentorName(this._supportCommentList, this.GenerationLevel);
        return this._supportCommentList;
            }
      set
      {
        this._supportCommentList = value;
      }
    }

    private List<KeyValuePair<short, string>> _currentAssigneeList;

    /// <summary>
    /// The valid assignee list in form of <see cref="List{KeyValuePair{short,string}}"/>.
    /// </summary>
    /// <value>
    /// Each item (i.e. <see cref="KeyValuePair{short,String}"/> has Key as enum value of <see cref="SupportLevelEnum"/> and value as it's display name.
    /// </value>
    [NotMapped]
    public List<KeyValuePair<short, string>> AssigneeList
    {
      get
      {
        return _currentAssigneeList;
      }
      set
      {
        _currentAssigneeList = value;
      }
    }

    //// Updates Commentor name in each comment item.
    //private IEnumerable<SupportCommentDTO> UpdateCommentorName(IEnumerable<SupportCommentDTO> commentorList, short ticketGenerationLevel) {
    //    if(commentorList != null) {
    //        foreach(SupportCommentDTO commentItem in commentorList) {
    //            commentItem.CommentorName = GetCommentorName(commentItem.CreatorLevel, ticketGenerationLevel);
    //        }
    //    }
    //    return commentorList;
    //}

    /////// <summary>
    /////// Fills the valid assignee support level list applicable in current request context.
    /////// </summary>
    /////// <param name="requesterLevel">The requester user's support level.</param>
    /////// <returns>Returns list of assignee in form of <see cref="List{KeyValuePair{short,string}}"/></returns>
    ////public List<KeyValuePair<short, string>> FillAssigneeList(SupportLevelEnum requesterLevel) {
    ////  List<KeyValuePair<short, string>> assigneeList = new List<KeyValuePair<short, string>>();

    ////  // If GenerationLevel is Customer
    ////  //    If CurrentLevel is Customer Then Business And Customer
    ////  //    If CurrentLevel is Business 
    ////  //      If RequesterLevel==Customer Then ReadOnly- CurrentLevel (i.e. Business)
    ////  //      If RequesterLevel==Business Then Business AND Publisher
    ////  //    If CurrentLevel is Publisher
    ////  //      If RequesterLevel==Customer Then ReadOnly- CurrentLevel (i.e. Business)
    ////  //      If RequesterLevel==Business Then ReadOnly- CurrentLevel (i.e. Publisher)
    ////  //      If RequesterLevel==Publisher Then Publisher AND Business

    ////  if(this.GenerationLevel == (short)SupportLevelEnum.Level1) {
    ////    if(this.CurrentLevel == (short)SupportLevelEnum.Level1) {
    ////      if(requesterLevel == SupportLevelEnum.Level1) {
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, this.TenantName));
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level1, this.CreatedByName));
    ////      }
    ////      else if(requesterLevel == SupportLevelEnum.Level2) {
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level1, this.CreatedByName));
    ////      }
    ////      else if(requesterLevel == SupportLevelEnum.Level3) {
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level1, this.CreatedByName));
    ////      }
    ////    }
    ////    else if(this.CurrentLevel == (short)SupportLevelEnum.Level2) {
    ////      if(requesterLevel == SupportLevelEnum.Level1) {
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, this.TenantName));
    ////      }
    ////      else if(requesterLevel == SupportLevelEnum.Level2) {
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level1, this.CreatedByName));
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, this.TenantName));
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, Constants.SuperAdminKey));
    ////      }
    ////      else if(requesterLevel == SupportLevelEnum.Level3) {
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, this.TenantName));
    ////      }
    ////    }
    ////    else if(this.CurrentLevel == (short)SupportLevelEnum.Level3) {
    ////      if(requesterLevel == SupportLevelEnum.Level1) {
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, Constants.SuperAdminKey));
    ////      }
    ////      else if(requesterLevel == SupportLevelEnum.Level2) {
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, Constants.SuperAdminKey));
    ////      }
    ////      else if(requesterLevel == SupportLevelEnum.Level3) {
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, this.TenantName));
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, Constants.SuperAdminKey));
    ////      }
    ////    }

    ////    //switch (this.CurrentLevel) {
    ////    //  case (short)SupportLevelEnum.Level1:
    ////    //    assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, this.TenantName));
    ////    //    assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level1, this.CustomerName));
    ////    //    break;
    ////    //  case (short)SupportLevelEnum.Level2:
    ////    //    assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level1, this.CustomerName));
    ////    //    assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, this.TenantName));
    ////    //    assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, Constants.SuperAdminKey));
    ////    //    break;
    ////    //  case (short)SupportLevelEnum.Level3:
    ////    //    assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, this.TenantName));
    ////    //    assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, Constants.SuperAdminKey));
    ////    //    break;
    ////    //  default:
    ////    //    break;
    ////    //}
    ////  }
    ////  else if(this.GenerationLevel == (short)SupportLevelEnum.Level2) {
    ////    // If GenerationLevel is Business
    ////    //    If CurrentLevel is Business
    ////    //      If RequesterLevel==Business Then Business AND Publisher
    ////    //    If CurrentLevel is Publisher
    ////    //      If RequesterLevel==Business Then ReadOnly- CurrentLevel (i.e. Publisher)
    ////    //      If RequesterLevel==Publisher Then Publisher AND Business

    ////    if(this.CurrentLevel == (short)SupportLevelEnum.Level2) {
    ////      if(requesterLevel == SupportLevelEnum.Level2) {
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, this.CreatedByName));
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, Constants.SuperAdminKey));
    ////      }
    ////      else if(requesterLevel == SupportLevelEnum.Level3) {
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, this.CreatedByName));
    ////      }
    ////    }
    ////    else if(this.CurrentLevel == (short)SupportLevelEnum.Level3) {
    ////      if(requesterLevel == SupportLevelEnum.Level2) {
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, Constants.SuperAdminKey));
    ////      }
    ////      else if(requesterLevel == SupportLevelEnum.Level3) {
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, this.CreatedByName));
    ////        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, Constants.SuperAdminKey));
    ////      }
    ////    }

    ////    //switch (this.CurrentLevel) {
    ////    //  case (short)SupportLevelEnum.Level2:
    ////    //    assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, Constants.SuperAdminKey));
    ////    //    break;
    ////    //  case (short)SupportLevelEnum.Level3:
    ////    //    assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, this.TenantName));
    ////    //    break;
    ////    //  case (short)SupportLevelEnum.Level1:
    ////    //  default:
    ////    //    break;
    ////    //}
    ////  }
    ////  _currentAssigneeList = assigneeList;
    ////  return assigneeList;
    ////}

    //// Gets assignee name against support level.
    //private string GetCommentorName(short commentGenerationLevel, short ticketGenerationLevel) {
    //    SupportLevelEnum supportLevel = (SupportLevelEnum)Enum.Parse(typeof(SupportLevelEnum), commentGenerationLevel.ToString());
    //    string name = "";
    //    switch(supportLevel) {
    //        case SupportLevelEnum.None:
    //            name = "";
    //            break;
    //        case SupportLevelEnum.Level1:
    //            name = this.CreatedByName;
    //            break;
    //        case SupportLevelEnum.Level2:
    //            if(ticketGenerationLevel == (short)SupportLevelEnum.Level2) {
    //                name = this.CreatedByName;
    //            }
    //            else {
    //                name = this.TenantName;
    //            }

    //            break;
    //        case SupportLevelEnum.Level3:
    //            name = "Publisher Name";
    //            break;
    //        case SupportLevelEnum.Level4:
    //            name = Constants.SuperAdminKey;
    //            break;
    //        default:
    //            break;
    //    }
    //    return name;
    //}
  }
}
