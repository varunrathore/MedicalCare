using MedicalCare.Domain.Entities;

namespace MedicalCare.Application.Interfaces
{
    public interface ITestCategoryRepository
    {
        Task<List<TestCategory>> GetAllAsync();
        Task<TestCategory?> GetByIdAsync(Guid id);
        Task AddAsync(TestCategory category);
        Task UpdateAsync(TestCategory category);
    }
}
