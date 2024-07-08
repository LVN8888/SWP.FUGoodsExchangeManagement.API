using SWP.FUGoodsExchangeManagement.Repository.Enums;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.ReportServices
{
    public interface IReportService
    {
        Task CreateReportAsync(string productPostId, string createdBy, List<ReportReasonEnums> reasons);
        Task ApproveReportAsync(string reportId);
        Task DeclineReportAsync(string reportId);
        Task<IEnumerable<Report>> GetAllReportsAsync();
        Task<IEnumerable<Report>> GetReportsByProductPostIdAsync(string productPostId);
    }
}
