/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 21 Aug 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 21 Aug 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Data {
    // <summary>
    /// This interface defines repository methods to get <see cref="Portal"/> entity related data.
    /// </summary>
    /// <seealso cref="IBaseRepository{Portal}" />
    public interface IPortalRepository:IBaseRepository<Portal> {
    }
}
