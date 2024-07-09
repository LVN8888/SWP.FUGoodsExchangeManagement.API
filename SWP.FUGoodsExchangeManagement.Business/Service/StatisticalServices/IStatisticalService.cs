using SWP.FUGoodsExchangeManagement.Repository.DTOs.StatisticalDTOs.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.StatisticalServices
{
    public interface IStatisticalService
    {
        Task<SummaryResponseModel> GetSummaryAsync();
        Task<IEnumerable<PostModeStatisticsResponseModel>> GetPostModeStatisticsAsync(DateTime? startDate, DateTime? endDate);
        Task<IEnumerable<UserPurchaseStatisticsResponseModel>> GetUserPurchaseStatisticsAsync(DateTime? startDate, DateTime? endDate);
    }
}
