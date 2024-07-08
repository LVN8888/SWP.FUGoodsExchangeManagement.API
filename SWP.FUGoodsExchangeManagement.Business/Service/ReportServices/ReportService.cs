using SWP.FUGoodsExchangeManagement.Business.Utils;
using SWP.FUGoodsExchangeManagement.Repository.Enums;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.ReportServices
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateReportAsync(string productPostId, string createdBy, List<ReportReasonEnums> reasons)
        {
            var existingReport = await _unitOfWork.ReportRepository.GetSingle(r => r.ProductPostId == productPostId && r.CreatedBy == createdBy);
            if (existingReport != null)
            {
                throw new CustomException("User has already reported this product post.");
            }

            var report = new Report
            {
                Id = Guid.NewGuid().ToString(),
                ProductPostId = productPostId,
                CreatedBy = createdBy,
                Date = DateTime.UtcNow,
                Content = string.Join(", ", reasons.Select(r => r.ToString()))
            };

            await _unitOfWork.ReportRepository.Insert(report);
            var result = await _unitOfWork.SaveChangeAsync();
            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task ApproveReportAsync(string reportId)
        {
            var report = await _unitOfWork.ReportRepository.GetSingle(r => r.Id == reportId);
            if (report == null)
            {
                throw new CustomException("Report not found.");
            }

            // Implement approval logic here
            // E.g., setting a status field, notifying user, etc.

            var result = await _unitOfWork.SaveChangeAsync();
            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task DeclineReportAsync(string reportId)
        {
            var report = await _unitOfWork.ReportRepository.GetSingle(r => r.Id == reportId);
            if (report == null)
            {
                throw new CustomException("Report not found.");
            }

            // Implement decline logic here
            // E.g., setting a status field, notifying user, etc.

            var result = await _unitOfWork.SaveChangeAsync();
            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task<IEnumerable<Report>> GetAllReportsAsync()
        {
            return await _unitOfWork.ReportRepository.Get();
        }

        public async Task<IEnumerable<Report>> GetReportsByProductPostIdAsync(string productPostId)
        {
            return await _unitOfWork.ReportRepository.Get(r => r.ProductPostId == productPostId);
        }
    }
}
