using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.BusinessEntity.Entity
{
  [Table("BAAPInvoiceAttachment", Schema = "be")]
  public class BAAPInvoiceAttachment : BaseEntity
  {

    /// <summary>
    /// 
    /// </summary>
    [Required]
    public Guid ID
    {
      get; set;
    }

    [MaxLength(100)]
    public string ERPConnectorKey
    {
      get; set;
    }

    [MaxLength(100)]
    public string ERPAPInvoiceAttachmentKey
    {
      get; set;
    }

    public Guid APInvoiceId
    {
      get; set;
    }

    [MaxLength(100)]
    public string ERPAPInvoiceKey
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(100)]
    public string Name
    {
      get; set;
    }

    /// <summary>
    /// 
    ///</summary>
    [MaxLength(100)]
    public string FreeText
    {
      get; set;
    }

    /// <summary>
    /// 
    /// </summary>
    public DateTime? AttachmentDate
    {
      get; set;
    }
  }
}