using SWP.FUGoodsExchangeManagement.Repository.Repository.ProductPostRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.ProductPostServices
{
    public class ProductPostService : IProductPostService
    {
        private readonly IProductPostRepository _productPostRepository;
        public ProductPostService(IProductPostRepository productPostRepository)
        {
            _productPostRepository = productPostRepository;
        }

        //public void AddProductPost()
    }
}
