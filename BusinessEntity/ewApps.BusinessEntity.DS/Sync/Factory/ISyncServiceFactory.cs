using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DS {

  public interface ISyncServiceFactory {

    /// <summary>
    /// Register the Quote Data Service
    /// </summary>
    /// <param name="name">Name of the carrier</param>
    /// <param name="carrier">Data service class for the carrier</param>
    void Register(string name, ISyncServiceDS service);

    /// <summary>
    /// Gets the Quote Data Service for given carrier
    /// </summary>
    /// <param name="name">Carrier Name</param>
    /// <returns>Carrier Data Service class</returns>
    ISyncServiceDS Resolve(string name);
  }
}
