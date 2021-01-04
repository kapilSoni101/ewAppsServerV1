using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.GeocodingService.EntityModel
{
  /// <summary>
  /// This entity model contains google geocoding API response information.
  /// </summary>
  public class LocationAndTimeZoneModel
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="LocationAndTimeZoneModel"/> class.
    /// </summary>
    public LocationAndTimeZoneModel()
    {
      IANATimeZoneId = "";
      LocationType = "";
      Latitude = 0;
      Longitude = 0;
    }

    /// <summary>
    /// The iana time zone id corresponding to requested address.
    /// </summary>
    public string IANATimeZoneId
    {
      get;
      set;
    }

    /// <summary>
    /// The type of the location defines the accuracy of address.
    /// </summary>
    public string LocationType
    {
      get;
      set;
    }

    /// <summary>
    /// The latitude point corrsponding to requested address.
    /// </summary>
    public double Latitude
    {
      get;
      set;
    }

    /// <summary>
    /// The longitude point corrsponding to requested address.
    /// </summary>
    public double Longitude
    {
      get;
      set;
    }

  }

  /// <summary>
  /// This model class contain address information to find time zone and location related information using google geocode API.
  /// </summary>
  public struct GeoAddressModel
  {

    /// <summary>
    /// The address as string to find location and time zone related information.
    /// </summary>
    public string Address
    {
      get;
      set;
    }

  }
}
