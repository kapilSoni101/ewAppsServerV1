using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.CacheService
{
  /// <summary>
  /// interface that exposed all method required by ewApps Cache
  /// </summary>
  public interface IEwAppsDistributedCache
  {
    /// <summary>
    /// Get the object stored with given Key
    /// </summary>
    /// <typeparam name="T">Type of object stored</typeparam>
    /// <param name="key">Key of the object stored</param>
    /// <returns>Object of type T</returns>
    T Get<T>(string key);

    /// <summary>
    /// Gets the object stored with given Key
    /// </summary>
    /// <typeparam name="T">Type of object stored</typeparam>
    /// <param name="key">Key of the object stored</param>
    /// <param name="token"> cancellation Token</param>
    /// <returns>Object of Type T</returns>
    Task<T> GetAsync<T>(string key, CancellationToken token);

    /// <summary>
    /// Get the string stored with given Key
    /// </summary>
    /// <param name="key">Key of the string stored</param>
    /// <returns>string value</returns>
    string GetString(string Key);

    /// <summary>
    /// Get the string stored with given Key
    /// </summary>
    /// <param name="key">Key of the string stored</param>
    /// <param name="token"> cancellation Token</param>
    /// <returns>string value</returns>
    Task<string> GetStringAsync(string key, CancellationToken token);

    /// <summary>
    /// Sets the value for given key in the cache 
    /// </summary>
    /// <param name="key">key</param>
    /// <param name="value">string value</param>
    /// <param name="options">Cache options thst defines the life of the stored cache value</param>
    void SetString(string key, string value, EwAppsDistributedCacheOptions options);

    /// <summary>
    /// Sets the value for given key in the cache 
    /// </summary>
    /// <param name="key">key</param>
    /// <param name="value">string value</param>
    /// <param name="token">CAncellation Token</param>
    /// <param name="options">Cache options thst defines the life of the stored cache value</param>
    Task SetStringAsync(string key, string value, EwAppsDistributedCacheOptions options, CancellationToken token);

    /// <summary>
    /// Sets the value for given key in the cache of type T
    /// </summary>
    /// <param name="key">key</param>
    /// <param name="value">object to store</param>
    /// <param name="options">Cache options thst defines the life of the stored cache value</param>
    void Set<T>(string key, T value, EwAppsDistributedCacheOptions options);

    /// <summary>
    /// Sets the value for given key in the cache of type T
    /// </summary>
    /// <param name="key">key</param>
    /// <param name="value">object to store</param>
    /// <param name="token">Cancellation Token</param>
    /// <param name="options">Cache options thst defines the life of the stored cache value</param>
    Task SetAsync<T>(string key, T value, EwAppsDistributedCacheOptions options, CancellationToken token);

    /// <summary>
    /// Refresh the expiration time of the key, if sliding
    /// </summary>
    /// <param name="key">Key to be refreshed</param>

    void Refresh(string key);

    /// <summary>
    /// Refresh the expiration time of the key, if sliding
    /// </summary>
    /// <param name="key">Key to be refreshed</param>
    /// <param name="token">Cancellation Token</param>
    Task RefreshAsync(string key, CancellationToken token);

    /// <summary>
    /// Deltes the key
    /// </summary>
    /// <param name="key">Key to be deleted</param>
    void Remove(string key);

    /// <summary>
    /// Deltes the key
    /// </summary>
    /// <param name="key">Key to be deleted</param>
    /// <param name="token"> Cancellation Token</param>
    Task RemoveAsync(string key, CancellationToken token);


  }
}
