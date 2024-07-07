using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.Repository.PaymentRepositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(FugoodsExchangeManagementContext context) : base(context)
        {
        }
    }
}
