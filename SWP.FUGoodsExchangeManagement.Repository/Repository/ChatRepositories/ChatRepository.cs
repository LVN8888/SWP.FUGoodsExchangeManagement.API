using Microsoft.EntityFrameworkCore;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.GenericRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.Repository.ChatRepositories
{
    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        private readonly FugoodsExchangeManagementContext _context;

        public ChatRepository(FugoodsExchangeManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Chat> GetChatByIdWithDetailsAsync(string chatId)
        {
            var chat = await _context.Chats
                                 .Include(c => c.ChatDetails)
                                 .FirstOrDefaultAsync(c => c.Id == chatId);
            if (chat != null && chat.ChatDetails == null)
            {
                chat.ChatDetails = new List<ChatDetail>();
            }
            return chat;
        }

        public async Task<List<Chat>> GetChatsByUserIdAsync(string userId)
        {
            return await _context.Chats
                .Include(c => c.ChatDetails)
                .Where(c => c.BuyerId == userId)
                .ToListAsync();
        }
    }
}
