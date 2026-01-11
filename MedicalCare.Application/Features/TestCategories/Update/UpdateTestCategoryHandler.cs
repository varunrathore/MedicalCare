using FluentValidation;
using MedicalCare.Application.Interfaces;

namespace MedicalCare.Application.Features.TestCategories.Update
{
    public class UpdateTestCategoryHandler
    {
        private readonly ITestCategoryRepository _repository;
        private readonly IValidator<UpdateTestCategoryCommand> _validator;
        public UpdateTestCategoryHandler(ITestCategoryRepository repository,
            IValidator<UpdateTestCategoryCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task HandleAsync(UpdateTestCategoryCommand command)
        {
            var result = await _validator.ValidateAsync(command);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            
            var category = await _repository.GetByIdAsync(command.Id);
            if (category == null)
                throw new Exception("Category not found");

            var existingCategory = await _repository.GetByNameAsync(command.Name);
            if (existingCategory != null && existingCategory.Id != command.Id)
                throw new InvalidOperationException(
                    "A category with the same name already exists");

            category.Name = command.Name;

            await _repository.UpdateAsync(category);
        }
    }
}
