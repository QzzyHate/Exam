using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Exam.Pages
{
    [IgnoreAntiforgeryToken]
    public class EditOrderModel : BaseController
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
            existingOrder.masterComment =  Order.masterComment;

            await _context.SaveChangesAsync();

            //Уведомление
            TempData["Message"] = $"Статус заявки номер {existingOrder.number} был изменён.";
            if (existingOrder.status == "выполнено")
            {
                TempData["Message"] = $"Заявка с номером {existingOrder.number} завершена.";
                existingOrder.dateCompleted = DateOnly.FromDateTime(DateTime.Now);
                await _context.SaveChangesAsync();
            }
            else
            {
                existingOrder.dateCompleted = null;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("GetOrders");
        }
    }
}