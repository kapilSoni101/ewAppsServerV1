using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace ewApps.Core.Money {
  public class CurrencyFormatTest {
    #region test
    public static void Main() {
      Console.WriteLine("*******************************");
      TestFormatter();
      TestSingleLoop();
      TestHundredThousandLoop();
      Console.WriteLine();
    }
    private static void TestHundredThousandLoop() {
      decimal d = 34956784.2334M;
      Stopwatch sWatch = Stopwatch.StartNew();
      sWatch.Restart();
      string s = "", s1 = "";

      for(int i = 0; i < 10000; i++) {
        AppCurrencyFormat format = CurrencyFormatter.GetCurrencyFormat("#", CurrencySymbolPosition.BackWithSpace, 3, CurrencyFormatGroupDigits.ThreeFour, CurrencyFormatSeparators.DotGSCommaDS);
        CultureInfo info = CurrencyFormatter.GetCultureInfo(format,null);
        s = CurrencyFormatter.CurrencyToString(d, info, true, true);
      }
      sWatch.Stop();
      Debug.WriteLine("Test2: 100000 By Creating Culture Info :" + s + " : Time:" + sWatch.ElapsedMilliseconds);
      Console.WriteLine("Test2: 100000 By Creating Culture Info :" + s + " : Time:" + sWatch.ElapsedMilliseconds);

      sWatch.Restart();
      for(int i = 0; i < 10000; i++) {
        s1 =CurrencyFormatter.CurrencyToString(d, CultureInfo.CurrentCulture, true, true);
      }
      sWatch.Stop();
      Debug.WriteLine("Test2: 100000 By Using Currenct Culture :" + s1 + " : Time:" + sWatch.ElapsedMilliseconds);
      Console.WriteLine("Test2: 100000 By Using Currenct Culture :" + s1 + " : Time:" + sWatch.ElapsedMilliseconds);

      sWatch.Restart();
      string s2 = "";
      for(int i = 0; i < 10000; i++) {
        s2 = d.ToString("0.00", CultureInfo.InvariantCulture);
      }
      sWatch.Stop();
      Debug.WriteLine("Test2: 100000 By using string format:" + s2 + " : Time:" + sWatch.ElapsedMilliseconds);
      Console.WriteLine("Test2: 100000 By Using string format :" + s2 + " : Time:" + sWatch.ElapsedMilliseconds);
    }
    private static void TestSingleLoop() {
      decimal d = 34956784.2334M;
      Stopwatch sWatch = Stopwatch.StartNew();

      AppCurrencyFormat format = CurrencyFormatter.GetCurrencyFormat("#", CurrencySymbolPosition.BackWithSpace, 3, CurrencyFormatGroupDigits.ThreeFour, CurrencyFormatSeparators.DotGSCommaDS);
      CultureInfo info = CurrencyFormatter.GetCultureInfo(format,null);
      string s = CurrencyFormatter.CurrencyToString(d, info, true, true);
      sWatch.Stop();
      Debug.WriteLine("Test1: By Creating Culture Info :" + s + " : Time:" + sWatch.ElapsedMilliseconds);
      Console.WriteLine("Test1: By Creating Culture Info :" + s + " : Time:" + sWatch.ElapsedMilliseconds);
      sWatch.Restart();
      string s1 = CurrencyFormatter.CurrencyToString(d, CultureInfo.CurrentCulture, true, true);
      sWatch.Stop();
      Debug.WriteLine("Test1: By Using Currenct Culture :" + s1 + " : Time:" + sWatch.ElapsedMilliseconds);
      Console.WriteLine("Test1: By Using Currenct Culture :" + s1 + " : Time:" + sWatch.ElapsedMilliseconds);
      sWatch.Restart();
      string s2 = d.ToString("0.00", CultureInfo.InvariantCulture);
      sWatch.Stop();
      Debug.WriteLine("Test1: By Using string format:" + s2 + " : Time:" + sWatch.ElapsedMilliseconds);
      Console.WriteLine("Test1: By Using string format :" + s2 + " : Time:" + sWatch.ElapsedMilliseconds);
    }

    private static void TestFormatter() {
      decimal d = 34956784.2334M;
      AppCurrencyFormat format = CurrencyFormatter.GetCurrencyFormat("$", CurrencySymbolPosition.FrontWithoutSpace, 3, CurrencyFormatGroupDigits.ThreeFour, CurrencyFormatSeparators.DotGSCommaDS);
      CultureInfo info = CurrencyFormatter.GetCultureInfo(format, null);
      string s = CurrencyFormatter.CurrencyToString(d, info, true, true);
      Debug.WriteLine("Positive Number with 3-4 seperator and 3 decimal :" + s );

       format = CurrencyFormatter.GetCurrencyFormat("#", CurrencySymbolPosition.BackWithSpace, 3, CurrencyFormatGroupDigits.ThreeFour, CurrencyFormatSeparators.DotGSCommaDS);
       info = CurrencyFormatter.GetCultureInfo(format, null);
       s = CurrencyFormatter.CurrencyToString(d, info, true, true);
      Debug.WriteLine(" Positive Number with Currency symbol after the number :" + s);

      d = -34956784.2334M;
      format = CurrencyFormatter.GetCurrencyFormat("#", CurrencySymbolPosition.FrontWithoutSpace, 3, CurrencyFormatGroupDigits.ThreeFour, CurrencyFormatSeparators.DotGSCommaDS);
      info = CurrencyFormatter.GetCultureInfo(format, null);
      s = CurrencyFormatter.CurrencyToString(d, info, true, true);
      Debug.WriteLine("Negative Number with Currency symbol after the number :" + s);

      d = 34956784.0000M;
      format = CurrencyFormatter.GetCurrencyFormat("#", CurrencySymbolPosition.FrontWithoutSpace, 3, CurrencyFormatGroupDigits.ThreeFour, CurrencyFormatSeparators.CommaGSDotDS);
      info = CurrencyFormatter.GetCultureInfo(format, null);
      s = CurrencyFormatter.CurrencyToString(d, info, true, true);
      Debug.WriteLine("No decimal value for 0 decimals in number, Comma Seperator" + s);
     
      s = CurrencyFormatter.CurrencyToString(d, info, true, false);
      Debug.WriteLine("Decimal value event if 0 decimals" + s);
    }

/**************************************************
    Positive Number with 3-4 seperator and 3 decimal :$3.4956.784,233
     Positive Number with Currency symbol after the number :3.4956.784,233 #
    Negative Number with Currency symbol after the number :(#3.4956.784,233)
    No decimal value for 0 decimals in number, Comma Seperator#3,4956,784
    Decimal value event if 0 decimals#3,4956,784.000

///Performance -------------------
    Test1: By Creating Culture Info :3.4956.784,233 # : Time:0
    Test1: By Using Currenct Culture :$34,956,784.23 : Time:0
    Test1: By Using string format:34956784.23 : Time:0
    Test2: 100000 By Creating Culture Info :3.4956.784,233 # : Time:113
    Test2: 100000 By UsiNG Currenct Culture :$34,956,784.23 : Time:19
    Test2: 100000 By using string format:34956784.23 : Time:8
         * 
        */
    #endregion
  }
}
