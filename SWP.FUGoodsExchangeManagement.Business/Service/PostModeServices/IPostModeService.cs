using SWP.FUGoodsExchangeManagement.Repository.DTOs.PostModeDTOs;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.PostModeServices
{
    public interface IPostModeService
    {
        Task AddPostMode(PostModeAddRequestModel requestModel);
        Task<List<PostModeListModel>> ShowPostModeList();
        Task UpdatePostMode(PostModeUpdateModel requestModel);
    }
}
