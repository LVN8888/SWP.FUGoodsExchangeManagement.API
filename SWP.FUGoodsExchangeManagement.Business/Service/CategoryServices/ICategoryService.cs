using SWP.FUGoodsExchangeManagement.Repository.DTOs.CategoryDTOs.RequestModel;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.CategoryDTOs.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.CategoryServices
{
    public interface ICategoryService
    {
        public Task<List<CategoryResponseModel>> GetAll(int? page, string? search);
        public Task Add(CategoryCreateRequestModel model);
        public Task Update(CategoryUpdateRequestModel model);
        public Task Delete(string Id);
    }
}
