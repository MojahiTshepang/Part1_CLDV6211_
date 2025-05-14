using System.Web;
using System.Web.Mvc;

namespace SlaezyBookingEventsApp1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
