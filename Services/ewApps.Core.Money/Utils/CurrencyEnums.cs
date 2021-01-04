using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.Money {
  /// <summary>
  /// It defines the Currency seperator for group and decimal
  /// </summary>
  public enum CurrencyFormatSeparators {
    CommaGSDotDS = '1',
    DotGSCommaDS = '2'
  }

  /// <summary>
  /// It defines the digit grouping
  /// </summary>
  public enum CurrencyFormatGroupDigits {
    TwoThree = 1,
    ThreeThree = 2,
    ThreeFour = 3,
    FourFour = 4
  }

  /// <summary>
  /// It defines the digit grouping
  /// </summary>
  public enum CurrencySymbolPosition {
    FrontWithoutSpace = 0,
    BackWithoutSpace= 1,
    FrontWithSpace = 2,
    BackWithSpace = 3
  }

}

