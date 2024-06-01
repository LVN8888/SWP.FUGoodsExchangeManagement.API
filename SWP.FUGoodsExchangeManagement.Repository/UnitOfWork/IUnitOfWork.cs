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
    }
}
