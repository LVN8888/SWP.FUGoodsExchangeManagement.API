using SWP.FUGoodsExchangeManagement.Repository.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.RequestModels
{
    public class UserRoleChangeRequestModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [EnumDataType(typeof(RoleEnums), ErrorMessage = "Invalid role.")]
        public string NewRole { get; set; }
    }
}
