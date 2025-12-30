using MedicalCare.Application.Interfaces;

namespace MedicalCare.Application.Features.TestCategories
{
    public class UpdateTestCategoryHandler
    {
        private readonly ITestCategoryRepository _repository;
        public UpdateTestCategoryHandler(ITestCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(UpdateTestCategoryCommand command)
        {
            var category = await _repository.GetByIdAsync(command.Id);

            if (category == null)
                throw new Exception("Category not found");

            if (string.IsNullOrWhiteSpace(command.Name))
                throw new ArgumentException("Category name cannot be empty");

            if (command.Name.Length < 3)
                throw new AggregateException("Category name must be at least 3 characters long");

            var existingCategory = await _repository.GetByNameAsync(command.Name);
            if (existingCategory != null)
                throw new InvalidOperationException("A category with the same name already exists");

            category.Name = command.Name;

            await _repository.UpdateAsync(category);
        }
    }
}
