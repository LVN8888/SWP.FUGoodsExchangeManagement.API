using SWP.FUGoodsExchangeManagement.Repository.DTOs.PostApplyDTOs;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.ProductPostDTOs.ResponseModels;
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
        Task<List<PostApplyResponseModel>> GetApplyOfPost(string postId, int? pageIndex);
        Task<List<ProductPostResponseModel>> GetOwnApplyPost(string token, int? pageIndex, int ItemPerPage);
        Task<List<ProductPostResponseModel>> GetOwnBuyedApplyPost(string token, int? pageIndex, int ItemPerPage);
    }
}
