using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ewApps.Core.TimeZoneService {
    /// <summary>
    /// This class provides time zone extension methods to get mapping between IANA and Windows time zones.
    /// </summary>
    public class TimeZoneMapper {

        // This is a dictionary to extend NodaTime IANA to Windows time zone mapping.
        // Its key is IANA Time Zone Id and Value is Window Time Zone Id.
        private static Dictionary<string, string> _customIANAToWindowsMapping = null;

        /// <summary>
        /// Initializes the <see cref="TimeZoneMapper"/> class.
        /// </summary>
        static TimeZoneMapper() {
            InitializeCustomIANAToWindowsTimeZoneMapping();
        }

        // Initializeds extended 
        private static void InitializeCustomIANAToWindowsTimeZoneMapping() {
            _customIANAToWindowsMapping = new Dictionary<string, string>();

            _customIANAToWindowsMapping.Add("Asia/Calcutta", "India Standard Time");

            // The America/Cordoba time zone has been deemed obsolete. 
            // It has been replaced by America/Argentina/Cordoba
            _customIANAToWindowsMapping.Add("America/Cordoba", "Argentina Standard Time");

            // The US/Pacific is link to America/Los_Angeles.
            _customIANAToWindowsMapping.Add("US/Pacific", "Pacific Standard Time");

            _customIANAToWindowsMapping.Add("US/Central", "Central Standard Time");

            _customIANAToWindowsMapping.Add("Asia/Katmandu", "Nepal Standard Time");

            _customIANAToWindowsMapping.Add("Etc/GMT+12", "Dateline Standard Time");

            _customIANAToWindowsMapping.Add("Zulu", "Greenwich Standard Time");

            _customIANAToWindowsMapping.Add("Pacific/Marquesas", "Marquesas Standard Time");

            _customIANAToWindowsMapping.Add("America/Buenos_Aires", "Argentina Standard Time");

            _customIANAToWindowsMapping.Add("Etc/GMT+9", "UTC-09");
            _customIANAToWindowsMapping.Add("Pacific/Gambier", "UTC-09");

            _customIANAToWindowsMapping.Add("Etc/GMT+8", "UTC-08");
            _customIANAToWindowsMapping.Add("Pacific/Pitcairn", "UTC-08");

            _customIANAToWindowsMapping.Add("Etc/GMT+2", "UTC-02");
            _customIANAToWindowsMapping.Add("America/Noronha", "UTC-02");
            _customIANAToWindowsMapping.Add("Atlantic/South_Georgia", "UTC-02");

            _customIANAToWindowsMapping.Add("Pacific/Norfolk", "Norfolk Standard Time");

            _customIANAToWindowsMapping.Add("Asia/Saigon", "SE Asia Standard Time");

            _customIANAToWindowsMapping.Add("America/Cancun", "Eastern Standard Time(Mexico)");

            _customIANAToWindowsMapping.Add("UTC", "UTC");

            _customIANAToWindowsMapping.Add("America/Indianapolis", "US Eastern Standard Time");

            _customIANAToWindowsMapping.Add("Africa/Sao_Tome", "W. Central Africa Standard Time");

        }

        /// <summary>
        /// Maps the provided Window time zone to iana time zone.
        /// </summary>
        /// <param name="windowsTimeZoneId">The windows time zone id.</param>
        /// <returns>Returns mapped IANA time zone id.</returns>
        /// <remarks>
        /// <ul>
        /// <li>
        /// If provided Window time zone id is not matching with defined set of Windows time zones return null.
        /// </li>
        /// <li>
        /// If provided Window time zone doesn't map with any IANA time zone return null;
        /// </li>
        /// <li>
        /// If provided Window time zone id is null throw an Exception Null Reference Exception.
        /// </li>
        /// /// <li>
        /// If provided Window time zone id is Empty Or Invalid throw an Exception Time Zone Not Found Exception.
        /// </li>
        /// </ul>
        /// </remarks>
        public static string WindowsToIANATimeZone(string windowsTimeZoneId) {
            string lowerCaseWindowsTimeZoneId = windowsTimeZoneId.ToLower();

            // Check that if passed windows time zone is UTC and map it to IANA UTC time zone.
            if(lowerCaseWindowsTimeZoneId.Equals("UTC", StringComparison.Ordinal))
                return "Etc/UTC";

            // Gets NodaTime time zone default database.
            NodaTime.TimeZones.TzdbDateTimeZoneSource tzdbSource = NodaTime.TimeZones.TzdbDateTimeZoneSource.Default;

            // Gets TimeZoneInfo instance corresponding to provided windowsTimeZoneId.
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(lowerCaseWindowsTimeZoneId);

            // If provided window time zone doesn't match in and TimeZoneInfo object is null.
            if(tzi == null)
                return null;

            // Gets Window's TimeZoneInfo object in NodaTime database.
            string tzid = tzdbSource.WindowsMapping.PrimaryMapping[tzi.StandardName];

            // If provided windows time zone doesn't match in NodaTime IANA time zone DB then return null.
            if(tzid == null)
                return null;

            // Returns reqested IANA time zone corresponding to provided windows time zone.
            return tzdbSource.CanonicalIdMap[tzid];
        }

        /// <summary>
        /// Maps provided IANA time zone to windows time zone.
        /// </summary>
        /// <param name="ianaTimeZoneId">The IANA time zone id.</param>
        /// <returns>
        /// Returns mapped Windows time zone id.
        /// </returns>
        /// <remarks>
        /// <ul>
        ///   <li>
        /// If provided IANA time zone doesn't match with any Windows time zone returns null.
        ///  </li>
        ///   <li>
        /// If provided Null IANA time zone then it thrown Argument Null Exception .
        ///  </li>
        ///   <li>
        /// If provided Empty Or Invalid IANA time zone then then it return Null Value.
        ///  </li>
        /// </ul>
        /// </remarks>
        public static string IANAToWindowsTimeZone(string ianaTimeZoneId) {

            // There's also "Etc/UCT". This is so that applications that rely on the TZDB for time zone abbreviations can use their choice 
            // of abbreviation (GMT, UTC, or UCT).
            string[] utcZones = new string[] { "Etc/UTC", "Etc/UCT", "Etc/GMT" };

            // Check provided iana time zone id with all abbreviated UTC time zones.
            // If provided windows time zone id is UTC, return windows time zone id.
            if(utcZones.Contains(ianaTimeZoneId, StringComparer.Ordinal))
                return "UTC";

            // Gets NodaTime time zone default database.
            NodaTime.TimeZones.TzdbDateTimeZoneSource tzdbSource = NodaTime.TimeZones.TzdbDateTimeZoneSource.Default;

            List<string> links = new List<string>();
            // Resolve any link, since the CLDR doesn't necessarily use canonical IDs
            // Returns a lookup from canonical ID (e.g. "Europe/London") to a group of aliases 
            // (e.g. {"Europe/Belfast", "Europe/Guernsey", "Europe/Jersey", "Europe/Isle_of_Man", "GB", "GB-Eire"}).
            links = (tzdbSource.CanonicalIdMap
                .Where(x => x.Value.Equals(ianaTimeZoneId, StringComparison.Ordinal))
                .Select(x => x.Key)).ToList<string>();

            List<string> possibleZones = new List<string>();
            // Resolve canonical zones as well
            if(tzdbSource.CanonicalIdMap.ContainsKey(ianaTimeZoneId)) {
                possibleZones.AddRange(links.Concat(new[] { tzdbSource.CanonicalIdMap[ianaTimeZoneId] }));
            }
            else {
                possibleZones = links;
            }

            // Gets MapZones of windows time zones.
            var mappings = tzdbSource.WindowsMapping.MapZones;

            // Try to get IANA time zones list from matched map zone list.
            var item = mappings.FirstOrDefault(x => x.TzdbIds.Any(possibleZones.Contains));

            // If System doesn't find any iana time zone mapping 
            if(item == null) {
                if(_customIANAToWindowsMapping.ContainsKey(ianaTimeZoneId)) {
                    return _customIANAToWindowsMapping[ianaTimeZoneId];
                }
                return null;
            }
            return item.WindowsId;
        }

    

        public static Tuple<bool, double> GetUTCOffsetInMinutesByTimeZone(string ianaTimeZoneId) {
            Tuple<bool, double> offsetValues;
            double offsetInMinutes = 0;
            bool timeZoneFound = false;
            string windowsTZId = TimeZoneMapper.IANAToWindowsTimeZone(ianaTimeZoneId);

            if(!string.IsNullOrEmpty(windowsTZId)) {
                timeZoneFound = true;
                TimeZoneInfo tzInfo = TimeZoneInfo.FindSystemTimeZoneById(windowsTZId);
                offsetInMinutes = tzInfo.GetUtcOffset(DateTime.UtcNow).TotalMinutes;
            }

            return offsetValues = new Tuple<bool, double>(timeZoneFound, offsetInMinutes);
        }

    }
}
