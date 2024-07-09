using Microsoft.EntityFrameworkCore;
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
        private readonly FugoodsExchangeManagementContext _context;

        public PaymentRepository(FugoodsExchangeManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Payment> GetById(string paymentId)
        {
            return await _context.Payments
                            .Include(p => p.ProductPost)
                            .FirstOrDefaultAsync(p => p.Id.Equals(paymentId));
        }
    }
}
