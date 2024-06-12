using SWP.FUGoodsExchangeManagement.Repository.DTOs.CampusDTOs;
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
        
    }
}
