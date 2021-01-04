using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public class PublisherExtDS:IPublisherExtDS {
        IQPublisherAndUserDS _qPublisherDS;
        IPublisherAddressDS _publisherAddressDS;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherExtDS"/> class.
        /// </summary>
        /// <param name="qPublisherDS">The q publisher ds.</param>
        /// <param name="publisherAddressDS">The publisher address ds.</param>
        public PublisherExtDS(IQPublisherAndUserDS qPublisherDS, IPublisherAddressDS publisherAddressDS) {
            _qPublisherDS = qPublisherDS;
            _publisherAddressDS = publisherAddressDS;
        }

        /// <inheritdoc/>
        public async Task<PublisherViewDTO> GetPublisherDetailByPublisherIdAsync(Guid publisherId, CancellationToken cancellationToken = default(CancellationToken)) {
            PublisherViewDTO publisherViewDTO = await _qPublisherDS.GetPublisherDetailByPublisherIdAsync(publisherId, cancellationToken);
            // Get Address List;
            publisherViewDTO.AddressList = await _publisherAddressDS.GetAddressListByPublisherIdAndAddressTypeAsync(publisherId, (int)PublisherAddressTypeEnum.DefaultPublisherAddress, cancellationToken);

            // Get Application List
            List<PubAppSettingDTO> appList = await _qPublisherDS.GetPubAppSettingListByPublisherTenantIdAsync(publisherViewDTO.TenantId, cancellationToken);

            List<Guid> appIdList = appList.Select(i => i.AppID).ToList<Guid>();

            // Get Subscription plan list
            List<SubscriptionPlanInfoDTO> subscriptionPlanInfoDTOs = await _qPublisherDS.GetPublishersBusinessSubscriptionPlanListByAppIdsAndPublisherTenantIdAsync(appIdList, publisherViewDTO.TenantId, cancellationToken);

            // Update app with subscription plan list.
            foreach(PubAppSettingDTO app in appList) {
                app.AppSubscriptionList = subscriptionPlanInfoDTOs.FindAll(i => i.AppId == app.AppID);
            }
            publisherViewDTO.ApplicationList = appList;

            return publisherViewDTO;
        }

    }
}
