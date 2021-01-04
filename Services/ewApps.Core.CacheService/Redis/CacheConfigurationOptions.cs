using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.CacheService
{
  /// <summary>
  /// Options to define Cache configuration
  /// </summary>
  public class CacheConfigurationOptions
  {
    /// <summary>
    /// Cache type used by the App, ex Redis
    /// </summary>
    public string CacheType { get; set; }

    /// <summary>
    /// Host IP of the Cache Server
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// Port number of the Cache server 
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Instance name of the Cache Server default master
    /// </summary>
    public string InstanceName { get; set; }

  }
}
