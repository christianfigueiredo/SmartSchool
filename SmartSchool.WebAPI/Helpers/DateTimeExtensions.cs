using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.Helpers
{
    public static class DateTimeExtensions
    {
        public static int GetCurrentAge(this DateTime dateTime)
        {
            var today = DateTime.Today;
            var age = today.Year - dateTime.Year;
            if (dateTime.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}