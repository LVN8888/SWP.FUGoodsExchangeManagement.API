using Microsoft.EntityFrameworkCore;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.CampusRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.CategoryRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.OTPRepositories;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICampusRepository _campusRepository;
        private readonly IReportRepository _reportRepository;

        public UnitOfWork(FugoodsExchangeManagementContext context, 
            IUserRepository userRepository, 
            IOTPRepository oTPRepository, 
            ITokenRepository tokenRepository,
            ICategoryRepository categoryRepository,
            ICampusRepository campusRepository,
            IReportRepository reportRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _oTPRepository = oTPRepository;
            _tokenRepository = tokenRepository;
            _categoryRepository = categoryRepository;
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
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public ICampusRepository CampusRepository => _campusRepository;
        public IReportRepository ReportRepository => _reportRepository;
    }
}
