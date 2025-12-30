using MedicalCare.Application.Interfaces;
using MedicalCare.Domain.Entities;

namespace MedicalCare.Application.Features.TestCategories
{
    public class GetAllTestCategoriesHandler
    {
        private readonly ITestCategoryRepository _repository;
        public GetAllTestCategoriesHandler(ITestCategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<TestCategory>> HandleAsync()
        {
            return await _repository.GetAllAsync();
        }

    }
}
