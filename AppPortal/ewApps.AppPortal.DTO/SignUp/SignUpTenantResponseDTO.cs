namespace ewApps.AppPortal.DTO {
    public class TenantSignUpResponseDTO {

        /// <summary>
        /// The signed-up tenant information.
        /// </summary>
        public TenantDTO TenantInfo {
            get; set;
        }

        /// <summary>
        /// The signed-up tenant user information.
        /// </summary>
        public TenantUserDTO TenantUserInfo {
            get; set;
        }
    }
}
