using SWP.FUGoodsExchangeManagement.Repository.DTOs.ChatDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.ChatServices
{
    public interface IChatService
    {
        Task<ChatRequestModel> CreateChatAsync(ChatCreateRequestModel chatDto); 
        Task AddChatDetailAsync(string chatId, string message, string buyerId);
        Task<ChatRequestModel> GetChatByIdAsync(string chatId);
        Task<List<ChatRequestModel>> GetChatsByUserIdAsync(string userId);
        Task CloseChatAsync(string chatId);
        Task FlagMessageAsync(string messageId);
    }
}
