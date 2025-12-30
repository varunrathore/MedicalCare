using MedicalCare.Application.Interfaces;
using MedicalCare.Domain.Entities;

namespace MedicalCare.Application.Features.TestCategories
{
    public class CreateTestCategoryHandler
    {
        private readonly ITestCategoryRepository _repository;
        public CreateTestCategoryHandler(ITestCategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(CreateTestCategoryCommand command)
        {
            var category = new TestCategory
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                IsActive = true
            };
            await _repository.AddAsync(category);
        }

    }
}
