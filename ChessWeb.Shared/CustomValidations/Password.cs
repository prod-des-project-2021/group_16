using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ChessWeb.Shared.CustomValidations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class Password : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var pattern = @"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=.,]).*$";
            return Regex.IsMatch(Convert.ToString(value), pattern);
        }
    }
}
