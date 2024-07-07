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
    }
}
