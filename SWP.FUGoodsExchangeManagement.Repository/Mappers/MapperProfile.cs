using AutoMapper;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.ResponseModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.TokenDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.CampusDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.CampusDTOs.ResponseModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.CategoryDTOs.ResponseModel;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.ProductPostDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.CategoryDTOs.RequestModel;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.PaymentDTOs.Response;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.PostModeDTOs;

namespace SWP.FUGoodsExchangeManagement.Repository.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserRegisterRequestModel, User>();
            CreateMap<UserRegisterRequestModelVer1, User>();
            CreateMap<RefreshToken, GetNewRefreshTokenDTO>();
            CreateMap<User, UserListResponseModel>();
            CreateMap<User, UserResponseModel>();
            //Campus
            CreateMap<Campus, CampusResponseModel>();
            CreateMap<AddCampusDTO, Campus>();
            CreateMap<EditCampusDTO, Campus>();
            CreateMap<DeleteCampusDTO, Campus>();

            CreateMap<Category, CategoryResponseModel>();
            CreateMap<CategoryCreateRequestModel, Category>();

            CreateMap<ProductPostUpdateRequestModel, ProductPost>();
            CreateMap<ProductPostCreateRequestModel, ProductPost>();

            CreateMap<PostMode, PostModeListModel>();

            CreateMap<Payment, PaymentResponseModel>();
        }
    }
}
