using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.Repository.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly FugoodsExchangeManagementContext _dbContext;
        public UserRepository(FugoodsExchangeManagementContext dbContext) : base(dbContext) 
        {
        }
    }
}
