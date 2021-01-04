using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.Common {

  /// <summary>
  /// List of term period types. 
  /// </summary>
  public enum TermPeriod { Month, Quarter, Year, OneMinute, FiveMinutes, TenMinutes };

  /// <summary>
  /// This is helper class to assist RecurringPayment implementation.
  /// It is a static class.
  /// </summary>
  public static class RecurringPaymentHelper {

    /// <summary>
    /// Calculate EndDate. Note that it is >= StartDate.
    /// </summary>
    /// <param name="startDate">Start date of recurring periods</param>
    /// <param name="nTerms">Number of terms</param>
    /// <param name="period">Cyclic period of terms</param>
    /// <returns></returns>
    public static DateTime CalculateEndDate(DateTime startDate, int nTerms, TermPeriod period) {
      // Init
      DateTime endDate = startDate;

      // Error cases
      // Boundary case, nTerms = 1
      if (nTerms <= 1) {
        endDate = startDate;
      }
      else {
        // Get period span in minutes/months
        int periodSpan = GetTermPeriodSpan(period);

        // Calculate total recurring span
        int recurringSpan = (nTerms - 1) * periodSpan;

        // Add termSpan to StartDate
        // In Minutes
        if (period == TermPeriod.OneMinute || period == TermPeriod.FiveMinutes || period == TermPeriod.TenMinutes)
          endDate = startDate.AddMinutes(recurringSpan);
        // In Months
        else
          endDate = startDate.AddMonths(recurringSpan);
      }

      // For Debugging
      Console.WriteLine($"EndDate:: Start:{startDate.ToShortDateString()}; #Terms:{nTerms}; Period:{period} ==> {endDate.ToShortDateString()}");

      return endDate;
    }

    // Used for Debugging
    // Don't Console.WriteLine for nested call
    private static bool _nestedCallToFullList = false;

    /// <summary>
    /// Get a list of all scheduled payments for the given start/end dates.
    /// </summary>
    /// <param name="startDate">Start date of recurring periods</param>
    /// <param name="endDate">End date of recurring periods</param>
    /// <param name="period">Cyclic period of terms</param>
    /// <returns>List of scheduled payment dates. Empty if none.</returns>
    public static List<DateTime> GetAllScheduledPaymentDates(DateTime startDate, DateTime endDate, TermPeriod period) {
      // Init list
      List<DateTime> list = new List<DateTime>();
      // Get period span
      int periodSpan = GetTermPeriodSpan(period);

      // Begin with StartDate
      DateTime scheduledDate = startDate;

      // Loop until EndDate
      while (scheduledDate <= endDate) {
        // Add current scheduledDated
        list.Add(scheduledDate);

        // Calculate next scheduledDate by adding period span to current scheduledDate
        // In Minutes
        if (period == TermPeriod.OneMinute || period == TermPeriod.FiveMinutes || period == TermPeriod.TenMinutes)
          scheduledDate = scheduledDate.AddMinutes(periodSpan);
        // In Months
        else
          scheduledDate = scheduledDate.AddMonths(periodSpan);
      }

      // For Debugging
      if (!_nestedCallToFullList) {
        string s = "";
        foreach (DateTime date in list) {
          if (s.Length > 0) s += ", ";
          s += date.ToShortDateString();
        }
        Console.WriteLine($"FullList:: Start:{startDate.ToShortDateString()}; End:{endDate.ToShortDateString()}; Period:{period} ==> [{s}]\n");
      }

      return list;
    }
    /// <summary>
    /// Given some DateTime, calculate the next schedule payment date
    /// </summary>
    /// <param name="startDate">Start date of recurring periods</param>
    /// <param name="endDate">End date of recurring periods</param>
    /// <param name="period">Cyclic period of terms</param>
    /// <param name="fromDate">Date from which the next scheduled payment date is calculated. It may be DateTime.Now</param>
    /// <returns>Next scheduled payment since fromdate. If none, returns Minimum DateTime</returns>
    public static DateTime GetNextScheduledPaymentDate(DateTime startDate, DateTime endDate, TermPeriod period, DateTime fromDate) {
      // Get the full list of all recurring payments.
      _nestedCallToFullList = true;
      List<DateTime> fullList = GetAllScheduledPaymentDates(startDate, endDate, period);
      _nestedCallToFullList = false;

      // Init to Minimum DateTime
      DateTime nextScheduledDate = DateTime.MinValue;

      // Loop over the whole list
      foreach (DateTime scheduledDate in fullList) {
        // If the list item date > date, then this is the first date since the given date.
        // Then exit the loop
        if (scheduledDate > fromDate) {
          nextScheduledDate = scheduledDate;
          break;
        }
      }

      // For Debugging
      Console.WriteLine($"NextScheduledDate:: Start:{startDate.ToShortDateString()}; End:{endDate.ToShortDateString()}; Period:{period}; From:{fromDate.ToShortDateString()} ==> {nextScheduledDate.ToShortDateString()}");

      return nextScheduledDate;
    }
    /// <summary>
    /// Given some dateTime, calculate the remainingnumber of scheduled payments 
    /// </summary>
    /// <param name="startDate">Start date of recurring periods</param>
    /// <param name="endDate">End date of recurring periods</param>
    /// <param name="period">Cyclic period of terms</param>
    /// <param name="fromDate">Date from which the next scheduled payment date is calculated. It may be DateTime.Now</param>
    /// <returns>Remaining scheduled payments since fromdate. If none, returns 0</returns>
    public static int GetRemainingPaymentTerms(DateTime startDate, DateTime endDate, TermPeriod period, DateTime fromDate) {
      // Get the full list of all recurring payments.
      _nestedCallToFullList = true;
      List<DateTime> fullList = GetAllScheduledPaymentDates(startDate, endDate, period);
      _nestedCallToFullList = false;

      // Init to 0
      int n = 0;

      // Loop over the whole list
      foreach (DateTime scheduledDate in fullList) {
        // Increment if scheduledDate > fromDate
        if (scheduledDate > fromDate)
          n++;
      }

      // For Debugging
      Console.WriteLine($"RemainingTerms:: Start:{startDate.ToShortDateString()}; End:{endDate.ToShortDateString()}; Period:{period}; From:{fromDate.ToShortDateString()} ==> {n}");

      return n;
    }

    // Get the TermPeriod span in months or minutes as needed
    private static int GetTermPeriodSpan(TermPeriod period) {
      int n = 0;

      switch (period) {
        case TermPeriod.Month:
          n = 1;
          break;
        case TermPeriod.Quarter:
          n = 3;
          break;
        case TermPeriod.Year:
          n = 12;
          break;
        case TermPeriod.OneMinute:
          n = 1;
          break;
        case TermPeriod.FiveMinutes:
          n = 5;
          break;
        case TermPeriod.TenMinutes:
          n = 10;
          break;
      }

      return n;
    }

    /*
      public static void Test() {
        // Test EndDate
        Console.WriteLine("Testing EndDate");
        CalculateEndDate(new DateTime(2019, 1, 1), 12, RecurringPaymentHelper.TermPeriod.Month);
        CalculateEndDate(new DateTime(2019, 1, 1), 0, RecurringPaymentHelper.TermPeriod.Month);
        CalculateEndDate(new DateTime(2019, 1, 1), 1, RecurringPaymentHelper.TermPeriod.Month);
        CalculateEndDate(new DateTime(2019, 1, 31), 24, RecurringPaymentHelper.TermPeriod.Month);
        CalculateEndDate(new DateTime(2019, 1, 31), 1, RecurringPaymentHelper.TermPeriod.Month);
        CalculateEndDate(new DateTime(2019, 1, 31), 2, RecurringPaymentHelper.TermPeriod.Month);
        CalculateEndDate(new DateTime(2019, 1, 31), 3, RecurringPaymentHelper.TermPeriod.Month);

        // Test FullList
        Console.WriteLine("");
        Console.WriteLine("Testing FullList");
        GetAllScheduledPaymentDates(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month);
        GetAllScheduledPaymentDates(new DateTime(2019, 1, 1), new DateTime(2020, 12, 1), RecurringPaymentHelper.TermPeriod.Month);
        GetAllScheduledPaymentDates(new DateTime(2019, 1, 1), new DateTime(2019, 1, 1), RecurringPaymentHelper.TermPeriod.Month);
        GetAllScheduledPaymentDates(new DateTime(2019, 1, 1), new DateTime(2018, 12, 1), RecurringPaymentHelper.TermPeriod.Month);

        // Test Next Date
        Console.WriteLine("");
        Console.WriteLine("Testing NextDate");
        GetNextScheduledPaymentDate(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2019, 5, 1));
        GetNextScheduledPaymentDate(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2018, 5, 1));
        GetNextScheduledPaymentDate(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2019, 1, 1));
        GetNextScheduledPaymentDate(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2019, 1, 2));
        GetNextScheduledPaymentDate(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2018, 12, 31));
        GetNextScheduledPaymentDate(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2020, 5, 1));
        GetNextScheduledPaymentDate(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2019, 12, 1));
        GetNextScheduledPaymentDate(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2019, 12, 2));
        GetNextScheduledPaymentDate(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2019, 11, 30));

        // Test Remaining Periods
        Console.WriteLine("");
        Console.WriteLine("Testing Remaining Terms");
        GetRemainingPaymentTerms(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2019, 5, 1));
        GetRemainingPaymentTerms(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2018, 5, 1));
        GetRemainingPaymentTerms(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2019, 1, 1));
        GetRemainingPaymentTerms(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2019, 1, 2));
        GetRemainingPaymentTerms(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2018, 12, 31));
        GetRemainingPaymentTerms(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2020, 5, 1));
        GetRemainingPaymentTerms(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2019, 12, 1));
        GetRemainingPaymentTerms(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2019, 12, 2));
        GetRemainingPaymentTerms(new DateTime(2019, 1, 1), new DateTime(2019, 12, 1), RecurringPaymentHelper.TermPeriod.Month, new DateTime(2019, 11, 30));
      }*/
  }
}
