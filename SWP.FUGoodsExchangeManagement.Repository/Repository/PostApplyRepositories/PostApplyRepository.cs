using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.Repository.PostApplyRepositories
{
    public class PostApplyRepository : GenericRepository<PostApply>, IPostApplyRepository
    {
        public PostApplyRepository(FugoodsExchangeManagementContext context) : base(context)
        {
        }
    }
}
