using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO
{
  public class BusVendorUserDTO
  {
    /// <summary>
    /// User identifier.
    /// </summary>
    public Guid TenantUserId
    {
      get; set;
    }

    /// <summary>
    /// First name of the user.
    /// </summary>
    public string FirstName
    {
      get; set;
    }

    /// <summary>
    /// Last name of the user.
    /// </summary>
    public string LastName
    {
      get; set;
    }

    /// <summary>
    /// Email id of the user which also his/her userid.
    /// </summary>
    public string Email
    {
      get; set;
    }

    /// <summary>
    /// Count of the application given to the user.
    /// </summary>
    public int ApplicationAccess
    {
      get; set;
    }

    /// <summary>
    /// Primary flag of the user which is primary user of the customer. 
    /// </summary>
    public bool IsPrimary
    {
      get; set;
    }

    /// <summary>
    /// Joined Date of customeruser
    /// </summary>
    // [NotMapped]
    public DateTime? JoinedDate
    {
      get; set;
    }

    /// <summary>
    /// List of applciation assigned to the user.
    /// </summary>
    [NotMapped]
    public List<AppInfoDTO> AssignedAppInfo
    {
      get; set;
    }

    [NotMapped]
    public int OperationType
    {
      get; set;
    }
  }
}

