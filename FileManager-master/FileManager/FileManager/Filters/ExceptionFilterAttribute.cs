using System.Web.Mvc;

namespace FileManager.Filters
{
    public class ExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ExceptionHandled)
            {
                exceptionContext.Result = new RedirectResult("/Shared/Error.cshtml");
                exceptionContext.ExceptionHandled = true;
            }
        }
    }
}