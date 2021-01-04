/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 3 December 2018
 */



using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.DBQuery;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.Data
{

  /// <summary>  
  /// This class contains a session of core database and can be used to query and 
  /// save instances of core entities. It is a combination of the Unit Of Work and Repository patterns.  
  /// </summary>
  public partial class AppPortalDbContext
  {

    #region Support
    /// <summary>
    /// This is use to get SupportMyTicketDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
    /// is translated into database query.
    /// </summary>
    /// <remarks>
    /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
    /// to properties of SupportMyTicketDTO.
    /// </remarks>
    public DbQuery<SupportMyTicketDTO> SupportMyTicketDTOQuery
    {
      get; set;
    }

    /// <summary>
    /// This is use to get SupportTicketDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
    /// is translated into database query.
    /// </summary>
    /// <remarks>
    /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
    /// to properties of SupportTicketDTO.
    /// </remarks>
    public DbQuery<SupportTicketDTO> SupportTicketDTOQuery
    {
      get; set;
    }


    /// <summary>
    /// This is use to get SupportTicketDetailDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
    /// is translated into database query.
    /// </summary>
    /// <remarks>
    /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
    /// to properties of SupportTicketDetailDTO.
    /// </remarks>
    public DbQuery<SupportTicketDetailDTO> SupportTicketDetailDTOQuery
    {
      get; set;
    }


    /// <summary>
    /// This is use to get SupportCommentDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
    /// is translated into database query.
    /// </summary>
    /// <remarks>
    /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
    /// to properties of SupportCommentDTO.
    /// </remarks>
    public DbQuery<SupportCommentDTO> SupportCommentDTOQuery
    {
      get; set;
    }


    #endregion

    /// <summary>
    /// This is use to get AppDetailDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
    /// is translated into database query.
    /// </summary>
    /// <remarks>
    /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
    /// to properties of AppDetailDTO.
    /// </remarks>
    public DbQuery<AppDetailDTO> AppDetailDTOQuery
    {
      get; set;
    }




    public DbQuery<ConnectorDQ> platformConnectorDQQuery
    {
      get; set;
    }

    public DbQuery<PublisherAddressDTO> PublisherAddressModelDQQuery
    {
      get; set;
    }

    public DbQuery<BusinessAddressModelDTO> BusinessAddressModelDQQuery
    {
      get; set;
    }

    public DbQuery<StringDTO> StringDTOQuery
    {
      get; set;
    }

    public DbQuery<CustomerAccountDTO> CustomerAccountDTOQuery
    {
      get; set;
    }

    public DbQuery<CustCreditCardRequestDTO> CustCreditCardTokenDTOQuery
    {
      get; set;
    }
    public DbQuery<CustResponseDTO> CustResponseDTOQuery
    {
      get; set;
    }
    public DbQuery<CustGetOnusTokenResponseDTO> CustGetOnusTokenResponseDTOQuery
    {
      get; set;
    }

    /// <summary>
    /// Get Business Sync Time Log
    /// </summary>
    public DbQuery<BusBASyncTimeLogDTO> BusBASyncTimeLogDTOQuery
    {
      get; set;
    }
    /// <summary>
    /// Add Favorite item
    /// </summary>
    public DbQuery<FavoriteAddDTO> FavoriteAddDTOQuery
    {
      get; set;
    }

    /// <summary>
    /// Favorite menu view model
    /// </summary>
    public DbQuery<FavoriteViewDTO> FavoriteViewDTOQuery
    {
      get; set;
    }

    /// <summary>
    /// Check Favorite menu is System default or not
    /// </summary>
    public DbQuery<FavoriteDTO> FavoriteDTOQuery
    {
      get; set;
    }

    /// <summary>
    /// View Setting DTo 
    /// </summary>
    public DbQuery<ViewSettingDTO> ViewSettingDTOQuery
    {
      get; set;
    }

    /// <summary>
    /// AS Notification view model
    /// </summary>
    public DbQuery<ASNotificationDTO> ASNotificationDTOQuery
    {
      get; set;
    }

        /// <summary>
        /// AS preferences view model
        /// </summary>
        public DbQuery<PreferenceViewDTO> PreferenceViewDTOQuery {
            get; set;
        }

    

  public DbQuery<BusinessSupportNotificationDTO> BusinessSupportNotificationDTOQuery
    {
      get; set;
    }

  }
}