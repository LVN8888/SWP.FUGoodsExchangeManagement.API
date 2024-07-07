using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.Repository.ReportRepositories
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        private readonly FugoodsExchangeManagementContext _context;
        public ReportRepository(FugoodsExchangeManagementContext context) : base(context)
        {

        }
    }
}
