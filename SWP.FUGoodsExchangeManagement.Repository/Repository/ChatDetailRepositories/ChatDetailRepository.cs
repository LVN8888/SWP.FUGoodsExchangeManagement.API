using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.Repository.ChatDetailRepositories
{
    public class ChatDetailRepository : GenericRepository<ChatDetail>, IChatDetailRepository
    {
        private readonly FugoodsExchangeManagementContext _context;
        public ChatDetailRepository(FugoodsExchangeManagementContext context) : base(context)
        {

        }
    }
}
