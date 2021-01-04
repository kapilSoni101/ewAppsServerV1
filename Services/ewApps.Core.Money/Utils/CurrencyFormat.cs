using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ewApps.Core.Money {
  public class CurrencyFormat {
    private string _groupSeperator;
    private string _decimalSeperator;
    private int _decimalPlaces;
    private int[] _groupSize = new int[3];
    private bool _isLeftToRightCurrencySymbolPosition;
    private string _currencySymbol;
    public string GroupSeperator {
      get => _groupSeperator;
      set => _groupSeperator = value;
    }
    public string DecimalSeperator {
      get => _decimalSeperator;
      set => _decimalSeperator = value;
    }
    public int DecimalPlaces {
      get => _decimalPlaces;
      set => _decimalPlaces = value;
    }
    public int[] GroupSize {
      get => _groupSize;
      set => _groupSize = value;
    }
    public bool IsLeftToRightCurrencySymbolPosition {
      get => _isLeftToRightCurrencySymbolPosition;
      set => _isLeftToRightCurrencySymbolPosition = value;
    }

    public string CurrencySymbol {
      get => _currencySymbol;
      set => _currencySymbol  = value;
    }
  }
}
