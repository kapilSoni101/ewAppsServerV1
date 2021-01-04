using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {
    public interface INotesDS:IBaseDS<Notes> {
        Task<Notes> AddNotesAsync(NotesAddDTO notesAddDTO);

        /// <summary>
        /// Add notes.
        /// </summary>
        /// <param name="notesList"></param>
        /// <param name="entityId"></param>
        /// <param name="entityType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddNotesListAsync(List<NotesAddDTO> notesList, Guid entityId, int entityType, CancellationToken token = default(CancellationToken));

        Task<List<NotesViewDTO>> GetNotesViewListByEntityId(Guid entityId, Guid tenantId);
    }
}
