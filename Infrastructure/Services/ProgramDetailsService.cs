using Infrastructure.Contexts;
using Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProgramDetailsService
    {
        private readonly DataContext _context;

        public ProgramDetailsService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ProgramDetailsEntity>> CreateAsync(string courseId, List<ProgramDetailsEntity> details) // Changed courseId to string
        {
            foreach (var detail in details)
            {
                detail.CourseId = courseId; 
                _context.ProgramDetails.Add(detail);
            }
            await _context.SaveChangesAsync();
            return details;
        }
    }
}
