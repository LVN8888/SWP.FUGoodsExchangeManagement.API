using Microsoft.EntityFrameworkCore;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.StatisticalDTOs.ResponseModels;
using SWP.FUGoodsExchangeManagement.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.StatisticalServices
{
    public class StatisticalService : IStatisticalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatisticalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SummaryResponseModel> GetSummaryAsync()
        {
            var totalUsers = await _unitOfWork.UserRepository.Count(u => u.Role != "Admin");
            var totalPosts = await _unitOfWork.ProductPostRepository.Count();
            var totalReports = await _unitOfWork.ReportRepository.Count();
            var totalCampuses = await _unitOfWork.CampusRepository.Count();
            var totalCategories = await _unitOfWork.CategoryRepository.Count();

            return new SummaryResponseModel
            {
                TotalUsers = totalUsers,
                TotalPosts = totalPosts,
                TotalReports = totalReports,
                TotalCampuses = totalCampuses,
                TotalCategories = totalCategories
            };
        }
    }
}
