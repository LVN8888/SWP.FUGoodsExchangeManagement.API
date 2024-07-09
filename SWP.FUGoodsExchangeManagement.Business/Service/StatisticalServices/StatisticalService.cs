using Microsoft.EntityFrameworkCore;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.StatisticalDTOs.ResponseModels;
using SWP.FUGoodsExchangeManagement.Repository.Enums;
using SWP.FUGoodsExchangeManagement.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<PostModeStatisticsResponseModel>> GetPostModeStatisticsAsync(DateTime? startDate, DateTime? endDate)
        {
            var start = startDate ?? new DateTime(1753, 1, 1);
            var end = endDate.HasValue && endDate.Value <= DateTime.Now ? endDate.Value : DateTime.Now;

            if (end < start)
            {
                throw new ArgumentException("End date must be greater than or equal to start date.");
            }

            var postApplyList = await _unitOfWork.PostApplyRepository
                .Get(pa => pa.ProductPost.CreatedDate >= start && pa.ProductPost.CreatedDate <= end,
                     includeProperties: "ProductPost.PostMode,ProductPost.Payments");

            var postModeStatistics = postApplyList
                .GroupBy(pa => pa.ProductPost.PostMode.Type)
                .Select(g => new PostModeStatisticsResponseModel
                {
                    PostModeType = g.Key,
                    TotalSold = g.Count(),
                    TotalRevenue = g.Sum(pa => pa.ProductPost.Payments.Sum(p => decimal.Parse(p.Price)))
                })
                .ToList();

            return postModeStatistics;
        }

        public async Task<IEnumerable<UserPurchaseStatisticsResponseModel>> GetUserPurchaseStatisticsAsync(DateTime? startDate, DateTime? endDate)
        {
            var start = startDate ?? new DateTime(1753, 1, 1);
            var end = endDate.HasValue && endDate.Value <= DateTime.Now ? endDate.Value : DateTime.Now;

            if (end < start)
            {
                throw new ArgumentException("End date must be greater than or equal to start date.");
            }

            var paymentList = await _unitOfWork.PaymentRepository
                .Get(p => p.Status == PaymentStatus.Success.ToString() && p.PaymentDate >= start && p.PaymentDate <= end,
                     includeProperties: "ProductPost.CreatedByNavigation");

            var userPurchaseStatistics = paymentList
                .GroupBy(p => new { p.ProductPost.CreatedBy, p.ProductPost.CreatedByNavigation.Fullname, p.ProductPost.CreatedByNavigation.Email })
                .Select(g => new UserPurchaseStatisticsResponseModel
                {
                    UserId = g.Key.CreatedBy,
                    Fullname = g.Key.Fullname,
                    Email = g.Key.Email,
                    TotalSpent = g.Sum(p => decimal.Parse(p.Price))
                })
                .ToList();

            return userPurchaseStatistics;
        }

    }
}
