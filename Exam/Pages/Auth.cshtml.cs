using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exam.Pages
{
    public class AuthModel : PageModel
    {
        private const string _correctLogin = "capybara";
        private const string _correctPassword = "1234";


        [BindProperty]
        public string login { get; set; }
        [BindProperty]
        public string password { get; set; }

        public IActionResult OnPost()
        {
            if (login == _correctLogin && password == _correctPassword)
            {
                HttpContext.Session.SetString("IsAuthenticated", "true"); //Устанавливаем статус авторизации.
                return RedirectToPage("GetOrders");
            }

            return Page();
        }
    }
}