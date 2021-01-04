
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog;


namespace ewApps.Core.CacheService
{
  /// <summary>
  /// It provides all the ewApps caching methods
  /// </summary>
  public class EwAppsDistributedCache : IEwAppsDistributedCache
  {
    private IDistributedCache _cache;
    private ILogger _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="cache">DI for IDistributedCache</param>
    public EwAppsDistributedCache(IDistributedCache cache)
    {
      _cache = cache;
      _logger = Log.ForContext<EwAppsDistributedCache>();
      _logger.Information("[{Method}] Service initialized", "EwAppsDistributedCache");
    }
    /// <inheritdoc/>
    public T Get<T>(string key)
    {
      string value = _cache.GetString(key); //Objects are saved as jSON strings
      if (string.IsNullOrEmpty(value)) return default(T);
      T obj = JsonConvert.DeserializeObject<T>(value);

      return obj;
    }

    /// <inheritdoc/>
    public async Task<T> GetAsync<T>(string key, CancellationToken token = default(CancellationToken))
    {
      string value = await _cache.GetStringAsync(key, token);
      if (string.IsNullOrEmpty(value)) return default(T);
      T obj = JsonConvert.DeserializeObject<T>(value);
      return obj;
    }

    /// <inheritdoc/>
    public string GetString(string key)
    {
      string value = _cache.GetString(key);
      return value;
    }

    /// <inheritdoc/>
    public async Task<string> GetStringAsync(string key, CancellationToken token = default(CancellationToken))
    {
      string value = await _cache.GetStringAsync(key, token);
      return value;
    }

    /// <inheritdoc/>
    public void Set<T>(string key, T value, EwAppsDistributedCacheOptions options)
    {
      string stringObj = JsonConvert.SerializeObject(value);
      if (options == null)
        _cache.SetString(key, stringObj);
      else
      {
        _cache.SetString(key, stringObj, GetOption(options));
      }
      _logger.Debug("[{method}] Calls  with key {@key} and value {@value}", "Set", key, value);

    }

    /// <inheritdoc/>
    public async Task SetAsync<T>(string key, T value, EwAppsDistributedCacheOptions options, CancellationToken token = default(CancellationToken))
    {
      string stringObj = JsonConvert.SerializeObject(value);
      if (options == null)
        await _cache.SetStringAsync(key, stringObj, token);
      else
      {
        await _cache.SetStringAsync(key, stringObj, GetOption(options), token);
      }
      _logger.Debug("[{method}] Calls  with key {@key} and value {@value}", "SetAsync", key, value);
    }


    /// <inheritdoc/>
    public void SetString(string key, string value, EwAppsDistributedCacheOptions options)
    {
      if (options == null)
        _cache.SetString(key, value);
      else
      {
        _cache.SetString(key, value, GetOption(options));
      }
      _logger.Debug("[{method}] Calls  with key {@key} and value {@value}", "SetString", key, value);
    }


    /// <inheritdoc/>
    public async Task SetStringAsync(string key, string value, EwAppsDistributedCacheOptions options, CancellationToken token = default(CancellationToken))
    {
      if (options == null)
        await _cache.SetStringAsync(key, value, token);
      else
      {
        await _cache.SetStringAsync(key, value, GetOption(options), token);
      }
      _logger.Debug("[{method}] Calls  with key {@key} and value {@value}", "SetStringAsync", key, value);
    }


    /// <inheritdoc/>
    public void Refresh(string key)
    {
      _cache.Refresh(key);
      _logger.Debug("[{method}] Calls  for key {@key} ", "Refresh", key);
    }


    /// <inheritdoc/>

    public async Task RefreshAsync(string key, CancellationToken token = default(CancellationToken))
    {
      await _cache.RefreshAsync(key, token);
      _logger.Debug("[{method}] Calls  for key {@key} ", "RefreshAsync", key);

    }

    /// <inheritdoc/>
    public void Remove(string key)
    {
      _cache.Remove(key);
      _logger.Debug("[{method}] Calls  for key {@key} ", "Remove", key);
    }

    /// <inheritdoc/>
    public async Task RemoveAsync(string key, CancellationToken token = default(CancellationToken))
    {
      await _cache.RemoveAsync(key, token);
      _logger.Debug("[{method}] Calls  for key {@key} ", "RemoveAsync", key);
    }

    #region Private

    private DistributedCacheEntryOptions GetOption(EwAppsDistributedCacheOptions option)
    {
      if (option == null) return null;
      DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();
      if (option.AbsoluteExpiration.HasValue)
        cacheOptions.AbsoluteExpiration = option.AbsoluteExpiration;
      if (option.AbsoluteExpirationRelativeToNow.HasValue)
        cacheOptions.AbsoluteExpirationRelativeToNow = option.AbsoluteExpirationRelativeToNow;
      if (option.SlidingExpiration.HasValue)
        cacheOptions.SlidingExpiration = option.SlidingExpiration;
      return cacheOptions;

    }

    #endregion
  }
}
