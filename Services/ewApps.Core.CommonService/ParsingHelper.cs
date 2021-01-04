/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul badgujar <abadgujar@batchmaster.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.CommonService {
    /// <summary>
    ///  Provides ParsingHelper
    /// </summary>
    public class ParsingHelper {

        #region public methods 
        /// <summary>
        /// Parses to json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>

        public static string ParseToJSON<T>(T entity) {
            return Newtonsoft.Json.JsonConvert.SerializeObject(entity);
        }

        /// <summary>
        /// Parses to json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>

        public static T ParseJSONToObject<T>(string entity) {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(entity);
        }
        #endregion
    }
}
