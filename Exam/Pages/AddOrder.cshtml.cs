using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Exam.Pages
{
    [IgnoreAntiforgeryToken]
    public class AddOrderModel : BaseController
    {

        private readonly AppDbContext _context;

        public AddOrderModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; } = new Order();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("GetOrders");
        }
    }
}