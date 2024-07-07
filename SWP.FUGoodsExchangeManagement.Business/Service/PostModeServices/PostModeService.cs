using SWP.FUGoodsExchangeManagement.Repository.DTOs.PostModeDTOs;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.GenericRepository;
using SWP.FUGoodsExchangeManagement.Repository.Repository.PostModeRepositories;
using SWP.FUGoodsExchangeManagement.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.PostModeServices
{
    public class PostModeService : IPostModeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostModeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddPostMode(PostModeAddRequestModel requestModel)
        {
            var newPostMode = new PostMode
            {
                Id = Guid.NewGuid().ToString(),
                Type = requestModel.Type,
                Duration = requestModel.Duration,
                Price = requestModel.Price
            };
            await _unitOfWork.PostModeRepository.Insert(newPostMode);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<List<PostModeListModel>> ShowPostModeList()
        {
            var postModeList = new List<PostModeListModel>();
            var allMode = await _unitOfWork.PostModeRepository.Get();
            foreach (var model in allMode)
            {
                postModeList.Add(new PostModeListModel
                {
                    Type = model.Type,
                    Duration = model.Duration,
                    Price = model.Price
                });
            }
            return postModeList;
        }

        public async Task UpdatePostMode(PostModeUpdateModel requestModel)
        {
            var postMode = await _unitOfWork.PostModeRepository.GetSingle(p => p.Id.Equals(requestModel.Id));
            postMode.Type = requestModel.Type;
            postMode.Duration = requestModel.Duration;
            postMode.Price = requestModel.Price;
            _unitOfWork.PostModeRepository.Update(postMode);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
