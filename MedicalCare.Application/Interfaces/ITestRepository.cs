using MedicalCare.Domain.Entities;

namespace MedicalCare.Application.Interfaces
{
    public interface ITestRepository
    {
        Task<List<Test>> GetAllAsync();
        Task<List<Test>> GetAllWithCategoryAsync();
        Task<Test?> GetByIdAsync(Guid id);
        Task<Test?> GetByNameAsync(string name);
        Task AddAsync(Test test);
        Task UpdateAsync(Test test);
    }
}
