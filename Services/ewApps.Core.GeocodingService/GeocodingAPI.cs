
using ewApps.Core.GeocodingService.EntityModel;
using ewApps.Core.CommonService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
//using ewApps.CommonRuntime.Common;

namespace ewApps.Core.GeocodingService
{

  /// <summary>
  /// This class contains methods to get information related to time zone and geocode information.
  /// </summary>
  public class GeocodingAPI
  {

    /// <summary>
    /// Finds the latitude longitude from address.
    /// </summary>
    /// <param name="address">The address string to find geometry information.</param>
    /// <returns>Returns geometry information based on input address.</returns>
    public static Geometry FindLatitudeLongitudeFromAddress(GeoAddressModel address)
    {
     

        GeocodeResponse response = FindGeocodeResponseFromAddress(address);

        if (response != null && response.status == HttpStatusCode.OK.ToString() && response.results.Count > 0)
        {
          Geometry geometry = null;

          if (response.results.Any(i => i.geometry.location_type.Equals("ROOFTOP")))
          {
            geometry = response.results.First(i => i.geometry.location_type.Equals("ROOFTOP")).geometry;
          }
          else if (response.results.Any(i => i.geometry.location_type.Equals("RANGE_INTERPOLATED")))
          {
            geometry = response.results.First(i => i.geometry.location_type.Equals("RANGE_INTERPOLATED")).geometry;
          }
          else if (response.results.Any(i => i.geometry.location_type.Equals("GEOMETRIC_CENTER")))
          {
            geometry = response.results.First(i => i.geometry.location_type.Equals("GEOMETRIC_CENTER")).geometry;
          }
          else if (response.results.Any(i => i.geometry.location_type.Equals("APPROXIMATE")))
          {
            geometry = response.results.First(i => i.geometry.location_type.Equals("APPROXIMATE")).geometry;
          }
          else
          {
            //RaiseInvalidRequestParameterException();
          }

          return geometry;
        }
        //else {
        //    RaiseInvalidRequestParameterException();
        //}
      //}
      //catch (Exception ex)
      //{
      //  StringBuilder logTitle = new StringBuilder();
      //  logTitle.Append(ex.Message);
      //  logTitle.AppendLine();
      //  logTitle.Append(ex.StackTrace);
      //  //MessageLogger.Instance.LogMessage(logTitle.ToString(), LoggerCategory.Production, null, false);
      //  throw;
      //}
      return null;
    }

    /// <summary>
    /// Finds the iana time zone identifier from a latitude longitude pair.
    /// </summary>
    /// <param name="latitude">Geographic latitude.</param>
    /// <param name="longitude">Geographic longitude.</param>
    /// <returns>Returns IANA timezone based on input latitude and longitude.</returns>
    public static string FindIANATimeZoneIdFromALatitudeLongitudePair(string latitude, string longitude)
    {
     

        List<KeyValuePair<string, string>> uriParam = new List<KeyValuePair<string, string>>();
        uriParam.Add(new KeyValuePair<string, string>("location", string.Format("{0},{1}", latitude, longitude)));
        uriParam.Add(new KeyValuePair<string, string>("timestamp", ToTimestamp(DateTime.UtcNow).ToString()));
        uriParam.Add(new KeyValuePair<string, string>("Key", AppSettingHelper.GetGoogleAPIServerKey()));

        string timezoneUri = AppSettingHelper.GetGoogleAPITimeZoneUri();

        //GoogleTimeZoneResponse result = HttpClientHelper.ExecuteGetRequest<GoogleTimeZoneResponse>(timezoneUri, "json", AcceptType.JSON, null, null, uriParam);
        GoogleTimeZoneResponse result = null;
        return result.timeZoneId;
      //}
      //catch (Exception ex)
      //{
      //  StringBuilder logTitle = new StringBuilder();
      //  logTitle.Append(ex.Message);
      //  logTitle.AppendLine();
      //  logTitle.Append(ex.StackTrace);
      //  //  MessageLogger.Instance.LogMessage(logTitle.ToString(), LoggerCategory.Production, null, false);
      //  throw;
      //}
    }

    /// <summary>
    /// Gets the time zone from address.
    /// </summary>
    /// <param name="address">The address information to find IANA time zone.</param>
    /// <returns>Returns IANA timezone information based on input latitude and longitude.</returns>
    public static string GetTimeZoneFromAddress(GeoAddressModel address)
    {
     
        Geometry geometry = FindLatitudeLongitudeFromAddress(address);
        if (geometry != null)
          return FindIANATimeZoneIdFromALatitudeLongitudePair(geometry.location.lat.ToString(), geometry.location.lng.ToString());
      //}
      //catch (Exception ex)
      //{
      //  StringBuilder logTitle = new StringBuilder();
      //  logTitle.Append(ex.Message);
      //  logTitle.AppendLine();
      //  logTitle.Append(ex.StackTrace);
      //  //  MessageLogger.Instance.LogMessage(logTitle.ToString(), LoggerCategory.Production, null, false);
      //  throw;
      //}
      return string.Empty;
    }

    /// <summary>
    /// Gets the time zone and geo points pair from address.
    /// </summary>
    /// <param name="address">The address to find geographical points and IANA timezone information.</param>
    /// <returns>Returns IANA timezone and location latitude-longitude pair.</returns>
    public static LocationAndTimeZoneModel GetTimeZoneAndGeoPointsPairFromAddress(GeoAddressModel address)
    {
      LocationAndTimeZoneModel locationAndTimezoneModel = new LocationAndTimeZoneModel();
      locationAndTimezoneModel.IANATimeZoneId = string.Empty;
      locationAndTimezoneModel.Latitude = -300;
      locationAndTimezoneModel.Longitude = -300;
     
        Geometry geoPoints = FindLatitudeLongitudeFromAddress(address);
        if (geoPoints != null)
        {
          string ianaTimeZoneId = FindIANATimeZoneIdFromALatitudeLongitudePair(geoPoints.location.lat.ToString(), geoPoints.location.lng.ToString());
          locationAndTimezoneModel.IANATimeZoneId = ianaTimeZoneId;
          locationAndTimezoneModel.Latitude = geoPoints.location.lat;
          locationAndTimezoneModel.Longitude = geoPoints.location.lng;
          locationAndTimezoneModel.LocationType = geoPoints.location_type;
        }
      //}
      //catch (Exception ex)
      //{
      //  StringBuilder logTitle = new StringBuilder();
      //  logTitle.Append(ex.Message);
      //  logTitle.AppendLine();
      //  logTitle.Append(ex.StackTrace);
      //  // MessageLogger.Instance.LogMessage(logTitle.ToString(), LoggerCategory.Production, null, false);
      //  throw;
      //}
      return locationAndTimezoneModel;
    }

    /// <summary>
    /// Finds the geocode response from raw address string.
    /// </summary>
    /// <param name="address">The raw address string to find exction address infomration.</param>
    /// <returns>Returns address information based on input raw address string.</returns>
    public static GeocodeResponse FindGeocodeResponseFromAddress(GeoAddressModel address)
    {

      List<KeyValuePair<string, string>> queryStringParam = new List<KeyValuePair<string, string>>();
      queryStringParam.Add(new KeyValuePair<string, string>("address", address.Address.SpilitAndConcatenate(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries, "+")));
      queryStringParam.Add(new KeyValuePair<string, string>("Key", AppSettingHelper.GetGoogleAPIServerKey()));

      string goecodeUri = AppSettingHelper.GetGoogleAPIGeocodeUri();

      GeocodeResponse response = null;
      //try
      //{
        System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) =>
        {
          return true;
        };

        //response = HttpClientHelper.ExecuteGetRequest<GeocodeResponse>(goecodeUri, "json", AcceptType.JSON, null, null, queryStringParam);
        response = null;
      //}
      //catch (Exception)
      //{
      //  //MessageLogger.Instance.LogMessage(string.Format("FindLatitudeLongitudeFromAddress: {0}", address.Address.ToString()), LoggerCategory.Production, null, false);
      //}
      return response;
    }

    /// <summary>
    /// Finds the region code from latitude longitude.
    /// </summary>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    /// <returns>Returns region code corresponding to input latitude and longitude points.</returns>
    public static string FindRegionCodeFromLatitudeLongitude(double latitude, double longitude)
    {
      List<KeyValuePair<string, string>> queryStringParam = new List<KeyValuePair<string, string>>();
      queryStringParam.Add(new KeyValuePair<string, string>("latlng", string.Format("{0},{1}", latitude, longitude)));
      queryStringParam.Add(new KeyValuePair<string, string>("Key", AppSettingHelper.GetGoogleAPIServerKey()));
      string goecodeUri = AppSettingHelper.GetGoogleAPIGeocodeUri();

      System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) =>
      {
        return true;
      };
      //GeocodeResponse response = HttpClientHelper.ExecuteGetRequest<GeocodeResponse>(goecodeUri, "json", AcceptType.JSON, null, null, queryStringParam);
      GeocodeResponse response = null;
      string regionCode = default(string);
      if (response != null && response.status == HttpStatusCode.OK.ToString() && response.results.Count > 0)
      {
        regionCode = response.results.SelectMany(i => i.address_components.Where(j => j.types.Any(k => k.Equals("country")))).FirstOrDefault().short_name;
      }
      return regionCode;
    }

    #region Private Methods

    private static double ToTimestamp(DateTime date)
    {
      DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
      TimeSpan diff = date.ToUniversalTime() - origin;
      return Math.Floor(diff.TotalSeconds);
    }

    //private static void RaiseInvalidRequestParameterException()
    //{
    //  List<EwpErrorData> ewpErrorDataList = new List<EwpErrorData>();
    //  List<KeyValuePair<string, string>> dataList = new List<KeyValuePair<string, string>>();
    //  dataList.Add(new KeyValuePair<string, string>("IANATimeZoneId", "UTC"));
    //  dataList.Add(new KeyValuePair<string, string>("Latitude", "0"));
    //  dataList.Add(new KeyValuePair<string, string>("Longitude", "0"));
    //  ewpErrorDataList.Add(new EwpErrorData()
    //  {
    //    ErrorSubType = (int)InvalidRequestArgumentErrorSubType.None,
    //    Data = dataList,
    //    Message = "Unable to locate address"
    //  });

    //  throw new EwpNullRequestArgumentException("Unable to locate address", ewpErrorDataList);
    //}

    #endregion

  }
}
