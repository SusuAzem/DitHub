using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DitHub.ViewModels
{
    public class TimeAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var valid = DateTime.TryParseExact(Convert.ToString(value),
                "HH:MM",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out _);
            return (valid);
        }
    }
}
