using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {
    public class TokenInfoDS  : BaseDS<TokenInfo> , ITokenInfoDS{

        ITokenInfoRepository _tokenInfoRepository;
        IUnitOfWork _unitOfWork;


        /// <summary>
        /// Initializes a new instance of the <see cref="PortalDS"/> class with its dependencies.
        /// </summary>
        /// <param name="tokenInfoRepository">An instance of <see cref="IPortalAppLinkingRepository"/> to communicate with storage.</param>
        public TokenInfoDS(ITokenInfoRepository tokenInfoRepository , IUnitOfWork unitOfWork) : base(tokenInfoRepository) {
            _tokenInfoRepository = tokenInfoRepository;
            _unitOfWork = unitOfWork;
        }

        ///<inheritdoc/>
        public async Task DeleteTokenAsync(TokenInfoDTO tokenInfoDTO) {

            if(string.IsNullOrEmpty(tokenInfoDTO.AppKey)) {
                List<TokenInfo> tokenInfos = (await FindAllAsync(ti => ti.TenantUserId == tokenInfoDTO.TenantUserId && ti.UserType == tokenInfoDTO.UserType && ti.TokenType == tokenInfoDTO.TokenType)).ToList();
                if(tokenInfos.Any(t => t.ID == tokenInfoDTO.TokenId)) {
                    foreach(TokenInfo item in tokenInfos) {
                        _tokenInfoRepository.Delete(item);
                        _unitOfWork.Save();
                    }
                }
            }
            else {
                List<TokenInfo> tokenInfos = (await FindAllAsync(ti => ti.TenantUserId == tokenInfoDTO.TenantUserId && ti.AppKey == tokenInfoDTO.AppKey && ti.TokenType == tokenInfoDTO.TokenType)).ToList();
                if(tokenInfos.Any(t => t.ID == tokenInfoDTO.TokenId)) {
                    foreach(TokenInfo item in tokenInfos) {
                        _tokenInfoRepository.Delete(item);
                        _unitOfWork.Save();
                    }
                }
            }
        }

        ///<inheritdoc/>
        public async Task DeleteTokenByTenantUserIdAndTokenType(Guid tenantUserId , Guid tenantId, int tokenType) {
            List<TokenInfo> tokenInfos = (await FindAllAsync(ti => ti.TenantUserId == tenantUserId && ti.TenantId == tenantId && ti.TokenType == tokenType)).ToList();
            foreach(TokenInfo item in tokenInfos) {
                _tokenInfoRepository.Delete(item);
                _unitOfWork.Save();
            }
        }

    }
}
