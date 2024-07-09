using MailKit.Search;
using SWP.FUGoodsExchangeManagement.Business.Service.AuthenticationServices;
using SWP.FUGoodsExchangeManagement.Business.Utils;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.PostApplyDTOs;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.ProductPostDTOs.ResponseModels;
using SWP.FUGoodsExchangeManagement.Repository.Enums;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.PostApplyServices
{
    public class PostApplyService : IPostApplyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;

        public PostApplyService(IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
        }

        public async Task BuyProduct(string? message, string postId, string token)
        {
            var userId = _authenticationService.decodeToken(token, "userId");
            var chosenPost = await _unitOfWork.ProductPostRepository.GetSingle(p => p.Id.Equals(postId));

            string mess = null;
            if (message != null)
            {
                mess = message;
            }

            var newPostApply = new PostApply
            {
                Id = Guid.NewGuid().ToString(),
                Message = mess,
                ProductPostId = postId,
                BuyerId = userId,
            };
            chosenPost.Status = ProductPostStatus.Pending.ToString();

            _unitOfWork.ProductPostRepository.Update(chosenPost);
            await _unitOfWork.PostApplyRepository.Insert(newPostApply);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteApplyPost(string id)
        {
            var chosenApply = await _unitOfWork.PostApplyRepository.GetSingle(p => p.Id == id);
            if (chosenApply != null)
            {
                await _unitOfWork.PostApplyRepository.Delete(chosenApply);
                await _unitOfWork.SaveChangeAsync();
            }
            else throw new CustomException("This apply post is not existed");
        }

        public async Task<List<PostApplyResponseModel>> GetApplyOfPost(string postId, int? pageIndex)
        {
            var applyList = await _unitOfWork.PostApplyRepository.Get(p => p.ProductPostId.Equals(postId), null, includeProperties: "Buyer", pageIndex ?? 1, 5);
            return applyList.Select(a => new PostApplyResponseModel
            {
                Id = a.Id,
                Message = a.Message ?? null,
                BuyerInfo = new BuyerInfo
                {
                    Id = a.BuyerId,
                    Email = a.Buyer.Email,
                    Name = a.Buyer.Fullname,
                    PhoneNumber = a.Buyer.PhoneNumber
                }
            }).ToList();
        }

        public async Task<List<ProductPostResponseModel>> GetOwnApplyPost(string token, int? pageIndex, int ItemPerPage)
        {
            var userId = _authenticationService.decodeToken(token, "userId");
            var ownList = await _unitOfWork.PostApplyRepository.Get(p => p.BuyerId.Equals(userId));
            var buyingPostListId = ownList.Select(o => o.ProductPostId).ToList();
            var allBuyingPost = await _unitOfWork.ProductPostRepository.Get(p => buyingPostListId.Contains(p.Id), null, includeProperties: "Category,PostMode,Campus,CreatedByNavigation", pageIndex ?? 1, ItemPerPage);
            var waitingPostListId = allBuyingPost.Select(a => a.Id).ToList();
            var allImages = await _unitOfWork.ProductImagesRepository.Get(i => waitingPostListId.Contains(i.ProductPostId));

            var responseList = allBuyingPost.Select(a => new ProductPostResponseModel
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                Price = a.Price,
                CreatedBy = new PostAuthor
                {
                    FullName = a.CreatedByNavigation.Fullname,
                    Email = a.CreatedByNavigation.Email,
                    PhoneNumber = a.CreatedByNavigation.PhoneNumber
                },
                Category = a.Category.Name,
                Campus = a.Campus.Name,
                CreatedDate = a.CreatedDate,
                ExpiredDate = a.ExpiredDate ?? null,
                PostMode = a.PostMode.Type,
                ImageUrls = allImages.Where(ai => ai.ProductPostId.Equals(a.Id)).Select(ai => ai.Url).ToList(),
            }).ToList();
            return responseList;
        }
    }
}
