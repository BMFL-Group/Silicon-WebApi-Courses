using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ProgramDetailsRepository
    {
        private readonly DataContext _context;

        public ProgramDetailsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ProgramDetailsEntity> AddProgramDetailAsync(ProgramDetailsEntity programDetail)
        {
            _context.Set<ProgramDetailsEntity>().Add(programDetail);
            await _context.SaveChangesAsync();
            return programDetail;
        }
    }
}