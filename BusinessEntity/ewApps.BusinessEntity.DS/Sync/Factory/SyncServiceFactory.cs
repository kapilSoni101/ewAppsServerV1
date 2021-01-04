using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Serilog.ILogger;

namespace ewApps.BusinessEntity.DS {

  /// <summary>
  /// Factory class to provide carrier based class for Rate/Quotation based on Carrier code
  /// </summary>
  public class SyncServiceFactory:ISyncServiceFactory {

    /// <summary>
    /// Dictionary to provide carrier based Data services for Rate service
    /// </summary>
    private readonly Dictionary<string, ISyncServiceDS> _services;
    private ILogger _logger;

    public SyncServiceFactory() {
      _logger = Log.ForContext<SyncServiceFactory>();
      _services = new Dictionary<string, ISyncServiceDS>();
      _logger.Information("[{Method}] Service initialized", "SyncServiceFactory");
    }

    /// <inheritdoc/>
    public void Register(string name, ISyncServiceDS service) {
      _services[name] = service;
      _logger.Debug("[{method}] register carrier {carrierCode}", "Register", name);
    }

    /// <inheritdoc/>
    public ISyncServiceDS Resolve(string name) {
      return _services[name];
    }
  }
}
