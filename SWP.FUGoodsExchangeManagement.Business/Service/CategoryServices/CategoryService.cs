using AutoMapper;
using SWP.FUGoodsExchangeManagement.Business.Utils;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.CategoryDTOs.RequestModel;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.CategoryDTOs.ResponseModel;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.ResponseModels;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.CategoryRepositories;
using SWP.FUGoodsExchangeManagement.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private const int PageSize = 10;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoryResponseModel>> GetAll(int? page, string? search)
        {
            var searchUnsign = Util.ConvertToUnsign(search ?? "");

            var qr = await _unitOfWork.CategoryRepository.Get(pageIndex: page ?? 0,
                                                          pageSize: PageSize,
                                                          filter: u => u.Name.Contains(searchUnsign),
                                                          orderBy: e => e.OrderBy(e => e.Name)
                                                         );

            return _mapper.Map<List<CategoryResponseModel>>(qr);
        }

        public async Task Add(CategoryCreateRequestModel model)
        {
            var currentCategory = await _unitOfWork.CategoryRepository.GetSingle(e => e.Name.Equals(model.Name));
            if (currentCategory != null) 
            {
                throw new CustomException("Category Name has existed");
            }

            Category category = _mapper.Map<Category>(model);
            
            await _unitOfWork.CategoryRepository.Insert(category);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task Update(CategoryUpdateRequestModel model)
        {
            var c1 = await _unitOfWork.CategoryRepository.GetSingle(e => e.Name.Equals(model.Name));
            if (c1 != null)
            {
                throw new CustomException("Category has existed");
            }            

            Category category = _mapper.Map<Category>(model);

            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveChangeAsync();
        }

        public Task Delete(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
