﻿using SWP.FUGoodsExchangeManagement.Repository.DTOs.ProductPostDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.ProductPostDTOs.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.ProductPostServices
{
    public interface IProductPostService
    {
        Task CreateWaitingProductPost(ProductPostCreateRequestModel requestModel, string token);
        Task ApprovePost(string status, string id);
        Task<List<ProductPostResponseModel>> ViewAllPostWithStatus(int? pageIndex, PostSearchModel searchModel, string status);
        Task<List<ProductPostResponseModel>> ViewOwnPostWithStatus(int? pageIndex, PostSearchModel searchModel, string status, string token);
        Task<List<ProductPostResponseModel>> ViewOwnPostExceptMine(int? pageIndex, PostSearchModel searchModel, string token);
        Task<ProductPostResponseModel> ViewDetailsOfPost(string id);
        Task UpdateProductPost(string id, ProductPostUpdateRequestModel requestModel);
        Task ExtendExpiredDate(string id, string postModeId, string token);
        Task CloseProductPost(string id);
    }
}