using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.BusinessEntity.DTO
{
  public class BusVendorSetUpAppDTO { 
  /// <summary>
  /// ewapps customer ID 
  /// </summary>
  public new Guid ID
  {
    get; set;
  }

  /// <summary> CustomerId
  /// ewapps Partner ID corresponding to this customer
  /// </summary>
  public string ERPVendorKey
  {
    get; set;
  }
  /// <summary>
  /// ewapps Partner ID corresponding to this customer
  /// </summary>
  public string VendorName
  {
    get; set;
  }

  /// <summary>
  /// Tenant Id 
  /// </summary>
  public Guid BusinessPartnerTenantId
  {
    get; set;
  }

  /// <summary>
  /// Tenant Id 
  /// </summary>
  public int ApplicationCount
  {
    get; set;
  }

  public DateTime CreatedOn
  {
    get; set;
  }
  
}
}
