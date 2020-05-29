using System.Web;
using System.Web.Mvc;

namespace BookRental
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
            filters.Add(new RequireHttpsAttribute()); // To disAllow Http on the app only Https is available now
        }
    }
}
