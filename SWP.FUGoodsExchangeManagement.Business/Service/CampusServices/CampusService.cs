﻿using AutoMapper;
using Microsoft.IdentityModel.Abstractions;
using SWP.FUGoodsExchangeManagement.Business.Utils;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.CampusDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.CampusDTOs.ResponseModels;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.CampusRepositories;
using SWP.FUGoodsExchangeManagement.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.CampusServices
{
    public class CampusService : ICampusService
    {
        private readonly ICampusRepository _campusRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CampusService(ICampusRepository campusRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _campusRepository = campusRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddCampusAsync(AddCampusDTO addCampusDto)
        {
            if (string.IsNullOrWhiteSpace(addCampusDto.Name)) throw new CustomException("Campus name cannot be empty");

            var existingCampus = await _campusRepository.GetSingle(c => c.Name == addCampusDto.Name);
            if (existingCampus != null) throw new CustomException("A campus with this name already exists");
            var newCampus = new Campus
            {
                Id = Guid.NewGuid().ToString(),
                Name = addCampusDto.Name
            };
            await _campusRepository.Insert(newCampus);
            var result = await _unitOfWork.SaveChangeAsync();
            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task EditCampusAsync(EditCampusDTO editCampusDto)
        {
            if (string.IsNullOrWhiteSpace(editCampusDto.Name))
                throw new CustomException("Campus name cannot be empty");

            var existingCampus = await _campusRepository.GetSingle(c => c.Name == editCampusDto.Name && c.Id != editCampusDto.Id);
            if (existingCampus != null)
                throw new CustomException("A campus with this name already exists");

            var campus = await _campusRepository.GetSingle(c => c.Id == editCampusDto.Id);
            if (campus == null)
                throw new CustomException("Campus not found");

            campus.Name = editCampusDto.Name;
            _campusRepository.Update(campus);
            var result = await _unitOfWork.SaveChangeAsync();
            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task DeleteCampusAsync(DeleteCampusDTO deleteCampusDTO)
        {
            var campus = await _campusRepository.GetSingle(c => c.Id == deleteCampusDTO.Id);
            if (campus == null)
                throw new CustomException("Campus not found");

            _campusRepository.Delete(campus);
            var result = await _unitOfWork.SaveChangeAsync();
            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task<IEnumerable<CampusResponseModel>> GetAllCampusesAsync()
        {
            var campuses = await _campusRepository.Get(orderBy: q => q.OrderBy(c => c.Name));
            return _mapper.Map<IEnumerable<CampusResponseModel>>(campuses);
        }
    }
}
