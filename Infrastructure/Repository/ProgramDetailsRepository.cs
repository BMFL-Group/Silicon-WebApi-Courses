using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        #region CREATE
        public async Task<ProgramDetailsEntity> AddProgramDetailAsync(ProgramDetailsEntity programDetail)
        {
            await _context.Set<ProgramDetailsEntity>().AddAsync(programDetail);            
            try
            {
                await _context.SaveChangesAsync();
                return programDetail;
            }
            catch (Exception ex)
            {
                throw new Exception("Database operation failed", ex);
            }
        }
        #endregion

        #region GET
        public async Task<ProgramDetailsEntity> GetProgramDetailAsync(string id)
        {
            return await _context.ProgramDetails.FindAsync(id);
        }
        #endregion

        #region GET ALL
        public async Task<List<ProgramDetailsEntity>> GetAllProgramDetailsAsync()
        {
            return await _context.ProgramDetails.ToListAsync();
        }
        #endregion

        #region UPDATE
        public async Task<bool> UpdateProgramDetailAsync(ProgramDetailsEntity programDetail)
        {
            try
            {
                _context.ProgramDetails.Update(programDetail);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramDetailExists(programDetail.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }
        #endregion

        #region DELETE
        public async Task<bool> DeleteProgramDetailAsync(string id)
        {
            var programDetail = await _context.ProgramDetails.FindAsync(id);
            if (programDetail == null)
            {
                return false;
            }
            _context.ProgramDetails.Remove(programDetail);
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

        #region EXISTS
        private bool ProgramDetailExists(int id)
        {
            return _context.ProgramDetails.Any(x => x.Id == id);
        }
        #endregion
    }
}