using AutoMapper;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.TokenDTOs;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserRegisterRequestModel, User>();
            CreateMap<UserRegisterRequestModelVer1, User>();
            CreateMap<RefreshToken, GetNewRefreshTokenDTO>();
        }
    }
}
