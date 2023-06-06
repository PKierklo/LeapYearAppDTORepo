using LeapYearApp.Data;
using LeapYearApp.Forms;
using ListLeapYears;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LeapYearApp.Services;
using System.Configuration;

namespace LeapYearApp.Pages
{
    public class ListModel : PageModel
    {
        public IEnumerable<LeapYear> LeapYearList;
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration Configuration;
        private readonly LeapYearInterface _DI;
        private readonly IHttpContextAccessor _contextAccessor;

        public ListModel(ILogger<IndexModel> logger, helper context, IConfiguration configuration, IHttpContextAccessor contextAccessor, LeapYearInterface DI)
        {
            _logger = logger;
            _DI = DI;
            Configuration = configuration;
            _contextAccessor = contextAccessor;
        }
        public LeapYear find { get; set; } = new LeapYear();
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<LeapYear> LeapYears { get; set; }
        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var user_id = _contextAccessor.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                find.user_id = user_id.Value;
                
            }
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_asc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            //nie mogê zamieniæ tego wywo³ania na korzystanie z serwisu :(

            var uzytkownicy = _context.LeapData.OrderByDescending(x => x.Data);

            var pageSize = Configuration.GetValue("PageSize", 20);
            var leapYearsList = await uzytkownicy.AsNoTracking().ToListAsync();
            LeapYears = PaginatedList<LeapYear>.CreateAsync(
                leapYearsList.AsQueryable(), pageIndex ?? 1, pageSize);
        }
        public IActionResult OnPost(int id_User)
        {
            _DI.Delete(id_User, find);

            return RedirectToAction("Async");
        }
    }
}