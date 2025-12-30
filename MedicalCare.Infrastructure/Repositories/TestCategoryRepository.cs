using MedicalCare.Application.Interfaces;
using MedicalCare.Domain.Entities;
using MedicalCare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace MedicalCare.Infrastructure.Repositories
{
    public class TestCategoryRepository : ITestCategoryRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public TestCategoryRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<TestCategory>> GetAllAsync()
        {
            return await _dbcontext.TestCategories.ToListAsync();
        }
        public async Task<TestCategory?> GetByIdAsync(Guid id)
        {
            return await _dbcontext.TestCategories.FindAsync(id);
        }
        public async Task AddAsync(TestCategory category)
        {
            await _dbcontext.TestCategories.AddAsync(category);
            await  _dbcontext.SaveChangesAsync();
        }
        public async Task UpdateAsync(TestCategory category)
        {
            _dbcontext.TestCategories.Update(category);
            await _dbcontext.SaveChangesAsync();

        }
    }
}
