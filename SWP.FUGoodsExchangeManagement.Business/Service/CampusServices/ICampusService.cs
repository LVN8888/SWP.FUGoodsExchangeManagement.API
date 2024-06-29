using SWP.FUGoodsExchangeManagement.Repository.DTOs.CampusDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.CampusDTOs.ResponseModels;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.CampusServices
{
    public interface ICampusService
    {
        Task AddCampusAsync(AddCampusDTO addCampusDto);
        Task EditCampusAsync(EditCampusDTO editCampusDto);
        Task DeleteCampusAsync(DeleteCampusDTO deleteCampusDTO);
        Task<IEnumerable<CampusResponseModel>> GetAllCampusesAsync(int pageIndex, int pageSize);
    }
}
