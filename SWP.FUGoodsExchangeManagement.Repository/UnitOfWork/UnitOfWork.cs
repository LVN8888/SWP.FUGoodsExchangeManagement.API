using SWP.FUGoodsExchangeManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FugoodsExchangeManagementContext context;
        public UnitOfWork(FugoodsExchangeManagementContext dbContext)
        {
            context = dbContext;
        }
    }
}
