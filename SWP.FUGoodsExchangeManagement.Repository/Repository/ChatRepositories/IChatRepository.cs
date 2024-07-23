using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.Repository.ChatRepositories
{
    public interface IChatRepository : IGenericRepository<Chat>
    {
        Task<Chat> GetChatByIdWithDetailsAsync(string chatId);
        Task<List<Chat>> GetChatsByUserIdAsync(string userId);
    }
}
