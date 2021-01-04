/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 26 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 26 August 2019
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public class SupportTicketAssigneeHelper:ISupportTicketAssigneeHelper {

   
        IQPublisherAndUserDS _qPublisherAndUserDS;   

        public SupportTicketAssigneeHelper( IQPublisherAndUserDS qPublisherAndUserDS) {         
           
            _qPublisherAndUserDS = qPublisherAndUserDS;       

        }

       

        public async Task <List<KeyValuePair<short, string>>> FillAssigneeList(SupportTicketDetailDTO supportTicket, SupportLevelEnum requesterLevel) {
            List<KeyValuePair<short, string>> assigneeList = new List<KeyValuePair<short, string>>();

            // Customer Ticket
            if(supportTicket.GenerationLevel == (short)SupportLevelEnum.Level1) {

                string publisherName = null;
                StringDTO strDTO = new StringDTO();
                strDTO = await _qPublisherAndUserDS.GetPublisherNameByPartnerTenantId(supportTicket.CustomerId);   
                if (strDTO != null)
                 publisherName = strDTO.Name;
                // App Customer
                if(supportTicket.CurrentLevel == (short)SupportLevelEnum.Level1) {
                    if(supportTicket.CurrentLevel == (short)requesterLevel) { // i.e. Level1
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, supportTicket.TenantName));
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level1, supportTicket.CreatedByName));
                    }
                    else {
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level1, supportTicket.CreatedByName));
                    }
                }
                // App Business
                else if(supportTicket.CurrentLevel == (short)SupportLevelEnum.Level2) {
                    if(supportTicket.CurrentLevel == (short)requesterLevel) {
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level1, supportTicket.CreatedByName));
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, supportTicket.TenantName));
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, publisherName));
                    }
                    else {
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, supportTicket.TenantName));
                    }
                }
                // App Publisher
                else if(supportTicket.CurrentLevel == (short)SupportLevelEnum.Level3) {
                    if(supportTicket.CurrentLevel == (short)requesterLevel) {
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, supportTicket.TenantName));
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, publisherName));
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level4, AppPortalConstants.SuperAdminKey));
                    }
                    else {
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, publisherName));
                    }
                }

                else if(supportTicket.CurrentLevel == (short)SupportLevelEnum.Level4) {
                    if(supportTicket.CurrentLevel == (short)requesterLevel) {
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, publisherName));
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level4, AppPortalConstants.SuperAdminKey));
                    }
                    else {
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level4, AppPortalConstants.SuperAdminKey));
                    }
                }
            }

            // App Business Ticket
            else if(supportTicket.GenerationLevel == (short)SupportLevelEnum.Level2) {
                string publisherName = null;
                StringDTO strDTO = new StringDTO();
                strDTO = await _qPublisherAndUserDS.GetPublisherNameByBusinessTenantId(supportTicket.TenantId);                
                if (strDTO != null)
                publisherName =  strDTO.Name;
                if(supportTicket.CurrentLevel == (short)SupportLevelEnum.Level2) {
                    // App Business
                    if(supportTicket.CurrentLevel == (short)requesterLevel) {
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, supportTicket.CreatedByName));
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, publisherName));
                    }
                    else {
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, supportTicket.CreatedByName));
                    }
                }
                else if(supportTicket.CurrentLevel == (short)SupportLevelEnum.Level3) {
                    if(supportTicket.CurrentLevel == (short)requesterLevel) {
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level2, supportTicket.CreatedByName));
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, publisherName));
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level4, AppPortalConstants.SuperAdminKey));
                    }
                    else {
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, publisherName));
                    }

                }
                else if(supportTicket.CurrentLevel == (short)SupportLevelEnum.Level4) {
                    if(supportTicket.CurrentLevel == (short)requesterLevel) {
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, publisherName));
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level4, AppPortalConstants.SuperAdminKey));
                    }
                    else {
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level4, AppPortalConstants.SuperAdminKey));
                    }
                }
            }

            else if(supportTicket.GenerationLevel == (short)SupportLevelEnum.Level3) {
                if(supportTicket.CurrentLevel == (short)SupportLevelEnum.Level3) {
                    if(supportTicket.CurrentLevel == (short)requesterLevel) {
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, supportTicket.CreatedByName));
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level4, AppPortalConstants.SuperAdminKey));
                    }
                    else {
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, supportTicket.CreatedByName));
                    }
                }
                else if(supportTicket.CurrentLevel == (short)SupportLevelEnum.Level4) {
                    if(supportTicket.CurrentLevel == (short)requesterLevel) {
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level3, supportTicket.CreatedByName));
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level4, AppPortalConstants.SuperAdminKey));
                    }
                    else {
                        assigneeList.Add(new KeyValuePair<short, string>((short)SupportLevelEnum.Level4, AppPortalConstants.SuperAdminKey));
                    }
                }
            }

            return assigneeList;
        }

        public async Task <string> GetAssigneeName(Guid partnerTenantId, Guid creatorTenantId, string createdByName, short currentLevel, short generationLevel) {
            StringDTO strDTO = new StringDTO();

            string assigneeName = "";
            SupportLevelEnum currentSupportLevel = (SupportLevelEnum)Enum.Parse(typeof(SupportLevelEnum), currentLevel.ToString());
            SupportLevelEnum generationSupportLevel = (SupportLevelEnum)Enum.Parse(typeof(SupportLevelEnum), generationLevel.ToString());
            // Customer Ticket
            if(generationSupportLevel == SupportLevelEnum.Level1) {
                // App Customer
                if(generationSupportLevel == currentSupportLevel) {
                    assigneeName = createdByName;
                }
                else if(currentSupportLevel == SupportLevelEnum.Level2) {
                   // assigneeName = "";// _tenantDataService.Get(creatorTenantId).Name;
                    strDTO = await _qPublisherAndUserDS.GetPublisherNameByTenantId(creatorTenantId);
                    if (strDTO != null)
                    assigneeName = strDTO.Name;
                }
                else if(currentSupportLevel == SupportLevelEnum.Level3) {
                    //assigneeName = _plaformAndPublisherDS.GetPublisherDetailByPartnerTenantId(partnerTenantId).Name;                    
                    strDTO = await _qPublisherAndUserDS.GetPublisherNameByPartnerTenantId(partnerTenantId);
                    if(strDTO != null)
                        assigneeName = strDTO.Name;
                }
                else if(currentSupportLevel == SupportLevelEnum.Level4) {
                    assigneeName = AppPortalConstants.SuperAdminKey;                   
                }
            }

            // Business Ticket
            else if(generationSupportLevel == SupportLevelEnum.Level2) {
                if(generationSupportLevel == currentSupportLevel) {
                    assigneeName = createdByName;
                }
                else if(currentSupportLevel == SupportLevelEnum.Level3) {
                     strDTO = await _qPublisherAndUserDS.GetPublisherNameByBusinessTenantId(creatorTenantId);
                    if(strDTO != null)
                        assigneeName = strDTO.Name;
                }
                else if(currentSupportLevel == SupportLevelEnum.Level4) {
                    assigneeName = AppPortalConstants.SuperAdminKey;                   
                }
            }

            // Publisher Ticket
            else if(generationSupportLevel == SupportLevelEnum.Level3) {
                if(generationSupportLevel == currentSupportLevel) {
                    assigneeName = createdByName;
                }
                else if(currentSupportLevel == SupportLevelEnum.Level4) {
                    assigneeName = AppPortalConstants.SuperAdminKey;                    
                }
            }
            return assigneeName;
        }

       
    }



}
