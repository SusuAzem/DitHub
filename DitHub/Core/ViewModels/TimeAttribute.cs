﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DitHub.Core.ViewModels
{
    public class TimeAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var valid = DateTime.TryParseExact(Convert.ToString(value),
                "HH:mm",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out _);
            return (valid);
        }
    }
}
