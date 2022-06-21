using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace BaseApplication.Helper
{
    public static class UtilitiesHelper
    {
        public static int GetAge(this DateTime? dateOfBirth)
        {
            if (dateOfBirth == null) return -1;
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Value.Year;
            //if (DateTime.Now.DayOfYear < dateOfBirth.Value.DayOfYear)
            //{
            //    age = age - 1;
            //}
            return age;
        }

        public static DateTime Add7Hour(DateTime d)
        {
            return d.AddHours(7);
        }

        public static DateTime SetZeroHour(this DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, 0, 0, 0,0);
        }

        public static long GetUnixTimestamp(this DateTime? d)
        {
            if (d.HasValue)
            {
                return ((DateTimeOffset)new DateTime(d.Value.Year, d.Value.Month, d.Value.Day, 0, 0, 0)).ToUnixTimeSeconds();
            }

            return 0;
        }
        public static DateTime ChangeTime(this DateTime dateTime, int hours = 0, int minutes = 0, int seconds = 0, int milliseconds = 0)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }
        public static DateTime? ConvertStringToDate(string input, string format = "dd/MM/yyyy")
        {
            try
            {
                DateTime d = DateTime.ParseExact(input, format,
                                  System.Globalization.CultureInfo.InvariantCulture);
                if (d.Year < 1900) return null;
                return d;
            }
            catch
            {
                return null;
            }
        }

        public static FullAuditedEntity SetDefaultInsertEntity(this FullAuditedEntity entity, Guid? userId)
        {
            entity.IsDeleted = false;
            //entity.CreationTime = DateTime.Now;
            //entity.CreatorId = userId;
            return entity;
        }

    }
}
