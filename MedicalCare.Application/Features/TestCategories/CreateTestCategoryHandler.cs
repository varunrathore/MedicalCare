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
            if(string.IsNullOrWhiteSpace(command.Name))
                throw new ArgumentException("Category name cannot be empty");

            if(command.Name.Length < 3)
                throw new AggregateException("Category name must be at least 3 characters long");

            var existingCategory = await _repository.GetByNameAsync(command.Name);
            if (existingCategory != null)
                throw new InvalidOperationException("A category with the same name already exists");

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
