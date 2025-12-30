using MedicalCare.Application.Interfaces;
using MedicalCare.Domain.Entities;
using MedicalCare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MedicalCare.Infrastructure.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public TestRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<Test>> GetAllAsync()
        { 
            return await _dbcontext.Tests.ToListAsync();
        }
        public async Task<Test?> GetByIdAsync(Guid id)
        {
            return await _dbcontext.Tests.FindAsync(id);
        }
        public async Task<Test?> GetByNameAsync(string name)
        {
            return await _dbcontext.Tests.FirstOrDefaultAsync(t => t.Name == name);
        }
        public async Task AddAsync(Test Test)
        {
            await _dbcontext.Tests.AddAsync(Test);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Test Test)
        {
            _dbcontext.Tests.Update(Test);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
