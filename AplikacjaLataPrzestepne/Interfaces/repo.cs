using LeapYearApp.Forms;
using System;

namespace LeapYearApp.Interfaces
{
    public interface repo
    {
        IQueryable<LeapYear> GetActive();
    }
}