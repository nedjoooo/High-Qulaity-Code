namespace OfficeTicket.Utilities
{
    using System;
    using System.Globalization;

    public static class Utils
    {
        public static DateTime ParseDateTime(string dateTime)
        {
            DateTime result = DateTime.ParseExact(dateTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            return result;
        }
    }
}
