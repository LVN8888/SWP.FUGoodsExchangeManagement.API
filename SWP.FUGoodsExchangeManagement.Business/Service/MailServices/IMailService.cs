using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.MailServices
{
    public interface IMailService
    {
        Task<bool> SendEmail(string Email, string Subject, string Html);
    }
}
