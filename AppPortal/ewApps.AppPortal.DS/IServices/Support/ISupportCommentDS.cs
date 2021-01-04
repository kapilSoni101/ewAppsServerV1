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
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using SupportLevelEnum = ewApps.AppPortal.Common.SupportLevelEnum;

namespace ewApps.AppPortal.DS {
    /// <summary>
    /// Provide functionality to write bussiness logic on support comment by creating an object to this class.
    /// </summary>
    public interface ISupportCommentDS : IBaseDS<SupportComment> {

    /// <summary>
    /// Manages support ticket comment list.
    /// </summary>
    /// <param name="supportId">Parent support ticket id.</param>
    /// <param name="supportCommentDTOList">Support comment list.</param>
    /// <param name="supportLevel">User's current support level.</param>
    /// <param name="parentOpType">Parent entity operation type.</param>
    /// <returns>Returns true if all operatoins performed on comment list is sucessful otherwise return false.</returns>
    bool ManageCommentList(Guid supportId, List<SupportCommentDTO> supportCommentDTOList, SupportLevelEnum supportLevel, OperationType parentOpType = OperationType.None);

    /// <summary>
    /// Gets list of <see cref="SupportComment"/> (in form of <see cref="SupportCommentDTO"/> that matches given support id.
    /// </summary>
    /// <param name="supportId">Support ticket id to find related comments.</param>
    /// <returns>Returns list of <see cref="SupportCommentDTO"/> that matches given support ticket id.</returns>
    IEnumerable<SupportCommentDTO> GetCommentListBySupportId(Guid supportId);
  }
}