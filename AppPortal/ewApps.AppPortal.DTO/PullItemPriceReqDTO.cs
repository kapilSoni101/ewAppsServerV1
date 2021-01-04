using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO
{
  public class PullItemPriceReqDTO
  {
    public string ItemCode { get; set; }
    public string CardCode { get; set; }
    public int Quantity { get; set; }
    public string Date { get; set; }
  }
}
