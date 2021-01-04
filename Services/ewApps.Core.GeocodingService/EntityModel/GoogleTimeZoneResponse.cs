using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.GeocodingService.EntityModel
{
  /// <summary>
  /// This class represents a time zone response from Google TimeZone API.
  /// </summary>
  public class GoogleTimeZoneResponse
  {

    /// <summary>
    ///  The DayLight Saving Time UTC offset value in minutes.
    /// </summary>
    public double dstOffset
    {
      get;
      set;
    }

    /// <summary>
    ///  The raw UTC offset value in minutes.
    /// </summary>
    public double rawOffset
    {
      get;
      set;
    }

    /// <summary>
    /// The status.
    /// </summary>
    public string status
    {
      get;
      set;
    }

    /// <summary>
    /// The IANA time zone id.
    /// </summary>
    public string timeZoneId
    {
      get;
      set;
    }

    /// <summary>
    /// The name of the IANA time zone.
    /// </summary>
    public string timeZoneName
    {
      get;
      set;
    }
  }
}
