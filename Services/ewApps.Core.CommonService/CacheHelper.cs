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
using System.Runtime.Caching;

namespace ewApps.Core.CommonService
{
  /// This class provides wrapper interface to cache block. 
  /// It is expected that all cache block properties and item policies are set in 
  /// the config file externally.
  /// </summary>
  public class CacheHelper
  {

    #region Constructor 
    /// <summary>
    /// Static constructor to initialize the members.
    /// </summary>
    static CacheHelper() {
    }
    #endregion

    #region public methods 
    /// <summary>
    /// Generates unique key to look up item in cache.
    /// </summary>
    /// <param name="entityName">Entity name to be look in cache.</param>
    /// <param name="id">Record id to find in cache.</param>
    /// <returns>A unique key, combination of entityName and id.</returns>
    public static string GetCacheKey(string entityName, Guid id) {
      string key = entityName + "-" + id.ToString();
      return key;
    }

    /// <summary>
    /// Generates unique key to look up item in cache.
    /// </summary>
    /// <param name="entityName">Entity name to be look in cache.</param>
    /// <param name="id">Record id to find in cache.</param>
    /// <returns>A unique key, combination of entityName and id.</returns>
    public static string GetCacheKey(string entityName, int id) {
      string key = entityName + "-" + id.ToString();
      return key;
    }

    /// <summary>
    /// Generates unique key to look up item in cache.
    /// </summary>
    /// <param name="entityName">Entity name to be look in cache.</param>
    /// <param name="compositIds">Composit Ids seperated by - sign to find in cache.</param>
    /// <returns>A unique key, combination of entityName and id.</returns>
    public static string GetCacheKey(string entityName, string compositIds) {
      string key = entityName + "-" + compositIds;
      return key;
    }

    /// <summary>
    /// Gets the cache data for a given cache key from given cache block.
    /// </summary>
    /// <typeparam name="T">Type of cached item to cast from object to actual type.</typeparam>
    /// <param name="key">A unique identifier for the cache entry.</param>
    /// <returns>The cache entry that is identified by key.</returns>
    /// <remarks>
    /// Since the cache manager returns the data as an object, type cast to the given type, T, is required.
    /// </remarks>
    public static T GetData<T>(string key) {
      return (T)MemoryCache.Default.Get(key);
    }



    /// <summary>
    /// Removes the cache entry from the cache.
    /// </summary>
    /// <param name="key">A unique identifier for the cache entry.</param>
    public static void RemoveData(string key) {
      if (IsInCache(key))
        MemoryCache.Default.Remove(key);
    }

    /// <summary>
    /// Flush the specified cache block data.
    /// </summary>
    /// <remarks>
    /// Once the cache block is disposed, any query for cache item returns null unless cache block is reinitialize.
    /// </remarks>
    public static void FlushCache() {
      MemoryCache.Default.Dispose();
    }

    /// <summary>
    /// Checks whether the cache entry already exists in the cache.
    /// </summary>
    /// <param name="key">A unique identifier for the cache entry.</param>
    ///<returns>true if the cache contains a cache entry with the same key value as key; otherwise, false.</returns>
    public static bool IsInCache(string key) {
      return MemoryCache.Default.Contains(key);
    } 
    #endregion

  }
}
