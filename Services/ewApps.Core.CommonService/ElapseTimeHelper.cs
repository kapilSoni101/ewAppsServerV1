using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace ewApps.Core.Common {
  /// <summary>
  /// Measures elapsed time from Start to End
  /// Includes method to log output in Debug mode
  /// Can chain calls like: timer.End().Log() etc
  /// </summary>
  public class ElapsedTimer {
    // Different times
    private DateTime _startTime;
    private DateTime _stopTime;
    private TimeSpan _elapsedTime;

    public ElapsedTimer() {
    }

    // Start the timer
    public ElapsedTimer Start() {
      // Start the timer
      _startTime = DateTime.Now;
      return this;
    }

    // End the timer
    public ElapsedTimer Stop() {
      // End timer
      _stopTime = DateTime.Now;
      // Elapsed time
      _elapsedTime = _stopTime - _startTime;
      return this;
    }

    public DateTime GetStartTime() {
      return _startTime;
    }

    public DateTime GetStopTime() {
      return _stopTime;
    }

    public TimeSpan GetElapsedTime() {
      return _elapsedTime;
    }

    // Get the elapsed time as string with Start/End Times
    public string GetElapsedTimeAsString(string header = null) {
      string s = $"ElapsedTime:{_elapsedTime}; Started:{_startTime}; Ended:{_stopTime}";
      if(header != null)
        s = $"{header}: {s}";
      return s;
    }

    /*
     * Asha
    // Log the elapsedTime in Debug mode
    public ElapsedTimer LogElapsedTime(string header, ILogger logger)
    {
      string s = GetElapsedTimeString(header);
      logger.Debug(s);
      return this;
    }
    */
  }
}
