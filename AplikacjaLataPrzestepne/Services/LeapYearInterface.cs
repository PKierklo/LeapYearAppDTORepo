using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeapYearApp.Forms;
namespace LeapYearApp.Services
{
    public interface LeapYearInterface
    {

        Task<List<LeapYear>> GetAllYears();
        Task<LeapYear> GetYear(int id);
        Task Create(LeapYear rok);
        Task Delete(int id, LeapYear r);

    }
}
