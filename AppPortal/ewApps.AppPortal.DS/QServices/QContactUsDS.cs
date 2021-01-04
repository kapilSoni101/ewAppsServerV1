using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;

namespace ewApps.AppPortal.DS {
    public class QContactUsDS : IQContactUsDS {

        IQContactUsRepository _qContactUsRepository;

        public QContactUsDS(IQContactUsRepository qContactUsRepository) {
            _qContactUsRepository = qContactUsRepository;
        }

        public async Task<UserEmailDTO> GetPubEmailRecipent(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _qContactUsRepository.GetPubEmailRecipent(tenantId, token);
        }

        public async Task<UserEmailDTO> GetPlatEmailRecipent(CancellationToken token = default(CancellationToken)) {
            return await _qContactUsRepository.GetPlatEmailRecipent(token);
        }
    }
}
