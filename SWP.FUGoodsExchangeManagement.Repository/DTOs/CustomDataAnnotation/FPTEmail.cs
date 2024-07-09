using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Utils.CustomDataAnnotation
{
    public class FPTEmail : ValidationAttribute
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^[\w!#$%&'*+\-/=?^_`{|}~]+(\.[\w!#$%&'*+\-/=?^_`{|}~]+)*"
            + @"@(fpt\.edu\.vn|fe\.edu\.vn)$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

        public FPTEmail() : base("E-mail must end with @fpt.edu.vn or @fe.edu.vn")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !EmailRegex.IsMatch(value.ToString()))
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
