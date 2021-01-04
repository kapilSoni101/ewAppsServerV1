using System;
using System.Collections.Generic;
using System.Text;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.Data {
    public class NotesRespository:BaseRepository<Notes, AppPortalDbContext>, INotesRespository {

        public NotesRespository(AppPortalDbContext context, IUserSessionManager userSession) : base(context, userSession) {

        }
    }
}
