using MedicalCare.Domain.Entities;

namespace MedicalCare.Application.Interfaces
{
    public interface ITestCategoryRepository
    {
        Task<List<TestCategory>> GetAllAsync();
        Task<TestCategory?> GetByIdAsync(Guid id);
        Task<TestCategory?> GetByNameAsync(string name);
        Task AddAsync(TestCategory category);
        Task UpdateAsync(TestCategory category);
    }
}
