using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.PostApplyServices
{
    public interface IPostApplyService
    {
        Task BuyProduct(string? message, string postId, string token);
        Task DeleteApplyPost(string id);
    }
}
