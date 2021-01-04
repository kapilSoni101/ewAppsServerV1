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

namespace ewApps.AppPortal.Data {
    /// <summary>
    /// This interface defines repository methods to get <see cref="ewApps.Core.Entity.SupportComment"/> entity related data.
    /// </summary>
    /// <seealso cref="ewApps.Core.Entity.SupportComment"/>
    public interface ISupportCommentRepository :IBaseRepository<SupportComment> {

    /// <summary>
    /// Gets list of <see cref="SupportComment"/> (in form of <see cref="SupportCommentDTO"/> that matches given support id.
    /// </summary>
    /// <param name="supportId">Support ticket id to find related comments.</param>
    /// <returns>Returns list of <see cref="SupportCommentDTO"/> that matches given support ticket id.</returns>
    IEnumerable<SupportCommentDTO> GetCommentListBySupportId(Guid supportId);

  }
}
