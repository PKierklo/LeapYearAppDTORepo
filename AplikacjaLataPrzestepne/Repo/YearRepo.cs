using LeapYearApp.Data;
using LeapYearApp.Forms;
using LeapYearApp.Interfaces;
using System;
namespace LeapYearApp.Repo
{
    public class YearRepo
    {
        private readonly helper _context;
        public YearRepo(helper context)
        {
            _context = context;
        }
        public IQueryable<LeapYear> GetActive()
        {
            return _context.LeapData;
        }
    }
}
