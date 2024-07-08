using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.ResponseModels
{
    public class UserListResponseModel
    {
        public string Id { get; set; } = null!;

        public string Fullname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string Status { get; set; } = null!;
    }

    public class UserResponseModel
    {
        public string Id { get; set; } = null!;

        public string Fullname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
    }
}
