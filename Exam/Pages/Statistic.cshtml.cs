using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Exam.Pages
{
    public class StatisticModel : BaseController
    {
        private readonly AppDbContext _context;

        public StatisticModel(AppDbContext context)
        {
            _context = context;
        }

        public int complitedOrdersCount { get; set; }
        public double avgCompleteOrderTime { get; set; }
        public Dictionary<string, int> problemStats { get; set; } = new();

        public async Task OnGetAsync()
        {
            //���������� ����������� ������
            complitedOrdersCount = await _context.Orders
                .CountAsync(o => o.dateCompleted != null);

            //������� ����� ���������� ������
            avgCompleteOrderTime = _context.Orders
                .Where(o => o.dateCompleted != null)
                .AsEnumerable() // ��������� ������ �� ����
                .Select(o => o.dateCompleted.Value.DayNumber - o.dateAdded.DayNumber)
                .Average();


            //���������� �� �����
            problemStats = await _context.Orders
                .GroupBy(o => o.problemType.ToLower())
                .Select(group => new { ProblemType = group.Key, Count = group.Count() })
                .ToDictionaryAsync(g => g.ProblemType, g => g.Count);
        }
    }
}