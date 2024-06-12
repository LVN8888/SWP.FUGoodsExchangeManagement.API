using SWP.FUGoodsExchangeManagement.Business.Utils;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.CampusDTOs;
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

        public CampusService(ICampusRepository campusRepository, IUnitOfWork unitOfWork)
        {
            _campusRepository = campusRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddCampusAsync(AddCampusDTO addCampusDto)
        {
            if (string.IsNullOrWhiteSpace(addCampusDto.Name))
                throw new CustomException("Campus name cannot be empty");

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
    }
}
