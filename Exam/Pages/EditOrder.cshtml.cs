using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Exam.Pages
{
    [IgnoreAntiforgeryToken]
    public class EditOrderModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditOrderModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var existingOrder = await _context.Orders.FirstOrDefaultAsync(o => o.number == Order.number);
            if (existingOrder == null)
                return NotFound($"Заявка номер {Order.number} не найдена.");

            existingOrder.description = Order.description == null ? existingOrder.description : Order.description;
            existingOrder.master = Order.master == null ? existingOrder.master : Order.master;
            existingOrder.status = Order.status == null ? existingOrder.status : Order.status;

            await _context.SaveChangesAsync();
            TempData["Message"] = $"Статус заявки номер {existingOrder.number} был изменён.";
            return RedirectToPage("GetOrders");
        }
    }
}
