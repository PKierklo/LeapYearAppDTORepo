using LeapYearApp.Forms;
using LeapYearApp.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using LeapYearApp.Interfaces;
using LeapYearApp.ViewModels;
namespace LeapYearApp.Services
{
    public class LeapYearService : LeapYearInterface
    {
        private readonly helper _context;

        public LeapYearService(helper context)
        {
            _context = context;
        }

        public async Task<List<LeapYear>> GetAllYears()
        {
            return await _context.LeapData.ToListAsync();
        }

        public async Task<LeapYear> GetYear(int id)
        {
            return await _context.LeapData.FindAsync(id);
        }

        public async Task Create(LeapYear rok)
        {
            _context.LeapData.Add(rok);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id, LeapYear y)
        {
            y = _context.LeapData.Find(id);
            _context.LeapData.Remove(y);
            await _context.SaveChangesAsync();

        }

    }
}
