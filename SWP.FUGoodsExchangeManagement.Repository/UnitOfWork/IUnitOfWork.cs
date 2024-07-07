using SWP.FUGoodsExchangeManagement.Repository.Repository.CampusRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.CategoryRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.OTPRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.ReportRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.TokenRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangeAsync();
        public IUserRepository UserRepository { get; }
        public IOTPRepository OTPRepository { get; }       
        public ITokenRepository TokenRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ICampusRepository CampusRepository { get; }
        public IReportRepository ReportRepository { get; }
    }
}
