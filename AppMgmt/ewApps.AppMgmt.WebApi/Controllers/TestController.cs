using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppMgmt.DS;
using ewApps.AppMgmt.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppMgmt.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TestController:ControllerBase {

        ITenantSignUpForPublisherDS _singUpTenantAndTenantUserDS;

        public TestController(ITenantSignUpForPublisherDS singUpTenantAndTenantUserDS) {
            _singUpTenantAndTenantUserDS = singUpTenantAndTenantUserDS;
        }

        [HttpGet]
        [Route("adduser")]
        public async Task AddUserTest() {
            TenantUserSignUpDTO addTenantUserRequestDTO = new TenantUserSignUpDTO(); //List< UserAppRelationDTO > userAppRelationDTOs

            addTenantUserRequestDTO.Email = "abc6f@mailinator.com";
            addTenantUserRequestDTO.FirstName = "f";
            addTenantUserRequestDTO.LastName = "L";
            addTenantUserRequestDTO.FullName = "L F";
            addTenantUserRequestDTO.Phone = "123";
            addTenantUserRequestDTO.TenantId = Guid.NewGuid();
            addTenantUserRequestDTO.UserType = 2;

            List<UserAppRelationDTO> userAppRelationDTOs = new List<UserAppRelationDTO>();
            UserAppRelationDTO dto = new UserAppRelationDTO();
            dto.AppId = new Guid("67D09A6F-CE95-498C-BF69-33C7D38F9041");
            dto.AppKey = "pub";
            userAppRelationDTOs.Add(dto);

            //await _singUpTenantAndTenantUserDS.SingUpPublisherAdminUser(addTenantUserRequestDTO, userAppRelationDTOs);
        }


    }
}