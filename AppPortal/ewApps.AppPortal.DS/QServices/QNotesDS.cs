using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;

namespace ewApps.AppPortal.DS {
   public class QNotesDS : IQNotesDS{

        IQNotesRepository _qNotesRepository;


        public QNotesDS(IQNotesRepository qNotesRepository) {
            _qNotesRepository = qNotesRepository;
        }

        public async Task<List<NotesViewDTO>> GetNotesViewListByEntityId(Guid entityId, Guid tenantId) {
            return await _qNotesRepository.GetNotesViewListByEntityId(entityId, tenantId);
        }

        public async Task<BusinessNotesNotificationDTO> GetBusinessUserListByEntityId(Guid entityId, Guid tenantId,  Guid appId, Guid userId, int userType) {
            return await _qNotesRepository.GetBusinessUserListByEntityId(entityId, tenantId,appId,userId,userType);
        }

        public async Task<BusinessNotesNotificationDTO> GetBusinessInfoWithQuotationByEntityId(Guid entityId, Guid tenantId, Guid appId, Guid userId, int userType) {
            return await _qNotesRepository.GetBusinessInfoWithQuotationByEntityId(entityId, tenantId, appId, userId, userType);
        }

        public async Task<BusinessNotesNotificationDTO> GetBusinessInfoWithSalesOrderByEntityId(Guid entityId, Guid tenantId, Guid appId, Guid userId, int userType) {
            return await _qNotesRepository.GetBusinessInfoWithSalesOrderByEntityId(entityId, tenantId, appId, userId, userType);
        }

        public async Task<BusinessNotesNotificationDTO> GetBusinessInfoWithDeliveryByEntityId(Guid entityId, Guid tenantId, Guid appId, Guid userId, int userType) {
            return await _qNotesRepository.GetBusinessInfoWithDeliveryByEntityId(entityId, tenantId, appId, userId, userType);
        }

        public async Task<BusinessNotesNotificationDTO> GetBusinessInfoWithDraftDeliveryByEntityId(Guid entityId, Guid tenantId, Guid appId, Guid userId, int userType) {
            return await _qNotesRepository.GetBusinessInfoWithDraftDeliveryByEntityId(entityId, tenantId, appId, userId, userType);
        }

        public async Task<BusinessNotesNotificationDTO> GetBusinessInfoWithContractByEntityId(Guid entityId, Guid tenantId, Guid appId, Guid userId, int userType) {
            return await _qNotesRepository.GetBusinessInfoWithContractByEntityId(entityId, tenantId, appId, userId, userType);
        }

    }
}
