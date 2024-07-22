using SWP.FUGoodsExchangeManagement.Repository.DTOs.ChatDTOs;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.ChatDetailRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.ChatRepositories;
using SWP.FUGoodsExchangeManagement.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.ChatServices
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IChatDetailRepository _chatDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChatService(IChatRepository chatRepository, IChatDetailRepository chatDetailRepository, IUnitOfWork unitOfWork)
        {
            _chatRepository = chatRepository;
            _chatDetailRepository = chatDetailRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ChatRequestModel> CreateChatAsync(ChatCreateRequestModel chatDto)
        {
            var chat = new Chat
            {
                Id = Guid.NewGuid().ToString(),
                ProductPostId = chatDto.ProductPostId,
                BuyerId = chatDto.BuyerId,
                ChatDetails = new List<ChatDetail>()
            };

            await _chatRepository.Insert(chat);

            var result = await _unitOfWork.SaveChangeAsync();
            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }

            return new ChatRequestModel
            {
                Id = chat.Id,
                ProductPostId = chat.ProductPostId,
                BuyerId = chat.BuyerId,
                ChatDetails = new List<ChatDetailRequestModel>()
            };
        }

        public async Task AddChatDetailAsync(string chatId, string message, string buyerId)
        {
            var chatDetail = new ChatDetail
            {
                Id = Guid.NewGuid().ToString(),
                Message = message,
                Time = DateTime.UtcNow,
                Flag = false,
                ChatId = chatId
            };

            await _chatDetailRepository.Insert(chatDetail);
            var result = await _unitOfWork.SaveChangeAsync();
            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task<ChatRequestModel> GetChatByIdAsync(string chatId)
        {
            var chat = await _chatRepository.GetChatByIdWithDetailsAsync(chatId);
            if (chat == null) return null;

            return new ChatRequestModel
            {
                Id = chat.Id,
                ProductPostId = chat.ProductPostId,
                BuyerId = chat.BuyerId,
                ChatDetails = chat.ChatDetails.Select(cd => new ChatDetailRequestModel
                {
                    Id = cd.Id,
                    Message = cd.Message,
                    Time = cd.Time,
                    Flag = cd.Flag,
                    ChatId = cd.ChatId
                }).ToList()
            };
        }

        public async Task<List<ChatRequestModel>> GetChatsByUserIdAsync(string userId)
        {
            var chats = await _chatRepository.GetChatsByUserIdAsync(userId);
            return chats.Select(chat => new ChatRequestModel
            {
                Id = chat.Id,
                ProductPostId = chat.ProductPostId,
                BuyerId = chat.BuyerId,
                ChatDetails = chat.ChatDetails.Select(cd => new ChatDetailRequestModel
                {
                    Id = cd.Id,
                    Message = cd.Message,
                    Time = cd.Time,
                    Flag = cd.Flag,
                    ChatId = cd.ChatId
                }).ToList()
            }).ToList();
        }

        public async Task CloseChatAsync(string chatId)
        {
            var chat = await _chatRepository.GetById(chatId);
            if (chat == null) return;
            _chatRepository.Update(chat);
            var result = await _unitOfWork.SaveChangeAsync();
            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task FlagMessageAsync(string messageId)
        {
            var chatDetail = await _chatDetailRepository.GetById(messageId);
            if (chatDetail == null) return;

            chatDetail.Flag = true;
            _chatDetailRepository.Update(chatDetail);

            var chat = await _chatRepository.GetById(chatDetail.ChatId);
            if (chat != null)
            {
                _chatRepository.Update(chat);
            }

            var result = await _unitOfWork.SaveChangeAsync();
            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }
    }
}
