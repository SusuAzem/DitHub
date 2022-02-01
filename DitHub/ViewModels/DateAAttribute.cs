using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DitHub.ViewModels
{
    public class DateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var valid = DateTime.TryParseExact(Convert.ToString(value),
                "dd MMM yyyy",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out DateTime dateTime);
            return (valid && dateTime >= DateTime.Now);
        }
    }
}
