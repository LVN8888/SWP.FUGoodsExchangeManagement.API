using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.Repository.CampusRepositories
{
    public class CampusRepository : GenericRepository<Campus>, ICampusRepository
    {
        private readonly FugoodsExchangeManagementContext _context;

        public CampusRepository(FugoodsExchangeManagementContext context) : base(context)
        {

        }
    }
}
