using FluentValidation;
using MedicalCare.Application.Interfaces;
using MedicalCare.Domain.Entities;

namespace MedicalCare.Application.Features.TestCategories.Create
{
    public class CreateTestCategoryHandler
    {
        private readonly ITestCategoryRepository _repository;
        private readonly IValidator<CreateTestCategoryCommand> _validator;
        public CreateTestCategoryHandler(ITestCategoryRepository repository, IValidator<CreateTestCategoryCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task HandleAsync(CreateTestCategoryCommand command)
        {
            var result = await _validator.ValidateAsync(command);
            if(!result.IsValid)
                throw new ValidationException(result.Errors);
            
            var existingCategory = await _repository.GetByNameAsync(command.Name);
            if (existingCategory != null)
                throw new Exception("A category with the same name already exists");

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
