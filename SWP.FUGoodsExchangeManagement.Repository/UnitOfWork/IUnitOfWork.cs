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
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {

        Task<int> SaveChangeAsync();
        IUserRepository UserRepository { get; }
        IOTPRepository OTPRepository { get; }
        ITokenRepository TokenRepository { get; }
        IPostModeRepository PostModeRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductImagesRepository ProductImagesRepository { get; }
        IProductPostRepository ProductPostRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IPostApplyRepository PostApplyRepository { get; }
        ICampusRepository CampusRepository { get; }
        IReportRepository ReportRepository { get; }
    }
}
