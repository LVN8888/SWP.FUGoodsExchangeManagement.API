using Microsoft.EntityFrameworkCore;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.CampusRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.CategoryRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.OTPRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.PaymentRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.PostApplyRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.PostModeRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.ProductImagesRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.ProductPostRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.ReportRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.TokenRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductImagesRepository _productImagesRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPostApplyRepository _postApplyRepository;
        private readonly ICampusRepository _campusRepository;
        private readonly IReportRepository _reportRepository;

        public UnitOfWork(FugoodsExchangeManagementContext context,
            IUserRepository userRepository,
            IOTPRepository oTPRepository,
            ITokenRepository tokenRepository,
            ICategoryRepository categoryRepository,
            IProductPostRepository productPostRepository,
            IPostModeRepository postModeRepository,
            IProductImagesRepository productImagesRepository,
            IPaymentRepository paymentRepository,
            IPostApplyRepository postApplyRepository,
            ICampusRepository campusRepository,
            IReportRepository reportRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _oTPRepository = oTPRepository;
            _tokenRepository = tokenRepository;
            _categoryRepository = categoryRepository;
            _productPostRepository = productPostRepository;
            _postModeRepository = postModeRepository;
            _productImagesRepository = productImagesRepository;
            _paymentRepository = paymentRepository;
            _postApplyRepository = postApplyRepository;
            _campusRepository = campusRepository;
            _reportRepository = reportRepository;
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
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IProductImagesRepository ProductImagesRepository => _productImagesRepository;
        public IPaymentRepository PaymentRepository => _paymentRepository;
        public IPostApplyRepository PostApplyRepository => _postApplyRepository;
        public ICampusRepository CampusRepository => _campusRepository;
        public IReportRepository ReportRepository => _reportRepository;
    }
}
