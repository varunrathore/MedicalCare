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

        public async Task Handle(UpdateTestCategoryCommand command)
        {
            var category = await _repository.GetByIdAsync(command.Id);

            if (category == null)
                throw new Exception("Category not found");

            category.Name = command.Name;

            await _repository.UpdateAsync(category);
        }
    }
}
