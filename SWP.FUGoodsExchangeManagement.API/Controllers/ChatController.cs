using Microsoft.AspNetCore.Mvc;
using SWP.FUGoodsExchangeManagement.Business.Service.ChatServices;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.ChatDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateChat([FromBody] ChatCreateRequestModel chatDto)
        {
            var chat = await _chatService.CreateChatAsync(chatDto);
            return Ok(chat);
        }

        [HttpPost("sendmessage")]
        public async Task<IActionResult> SendMessage(string chatId, string message, string buyerId)
        {
            await _chatService.AddChatDetailAsync(chatId, message, buyerId);
            return Ok();
        }

        [HttpPost("close/{chatId}")]
        public async Task<IActionResult> CloseChat(string chatId)
        {
            await _chatService.CloseChatAsync(chatId);
            return Ok();
        }

        [HttpGet("history/{chatId}")]
        public async Task<IActionResult> GetChatHistory(string chatId)
        {
            var chat = await _chatService.GetChatByIdAsync(chatId);
            if (chat == null) return NotFound();

            return Ok(chat);
        }

        [HttpGet("userchats/{userId}")]
        public async Task<IActionResult> GetChatsByUserId(string userId)
        {
            var chats = await _chatService.GetChatsByUserIdAsync(userId);
            return Ok(chats);
        }
    }
}