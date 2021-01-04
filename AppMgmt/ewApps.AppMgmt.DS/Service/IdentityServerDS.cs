using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ewApps.AppMgmt.Common;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.ExceptionService;
using ewApps.Core.ServiceProcessor;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.AppMgmt.DS {
    public class IdentityServerDS:IIdentityServerDS {

        AppMgmtAppSettings _appMgmtAppSettings;
        ILogger<IdentityServerDS> _loggerService;


        public IdentityServerDS(IOptions<AppMgmtAppSettings> appMgmtAppSettings, ILogger<IdentityServerDS> loggerService) {
            _appMgmtAppSettings = appMgmtAppSettings.Value;
            _loggerService = loggerService;
        }

        // This method calles identity server and add user with application on identity server and provide with identyuserId.
        public async Task<IdentityServerAddUserResponseDTO> AddUserOnIdentityServerAsync(IdentityUserDTO identityUserDTO) {

            // Initialize local variables.
            IdentityServerAddUserResponseDTO addUserResponseDTO = new IdentityServerAddUserResponseDTO();

            // Create Service calling parameters.
            string baseurl = _appMgmtAppSettings.IdentityServerUrl;
            string requesturl = "user/add";
            //RequestOptions requestOptions = new RequestOptions();
            //requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            //requestOptions.Method = requesturl;
            //requestOptions.MethodData = identityUserDTO;
            //requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            //requestOptions.ServiceRequestType = RequestTypeEnum.Post;
            //requestOptions.BearerTokenInfo = new BearerTokenOption {
            //    AppClientName = _appMgmtAppSettings.AppName,
            //    AuthServiceUrl = _appMgmtAppSettings.IdentityServerUrl
            //};

            // Call the service on identity server to add user.
            //ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseurl);
            //addUserResponseDTO = await httpRequestProcessor.ExecuteAsync<IdentityServerAddUserResponseDTO>(requestOptions, false);

            // Calling the identity service API for deleting the user.
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, requesturl, identityUserDTO, _appMgmtAppSettings.AppName, _appMgmtAppSettings.IdentityServerUrl, null);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseurl);
            addUserResponseDTO = await httpRequestProcessor.ExecuteAsync<IdentityServerAddUserResponseDTO>(requestOptions, false);

            // Throw exception if there is an error in adding the user on identity server.
            if(addUserResponseDTO.EwpError != null) {
                if(addUserResponseDTO.EwpError.ErrorType != ErrorType.Success) {
                    if(addUserResponseDTO.EwpError.ErrorType == ErrorType.Validation) {
                        throw new EwpValidationException("", addUserResponseDTO.EwpError.EwpErrorDataList);
                    }
                    if(addUserResponseDTO.EwpError.ErrorType == ErrorType.Duplicate) {
                        throw new EwpDuplicateNameException("User with email(" + identityUserDTO.Email + ") already exist.", addUserResponseDTO.EwpError.EwpErrorDataList);
                    }
                }
            }
            return addUserResponseDTO;
        }

        public async Task AssignApplicationOnIdentityServerAsync(Guid identityUserId, Guid tenantId, string clientAppType) {
            // Calling identity server API To assign application.
            HttpClient client = new HttpClient();
            string baseurl = _appMgmtAppSettings.IdentityServerUrl;
            string requesturl = "user/AssignApplicationToUser";

            // Create DTO for assiging applications.
            UserApplicationModel userApplicationModel = new UserApplicationModel {
                TenantId = tenantId,
                ClientAppType = clientAppType,
                UserId = identityUserId.ToString()
            };

            // Calling the identity service API for deleting the user.
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, requesturl, userApplicationModel, _appMgmtAppSettings.AppName, _appMgmtAppSettings.IdentityServerUrl, null);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseurl);
            await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
        }



        // Delete user from identity server.
        public async Task DeleteUserByTenantIdOnIdentityServerAsync(Guid idnetityUserId, Guid tenantId) {

            // Create request object.
            string baseurl = _appMgmtAppSettings.IdentityServerUrl;
            string requesturl = "user/DeleteUserForSpecificTenant?userid=" + idnetityUserId + "&tenantid=" + tenantId;

            // Calling the identity service API for deleting the user.
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Delete, AcceptMediaTypeEnum.JSON, requesturl, null, _appMgmtAppSettings.AppName, _appMgmtAppSettings.IdentityServerUrl, null);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseurl);
            await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
        }

        // Mark user active in tenant on identity server.
        public async Task MarkActiveInActiveOnIdentityServerAsync(bool active, Guid identityUserId, Guid tenantId) {
      // Create request object.
      //tenantId = new Guid("A3A84F84-2237-4420-8B20-D08428B420FD");
            string requesturl = string.Empty;
            string baseurl = _appMgmtAppSettings.IdentityServerUrl;
            if(active == true) {
                requesturl = string.Format("user/MarkUserActive?userid={0}&tenantId={1}", identityUserId, tenantId);
            }
            else {
                requesturl = string.Format("user/MarkUserInActive?userid={0}&tenantId={1}", identityUserId, tenantId);
            }

            // Calling the identity service API for deleting the user.
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, requesturl, null, _appMgmtAppSettings.AppName, _appMgmtAppSettings.IdentityServerUrl, null);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseurl);
            await httpRequestProcessor.ExecuteAsync<string>(requestOptions, false);
        }

        // Remove application to user on idnetity server.
        public async Task DeAssignApplicationOnIdentityServerAsync(Guid identityUserId, Guid tenantId, string appKey) {

            string baseurl = _appMgmtAppSettings.IdentityServerUrl;
            string requesturl = "user/RemoveApplicationFromUser";

            // Create UserApplicationModel for removing application on identity server.
            UserApplicationModel userApplicationModel = new UserApplicationModel {
                ClientAppType = appKey,
                TenantId = tenantId,
                UserId = identityUserId.ToString()
            };

            // Calling the identity service API for deleting the user.
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, requesturl, userApplicationModel, _appMgmtAppSettings.AppName, _appMgmtAppSettings.IdentityServerUrl, null);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseurl);
            await httpRequestProcessor.ExecuteAsync<string>(requestOptions, false);
        }

    }
}
