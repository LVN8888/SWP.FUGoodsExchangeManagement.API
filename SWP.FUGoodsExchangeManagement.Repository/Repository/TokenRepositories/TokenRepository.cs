using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.Repository.TokenRepositories
{
    public class TokenRepository : GenericRepository<RefreshToken> , ITokenRepository
    {
        private readonly FugoodsExchangeManagementContext _context;
        public TokenRepository(FugoodsExchangeManagementContext context) : base(context)
        {
        }

    }
}
