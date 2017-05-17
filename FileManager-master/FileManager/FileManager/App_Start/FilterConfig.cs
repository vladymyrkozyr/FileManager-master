using System.Web.Mvc;
using FileManager.Filters;

namespace FileManager
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionFilterAttribute());
        }
    }
}
