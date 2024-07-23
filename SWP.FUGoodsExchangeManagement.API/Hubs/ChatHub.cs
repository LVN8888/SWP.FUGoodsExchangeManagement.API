using Microsoft.AspNetCore.SignalR;
using SWP.FUGoodsExchangeManagement.Business.Service.ChatServices;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SWP.FUGoodsExchangeManagement.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(IChatService chatService, ILogger<ChatHub> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        public async Task SendMessage(string chatId, string message, bool isSeller)
        {
            try
            {
                if (string.IsNullOrEmpty(chatId) || string.IsNullOrEmpty(message))
                {
                    throw new HubException("Invalid parameters.");
                }
                await _chatService.AddChatDetailAsync(chatId, message, isSeller);
                await Clients.Group(chatId).SendAsync("ReceiveMessage", message, isSeller);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SendMessage");
                throw;
            }
        }

        public async Task JoinChat(string chatId)
        {
            try
            {
                if (string.IsNullOrEmpty(chatId))
                {
                    throw new HubException("Invalid chatId.");
                }

                _logger.LogInformation($"Client {Context.ConnectionId} attempting to join chat {chatId}");
                await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
                _logger.LogInformation($"Client {Context.ConnectionId} successfully joined chat {chatId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in JoinChat");
                throw;
            }
        }

        public async Task LeaveChat(string chatId)
        {
            try
            {
                _logger.LogInformation($"Client {Context.ConnectionId} leaving chat {chatId}");
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
                _logger.LogInformation($"Client {Context.ConnectionId} left chat {chatId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LeaveChat");
                throw;
            }
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            _logger.LogInformation($"Client connected: {Context.ConnectionId}");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            _logger.LogInformation($"Client disconnected: {Context.ConnectionId}");
        }
    }
}