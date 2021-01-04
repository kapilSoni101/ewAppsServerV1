using ewApps.Core.UserSessionService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO
{
  public class BusinessSupportNotificationDTO
  {


    /*
     < xsl:param name = "publisherCompanyName" />

       < xsl:param name = "businessCompanyName" />

        < xsl:param name = "customerCompanyName" />

         < xsl:param name = "customerCompanyID" />

          < xsl:param name = "userName" />

           < xsl:param name = "ticketID" />

            < xsl:param name = "title" />

             < xsl:param name = "description" />

              < xsl:param name = "oldPriority" />

               < xsl:param name = "newPriority" />

                < xsl:param name = "oldStatus" />

                 < xsl:param name = "newStatus" />

                  < xsl:param name = "dateTime" />

                   < xsl:param name = "count" />

                    < xsl:param name = "commentsText" />

                     < xsl:param name = "modifiedOn" />

                      < xsl:param name = "updatedBy" />

                       < xsl:param name = "assignedTo" />

                        < xsl:param name = "subDomain" />

                         < xsl:param name = "portalURL" />

                          < xsl:param name = "applicationName" />

                           < xsl:param name = "copyrightText" />*/


    public Guid TicketId
    {
      get; set;
    }

    public string IdentityNumber
    {
      get; set;
    }
    
    //  
    public string PublisherName
    {
      get; set;
    }
    //  
    public string BusinessName
    {
      get; set;
    }

    public string AppName
    {
      get; set;
    }

    public string ModifiedBy
    {
      get; set;
    }

    public DateTime? ModifiedOn
    {
      get; set;
    }


    // Customer Detail
    public string CustomerName
    {
      get; set;
    }
    // 

    public string UserName
    {
      get; set;
    }


    public string ContactEmail
    {
      get; set;
    }

    //Ticket detail

    public string Title
    {
      get; set;
    }

    public string Description
    {
      get; set;
    }


    public Guid AppId
    {
      get; set;
    }

    public Guid CreatedBy
    {
      get; set;
    }

    public DateTime? CreatedOn
    {
      get; set;
    }   

    public string Subdomain
    {
      get; set;
    }
   
    public Guid BusinessTenantId
    {
      get; set;
    }
    public short Status
    {
      get; set;
    }
    public short Priority
    {
      get; set;
    }
    public string Copyright
    {
      get; set;
    }
    public string UserIdentityNumber {
      get; set;
    }
    public string CustIdentityNumber {
      get; set;
    }
    public string DateTimeFormat {
      get; set;
    }
    public string TimeZone {
      get; set;
    }

    [NotMapped]
    public bool IsPublisherAssinged
    {
      get; set;
    }

    [NotMapped]
    public string PortalUrl
    {
      get; set;
    }

    [NotMapped]
    public UserSession UserSessionInfo
    {
      get; set;
    }

    // 
    [NotMapped]
    public string NewAssignee
    {
      get; set;
    }

    [NotMapped]
    public string OldAssignee {
      get; set;
    }
    //
    [NotMapped]
    public string OldStatus
    {
      get; set;
    }
    //
    [NotMapped]
    public string NewStatus
    {
      get; set;
    }
    // 
    [NotMapped]
    public string OldPriority
    {
      get; set;
    }
    //
    [NotMapped]
    public string NewPriority
    {
      get; set;
    }
    [NotMapped]
    public string CommentText
    {
      get; set;
    }
    [NotMapped]
    public string Count
    {
      get; set;

    }
    //  
    [NotMapped]
    public string CustomerCompanyName
    {
      get; set;
    }
  }
}
