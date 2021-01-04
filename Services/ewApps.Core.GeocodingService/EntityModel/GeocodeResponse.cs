using System.Collections.Generic;

namespace ewApps.Core.GeocodingService
{

  /// <summary>
  /// Response of Geocoding api.
  /// </summary>
  public class GeocodeResponse
  {
    /// <summary>
    /// A single geocoder result retrieved from the geocode server in response of geocoding api.
    /// </summary>
    public List<Result> results
    {
      get;
      set;
    }

    /// <summary>
    /// The status returned by the Geocoder on the completion of a call to geocode Api.
    /// </summary>
    /// <value>
    /// Possible values are ERROR, INVALID_REQUEST, OK, OVER_QUERY_LIMIT, REQUEST_DENIED, UNKNOWN_ERROR and ZERO_RESULTS.
    /// </value>
    public string status
    {
      get;
      set;
    }
  }


  /// <summary>
  /// Google geocoding API response has different parts.
  /// This class represents a street address component in the format used by the national postal service of the country concerned, 
  /// received in geocoding api response.
  /// </summary>
  public class AddressComponent
  {
    /// <summary>
    /// Address long name.
    /// </summary>
    /// <remarks>Address long name and short name value can be same depending on type.
    /// Ex. In-case of Type='Street Number', long and short names are same for address '1600 Amphitheatre Parkway, Mountain View, CA'.
    /// </remarks>
    public string long_name
    {
      get;
      set;
    }

    /// <summary>
    /// Address short name.
    /// </summary>
    /// <remarks>Address long name and short name value can be same depending on type.
    /// Ex. In-case of Type='Street Number', long and short names are same for address '1600 Amphitheatre Parkway, Mountain View, CA'.
    /// </remarks>
    public string short_name
    {
      get;
      set;
    }

    /// <summary>
    /// The address type indicates the current address component. Ex. 'street_number', 'locality' etc.
    /// </summary>
    /// <remarks>
    /// A single address component can have multiple types.
    /// Ex. For address '1600 Amphitheatre Parkway, Mountain View, CA', 'Mountain View' address part has two types "locality", "political".
    /// </remarks>
    public List<string> types
    {
      get;
      set;
    }
  }

  /// <summary>
  /// This class represents view port that contains the recommended viewport for displaying the returned result. 
  /// Generally the viewport is used to frame a result when displaying it to a user.
  /// </summary>
  public class Viewport
  {
    /// <summary>
    /// The latitude-longitude pair from North-East geographic coordinate system.
    /// </summary>
    public LatLongPoints northeast
    {
      get;
      set;
    }

    /// <summary>
    /// The latitude-longitude pair from South-West geographic coordinate system.
    /// </summary>
    public LatLongPoints southwest
    {
      get;
      set;
    }
  }

  /// <summary>
  /// Google geocoding API response has different parts. This class represents Bound use to stores the bounding box which can fully contain the returned result.
  /// </summary>
  /// <remarks>
  /// These bounds may not match the recommended viewport. 
  /// (For example, San Francisco includes the Farallon islands, which are technically part of the city, 
  /// but probably should not be returned in the viewport.)
  /// </remarks>
  public class Bounds
  {

    /// <summary>
    /// The latitude-longitude pair from North-East geographic coordinate system.
    /// </summary>
    public LatLongPoints northeast
    {
      get;
      set;
    }

    /// <summary>
    /// The latitude-longitude pair from South-West geographic coordinate system.
    /// </summary>
    public LatLongPoints southwest
    {
      get;
      set;
    }
  }

  /// <summary>
  /// This class represents different set of point in form of latitude and longitude points.
  /// </summary>
  public class Geometry
  {

    /// <summary>
    /// The Latitude-Longitude pair for location.
    /// </summary>
    public LatLongPoints location
    {
      get;
      set;
    }

    /// <summary>
    /// The type of location returned in location/address.
    /// </summary>
    public string location_type
    {
      get;
      set;
    }

    /// <summary>
    /// The bounds of the recommended viewport for displaying this address result.
    /// </summary>
    public Viewport viewport
    {
      get;
      set;
    }

    /// <summary>
    /// The precise bounds of this GeocoderResult.
    /// </summary>
    public Bounds bounds
    {
      get;
      set;
    }
  }

  /// <summary>
  /// This class represents Google Geocoding Api response.
  /// </summary>
  public class Result
  {
    /// <summary>
    /// This property contains the list of different types of address component like street address etc.
    /// </summary>
    public List<AddressComponent> address_components
    {
      get;
      set;
    }

    /// <summary>
    /// This property contains formatted address string.
    /// </summary>
    public string formatted_address
    {
      get;
      set;
    }

    /// <summary>
    /// Different set of point in form of latitude and longitude points.
    /// </summary>
    public Geometry geometry
    {
      get;
      set;
    }

    /// <summary>
    /// Whether the geocoder did not return an exact match for the original request, though it was able to match part of the requested address.
    /// </summary>
    /// <value>
    ///   <c>true</c> if [partial match]; otherwise, <c>false</c>.
    /// </value>
    public bool partial_match
    {
      get;
      set;
    }

    /// <summary>
    /// The place ID associated with the location. Place IDs uniquely identify a place in the Google Places database and on Google Maps.
    /// </summary>
    public string place_id
    {
      get;
      set;
    }

    /// <summary>
    /// An array of strings denoting the type of the returned geocoded element.
    /// </summary>
    public List<string> type
    {
      get;
      set;
    }
  }

  /// <summary>
  /// The latitude-longitude pair that represent the a point coordinates at geographic coordinate system.
  /// </summary>
  public class LatLongPoints
  {
    /// <summary>
    /// The latitude point (in degree).
    /// </summary>
    public double lat
    {
      get;
      set;
    }

    /// <summary>
    /// The longitude point (in degree).
    /// </summary>
    public double lng
    {
      get;
      set;
    }

  }
}
