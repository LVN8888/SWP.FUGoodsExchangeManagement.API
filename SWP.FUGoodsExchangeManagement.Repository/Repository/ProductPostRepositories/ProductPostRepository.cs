using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.GenericRepository;
using SWP.FUGoodsExchangeManagement.Repository.Repository.ProductPostRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.Repository.ProductPostRepository
{
    public class ProductPostRepository : GenericRepository<ProductPost>, IProductPostRepository
    {
        public ProductPostRepository(FugoodsExchangeManagementContext context) : base(context)
        {
        }
    }
}
