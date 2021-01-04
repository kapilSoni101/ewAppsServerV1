using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
   public interface IQNotesDS {

        Task<List<NotesViewDTO>> GetNotesViewListByEntityId(Guid entityId, Guid tenantId);

        Task<BusinessNotesNotificationDTO> GetBusinessUserListByEntityId(Guid entityId, Guid tenantId, Guid appId, Guid userId, int userType);

        /// <summary>
        /// Get Business Information with Quotation 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        Task<BusinessNotesNotificationDTO> GetBusinessInfoWithQuotationByEntityId(Guid entityId, Guid tenantId, Guid appId, Guid userId, int userType);

        /// <summary>
        /// Get Business Information with Contract 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        Task<BusinessNotesNotificationDTO> GetBusinessInfoWithContractByEntityId(Guid entityId, Guid tenantId, Guid appId, Guid userId, int userType);

        /// <summary>
        /// Get Business Information With Draft Delivery
        /// 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        Task<BusinessNotesNotificationDTO> GetBusinessInfoWithDraftDeliveryByEntityId(Guid entityId, Guid tenantId, Guid appId, Guid userId, int userType);

        /// <summary>
        /// Get Business Information With Delivery
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        Task<BusinessNotesNotificationDTO> GetBusinessInfoWithDeliveryByEntityId(Guid entityId, Guid tenantId, Guid appId, Guid userId, int userType);

        /// <summary>
        /// Get Business Information With Sales Order 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        Task<BusinessNotesNotificationDTO> GetBusinessInfoWithSalesOrderByEntityId(Guid entityId, Guid tenantId, Guid appId, Guid userId, int userType);
    }
}
