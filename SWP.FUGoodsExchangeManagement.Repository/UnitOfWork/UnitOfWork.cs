using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.OTPRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.PostModeRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.ProductPostRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.TokenRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FugoodsExchangeManagementContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IOTPRepository _oTPRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IPostModeRepository _postModeRepository;
        private readonly IProductPostRepository _productPostRepository;

        public UnitOfWork(FugoodsExchangeManagementContext context, IUserRepository userRepository, IOTPRepository oTPRepository, ITokenRepository tokenRepository, 
            IPostModeRepository postModeRepository, IProductPostRepository productPostRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _oTPRepository = oTPRepository;
            _tokenRepository = tokenRepository;
            _postModeRepository = postModeRepository;
            _productPostRepository = productPostRepository;
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IUserRepository UserRepository => _userRepository;
        public IOTPRepository OTPRepository => _oTPRepository;
        public ITokenRepository TokenRepository => _tokenRepository;
        public IPostModeRepository PostModeRepository => _postModeRepository;
        public IProductPostRepository ProductPostRepository => _productPostRepository;
    }
}
