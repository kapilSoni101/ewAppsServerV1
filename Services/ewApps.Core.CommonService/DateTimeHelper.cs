/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul badgujar <abadgujar@batchmaster.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using ewApps.Core.TimeZoneService;

namespace ewApps.Core.CommonService {

    /// <summary>
    /// Provides methods to validate and parse different DateTime formats.   
    /// </summary>
    public class DateTimeHelper {

        #region public methods 
        /// <summary>
        /// Validate text by diffrent date format and cast text into date, if it is 
        /// of date time type.
        /// </summary>
        /// <param name="searchText">Search text.</param>
        /// <param name="searchDate">Valid date in output.</param>
        /// <returns>True, if date is valid.</returns>
        public static bool IsValidDate(string searchText, out string searchDate) {
            DateTime validDate;
            searchDate = string.Empty;
            bool isValidDate = DateTime.TryParseExact(searchText, DateFormates(), null,
                                       DateTimeStyles.AllowWhiteSpaces,
                                       out validDate);

            if(validDate < DateTime.Parse("1/1/1753")) {
                isValidDate = false;

            }
            // Swap date and month to make another possible date.  
            // For Ex- if date is 7/8/2014 than make another date 8/7/2014 by swaping month and date.
            if(isValidDate) {
                if(validDate.Day <= 12 && validDate.Month <= 12 && (validDate.Day != validDate.Month))
                    searchDate = string.Format("'{0}/{1}/{2}','{1}/{0}/{2}'", validDate.Day, validDate.Month, validDate.Year);
                else
                    searchDate = string.Format("'{0}/{1}/{2}'", validDate.Month, validDate.Day, validDate.Year);
            }

            return isValidDate;
        }

        /// <summary>
        /// Provides possible date formates.
        /// </summary>
        /// <returns>Array of date formates.</returns>
        public static string[] DateFormates() {
            string[] formats = {
                    "dd/MM/yyyy", "dd.MM.yyyy", "dd-MM-yyyy", "dd MMMM yyyy", "dd MMM yyyy",
                    "d/MM/yyyy", "d.MM.yyyy", "d-MM-yyyy",   "d MMMM yyyy", "d MMM yyyy",
                    "dd/M/yyyy", "dd.M.yyyy", "dd-M-yyyy",
                    "d/M/yyyy", "d.M.yyyy", "d-M-yyyy",
                    "MM/dd/yyyy", "MM.dd.yyyy", "MM-dd-yyyy", "MMMM dd yyyy", "MMM dd yyyy",
                    "M/dd/yyyy",  "M.dd.yyyy",  "M-dd-yyyy",  "MMMM d yyyy",  "MMM d yyyy",
                    "MM/d/yyyy", "MM.d.yyyy", "MM-d-yyyy",
                    "M/d/yyyy", "M.d.yyyy", "M-d-yyyy",
                    "yyyy/MM/dd", "yyyy.MM.dd", "yyyy-MM-dd", "yyyy MMMM dd", "yyyy MMM dd",
                    "yyyy/MM/d",  "yyyy.MM.d",  "yyyy-MM-d",  "yyyy MMMM d",  "yyyy MMM d",
                    "yyyy/M/dd",  "yyyy.M.dd",  "yyyy-M-dd" ,
                    "yyyy/M/d",   "yyyy.M.d",   "yyyy-M-d", "yyyy M d" ,
                    "yyyy/d/M",   "yyyy.d.M",   "yyyy-d-M", "yyyy d M" };

            return formats;
        }

        /// <summary>
        /// Compares the value of first System.DateTime instance to a second System.DateTime value
        /// and returns an integer that indicates whether first instance is earlier than,
        /// the same as, or later than the second System.DateTime value.
        /// </summary>
        /// <param name="date1">The first date to be compare.</param>
        /// <param name="date2">The second date to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of first instance and the second parameter.
        /// Value Description Less than zero means first instance is earlier than
        /// value. Zero means first instance is the same as second value.
        /// Greater than zero means first instance is later than second value.
        /// </returns>
        public static int CompareDatesWithMinutes(DateTime date1, DateTime date2) {
            string dateFormat = "MM/dd/yyy hh:mm tt";
            DateTime date1WithMinutes = DateTime.Parse(date1.ToString(dateFormat));
            DateTime date2WithMinutes = DateTime.Parse(date2.ToString(dateFormat));

            return date1WithMinutes.CompareTo(date2WithMinutes);
        }





        /// <summary>
        /// Gets the date excluding seconds.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime GetDateExcludingSeconds(DateTime date) {
            return Convert.ToDateTime(date.ToString("MM-dd-yyyy HH:mm tt"));
        }

        /// <summary>
        /// Gets the SQL minimum date.
        /// </summary>
        /// <value>
        /// The SQL minimum date.
        /// </value>
        public static DateTime SQLMinDate {
            get {
                //return DateTime.Parse("1/1/1753 12:00:00");
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Gets the SQL maximum date.
        /// </summary>
        /// <value>
        /// The SQL maximum date.
        /// </value>
        public static DateTime SQLMaxDate {
            get {
                //return DateTime.Parse("12/31/9999 11:59:59 PM");
                return DateTime.MaxValue;
            }
        }

        /// <summary>
        /// Gets the time difference between date1 and date2 in given unit by substracting second date from first date.
        /// </summary>
        /// <param name="date1">First date of which is to substract second date.</param>
        /// <param name="date2">Second date to be substract from date1.</param>
        /// <param name="frequencyType">The unit in which value is return.</param>
        /// <returns>Returns difference between two dates in given unit.</returns>
        public static int GetTimeDifferenceInGivenUnit(DateTime date1, DateTime date2, string frequencyType) {
            int difference = 0;
            TimeSpan dateDiff = GetDateExcludingSeconds(date1).Subtract(GetDateExcludingSeconds(date2));
            switch(frequencyType.ToLower()) {
                case "minute":
                case "minutes":
                    difference = (int)dateDiff.TotalMinutes;
                    break;
                case "hour":
                case "hours":
                    difference = (int)dateDiff.TotalHours;
                    break;
                case "day":
                case "days":
                    difference = (int)dateDiff.TotalDays;
                    break;
                case "week":
                case "weeks":
                    return (int)(dateDiff.TotalDays / 7);
                    //case "month":
                    //case "months":
                    //  return (int)dateDiff.tot/ 7;
                    //  break;
                    //case "quater":
                    //case "quaters":
                    //  break;
            }
            return difference;
        }

        /// <summary>
        /// Gets Strings from ns date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="inMillisec">if set to <c>true</c> [in millisec].</param>
        /// <param name="useUTC">if set to <c>true</c> [use UTC].</param>
        /// <returns>Returns string from ns date.</returns>
        public static string StringFromDate(DateTime? date, Boolean inMillisec, Boolean useUTC) {
            string dateString;
            if(date.HasValue) {
                string s = "yyyy-MM-dd'T'HH:mm:ss";
                if(inMillisec) {
                    s += ".fff";
                }

                if(useUTC) {
                    date = date.Value.ToUniversalTime();
                }

                dateString = date.Value.ToString(s);

                return dateString;
            }

            return null;
        }

        /// <summary>
        /// Convert the date string in POSIX date format and return the date as NSDate 2013-12-15T01:47:44.026.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="inMillisec">if set to <c>true</c> [in millisec].</param>
        /// <param name="useUTC">if set to <c>true</c> [use UTC].</param>
        /// <returns></returns>
        public static DateTime? DateFromString(String s, Boolean inMillisec, Boolean useUTC) {
            if(!string.IsNullOrEmpty(s)) {
                DateTime date;
                string s2 = "yyyy-MM-dd'T'HH:mm:ss";

                if(inMillisec) {
                    s2 += ".fff";
                }

                date = DateTime.Parse(s);

                if(useUTC) {
                    date = date.ToUniversalTime();
                }

                return Convert.ToDateTime(date.ToString(s2));
            }
            return null;
        }



        /// <summary>
        /// Formats the date.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="removeSeconds">if set to <c>true</c> [remove seconds].</param>
        /// <param name="kind">The kind.</param>
        /// <returns></returns>
        public static DateTime? FormatDate(DateTime? dateTime, bool removeSeconds = false, DateTimeKind kind = DateTimeKind.Utc) {
            //Set invariant culture 
            CultureInfo invc = new CultureInfo("");
            // Specify Kind  
            if(dateTime.HasValue) {
                if(removeSeconds) {
                    return DateTime.SpecifyKind(Convert.ToDateTime(dateTime.Value.ToString("MM/dd/yyyy hh:mm tt", invc), invc), kind);
                }
                else {
                    return DateTime.SpecifyKind(Convert.ToDateTime(dateTime.Value, invc), kind);
                }
            }
            else {
                return dateTime;
            }
        }

        /// <summary>
        /// Converts the specified string to an equivalent date and time.
        /// </summary>
        /// <param name="dateTime">A string that contains a date and time to convert.</param>
        /// <returns>Returns the date and time equivalent of the given datetime string.</returns>
        public static DateTime? ToDateTime(string dateTime) {
            CultureInfo invariantCulture = new CultureInfo("");
            return Convert.ToDateTime(dateTime, invariantCulture);
        }

        /// <summary>
        /// Gets the last week start and end date.
        /// </summary>
        /// <param name="currentDate">The current date.</param>
        /// <returns>Returns Start and End date of last week of given date.</returns>
        public static Tuple<DateTime, DateTime> GetLastWeekStartAndEndDate(DateTime currentDate) {
            DateTime date = currentDate.AddDays(-7);
            while(date.DayOfWeek != DayOfWeek.Sunday) {
                date = date.AddDays(-1);
            }

            DateTime startDate = date;
            DateTime endDate = date.AddDays(6);
            return new Tuple<DateTime, DateTime>(startDate, endDate);
        }

        /// <summary>
        /// Gets the current week start and end date.
        /// </summary>
        /// <param name="currentDate">The current date.</param>
        /// <returns>Returns Start and End date of current week of given date.</returns>
        public static Tuple<DateTime, DateTime> GetCurrentWeekStartAndEndDate(DateTime currentDate) {
            var thisWeekStart = currentDate.AddDays(-(int)currentDate.DayOfWeek);
            var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);
            return new Tuple<DateTime, DateTime>(thisWeekStart, thisWeekEnd);
        }

        /// <summary>
        /// Appends the time part of source date with provided time part.
        /// </summary>
        /// <param name="sourceDate">The source date.</param>
        /// <param name="timePart">The time part.</param>
        /// <returns>Returns the updated date.</returns>
        public static DateTime AppendTimePart(DateTime sourceDate, string timePart) {
            DateTime newDate = DateTime.Parse(string.Format("{0} {1}", sourceDate.ToShortDateString(), timePart));
            return new DateTime(newDate.Ticks, sourceDate.Kind);
        }

        /// <summary>
        /// Gets the next weekday.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="day">The day.</param>
        /// <returns></returns>
        public static DateTime GetNextWeekday(DateTime startDate, DayOfWeek day) {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)startDate.DayOfWeek + 7) % 7;
            if(daysToAdd == 0) {
                daysToAdd = 7;
            }
            return startDate.AddDays(daysToAdd);
        }


        /// <summary>
        /// Convert local datetime to an utc date and time
        /// </summary>
        /// <param name="localDateTime">Local date and time</param>
        /// <param name="ianaTimeZone">IANA time zone.</param>
        /// <returns>UTC date and time</returns>
        public static DateTime? ConvertLocalToUtc(DateTime localDateTime, string ianaTimeZone) {
            string windowsTimeZoneId = TimeZoneMapper.IANAToWindowsTimeZone(ianaTimeZone);
            TimeZoneInfo windowsTimeZone = TimeZoneInfo.FindSystemTimeZoneById(windowsTimeZoneId);
            localDateTime = TimeZoneInfo.ConvertTime(localDateTime, TimeZoneInfo.Local, windowsTimeZone);
            return localDateTime.ToUniversalTime();
        }


        /// <summary>
        /// Converts the specified UTC date-time to local date-time in given time zone.
        /// </summary>
        /// <param name="utcTime">The current UTC time.</param>
        /// <param name="ianaTimeZoneId">The iana time zone id.</param>
        /// <param name="excludeTime">if set to <c>true</c> [exclude time].</param>
        /// <returns>
        /// Returns the local DateTime of specified time zone.
        /// </returns>
        /// <exception cref="System.TimeZoneNotFoundException">
        /// Throw when system is unable to find windows time zone corresponding to specified iana time zone.
        /// </exception>
        public static DateTime ConvertUTCToLocalInGivenTimeZone(DateTime utcTime, string ianaTimeZoneId, bool excludeTime) {
            string windowsTimeZoneId = string.Empty;
            if(string.IsNullOrEmpty(ianaTimeZoneId) == false) {
                windowsTimeZoneId = TimeZoneMapper.IANAToWindowsTimeZone(ianaTimeZoneId);
            }
            //if (string.IsNullOrEmpty(windowsTimeZoneId)) {
            //    //   windowsTimeZoneId = Constants.UTCWindowsTimeZoneId;
            //    throw new System.TimeZoneNotFoundException();
            //}
            DateTime requiredDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(utcTime, windowsTimeZoneId);

            if(excludeTime) {
                requiredDate = DateTime.Parse(requiredDate.ToShortDateString());
            }

            return requiredDate;
        }

        public static string FormatDate(DateTime dateTime, string dateTimeFormat) {
            return dateTime.ToString(dateTimeFormat);
        }

        #endregion

    }
}
