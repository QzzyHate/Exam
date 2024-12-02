using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

//Kонтролер для проверки авторизации на других страницах
namespace Exam.Pages
{
    public class BaseController : PageModel
    {
        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var isAuth = HttpContext.Session.GetString("IsAuthenticated");
            if (isAuth != "true")
            {
                context.Result = new RedirectToPageResult("Auth");
            }
        }
    }
}