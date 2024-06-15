﻿using AutoMapper;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.ResponseModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.TokenDTOs;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.CategoryDTOs.RequestModel;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.CategoryDTOs.ResponseModel;

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
            
            CreateMap<CategoryCreateRequestModel, Category>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));

            CreateMap<CategoryUpdateRequestModel, Category>();

            CreateMap<Category, CategoryResponseModel>();
        }
    }
}
