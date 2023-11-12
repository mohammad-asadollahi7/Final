using System.Globalization;

namespace Endpoint.MVC.ExtensionMethods;

public static class ToPersianDateTimeExtension
{
    public static string ToPersian(this DateTime dateTime)
    {
        var persianCalendar = new PersianCalendar();

        return String.Format("{0}/{1}/{2}", 
                             persianCalendar.GetYear(dateTime),
                             persianCalendar.GetMonth(dateTime),
                             persianCalendar.GetDayOfMonth(dateTime));
    }
}
